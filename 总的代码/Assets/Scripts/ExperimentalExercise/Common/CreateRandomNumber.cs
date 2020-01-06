/****
 * 
 *     产生不重复的随机数 
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRandomNumber{

    /// <summary>
    /// 获取不重复的随机数
    /// </summary>
    /// <param name="sourceNumber">总的个数的列表</param>
    /// <param name="wantNumber">要获取的随机数的个数</param>
    /// <returns></returns>
    public static List<string> GetRandomNumber(List<string> sourceList,int wantNumber) {
        if (sourceList==null||sourceList.Count == 0)
        {
            return null;
        }
        if (sourceList.Count< wantNumber)
        {
            return null;
        }
        
        List<string> numberList = new List<string>();
        System.Random r = new System.Random();
        for (int i = 0; i <sourceList.Count; i++)
        {
            int index = r.Next(0, sourceList.Count);
            string tmp=sourceList[0];
            sourceList[0] = sourceList[index];
            sourceList[index] = tmp;
        }
        for (int i = 0; i < wantNumber; i++)
        {
            numberList.Add(sourceList[i]);
        }
        return numberList;    
    }
}
