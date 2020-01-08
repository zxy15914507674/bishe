using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QADBService{
    /// <summary>
    /// 添加问题
    /// </summary>
    /// <param name="qaObject"></param>
    /// <returns></returns>
    public int AddQuestion(QA qaObject) {
        string sql = "insert into QA(question,answer,ignoreFlag,hasAnswer,addTime) values('{0}','',0,0,now())";
        sql = string.Format(sql, qaObject.question);
        
        try
        {
            return fvc.exp.qa.SqlHelper.Update(sql);
        }
        catch (System.Exception)
        {
            
            throw;
        }
       
    }
}
