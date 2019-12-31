/****
 * 
 *  在服务端生成所有同步的游戏物体 
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CreateServerObject : NetworkBehaviour {
    public GameObject []createObjectList;

    public Vector3[] createObjectListPosition;
    public override void OnStartServer()
    {
       
        if(createObjectList.Length==0||createObjectListPosition.Length==0){
            return;
        }

        if(createObjectListPosition.Length!=createObjectList.Length){
            return;
        }
        //逐一生成游戏物体并同步到各个客户端
        for (int index = 0; index < createObjectList.Length;index++)
        {
            GameObject objClone = GameObject.Instantiate(createObjectList[index] as GameObject);
            objClone.transform.position=createObjectListPosition[index];
            objClone.name = createObjectList[index].name;
            NetworkServer.Spawn(objClone);
        }
    }
}
