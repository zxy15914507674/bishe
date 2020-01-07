using fvc.exp.qa;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace fvc.unet.start {
    public delegate void ClientMsgDelegate(string msg);

    public delegate string ServerMsgDelegate(string msg);

    public class SendQAMessage
    {
        public ClientMsgDelegate clientMsgDelegate;
        public ServerMsgDelegate serverMsgDelegate;
        private short MsgType;
        SendMessageManager sendManager;
        public SendQAMessage(short MsgType)
        {
            this.MsgType = MsgType;
            sendManager = new SendMessageManager(this.MsgType);
            sendManager.clientMsgDelegate = new ClientRecevieMsgDelegate(ClientMsgReceive);
            sendManager.serverMsgDelegate = new ServerSendMsgDelegate(ServerMsgReceive);
        }


        // <summary>
        /// 客户端接收到消息回调的函数
        /// </summary>
        /// <param name="msg"></param>
        private void ClientMsgReceive(string msg)
        {
            if (clientMsgDelegate != null)
            {
                clientMsgDelegate(msg);
            }
        }

        /// <summary>
        /// 服务端接收到消息回调的函数
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private string ServerMsgReceive(string msg) {
            if (serverMsgDelegate != null)
            {
                return serverMsgDelegate(msg);
            }
            return msg;
        }
        /// <summary>
        /// 客户端发送消息
        /// </summary>
        /// <param name="msg"></param>
        public void SendMsg(string msg)
        {
            sendManager.SendMessageToServer(msg);
        }


    }

}

