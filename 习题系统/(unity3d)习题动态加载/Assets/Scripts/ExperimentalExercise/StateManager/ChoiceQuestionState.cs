using fvc.exp.state;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using fvc.exp.model;
using fvc.exp.bll;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using fvc.exp.voice;

namespace fvc.exp.state
{
    public class ChoiceQuestionState : State
    {
        TTSManager ttsManager = new TTSManager();
        AudioSource audioSource;                                             //从主摄像机上获取AudioSource
        
        private GameObject _QuestionUI;                                      //选择题UI界面游戏物体
        private int _NumberCount=0;                                             //保存题目的下标
        public ChoiceQuestionState(GameObject QuestionUI, string sql)
        {
            try
            {
                audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();    //从主摄像机上获取AudioSoruce
                
            }
            catch (Exception)
            {

                Debug.LogError("请在Main Camera上挂载AudioSource组件");
            }
            
            StateEnter(QuestionUI,sql);
        }


        /// <summary>
        /// 下一题按钮点击触发
        /// </summary>
        public override void NextQuestion()
        {
            if (StateStaticParams.ChoiceQuestionList == null || this._QuestionUI == null)
            {
                return;
            }

            if (_NumberCount >= StateStaticParams.ChoiceQuestionList.Count - 1)
            {
                bool saveResult = _SaveAnswer();                     //保存答案
                if (saveResult == false)
                {
                    return;
                }

                Debug.Log("切换到填空题");

                
                StateStaticParams.currentQuestionType = QuestionType.CompletionQuestion;
            }

            if (_NumberCount >= 0 && _NumberCount < StateStaticParams.ChoiceQuestionList.Count - 1)
            {
                bool saveResult = _SaveAnswer();                     //保存答案
                if (saveResult == false)
                {
                    return;
                }


                _NumberCount++;
                _ShowMessageOnUI(this._QuestionUI, StateStaticParams.ChoiceQuestionList[_NumberCount]);
            }

            
            

        }

        /// <summary>
        /// 上一题按钮点击触发
        /// </summary>
        public override void PreviousQuestion()
        {
            if (StateStaticParams.ChoiceQuestionList == null || this._QuestionUI == null)
            {
                return;
            }

            if (_NumberCount <=0)
            {
                return;
            }

            if (_NumberCount >= 0 && _NumberCount <= StateStaticParams.ChoiceQuestionList.Count - 1)
            {
               bool saveResult= _SaveAnswer();                     //保存答案
               if (saveResult == false)
               {
                   return;
               }

                _NumberCount--;
                _ShowMessageOnUI(this._QuestionUI, StateStaticParams.ChoiceQuestionList[_NumberCount]);
            }
        }


        /// <summary>
        /// 保存答案
        /// </summary>
        private bool _SaveAnswer()
        {


            Toggle ToggleA = GameObject.Find("ToggleA").GetComponent<Toggle>();
            Toggle ToggleB = GameObject.Find("ToggleB").GetComponent<Toggle>();
            Toggle ToggleC = GameObject.Find("ToggleC").GetComponent<Toggle>();
            Toggle ToggleD = GameObject.Find("ToggleD").GetComponent<Toggle>();
            if (ToggleA.isOn)
            {
                StateStaticParams.ChoiceQuestionList[_NumberCount].UserAnswer ="A";
            }
            if (ToggleB.isOn)
            {
                StateStaticParams.ChoiceQuestionList[_NumberCount].UserAnswer = "B";
            }
            if (ToggleC.isOn)
            {
                StateStaticParams.ChoiceQuestionList[_NumberCount].UserAnswer = "C";
            }
            if (ToggleD.isOn)
            {
                StateStaticParams.ChoiceQuestionList[_NumberCount].UserAnswer = "D";
            }
          
           
            return true;
        }


        /// <summary>
        /// 状态进入，初始化选择题窗体，并加载第一道习题
        /// </summary>
        /// <param name="QuestionUI"></param>
        public override void StateEnter(GameObject QuestionUI,string nullStr)
        {
            if (QuestionUI == null)
            {
                return;
            }
            this._QuestionUI = QuestionUI;

            if (StateStaticParams.ChoiceQuestionList != null && StateStaticParams.ChoiceQuestionList.Count > 0)
            {
                this._QuestionUI.SetActive(true);

                _ShowMessageOnUI(this._QuestionUI, StateStaticParams.ChoiceQuestionList[0]);
            }
            else
            {
                //ttsManager.CloseCannel();
                StateStaticParams.currentQuestionType = QuestionType.CompletionQuestion;        //查询不到数据，直接进入填空题
                
            }
        }


