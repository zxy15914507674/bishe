using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightBoxCollider: MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    void OnCollisionEnter(Collision coll) {
       
        if (GameObject.Find("Hand")==null)
        {
            return;
        }

        if (GameObject.Find("Hand").GetComponent<SendToServerByPlayer>().isClient) {
            GameObject hasCathced = GameObject.Find("Hand").GetComponent<HandControl>().hasCatched;
            if (hasCathced == null) return;
            if (hasCathced.name == "tweezer") {    //前面几个条件都是为了保证在对应的单个客户端执行

                if (coll.gameObject.name == "Weight")
                {
                    if (WeightVacancyPositionDic.ContainsKey(coll.gameObject.transform.parent.gameObject.name))
                    {
                        Debug.Log("WeightBoxCollider发生碰撞:" + coll.gameObject.transform.name);
                        coll.gameObject.transform.parent.parent = null;
                        WeightParamer.IsHasClamped = false;
                        coll.gameObject.transform.parent.position = WeightVacancyPositionDic[coll.gameObject.transform.parent.gameObject.name];
                    }
                }
            }
        }
    }



    //砝码架放砝码对应的空位的位置集合
    static Dictionary<string, Vector3> WeightVacancyPositionDic = new Dictionary<string, Vector3>() { 
        {"WeightReferencePoint0.5",new Vector3(-3.6521f,1.4457f,1.3035f)},
        {"WeightReferencePoint0.7",new Vector3(-3.652f,1.4591f,1.1258f)},
        {"WeightReferencePoint",new Vector3(-3.652f,1.4637f,0.936f)},
        {"WeightReferencePoint1.2",new Vector3(-3.644f,1.477f,0.741f)}
    };
}
