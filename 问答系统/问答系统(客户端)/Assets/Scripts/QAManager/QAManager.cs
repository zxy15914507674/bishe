using System.Collections;
using System.Collections.Generic;
using System.ServiceModel;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class QAManager : MonoBehaviour {

    private InputField InputQuestion;          //输入问题的输入框
    private Button BtnAsk;                     //提问按钮
    private Text QATxtMsg;                     //显示提问问题和返回答案的文本框 

    EndpointAddress address;
    WSHttpBinding binding;
    QAServiceConstract.IQAContract channel;
    ChannelFactory<QAServiceConstract.IQAContract> factory;
    fvc.unet.start.SendQAMessage qaMessage = new fvc.unet.start.SendQAMessage(short.MaxValue - 4);

    

	void Start () {
        InputQuestion = GameObject.Find("InputQuestion").GetComponent<InputField>();
        BtnAsk = GameObject.Find("BtnAsk").GetComponent<Button>();
        QATxtMsg = GameObject.Find("QATxtMsg").GetComponent<Text>();

        BtnAsk.onClick.AddListener(BtnAskClick);


        qaMessage.serverMsgDelegate = new fvc.unet.start.ServerMsgDelegate(ServerMsgReceive);
        qaMessage.clientMsgDelegate = new fvc.unet.start.ClientMsgDelegate(ClientMsgRecevie);


        if (NetworkServer.active)
        {
            address = new EndpointAddress("http://47.94.102.147:8000/QAService/QAInterface");
            binding = new WSHttpBinding();
            binding.Security.Mode = SecurityMode.None;
            factory = new ChannelFactory<QAServiceConstract.IQAContract>(binding, address);
            channel = factory.CreateChannel();
        }    
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void BtnAskClick() { 
          //获取输入的问题
        string inputFiled = InputQuestion.text.Trim();
        if (inputFiled.Length == 0)
        {
            return;
        }
        //加上用户名
        inputFiled = "用户" + Random.Range(0, 100)+":" + inputFiled;
        //发送到服务端，然服务端处理
        qaMessage.SendMsg(inputFiled);

        //清空输入框
        InputQuestion.text = "";
    }

   /// <summary>
    /// 服务端接收到消息回调的函数
   /// </summary>
   /// <param name="msg">接收传入的消息</param>
   /// <returns>处理完的消息</returns>
    private string ServerMsgReceive(string msg) {
        //获取问题
        string[] msgArray = msg.Split(':');
        string question = msgArray[1];
        //把问题写入数据库
        QA qaObj = new QA() {  question=question};
       
       
        try
        {
            System.Threading.Thread thread = new System.Threading.Thread(new QADBManager().AddQuestion);
            thread.IsBackground = true;
            //thread.Abort();
            thread.Start(qaObj);
            
        }
        catch (System.Exception ex)
        {

            Debug.LogError("问题写入数据库出错"+ex);
        }
        //获取答案
        string answer = channel.GetAnswer(question);
        answer = "chatterbot:" + answer;
        string questionAndAnswer = msg + "\n" + answer;
        return questionAndAnswer;
    }

    /// <summary>
    /// 客户端接收到消息回调的函数
    /// </summary>
    /// <param name="msg">获取到的消息</param>
    private void ClientMsgRecevie(string msg) { 
        //显示到文本框上
        QATxtMsg.text = QATxtMsg.text +"\n"+ msg;
    }

}
