/*****
 * 
 * 
 *   位置常量 
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionErrorContanst{ 
    //游戏物体r2和r1之间相对位置大小，下面的同理
    public static Vector3 r21 = new Vector3(-0.013f, -0.022f, -0.0126f);
    public static Vector3 r31 = new Vector3(-0.01f, 0.339f, 0.03f);
    public static Vector3 r4Left = new Vector3(0.2f, 0.264f, 0.026f);
    public static Vector3 r4Right = new Vector3(-0.228f, 0.264f, 0.026f);
    public static Vector3 r5Left = new Vector3(0.2f, -0.01f, 0.026f);
    public static Vector3 r5Right = new Vector3(-0.228f, -0.01f, 0.026f);
    public static Vector3 r6Left = new Vector3(0.202f, -0.195f, 0.0026f);
    public static Vector3 r6Right = new Vector3(-0.228f, -0.195f, 0.026f);



    //手模型往左右、往前后、往下移动的临界值
    public static float y_Down = 1.474f;
    public static float x_Left = -3f;
    public static float x_Right = -6f;
    public static float z_Back = -1f;
    public static float z_Forward = 2.5f;
}
