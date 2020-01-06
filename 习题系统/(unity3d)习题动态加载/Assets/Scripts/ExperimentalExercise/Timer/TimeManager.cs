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

     public bool IsChoiceQuestionOperation=false;               //选择题操作
     public bool IsCompletionOperation=false;                   //填空题操作

     void Start() {
         TipMessageDialog=GameObject.Find("TipMessageDialog");
         //注册用户点击是或者否按钮的点击事件
         GameObject.Find("TipMessageDialog/BtnYes").GetComponent<Button>().onClick.AddListener(BtnYesClick);
         GameObject.Find("TipMessageDialog/BtnNo").GetComponent<Button>().onClick.AddListener(BtnNoClick);
         TipMessageDialog.SetActive(false);
     }

     void Update() {
         if (IsStartTip && IsChoiceQuestionOperation && ChoiceQuestionShowDialog())
         {
             //弹出询问是否需要提示的提示框
             TipMessageDialog.SetActive(true);
             IsStartTip = false;
             IsChoiceQuestionOperation = false;
         }
         else if (IsStartTip && IsCompletionOperation)
         {
             //弹出询问是否需要提示的提示框
             TipMessageDialog.SetActive(true);
             IsStartTip = false;
             IsCompletionOperation = false;
         }
     }

     /// <summary>
     /// 定时
     /// </summary>
     /// <param name="seconds">定时的秒数</param>
     /// <param name="callback">定时的时间到回调的委托</param>
     public void Timming(float seconds,TTSManager ttsManager_input,string tipMessage_input,AudioSource audioSource_input)
     {
         IsStartTip = false;
        //安全验证
         if (seconds <= 0 )
        {
            return;
        }
         if (timer != null)
         {
             timer.Stop();
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

    /// <summary>
    /// 选择题是否需要弹窗
    /// </summary>
    /// <returns></returns>
    private bool ChoiceQuestionShowDialog()
    {
        #region 判断用户是否选择了选择题答案，如果定时时间到了，还没有选择，则弹窗询问是否需要提示信息的对话框
        if (GameObject.Find("ToggleA") != null)
        {
            Toggle ToggleA = GameObject.Find("ToggleA").GetComponent<Toggle>();
            Toggle ToggleB = GameObject.Find("ToggleB").GetComponent<Toggle>();
            Toggle ToggleC = GameObject.Find("ToggleC").GetComponent<Toggle>();
            Toggle ToggleD = GameObject.Find("ToggleD").GetComponent<Toggle>();
            if (ToggleA.isOn || ToggleB.isOn || ToggleC.isOn || ToggleD.isOn)
            {
                return false;
            }
            return true;
        }

        return false;
        #endregion
    }

  
	
}
