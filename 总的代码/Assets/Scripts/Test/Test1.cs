using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour {
    //天平模型部件集合
    public GameObject[] scales;
    Vector3 origin = new Vector3(2.56f, 0.318f, 1f);

	void Start () {
        CreateScales(origin);
	}
	
	
	void Update () {
		
	}

    void CreateScales(Vector3 origin) {
        Vector3 r21 = new Vector3(-0.01f, -0.022f, 0f);
        Vector3 r31 = new Vector3(-0.01f, 0.339f, 0.03f);
        Vector3 r4Left = new Vector3(0.2f,0.264f,0.026f);
        Vector3 r4Right = new Vector3(-0.228f, 0.264f, 0.026f);
        Vector3 r5Left = new Vector3(0.2f, -0.01f, 0.026f);
        Vector3 r5Right = new Vector3(-0.228f, -0.01f, 0.026f);
        Vector3 r6Left = new Vector3(0.202f, -0.195f, 0.0026f);
        Vector3 r6Right = new Vector3(-0.228f, -0.195f, 0.026f);
        GameObject r1Obj = GameObject.Instantiate(scales[0],origin,Quaternion.identity);
        Vector3 r1 = r1Obj.transform.position;
        r21 =r1 + r21;
        r31 = r1 + r31;
        r4Left = r1 + r4Left;
        r4Right = r1 + r4Right;
        r5Left = r1 + r5Left;
        r5Right = r1 + r5Right;
        r6Left = r1 + r6Left;
        r6Right = r1 + r6Right;
       
        GameObject r2Obj = GameObject.Instantiate(scales[1],r21, Quaternion.identity);
        GameObject r3Obj = GameObject.Instantiate(scales[2], r31, Quaternion.identity);
        GameObject r4LeftObj = GameObject.Instantiate(scales[3],r4Left, Quaternion.identity);
        GameObject r4RightObj = GameObject.Instantiate(scales[3],r4Right, Quaternion.identity);
        GameObject r5LeftObj = GameObject.Instantiate(scales[4], r5Left, Quaternion.identity);
        GameObject r5RightObj = GameObject.Instantiate(scales[4], r5Right, Quaternion.identity);
        GameObject r6LeftObj = GameObject.Instantiate(scales[5], r6Left, Quaternion.identity);
        GameObject r6RightObj = GameObject.Instantiate(scales[5], r6Right, Quaternion.identity);


    }
}
