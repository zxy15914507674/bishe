using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonLoginManager : MonoBehaviour {

    
    public InputField InputStudentName;            //用户名输入文本框
    public InputField InputStudentNumber;          //学号输入文本框
    public InputField InputPasswd;                 //密码输入文本框


    public Button btn_login;                      //登录按钮
    public Button btn_regedit;                    //注册按钮

    public Text TxtMessageShow;                   //提示信息显示文本


    public string SceneLoginToName;               //登录后的界面名称

    public AudioSource audioSource;


    public GameObject FaceRegeditUI;

    UserInfoService userInfoService;

    fvc.exp.voice.TTSManager ttsManager;

	void Start () {
        btn_login.onClick.AddListener(btn_loginClick);
        btn_regedit.onClick.AddListener(btn_regeditClick);

        userInfoService = new UserInfoService();
        ttsManager = new fvc.exp.voice.TTSManager();

        FaceRegeditUI.SetActive(false);
	}

    /// <summary>
    /// 登录按钮点击事件
    /// </summary>
    private void btn_loginClick() {
        TxtMessageShow.text = "";

        //数据安全验证
        #region 数据安全验证
        if (InputStudentName.text.Trim().Length == 0)
        {
            TxtMessageShow.text = "请输入姓名";
            ttsManager.ConvertAndPlay(audioSource, "请输入姓名");
            return;
        }

        if (InputStudentNumber.text.Trim().Length == 0)
        {
            TxtMessageShow.text = "请输入学号";
            ttsManager.ConvertAndPlay(audioSource, "请输入学号");
            return;
        }

        if (InputPasswd.text.Trim().Length == 0)
        {
            TxtMessageShow.text = "请输入密码";
            ttsManager.ConvertAndPlay(audioSource, "请输入密码");
            return;
        }
        #endregion


        #region 对象封装
        UserInfo userInfo = new UserInfo() {
            studentNumber = InputStudentNumber.text.Trim(),
            studentName = InputStudentName.text.Trim(),
            pwd = InputPasswd.text.Trim()
        };
        #endregion


        #region 操作后台进行登录操作
        try
        {
          
            userInfo=userInfoService.UserLogin(userInfo);
  
            if (userInfo!= null)
            {
                //进行场景的切换
                UnityEngine.SceneManagement.SceneManager.LoadScene(SceneLoginToName);
            }
            else {
                TxtMessageShow.text = "学号或者密码错误";
                ttsManager.ConvertAndPlay(audioSource, "学号或者密码错误");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("登录失败:"+ex);
            TxtMessageShow.text = "登录失败!请稍后重试";
            ttsManager.ConvertAndPlay(audioSource, "登录失败!请稍后重试");
            return;
        }
        #endregion

    }

    

   


    

    /// <summary>
    /// 注册按钮点击事件
    /// </summary>
    private void btn_regeditClick() {
        TxtMessageShow.text = "";

        //数据安全验证
        #region 数据安全验证
        if (InputStudentName.text.Trim().Length == 0)
        {
            TxtMessageShow.text = "请输入姓名";
            ttsManager.ConvertAndPlay(audioSource, "请输入姓名");
            return;
        }

        if (InputStudentNumber.text.Trim().Length == 0)
        {
            TxtMessageShow.text = "请输入学号";
            ttsManager.ConvertAndPlay(audioSource, "请输入学号");
            return;
        }

        if (InputPasswd.text.Trim().Length == 0)
        {
            TxtMessageShow.text = "请输入密码";
            ttsManager.ConvertAndPlay(audioSource, "请输入密码");
            return;
        }
        #endregion


        #region 对象封装
        UserInfo userInfo = new UserInfo()
        {
            studentNumber = InputStudentNumber.text.Trim(),
            studentName = InputStudentName.text.Trim(),
            pwd = InputPasswd.text.Trim()
        };
        #endregion


        #region 验证是否有人注册过
        try
        {
           bool result=userInfoService.CheckStudentIdIsExist(InputStudentNumber.text.Trim());
           if (result == true)
           {
               TxtMessageShow.text = "该学号已经注册过";
               ttsManager.ConvertAndPlay(audioSource, "该学号已经注册过");
               return;
           }
        }
        catch (System.Exception ex)
        {
            
            Debug.LogError("注册操作失败"+ex.Message);
            TxtMessageShow.text = "注册失败!请稍后重试";
            ttsManager.ConvertAndPlay(audioSource, "注册失败!请稍后重试");
            return;
        }
        #endregion

        #region 操作后台进行注册操作
        try
        {
           int result=userInfoService.AddUser(userInfo);
           if (result > 0)
           {
               TxtMessageShow.text = "现在进行人脸注册,请看向摄像头并调整好头部的位置";
               //ttsManager.ConvertAndPlay(audioSource, "注册成功，请进行登录操作");
               //弹出人脸注册的窗口
               ttsManager.ConvertAndPlay(audioSource, "现在进行人脸注册,请看向摄像头并调整好头部的位置并按确认键");
               FaceRegeditUI.SetActive(true);
               FaceRegeditUI.GetComponent<FaceRegeditUI>().OpenCamera();
           }
           else {
               TxtMessageShow.text = "注册失败!请稍后重试";
               ttsManager.ConvertAndPlay(audioSource, "注册失败!请稍后重试");
               return;
           }
        }
        catch (System.Exception ex)
        {

            Debug.LogError("注册操作失败"+ex.Message);
            TxtMessageShow.text = "注册失败!请稍后重试";
            ttsManager.ConvertAndPlay(audioSource, "注册失败!请稍后重试");
            return;
        }
        #endregion
    }




	// Update is called once per frame
	void Update () {
		
	}
}
