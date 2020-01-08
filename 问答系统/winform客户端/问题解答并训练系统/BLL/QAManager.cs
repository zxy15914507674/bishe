using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class QAManager
    {
        QAService qaService = new QAService();

        /// <summary>
        /// 同时根据回答状态和忽略状态查询QA
        /// </summary>
        /// <param name="answerStatus">回答状态</param>
        /// <param name="ignoreStatus">忽略状态</param>
        /// <returns></returns>
        public List<QA> GetQAByAnswerStatusAndIgnoreStatus(int answerStatus, int ignoreStatus) {
            try
            {
                List<QA> listQA = qaService.GetQAByAnswerStatusAndIgnoreStatus(answerStatus,ignoreStatus);
                if (listQA != null)
                {
                    foreach (var item in listQA)
                    {
                        if (item.hasAnswer == 0)
                        {
                            item.hasAnswerDisplay = "未解答";
                        }
                        else
                        {
                            item.hasAnswerDisplay = "已解答";
                        }

                        if (item.ignoreFlag == 0)
                        {
                            item.ignoreDisplay = "未忽略";
                        }
                        else
                        {
                            item.ignoreDisplay = "已忽略";
                        }


                        int LimitLength = 15;
                        //限制问题和答案显示的长度
                        if (item.question.Length >= LimitLength)
                        {
                            item.question = item.question.Substring(0, LimitLength) + "...";
                        }

                        if (item.answer.Length >= LimitLength)
                        {
                            item.answer = item.answer.Substring(0, LimitLength) + "...";
                        }
                    }
                }
                return listQA;
            }
            catch (Exception)
            {

                throw;
            }
            
        }


         /// <summary>
        /// 根据回答状态查询问题
        /// </summary>
        /// <param name="answerStatus">回答状态</param>
        /// <returns></returns>
        public List<QA> GetQAByAnswerStatus(int answerStatus) {
            try
            {
               List<QA> listQA=qaService.GetQAByAnswerStatus(answerStatus);
               if (listQA != null)
               {
                   foreach (var item in listQA)
                   {
                       if (item.hasAnswer == 0)
                       {
                           item.hasAnswerDisplay = "未解答";
                       }
                       else {
                           item.hasAnswerDisplay = "已解答";
                       }

                       if (item.ignoreFlag == 0)
                       {
                           item.ignoreDisplay = "未忽略";
                       }
                       else {
                           item.ignoreDisplay = "已忽略";
                       }


                       int LimitLength = 15;
                       //限制问题和答案显示的长度
                       if (item.question.Length >= LimitLength)
                       {
                           item.question = item.question.Substring(0, LimitLength) + "...";
                       }

                       if (item.answer.Length >= LimitLength)
                       {
                           item.answer = item.answer.Substring(0, LimitLength) + "...";
                       }
                   }
               }
               return listQA;
            }
            catch (Exception)
            {
                
                throw;
            }
        }



          /// <summary>
        /// 根据问题忽略状态查询问题
        /// </summary>
        /// <param name="ignoreStatus"></param>
        /// <returns></returns>
        public List<QA> GetQAByIgnoreStatus(int ignoreStatus) {

            try
            {
                List<QA> listQA = qaService.GetQAByIgnoreStatus(ignoreStatus);
                if (listQA != null)
                {
                    foreach (var item in listQA)
                    {
                        if (item.hasAnswer == 0)
                        {
                            item.hasAnswerDisplay = "未解答";
                        }
                        else
                        {
                            item.hasAnswerDisplay = "已解答";
                        }

                        if (item.ignoreFlag == 0)
                        {
                            item.ignoreDisplay = "未忽略";
                        }
                        else
                        {
                            item.ignoreDisplay = "已忽略";
                        }


                        int LimitLength = 15;
                        //限制问题和答案显示的长度
                        if (item.question.Length >= LimitLength)
                        {
                            item.question = item.question.Substring(0, LimitLength) + "...";
                        }

                        if (item.answer.Length >= LimitLength)
                        {
                            item.answer = item.answer.Substring(0, LimitLength) + "...";
                        }
                    }
                }
                return listQA;
            }
            catch (Exception)
            {

                throw;
            }
        }



        /// <summary>
        /// 根据题目Id忽略题目
        /// </summary>
        /// <param name="id">题目id</param>
        /// <returns>忽略结果</returns>
        public int IgnoreQuestion(string id) {
            try
            {
                return qaService.IgnoreQuestion(id);
            }
            catch (Exception)
            {
                
                throw;
            }
        }



         /// <summary>
        /// 根据题目id查询问题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QA GetQAById(string id) {
            try
            {
                return qaService.GetQAById(id);
            }
            catch (Exception)
            {
                
                throw;
            }
        }


         /// <summary>
        /// 根据题目Id同时更新回答状态和忽略状态
        /// </summary>
        /// <param name="AnswerStatus">回答的状态</param>
        /// <param name="IgnoreStatus">忽略状态</param>
        /// <param name="answer">回答的答案</param>
        /// <param name="id">id号</param>
        /// <returns></returns>
        public int UpdateAnswerFlagAndIgnoreFlag(int AnswerStatus, int IgnoreStatus,string answer, string id){
            try
            {
                return qaService.UpdateAnswerFlagAndIgnoreFlag(AnswerStatus, IgnoreStatus,answer,id);
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        public int AddQuestion(QA qaObject)
        {
            try
            {
                return qaService.AddQuestion(qaObject);
            }
            catch (System.Exception)
            {

                throw;
            }
        }


    }
}
