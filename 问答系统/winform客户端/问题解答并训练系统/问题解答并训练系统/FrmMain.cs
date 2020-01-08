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
    public partial class FrmMain : Form
    {
        QAManager qaManager = new QAManager();

        int CurrentSelectedIndex = 0;                 //当前选择按钮的下标，方便操作后刷新,0:未解答按钮  1：已解答按钮   2：已忽略按钮

        public FrmMain()
        {
            InitializeComponent();
            //加载未解答和未忽略的问题
            this.dgvQuestionInfo.AutoGenerateColumns = false;
            List<QA> qaList = null;
            try
            {
                qaList=qaManager.GetQAByAnswerStatusAndIgnoreStatus(0,0);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            if (qaList != null && qaList.Count > 0)
            { 
                //把问题显示到界面上
                this.dgvQuestionInfo.DataSource = qaList;
            }
        }

        /// <summary>
        /// 未解答按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            CurrentSelectedIndex = 0;
            //清空上一次的信息
            this.dgvQuestionInfo.DataSource = null;
            List<QA> qaList = null;
            try
            {
                qaList = qaManager.GetQAByAnswerStatusAndIgnoreStatus(0, 0);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            if (qaList != null && qaList.Count > 0)
            {
                //把问题显示到界面上
                this.dgvQuestionInfo.DataSource = qaList;
            }
        }

        /// <summary>
        /// 已解答按钮点击事件(加载已解答未忽略的问题)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            CurrentSelectedIndex = 1;
            //清空上一次的信息
            this.dgvQuestionInfo.DataSource = null;
            List<QA> qaList = null;
            try
            {
                qaList = qaManager.GetQAByAnswerStatusAndIgnoreStatus(1, 0);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            if (qaList != null && qaList.Count > 0)
            {
                //把问题显示到界面上
                this.dgvQuestionInfo.DataSource = qaList;
            }
        }

        /// <summary>
        /// 已忽略按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
             CurrentSelectedIndex=2;
            //清空上一次的信息
            this.dgvQuestionInfo.DataSource = null;
            List<QA> qaList = null;
            try
            {
                qaList = qaManager.GetQAByIgnoreStatus(1);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            if (qaList != null && qaList.Count > 0)
            {
                //把问题显示到界面上
                this.dgvQuestionInfo.DataSource = qaList;
            }
        }

        /// <summary>
        /// 忽略按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIgnore_Click(object sender, EventArgs e)
        {
            //安全校验
            if (dgvQuestionInfo.DataSource == null)
            {
                return;
            }
            //获取选中的行

            string id=dgvQuestionInfo.CurrentRow.Cells[0].Value.ToString();
            
            //操作数据库进行忽略操作
            try
            {
                 qaManager.IgnoreQuestion(id);
                 if (CurrentSelectedIndex == 0)
                 {
                     //刷新操作，调用为解答按钮点击的事件进行刷新
                     button1_Click(null, null);
                 }
                 else if (CurrentSelectedIndex == 1)
                 { 
                    //调用已解答按钮点击事件进行刷新操作
                     button2_Click(null, null);
                 }
                 
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        /// 解答按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnswer_Click(object sender, EventArgs e)
        {
            //安全校验
            if (dgvQuestionInfo.DataSource == null)
            {
                return;
            }
            //获取选中的行

            string id = dgvQuestionInfo.CurrentRow.Cells[0].Value.ToString();
            QA qaObj = null;
           
            try
            {
                qaObj = qaManager.GetQAById(id);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }

            if (qaObj != null)
            { 
                //打开解答窗体
                FrmQuestionSolve frmQuesiontSolve = new FrmQuestionSolve(qaObj);
                DialogResult dialogResult=frmQuesiontSolve.ShowDialog();
                //如果成功，则进行刷新操作
                if (dialogResult == DialogResult.OK)
                {
                    if (CurrentSelectedIndex == 0)
                    {
                        //刷新操作，调用为解答按钮点击的事件进行刷新
                        button1_Click(null, null);
                    }
                    else if (CurrentSelectedIndex == 1)
                    {
                        //调用已解答按钮点击事件进行刷新操作
                        button2_Click(null, null);
                    }
                    else if (CurrentSelectedIndex == 2)
                    { 
                        //调用已忽略按钮点击事件进行刷新操作
                        button4_Click(null, null);
                    }
                }
            }
        }

        /// <summary>
        /// 详细信息按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInfo_Click(object sender, EventArgs e)
        {
            //安全校验
            if (dgvQuestionInfo.DataSource == null)
            {
                return;
            }
            //获取选中的行

            string id = dgvQuestionInfo.CurrentRow.Cells[0].Value.ToString();
            QA qaObj = null;

            try
            {
                qaObj = qaManager.GetQAById(id);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }

            if (qaObj != null)
            {
                //打开解答窗体
                FrmQuestionInfo frmQuesiontInfo = new FrmQuestionInfo(qaObj);
                frmQuesiontInfo.ShowDialog();
                
            }
        }

        /// <summary>
        /// 添加问题按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //打开解答窗体
            FrmQuestionAdd frmQuesiontAdd = new FrmQuestionAdd();
            DialogResult dialogResult = frmQuesiontAdd.ShowDialog();
            //如果成功，则进行刷新操作
            if (dialogResult == DialogResult.OK)
            {
                //调用已解答按钮点击事件进行刷新操作
                button2_Click(null, null);
            }
        }

    }
}
