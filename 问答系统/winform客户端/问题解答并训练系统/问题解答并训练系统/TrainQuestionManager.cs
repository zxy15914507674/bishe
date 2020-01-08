using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// 问题训练管理类
/// </summary>
class TrainQuestionManager
{
    EndpointAddress address;
    WSHttpBinding binding;
    QAServiceConstract.IQAContract channel;
    ChannelFactory<QAServiceConstract.IQAContract> factory;
    public TrainQuestionManager()
    {
        address = new EndpointAddress("http://47.94.102.147:8000/QAService/QAInterface");
        binding = new WSHttpBinding();
        binding.Security.Mode = SecurityMode.None;
        factory = new ChannelFactory<QAServiceConstract.IQAContract>(binding, address);
        channel = factory.CreateChannel();
    }

    /// <summary>
    /// 训练问题和答案
    /// </summary>
    /// <param name="question">要训练的问题</param>
    /// <param name="answer">要训练的答案</param>
    /// <returns></returns>
    public bool TrainQuestion(string question,string answer) {
        try
        {
            channel.TrainQA(question, answer);
            return true;
        }
        catch (Exception )
        {

            return false;
        }
       
    }
}
