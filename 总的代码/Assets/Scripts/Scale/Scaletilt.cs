/***
 * 
 *  天平倾斜,通过旋转r3
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaletilt : MonoBehaviour {
    GameObject r3;
    //右参考点
    GameObject RightReferenceParent;
    //左参考点
    GameObject LeftReferenceParent;
    void Start () {
       
    }
	
	
    //void Update () {
		
    //}

   /// <summary>
   /// 向左倾斜
   /// </summary>
   /// <param name="agent">倾斜的角度</param>
    public void LeftTilt(float agent) {
        if(agent<-5f){
            agent =-5f;
        }
        Tilt(agent);
       
    }
    /// <summary>
    /// 天平平衡
    /// </summary>
    public void Balance() {
        Tilt(0f);
    }
   
    /// <summary>
    /// 向右倾斜
    /// </summary>
    /// <param name="agent">倾斜的角度</param>
    public void RightTilt(float agent)
    {
        if(agent>5f){
            agent = 5f;
        }
        Tilt(agent);
    }
    /// <summary>
    /// 天平倾斜
    /// </summary>
    /// <param name="agent">倾斜的角度</param>
    private void Tilt(float agent) {
        r3 = GameObject.Find("r3Trigger");
        RightReferenceParent = GameObject.Find("RightReferenceParent");
        LeftReferenceParent = GameObject.Find("LeftReferenceParent");
        if(r3==null||RightReferenceParent==null||LeftReferenceParent==null){
            return;
        }

        LeftReferenceParent.transform.localEulerAngles = new Vector3(0f, 0f,-agent);
        RightReferenceParent.transform.localEulerAngles = new Vector3(0f, 0f,-agent);
        r3.transform.localEulerAngles = new Vector3(0f, 0f,agent);
    }
   




}
