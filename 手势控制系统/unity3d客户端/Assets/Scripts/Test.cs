using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {



    public GameObject Cube;

    EndpointAddress address = new EndpointAddress("http://47.94.102.147:8000/HandService/HandGestureInterface");
    WSHttpBinding binding = new WSHttpBinding();
    HandServiceConstract.IHandContract channel;
    
    //摄像头图像类，继承自texture
    WebCamTexture tex;
    public Image WebCam;
    public MeshRenderer ma;
    public Button saveImage;
    int TimeCount = 0;


    //保存上一次的位置
    float last_pos_x;
    float last_pos_y;
    float last_width;
    float last_hight;
    bool firstEnter=true;
    float MarkWidth;  //基准宽度

    void Start()
    {
        binding.Security.Mode = SecurityMode.None;
        ChannelFactory<HandServiceConstract.IHandContract> factory = new ChannelFactory<HandServiceConstract.IHandContract>(binding, address);
        channel = factory.CreateChannel();

        //开启协程，获取摄像头图像数据
        StartCoroutine(OpenCamera());

        //saveImage.onClick.AddListener(SaveImage);
    }

    // Update is called once per frame
    void Update()
    {
        TimeCount++;
        if (tex != null && TimeCount == 50)
        {
            try
            {
                Texture2D t2d = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, true);
                //将WebCamTexture 的像素保存到texture2D中
                t2d.SetPixels(tex.GetPixels());
                //t2d.ReadPixels(new Rect(200,200,200,200),0,0,false);
                t2d.Apply();
               
                //编码
                byte[] imageTytes = t2d.EncodeToJPG();
                string returMsg=GetGestureMsg(imageTytes);
                //str(pos_x)+":"+str(pos_y)+":"+str(width)+":"+str(hight)
                if (returMsg != null && returMsg.Length > 0)
                {
                    string[] strArray = returMsg.Split(':');
                    if (strArray != null && strArray.Length == 4)
                    {
                        float pos_x = Convert.ToSingle(strArray[0]);
                        float pos_y = Convert.ToSingle(strArray[1]);
                        float width = Convert.ToSingle(strArray[2]);
                        float hight = Convert.ToSingle(strArray[3]);

                        if (firstEnter)
                        {
                            firstEnter = false;
                            MarkWidth = width;
                        }
                        else {
                            float turnLeftGap = pos_x - last_pos_x;
                            float turnUpGap = pos_y - last_pos_y;
                            if (turnLeftGap >10)
                            {
                                Debug.Log("向右");
                                //真实动作向左
                                Cube.transform.Translate(Vector3.left,Space.Self);
                            }
                            if (turnLeftGap < -10)
                            {
                                Debug.Log("向左");
                                //真实动作向右
                                Cube.transform.Translate(Vector3.right, Space.Self);
                            }

                            if (turnUpGap > 10)
                            {
                                Debug.Log("向下");
                                Cube.transform.Translate(Vector3.down, Space.Self);
                            }

                            if (turnUpGap <-10)
                            {
                                Debug.Log("向上");
                                Cube.transform.Translate(Vector3.up, Space.Self);
                            }

                            //计算缩放
                            float scale = width / MarkWidth;
                            if (scale > 1)
                            {
                                //向前
                                if ((last_width - width) >10)
                                {
                                    Cube.transform.Translate(Vector3.forward, Space.Self);
                                }
                            }
                            else
                            { 
                               //向后
                                
                                if ((last_width - width) <-10)
                                {
                                    Cube.transform.Translate(Vector3.back, Space.Self);
                                }
                            }
                            
                        }

                        last_pos_x = pos_x;
                        last_pos_y = pos_y;
                        last_width = width;
                        last_hight = hight;

                    }

                }

                TimeCount = 0;
                Debug.Log("11");
            }
            catch (System.Exception ex)
            {

                Debug.LogError("发生异常:"+ex.Message);
            }
           
        }
    }

    private string GetGestureMsg(byte[] buffer)
    {
        string returnMessage = channel.GetGestureInfo(buffer);
        return returnMessage;
    }
   

    IEnumerator OpenCamera()
    {
        //等待用户允许访问
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        //如果用户允许访问，开始获取图像        
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            //先获取设备
            WebCamDevice[] device = WebCamTexture.devices;

            string deviceName = device[0].name;
            //然后获取图像
            tex = new WebCamTexture(deviceName);
            //将获取的图像赋值
            ma.material.mainTexture = tex;
            //开始实施获取
            tex.Play();

        }
    }


    private void SaveImage()
    {
        //在上一段代码中加入该方法
        CameraTextureSave.Save(tex);

    }
}




public class CameraTextureSave : MonoBehaviour
{

    public static void Save(WebCamTexture t)
    {
        Texture2D t2d = new Texture2D(t.width, t.height, TextureFormat.ARGB32, true);
        //将WebCamTexture 的像素保存到texture2D中
        t2d.SetPixels(t.GetPixels());
        //t2d.ReadPixels(new Rect(200,200,200,200),0,0,false);
        t2d.Apply();
        //编码
        byte[] imageTytes = t2d.EncodeToJPG();

        //存储
        File.WriteAllBytes("./Pic/" + Time.time + ".jpg", imageTytes);
    }
}
