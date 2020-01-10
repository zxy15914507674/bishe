using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceLoginUI : MonoBehaviour
{
    //摄像头图像类，继承自texture
    public CanvasRenderer renderer;

    public Button BtnLogin;            //一键登录按钮
    public Button BtnLoginRedirect;    //注册按钮点击

    public AudioSource audioSource;

    public GameObject CommonLoginUI;   //普通登录方式UI
    public GameObject FaceRegeditUI;   //人脸注册UI

    public string SceneLoginToName;               //登录后的界面名称

    WebCamTexture tex;


    //图像数据
    private byte[] image;
 


    fvc.exp.voice.TTSManager ttsManager;
    FaceRecognitionManager faceManager;
	void Start () {
        
        ttsManager = new fvc.exp.voice.TTSManager();
        faceManager = new FaceRecognitionManager();

        CommonLoginUI.SetActive(false);
        FaceRegeditUI.SetActive(false);

        BtnLogin.onClick.AddListener(BtnLoginClick);
        BtnLoginRedirect.onClick.AddListener(BtnLoginRedirectClick);

        OpenCamera();
        ttsManager.ConvertAndPlay(audioSource, "欢迎使用协同仿真实验系统，现在进行刷脸登录，请看向摄像头并按一键登录键进行登录");
	}


    /// <summary>
    /// 登录按钮点击事件
    /// </summary>
    private void BtnLoginClick() {
        try
        {
            image = GetImageByte(tex);
            string loginResult=faceManager.FaceLogin(image);
            if (loginResult != null && loginResult.Length > 0 && loginResult != "unknown")
            {
                //关闭摄像头
                CloseCamera();
                
                //查询用户
               UserInfo userInfo=new UserInfoService().GetUserInfoByStudentId(loginResult);
               ttsManager.ConvertAndPlay(audioSource,"欢迎您"+ userInfo.studentName);
               Debug.Log(userInfo.studentName);
               //保存登录信息
               UserProgramInfo.userInfo = userInfo;
               Invoke("LoadScene",3f);
            }
            else {
                ttsManager.ConvertAndPlay(audioSource, "登录失败，请调整好脸的位置");
            }
        }
        catch (System.Exception ex)
        {

            Debug.LogError("登录失败:"+ex.Message);
            return;
        }
    }


    private void LoadScene()
    {
        //进行场景的切换
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneLoginToName);
    }
    
    /// <summary>
    /// 注册按钮点击事件
    /// </summary>
    private void BtnLoginRedirectClick() {
        CloseCamera();
        this.gameObject.SetActive(false);

        CommonLoginUI.SetActive(true);
    }


    /// <summary>
    /// 打开摄像头
    /// </summary>
    public void OpenCamera() {
        //开启协程，获取摄像头图像数据
        StartCoroutine(OpenCameraIEnumerator());
        
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

    void OnDestroy() {
        CloseCamera();
    }


}
