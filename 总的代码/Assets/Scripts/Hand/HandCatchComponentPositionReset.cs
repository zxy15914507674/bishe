using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCatchComponentPositionReset{

    ///// <summary>
    ///// 调整抓到的仪器部件的位置
    ///// </summary>
    ///// <param name="obj"></param>
    public static void ResetHasedCathedComponentPosition(GameObject obj)
    {
        switch (obj.name)
        {
            case "r2":
                r2Position.y = obj.transform.localPosition.y;
                obj.transform.localPosition = r2Position;
                obj.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                break;
            case "r3":
                obj.transform.localPosition = r3Position;
                obj.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                break;
            case "r4_left":
                obj.transform.localPosition = r4Position;
                obj.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                break;
            case "r4_right":
                obj.transform.localPosition = r4Position;
                obj.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                break;
            case "r5_left":
                r5Position.y = obj.transform.localPosition.y;
                obj.transform.localPosition = r5Position;
                obj.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                break;
            case "r5_right":
                r5Position.y = obj.transform.localPosition.y;
                obj.transform.localPosition = r5Position;
                obj.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                break;
            case "r6_left":
                obj.transform.localPosition = r6Position;
                obj.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                break;
            case "r6_right":
                obj.transform.localPosition = r6Position;
                obj.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                break;
            case "tweezer":       //镊子
                obj.transform.localPosition = tweezerPosition;
                obj.transform.localEulerAngles = new Vector3(-90f, 0f, 90f);
                break;
            case "Coin":
                obj.transform.localPosition = CoinPosition;
                break;
            default:
                break;

        }
    }


    /// <summary>
    /// 根据传入的游戏物体的名称获取与手模型的相对位置(为了调整手模型抓取物体后的位置，看上去更真实)
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    //public static Vector3 GetHasedCatchedRelativePosition(GameObject obj)
    //{
    //    switch(obj.name){
    //        case "r2":
    //            r2Position.y = 0f;
    //            return r2Position;
    //        default:
    //            return new Vector3(0f,0f,0f);
    //    }
        
    //}


    static Vector3 r6Position = new Vector3(-0.08f,-0.05f,-0.122f);
    static Vector3 r5Position = new Vector3(-0.19219f,-0.05f,-0.0332f);
    static Vector3 r4Position = new Vector3(-0.04f,-0.011f,-0.02f);
    static Vector3 r3Position = new Vector3(-0.0348f,0.044f,-0.08f);
    static Vector3 r2Position = new Vector3(-0.03f,-0.08f,-0.04f);
    

    //找到镊子后调整镊子的位置
    static Vector3 tweezerPosition = new Vector3(-0.019f,-0.093f,-0.913f);

    //抓到金币后调整金币的位置
    static Vector3 CoinPosition = new Vector3(-0.0428f,-0.043f,-0.05f);
}
