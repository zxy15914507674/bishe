using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class r3Trigger : MonoBehaviour {

    GameObject r3;
    bool flag = false;
    bool setParentFlag = true;
    Transform r1;
    BoxCollider boxCollider;
	void Start () {
        //找到当前物体的碰撞体
        boxCollider = GetComponent<BoxCollider>();
	}

    void Update()
    {
        if(flag&&r3!=null){
            //调整r3位置
            r3.transform.position =transform.position;
            //调整r3角度
            r3.transform.eulerAngles = transform.eulerAngles;

            //只执行一次
            if(setParentFlag){
                Transform RightReferenceParent = GameObject.Find("RightReferenceParent").GetComponent<Transform>();
                Transform LeftReferenceParent = GameObject.Find("LeftReferenceParent").GetComponent<Transform>();
                //把左右参考点设为r3的子物体
                RightReferenceParent.parent = r3.transform;
                LeftReferenceParent.parent = r3.transform;
                setParentFlag = false;
                //使碰撞体不可用
                boxCollider.enabled = false;
                //禁用r2的同步脚本,因为r3已经装配
                //GameObject.Find("r2").GetComponent<ClientGameObjectMethod>().enabled = false;
            }
        }
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.name.Equals(objNameContanst.r3))
        {
           
            //触发到的仪器部件r3的碰撞体不可用
            obj.gameObject.GetComponent<BoxCollider>().enabled = false;
            r3 = obj.gameObject;
            Debug.Log("r3触发");
            flag = true;
            //如果存在刚体，则移除刚体
            Rigidbody rigibody = obj.gameObject.GetComponent<Rigidbody>();
            if (rigibody != null)
            {
                Destroy(rigibody);
            }

           
            //把当前游戏物体对象的名称添加到触发器名称集合中
            CommonParameter.ComponentTriggerNameList.Add(gameObject.name);

            InterferenceMatrixManager.Instance.RemoveCurrentComponent("r3");

            //从干涉矩阵中查出下一个动画的名称，然后进行播放该动画
            string NextAnimationName = InterferenceMatrixManager.Instance.ReturnCurrentAssembleName();
            //播放下一个动画
            AnimationManager.Instance.PlayNextAnimation(NextAnimationName);
 

        }
    }
}
