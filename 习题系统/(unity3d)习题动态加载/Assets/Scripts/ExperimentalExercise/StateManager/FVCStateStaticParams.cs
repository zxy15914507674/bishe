using System.Collections;
using System.Collections.Generic;

using fvc.exp.model;
namespace fvc.exp.state
{
    public class FVCStateStaticParams
    {
        public static FVCQuestionType currentQuestionType;             //当前问题类型

        public static bool IsStartExercise;                        //是否进入习题阶段


        public static List<ChoiceQuestion> ChoiceQuestionList;             //存储从数据库查询回来的选择题的题目信息的列表
        public static List<CompletionQuestion> CompletionQuestionList;     //存储从数据库查询回来的填空题的题目信息的列表


        public static List<ChoiceQuestonScoreInfo> ChoiceQuestionScoreInfoList=new List<ChoiceQuestonScoreInfo>();                     //保存选择题错误信息列表
        public static List<CompletionQuestionScoreInfo> CompletionScoreInfoList = new List<CompletionQuestionScoreInfo>();              //保存填空题错误信息列表
    }
}

