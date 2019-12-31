using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class r6RightTrigger : MonoBehaviour {

    bool flag;
    public GameObject r6Right;
    //右参考点
    Transform RightReferenceParent;
    BoxCollider boxCollider;


   
	void Start () {
        flag = false;
        //找到当前物体的碰撞体
        boxCollider = GetComponent<BoxCollider>();
        
	}
	
	
	void Update () {
        if (flag && r6Right != null)
        {
            //调整r6Right的位置
            r6Right.transform.position =transform.position;
            //调整r6Right的角度
            //调整角度
            r6Right.transform.eulerAngles =transform.eulerAngles;
            //使碰撞体不可用
            //boxCollider.enabled = false;


            //禁用r5Right的同步脚本,因为r6Right已经装配
            //GameObject.Find("r5_right").GetComponent<ClientGameObjectMethod>().enabled = false;
        }
	}

    void OnTriggerEnter(Collider obj)
    {
        //如果r5Right还没有装配，则r6Right不能装配
        if (!CommonParameter.ComponentTriggerNameList.Contains("r5RightTrigger")) return;


        if (obj.gameObject.name.Equals(objNameContanst.r6_right) || obj.gameObject.name.Equals(objNameContanst.r6_left))
        {

            //控制只执行一次
            if(!flag){
                
                r6Right = obj.gameObject;
                Debug.Log("r6Right触发");
                
                //如果存在刚体，则移除刚体
                Rigidbody rigibody = obj.gameObject.GetComponent<Rigidbody>();
                if (rigibody != null)
                {
                    Destroy(rigibody);
                }

               
                //向左倾斜
                //Scaletilt scaletilt = GameObject.Find("ScriptControl").GetComponent<Scaletilt>();
                //scaletilt.RightTilt();

                ScaleTiltControl scaletilt = GameObject.Find("ScriptControl").GetComponent<ScaleTiltControl>();
                scaletilt.m_Right += 1;
                flag = true;

                //把当前游戏物体的名称添加到触发器名称集合中
                CommonParameter.ComponentTriggerNameList.Add(gameObject.name);


                InterferenceMatrixManager.Instance.RemoveCurrentComponent("r6_right");

                //从干涉矩阵中查出下一个动画的名称，然后进行播放该动画
                string NextAnimationName = InterferenceMatrixManager.Instance.ReturnCurrentAssembleName();
                //播放下一个动画
                AnimationManager.Instance.PlayNextAnimation(NextAnimationName);
 
            }
        }

        #region 砝码触发到盘上，添加质量

        if (!CommonParameter.ComponentTriggerNameList.Contains("r6RightTrigger")) return;

        //if (GameObject.Find("Hand").GetComponent<HandControl>().hasCatched == null) return;

        if (obj.gameObject.name == "Weight" && CommonParameter.ComponentTriggerNameList.Contains("r6RightTrigger")
        //    && GameObject.Find("Hand").GetComponent<HandControl>().hasCatched.name == "tweezer"
        //    && obj.gameObject.transform.parent.parent.name == "tweezer"
            &&!obj.gameObject.GetComponent<WeightCollider>().HasWeigthEnter)
        {

            //保证在各自的客户端执行
            if (GameObject.Find("Hand").GetComponent<SendToServerByPlayer>().isClient) {

                Debug.Log("触发到右边的托盘");
                //把砝码放到托盘上
                Transform Weightparent = obj.gameObject.transform.parent;
                Weightparent.parent = GameObject.Find("r6RightTrigger").transform;
                //并设置好位置


                Weightparent.localPosition = new Vector3(WeightPositionDic[obj.transform.parent.gameObject.name], 0.032f + Weightparent.position.y - obj.gameObject.transform.position.y, WeightPositionDic[obj.transform.parent.gameObject.name]);

                //可以夹第二个砝码了
                WeightParamer.IsHasClamped = false;

                //砝码放到右边的托盘后，为右托盘增加相应的质量,根据需要，可以设定质量的大小为砝码的Rigibody中的Mass属性的大小
                ScaleTiltControl scaletilt = GameObject.Find("ScriptControl").GetComponent<ScaleTiltControl>();
                scaletilt.m_Right += Convert.ToInt32(obj.gameObject.GetComponent<Rigidbody>().mass);
                Debug.Log("m_Right=" + scaletilt.m_Right + "  " + "scaletilt.m_Left=" + scaletilt.m_Left);

                obj.gameObject.GetComponent<WeightCollider>().HasWeigthEnter = true;     //砝码已经进入盘中

                PlayAnimation();
            }
           
        }
        #endregion
    }




    /// <summary>
    /// 播放硬币放到左盘上的动画
    /// </summary>
    private void PlayAnimation()
    {
        if (!InterferenceMatrixManager.Instance.GetHasAssemblyComponentName().Contains("Weight"))
        {
            InterferenceMatrixManager.Instance.RemoveCurrentComponent("Weight");
            string NextAnimationName = InterferenceMatrixManager.Instance.ReturnCurrentAssembleName();
            AnimationManager.Instance.PlayNextAnimation(NextAnimationName);
        }
    }


    /// <summary>
    /// 检测砝码是否离开天平右盘
    /// </summary>
    /// <param name="obj"></param>
    void OnTriggerExit(Collider obj) {

        if (!CommonParameter.ComponentTriggerNameList.Contains("r6RightTrigger")) return;

        //if (GameObject.Find("Hand").GetComponent<HandControl>().hasCatched == null) return;

        if (obj.gameObject.name == "Weight" && CommonParameter.ComponentTriggerNameList.Contains("r6RightTrigger")
            //&& GameObject.Find("Hand").GetComponent<HandControl>().hasCatched.name == "tweezer"
            //&& obj.gameObject.transform.parent.parent.name == "tweezer"
            && obj.gameObject.GetComponent<WeightCollider>().HasWeigthEnter
            )
        {

             //保证在各自的客户端执行
            if (GameObject.Find("Hand").GetComponent<SendToServerByPlayer>().isClient) {
                Debug.Log("离开右盘了");
                //移走了砝码，应当减少天平右盘对应的质量
                ScaleTiltControl scaletilt = GameObject.Find("ScriptControl").GetComponent<ScaleTiltControl>();
                scaletilt.m_Right -= Convert.ToInt32(obj.gameObject.GetComponent<Rigidbody>().mass);
                Debug.Log("m_Right=" + scaletilt.m_Right + "  " + "scaletilt.m_Left=" + scaletilt.m_Left);

                obj.gameObject.GetComponent<WeightCollider>().HasWeigthEnter = false;           //砝码已经离开盘中
                
            }

            
            

        }
    }



    //砝码触发到天平右盘后，通过砝码对应的父游戏物体名称来调整砝码的位置
    static Dictionary<string, float> WeightPositionDic = new Dictionary<string, float>() { 
         {"WeightReferencePoint1.2",-0.04f},
         {"WeightReferencePoint",0.04f},
         {"WeightReferencePoint0.7",0f},
         {"WeightReferencePoint0.5",0.02f}
    };
}
