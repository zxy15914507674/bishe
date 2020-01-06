/****
 * 
 *   发送上下一题指令的操作类 
 *   
 *   字符串指令  Choice:Next              代表选择题下一题指令
 *   字符串指令  Choice:Previous          代表选择题上一题指令
 *   
 *   字符串指令  Completion:Next          代表填空题下一题指令
 *   字符串指令  Completion:Previous      代表填空题上一题指令
 *   
 *   
 */
using fvc.exp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fvc.unet.change {
    public delegate void ClientMsgDelegate(string msg);
    public class SendNextOrPreviousQuestionMessage
    {
        public ClientMsgDelegate clientMsgDelegate;
        private short MsgType;
        SendMessageManager sendManager;
        public SendNextOrPreviousQuestionMessage(short MsgType)
        {
            this.MsgType = MsgType;
            sendManager = new SendMessageManager(this.MsgType);
            sendManager.clientMsgDelegate = new ClientRecevieMsgDelegate(ClientMsgReceive);
            
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
        /// 客户端发送消息
        /// </summary>
        /// <param name="msg"></param>
        public void SendMsg(string msg)
        {
            sendManager.SendMessageToServer(msg);
        }

    }
}

