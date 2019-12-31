using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeightCollider : MonoBehaviour
{

    public bool HasWeigthEnter= false;         //砝码是否进入盘中的标志位

    //public bool HasPutBackWeightVacancy;        //是否放回砝码架的标准位
	void Start () {
        
        //HasPutBackWeightVacancy =true;
	}
	
	void Update () {
		
	}

    /// <summary>
    /// 碰撞进入
    /// </summary>
    /// <param name="coll"></param>
    void OnCollisionEnter(Collision coll)
    {

        if (GameObject.Find("Hand")==null)
        {
            return;
        }

       if(GameObject.Find("Hand").GetComponent<SendToServerByPlayer>().isClient){
           //判断手模型是否已经抓到了镊子，抓住了镊子，镊子才能夹住砝码
           GameObject hasCathced = GameObject.Find("Hand").GetComponent<HandControl>().hasCatched;
           if (hasCathced == null) return;
           if (hasCathced.name == "tweezer" && coll.gameObject.name == "tweezer" && !WeightParamer.IsHasClamped
               //&&HasPutBackWeightVacancy
               )
           {
               Debug.Log("碰撞到:" + coll.gameObject.name);
               //把砝码的上一级父物体设为镊子的子物体,这样就可以使得镊子运动而带动砝码
               Transform Weightparent = gameObject.transform.parent;

               Weightparent.parent = coll.gameObject.transform;
               //调整砝码被夹后的位置和角度
               Weightparent.localPosition = WeightPositon;
               Weightparent.localEulerAngles = WeightRotation;

               //砝码被夹了，还没有放开前不能夹第二个
               WeightParamer.IsHasClamped = true;
               //HasPutBackWeightVacancy = false;

           }
       }


        


        //#region 砝码碰到托盘就放开砝码(把砝码设为托盘碰撞体的子物体即可)
        //托盘天平的右边托盘放砝码
        //if (coll.gameObject.name == "WeightColliderRight" && CommonParameter.ComponentTriggerNameList.Contains("r6RightTrigger")
        //    && GameObject.Find("Hand").GetComponent<HandControl>().hasCatched.name == "tweezer"
        //    && gameObject.transform.parent.parent.name == "tweezer")
        //{
        //    Debug.Log("碰撞到右边的托盘");
        //    //把砝码放到托盘上
        //    Transform Weightparent = gameObject.transform.parent;
        //    Weightparent.parent = GameObject.Find("r6RightTrigger").transform;
        //    //并设置好位置
        //    gameObject.transform.parent.localPosition = new Vector3(Weightparent.localPosition.x,0.032f+Weightparent.position.y-transform.position.y,Weightparent.localPosition.z);
        //    //可以夹第二个砝码了
        //    WeightParamer.IsHasClamped =false;

        //    //砝码放到右边的托盘后，为右托盘增加相应的质量,根据需要，可以设定质量的大小为砝码的Rigibody中的Mass属性的大小
        //    ScaleTiltControl scaletilt = GameObject.Find("ScriptControl").GetComponent<ScaleTiltControl>();
        //    scaletilt.m_Right += Convert.ToInt32(gameObject.GetComponent<Rigidbody>().mass);
        //    Debug.Log("m_Right=" + scaletilt.m_Right + "  " + "scaletilt.m_Left=" + scaletilt.m_Left);
        //}

        //#endregion
    }

    //void OnTriggerEnter(Collider coll) {
    //    //把砝码放回砝码架上
    //    if (WeightVacancyPositionDic.ContainsKey(coll.gameObject.name) &&!HasPutBackWeightVacancy
    //        &&WeightParamer.IsHasClamped
    //        )
    //    {
    //        Debug.Log("放回砝码架了");
    //        gameObject.transform.parent.parent = null;
    //        gameObject.transform.parent.position = WeightVacancyPositionDic[coll.gameObject.name];
    //        //砝码放回砝码架了
    //        WeightParamer.IsHasClamped = false;
    //    }
    //}



    //void OnCollisionExit(Collision coll)
    //{
    //    //把砝码放回砝码架上
    //    if (WeightVacancyPositionDic.ContainsKey(coll.gameObject.name)&& !HasPutBackWeightVacancy
      
    //        )
    //    {
    //        Debug.Log("离开砝码架了");

    //        HasPutBackWeightVacancy = true;
    //    }
    //}



    ///// <summary>
    ///// 离开托盘，利用碰撞检测移除该砝码作用在托盘上的质量
    ///// </summary>
    ///// <param name="coll"></param>
    //void OnCollisionExit(Collision coll) {
    //    if (GameObject.Find("Hand").GetComponent<HandControl>().hasCatched == null) return;

    //    if (coll.gameObject.name == "WeightColliderRight" && CommonParameter.ComponentTriggerNameList.Contains("r6RightTrigger")
    //        && GameObject.Find("Hand").GetComponent<HandControl>().hasCatched.name == "tweezer"
    //        && gameObject.transform.parent.parent.name == "tweezer"
    //        )
    //    {
    //        Debug.Log("离开右盘了");
    //        ScaleTiltControl scaletilt = GameObject.Find("ScriptControl").GetComponent<ScaleTiltControl>();
    //        scaletilt.m_Right -=Convert.ToInt32(gameObject.GetComponent<Rigidbody>().mass);
    //        Debug.Log("m_Right=" + scaletilt.m_Right + "  " + "scaletilt.m_Left=" + scaletilt.m_Left);
    //    }
    //}

    #region 常量参数
    //砝码被夹后调整的位置
    public static Vector3 WeightPositon = new Vector3(0.034f, 0.083f, 0.043f);
    //砝码被夹后调整的角度
    public static Vector3 WeightRotation = new Vector3(54.527f,-89.994f,-89.986f);


    //砝码架放砝码对应的空位的位置集合
    static Dictionary<string, Vector3> WeightVacancyPositionDic = new Dictionary<string, Vector3>() { 
        {"Weight_1gVacancy",new Vector3(-3.6521f,1.4757f,1.3035f)},
        {"Weight_2gVacancy",new Vector3(-3.652f,1.4891f,1.1852f)},
        {"Weight_5gVacancy",new Vector3(-4.579f,1.423f,-0.032f)},
        {"Weight_10gVacancy",new Vector3(-3.644f,1.507f,1.029f)}
    };
    #endregion
}
