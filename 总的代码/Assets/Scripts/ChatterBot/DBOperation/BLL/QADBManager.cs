using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QADBManager{

    public void AddQuestion(object  qaObject) {
        try
        {
             new QADBService().AddQuestion((QA)qaObject);
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}
