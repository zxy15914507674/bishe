using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class NetworkStartUI:MonoBehaviour{
    Process pro;

    ProcessStartInfo processStartInfo;

    public Button BtnStartServer;
    public Button BtnStartClient;
    private InputField InputIp;
    private InputField InputGroupId;
    private InputField InputPort;
    private Text ErrorMsg;
    void Start() {
        BtnStartServer.onClick.AddListener(BtnStartServerClick);
        BtnStartClient.onClick.AddListener(BtnStartClientClick);
        InputIp = GameObject.Find("InputIP").GetComponent<InputField>();
        InputGroupId = GameObject.Find("InputGroupId").GetComponent<InputField>();
        InputPort = GameObject.Find("InputPort").GetComponent<InputField>();
        ErrorMsg = GameObject.Find("ErrorMsg").GetComponent<Text>();


        processStartInfo = new ProcessStartInfo();
        processStartInfo.FileName = "VoiceClientRoom\\AudioChatRoom.exe";
       
        processStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        //processStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
    }

    void Update() { 
    
    }

    private void BtnStartServerClick() {
        ErrorMsg.text = "";
        if (InputIp.text.Trim().Length== 0)
        {
            ErrorMsg.text = "请输入ip地址";
            return;
        }
        if (InputGroupId.text.Trim().Length == 0)
        {
            ErrorMsg.text = "请输入组id";
            return;
        }
        if (InputPort.text.Trim().Length == 0)
        {
            ErrorMsg.text = "请输入端口号";
            return;
        }

        NetworkManager.singleton.networkAddress = InputIp.text.Trim();
        NetworkManager.singleton.networkPort =Convert.ToInt32(InputPort.text.Trim());
        NetworkManager.singleton.StartHost();
        processStartInfo.Arguments = InputGroupId.text.Trim();
        pro = Process.Start(processStartInfo);
        
    }

    private void BtnStartClientClick() {
        ErrorMsg.text = "";
        if (InputIp.text.Trim().Length == 0)
        {
            ErrorMsg.text = "请输入ip地址";
            return;
        }
        if (InputGroupId.text.Trim().Length == 0)
        {
            ErrorMsg.text = "请输入组id";
            return;
        }
        if (InputPort.text.Trim().Length == 0)
        {
            ErrorMsg.text = "请输入端口号";
            return;
        }

        NetworkManager.singleton.networkAddress = InputIp.text.Trim();
        NetworkManager.singleton.networkPort = Convert.ToInt32(InputPort.text.Trim());
        NetworkManager.singleton.StartClient();
        processStartInfo.Arguments = InputGroupId.text.Trim();
        pro = Process.Start(processStartInfo);
       
    }


    //退出时杀死进程

    void OnDestroy()
    {
        if (pro != null)
        {
            pro.Kill();                  //杀死所有的进程
            pro.Dispose();               //释放所有的资源
            pro.Close();                 //关闭exe程序
        }

    }
}
