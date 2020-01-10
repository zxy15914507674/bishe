using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfoService{
    
    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public UserInfo UserLogin(UserInfo userInfo)
    {
        UserLoginDelegate dg =UserLoginMethod;
        System.IAsyncResult ir =dg.BeginInvoke(userInfo,null,null);
            //主程序在此等待接收 返回值，拿到返回值后接着往下执行
        return dg.EndInvoke(ir);
    }



    /// <summary>
    /// 登录的委托，为了能够访问wcf后能访问数据库
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    private delegate UserInfo UserLoginDelegate(UserInfo userInfo);



    /// <summary>
    /// 登录的异步方法
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    private UserInfo UserLoginMethod(UserInfo userInfo)
    {
        string sql = "select studentNumber,studentName,pwd from UserInfo where studentNumber='{0}' and pwd='{1}'";
        sql = string.Format(sql, userInfo.studentNumber, userInfo.pwd);
        MySql.Data.MySqlClient.MySqlDataReader reader = null;
        try
        {
            reader = fvc.exp.SqlHelper.GetReader(sql);
            if (reader.Read())
            {
                userInfo.studentName = reader["studentName"].ToString();
            }
            else
            {
                userInfo = null;
            }


        }
        catch (System.Exception)
        {

            throw;
        }

        return userInfo;

    }


    /// <summary>
    /// 检查学号是否存在
    /// </summary>
    /// <param name="studentNumber"></param>
    /// <returns></returns>
    public bool CheckStudentIdIsExist(string studentNumber)
    {

        CheckStudentIdIsExisDelegate dg = CheckStudentIdIsExisMethod;
        System.IAsyncResult ir = dg.BeginInvoke(studentNumber, null, null);
        //主程序在此等待接收 返回值，拿到返回值后接着往下执行
        return dg.EndInvoke(ir);
    }


    /// <summary>
    /// 检查学生学号是否存在的委托
    /// </summary>
    /// <param name="studentNumber">学号</param>
    /// <returns></returns>
    private delegate bool CheckStudentIdIsExisDelegate(string studentNumber);

    /// <summary>
    /// 检查学生学号是否存在的异步方法
    /// </summary>
    /// <param name="studentNumber"></param>
    /// <returns></returns>
    private bool CheckStudentIdIsExisMethod(string studentNumber)
    {

        string sql = "select studentNumber from UserInfo where studentNumber=" + studentNumber;
        MySql.Data.MySqlClient.MySqlDataReader reader = null;
        try
        {
            reader = fvc.exp.SqlHelper.GetReader(sql);
            if (reader.Read() == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        catch (System.Exception)
        {

            throw;
        }
    }


    /// <summary>
    /// 添加用户
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public int AddUser(UserInfo userInfo)
    {
        AddUserDelegate dg = AddUserDelegateMethod;
        System.IAsyncResult ir = dg.BeginInvoke(userInfo, null, null);
        //主程序在此等待接收 返回值，拿到返回值后接着往下执行
        return dg.EndInvoke(ir);
        
    }


    /// <summary>
    /// 添加用户的委托
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    private delegate int AddUserDelegate(UserInfo userInfo);


    private int AddUserDelegateMethod(UserInfo userInfo) {
        string sql = "insert into UserInfo(studentNumber,studentName,pwd) values('{0}','{1}','{2}')";
        sql = string.Format(sql, userInfo.studentNumber, userInfo.studentName, userInfo.pwd);
        try
        {
            return fvc.exp.SqlHelper.Update(sql);
        }
        catch (System.Exception)
        {

            throw;
        }
        
    }


    /// <summary>
    /// 根据学生的学号查询学生的信息
    /// </summary>
    /// <returns></returns>
    public UserInfo GetUserInfoByStudentId(string studentNumber) {
        GetUserInfoByStudentIdDelegate dg = GetUserInfoByStudentIdMethod;
        System.IAsyncResult ir = dg.BeginInvoke(studentNumber, null, null);
        //主程序在此等待接收 返回值，拿到返回值后接着往下执行
        return dg.EndInvoke(ir);
    }


    private delegate UserInfo GetUserInfoByStudentIdDelegate(string studentNumber);


    private UserInfo GetUserInfoByStudentIdMethod(string studentNumber)
    {
        string sql = "select studentNumber,studentName,pwd  from UserInfo where studentNumber='"+studentNumber+"'";
        UserInfo userInfo=null;
        MySql.Data.MySqlClient.MySqlDataReader reader = null;
        try
        {
            reader = fvc.exp.SqlHelper.GetReader(sql);
            if (reader.Read())
            {
                userInfo = new UserInfo() {
                    studentNumber = reader["studentNumber"].ToString(),
                    studentName = reader["studentName"].ToString(),
                    pwd=reader["pwd"].ToString()
                };
            }
           


        }
        catch (System.Exception)
        {

            throw;
        }

        return userInfo;

    }
}
