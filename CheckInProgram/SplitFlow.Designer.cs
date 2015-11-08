namespace CheckInProgram
{
    partial class SplitFlow
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
            this.IDTextBox = new System.Windows.Forms.TextBox();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.IDLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.SignInButton = new System.Windows.Forms.Button();
            this.Title = new System.Windows.Forms.Label();
            this.CheckInTimer = new System.Windows.Forms.Timer(this.components);
            this.ExitTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // IDTextBox
            // 
            this.IDTextBox.Font = new System.Drawing.Font("方正等线", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IDTextBox.Location = new System.Drawing.Point(512, 270);
            this.IDTextBox.Margin = new System.Windows.Forms.Padding(10);
            this.IDTextBox.Name = "IDTextBox";
            this.IDTextBox.Size = new System.Drawing.Size(343, 37);
            this.IDTextBox.TabIndex = 0;
            this.IDTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.IDTextBox.TextChanged += new System.EventHandler(this.IDTextBox_TextChanged);
            // 
            // NameTextBox
            // 
            this.NameTextBox.Font = new System.Drawing.Font("方正等线", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NameTextBox.Location = new System.Drawing.Point(512, 382);
            this.NameTextBox.Margin = new System.Windows.Forms.Padding(10);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(343, 37);
            this.NameTextBox.TabIndex = 1;
            this.NameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // IDLabel
            // 
            this.IDLabel.AutoSize = true;
            this.IDLabel.BackColor = System.Drawing.Color.Transparent;
            this.IDLabel.Location = new System.Drawing.Point(585, 215);
            this.IDLabel.Margin = new System.Windows.Forms.Padding(10);
            this.IDLabel.Name = "IDLabel";
            this.IDLabel.Size = new System.Drawing.Size(197, 35);
            this.IDLabel.TabIndex = 2;
            this.IDLabel.Text = "请输入你的学号";
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.BackColor = System.Drawing.Color.Transparent;
            this.NameLabel.Location = new System.Drawing.Point(585, 327);
            this.NameLabel.Margin = new System.Windows.Forms.Padding(10);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(197, 35);
            this.NameLabel.TabIndex = 3;
            this.NameLabel.Text = "请输入你的姓名";
            // 
            // SignInButton
            // 
            this.SignInButton.Location = new System.Drawing.Point(588, 439);
            this.SignInButton.Margin = new System.Windows.Forms.Padding(10);
            this.SignInButton.Name = "SignInButton";
            this.SignInButton.Size = new System.Drawing.Size(191, 48);
            this.SignInButton.TabIndex = 4;
            this.SignInButton.Text = "登入系统";
            this.SignInButton.UseVisualStyleBackColor = true;
            this.SignInButton.Click += new System.EventHandler(this.SignInButton_Click);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.BackColor = System.Drawing.Color.Transparent;
            this.Title.Font = new System.Drawing.Font("黑体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Title.Location = new System.Drawing.Point(385, 127);
            this.Title.Margin = new System.Windows.Forms.Padding(30);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(596, 48);
            this.Title.TabIndex = 5;
            this.Title.Text = "大学英语听力课程分流系统";
            // 
            // CheckInTimer
            // 
            this.CheckInTimer.Tick += new System.EventHandler(this.CheckInTimer_Tick);
            // 
            // ExitTimer
            // 
            this.ExitTimer.Enabled = true;
            this.ExitTimer.Interval = 1800000;
            this.ExitTimer.Tick += new System.EventHandler(this.ExitTimer_Tick);
            // 
            // SplitFlow
            // 
            this.AcceptButton = this.SignInButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::CheckInProgram.Properties.Resources.分流系统界面;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.ControlBox = false;
            this.Controls.Add(this.Title);
            this.Controls.Add(this.SignInButton);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.IDLabel);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.IDTextBox);
            this.Font = new System.Drawing.Font("方正等线", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "SplitFlow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SplitFlow";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SplitFlow_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox IDTextBox;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Label IDLabel;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Button SignInButton;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Timer CheckInTimer;
        private System.Windows.Forms.Timer ExitTimer;
    }
}