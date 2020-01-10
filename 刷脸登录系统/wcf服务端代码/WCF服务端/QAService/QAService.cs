using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF服务端
{
    public class QAService:IQAContract
    {
        QAProxyInterface proxy = (QAProxyInterface)XmlRpcProxyGen.Create(typeof(QAProxyInterface));
        public string GetAnswer(string question)
        {
            try
            {
                return proxy.getAnswer(question);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAnswer发生异常:" + ex.Message);
                return "发生异常";
            }
            
        }


        public bool TrainQA(string question, string answer)
        {
            string questionAndAnswer ="["+ "'"+question +"'"+ "," + "'"+answer+"'"+"]";
            try
            {
                proxy.trainQA(questionAndAnswer);
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine("TrainQA发生异常:"+ex.Message);
                return false;
            }
           
        }
    }

    [XmlRpcUrl("http://47.94.102.147:666")]
    public interface QAProxyInterface : IXmlRpcProxy
    {
        [XmlRpcMethod("getAnswer")]
        string getAnswer(string question);

        [XmlRpcMethod("trainQA")]
        void trainQA(string questionAndAnswer);
    }
}
