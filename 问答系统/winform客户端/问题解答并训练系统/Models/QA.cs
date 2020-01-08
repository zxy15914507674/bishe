using System;
using System.Collections;
using System.Collections.Generic;

public class QA{
    //question    answer  ignoreFlag  hasAnswer  addTime


    public int id { set; get; }
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
    /// 显示在界面上是否忽略
    /// </summary>
    public string ignoreDisplay { set; get; }

    /// <summary>
    /// 是否回答标志位
    /// </summary>
    public int hasAnswer { set; get; }


    /// <summary>
    /// 显示在界面上的是否已经回答
    /// </summary>
    public string hasAnswerDisplay { set; get; }


    /// <summary>
    /// 添加时间
    /// </summary>
    public DateTime addTime { set; get; }
}
