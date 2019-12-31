/****
 * 
 * 
 *  天平倾斜控制 
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTiltControl : MonoBehaviour {

    public int m_Left;    //左边的质量
    public int m_Right;   //右边的质量

    float time1;          //定时的时间
    float timeCount;     //定时的时间计算
     
    Scaletilt scaletilt;
	void Start () {
        m_Left = 0;
        m_Right = 0;
        scaletilt = GameObject.Find("ScriptControl").GetComponent<Scaletilt>();
        time1 = 2f;
	}
	
	void Update () {
		if(m_Left>m_Right){

            //向左倾斜
            scaletilt.LeftTilt(-(m_Left - m_Right));
            if (CommonParameter.ComponentTriggerNameList.Contains("r6LeftTrigger") && CommonParameter.ComponentTriggerNameList.Contains("r6RightTrigger"))
            {
                
                GameObject.Find("ScriptControl").GetComponent<ShowWeight>().ShowWeightMessage("左盘重于右盘");
            }
        }
        else if (m_Right > m_Left)
        {
            //向右倾斜
            scaletilt.RightTilt(m_Right - m_Left);
            if (CommonParameter.ComponentTriggerNameList.Contains("r6LeftTrigger") && CommonParameter.ComponentTriggerNameList.Contains("r6RightTrigger"))
            {
                
                GameObject.Find("ScriptControl").GetComponent<ShowWeight>().ShowWeightMessage("右盘重于左盘");
            }
        }
        else {

            //平衡
            scaletilt.Balance();
            if (CommonParameter.ComponentTriggerNameList.Contains("r6LeftTrigger") && CommonParameter.ComponentTriggerNameList.Contains("r6RightTrigger"))
            {
                
                GameObject.Find("ScriptControl").GetComponent<ShowWeight>().ShowWeightMessage("天平平衡，质量为:"+(m_Right-2)+"g");
            }
        }
	}

  
}
