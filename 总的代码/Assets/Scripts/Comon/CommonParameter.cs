/*****
 * 
 * 
 *   公共参数类 
 * 
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonParameter{
    //是否能继续不断调整天平部件位置标志位
    public static bool CanSetScalePosition = true;


    //仪器部件在相应位置组装的触发器的名称集合，用来调整装配的顺序
    public static List<string> ComponentTriggerNameList=new List<string>();


}
