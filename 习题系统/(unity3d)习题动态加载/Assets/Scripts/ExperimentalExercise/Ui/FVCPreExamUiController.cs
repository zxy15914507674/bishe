using FVCPosExamUiController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace fvc.exp.ui
{
    public class FVCPreExamUiController : FVCUiGameStateBase
    {
        private Button _CloseButton;

        protected Dictionary<string, float> _Scores;
        // Use this for initialization
        void Start()
        {
           
            //_CloseButton.onClick.AddListener(_OnCloseClick);
            //计算原理学习的成绩，并显示到TopUi上
            _Scores = new Dictionary<string, float>();
        }

        private void _OnCloseClick()
        {
            //TODO  计算分数
            


            //string name = FVCExp.GameScene.GameInfo.ExpName;

            //FVCExp.Tutor.SetScorePreExam(_Scores);

            State = FVCGameState.GameOver;
            //FVCExp.Events.Fire(FVCExp.EventNames.GameStateChanged, State);

            gameObject.SetActive(false);
        }


        public override void _OnGameStateChanged(FVCGameState state)
        {
            if (state == FVCGameState.PostExam)
                gameObject.SetActive(true);
            GameObject.Find("QuestionParent").GetComponent<FVCStateSystem>().StartExperimentalExercise("Scene1");                   //测试阶段使用
            //GameObject.Find("QuestionParent").GetComponent<FVCStateSystem>().StartExperimentalExercise(gameObject.scene.name);      //真正使用
        }

    }

}

