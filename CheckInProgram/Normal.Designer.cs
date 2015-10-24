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
            this.NameLabel = new System.Windows.Forms.Label();
            this.IDLabel = new System.Windows.Forms.Label();
            this.StateLabel = new System.Windows.Forms.Label();
            this.QueryButton = new System.Windows.Forms.Button();
            this.SignOutButton = new System.Windows.Forms.Button();
            this.ChangeButton = new System.Windows.Forms.Button();
            this.KeepTimer = new System.Windows.Forms.Timer(this.components);
            this.CheckTimer = new System.Windows.Forms.Timer(this.components);
            this.RoomLabel = new System.Windows.Forms.Label();
            this.MoreInfoLinkLabel = new System.Windows.Forms.LinkLabel();
            this.PoweredLabel = new System.Windows.Forms.Label();
            this.CheckIfThereButton = new System.Windows.Forms.Button();
            this.ColdDownTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Font = new System.Drawing.Font("幼圆", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NameLabel.Location = new System.Drawing.Point(12, 12);
            this.NameLabel.Margin = new System.Windows.Forms.Padding(5);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(53, 20);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "Name";
            this.NameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IDLabel
            // 
            this.IDLabel.AutoSize = true;
            this.IDLabel.Font = new System.Drawing.Font("幼圆", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IDLabel.Location = new System.Drawing.Point(12, 42);
            this.IDLabel.Margin = new System.Windows.Forms.Padding(5);
            this.IDLabel.Name = "IDLabel";
            this.IDLabel.Size = new System.Drawing.Size(31, 20);
            this.IDLabel.TabIndex = 1;
            this.IDLabel.Text = "ID";
            this.IDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StateLabel
            // 
            this.StateLabel.AutoSize = true;
            this.StateLabel.Font = new System.Drawing.Font("幼圆", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StateLabel.Location = new System.Drawing.Point(12, 71);
            this.StateLabel.Margin = new System.Windows.Forms.Padding(5);
            this.StateLabel.Name = "StateLabel";
            this.StateLabel.Size = new System.Drawing.Size(64, 20);
            this.StateLabel.TabIndex = 2;
            this.StateLabel.Text = "State";
            this.StateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // QueryButton
            // 
            this.QueryButton.Location = new System.Drawing.Point(129, 162);
            this.QueryButton.Margin = new System.Windows.Forms.Padding(2);
            this.QueryButton.Name = "QueryButton";
            this.QueryButton.Size = new System.Drawing.Size(115, 28);
            this.QueryButton.TabIndex = 3;
            this.QueryButton.Text = "查询记录";
            this.QueryButton.UseVisualStyleBackColor = true;
            this.QueryButton.Click += new System.EventHandler(this.queryButton_Click);
            // 
            // SignOutButton
            // 
            this.SignOutButton.Location = new System.Drawing.Point(10, 128);
            this.SignOutButton.Margin = new System.Windows.Forms.Padding(2);
            this.SignOutButton.Name = "SignOutButton";
            this.SignOutButton.Size = new System.Drawing.Size(115, 62);
            this.SignOutButton.TabIndex = 4;
            this.SignOutButton.Text = "注销";
            this.SignOutButton.UseVisualStyleBackColor = true;
            this.SignOutButton.Click += new System.EventHandler(this.signOutButton_Click);
            // 
            // ChangeButton
            // 
            this.ChangeButton.Location = new System.Drawing.Point(129, 128);
            this.ChangeButton.Margin = new System.Windows.Forms.Padding(2);
            this.ChangeButton.Name = "ChangeButton";
            this.ChangeButton.Size = new System.Drawing.Size(115, 29);
            this.ChangeButton.TabIndex = 5;
            this.ChangeButton.Text = "更换机器";
            this.ChangeButton.UseVisualStyleBackColor = true;
            this.ChangeButton.Click += new System.EventHandler(this.changeButton_Click);
            // 
            // KeepTimer
            // 
            this.KeepTimer.Enabled = true;
            this.KeepTimer.Interval = 600000;
            this.KeepTimer.Tick += new System.EventHandler(this.keepTimer_Tick);
            // 
            // CheckTimer
            // 
            this.CheckTimer.Interval = 300000;
            this.CheckTimer.Tick += new System.EventHandler(this.checkTimer_Tick);
            // 
            // RoomLabel
            // 
            this.RoomLabel.AutoSize = true;
            this.RoomLabel.Location = new System.Drawing.Point(12, 101);
            this.RoomLabel.Margin = new System.Windows.Forms.Padding(5);
            this.RoomLabel.Name = "RoomLabel";
            this.RoomLabel.Size = new System.Drawing.Size(44, 16);
            this.RoomLabel.TabIndex = 6;
            this.RoomLabel.Text = "Room";
            // 
            // MoreInfoLinkLabel
            // 
            this.MoreInfoLinkLabel.AutoSize = true;
            this.MoreInfoLinkLabel.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MoreInfoLinkLabel.Location = new System.Drawing.Point(8, 208);
            this.MoreInfoLinkLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MoreInfoLinkLabel.Name = "MoreInfoLinkLabel";
            this.MoreInfoLinkLabel.Size = new System.Drawing.Size(56, 16);
            this.MoreInfoLinkLabel.TabIndex = 8;
            this.MoreInfoLinkLabel.TabStop = true;
            this.MoreInfoLinkLabel.Text = "moreInfo";
            this.MoreInfoLinkLabel.VisitedLinkColor = System.Drawing.Color.Blue;
            this.MoreInfoLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreInfoLinkLabel_LinkClicked);
            // 
            // PoweredLabel
            // 
            this.PoweredLabel.AutoSize = true;
            this.PoweredLabel.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PoweredLabel.Location = new System.Drawing.Point(10, 192);
            this.PoweredLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PoweredLabel.Name = "PoweredLabel";
            this.PoweredLabel.Size = new System.Drawing.Size(54, 16);
            this.PoweredLabel.TabIndex = 7;
            this.PoweredLabel.Text = "Powered";
            // 
            // CheckIfThereButton
            // 
            this.CheckIfThereButton.Location = new System.Drawing.Point(10, 71);
            this.CheckIfThereButton.Name = "CheckIfThereButton";
            this.CheckIfThereButton.Size = new System.Drawing.Size(234, 52);
            this.CheckIfThereButton.TabIndex = 9;
            this.CheckIfThereButton.Text = "请点击本按钮确认在线";
            this.CheckIfThereButton.UseVisualStyleBackColor = true;
            this.CheckIfThereButton.Visible = false;
            this.CheckIfThereButton.Click += new System.EventHandler(this.checkIfThereButton_Click);
            // 
            // ColdDownTimer
            // 
            this.ColdDownTimer.Interval = 120000;
            this.ColdDownTimer.Tick += new System.EventHandler(this.coldDownTimer_Tick);
            // 
            // Normal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(254, 233);
            this.Controls.Add(this.CheckIfThereButton);
            this.Controls.Add(this.MoreInfoLinkLabel);
            this.Controls.Add(this.PoweredLabel);
            this.Controls.Add(this.RoomLabel);
            this.Controls.Add(this.ChangeButton);
            this.Controls.Add(this.SignOutButton);
            this.Controls.Add(this.QueryButton);
            this.Controls.Add(this.StateLabel);
            this.Controls.Add(this.IDLabel);
            this.Controls.Add(this.NameLabel);
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

        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label IDLabel;
        private System.Windows.Forms.Label StateLabel;
        private System.Windows.Forms.Button QueryButton;
        private System.Windows.Forms.Button SignOutButton;
        private System.Windows.Forms.Button ChangeButton;
        private System.Windows.Forms.Timer KeepTimer;
        private System.Windows.Forms.Timer CheckTimer;
        private System.Windows.Forms.Label RoomLabel;
        private System.Windows.Forms.LinkLabel MoreInfoLinkLabel;
        private System.Windows.Forms.Label PoweredLabel;
        private System.Windows.Forms.Button CheckIfThereButton;
        private System.Windows.Forms.Timer ColdDownTimer;
    }
}