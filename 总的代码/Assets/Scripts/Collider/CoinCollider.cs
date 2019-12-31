using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollider: MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //void OnCollisionEnter(Collision coll) { 
    //     GameObject  hasCathced=GameObject.Find("Hand").GetComponent<HandControl>().hasCatched;
    //    if(hasCathced==null) return;
    //    //手模型抓住了硬币并且硬币触发到天平的左盘
    //    if (hasCathced.name == "Coin" && coll.gameObject.name == "CoinColliderLeft"
    //        && gameObject.transform.parent == GameObject.Find("Hand").GetComponent<HandControl>().gameObject.transform)
    //    {
    //        //把硬币放进天平的左盘，即把硬币设为CoinColliderLeft的子物体，并调节硬币的位置
    //        gameObject.transform.parent = coll.gameObject.transform;
    //        Debug.Log("硬币碰撞到左盘");
    //        //调整硬币的位置
    //        gameObject.transform.localPosition = new Vector3(0f, 0.02f,0f);

    //        //把硬币的质量作用在天平的左盘上
    //        ScaleTiltControl scaletilt = GameObject.Find("ScriptControl").GetComponent<ScaleTiltControl>();
    //        scaletilt.m_Left+= Convert.ToInt32(gameObject.GetComponent<Rigidbody>().mass);
    //        Debug.Log("m_Right=" + scaletilt.m_Right + "  " + "scaletilt.m_Left=" + scaletilt.m_Left);
    //    }
    //}


    ///// <summary>
    ///// 离开托盘并且r6LeftTrigger已经被对应的仪器部件触发了，利用碰撞检测移除硬币作用在托盘上的质量
    ///// </summary>
    ///// <param name="coll"></param>
    //void OnCollisionExit(Collision coll)
    //{
    //    if (GameObject.Find("Hand").GetComponent<HandControl>().hasCatched == null) return;
        
        
    //    if (coll.gameObject.name == "CoinColliderLeft" && CommonParameter.ComponentTriggerNameList.Contains("r6LeftTrigger")
    //        && GameObject.Find("Hand").GetComponent<HandControl>().hasCatched.name == "Coin"
    //        && gameObject.transform.parent == GameObject.Find("Hand").transform
    //       )
    //    {
    //        ScaleTiltControl scaletilt = GameObject.Find("ScriptControl").GetComponent<ScaleTiltControl>();
    //        scaletilt.m_Left -= Convert.ToInt32(gameObject.GetComponent<Rigidbody>().mass);
    //        Debug.Log("m_Right=" + scaletilt.m_Right + "  " + "scaletilt.m_Left=" + scaletilt.m_Left);
    //    }
    //}



    //void OnTriggerEnter(Collider coll)
    //{
    //    GameObject hasCathced = GameObject.Find("Hand").GetComponent<HandControl>().hasCatched;
    //    if (hasCathced == null) return;
    //    //手模型抓住了硬币并且硬币触发到天平的左盘
    //    if (hasCathced.name == "Coin" && coll.gameObject.name == "r6LeftTrigger"
    //        && gameObject.transform.parent == GameObject.Find("Hand").GetComponent<HandControl>().gameObject.transform)
    //    {
    //        //把硬币放进天平的左盘，即把硬币设为CoinColliderLeft的子物体，并调节硬币的位置
    //        gameObject.transform.parent = coll.gameObject.transform;
    //        Debug.Log("硬币碰撞到左盘");
    //        //调整硬币的位置
    //        gameObject.transform.localPosition = new Vector3(0f, 0.02f, 0f);

    //        //把硬币的质量作用在天平的左盘上
    //        ScaleTiltControl scaletilt = GameObject.Find("ScriptControl").GetComponent<ScaleTiltControl>();
    //        scaletilt.m_Left += Convert.ToInt32(gameObject.GetComponent<Rigidbody>().mass);
    //        Debug.Log("m_Right=" + scaletilt.m_Right + "  " + "scaletilt.m_Left=" + scaletilt.m_Left);
    //    }
    //}


    ///// <summary>
    ///// 离开托盘并且r6LeftTrigger已经被对应的仪器部件触发了，利用碰撞检测移除硬币作用在托盘上的质量
    ///// </summary>
    ///// <param name="coll"></param>
    //void OnTriggerExit(Collider coll)
    //{
    //    if (GameObject.Find("Hand").GetComponent<HandControl>().hasCatched == null) return;


    //    if (coll.gameObject.name == "r6LeftTrigger" && CommonParameter.ComponentTriggerNameList.Contains("r6LeftTrigger")
    //        && GameObject.Find("Hand").GetComponent<HandControl>().hasCatched.name == "Coin"
    //        && gameObject.transform.parent == GameObject.Find("Hand").transform
    //       )
    //    {
    //        ScaleTiltControl scaletilt = GameObject.Find("ScriptControl").GetComponent<ScaleTiltControl>();
    //        scaletilt.m_Left -= Convert.ToInt32(gameObject.GetComponent<Rigidbody>().mass);
    //        Debug.Log("m_Right=" + scaletilt.m_Right + "  " + "scaletilt.m_Left=" + scaletilt.m_Left);
    //    }
    //}
}
