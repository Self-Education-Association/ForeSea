namespace CheckInProgram
{
    partial class Normal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.nameLabel = new System.Windows.Forms.Label();
            this.IDLabel = new System.Windows.Forms.Label();
            this.stateLabel = new System.Windows.Forms.Label();
            this.queryButton = new System.Windows.Forms.Button();
            this.signOutButton = new System.Windows.Forms.Button();
            this.changeButton = new System.Windows.Forms.Button();
            this.keepTimer = new System.Windows.Forms.Timer(this.components);
            this.checkTimer = new System.Windows.Forms.Timer(this.components);
            this.roomLabel = new System.Windows.Forms.Label();
            this.moreInfoLinkLabel = new System.Windows.Forms.LinkLabel();
            this.poweredLabel = new System.Windows.Forms.Label();
            this.checkIfThereButton = new System.Windows.Forms.Button();
            this.coldDownTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("幼圆", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nameLabel.Location = new System.Drawing.Point(12, 12);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(5);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(68, 25);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Name";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IDLabel
            // 
            this.IDLabel.AutoSize = true;
            this.IDLabel.Font = new System.Drawing.Font("幼圆", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IDLabel.Location = new System.Drawing.Point(12, 42);
            this.IDLabel.Margin = new System.Windows.Forms.Padding(5);
            this.IDLabel.Name = "IDLabel";
            this.IDLabel.Size = new System.Drawing.Size(40, 25);
            this.IDLabel.TabIndex = 1;
            this.IDLabel.Text = "ID";
            this.IDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // stateLabel
            // 
            this.stateLabel.AutoSize = true;
            this.stateLabel.Font = new System.Drawing.Font("幼圆", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stateLabel.Location = new System.Drawing.Point(12, 71);
            this.stateLabel.Margin = new System.Windows.Forms.Padding(5);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(82, 25);
            this.stateLabel.TabIndex = 2;
            this.stateLabel.Text = "State";
            this.stateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // queryButton
            // 
            this.queryButton.Location = new System.Drawing.Point(129, 162);
            this.queryButton.Margin = new System.Windows.Forms.Padding(2);
            this.queryButton.Name = "queryButton";
            this.queryButton.Size = new System.Drawing.Size(115, 28);
            this.queryButton.TabIndex = 3;
            this.queryButton.Text = "查询记录";
            this.queryButton.UseVisualStyleBackColor = true;
            this.queryButton.Click += new System.EventHandler(this.queryButton_Click);
            // 
            // signOutButton
            // 
            this.signOutButton.Location = new System.Drawing.Point(10, 128);
            this.signOutButton.Margin = new System.Windows.Forms.Padding(2);
            this.signOutButton.Name = "signOutButton";
            this.signOutButton.Size = new System.Drawing.Size(115, 62);
            this.signOutButton.TabIndex = 4;
            this.signOutButton.Text = "注销";
            this.signOutButton.UseVisualStyleBackColor = true;
            this.signOutButton.Click += new System.EventHandler(this.signOutButton_Click);
            // 
            // changeButton
            // 
            this.changeButton.Location = new System.Drawing.Point(129, 128);
            this.changeButton.Margin = new System.Windows.Forms.Padding(2);
            this.changeButton.Name = "changeButton";
            this.changeButton.Size = new System.Drawing.Size(115, 29);
            this.changeButton.TabIndex = 5;
            this.changeButton.Text = "更换机器";
            this.changeButton.UseVisualStyleBackColor = true;
            this.changeButton.Click += new System.EventHandler(this.changeButton_Click);
            // 
            // keepTimer
            // 
            this.keepTimer.Enabled = true;
            this.keepTimer.Interval = 600000;
            this.keepTimer.Tick += new System.EventHandler(this.keepTimer_Tick);
            // 
            // checkTimer
            // 
            this.checkTimer.Interval = 300000;
            this.checkTimer.Tick += new System.EventHandler(this.checkTimer_Tick);
            // 
            // roomLabel
            // 
            this.roomLabel.AutoSize = true;
            this.roomLabel.Location = new System.Drawing.Point(12, 101);
            this.roomLabel.Margin = new System.Windows.Forms.Padding(5);
            this.roomLabel.Name = "roomLabel";
            this.roomLabel.Size = new System.Drawing.Size(53, 20);
            this.roomLabel.TabIndex = 6;
            this.roomLabel.Text = "Room";
            // 
            // moreInfoLinkLabel
            // 
            this.moreInfoLinkLabel.AutoSize = true;
            this.moreInfoLinkLabel.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.moreInfoLinkLabel.Location = new System.Drawing.Point(8, 208);
            this.moreInfoLinkLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.moreInfoLinkLabel.Name = "moreInfoLinkLabel";
            this.moreInfoLinkLabel.Size = new System.Drawing.Size(71, 20);
            this.moreInfoLinkLabel.TabIndex = 8;
            this.moreInfoLinkLabel.TabStop = true;
            this.moreInfoLinkLabel.Text = "moreInfo";
            this.moreInfoLinkLabel.VisitedLinkColor = System.Drawing.Color.Blue;
            this.moreInfoLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreInfoLinkLabel_LinkClicked);
            // 
            // poweredLabel
            // 
            this.poweredLabel.AutoSize = true;
            this.poweredLabel.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.poweredLabel.Location = new System.Drawing.Point(10, 192);
            this.poweredLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.poweredLabel.Name = "poweredLabel";
            this.poweredLabel.Size = new System.Drawing.Size(68, 20);
            this.poweredLabel.TabIndex = 7;
            this.poweredLabel.Text = "Powered";
            // 
            // checkIfThereButton
            // 
            this.checkIfThereButton.Location = new System.Drawing.Point(11, 129);
            this.checkIfThereButton.Name = "checkIfThereButton";
            this.checkIfThereButton.Size = new System.Drawing.Size(232, 60);
            this.checkIfThereButton.TabIndex = 9;
            this.checkIfThereButton.Text = "请点击本按钮确认在线";
            this.checkIfThereButton.UseVisualStyleBackColor = true;
            this.checkIfThereButton.Visible = false;
            this.checkIfThereButton.Click += new System.EventHandler(this.checkIfThereButton_Click);
            // 
            // coldDownTimer
            // 
            this.coldDownTimer.Interval = 120000;
            this.coldDownTimer.Tick += new System.EventHandler(this.coldDownTimer_Tick);
            // 
            // Normal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(254, 233);
            this.Controls.Add(this.checkIfThereButton);
            this.Controls.Add(this.moreInfoLinkLabel);
            this.Controls.Add(this.poweredLabel);
            this.Controls.Add(this.roomLabel);
            this.Controls.Add(this.changeButton);
            this.Controls.Add(this.signOutButton);
            this.Controls.Add(this.queryButton);
            this.Controls.Add(this.stateLabel);
            this.Controls.Add(this.IDLabel);
            this.Controls.Add(this.nameLabel);
            this.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Normal";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Normal";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.White;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Normal_FormClosed);
            this.Load += new System.EventHandler(this.Normal_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Normal_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label IDLabel;
        private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.Button queryButton;
        private System.Windows.Forms.Button signOutButton;
        private System.Windows.Forms.Button changeButton;
        private System.Windows.Forms.Timer keepTimer;
        private System.Windows.Forms.Timer checkTimer;
        private System.Windows.Forms.Label roomLabel;
        private System.Windows.Forms.LinkLabel moreInfoLinkLabel;
        private System.Windows.Forms.Label poweredLabel;
        private System.Windows.Forms.Button checkIfThereButton;
        private System.Windows.Forms.Timer coldDownTimer;
    }
}