using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class r2Trigger : MonoBehaviour {
    GameObject r2;
    bool flag = false;
    BoxCollider boxCollider;
    void Start () {
       //找到当前物体的碰撞体
        boxCollider = GetComponent<BoxCollider>();
    }


    void Update()
    {
        if(flag&&r2!=null){
            //调整位置
            r2.transform.position = transform.position;
            //调整角度
            r2.transform.eulerAngles = transform.eulerAngles;
            //使碰撞体不可用
            boxCollider.enabled = false;

            //禁用r2的同步脚本
           // r2.GetComponent<ClientGameObjectMethod>().enabled = false;
            
        }
    }
    void OnTriggerEnter(Collider obj) {
        if(obj.gameObject.name.Equals(objNameContanst.r2)){
            
            //触发到的仪器部件r2的碰撞体不可用
            obj.gameObject.GetComponent<BoxCollider>().enabled = false;
         
            r2 = obj.gameObject;
            Debug.Log("r2触发");
            flag = true;
            //如果存在刚体，则移除刚体
            Rigidbody rigibody = obj.gameObject.GetComponent<Rigidbody>();
            if (rigibody != null)
            {
                Destroy(rigibody);
            }

            InterferenceMatrixManager.Instance.RemoveCurrentComponent("r2");

            //从干涉矩阵中查出下一个动画的名称，然后进行播放该动画
            string NextAnimationName = InterferenceMatrixManager.Instance.ReturnCurrentAssembleName();
            //播放下一个动画
            AnimationManager.Instance.PlayNextAnimation(NextAnimationName);
 
            
           
        }
    }
}
