using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceRegeditUI : MonoBehaviour {

    //摄像头图像类，继承自texture
    public CanvasRenderer renderer;

    public Button BtnGetImage;            //获取摄像头数据

    public AudioSource audioSource;

    public InputField InputStudentNumber;


    public GameObject FaceLoginUI;                //人脸登录界面 
    public GameObject FaceRegeditUInterface;              //人脸注册界面
    public GameObject CommonLoginUI;              //普通注册界面


    WebCamTexture tex;


    //三次采集的图像
    private byte[] image1;
    private byte[] image2;
    private byte[] image3;

    int count = 0;     //记录采集的次数


    fvc.exp.voice.TTSManager ttsManager;
    FaceRecognitionManager faceManager;
	void Start () {
        ttsManager = new fvc.exp.voice.TTSManager();
        faceManager = new FaceRecognitionManager();
        BtnGetImage.onClick.AddListener(BtnGetImageClick);
       
	}
	
	
	

    private void BtnGetImageClick()
    {
        count++;
        if (count == 1)
        {
            image1 = GetImageByte(tex);
            ttsManager.ConvertAndPlay(audioSource,"请做微笑表情并按确认键");
        }
        else if (count == 2)
        {
            image2 = GetImageByte(tex);
            ttsManager.ConvertAndPlay(audioSource, "请做难过表情并按确认键");
        }
        else if (count == 3)
        {
            image3 = GetImageByte(tex);
            count = 0;

            try
            {
                faceManager.FaceRegistration(image1, image2, image3, InputStudentNumber.text.Trim());
                ttsManager.ConvertAndPlay(audioSource, "注册成功，请进行登录操作");
            }
            catch (System.Exception ex)
            {

                Debug.LogError("人脸注册失败"+ex.Message);
                ttsManager.ConvertAndPlay(audioSource, "人脸注册失败");
            }
            
            //关闭摄像头
            CloseCamera();
            //this.gameObject.SetActive(false);
            FaceLoginUI.SetActive(true);
            FaceRegeditUInterface.SetActive(false);
            CommonLoginUI.SetActive(false);
            //打开人脸登录的摄像头
            FaceLoginUI.GetComponent<FaceLoginUI>().OpenCamera();
        }
    }


    /// <summary>
    /// 打开摄像头
    /// </summary>
    public void OpenCamera() {
        //开启协程，获取摄像头图像数据
        StartCoroutine(OpenCameraIEnumerator());
        count = 0;
    }

    IEnumerator OpenCameraIEnumerator()
    {
        //等待用户允许访问
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        //如果用户允许访问，开始获取图像        
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            //先获取设备
            WebCamDevice[] device = WebCamTexture.devices;
            if (device != null && device.Length > 0)
            {
                string deviceName = device[device.Length - 1].name;
                //然后获取图像
                tex = new WebCamTexture(deviceName);
                //将获取的图像赋值
                renderer.SetTexture(tex);

                //开始实施获取
                tex.Play();
            }


        }
    }


    /// <summary>
    /// 关闭摄像头
    /// </summary>
    public void CloseCamera() {
        tex.Stop();
    }



    private byte[]  GetImageByte(WebCamTexture t)
    {
        Texture2D t2d = new Texture2D(t.width, t.height, TextureFormat.ARGB32, true);
        //将WebCamTexture 的像素保存到texture2D中
        t2d.SetPixels(t.GetPixels());
        //t2d.ReadPixels(new Rect(200,200,200,200),0,0,false);
        t2d.Apply();
        //编码
        byte[] imageTytes = t2d.EncodeToJPG();
        return imageTytes;
    }


}
