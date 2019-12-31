using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class HandControl : NetworkBehaviour {

    //中指
    Transform MIDLK31;
    Transform MIDLK21;
    Transform MIDLK11;

    Vector3 MIDLK31_Start;  //初始旋转角度
    Vector3 MIDLK21_Start;
    Vector3 MIDLK11_Start;

    
    //食指
    Transform INDXK31;
    Transform INDXK21;
    Transform INDXK11;

    Vector3 INDXK31_Start; //初始旋转角度
    Vector3 INDXK21_Start;
    Vector3 INDXK11_Start;

   
    //无名指
    Transform RINGK31;
    Transform RINGK21;
    Transform RINGK11;

    Vector3 RINGK31_Start; //初始旋转角度
    Vector3 RINGK21_Start;
    Vector3 RINGK11_Start;

   
    //小指
    Transform PNKY31;
    Transform PNKY21;
    Transform PNKY11;

    Vector3 PNKY31_Start; //初始旋转角度
    Vector3 PNKY21_Start;
    Vector3 PNKY11_Start;

   

    //拇指
    Transform THMB21;
    Transform THMB11;

    Vector3 THMB21_Start;  //初始旋转角度
    Vector3 THMB11_Start;


    Dictionary<string, string> handNames;   //手指全部关节名称集合

    float agent=0f;         //所有手指弯曲的角度
    bool HandModelIsHasedBend;  //手模型是否已经弯曲(代表是否开始抓取物体)
    public GameObject hasCatched;      //手模型已经抓到的仪器部件

	void Start ()
    {

       if(isLocalPlayer){

           gameObject.name = "Hand";
           #region 从配置文件中读取手模型关节的名称，并初始化手模型数据
           if (File.Exists("Contact.xml"))
           {
               //初始化手指全部关节名称集合
               handNames = new Dictionary<string, string>();
               //将文件加载进来
               XDocument document = XDocument.Load("Contact.xml");
               //获取根元素
               XElement root = document.Root;
               //获取indx节点下的所有元素
               IEnumerable<XElement> indxs = root.Element("indx").Elements();
               foreach (XElement item in indxs)
               {
                   handNames.Add(item.Name.ToString(), item.Value);
               }

               //获取midl节点下的所有元素
               IEnumerable<XElement> midls = root.Element("midl").Elements();
               foreach (XElement item in midls)
               {
                   handNames.Add(item.Name.ToString(), item.Value);
               }

               //获取pnky节点下的所有元素
               IEnumerable<XElement> pnkys = root.Element("pnky").Elements();
               foreach (XElement item in pnkys)
               {
                   handNames.Add(item.Name.ToString(), item.Value);
               }

               //获取pnky节点下的所有元素
               IEnumerable<XElement> rings = root.Element("ring").Elements();
               foreach (XElement item in rings)
               {
                   handNames.Add(item.Name.ToString(), item.Value);
               }

               //获取thmb节点下的所有元素
               IEnumerable<XElement> thmbs = root.Element("thmb").Elements();
               foreach (XElement item in thmbs)
               {
                   handNames.Add(item.Name.ToString(), item.Value);
               }

               
               //中指
               MIDLK31 = transform.FindChild("HAND-JNTD-FLAT/"+handNames["midl_k31"]);
               MIDLK21 = transform.FindChild("HAND-JNTD-FLAT/" + "MIDL K3 1/" + handNames["midl_k21"]);
               MIDLK11 = transform.FindChild("HAND-JNTD-FLAT/" + "MIDL K3 1/" + "MIDL K2 1/" + handNames["midl_k11"]);

               MIDLK31_Start = MIDLK31.localEulerAngles;
               MIDLK21_Start = MIDLK21.localEulerAngles;
               MIDLK11_Start = MIDLK11.localEulerAngles;

               //食指
               INDXK31 = transform.FindChild("HAND-JNTD-FLAT/" + handNames["indx_k31"]);
               INDXK21 = transform.FindChild("HAND-JNTD-FLAT/" + "INDX K3 1/" + handNames["indx_k21"]);
               INDXK11 = transform.FindChild("HAND-JNTD-FLAT/" + "INDX K3 1/" + "INDX K2 1/" + handNames["indx_k11"]);

               INDXK11_Start = INDXK11.localEulerAngles;
               INDXK21_Start = INDXK21.localEulerAngles;
               INDXK31_Start = INDXK31.localEulerAngles;



               //无名指
               RINGK31 = transform.FindChild("HAND-JNTD-FLAT/" + handNames["ring_k31"]);
               RINGK21 = transform.FindChild("HAND-JNTD-FLAT/" + "RING K3 1/" + handNames["ring_k21"]);
               RINGK11 = transform.FindChild("HAND-JNTD-FLAT/" + "RING K3 1/" + "RING K2 1/"+handNames["ring_k11"]);

               RINGK11_Start = INDXK11.localEulerAngles;
               RINGK21_Start = INDXK21.localEulerAngles;
               RINGK31_Start = INDXK31.localEulerAngles;


               //小指
               PNKY31 = transform.FindChild("HAND-JNTD-FLAT/" + handNames["pnky_k31"]);
               PNKY21 = transform.FindChild("HAND-JNTD-FLAT/" + "PNKY K3 1/" + handNames["pnky_k21"]);
               PNKY11 = transform.FindChild("HAND-JNTD-FLAT/" + "PNKY K3 1/" + "PNKY K2 1/" + handNames["pnky_k11"]);

               PNKY11_Start = INDXK11.localEulerAngles;
               PNKY21_Start = INDXK21.localEulerAngles;
               PNKY31_Start = INDXK31.localEulerAngles;

               //拇指
               THMB21 = transform.FindChild("HAND-JNTD-FLAT/" + handNames["thmb_k21"]);
               THMB11 = transform.FindChild("HAND-JNTD-FLAT/" + "THMB K2 1/" + handNames["thmb_k11"]);

               THMB11_Start = THMB11.localEulerAngles;
               THMB21_Start = THMB21.localEulerAngles;

           }


           #endregion

           HandModelIsHasedBend = false;
       }
    }


   

	void Update () {
       if(isLocalPlayer){
           if (agent < 0) return;
           
           //if(hasCatched!=null){
           //    CmdMove(transform.position,transform.eulerAngles);
           //}
           
           

           if (Input.GetKeyDown(KeyCode.J))
           {
               HandModelIsHasedBend = true;
               agent = 1;

           }
           else if (Input.GetKeyUp(KeyCode.J))
           {
               HandModelIsHasedBend = false;
               agent = 0;
               //抓到的仪器部件放开
               if (hasCatched != null)
               {
                   //如果是抓到的硬币的话，比较特殊，直接返回
                   if (hasCatched.name == "Coin")
                   {

                       if (hasCatched.transform.parent.name == "r6LeftTrigger")
                       {
                           hasCatched = null;
                           return;
                       }

                   }

                   //如果抓到的是镊子，则把砝码也放开
                   if (hasCatched.name == "tweezer")
                   {
                       //找出镊子的子物体是否存在砝码
                       Transform[] childs = hasCatched.GetComponentsInChildren<Transform>();
                       foreach (Transform weight in childs)
                       {
                           if (WeigthParentName.Contains(weight.name))
                           {
                               weight.localPosition = new Vector3(weight.localPosition.x + 0.02f, weight.localPosition.y + 0.02f, weight.localPosition.z);
                               weight.parent = null;
                               WeightParamer.IsHasClamped = false;
                           }

                       }
                   }
                   hasCatched.transform.parent = null;

                   //放开抓到的模型
                   hasCatched = null;

               }
           }
           MIDLKBlendAngle(agent);
           INDXKBlendAngle(agent);
           RINGKBlendAngle(agent);
           PNKYBlendAngle(agent);
           THMBBlendAngle(agent);
       }
    }



    #region 设置手模型五指弯曲
    /// <summary>
    /// 设置手模型中指弯曲
    /// </summary>
    /// <param name="agent">中指弯曲的角度</param>
    public void MIDLKBlendAngle(float agent)
    {
        if (MIDLK31 == null) return;
        //设置角度
        Transform[] obj = { MIDLK31, MIDLK21, MIDLK11 };
        Vector3[] vec = { MIDLK31_Start, MIDLK21_Start, MIDLK11_Start };
        BlendAngle(obj, vec, agent);

    }

    /// <summary>
    /// 设置手模型食指弯曲
    /// </summary>
    /// <param name="agent"></param>
    public void INDXKBlendAngle(float agent)
    {
        if (INDXK31 == null) return;
        //设置角度
        Transform[] obj = { INDXK31, INDXK21, INDXK11 };
        Vector3[] vec = { INDXK31_Start, INDXK21_Start, INDXK11_Start };
        BlendAngle(obj, vec, agent);

    }

    /// <summary>
    /// 设置手模型无名指弯曲
    /// </summary>
    /// <param name="agent">弯曲角度</param>
    public void RINGKBlendAngle(float agent)
    {
        if (RINGK31 == null) return;
        //设置角度
        Transform[] obj = { RINGK31, RINGK21, RINGK11 };
        Vector3[] vec = { RINGK31_Start, RINGK21_Start, RINGK11_Start };
        BlendAngle(obj, vec, agent);

    }

    /// <summary>
    /// 设置手模型小指弯曲
    /// </summary>
    /// <param name="agent">弯曲的角度</param>
    public void PNKYBlendAngle(float agent)
    {
        if (PNKY31 == null) return;
        //设置角度
        Transform[] obj = { PNKY31, PNKY21, PNKY11 };
        Vector3[] vec = { PNKY31_Start, PNKY21_Start, PNKY11_Start };
        BlendAngle(obj, vec, agent);

    }

    /// <summary>
    /// 设置手模型拇指弯曲程度
    /// </summary>
    /// <param name="agent">弯曲的角度</param>
    public void THMBBlendAngle(float agent)
    {
        if (THMB21 == null) return;
        //设置角度
        Transform[] obj = { THMB21, THMB11 };
        Vector3[] vec = { THMB21_Start, THMB11_Start };
        BlendAngle(obj, vec, agent);

    }

    /// <summary>
    /// 设置五指的弯曲通用方法
    /// </summary>
    /// <param name="obj">每根手指中的关节对象</param>
    /// <param name="vec">每根手指中关节的初始旋转角度</param>
    /// <param name="agent">要旋转的角度</param>
    private void BlendAngle(Transform[] obj, Vector3[] vec, float agent)
    {
        //设置除了拇指之外的四指的弯曲(因为拇指只有两节手指，而其余四指有三节手指)
        if (obj.Length == 3 && vec.Length == 3)
        {
            //设置角度
            obj[0].transform.localEulerAngles = new Vector3(vec[0].x + agent * 80, vec[0].y, vec[0].z);
            obj[1].transform.localEulerAngles = new Vector3(vec[1].x, vec[1].y - agent * 70, vec[1].z);
            obj[2].transform.localEulerAngles = new Vector3(vec[2].x, vec[2].y - agent * 10, vec[2].z);
        }
        //设置拇指的弯曲
        else if (obj.Length == 2 && vec.Length == 2)
        {
            obj[0].transform.localEulerAngles = new Vector3(vec[0].x, vec[0].y, vec[0].z + agent * 60f);
            obj[1].transform.localEulerAngles = new Vector3(vec[1].x, vec[1].y, vec[1].z + agent * 60f);
        }
    }
    #endregion


    public void OnCollisionEnter(Collision col)
    {
       //找到墙(Wall1--Wall4)、墙的父游戏物体Wall、桌面平面 就返回
        if (col.gameObject.name == "Wall1" || col.gameObject.name == "Wall2" ||
            col.gameObject.name == "Wall3" || col.gameObject.name == "Wall4" ||
            col.gameObject.name == "Wall" || col.gameObject.name == "Group 843"
            ) return;
        if(isClient){
            if (HandCanNotCatchObjectName.Contains(col.gameObject.name))
            {
                return;
            }
            if (HandModelIsHasedBend && hasCatched == null)
            {
                //把抓到的仪器部件设为手模型的子物体
                col.gameObject.transform.parent = gameObject.transform;
                hasCatched = col.gameObject;
                Debug.Log(col.gameObject.name);

                //调整抓到后仪器部件的位置和角度
                HandCatchComponentPositionReset.ResetHasedCathedComponentPosition(col.gameObject);

                
            }
           
        }
    }

    
    private static List<string> HandCanNotCatchObjectName = new List<string>() { 
          "Wall1",
          "Wall2",
          "Wall3",
          "Wall4",
          "Wall",               //不能抓墙
          "Group 843",          //不能抓桌面
          "Weight",             //不能直接抓砝码
          "WeightBox",
          "Model"
    };

    private static List<string> WeigthParentName = new List<string>() { 
        "WeightReferencePoint",
        "WeightReferencePoint1.2",
        "WeightReferencePoint0.7",
        "WeightReferencePoint0.5"
    };
}
