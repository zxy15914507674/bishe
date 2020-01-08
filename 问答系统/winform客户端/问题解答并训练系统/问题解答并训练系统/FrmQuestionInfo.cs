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
    public partial class FrmQuestionInfo : Form
    {

        public FrmQuestionInfo(QA qaObj)
        {
            InitializeComponent();
            this.txtQuestion.Text=qaObj.question;
            this.txtAnswer.Text = qaObj.answer;
           
        }
        public FrmQuestionInfo()
        {
            InitializeComponent();
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
