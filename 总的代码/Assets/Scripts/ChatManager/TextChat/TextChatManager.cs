using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;
using UnityEngine.UI;

namespace textchat
{
    public class TextChatManager : MonoBehaviour
{
    private Button btnSend;
    private Button btnClose;          //关闭聊天窗体
    private Button btnPop;            //弹出聊天窗体

    private GameObject TextChatUI;    //聊天窗体


    private Text txtMsg;

    private InputField inputMsg;

    private NetworkClient client;

    private float interval = 5f;
    private float time = 0f;

    void Start()
    {
        btnSend = GameObject.Find("BtnSend").GetComponent<Button>();
        btnClose = GameObject.Find("BtnClose").GetComponent<Button>();
        btnPop = GameObject.Find("BtnTextChatPop").GetComponent<Button>();

        txtMsg = GameObject.Find("TxtMsg").GetComponent<Text>();
        inputMsg = GameObject.Find("InputMsg").GetComponent<InputField>();
        TextChatUI = GameObject.Find("TextChatUI");

        if (NetworkServer.active)
        {
            NetworkServer.RegisterHandler(MessageX.MsgType, OnMessageReceived);
        }
        if (NetworkClient.active)
        {
            NetworkManager.singleton.client.RegisterHandler(MessageX.MsgType, OnClientMessageReceived);
            btnSend.onClick.AddListener(BtnSendClick);
            btnClose.onClick.AddListener(btnCloseClick);
            btnPop.onClick.AddListener(btnPopClick);
        }
        
       
        
    }


    private void btnCloseClick() {
        
        TextChatUI.GetComponent<RectTransform>().localPosition = new Vector3(0, 390,0);
    }

    private void btnPopClick() {

        TextChatUI.GetComponent<RectTransform>().localPosition = new Vector3(0, 0,0);
    }

    private void OnMessageReceived(NetworkMessage msg)
    {
        //Debug.Log(string.Format("SERVER: {0}", msg.ReadMessage<MessageX>()));
        //txtMsg.text = txtMsg.text + "\n" + msg.ReadMessage<MessageX>();
        NetworkServer.SendToAll(MessageX.MsgType, msg.ReadMessage<MessageX>());
    }

    private void SendMessageToServer(string From,string Message)
    {
       
        MessageX mx = new MessageX();
        mx.From = From;
        mx.Message =Message;

        NetworkManager.singleton.client.Send(MessageX.MsgType, mx);
      
    }

    private void OnClientMessageReceived(NetworkMessage msg)
    {
        
        //Debug.Log(string.Format("SERVER: {0}", msg.ReadMessage<MessageX>()));
        txtMsg.text = txtMsg.text + "\n" + msg.ReadMessage<MessageX>();
    }

    //发送输入的文本
    private void BtnSendClick() {
        SendMessageToServer("用户"+Random.Range(0,10), inputMsg.text.Trim());
        //发送后清空文本
        inputMsg.text = "";
    }
}



public class MessageX : MessageBase
{
    public static readonly short MsgType = short.MaxValue-3;

    // Use Field here, NOT Property
    //
    public string Message;
    public string From;

    public override string ToString()
    {
        return string.Format("{0}:{1}",From,Message);
    }
}
}
