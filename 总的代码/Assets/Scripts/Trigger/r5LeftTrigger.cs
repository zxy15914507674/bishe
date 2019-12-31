using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class r5LeftTrigger : MonoBehaviour {

    bool flag;
    GameObject r5Left;
    //左参考点
    Transform LeftReferenceParent;
    BoxCollider boxCollider;
	void Start () {
        flag = false;
        //找到当前物体的碰撞体
        boxCollider = GetComponent<BoxCollider>();
	}
	
	
	void Update () {
        if (flag && r5Left != null)
        {
            //调整r5Left的位置
            r5Left.transform.position =transform.position;
            //调整r5Left的角度
            //调整角度
            r5Left.transform.eulerAngles = transform.eulerAngles;
            //使碰撞体不可用
            boxCollider.enabled = false;


            //禁用r4Left的同步脚本,因为r5Left已经装配
            //GameObject.Find("r4_left").GetComponent<ClientGameObjectMethod>().enabled = false;
        }
	}

    void OnTriggerEnter(Collider obj)
    {

            //如果r4Left还没有装配，则r5Left不能装配
            if (!CommonParameter.ComponentTriggerNameList.Contains("r4LeftTrigger"))
                return;

            if (obj.gameObject.name.Equals(objNameContanst.r5_left) || obj.gameObject.name.Equals(objNameContanst.r5_right))
            {
                //控制只执行一次
                if(!flag){
                    
                    //触发到的仪器部件r5Left的碰撞体不可用
                    obj.gameObject.GetComponent<BoxCollider>().enabled = false;
                    
                    r5Left = obj.gameObject;
                    Debug.Log("r5Left触发");
                    
                    //如果存在刚体，则移除刚体
                    Rigidbody rigibody = obj.gameObject.GetComponent<Rigidbody>();
                    if (rigibody != null)
                    {
                        Destroy(rigibody);
                    }
                    
                    ScaleTiltControl scaletilt = GameObject.Find("ScriptControl").GetComponent<ScaleTiltControl>();
                    scaletilt.m_Left += 1;
                    flag = true;
                    

                    //把当前游戏物体的名称添加到触发器名称集合中
                    CommonParameter.ComponentTriggerNameList.Add(gameObject.name);


                    InterferenceMatrixManager.Instance.RemoveCurrentComponent("r5_left");

                    //从干涉矩阵中查出下一个动画的名称，然后进行播放该动画
                    string NextAnimationName = InterferenceMatrixManager.Instance.ReturnCurrentAssembleName();
                    //播放下一个动画
                    AnimationManager.Instance.PlayNextAnimation(NextAnimationName);
 
                    
                }
                
            }
        
    }
}
