using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Test : MonoBehaviour {

    GameObject r3;
    GameObject Hand;
	void Start () {
        //Transform r1 = GameObject.Find("r1").GetComponent<Transform>();
        //Transform r2 = GameObject.Find("r2").GetComponent<Transform>();
        //Transform r3 = GameObject.Find("r3").GetComponent<Transform>();
        //Transform r4_left = GameObject.Find("r4_left").GetComponent<Transform>();
        //Transform r4_rigth = GameObject.Find("r4_rigth").GetComponent<Transform>();
        //Transform r5_left = GameObject.Find("r5_left").GetComponent<Transform>();
        //Transform r5_right = GameObject.Find("r5_right").GetComponent<Transform>();
        //Transform r6_left = GameObject.Find("r6_left").GetComponent<Transform>();
        //Transform r6_right = GameObject.Find("r6_right").GetComponent<Transform>();

        //Vector3 r2Position = r2.position - r1.position;
        //Vector3 r3Position = r3.position - r1.position;
        //Vector3 r4_leftPosition = r4_left.position - r1.position;
        //Vector3 r4_rigthPosition = r4_rigth.position - r1.position;
        //Vector3 r5_leftPosition = r5_left.position - r1.position;
        //Vector3 r5_rightPosition = r5_right.position - r1.position;
        //Vector3 r6_leftPosition = r6_left.position - r1.position;
        //Vector3 r6_rightPosition = r6_right.position - r1.position;

        //Debug.Log("r2Position:" + r2Position);
        //Debug.Log("r3Position:" + r3Position);
        //Debug.Log("r4_leftPosition:" + r4_leftPosition);
        //Debug.Log("r4_rigthPosition:" + r4_rigthPosition);
        //Debug.Log("r5_leftPosition:" + r5_leftPosition);
        //Debug.Log("r5_rightPosition:" + r5_rightPosition);
        //Debug.Log("r6_leftPosition:" + r6_leftPosition);
        //Debug.Log("r6_rightPosition:" + r6_rightPosition);
        //string positionMessage = "";
        //positionMessage = "r2Position:" + r2Position + "\r\n" +
        //                "r3Position:" + r3Position + "\r\n" +
        //                "r4_leftPosition:" + r4_leftPosition + "\r\n" +
        //                "r4_rigthPosition:" + r4_rigthPosition + "\r\n" +
        //                "r5_leftPosition:" + r5_leftPosition + "\r\n" +
        //                "r5_rightPosition:" + r5_rightPosition + "\r\n" +
        //                "r6_leftPosition:" + r6_leftPosition + "\r\n" +
        //                "r6_rightPosition:" + r6_rightPosition;

        //File.WriteAllText("误差值.txt", positionMessage);

        //Transform objLeft = GameObject.Find("Left").GetComponent<Transform>();
        //Debug.Log("Left:"+objLeft.position);
        //Transform objRight = GameObject.Find("Right").GetComponent<Transform>();
        //Debug.Log("Right:" + objRight.position);

        Hand = GameObject.Find("Hand");
        r3 = GameObject.Find("r3");
        r3.transform.parent = Hand.transform;
        r3.transform.localPosition = new Vector3(-0.0348f, 0.044f, -0.08f);


        
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
