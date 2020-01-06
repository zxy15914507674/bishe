using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace fvc.exp.model 
{
    public class ChoiceQuestonScoreInfo
    {

        /// <summary>
        /// 错误的题号
        /// </summary>
        public int errorNumber { set; get; }


        /// <summary>
        /// 用户的答案
        /// </summary>
        public string userAnswer { set; get; }


        /// <summary>
        /// 正确的答案
        /// </summary>
        public string answer { set; get; }


        /// <summary>
        /// 该题的分值
        /// </summary>
        public float score { set; get; }
    }

}


