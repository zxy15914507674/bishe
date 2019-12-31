using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightVacancyTrigger : MonoBehaviour {

	// Use this for initialization
    bool  HasPutBackWeightVacancy;        //是否放回砝码架的标准位
	void Start () {
        HasPutBackWeightVacancy =false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //void OnTriggerEnter(Collider coll)
    //{
    //    if (coll.gameObject.name == "tweezer") return;
    //    //把砝码放回砝码架上
    //    Debug.Log("WeightVacancyTrigger方法:"+coll.gameObject.transform.parent.gameObject.name);
    //    if (
    //        WeightParamer.IsHasClamped
    //        && !HasPutBackWeightVacancy
    //        && coll.gameObject.name == "Weight"
    //        )
    //    {
    //        Debug.Log("放回砝码架了");
    //        coll.gameObject.transform.parent.parent = null;
    //        coll.gameObject.transform.parent.position = WeightVacancyPositionDic[coll.gameObject.transform.parent.gameObject.name];
    //        //砝码放回砝码架了
    //        //WeightParamer.IsHasClamped = false;
    //        HasPutBackWeightVacancy = true;
    //        //砝码放回砝码架了
    //        WeightParamer.IsHasClamped = false;
    //    }
    //}

    //void OnTriggerExit(Collider coll)
    //{
    //    Debug.Log("离开的物体名称:"+coll.gameObject.name);
    //    if (coll.gameObject.name == "tweezer"
    //        && HasPutBackWeightVacancy &&
    //        !WeightParamer.IsHasClamped
    //        )
    //    {
    //        Debug.Log("离开砝码架了");

    //        HasPutBackWeightVacancy = false;
    //    }
    //}


    ////砝码架放砝码对应的空位的位置集合
    //static Dictionary<string, Vector3> WeightVacancyPositionDic = new Dictionary<string, Vector3>() { 
    //    {"WeightReferencePoint0.5",new Vector3(-3.6521f,1.4757f,1.3035f)},
    //    {"WeightReferencePoint0.7",new Vector3(-3.652f,1.4891f,1.1258f)},
    //    {"WeightReferencePoint",new Vector3(-3.652f,1.4937f,0.936f)},
    //    {"WeightReferencePoint1.2",new Vector3(-3.644f,1.507f,0.741f)}
    //};
}
