using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Timers;
using UnityEngine.UI;
using System;



public delegate void SetTextDeleagate();

public class TimeManager{
     static Timer timer;
     static SetTextDeleagate m_MyDeleagete;
     

     /// <summary>
     /// 定时
     /// </summary>
     /// <param name="seconds">定时的秒数</param>
     /// <param name="MyDelegate">委托</param>
     public static void Timming(float seconds,SetTextDeleagate MyDelegate) { 
        //安全验证
         if (seconds <= 0 || MyDelegate == null)
        {
            return;
        }

        m_MyDeleagete = MyDelegate;
       
        timer = new Timer((int)(seconds*1000));
        timer.Elapsed += new System.Timers.ElapsedEventHandler(TimerUp);
        timer.AutoReset = false;
        timer.Enabled = true;
        timer.Start();
    }


    /// <summary>
    /// 定时时间到回调的方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    static void TimerUp(object sender, EventArgs e)
    {
         m_MyDeleagete();
         timer.Stop();
    }

    
	
}
