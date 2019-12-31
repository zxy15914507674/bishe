/****
 * 
 * 
 *  第一人称视角，摄像机跟随 
 * 
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FirstPersonView : NetworkBehaviour {
    //前后移动的速度
    float moveSpeed;
    //上下移动的速度
    float UpDownSpeed;
    //旋转速度
    float RotateSpeed;
	void Start () {
        if(isLocalPlayer){
            moveSpeed = Time.deltaTime * 2f;
            UpDownSpeed = 0.01f;
            RotateSpeed = 10f;
            transform.position = new Vector3(-4.32f, 1.55f, 1.85f);
           // Debug.Log("FirstPersonView: isClient: " + isClient + "  isServer:" + isServer + "  isLocalPlayer: " + isLocalPlayer);
            //设置摄像机跟随
            GameObject.Find("Camera").transform.parent = transform.transform;
        }
	}


    void Update()
    {
        if(isLocalPlayer){
            #region 控制手模型在一定的范围内移动
            if (transform.position.y < PositionErrorContanst.y_Down)
                transform.position = new Vector3(transform.position.x, PositionErrorContanst.y_Down, transform.position.z);

            if (transform.position.x > PositionErrorContanst.x_Left)
                transform.position = new Vector3(PositionErrorContanst.x_Left, transform.position.y, transform.position.z);

            if (transform.position.x < PositionErrorContanst.x_Right)
                transform.position = new Vector3(PositionErrorContanst.x_Right, transform.position.y, transform.position.z);

            if (transform.position.z < PositionErrorContanst.z_Back)
                transform.position = new Vector3(transform.position.x, transform.position.y, PositionErrorContanst.z_Back);

            if (transform.position.z > PositionErrorContanst.z_Forward)
                transform.position = new Vector3(transform.position.x, transform.position.y, PositionErrorContanst.z_Forward);
            #endregion

            //水平方向
            float horizantal = Input.GetAxis("Horizontal");
            //垂直方向
            float vertical = Input.GetAxis("Vertical");
            transform.Rotate(Vector3.up, horizantal * RotateSpeed, Space.Self);
            transform.Translate(new Vector3(0f, 0f, -vertical * moveSpeed), Space.Self);

            //上下移动

            //向上移动
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Translate(new Vector3(0f, Time.deltaTime, 0f), Space.Self);
            }

            //向下移动
            if (Input.GetKey(KeyCode.E))
            {
                transform.Translate(new Vector3(0f, -Time.deltaTime, 0f), Space.Self);
            }
        }
    }
}
