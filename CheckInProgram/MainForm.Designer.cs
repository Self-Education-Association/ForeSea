namespace CheckInProgram
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.signInButton = new System.Windows.Forms.Button();
            this.enterIDBox = new System.Windows.Forms.TextBox();
            this.poweredLabel = new System.Windows.Forms.Label();
            this.moreInfoLinkLabel = new System.Windows.Forms.LinkLabel();
            this.closeTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // signInButton
            // 
            this.signInButton.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.signInButton.Location = new System.Drawing.Point(15, 66);
            this.signInButton.Margin = new System.Windows.Forms.Padding(6);
            this.signInButton.Name = "signInButton";
            this.signInButton.Size = new System.Drawing.Size(295, 49);
            this.signInButton.TabIndex = 0;
            this.signInButton.Text = "签到";
            this.signInButton.UseVisualStyleBackColor = true;
            this.signInButton.Click += new System.EventHandler(this.signInButton_Click);
            // 
            // enterIDBox
            // 
            this.enterIDBox.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.enterIDBox.Location = new System.Drawing.Point(15, 15);
            this.enterIDBox.Margin = new System.Windows.Forms.Padding(6);
            this.enterIDBox.MaxLength = 9;
            this.enterIDBox.Name = "enterIDBox";
            this.enterIDBox.Size = new System.Drawing.Size(295, 47);
            this.enterIDBox.TabIndex = 1;
            this.enterIDBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // poweredLabel
            // 
            this.poweredLabel.AutoSize = true;
            this.poweredLabel.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.poweredLabel.Location = new System.Drawing.Point(12, 121);
            this.poweredLabel.Name = "poweredLabel";
            this.poweredLabel.Size = new System.Drawing.Size(85, 24);
            this.poweredLabel.TabIndex = 2;
            this.poweredLabel.Text = "Powered";
            // 
            // moreInfoLinkLabel
            // 
            this.moreInfoLinkLabel.AutoSize = true;
            this.moreInfoLinkLabel.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.moreInfoLinkLabel.Location = new System.Drawing.Point(12, 141);
            this.moreInfoLinkLabel.Name = "moreInfoLinkLabel";
            this.moreInfoLinkLabel.Size = new System.Drawing.Size(88, 24);
            this.moreInfoLinkLabel.TabIndex = 3;
            this.moreInfoLinkLabel.TabStop = true;
            this.moreInfoLinkLabel.Text = "moreInfo";
            this.moreInfoLinkLabel.VisitedLinkColor = System.Drawing.Color.Blue;
            this.moreInfoLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreInfoLinkLabel_LinkClicked);
            // 
            // closeTimer
            // 
            this.closeTimer.Enabled = true;
            this.closeTimer.Interval = 6000000;
            this.closeTimer.Tick += new System.EventHandler(this.closeTimer_Tick);
            // 
            // MainForm
            // 
            this.AcceptButton = this.signInButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 172);
            this.Controls.Add(this.moreInfoLinkLabel);
            this.Controls.Add(this.poweredLabel);
            this.Controls.Add(this.enterIDBox);
            this.Controls.Add(this.signInButton);
            this.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "签到程序";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button signInButton;
        private System.Windows.Forms.TextBox enterIDBox;
        private System.Windows.Forms.Label poweredLabel;
        private System.Windows.Forms.LinkLabel moreInfoLinkLabel;
        private System.Windows.Forms.Timer closeTimer;
    }
}

