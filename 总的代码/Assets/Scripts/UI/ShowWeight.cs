using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowWeight : MonoBehaviour {

	
	void Start () {
		
	}
	
	
	void Update () {
		
	}

    public void ShowWeightMessage(string msg) {
        if (msg==null) return;
        GameObject.Find("TxtShowWeight").GetComponent<Text>().text = msg;
    }
}
