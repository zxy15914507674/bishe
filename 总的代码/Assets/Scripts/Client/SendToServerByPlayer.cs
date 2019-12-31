using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendToServerByPlayer : NetworkBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //客户端发起，服务端执行，通过移动和旋转服务端的游戏物体进而同步到各个客户端
    [Command]
    public void CmdMove(string hasCatchName, Vector3 position, Vector3 eulerAngles)
    {
        
        //GameObject.Find(hasCatchName).transform.position =position;
        //GameObject.Find(hasCatchName).transform.eulerAngles = eulerAngles;
        ClientGameObjectMethod objClientMethod = GameObject.Find(hasCatchName).GetComponent<ClientGameObjectMethod>();
        objClientMethod.v3_Pos = position;
        objClientMethod.qua_Rotate = eulerAngles;

    }


    //砝码同步的方法,客户端发起，服务端执行，通过移动和旋转服务端的游戏物体进而同步到各个客户端
    [Command]
    public void CmdWeightMove(string hasCatchName, Vector3 position, Vector3 eulerAngles)
    {

        //GameObject.Find(hasCatchName).transform.position =position;
        //GameObject.Find(hasCatchName).transform.eulerAngles = eulerAngles;
        ClientWeightObjectMethod objClientMethod = GameObject.Find(hasCatchName).GetComponent<ClientWeightObjectMethod>();
        objClientMethod.v3_Pos = position;
        objClientMethod.qua_Rotate = eulerAngles;

    }
}
