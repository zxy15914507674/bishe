using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class QAService
    {

        /// <summary>
        /// 同时根据回答状态和忽略状态查询QA
        /// </summary>
        /// <param name="answerStatus">回答状态</param>
        /// <param name="ignoreStatus">忽略状态</param>
        /// <returns></returns>
        public List<QA> GetQAByAnswerStatusAndIgnoreStatus(int answerStatus, int ignoreStatus)
        {
            string sql = "select id,question,answer,ignoreFlag,hasAnswer from QA where hasAnswer={0} and ignoreFlag={1}";
            sql = string.Format(sql,answerStatus,ignoreStatus);
            MySqlDataReader reader = null;
            List<QA> listQA = null;
            try
            {
                reader = fvc.exp.qa.SqlHelper.GetReader(sql);
                if (reader != null)
                {
                    listQA = new List<QA>();
                    while (reader.Read())
                    {
                        QA qaObj = new QA()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            question = reader["question"].ToString(),
                            answer = reader["answer"].ToString(),
                            ignoreFlag = Convert.ToInt32(reader["ignoreFlag"]),
                            hasAnswer = Convert.ToInt32(reader["hasAnswer"])
                        };
                        listQA.Add(qaObj);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return listQA;
        }

        /// <summary>
        /// 根据回答状态查询问题
        /// </summary>
        /// <param name="answerStatus">回答状态</param>
        /// <returns></returns>
        public List<QA> GetQAByAnswerStatus(int answerStatus)
        {
            string sql = "select id,question,answer,ignoreFlag,hasAnswer from QA where hasAnswer="+answerStatus;
            MySqlDataReader reader = null;
            List<QA> listQA =null;
            try
            {
                reader = fvc.exp.qa.SqlHelper.GetReader(sql);
                if (reader != null)
                {
                    listQA = new List<QA>();
                    while (reader.Read()) {
                        QA qaObj = new QA() { 
                            id=Convert.ToInt32(reader["id"]),
                            question=reader["question"].ToString(),
                            answer=reader["answer"].ToString(),
                            ignoreFlag = Convert.ToInt32(reader["ignoreFlag"]),
                            hasAnswer = Convert.ToInt32(reader["hasAnswer"])
                        };
                        listQA.Add(qaObj);
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return listQA;
        }

        /// <summary>
        /// 根据问题忽略状态查询问题
        /// </summary>
        /// <param name="ignoreStatus"></param>
        /// <returns></returns>
        public List<QA> GetQAByIgnoreStatus(int ignoreStatus) {
            string sql = "select id,question,answer,ignoreFlag,hasAnswer from QA where ignoreFlag=" + ignoreStatus;
            MySqlDataReader reader = null;
            List<QA> listQA = null;
            try
            {
                reader = fvc.exp.qa.SqlHelper.GetReader(sql);
                if (reader != null)
                {
                    listQA = new List<QA>();
                    while (reader.Read())
                    {
                        QA qaObj = new QA()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            question = reader["question"].ToString(),
                            answer = reader["answer"].ToString(),
                            ignoreFlag = Convert.ToInt32(reader["ignoreFlag"]),
                            hasAnswer = Convert.ToInt32(reader["hasAnswer"])
                        };
                        listQA.Add(qaObj);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return listQA;
        }



        /// <summary>
        /// 根据题目Id忽略题目
        /// </summary>
        /// <param name="id">题目id</param>
        /// <returns>忽略结果</returns>
        public int IgnoreQuestion(string id)
        {
            string sql = "update QA set ignoreFlag=1 where id="+id;
            try
            {
                return fvc.exp.qa.SqlHelper.Update(sql);
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
        public QA GetQAById(string id)
        {
            string sql = "select id,question,answer,ignoreFlag,hasAnswer from QA where id=" +id;
            MySqlDataReader reader = null;
            QA qaObj = null;
            try
            {
                reader = fvc.exp.qa.SqlHelper.GetReader(sql);
                if (reader != null)
                {
                   
                    if(reader.Read())
                    {
                        qaObj = new QA()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            question = reader["question"].ToString(),
                            answer = reader["answer"].ToString(),
                            ignoreFlag = Convert.ToInt32(reader["ignoreFlag"]),
                            hasAnswer = Convert.ToInt32(reader["hasAnswer"])
                        };
                        
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return qaObj;
           
        }

        /// <summary>
        /// 根据题目Id同时更新回答状态和忽略状态
        /// </summary>
        /// <param name="AnswerStatus">回答的状态</param>
        /// <param name="IgnoreStatus">忽略状态</param>
        /// <param name="answer">回答的答案</param>
        /// <param name="id">id号</param>
        /// <returns></returns>
        public int UpdateAnswerFlagAndIgnoreFlag(int AnswerStatus, int IgnoreStatus,string answer, string id)
        {
            string sql = "update QA set hasAnswer={0},ignoreFlag={1},answer='{2}' where id={3}";
            sql = string.Format(sql, AnswerStatus,IgnoreStatus,answer, id);
            try
            {
                int returnResult=fvc.exp.qa.SqlHelper.Update(sql);
                return returnResult;
            }
            catch (Exception)
            {

                throw;
            }
           
        }



        /// <summary>
        /// 添加问题
        /// </summary>
        /// <param name="qaObject"></param>
        /// <returns></returns>
        public int AddQuestion(QA qaObject)
        {
            string sql = "insert into QA(question,answer,ignoreFlag,hasAnswer,addTime) values('{0}','{1}',0,1,now())";
            sql = string.Format(sql, qaObject.question,qaObject.answer);

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
}
