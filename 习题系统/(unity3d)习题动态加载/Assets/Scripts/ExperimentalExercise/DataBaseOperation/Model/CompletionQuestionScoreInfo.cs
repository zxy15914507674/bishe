using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionQuestionScoreInfo
{

    /// <summary>
    /// 错误的题号
    /// </summary>
    public int errorNumber { set; get; }


    /// <summary>
    /// 用户的答案,int的值为空的位置序号，string的值代表的是对应的空的答案
    /// </summary>
    public Dictionary<int, string> userAnswerDic = new Dictionary<int, string>();


    /// <summary>
    /// 正确的答案,int为空的位置序号，string代表的是对应的空的答案
    /// </summary>
    public Dictionary<int, string> answerDic = new Dictionary<int, string>();


    /// <summary>
    /// 该题的分值
    /// </summary>
    public float score { set; get; }
}
