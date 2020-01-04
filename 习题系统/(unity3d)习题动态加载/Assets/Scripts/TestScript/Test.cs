using fvc.exp.score;
using FVCPosExamUiController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            //GameObject.Find("QuestionParent").GetComponent<FVCStateSystem>().StartExperimentalExercise("Scene2");
            //GetComponent<FVCPreExamUiController>()._OnGameStateChanged(FVCGameState.PostExam);
            GetComponent<FVCStateSystem>().StartExperimentalExercise("Scene1");
        }
        else if (Input.GetMouseButtonDown(2))
        {
            Debug.Log(new FVCScore().CalculateScore());
        }
	}
}
