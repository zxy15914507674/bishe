using fvc.exp.score;
using fvc.unet.tip;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartExperiment : MonoBehaviour
{
    public Button btnStartExperiment;
	void Start () {
        btnStartExperiment.onClick.AddListener(BtnStartExperimentClick);
    }
	
	// Update is called once per frame
	void Update () {
 
	}

    private void BtnStartExperimentClick() {
        GetComponent<StateSystem>().StartExperimentalExercise("Scene1");
        
    }
}
