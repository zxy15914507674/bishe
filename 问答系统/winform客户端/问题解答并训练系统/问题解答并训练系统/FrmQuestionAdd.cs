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
    public partial class FrmQuestionAdd : Form
    {

        TrainQuestionManager trainQuestionManager = new TrainQuestionManager();
        QAManager qaManager = new QAManager();
       
        public FrmQuestionAdd()
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
                MessageBox.Show("请输入问题");
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
                QA qaObj = new QA() {  question=this.txtQuestion.Text.Trim(),answer=this.txtAnswer.Text.Trim()};
                //训练成功,添加进数据库,即添加为已解答，未忽略
                qaManager.AddQuestion(qaObj);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            //训练失败
            else
            {
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
