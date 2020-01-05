using fvc.exp.score;
using fvc.unet.tip;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {

    public Button btnSendTest;
    public Text txtReceive;
    SendSynchronizationTipMessage sendTip; 

	void Start () {
        btnSendTest.onClick.AddListener(BtnSendTestClick);
        sendTip = new SendSynchronizationTipMessage(short.MaxValue - 3);
        sendTip.tipMsgDelegate = new TipMsgDelegate(Recevie);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            GetComponent<StateSystem>().StartExperimentalExercise("Scene1");
        }
        else if (Input.GetMouseButtonDown(2))
        {
            Debug.Log(new Score().CalculateScore());
        }
	}

    private void BtnSendTestClick() {

        sendTip.SendMsg("客户端发送消息啦");
    }

    private void Recevie(string msg) {
        txtReceive.text = msg;
    }

   
}
