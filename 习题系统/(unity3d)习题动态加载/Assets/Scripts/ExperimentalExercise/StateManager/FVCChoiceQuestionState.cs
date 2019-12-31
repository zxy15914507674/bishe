﻿using fvc.exp.state;
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

namespace fvc.exp.state
{
    public class FVCChoiceQuestionState : FVCState
    {
     
        private GameObject _QuestionUI;                                      //选择题UI界面游戏物体
        private int _NumberCount=0;                                             //保存题目的下标
        public FVCChoiceQuestionState(GameObject QuestionUI, string SceneName)
        {
            StateEnter(QuestionUI,SceneName);
        }


        /// <summary>
        /// 下一题按钮点击触发
        /// </summary>
        public override void NextQuestion()
        {
            if (FVCStateStaticParams.ChoiceQuestionList == null || this._QuestionUI == null)
            {
                return;
            }

            if (_NumberCount >= FVCStateStaticParams.ChoiceQuestionList.Count - 1)
            {
                bool saveResult = _SaveAnswer();                     //保存答案
                if (saveResult == false)
                {
                    return;
                }

                Debug.Log("切换到填空题");
                FVCStateStaticParams.currentQuestionType = FVCQuestionType.CompletionQuestion;
            }

            if (_NumberCount >= 0 && _NumberCount < FVCStateStaticParams.ChoiceQuestionList.Count - 1)
            {
                bool saveResult = _SaveAnswer();                     //保存答案
                if (saveResult == false)
                {
                    return;
                }


                _NumberCount++;
                _ShowMessageOnUI(this._QuestionUI, FVCStateStaticParams.ChoiceQuestionList[_NumberCount]);
            }

            
            

        }

        /// <summary>
        /// 上一题按钮点击触发
        /// </summary>
        public override void PreviousQuestion()
        {
            if (FVCStateStaticParams.ChoiceQuestionList == null || this._QuestionUI == null)
            {
                return;
            }

            if (_NumberCount <=0)
            {
                return;
            }

            if (_NumberCount >= 0 && _NumberCount <= FVCStateStaticParams.ChoiceQuestionList.Count - 1)
            {
               bool saveResult= _SaveAnswer();                     //保存答案
               if (saveResult == false)
               {
                   return;
               }

                _NumberCount--;
                _ShowMessageOnUI(this._QuestionUI, FVCStateStaticParams.ChoiceQuestionList[_NumberCount]);
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
                FVCStateStaticParams.ChoiceQuestionList[_NumberCount].UserAnswer ="A";
            }
            if (ToggleB.isOn)
            {
                FVCStateStaticParams.ChoiceQuestionList[_NumberCount].UserAnswer = "B";
            }
            if (ToggleC.isOn)
            {
                FVCStateStaticParams.ChoiceQuestionList[_NumberCount].UserAnswer = "C";
            }
            if (ToggleD.isOn)
            {
                FVCStateStaticParams.ChoiceQuestionList[_NumberCount].UserAnswer = "D";
            }
          
           
            return true;
        }


        /// <summary>
        /// 状态进入，初始化选择题窗体，并加载第一道习题
        /// </summary>
        /// <param name="QuestionUI"></param>
        public override void StateEnter(GameObject QuestionUI,string SceneName)
        {
            if (QuestionUI == null || SceneName== null || SceneName.Length == 0)
            {
                return;
            }
            this._QuestionUI = QuestionUI;

            
            
            try
            {
                FVCStateStaticParams.ChoiceQuestionList = new ChoiceQuestionManager().GetChoiceQuestionInfoBySceneName(SceneName);
            }
            catch (System.Exception)
            {
                //TODO 提示用户出错了
                 
            }
            if (FVCStateStaticParams.ChoiceQuestionList != null && FVCStateStaticParams.ChoiceQuestionList.Count > 0)
            {
                this._QuestionUI.SetActive(true);

                _ShowMessageOnUI(this._QuestionUI, FVCStateStaticParams.ChoiceQuestionList[0]);
            }
            else
            {
                FVCStateStaticParams.currentQuestionType = FVCQuestionType.CompletionQuestion;        //查询不到数据，直接进入填空题
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

            if (FVCStateStaticParams.ChoiceQuestionList[_NumberCount].UserAnswer != null && FVCStateStaticParams.ChoiceQuestionList[_NumberCount].UserAnswer.Length > 0)
            {
                if (FVCStateStaticParams.ChoiceQuestionList[_NumberCount].UserAnswer == "A")
                {
                    ToggleA.isOn = true;
                }
                else if (FVCStateStaticParams.ChoiceQuestionList[_NumberCount].UserAnswer == "B")
                {
                    ToggleB.isOn = true;
                }
                else if (FVCStateStaticParams.ChoiceQuestionList[_NumberCount].UserAnswer == "C")
                {
                    ToggleC.isOn = true;
                }
                else if (FVCStateStaticParams.ChoiceQuestionList[_NumberCount].UserAnswer == "D")
                {
                    ToggleD.isOn = true;
                }
            }
            #endregion
        }
    }
}


