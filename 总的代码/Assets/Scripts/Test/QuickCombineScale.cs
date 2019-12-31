using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickCombineScale : MonoBehaviour {

    GameObject r2Trigger;
    GameObject r3Trigger;
    GameObject r4RightTrigger;
    GameObject r5RightTrigger;
    GameObject r6RightTrigger;
    GameObject r4LeftTrigger;
    GameObject r5LeftTrigger;
    GameObject r6LeftTrigger;

    GameObject r2;
    GameObject r3;
    GameObject r4_left;
    GameObject r4_right;
    GameObject r5_left;
    GameObject r5_right;
    GameObject r6_left;
    GameObject r6_right;

	void Start () {
        r2Trigger = GameObject.Find("r2Trigger");
        r3Trigger = GameObject.Find("r3Trigger");
        r4RightTrigger = GameObject.Find("r4RightTrigger");
        r5RightTrigger = GameObject.Find("r5RightTrigger");
        r6RightTrigger = GameObject.Find("r6RightTrigger");
        r4LeftTrigger = GameObject.Find("r4LeftTrigger");
        r5LeftTrigger = GameObject.Find("r5LeftTrigger");
        r6LeftTrigger = GameObject.Find("r6LeftTrigger");

        CommonParameter.ComponentTriggerNameList.Add("r3Trigger");
        CommonParameter.ComponentTriggerNameList.Add("r4RightTrigger");
        CommonParameter.ComponentTriggerNameList.Add("r5RightTrigger");
        CommonParameter.ComponentTriggerNameList.Add("r6RightTrigger");
        CommonParameter.ComponentTriggerNameList.Add("r4LeftTrigger");
        CommonParameter.ComponentTriggerNameList.Add("r5LeftTrigger");
        CommonParameter.ComponentTriggerNameList.Add("r6LeftTrigger");
       

        r2 = GameObject.Find("r2");
        r3 = GameObject.Find("r3");
        r4_left = GameObject.Find("r4_left");
        r4_right = GameObject.Find("r4_right");
        r5_left = GameObject.Find("r5_left");
        r5_right = GameObject.Find("r5_right");
        r6_left = GameObject.Find("r6_left");
        r6_right = GameObject.Find("r6_right");

	}
	

	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
            Start();
            r2.transform.position = r2Trigger.transform.position;
            r3.transform.position = r3Trigger.transform.position;
            r4_left.transform.position = r4LeftTrigger.transform.position;
            r4_right.transform.position = r4RightTrigger.transform.position;
            r5_left.transform.position = r5LeftTrigger.transform.position;
            r5_right.transform.position = r5RightTrigger.transform.position;
            r6_left.transform.position = r6LeftTrigger.transform.position;
            r6_right.transform.position = r6RightTrigger.transform.position;

        }
	}
}
