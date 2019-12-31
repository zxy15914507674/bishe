using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class r6LeftTrigger : MonoBehaviour {

    bool flag;
    public GameObject r6Left;
    BoxCollider boxCollider;
    bool HasWeigthEnter;          //砝码是否进入盘中的标志位
    //左参考点
    Transform LeftReferenceParent;
	void Start () {
        flag = false;
        //找到当前物体的碰撞体
        boxCollider = GetComponent<BoxCollider>();
        HasWeigthEnter = false;
	}
	
	
	void Update () {
        if (flag && r6Left != null)
        {
            //调整r6Left的位置
            r6Left.transform.position = transform.position;
            //调整r6Left的角度
            //调整角度
            r6Left.transform.eulerAngles = transform.eulerAngles;
            //使碰撞体不可用
            //boxCollider.enabled = false;


            //禁用r5Left的同步脚本,因为r6Left已经装配
            //GameObject.Find("r5_left").GetComponent<ClientGameObjectMethod>().enabled = false;
        }
	}


    /// <summary>
    /// 禁用所有天平仪器部件的同步脚本
    /// </summary>
    void UnableClientObjectMethod() {
        GameObject.Find("r2").GetComponent<ClientGameObjectMethod>().enabled = false;
        GameObject.Find("r3").GetComponent<ClientGameObjectMethod>().enabled = false;
        GameObject.Find("r4_left").GetComponent<ClientGameObjectMethod>().enabled = false;
        GameObject.Find("r4_right").GetComponent<ClientGameObjectMethod>().enabled = false;
        GameObject.Find("r5_left").GetComponent<ClientGameObjectMethod>().enabled = false;
        GameObject.Find("r5_right").GetComponent<ClientGameObjectMethod>().enabled = false;
        GameObject.Find("r6_left").GetComponent<ClientGameObjectMethod>().enabled = false;
        GameObject.Find("r6_right").GetComponent<ClientGameObjectMethod>().enabled = false;
        
    }

    void OnTriggerEnter(Collider obj)
    {

        //如果r5Left还没有装配，则不能装配r6Left
        if (!CommonParameter.ComponentTriggerNameList.Contains("r5LeftTrigger")) return;


        if (obj.gameObject.name.Equals(objNameContanst.r6_left) || obj.gameObject.name.Equals(objNameContanst.r6_right))
        {
            //控制只触发一次
            if(!flag){
                
                r6Left = obj.gameObject;
                Debug.Log("r6Left触发");
                
                //如果存在刚体，则移除刚体
                Rigidbody rigibody=obj.gameObject.GetComponent<Rigidbody>();
                if(rigibody!=null){
                    Destroy(rigibody);
                }

               
                //向左倾斜
                //Scaletilt scaletilt = GameObject.Find("ScriptControl").GetComponent<Scaletilt>();
                //scaletilt.LeftTilt();

                ScaleTiltControl scaletilt = GameObject.Find("ScriptControl").GetComponent<ScaleTiltControl>();
                scaletilt.m_Left += 1;
                flag = true;

                //把当前游戏物体的名称添加到触发器名称集合中
                CommonParameter.ComponentTriggerNameList.Add(gameObject.name);


                InterferenceMatrixManager.Instance.RemoveCurrentComponent("r6_left");

                //从干涉矩阵中查出下一个动画的名称，然后进行播放该动画
                string NextAnimationName = InterferenceMatrixManager.Instance.ReturnCurrentAssembleName();
                //播放下一个动画
                AnimationManager.Instance.PlayNextAnimation(NextAnimationName);
 
            }
        }


        if (!CommonParameter.ComponentTriggerNameList.Contains("r6LeftTrigger")) return;

        //硬币触发到天平左盘，把硬币放进天平左盘，并添加左盘的质量
        //GameObject hasCathced = GameObject.Find("Hand").GetComponent<HandControl>().hasCatched;
        //if (hasCathced == null) return;
        //手模型抓住了硬币并且硬币触发到天平的左盘
        //if (hasCathced.name == "Coin" && obj.gameObject.name == "Coin"
        //    //&& obj.gameObject.transform.parent == GameObject.Find("Hand").GetComponent<HandControl>().gameObject.transform
        //    &&!HasWeigthEnter)
        if (obj.gameObject.name == "Coin"
           && !HasWeigthEnter)
        {
            if (GameObject.Find("Hand").GetComponent<SendToServerByPlayer>().isClient)
            {
                UnableClientObjectMethod();
                //把硬币放进天平的左盘，即把硬币设为CoinColliderLeft的子物体，并调节硬币的位置
                obj.gameObject.transform.parent = gameObject.transform;
                Debug.Log("硬币碰撞到左盘");
                //调整硬币的位置
                obj.gameObject.transform.localPosition = new Vector3(0f, 0.02f, 0f);
                //调整硬币的旋转角度
                obj.gameObject.transform.localEulerAngles = new Vector3(0f,0f,0f);

                //把硬币的质量作用在天平的左盘上
                ScaleTiltControl scaletilt = GameObject.Find("ScriptControl").GetComponent<ScaleTiltControl>();
                scaletilt.m_Left += Convert.ToInt32(obj.gameObject.GetComponent<Rigidbody>().mass);
                Debug.Log("m_Right=" + scaletilt.m_Right + "  " + "scaletilt.m_Left=" + scaletilt.m_Left);
                HasWeigthEnter = true;


                PlayAnimation();
            }
           
        }
    }


    /// <summary>
    /// 播放硬币放到左盘上的动画
    /// </summary>
    private void PlayAnimation() { 
        if(!InterferenceMatrixManager.Instance.GetHasAssemblyComponentName().Contains("Coin")){
            InterferenceMatrixManager.Instance.RemoveCurrentComponent("Coin");
            string NextAnimationName = InterferenceMatrixManager.Instance.ReturnCurrentAssembleName();
            AnimationManager.Instance.PlayNextAnimation(NextAnimationName);
        }
    }


    /// <summary>
    /// 离开托盘并且r6LeftTrigger已经被对应的仪器部件触发了，利用碰撞检测移除硬币作用在托盘上的质量
    /// </summary>
    /// <param name="coll"></param>
    void OnTriggerExit(Collider coll)
    {
        if (GameObject.Find("Hand").GetComponent<SendToServerByPlayer>().isClient) {
            if (!CommonParameter.ComponentTriggerNameList.Contains("r6LeftTrigger")) return;

            //if (GameObject.Find("Hand").GetComponent<HandControl>().hasCatched == null) return;


            if (coll.gameObject.name == "Coin" && CommonParameter.ComponentTriggerNameList.Contains("r6LeftTrigger")
                //&& GameObject.Find("Hand").GetComponent<HandControl>().hasCatched.name == "Coin"
                //&& coll.gameObject.transform.parent == GameObject.Find("Hand").transform
                && HasWeigthEnter
               )
            {
                Debug.Log("离开了左盘");
                ScaleTiltControl scaletilt = GameObject.Find("ScriptControl").GetComponent<ScaleTiltControl>();
                scaletilt.m_Left -= Convert.ToInt32(coll.gameObject.GetComponent<Rigidbody>().mass);
                Debug.Log("m_Right=" + scaletilt.m_Right + "  " + "scaletilt.m_Left=" + scaletilt.m_Left);
                HasWeigthEnter = false;
            }    
        }
       
    }
}
