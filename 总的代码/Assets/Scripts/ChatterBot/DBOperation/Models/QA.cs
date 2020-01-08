using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QA{
    //question    answer  ignoreFlag  hasAnswer  addTime

    /// <summary>
    /// 问题
    /// </summary>
    public string question { set; get; }

    /// <summary>
    /// 答案
    /// </summary>
    public string answer { set; get; }

    /// <summary>
    /// 是否忽略标志位
    /// </summary>
    public int ignoreFlag { set; get; }


    /// <summary>
    /// 是否回答标志位
    /// </summary>
    public int hasAnswer { set; get; }

    /// <summary>
    /// 添加时间
    /// </summary>
    public DateTime addTime { set; get; }
}