        /// <summary>
        /// 把数据展示到UI上
        /// </summary>
        /// <param name="QuestionUI">选择题界面</param>
        /// <param name="ChoiceQuestion">问题信息</param>
        private void _ShowMessageOnUI(GameObject QuestionUI,ChoiceQuestion choiceQuestion) 
        {
            if (QuestionUI == null || choiceQuestion == null)
            {
                return;
            }




            #region 显示问题
            GameObject ChoiceQuestionContent = GameObject.Find("ChoiceQuestionContent");      //显示问题的游戏物体
            if (ChoiceQuestionContent != null && ChoiceQuestionContent.GetComponent<Text>() != null)
            {
                ChoiceQuestionContent.GetComponent<Text>().text = choiceQuestion.content;
            }
            #endregion


            #region 显示选项ABCD的内容
            GameObject OptionAContent = GameObject.Find("OptionAContent");
            GameObject OptionBContent = GameObject.Find("OptionBContent");
            GameObject OptionCContent = GameObject.Find("OptionCContent");
            GameObject OptionDContent = GameObject.Find("OptionDContent");

            if (OptionAContent == null || OptionBContent == null || OptionCContent == null || OptionDContent == null)
            {
                return;
            }
            OptionAContent.GetComponent<Text>().text = choiceQuestion.optionA;
            OptionBContent.GetComponent<Text>().text = choiceQuestion.optionB;
            OptionCContent.GetComponent<Text>().text = choiceQuestion.optionC;
            OptionDContent.GetComponent<Text>().text = choiceQuestion.optionD;

            #endregion


            #region 显示图片
            GameObject ChoiceQuestionImage = GameObject.Find("ChoiceQuestionImage");     //显示图片游戏物体

            //每次显示图片前都把原先的清空
            if (ChoiceQuestionImage != null && ChoiceQuestionImage.GetComponent<Image>() != null)
            {
                ChoiceQuestionImage.GetComponent<Image>().sprite = null;
            }

            if (choiceQuestion.picture != null && choiceQuestion.picture.Length > 0)
            {
                System.Drawing.Image image;
                byte[] bytes;
                try
                {
                    image = (new SerializeObjectToString().DeserializeObject(choiceQuestion.picture)) as System.Drawing.Image;
                    MemoryStream ms = new MemoryStream();
                    image.Save(ms, image.RawFormat);

                    ms.Flush();
                    ms.Seek(0, SeekOrigin.Begin);
                    //创建文件长度缓冲区
                    bytes = new byte[ms.Length];
                    //读取文件
                    ms.Read(bytes, 0, (int)ms.Length);
                    ms.Flush();
                    //释放文件读取流
                    ms.Close();
                    ms.Dispose();
                    ms = null;
                }
                catch (Exception)
                {

                    return;
                }


                if (ChoiceQuestionImage != null && ChoiceQuestionImage.GetComponent<Image>() != null && image != null)
                {
                    Texture2D texture2d = new Texture2D(800, 600);
                    texture2d.LoadImage(bytes);
                    ChoiceQuestionImage.GetComponent<Image>().sprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), new Vector2(0.5f, 0.5f));

                }
            }

            #endregion


            #region 显示用户选择的答案
            Toggle ToggleA = GameObject.Find("ToggleA").GetComponent<Toggle>();
            Toggle ToggleB = GameObject.Find("ToggleB").GetComponent<Toggle>();
            Toggle ToggleC = GameObject.Find("ToggleC").GetComponent<Toggle>();
            Toggle ToggleD = GameObject.Find("ToggleD").GetComponent<Toggle>();

            ToggleA.isOn = false;
            ToggleB.isOn = false;
            ToggleC.isOn = false;
            ToggleD.isOn = false;

            if (StateStaticParams.ChoiceQuestionList[_NumberCount].UserAnswer != null && StateStaticParams.ChoiceQuestionList[_NumberCount].UserAnswer.Length > 0)
            {
                if (StateStaticParams.ChoiceQuestionList[_NumberCount].UserAnswer == "A")
                {
                    ToggleA.isOn = true;
                }
                else if (StateStaticParams.ChoiceQuestionList[_NumberCount].UserAnswer == "B")
                {
                    ToggleB.isOn = true;
                }
                else if (StateStaticParams.ChoiceQuestionList[_NumberCount].UserAnswer == "C")
                {
                    ToggleC.isOn = true;
                }
                else if (StateStaticParams.ChoiceQuestionList[_NumberCount].UserAnswer == "D")
                {
                    ToggleD.isOn = true;
                }
            }


            //TODO  朗读题目  并启动定时器，当定时到达2/3的思考时间时，检查用户是否进行了答案的选择，如果没有选择，提示用户是否需要提示
            try
            {
                ttsManager.ConvertAndPlay(audioSource, choiceQuestion.content);
                Debug.Log("定时时长:" + choiceQuestion.thinkTime);
                //TimeManager.Timming(choiceQuestion.thinkTime*2/3, TimeCallBack);
                GameObject.Find("QuestionParent").GetComponent<TimeManager>().IsChoiceQuestionOperation = true;
                GameObject.Find("QuestionParent").GetComponent<TimeManager>().Timming(choiceQuestion.thinkTime * 2 / 3, ttsManager, choiceQuestion.tipMessage, audioSource);
            }
            catch (Exception ex)
            {

                Debug.LogError("朗读出错"+ex.Message);
            }
            
            #endregion
        }


        

        
    }
}


