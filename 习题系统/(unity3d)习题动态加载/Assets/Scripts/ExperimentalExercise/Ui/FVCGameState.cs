public enum FVCGameState
{
    PreGame = -1,
    PreExam,             //实验原理学习及其测试
    EquipsChoice,        //选择仪器和器材
    EquipsPreparation,   //调节仪器
    GameInProgress,      //进行实验，获取数据提交数据
    PostExam,            //实验后的试题测试
    GameOver             //给出评分，实验结束
}