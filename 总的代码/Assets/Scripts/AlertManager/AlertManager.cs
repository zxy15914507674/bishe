using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertManager: MonoBehaviour {

    public GameObject TargetGameObject;        //目标游戏物体，设置其在定时期间显示，非定时时间不显示(可以是文本框本身的游戏物体，也可以是文本框游戏物体所属的顶级游戏物体)

    public Text TitleTextComponent;           //闪烁的文本的文本框

    public Text MessageShowTextComponent;     //显示信息的文本框

    [Range(0,4)]
    public  int seconds=2;                    //定时的秒数

    public static AlertManager Instance;   


   
    SetTextDeleagate myDelegate;
    bool flag;


    bool ScintillationFrequencyFlag=false;              //文字闪动标准位
    float TimeCount;                                  //文字闪动时长计算
    [Range(0, 1)]
    public float ScintillationTime=0.4f;                //文字闪动的切换时长

    void Awake() {
        Instance = this;
    }

	void Start () {
        myDelegate +=SetMsg;
        flag = false;
        TimeCount = 0f;
        //显示的文本清空
        //TitleTextComponent.text = "";
        TargetGameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (flag)
        {
            if (TitleTextComponent == null || TargetGameObject == null || MessageShowTextComponent==null)
            {
                Debug.LogError("TargetTextComponent或者TargetGameObject或者MessageShowTextComponent不能为空");
                return;
            }
            //显示的文本清空
            MessageShowTextComponent.text = "";
            TargetGameObject.SetActive(false);
            
            ScintillationFrequencyFlag = false;
            flag = false;
            TimeCount = 0f;    
        }

        

        if (ScintillationFrequencyFlag)
        {
            TimeCount += Time.deltaTime;

            if (TimeCount < ScintillationTime)
            {
                //显示文本最大
                TitleTextComponent.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (TimeCount > ScintillationTime && TimeCount < ScintillationTime * 2)
            {
                //显示文本最小
                TitleTextComponent.transform.localScale = new Vector3(0f, 0f, 0f);
                
            }
            else if (TimeCount > ScintillationTime * 2)
            {
                TimeCount = 0f;            
            }
        }
              
	}

    /// <summary>
    /// 定时到了，委托回调的方法
    /// </summary>
    private void SetMsg()
    {
        flag = true;
    }



    /// <summary>
    /// 从外部设置显示的文本
    /// </summary>
    /// <param name="msg">要显示的文本</param>
    public void SetMsg(string msg) {

        if(!flag&&!ScintillationFrequencyFlag){
            if (TitleTextComponent == null || TargetGameObject == null || MessageShowTextComponent == null)
            {
                Debug.LogError("TargetTextComponent或者TargetGameObject或者MessageShowTextComponent不能为空");
                return;
            }
           

            ScintillationFrequencyFlag = true;
            
            TargetGameObject.SetActive(true);
            MessageShowTextComponent.text = msg;
            //定时开始
            TimeManager.Timming(seconds, myDelegate);
        }
    }
}



