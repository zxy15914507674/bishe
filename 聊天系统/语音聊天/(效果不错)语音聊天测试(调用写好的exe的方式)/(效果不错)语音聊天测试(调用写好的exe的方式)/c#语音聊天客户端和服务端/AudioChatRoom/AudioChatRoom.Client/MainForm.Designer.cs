﻿namespace AudioChatRoom
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel_userID = new CCWin.SkinControl.SkinLabel();
            this.multiAudioChatContainer1 = new AudioChatRoom.MultiAudioChatContainer();
            this.SuspendLayout();
            // 
            // skinLabel1
            // 
            this.skinLabel1.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(16, 22);
            this.skinLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(84, 20);
            this.skinLabel1.TabIndex = 1;
            this.skinLabel1.Text = "当前登录：";
            // 
            // skinLabel_userID
            // 
            this.skinLabel_userID.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel_userID.AutoSize = true;
            this.skinLabel_userID.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel_userID.BorderColor = System.Drawing.Color.White;
            this.skinLabel_userID.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel_userID.Location = new System.Drawing.Point(104, 22);
            this.skinLabel_userID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.skinLabel_userID.Name = "skinLabel_userID";
            this.skinLabel_userID.Size = new System.Drawing.Size(43, 20);
            this.skinLabel_userID.TabIndex = 1;
            this.skinLabel_userID.Text = "aa01";
            // 
            // multiAudioChatContainer1
            // 
            this.multiAudioChatContainer1.BackColor = System.Drawing.Color.Transparent;
            this.multiAudioChatContainer1.Location = new System.Drawing.Point(3, 62);
            this.multiAudioChatContainer1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.multiAudioChatContainer1.Name = "multiAudioChatContainer1";
            this.multiAudioChatContainer1.Size = new System.Drawing.Size(389, 594);
            this.multiAudioChatContainer1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 654);
            this.Controls.Add(this.skinLabel_userID);
            this.Controls.Add(this.skinLabel1);
            this.Controls.Add(this.multiAudioChatContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "语音聊天室";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MultiAudioChatContainer multiAudioChatContainer1;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinLabel skinLabel_userID;
    }
}

