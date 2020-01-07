using System.Collections;
using System.Collections.Generic;
using System.ServiceModel;
using UnityEngine;

public class QAManager : MonoBehaviour {
    EndpointAddress address;
    WSHttpBinding binding;
    QAServiceConstract.IQAContract channel;
    ChannelFactory<QAServiceConstract.IQAContract> factory;
    fvc.unet.start.SendQAMessage qaMessage = new fvc.unet.start.SendQAMessage(short.MaxValue - 4);
	void Start () {
        qaMessage.serverMsgDelegate = new fvc.unet.start.ServerMsgDelegate(ServerMsgReceive);
        qaMessage.clientMsgDelegate = new fvc.unet.start.ClientMsgDelegate(ClientMsgRecevie);

        address = new EndpointAddress("http://47.94.102.147:8000/QAService/QAInterface");
        binding = new WSHttpBinding();
        binding.Security.Mode = SecurityMode.None;
        factory = new ChannelFactory<QAServiceConstract.IQAContract>(binding, address);
        channel = factory.CreateChannel();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


   /// <summary>
    /// 服务端接收到消息回调的函数
   /// </summary>
   /// <param name="msg">接收传入的消息</param>
   /// <returns>处理完的消息</returns>
    private string ServerMsgReceive(string msg) {
        return "";
    }

    /// <summary>
    /// 客户端接收到消息回调的函数
    /// </summary>
    /// <param name="msg"></param>
    private void ClientMsgRecevie(string msg) { 
    
    }

    /// <summary>
    /// 获取问题答案
    /// </summary>
    /// <param name="question">输入的问题</param>
    /// <returns></returns>
    private string GetAnswer(string question)
    {
        return channel.GetAnswer(question);
    }
}
