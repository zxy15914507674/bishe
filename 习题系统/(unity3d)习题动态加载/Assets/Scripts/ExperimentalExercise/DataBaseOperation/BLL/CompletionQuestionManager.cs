using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using fvc.exp.model;
using fvc.exp.dal;
namespace fvc.exp.bll
{
    /// <summary>
    /// 填空题数据业务逻辑类
    /// </summary>
    public class CompletionQuestionManager
    {
        CompletionQuestionService completionQuestionServiceObj = new CompletionQuestionService();
       


         /// <summary>
        /// 通过sql获取填空题
        /// </summary>
        /// <param name="sceneName">场景名称</param>
        /// <returns></returns>
        public List<CompletionQuestion> GetCompletionQuestionInfoBySql(string completionSqlLocal)
        {
            try
            {
                List<CompletionQuestion> CompletionQuestionList = completionQuestionServiceObj.GetCompletionQuestionInfoBySql(completionSqlLocal);

                //为题目添加上题号
                for (int index = 0; index < CompletionQuestionList.Count; index++)
                {
                    CompletionQuestionList[index].content = (index + 1) + "  " + CompletionQuestionList[index].content;

                    CompletionQuestionList[index].QuestionNumber = index + 1;                   //封装上题号
                }

                return CompletionQuestionList;
            }
            catch (Exception)
            {

                throw;
            }
        }
    
        /// <summary>
        /// 通过填空题编号获取单个题目的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CompletionQuestion GetCompletionQuestionInfoById(string id)
        {
            try
            {
                return completionQuestionServiceObj.GetCompletionQuestionInfoById(id);
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        /// <summary>
        /// 通过场景名称获取所有的id号
        /// </summary>
        /// <param name="sceneName">场景名称</param>
        /// <returns></returns>
        public List<string> GetAllIdNumber(string sceneName)
        {
            try
            {
                return completionQuestionServiceObj.GetAllIdNumber(sceneName);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
