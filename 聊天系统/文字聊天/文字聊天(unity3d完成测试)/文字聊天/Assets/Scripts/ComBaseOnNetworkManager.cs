using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;
using UnityEngine.UI;

public class ComBaseOnNetworkManager : MonoBehaviour
{
    public Button btnSend;
    public Text txtMsg;

    public InputField inputMsg;

    private NetworkClient client;

    private float interval = 5f;
    private float time = 0f;

    void Start()
    {
        btnSend = GameObject.Find("BtnSend").GetComponent<Button>();
        txtMsg = GameObject.Find("TxtMsg").GetComponent<Text>();
        inputMsg = GameObject.Find("InputMsg").GetComponent<InputField>();

        if (NetworkServer.active)
        {
            NetworkServer.RegisterHandler(MessageX.MsgType, OnMessageReceived);
        }
        if (NetworkClient.active)
        {
            NetworkManager.singleton.client.RegisterHandler(MessageX.MsgType, OnClintMessageReceived);
            btnSend.onClick.AddListener(BtnSendClick);
        }
        
       
        
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

    private void OnClintMessageReceived(NetworkMessage msg)
    {
        //Debug.Log(string.Format("SERVER: {0}", msg.ReadMessage<MessageX>()));
        txtMsg.text = txtMsg.text + "\n" + msg.ReadMessage<MessageX>();
    }

    private void BtnSendClick() {
        SendMessageToServer("NetworkMangerBase Sample", inputMsg.text.Trim());
    }
}



public class MessageX : MessageBase
{
    public static readonly short MsgType = short.MaxValue;

    // Use Field here, NOT Property
    //
    public string Message;
    public string From;

    public override string ToString()
    {
        return string.Format("Message '{0}' from '{1}'", Message, From);
    }
}
