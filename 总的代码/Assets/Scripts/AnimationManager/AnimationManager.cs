using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    //动画名称集合
    public List<string> listAnimationName;
    //动画名称对应的动画
    public List<Animation> listAnimation;
    //动画名称和动画建立一一对应的关系，方便操作动画播放
    private Dictionary<string, Animation> dicAnimation = new Dictionary<string, Animation>();

    //本类的静态实例
    public static AnimationManager Instance;

    //上一次播放的动画名称
    private string lastAnimationName;

    //存储已经播放过了的动画的名称
    List<string> AnimationNameHasPlay = new List<string>();

	void Start () {
		if(listAnimationName!=null&&listAnimation!=null&&listAnimationName.Count==listAnimation.Count){
            for (int index = 0; index < listAnimation.Count;index++ )
            {
                dicAnimation[listAnimationName[index]] = listAnimation[index];
            }

            //默认播放第一个动画
            dicAnimation[listAnimationName[0]].Play(listAnimationName[0]);
            //默认重复播放第一个动画
            dicAnimation[listAnimationName[0]].wrapMode = WrapMode.Loop;
            lastAnimationName = listAnimationName[0];
        }
        Instance = this;
        
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
            
        //    //listAnimation[0].Play("r2");
        //    ////listAnimation[0].wrapMode = WrapMode.Loop;
        //    //listAnimation[1].Play("r3");
        //    //listAnimation[2].Play("r4_left");
        //    //listAnimation[3].Play("r4_right");
        //    //listAnimation[4].Play("r5_left");
        //    //listAnimation[5].Play("r5_right");
        //    //listAnimation[6].Play("r6_left");
        //    //listAnimation[7].Play("r6_right");
        //    //listAnimation[8].Play("Weight");
        //    //listAnimation[9].Play("Coin");

        //    foreach (string key in dicAnimation.Keys)
        //    {
        //            dicAnimation[key].Play(key);
        //    }

        //}
	}


    /// <summary>
    /// 播放下一次动画
    /// </summary>
    /// <param name="NextAnimationName">要播放的下一次动画的名称</param>
    public void PlayNextAnimation(string NextAnimationName) { 
        //首先判断所有步骤都已经完成
        if(InterferenceMatrixManager.Instance.IsHasAllAssemble()){
            //设置上一个动画播放完就停止到目标的位置
            if(dicAnimation.ContainsKey(lastAnimationName)){
                dicAnimation[lastAnimationName].wrapMode = WrapMode.Default;
                dicAnimation[lastAnimationName].Play(lastAnimationName);
            }
            return;
        }

        if(NextAnimationName==null||NextAnimationName.Length==0||!dicAnimation.ContainsKey(NextAnimationName)){
            return;
        }
       

        //上一次的动画停止重复播放
        if(lastAnimationName==null||lastAnimationName.Length==0||!dicAnimation.ContainsKey(lastAnimationName)){
            return;
        }

            
        if (InterferenceMatrixManager.Instance.GetHasAssemblyComponentName().Contains(lastAnimationName))
        {
            //播完就算了
            dicAnimation[lastAnimationName].wrapMode = WrapMode.Default;
            AnimationNameHasPlay.Add(lastAnimationName);
        }
        //操作了非动画推荐的步骤，则需要把推荐的步骤的动画还原为原来的位置
        else { 
            //对应的动画则需要恢复到原来的位置
            dicAnimation[lastAnimationName].Stop();
            dicAnimation[lastAnimationName][lastAnimationName].time = 0f;
        }

        //判断是否操作了非推荐的步骤,如果是，则相应的动画教程步骤要执行
        foreach (string hasAssemblyName in InterferenceMatrixManager.Instance.GetHasAssemblyComponentName())
        {
            if (!AnimationNameHasPlay.Contains(hasAssemblyName))
            {
                dicAnimation[hasAssemblyName].wrapMode = WrapMode.Default;
                dicAnimation[hasAssemblyName].Play(hasAssemblyName);
            }
        }


        
        //这一次的动画重复播放
        dicAnimation[NextAnimationName].wrapMode = WrapMode.Loop;
        dicAnimation[NextAnimationName].Play(NextAnimationName);
        lastAnimationName = NextAnimationName;
        

       
    }
}
