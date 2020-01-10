using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF服务端
{
    [ServiceContract]
    public interface IQAContract
    {
        /// <summary>
        /// 获取问题答案
        /// </summary>
        /// <param name="question">输入的问题</param>
        /// <returns></returns>
        [OperationContract]
        string GetAnswer(string question);

        
        /// <summary>
        /// 训练问题和答案
        /// </summary>
        /// <param name="question">问题</param>
        /// <param name="answer">问题对应的答案</param>
        /// <returns></returns>
        [OperationContract]
        bool TrainQA(string question,string answer);
    }
}
