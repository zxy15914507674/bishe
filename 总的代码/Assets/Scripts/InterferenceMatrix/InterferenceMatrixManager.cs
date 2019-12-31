using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using InterferenceMatrixPro;

public class InterferenceMatrixManager{
    
    public static InterferenceMatrixManager Instance=new InterferenceMatrixManager();

    //仪器部件和干涉矩阵索引之间的映射关系
    public  Dictionary<string, int> listComponentNameIndexRelation = new Dictionary<string, int>();


    private InterferenceMatrix matrix;

    public InterferenceMatrixManager()
    {
        //仪器部件和干涉矩阵索引之间的映射关系
        listComponentNameIndexRelation.Add("r2",1);
        listComponentNameIndexRelation.Add("r3",2);
        listComponentNameIndexRelation.Add("r4_left",3);
        listComponentNameIndexRelation.Add("r4_right",4);
        listComponentNameIndexRelation.Add("r5_left",5);
        listComponentNameIndexRelation.Add("r5_right",6);
        listComponentNameIndexRelation.Add("r6_left",7);
        listComponentNameIndexRelation.Add("r6_right",8);
        listComponentNameIndexRelation.Add("Coin",9);
        listComponentNameIndexRelation.Add("Weight",10);

        

        #region 构建天平组装的干涉矩阵
        List<int> r2 = new List<int>(new int[] { 0, 0, 0, 0, 0, 0, 0, 0,0,0 });
        List<int> r3 = new List<int>(new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
        List<int> r4left = new List<int>(new int[] { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 });
        List<int> r4right = new List<int>(new int[] { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 });
        List<int> r5left = new List<int>(new int[] { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0 });
        List<int> r5right = new List<int>(new int[] { 1, 1, 0, 1, 0, 0, 0, 0, 0, 0 });
        List<int> r6left = new List<int>(new int[] { 1, 1, 1, 0, 1, 0, 0, 0, 0, 0 });
        List<int> r6right = new List<int>(new int[] { 1, 1, 0, 1, 0, 1, 0, 0, 0, 0 });
        List<int> Coin = new List<int>(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 0 });
        List<int> Weight = new List<int>(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 });
        Dictionary<int, List<int>> dic = new Dictionary<int, List<int>>();
        dic.Add(1, r2);
        dic.Add(2, r3);
        dic.Add(3, r4left);
        dic.Add(4, r4right);
        dic.Add(5, r5left);
        dic.Add(6, r5right);
        dic.Add(7, r6left);
        dic.Add(8, r6right);
        dic.Add(9,Coin);
        dic.Add(10,Weight);
        #endregion

        matrix = new InterferenceMatrix(dic);
    }


    /// <summary>
    /// 检查当前仪器部件是否满足装配
    /// </summary>
    /// <param name="componentName">仪器部件的名称</param>
    /// <returns></returns>
    public bool CheckCurrentComponentMeetAssembe(string componentName) {

        if(!listComponentNameIndexRelation.ContainsKey(componentName)){
            return false;
        }

        return  matrix.CheckConstraint(listComponentNameIndexRelation[componentName]);
    }


    /// <summary>
    /// 返回当前需要装配的仪器部件的名称
    /// </summary>
    /// <returns>仪器部件的名称</returns>
    public string ReturnCurrentAssembleName() {

        List<int> listIndex = matrix.CheckAllRowMeetConstraint();
       
        //存储所有满足条件的仪器部件的名称
        List<string> listComponentName = new List<string>();
        foreach (string key in listComponentNameIndexRelation.Keys)
        {
             //if(listComponentNameIndexRelation[key]==index){
             //    componentName = key;
             //    break;
             //}
            foreach (int index in listIndex)
            {
                if (listComponentNameIndexRelation[key] == index)
                {
                    //把满足的名称添加到集合中
                    listComponentName.Add(key);
                }

            }
        }

        //为了多样性，随机抽取一个名称返回
        int listComponentNameCount = listComponentName.Count;
        if (listComponentNameCount > 1)
        {
            int componentNameIndex = Random.Range(0, listComponentNameCount);
            return listComponentName[componentNameIndex];
        }
        else if (listComponentNameCount == 1)
        {
            return listComponentName[0];
        }
        else {
            return "";
        }
    }


    /// <summary>
    /// 移除当前部件
    /// </summary>
    /// <param name="componentName">部件的名称</param>
    
    public void RemoveCurrentComponent(string componentName) {
        if(!listComponentNameIndexRelation.ContainsKey(componentName)){
            return;
        }
        int index=listComponentNameIndexRelation[componentName];
        matrix.RemoveElement(index);
        
    }


    /// <summary>
    /// 是否已经全部装配完毕
    /// </summary>
    /// <returns></returns>
    public bool IsHasAllAssemble() {
        return matrix.NoteCount()==0?true:false;
    }


    public List<string> GetHasAssemblyComponentName() {
        List<string> hasAssemblyNameList = new List<string>();
        foreach (int index in matrix.hasRemovedRowIndex)
        {
            foreach (string key in listComponentNameIndexRelation.Keys)
            {
                  if(listComponentNameIndexRelation[key]==index){
                      hasAssemblyNameList.Add(key);
                  }
            }
        }
        return hasAssemblyNameList;
    }
}
