﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fvc.exp.model
{
    public class ChoiceQuestion
    {
        /****
         * 
         *      sceneName varchar(50) not null,
                questionTypeNumber tinyint not null,
                content text not null,
                optionA text,
                optionB text,
                optionC text,
                optionD text,
                picture text,
                answer varchar(10) not null,
                score tinyint not null
         * 
         * 
         * 
         */


        /// <summary>
        /// id号
        /// </summary>
        public int id { set; get; }

        /// <summary>
        /// 所属实验室名称
        /// </summary>
        public string labRoom { set; get; }

        /// <summary>
        /// 实验名称
        /// </summary>
        public string expName { set; get; }

        /// <summary>
        /// 实验场景名称
        /// </summary>
        public string sceneName { set; get; }

        /// <summary>
        /// 问题类型   (1:代表选择题  2：代表填空题  3：代表简答题)
        /// </summary>
        public string questionTypeNumber { set; get; }


        /// <summary>
        /// 问题内容
        /// </summary>
        public string content { set; get; }

        /// <summary>
        /// 选项A
        /// </summary>
        public string optionA { set; get; }

       
        /// <summary>
        /// 选项B
        /// </summary>
        public string optionB { set; get; }
        /// <summary>
        /// 选项C
        /// </summary>
        public string optionC { set; get; }
        /// <summary>
        /// 选项D
        /// </summary>
        public string optionD { set; get; }

        /// <summary>
        /// 图片(需要序列化)
        /// </summary>
        public string picture { set; get; }

        /// <summary>
        /// 习题答案
        /// </summary>
        public string answer { set; get; }


        /// <summary>
        /// 习题分值
        /// </summary>
        public string score { set; get; }


        /// <summary>
        /// 添加人
        /// </summary>
        public string teacherName { set; get; }


        /// <summary>
        /// 用户的答案
        /// </summary>
        public string  UserAnswer { set; get; }


        /// <summary>
        /// 题号
        /// </summary>
        public int QuestionNumber { set; get; }


        /// <summary>
        /// 提示信息
        /// </summary>
        public string tipMessage { set; get; }

        /// <summary>
        /// 作答时长
        /// </summary>
        public int thinkTime { set; get; }

    }
}
