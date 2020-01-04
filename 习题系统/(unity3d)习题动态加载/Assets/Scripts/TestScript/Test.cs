using fvc.exp.score;
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
            GetComponent<StateSystem>().StartExperimentalExercise("Scene1");
        }
        else if (Input.GetMouseButtonDown(2))
        {
            Debug.Log(new Score().CalculateScore());
        }
	}
}
