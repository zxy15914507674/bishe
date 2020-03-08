using fvc.exp.model;
using fvc.exp.state;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fvc.exp.score
{
    public class Score
    {
        private float _ConvertScore = 20f;                      //折合的分数
       
        /// <summary>
        /// 计算分数并获取详细信息
        /// </summary>
        /// <returns>详细成绩信息</returns>
        public string GetScoreMsg()
        {
            float ScoreSum = 0;

            //计算选择题的分数
            if (StateStaticParams.ChoiceQuestionList != null && StateStaticParams.ChoiceQuestionList.Count > 0)
            {
                foreach (var choiceQuestion in StateStaticParams.ChoiceQuestionList)
                {
                    
                    //计算选择题的每一道题的得分
                    if (choiceQuestion.UserAnswer != null && choiceQuestion.UserAnswer.Trim() == choiceQuestion.answer.Trim())
                    {
                        ScoreSum += Convert.ToSingle(choiceQuestion.score);
                    }

                    //保存错误题目的信息
                    else
                    {
                        ChoiceQuestonScoreInfo ChoiceQuestionScoreInfoObj = new ChoiceQuestonScoreInfo();
                        
                        
                        //用户的答案
                        if (choiceQuestion.UserAnswer == null || choiceQuestion.UserAnswer.Trim().Length == 0)
                        {
                            ChoiceQuestionScoreInfoObj.userAnswer = "为空";
                        }
                        else
                        {
                            ChoiceQuestionScoreInfoObj.userAnswer = choiceQuestion.UserAnswer;
                        }

                        //错误题号
                        ChoiceQuestionScoreInfoObj.errorNumber = choiceQuestion.QuestionNumber;

                        //习题正确答案
                        ChoiceQuestionScoreInfoObj.answer = choiceQuestion.answer;
                        
                        //该题的分数
                        ChoiceQuestionScoreInfoObj.score = Convert.ToSingle(choiceQuestion.score);

                        //为了防止重复添加
                        for (int index = StateStaticParams.ChoiceQuestionScoreInfoList.Count-1; index >= 0; index--) 
                        {
                            if (StateStaticParams.ChoiceQuestionScoreInfoList[index].errorNumber == ChoiceQuestionScoreInfoObj.errorNumber)
                            {
                                StateStaticParams.ChoiceQuestionScoreInfoList.Remove(StateStaticParams.ChoiceQuestionScoreInfoList[index]);
                            }
                        }

                        //把错误的信息添加进集合中
                        StateStaticParams.ChoiceQuestionScoreInfoList.Add(ChoiceQuestionScoreInfoObj);
                    }
                }
            }

            //计算填空题的分数
            if (StateStaticParams.CompletionQuestionList != null && StateStaticParams.CompletionQuestionList.Count > 0)
            {
                foreach (var completionQuestion in StateStaticParams.CompletionQuestionList)
                {
                    string answer = completionQuestion.answer.Trim();
                    while (answer.IndexOf("  ") > -1)
                    {
                        answer = answer.Replace("  ", " ");                 //答案中连续多于两个空格的替换成一个的空格
                    }
                    string[] answerList = answer.Split(' ');
                    for (int index = 0; index < completionQuestion.userAnswerDic.Count; index++)
                    {
                        //每对一个空就计算分数
                        if (answerList[index] == completionQuestion.userAnswerDic[index])
                        {
                            ScoreSum += Convert.ToSingle(completionQuestion.score) / answerList.Length;
                        }
                        //统计那个空出错了
                        else
                        {
                            CompletionQuestionScoreInfo completionQuestionScoreInfo = new CompletionQuestionScoreInfo();
                            //题号
                            completionQuestionScoreInfo.errorNumber = completionQuestion.QuestionNumber;

                            //每个空占的分值(把这道题的总分除以空的个数)
                            completionQuestionScoreInfo.score = Convert.ToSingle(completionQuestion.score) / answerList.Length;

                            //保存用户输入该空的答案
                            completionQuestionScoreInfo.userAnswerDic[index]=completionQuestion.userAnswerDic[index];
                            //保存该空的正确答案
                            completionQuestionScoreInfo.answerDic[index]=answerList[index];

                            //避免重复添加
                            //for (int listIndex = StateStaticParams.CompletionScoreInfoList.Count - 1; listIndex >= 0; listIndex--)
                            //{
                            //    if (StateStaticParams.CompletionScoreInfoList[listIndex].errorNumber == completionQuestionScoreInfo.errorNumber)
                            //    {
                            //        StateStaticParams.CompletionScoreInfoList.Remove(StateStaticParams.CompletionScoreInfoList[listIndex]);
                            //    }
                            //}
                            //把错误的信息添加进集合中
                            StateStaticParams.CompletionScoreInfoList.Add(completionQuestionScoreInfo);

                        }
                    }
                }
            }

            //_ShowScoreMessage(ScoreSum);
            //ScoreSum = _ConvertScore * (ScoreSum / _CalculateTotalScore());                   //进行分数的折合
           return  _GetScoreMessage(ScoreSum);
          
        }



        /// <summary>
        /// 计算满分的分数
        /// </summary>
        /// <returns></returns>
        private float _CalculateTotalScore()
        {
            float ScoreSum = 0;

            //计算选择题的总分数
            if (StateStaticParams.ChoiceQuestionList != null && StateStaticParams.ChoiceQuestionList.Count > 0)
            {
                foreach (var choiceQuestion in StateStaticParams.ChoiceQuestionList)
                {
                    ScoreSum += Convert.ToSingle(choiceQuestion.score);
                }
            }

            //计算填空题的总分数
            if (StateStaticParams.CompletionQuestionList != null && StateStaticParams.CompletionQuestionList.Count > 0)
            {
                foreach (var completionQuestion in StateStaticParams.CompletionQuestionList)
                {
                    ScoreSum += Convert.ToSingle(completionQuestion.score);
                }
            }

           
            return ScoreSum;
        }


        /// <summary>
        /// 获取成绩的详细信息
        /// </summary>
        /// <param name="ScoreSum">输入的总分</param>
        /// <returns>返回信息列表（包括总分、错误信息信息）</returns>
        private string _GetScoreMessage(float ScoreSum) {
            string scoreMsg = "习题满分为: " + _CalculateTotalScore()+"\n"+"您的得分为:  "+ScoreSum;
            System.Text.StringBuilder bulider = new System.Text.StringBuilder();

            bulider.Append("选择题:"+"\n");

            //测试选择题
            if (StateStaticParams.ChoiceQuestionScoreInfoList != null && StateStaticParams.ChoiceQuestionScoreInfoList.Count > 0)
            {
                foreach (var infoItem in StateStaticParams.ChoiceQuestionScoreInfoList)
                {
                    //Debug.Log("错误的题号是: " + infoItem.errorNumber + "   您的答案为:" + infoItem.userAnswer + "   正确答案为:" + infoItem.answer + "   该题的分值为:" + infoItem.score);
                    bulider.Append("错误的题号" + infoItem.errorNumber + ":   您的答案为:" + infoItem.userAnswer + "   正确答案为:" + infoItem.answer + "\n");
                }
            }

            bulider.Append("\n"+"填空题:"+"\n");
            //测试填空题
            if (StateStaticParams.CompletionScoreInfoList != null && StateStaticParams.CompletionQuestionList.Count > 0)
            {
                foreach (var infoItem in StateStaticParams.CompletionScoreInfoList)
                {
                    foreach (var item in infoItem.userAnswerDic)
                    {
                        //Debug.Log("题号为: " + infoItem.errorNumber + "的第" + (item.Key + 1) + "个空错了" + "  您的答案是: " + item.Value + "   正确答案是:" + infoItem.answerDic[item.Key]);
                        bulider.Append("错误的题号" + infoItem.errorNumber + ":   第" + (item.Key + 1) + "个空错了" + "  您的答案是: " + item.Value + "   正确答案是:" + infoItem.answerDic[item.Key]+"\n");
                    }
                }
            }
            //Debug.Log(scoreMsg);
            //Debug.Log(bulider.ToString());
            return scoreMsg+"&"+bulider.ToString();
        
        }

        /// <summary>
        /// 测试使用  
        /// </summary>
        /// <param name="ScoreSum"></param>
        private void _ShowScoreMessage(float ScoreSum)
        {
            Debug.Log("没折合时得到的总分:"+ScoreSum);
            Debug.Log("习题满分为: " + _CalculateTotalScore());
            
            ScoreSum=_ConvertScore*(ScoreSum/_CalculateTotalScore());
           
            Debug.Log("总得分为: " + ScoreSum);

            //测试选择题
            if (StateStaticParams.ChoiceQuestionScoreInfoList != null && StateStaticParams.ChoiceQuestionScoreInfoList.Count > 0)
            {
                foreach (var infoItem in StateStaticParams.ChoiceQuestionScoreInfoList)
                {
                    Debug.Log("选择题：错误的题号是: "+infoItem.errorNumber+"   您的答案为:"+infoItem.userAnswer+  "   正确答案为:"+infoItem.answer+"   该题的分值为:"+infoItem.score);
                }
            }

            //测试填空题
            if (StateStaticParams.CompletionScoreInfoList != null && StateStaticParams.CompletionQuestionList.Count > 0)
            {
                foreach (var infoItem in StateStaticParams.CompletionScoreInfoList)
                {
                    foreach (var item in infoItem.userAnswerDic)
                    {
                        Debug.Log("填空题：题号为: "+infoItem.errorNumber+"的第"+(item.Key+1)+"个空错了"+"  您的答案是: "+item.Value+"   正确答案是:"+infoItem.answerDic[item.Key]);
                    }
                }
            }
        }
    }
}

