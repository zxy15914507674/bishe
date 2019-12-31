using fvc.exp.score;
using fvc.exp.state;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace FVCPosExamUiController
{
    public class FVCStateSystem: MonoBehaviour
    {
        private GameObject _ChoiceQuestionUI;                   //选择题UI
        private GameObject _CompletionQuestionUI;               //填空题UI

        //private GameObject _ScoreUI;                            //显示分数的UI


        private Button _ChoiceQuestionBtnPrevious;
        private Button _ChoiceQuestionBtnNext;                  //选择题界面中的  按钮 '上一题' 和 '下一题'
        private Button _CompletionQuestionBtnPrevious;
        private Button _CompletionQuestionBtnNext;              //填空题界面中的  '按钮' '上一题' 和 '下一题'



        private FVCChoiceQuestionState _ChoiceQuestionState;         //选题题状态
        private FVCCompletionQuestionState _CompletionQuestionState;      //填空题状态



        private string _SceneName;                                  //场景名称
        void Start()
        {
            _ChoiceQuestionUI = GameObject.Find("ChoiceQuestionUI");
            _CompletionQuestionUI = GameObject.Find("CompletionQuestionUI");
            //_ScoreUI = GameObject.Find("ScoreUI");


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
            if (FVCStateStaticParams.IsStartExercise)
            {
                FVCQuestionType currentQuestinType = FVCStateStaticParams.currentQuestionType;
                if (currentQuestinType == FVCQuestionType.ChoiceQuestion)          //选择题
                {
                    _CompletionQuestionUI.SetActive(false);

                    if (FVCStateStaticParams.ChoiceQuestionList == null)
                    {
                        _ChoiceQuestionState = new FVCChoiceQuestionState(_ChoiceQuestionUI, _SceneName);
                        //上一题和下一题按钮动态注册事件
                        _ChoiceQuestionBtnPrevious.onClick.AddListener(_ChoiceQuestionState.PreviousQuestion);
                        _ChoiceQuestionBtnNext.onClick.AddListener(_ChoiceQuestionState.NextQuestion);
                    }
                    else
                    {
                        _ChoiceQuestionUI.SetActive(true);
                    }
                }
                else if (currentQuestinType == FVCQuestionType.CompletionQuestion) //填空题
                {
                    _ChoiceQuestionUI.SetActive(false);


                    if (FVCStateStaticParams.CompletionQuestionList == null)
                    {
                        _CompletionQuestionState = new FVCCompletionQuestionState(_CompletionQuestionUI, _SceneName);
                        _CompletionQuestionBtnPrevious.onClick.AddListener(_CompletionQuestionState.PreviousQuestion);
                        _CompletionQuestionBtnNext.onClick.AddListener(_CompletionQuestionState.NextQuestion);
                    }
                    else
                    {
                        _CompletionQuestionUI.SetActive(true);
                    }

                }
                else if (FVCStateStaticParams.currentQuestionType == FVCQuestionType.Null)
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
            this._SceneName = SceneName;
            //静态数据初始化
            FVCStateStaticParams.currentQuestionType = FVCQuestionType.ChoiceQuestion;                //从选择题开始
            FVCStateStaticParams.IsStartExercise = false;
            FVCStateStaticParams.ChoiceQuestionList = null;
            FVCStateStaticParams.CompletionQuestionList = null;

            
            FVCStateStaticParams.IsStartExercise = true;
        }
    }
}




