using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class r5RightTrigger : MonoBehaviour {

    bool flag;
    GameObject r5Right;
    //右参考点
    Transform RightReferenceParent;
    BoxCollider boxCollider;
	void Start () {
        flag = false;
        //找到当前物体的碰撞体
        boxCollider = GetComponent<BoxCollider>();
	}
	
	
	void Update () {
        if (flag && r5Right != null)
        {
            //调整r5Rigth的位置
            r5Right.transform.position =transform.position;
            //调整r5Rigth的角度
            //调整角度
            r5Right.transform.eulerAngles = transform.eulerAngles;
            //使碰撞体不可用
            boxCollider.enabled = false;

            //禁用r4Right的同步脚本,因为r5Right已经装配
            //GameObject.Find("r4_right").GetComponent<ClientGameObjectMethod>().enabled = false;
        }
	}

    void OnTriggerEnter(Collider obj)
    {
        
            //如果r4Right还没有装配，则r5Right不能装配
           if (!CommonParameter.ComponentTriggerNameList.Contains("r4RightTrigger"))
                return;

           if (obj.gameObject.name.Equals(objNameContanst.r5_right) || obj.gameObject.name.Equals(objNameContanst.r5_left))
           {
               //只控制执行一次
               if(!flag){
                  
                   //触发到的仪器部件r5Right的碰撞体不可用
                   obj.gameObject.GetComponent<BoxCollider>().enabled = false;
                   
                   r5Right = obj.gameObject;
                   Debug.Log("r5Right触发");
                   //如果存在刚体，则移除刚体
                   Rigidbody rigibody = obj.gameObject.GetComponent<Rigidbody>();
                   if (rigibody != null)
                   {
                       Destroy(rigibody);
                   }
                   
                   ScaleTiltControl scaletilt = GameObject.Find("ScriptControl").GetComponent<ScaleTiltControl>();
                   scaletilt.m_Right += 1;
                   flag = true;



                   //把当前游戏物体的名称添加到触发器名称集合中
                   CommonParameter.ComponentTriggerNameList.Add(gameObject.name);


                   InterferenceMatrixManager.Instance.RemoveCurrentComponent("r5_right");

                   //从干涉矩阵中查出下一个动画的名称，然后进行播放该动画
                   string NextAnimationName = InterferenceMatrixManager.Instance.ReturnCurrentAssembleName();
                   //播放下一个动画
                   AnimationManager.Instance.PlayNextAnimation(NextAnimationName);
 

               }

           }
    }
}
