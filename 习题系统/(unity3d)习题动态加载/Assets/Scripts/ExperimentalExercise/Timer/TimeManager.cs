using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Timers;
using UnityEngine.UI;
using System;
using fvc.exp.voice;

public class TimeManager : MonoBehaviour{
     Timer timer;
     TTSManager ttsManager;
     string tipMessage;
     AudioSource audioSource;
     GameObject TipMessageDialog;
     bool IsStartTip = false;
     void Start() {
         TipMessageDialog=GameObject.Find("TipMessageDialog");
         //注册用户点击是或者否按钮的点击事件
         GameObject.Find("TipMessageDialog/BtnYes").GetComponent<Button>().onClick.AddListener(BtnYesClick);
         GameObject.Find("TipMessageDialog/BtnNo").GetComponent<Button>().onClick.AddListener(BtnNoClick);
         TipMessageDialog.SetActive(false);
     }

     void Update() {
         if (IsStartTip)
         {
             //弹出询问是否需要提示的提示框
             TipMessageDialog.SetActive(true);
             IsStartTip = false;
             
         }
     }

     /// <summary>
     /// 定时
     /// </summary>
     /// <param name="seconds">定时的秒数</param>
     /// <param name="callback">定时的时间到回调的委托</param>
     public void Timming(float seconds,TTSManager ttsManager_input,string tipMessage_input,AudioSource audioSource_input)
     { 
        //安全验证
         if (seconds <= 0 )
        {
            return;
        }

        ttsManager = ttsManager_input;
        tipMessage = tipMessage_input;
        audioSource = audioSource_input;

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
     void TimerUp(object sender, EventArgs e)
    {
        IsStartTip = true;
        timer.Stop();
    }

    //点击yes按钮的点击事件
    private void BtnYesClick()
    {
        GameObject.Find("TipMessageDialog").SetActive(false);
        ttsManager.ConvertAndPlay(audioSource, tipMessage);
    }

    //点击否按钮的点击事件
    private void BtnNoClick()
    {
        GameObject.Find("TipMessageDialog").SetActive(false);
    }

  
	
}
