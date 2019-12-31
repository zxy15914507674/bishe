﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class r4LeftTrigger : MonoBehaviour {

    bool flag;
    GameObject r4Left;
    //左参考点
    Transform LeftReferenceParent;
    BoxCollider boxCollider;
	void Start () {
        flag = false;
        //找到当前物体的碰撞体
        boxCollider = GetComponent<BoxCollider>();
	}
	
    
    void Update () {
		if(flag&&r4Left!=null){
            //调整r4Left的位置
            r4Left.transform.position =transform.position;
            //调整r4Left的角度
            //调整角度
            r4Left.transform.eulerAngles = transform.eulerAngles;
            //使碰撞体不可用
            boxCollider.enabled = false;

            //禁用r3的同步脚本,因为r4Left已经装配
            //GameObject.Find("r3").GetComponent<ClientGameObjectMethod>().enabled = false;
        }
    }

    void OnTriggerEnter(Collider obj)
    {
        
            //如果r3还没有装配，则不能装配r4
            if (!CommonParameter.ComponentTriggerNameList.Contains("r3Trigger"))
                    return;
                
            if ((obj.gameObject.name.Equals(objNameContanst.r4_left))||(obj.gameObject.name.Equals(objNameContanst.r4_right)))
            {
                //控制只触发一次
                if(!flag){
                    //触发到的仪器部件r4Left的碰撞体不可用
                    obj.gameObject.GetComponent<BoxCollider>().enabled = false;
                    r4Left = obj.gameObject;
                    Debug.Log("r4Left触发+flag" + flag);
                    //如果存在刚体，则移除刚体
                    Rigidbody rigibody = obj.gameObject.GetComponent<Rigidbody>();
                    if (rigibody != null)
                    {
                        Destroy(rigibody);
                    }

                    flag = true;

                    //把当前游戏物体对象的名称添加到触发器名称集合中
                    CommonParameter.ComponentTriggerNameList.Add(gameObject.name);


                    InterferenceMatrixManager.Instance.RemoveCurrentComponent("r4_left");

                    //从干涉矩阵中查出下一个动画的名称，然后进行播放该动画
                    string NextAnimationName = InterferenceMatrixManager.Instance.ReturnCurrentAssembleName();
                    //播放下一个动画
                    AnimationManager.Instance.PlayNextAnimation(NextAnimationName);
 
                }

                
            }
           
        
    }
}
