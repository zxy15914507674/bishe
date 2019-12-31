/****
 * 
 *  移除游戏物体名称中的后缀Clone 和本地客户端需要同步的方法
 * 
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ClientGameObjectMethod : NetworkBehaviour
{

    [SyncVar]
    public  Vector3 v3_Pos;

    [SyncVar]
    public  Vector3 qua_Rotate;


    GameObject isLocalHand;
    void Start()
    {
      //去掉游戏物体的后缀(Clone)
      string gameObjectName = gameObject.name;
      if (gameObjectName.Contains("(Clone)"))
      {
           gameObjectName = gameObjectName.Replace("(Clone)", "").Trim();
           gameObject.name = gameObjectName;
      }
      v3_Pos = transform.position;
      qua_Rotate = transform.eulerAngles;
    }


    void Update()
    {
        if(isClient){
           
            //判断是否是本地操作
            GameObject localPlayer = GameObject.Find("Hand");
            if (localPlayer == null)
            {
                return;
            }
            //判断本地手模型是否抓取物体,并且是抓到了本游戏物体
            HandControl objHandControl = localPlayer.GetComponent<HandControl>();
            if (objHandControl.hasCatched != null && objHandControl.hasCatched.name == gameObject.name)
            {
                //通过本地游戏物体发送当前游戏物体同步位置和旋转信息到服务端执行，进入同步到各个客户端
                SendToServerByPlayer objSend = localPlayer.GetComponent<SendToServerByPlayer>();
                v3_Pos = transform.position;
                qua_Rotate = transform.eulerAngles;
                objSend.CmdMove(objHandControl.hasCatched.name, v3_Pos, qua_Rotate);

            }
            else
            {
                SetPosition();
            }
        }    
    }

       
    //插值移动
    void SetPosition()
    {
        transform.position = v3_Pos;
        transform.eulerAngles=qua_Rotate;
    }



}
