/***
 * 
 *   打断定时,发送提示信息，重新定时 
 * 
 */


using fvc.exp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace fvc.unet.tip {
    public delegate void TipMsgDelegate(string msg);

    public class SendSynchronizationTipMessage
    {
        public TipMsgDelegate tipMsgDelegate;
        private short MsgType;
        SendMessageManager sendManager;
        public SendSynchronizationTipMessage(short MsgType)
        {
            this.MsgType = MsgType;
            sendManager = new SendMessageManager(this.MsgType);
            sendManager.clientMsgDelegate = new ClientRecevieMsgDelegate(MsgReceive);
        }

        /// <summary>
        /// 接收到消息回调的函数
        /// </summary>
        /// <param name="msg"></param>
        private void MsgReceive(string msg)
        {
            if (tipMsgDelegate != null)
            {
                tipMsgDelegate(msg);
            }
        }

        /// <summary>
        /// 客户端发送消息
        /// </summary>
        /// <param name="msg"></param>
        public void SendMsg(string msg) {
            sendManager.SendMessageToServer(msg);
        }

    }

}


