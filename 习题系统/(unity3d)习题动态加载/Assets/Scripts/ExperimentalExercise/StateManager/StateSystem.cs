using fvc.exp.bll;
using fvc.exp.score;
using fvc.exp.state;
using fvc.unet.change;
using fvc.unet.start;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StateSystem : MonoBehaviour
{
    private GameObject _ChoiceQuestionUI;                   //选择题UI
    private GameObject _CompletionQuestionUI;               //填空题UI


    SendStartExperientalMessage startExpMsg;               //开始发送进入协同进入实验的指令操作类
    SendNextOrPreviousQuestionMessage nextOrPreMsg;        //协同上下一题切换的操作类
         
    //private GameObject _ScoreUI;                            //显示分数的UI


    private Button _ChoiceQuestionBtnPrevious;
    private Button _ChoiceQuestionBtnNext;                  //选择题界面中的  按钮 '上一题' 和 '下一题'
    private Button _CompletionQuestionBtnPrevious;
    private Button _CompletionQuestionBtnNext;              //填空题界面中的  '按钮' '上一题' 和 '下一题'



    private ChoiceQuestionState _ChoiceQuestionState;         //选题题状态
    private CompletionQuestionState _CompletionQuestionState;      //填空题状态

    string choiceSqlLocal = "";
    string completionSqlLocal = "";

    private string _SceneName;                                  //场景名称
    void Start()
    {
        _ChoiceQuestionUI = GameObject.Find("ChoiceQuestionUI");
        _CompletionQuestionUI = GameObject.Find("CompletionQuestionUI");
        //_ScoreUI = GameObject.Find("ScoreUI");




        //开始发送进入协同进入实验的指令操作类
        startExpMsg = new SendStartExperientalMessage(short.MaxValue - 1);

        //注册协同开始实验的服务端接收到消息后回调的函数
        startExpMsg.serverMsgDelegate = new fvc.unet.start.ServerMsgDelegate(startExpServerReceive);
        //注册协同开始实验的客户端接收到消息后回调的函数
        startExpMsg.clientMsgDelegate = new fvc.unet.start.ClientMsgDelegate(startExpClientReceive);

        //上下一题协同操作的指令类
        nextOrPreMsg = new SendNextOrPreviousQuestionMessage(short.MaxValue - 2);

        //注册上下一题客户端接收到消息后回调的函数
        nextOrPreMsg.clientMsgDelegate = new fvc.unet.change.ClientMsgDelegate(nextOrPreClientReceive);



        _ChoiceQuestionBtnPrevious = GameObject.Find("ChoiceQuestionUI/BtnPrevious").GetComponent<Button>();
        _ChoiceQuestionBtnNext = GameObject.Find("ChoiceQuestionUI/BtnNext").GetComponent<Button>();

         _CompletionQuestionBtnPrevious = GameObject.Find("CompletionQuestionUI/BtnPrevious").GetComponent<Button>();
         _CompletionQuestionBtnNext = GameObject.Find("CompletionQuestionUI/BtnNext").GetComponent<Button>();




        _ChoiceQuestionUI.SetActive(false);
        _CompletionQuestionUI.SetActive(false);
        //_ScoreUI.SetActive(false);

    }

    void Update()
    {
        if (StateStaticParams.IsStartExercise)
        {
            QuestionType currentQuestinType = StateStaticParams.currentQuestionType;
            if (currentQuestinType == QuestionType.ChoiceQuestion)          //选择题
            {
                _CompletionQuestionUI.SetActive(false);

                if (StateStaticParams.ChoiceQuestionList == null)
                {
                    _ChoiceQuestionState = new ChoiceQuestionState(_ChoiceQuestionUI, choiceSqlLocal);
                    //上一题和下一题按钮动态注册事件
                    _ChoiceQuestionBtnPrevious.onClick.AddListener(ChoiceQuestionBtnPreviousClick);
                    _ChoiceQuestionBtnNext.onClick.AddListener(ChoiceQuestionBtnNextClick);
                    //_ChoiceQuestionBtnPrevious.onClick.AddListener(_ChoiceQuestionState.PreviousQuestion);
                    //_ChoiceQuestionBtnNext.onClick.AddListener(_ChoiceQuestionState.NextQuestion);
                    

                   
                }
                else
                {
                    _ChoiceQuestionUI.SetActive(true);
                }
            }
            else if (currentQuestinType == QuestionType.CompletionQuestion) //填空题
            {
                _ChoiceQuestionUI.SetActive(false);


                if (StateStaticParams.CompletionQuestionList == null)
                {
                    _CompletionQuestionState = new CompletionQuestionState(_CompletionQuestionUI, completionSqlLocal);
                    _CompletionQuestionBtnPrevious.onClick.AddListener(CompletionQuestionBtnPreviousClick);
                    _CompletionQuestionBtnNext.onClick.AddListener(CompletionQuestionBtnNextClick);

                    //_CompletionQuestionBtnPrevious.onClick.AddListener(_CompletionQuestionState.PreviousQuestion);
                    //_CompletionQuestionBtnNext.onClick.AddListener(_CompletionQuestionState.NextQuestion);

                    //TODO   注册进入下一题的事件
                }
                else
                {
                    _CompletionQuestionUI.SetActive(true);
                }

            }
            else if (StateStaticParams.currentQuestionType == QuestionType.Null)
            {
                //_ScoreUI.SetActive(true);
                // if (_ScoreUI != null && GameObject.Find("ScoreUI/TxtScore") != null)
                // {
                //     GameObject.Find("ScoreUI/TxtScore").GetComponent<Text>().text = "您的分数为："+new FVCScore().CalculateScore()+"分";
                // }
            }

        }
    }



    /// <summary>
    /// 开始习题
    /// </summary>
    /// <param name="SceneName">场景名称</param>
    public void StartExperimentalExercise(string SceneName)
    {
        if (SceneName == null || SceneName.Length == 0)
        {
            return;
        }
        
        startExpMsg.SendMsg(SceneName);
    }


    /// <summary>
    /// 服务端接收到消息后回调的函数
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    private string startExpServerReceive(string msg)
    {
        //根据接收到的场景名称获取各种题型的所有id号
       List<string> choiceIdList=new ChoiceQuestionManager().GetAllIdNumber(msg);
       List<string> completionIdList=new CompletionQuestionManager().GetAllIdNumber(msg);
       List<string> choiceIdWant;
       List<string> completionIdWant;
       string choiceSql = "";
       string completionSql = "";
       string returnSql = "";
       //选择题和填空题各随机抽取3题
       if (choiceIdList.Count > 3)
       {
           choiceIdWant = CreateRandomNumber.GetRandomNumber(choiceIdList, 3);
           choiceSql = "select expName,sceneName,questionTypeNumber,content,optionA,optionB,optionC,optionD,picture,answer,score,teacherName from ChoiceQuestion where sceneName='{0}' and id in ({1},{2},{3})";
           choiceSql=string.Format(choiceSql, msg, choiceIdWant[0], choiceIdWant[1], choiceIdWant[2]);
       }
       else {
           choiceSql = "select expName,sceneName,questionTypeNumber,content,optionA,optionB,optionC,optionD,picture,answer,score,teacherName from ChoiceQuestion where sceneName='{0}'";
           choiceSql=string.Format(choiceSql, msg);
       }

       if (completionIdList.Count > 3)
       {
           completionIdWant = CreateRandomNumber.GetRandomNumber(completionIdList, 3);
           completionSql = "select expName,sceneName,questionTypeNumber,content,picture,answer,score from CompletionQuestion where sceneName='{0}' and id in ({1},{2},{3})";
           completionSql = string.Format(choiceSql, msg, completionIdWant[0], completionIdWant[1], completionIdWant[2]);
       }
       else
       {
           completionSql = "select expName,sceneName,questionTypeNumber,content,picture,answer,score from CompletionQuestion where sceneName='{0}'";
           completionSql = string.Format(completionSql, msg);
       }
       returnSql = choiceSql + "&" + completionSql;
       return returnSql;
    }

    /// <summary>
    /// (习题开始时初始化的函数)客户端接收到消息后回调的函数
    /// </summary>
    /// <param name="msg"></param>
    private void startExpClientReceive(string msg)
    {
        //this._SceneName =msg;
        //分割从服务端返回的字符串，拿到查询选择题和填空题的sql语句
        string []sqlArray=msg.Split('&');
        if (sqlArray == null && sqlArray.Length == 0)
        {
            return;
        }
        //选择题的sql语句
        choiceSqlLocal = sqlArray[0];
        //填空题的sql语句
        completionSqlLocal = sqlArray[1];

        //静态数据初始化
        StateStaticParams.currentQuestionType = QuestionType.ChoiceQuestion;                //从选择题开始
        StateStaticParams.IsStartExercise = false;
        StateStaticParams.ChoiceQuestionList = null;
        StateStaticParams.CompletionQuestionList = null;


        StateStaticParams.IsStartExercise = true;
    }



    /// <summary>
    /// 选择题上一题按钮点击事件回调函数
    /// </summary>
    private void ChoiceQuestionBtnPreviousClick() {
        nextOrPreMsg.SendMsg("Choice:Previous");
    }

    /// <summary>
    /// 选择题下一题按钮点击事件回调函数
    /// </summary>
    private void ChoiceQuestionBtnNextClick() {
        nextOrPreMsg.SendMsg("Choice:Next");
    }


    /// <summary>
    /// 填空题上一题按钮点击事件回调函数
    /// </summary>
    private void CompletionQuestionBtnPreviousClick()
    {
        nextOrPreMsg.SendMsg("Completion:Previous");
    }

    /// <summary>
    /// 填空题下一题按钮点击事件回调函数
    /// </summary>
    private void CompletionQuestionBtnNextClick()
    {
        nextOrPreMsg.SendMsg("Completion:Next");
    }


    /// <summary>
    /// 上下一题客户端接收到消息回调的函数
    /// </summary>
    private void nextOrPreClientReceive(string msg) {
        if (msg == null || msg.Length == 0)
        {
            return;
        }
        //以  :  为分割符，切割出是那种题型和那种操作指令
        string []msgArray=msg.Split(':');

        if (msgArray == null || msgArray.Length == 0)
        {
            return;
        }
        string questionType = msgArray[0];
        string questionOperation = msgArray[1];

        //题型是选择题
        if (questionType == "Choice")
        {
            //下一题操作
            if (questionOperation == "Next")
            {
                _ChoiceQuestionState.NextQuestion();
            }
            //上一题操作
            else if (questionOperation == "Previous")
            {
                _ChoiceQuestionState.PreviousQuestion();
            }
        }
        //题型是填空题
        else if (questionType == "Completion")
        {
            //下一题操作
            if (questionOperation == "Next")
            {
                _CompletionQuestionState.NextQuestion();
            }
            //上一题操作
            else if (questionOperation == "Previous")
            {
                _CompletionQuestionState.PreviousQuestion();
            }
        }
        
    }
}




