using fvc.exp.model;
using fvc.exp.state;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fvc.exp.score
{
    public class FVCScore
    {
        private float _ConvertScore = 20f;                      //折合的分数
        /// <summary>
        /// 计算分数
        /// </summary>
        public float CalculateScore()
        {
            float ScoreSum = 0;

            //计算选择题的分数
            if (FVCStateStaticParams.ChoiceQuestionList != null && FVCStateStaticParams.ChoiceQuestionList.Count > 0)
            {
                foreach (var choiceQuestion in FVCStateStaticParams.ChoiceQuestionList)
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
                        for (int index = FVCStateStaticParams.ChoiceQuestionScoreInfoList.Count-1; index >= 0; index--) 
                        {
                            if (FVCStateStaticParams.ChoiceQuestionScoreInfoList[index].errorNumber == ChoiceQuestionScoreInfoObj.errorNumber)
                            {
                                FVCStateStaticParams.ChoiceQuestionScoreInfoList.Remove(FVCStateStaticParams.ChoiceQuestionScoreInfoList[index]);
                            }
                        }

                        //把错误的信息添加进集合中
                        FVCStateStaticParams.ChoiceQuestionScoreInfoList.Add(ChoiceQuestionScoreInfoObj);
                    }
                }
            }

            //计算填空题的分数
            if (FVCStateStaticParams.CompletionQuestionList != null && FVCStateStaticParams.CompletionQuestionList.Count > 0)
            {
                foreach (var completionQuestion in FVCStateStaticParams.CompletionQuestionList)
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
                            for (int listIndex = FVCStateStaticParams.CompletionScoreInfoList.Count - 1; listIndex >= 0; listIndex--)
                            {
                                if (FVCStateStaticParams.CompletionScoreInfoList[listIndex].errorNumber == completionQuestionScoreInfo.errorNumber)
                                {
                                    FVCStateStaticParams.CompletionScoreInfoList.Remove(FVCStateStaticParams.CompletionScoreInfoList[listIndex]);
                                }
                            }
                            //把错误的信息添加进集合中
                            FVCStateStaticParams.CompletionScoreInfoList.Add(completionQuestionScoreInfo);

                        }
                    }
                }
            }

            _ShowScoreMessage(ScoreSum);
            ScoreSum = _ConvertScore * (ScoreSum / _CalculateTotalScore());                   //进行分数的折合
            return ScoreSum;
        }



        /// <summary>
        /// 计算满分的分数
        /// </summary>
        /// <returns></returns>
        private float _CalculateTotalScore()
        {
            float ScoreSum = 0;

            //计算选择题的总分数
            if (FVCStateStaticParams.ChoiceQuestionList != null && FVCStateStaticParams.ChoiceQuestionList.Count > 0)
            {
                foreach (var choiceQuestion in FVCStateStaticParams.ChoiceQuestionList)
                {
                    ScoreSum += Convert.ToSingle(choiceQuestion.score);
                }
            }

            //计算填空题的总分数
            if (FVCStateStaticParams.CompletionQuestionList != null && FVCStateStaticParams.CompletionQuestionList.Count > 0)
            {
                foreach (var completionQuestion in FVCStateStaticParams.CompletionQuestionList)
                {
                    ScoreSum += Convert.ToSingle(completionQuestion.score);
                }
            }

           
            return ScoreSum;
        }

        /// <summary>
        /// 测试使用  
        /// </summary>
        /// <param name="ScoreSum"></param>
        private void _ShowScoreMessage(float ScoreSum)
        {
            Debug.Log("没折合时得到的总分:"+ScoreSum);
            Debug.Log("没折合时习题的总分: " + _CalculateTotalScore());
            
            ScoreSum=_ConvertScore*(ScoreSum/_CalculateTotalScore());
           
            Debug.Log("总得分为: " + ScoreSum);

            //测试选择题
            if (FVCStateStaticParams.ChoiceQuestionScoreInfoList != null && FVCStateStaticParams.ChoiceQuestionScoreInfoList.Count > 0)
            {
                foreach (var infoItem in FVCStateStaticParams.ChoiceQuestionScoreInfoList)
                {
                    Debug.Log("错误的题号是: "+infoItem.errorNumber+"   您的答案为:"+infoItem.userAnswer+  "   正确答案为:"+infoItem.answer+"   该题的分值为:"+infoItem.score);
                }
            }

            //测试填空题
            if (FVCStateStaticParams.CompletionScoreInfoList != null && FVCStateStaticParams.CompletionQuestionList.Count > 0)
            {
                foreach (var infoItem in FVCStateStaticParams.CompletionScoreInfoList)
                {
                    foreach (var item in infoItem.userAnswerDic)
                    {
                        Debug.Log("题号为: "+infoItem.errorNumber+"的第"+(item.Key+1)+"个空错了"+"  您的答案是: "+item.Value+"   正确答案是:"+infoItem.answerDic[item.Key]);
                    }
                }
            }
        }
    }
}

