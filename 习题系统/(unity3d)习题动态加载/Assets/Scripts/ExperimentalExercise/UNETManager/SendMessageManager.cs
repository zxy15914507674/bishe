using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;
using UnityEngine.UI;

namespace fvc.exp {
    
    public delegate void ClientRecevieMsgDelegate(string msg);


    public delegate string ServerSendMsgDelegate(string msg);

    public class SendMessageManager
    {
        /// <summary>
        /// 客户端接收到消息的委托
        /// </summary>
        public ClientRecevieMsgDelegate clientMsgDelegate;

        /// <summary>
        /// 服务端向各个客户端发送消息的委托
        /// </summary>
        public ServerSendMsgDelegate serverMsgDelegate;

        private NetworkClient client;

        private float interval = 5f;
        private float time = 0f;
        private short MsgType;
        public SendMessageManager(short MsgType)
        {
            this.MsgType = MsgType;
            if (NetworkServer.active)
            {
                NetworkServer.RegisterHandler(this.MsgType, OnMessageReceived);
            }
            if (NetworkClient.active)
            {
                NetworkManager.singleton.client.RegisterHandler(this.MsgType, OnClientMessageReceived);
            }
        }


        /// <summary>
        /// 服务端接收到消息回调的函数
        /// </summary>
        /// <param name="msg"></param>
        private void OnMessageReceived(NetworkMessage msg)
        {
            MessageX mx = new MessageX();

            string RecevieMsg= msg.ReadMessage<MessageX>().Message;
            if (serverMsgDelegate != null)
            {
                RecevieMsg=serverMsgDelegate(RecevieMsg);
            }
            mx.Message = RecevieMsg;
            NetworkServer.SendToAll(this.MsgType,mx);
        }

        /// <summary>
        /// 客户端向服务端发送消息
        /// </summary>
        /// <param name="Message">要发送的消息</param>
        public void SendMessageToServer(string Message)
        {

            MessageX mx = new MessageX();

            mx.Message = Message;

            NetworkManager.singleton.client.Send(this.MsgType, mx);

        }


        /// <summary>
        /// 客户端接收到消息回调的函数
        /// </summary>
        /// <param name="msg"></param>
        private void OnClientMessageReceived(NetworkMessage msg)
        {
            //Debug.Log(string.Format("SERVER: {0}", msg.ReadMessage<MessageX>()));
            string ReceiveMsg = msg.ReadMessage<MessageX>().Message;

            if (clientMsgDelegate != null)
            {
                clientMsgDelegate(ReceiveMsg);
            }
        }

    }


    public class MessageX : MessageBase
    {
        public string Message;
    }
}





