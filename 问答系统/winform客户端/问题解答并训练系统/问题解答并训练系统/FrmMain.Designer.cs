namespace 问题解答并训练系统
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvQuestionInfo = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.question = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.answer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ignoreDisplay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hasAnswerDisplay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnIgnore = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btnInfo = new System.Windows.Forms.Button();
            this.btnAnswer = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuestionInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvQuestionInfo
            // 
            this.dgvQuestionInfo.AllowUserToResizeColumns = false;
            this.dgvQuestionInfo.AllowUserToResizeRows = false;
            this.dgvQuestionInfo.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvQuestionInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQuestionInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.question,
            this.answer,
            this.ignoreDisplay,
            this.hasAnswerDisplay});
            this.dgvQuestionInfo.Location = new System.Drawing.Point(26, 26);
            this.dgvQuestionInfo.Name = "dgvQuestionInfo";
            this.dgvQuestionInfo.RowTemplate.Height = 23;
            this.dgvQuestionInfo.Size = new System.Drawing.Size(785, 375);
            this.dgvQuestionInfo.TabIndex = 0;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "序号";
            this.id.Name = "id";
            this.id.Width = 70;
            // 
            // question
            // 
            this.question.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.question.DataPropertyName = "question";
            this.question.HeaderText = "问题";
            this.question.Name = "question";
            // 
            // answer
            // 
            this.answer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.answer.DataPropertyName = "answer";
            this.answer.HeaderText = "问题答案";
            this.answer.Name = "answer";
            // 
            // ignoreDisplay
            // 
            this.ignoreDisplay.DataPropertyName = "ignoreDisplay";
            this.ignoreDisplay.HeaderText = "忽略状态";
            this.ignoreDisplay.Name = "ignoreDisplay";
            // 
            // hasAnswerDisplay
            // 
            this.hasAnswerDisplay.DataPropertyName = "hasAnswerDisplay";
            this.hasAnswerDisplay.HeaderText = "解答状态";
            this.hasAnswerDisplay.Name = "hasAnswerDisplay";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(817, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "未解答";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(819, 64);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(65, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "已解答";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnIgnore
            // 
            this.btnIgnore.Location = new System.Drawing.Point(817, 288);
            this.btnIgnore.Name = "btnIgnore";
            this.btnIgnore.Size = new System.Drawing.Size(65, 23);
            this.btnIgnore.TabIndex = 3;
            this.btnIgnore.Text = "忽略";
            this.btnIgnore.UseVisualStyleBackColor = true;
            this.btnIgnore.Click += new System.EventHandler(this.btnIgnore_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(819, 106);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(65, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "已忽略";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.Location = new System.Drawing.Point(819, 327);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(65, 23);
            this.btnInfo.TabIndex = 5;
            this.btnInfo.Text = "详细信息";
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // btnAnswer
            // 
            this.btnAnswer.Location = new System.Drawing.Point(817, 246);
            this.btnAnswer.Name = "btnAnswer";
            this.btnAnswer.Size = new System.Drawing.Size(65, 23);
            this.btnAnswer.TabIndex = 6;
            this.btnAnswer.Text = "解答";
            this.btnAnswer.UseVisualStyleBackColor = true;
            this.btnAnswer.Click += new System.EventHandler(this.btnAnswer_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(819, 368);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(65, 23);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "添加问题";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 429);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnAnswer);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnIgnore);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvQuestionInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "问题训练系统";
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuestionInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvQuestionInfo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnIgnore;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.Button btnAnswer;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn question;
        private System.Windows.Forms.DataGridViewTextBoxColumn answer;
        private System.Windows.Forms.DataGridViewTextBoxColumn ignoreDisplay;
        private System.Windows.Forms.DataGridViewTextBoxColumn hasAnswerDisplay;
        private System.Windows.Forms.Button btnAdd;
    }
}

