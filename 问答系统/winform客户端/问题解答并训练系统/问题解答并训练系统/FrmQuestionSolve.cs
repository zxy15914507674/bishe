using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 问题解答并训练系统
{
    public partial class FrmQuestionSolve : Form
    {
        TrainQuestionManager trainQuestionManager = new TrainQuestionManager();
        QAManager qaManager = new QAManager();
        QA qaObj;
        public FrmQuestionSolve(QA qaObj)
        {
            InitializeComponent();
            this.txtQuestion.Text=qaObj.question;
            this.txtAnswer.Text = qaObj.answer;
            this.qaObj = qaObj;
        }
        public FrmQuestionSolve()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 提交答案并进行训练按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.txtQuestion.Text.Trim().Length == 0)
            {
                return;
            }
            //安全校验
            if (this.txtAnswer.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入答案");
                return;
            }
            bool trainResult = false;
           
            try
            {
                //把问题和答案提交到python服务端进行训练
                trainResult = trainQuestionManager.TrainQuestion(this.txtQuestion.Text.Trim(), this.txtAnswer.Text.Trim());
            }
            catch (Exception ex)
            {

                MessageBox.Show("问题和答案训练失败并发生异常:" + ex.Message);
               
                return;
            }
            if (trainResult == true)
            {
                //训练成功,更新数据库,即更新为已解答，未忽略
                qaManager.UpdateAnswerFlagAndIgnoreFlag(1, 0,this.txtAnswer.Text.Trim(), qaObj.id.ToString());
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            //训练失败
            else {
                MessageBox.Show("问题和答案训练失败");
                this.Close();
            }
            

        }

        /// <summary>
        /// 关闭按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
