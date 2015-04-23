namespace CheckInProgram
{
    partial class QueryForm
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
            this.checkIn_StudentDetailsDataGridView = new System.Windows.Forms.DataGridView();
            this.checkInStudentDetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.studentDetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.workOnlineDataSet = new CheckInProgram.WorkOnlineDataSet();
            this.checkIn_StudentDetailsTableAdapter = new CheckInProgram.WorkOnlineDataSetTableAdapters.CheckIn_StudentDetailsTableAdapter();
            this.tableAdapterManager = new CheckInProgram.WorkOnlineDataSetTableAdapters.TableAdapterManager();
            this.signInTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.signOutTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.checkIn_StudentDetailsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkInStudentDetailsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentDetailsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.workOnlineDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // checkIn_StudentDetailsDataGridView
            // 
            this.checkIn_StudentDetailsDataGridView.AllowUserToAddRows = false;
            this.checkIn_StudentDetailsDataGridView.AllowUserToDeleteRows = false;
            this.checkIn_StudentDetailsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkIn_StudentDetailsDataGridView.AutoGenerateColumns = false;
            this.checkIn_StudentDetailsDataGridView.BackgroundColor = System.Drawing.SystemColors.Menu;
            this.checkIn_StudentDetailsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkIn_StudentDetailsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.checkIn_StudentDetailsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.signInTimeDataGridViewTextBoxColumn,
            this.signOutTimeDataGridViewTextBoxColumn,
            this.actionDataGridViewTextBoxColumn,
            this.stateDataGridViewTextBoxColumn,
            this.noteDataGridViewTextBoxColumn});
            this.checkIn_StudentDetailsDataGridView.DataSource = this.checkInStudentDetailsBindingSource;
            this.checkIn_StudentDetailsDataGridView.Location = new System.Drawing.Point(12, 12);
            this.checkIn_StudentDetailsDataGridView.Name = "checkIn_StudentDetailsDataGridView";
            this.checkIn_StudentDetailsDataGridView.ReadOnly = true;
            this.checkIn_StudentDetailsDataGridView.RowHeadersVisible = false;
            this.checkIn_StudentDetailsDataGridView.RowTemplate.Height = 27;
            this.checkIn_StudentDetailsDataGridView.Size = new System.Drawing.Size(806, 180);
            this.checkIn_StudentDetailsDataGridView.TabIndex = 1;
            // 
            // checkInStudentDetailsBindingSource
            // 
            this.checkInStudentDetailsBindingSource.DataMember = "CheckIn.StudentDetails";
            this.checkInStudentDetailsBindingSource.DataSource = this.studentDetailsBindingSource;
            // 
            // studentDetailsBindingSource
            // 
            this.studentDetailsBindingSource.DataSource = this.workOnlineDataSet;
            this.studentDetailsBindingSource.Position = 0;
            // 
            // workOnlineDataSet
            // 
            this.workOnlineDataSet.DataSetName = "WorkOnlineDataSet";
            this.workOnlineDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // checkIn_StudentDetailsTableAdapter
            // 
            this.checkIn_StudentDetailsTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.Connection = null;
            this.tableAdapterManager.UpdateOrder = CheckInProgram.WorkOnlineDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // signInTimeDataGridViewTextBoxColumn
            // 
            this.signInTimeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.signInTimeDataGridViewTextBoxColumn.DataPropertyName = "SignInTime";
            this.signInTimeDataGridViewTextBoxColumn.FillWeight = 150F;
            this.signInTimeDataGridViewTextBoxColumn.HeaderText = "签到时间";
            this.signInTimeDataGridViewTextBoxColumn.Name = "signInTimeDataGridViewTextBoxColumn";
            this.signInTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // signOutTimeDataGridViewTextBoxColumn
            // 
            this.signOutTimeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.signOutTimeDataGridViewTextBoxColumn.DataPropertyName = "SignOutTime";
            this.signOutTimeDataGridViewTextBoxColumn.FillWeight = 150F;
            this.signOutTimeDataGridViewTextBoxColumn.HeaderText = "签退时间";
            this.signOutTimeDataGridViewTextBoxColumn.Name = "signOutTimeDataGridViewTextBoxColumn";
            this.signOutTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // actionDataGridViewTextBoxColumn
            // 
            this.actionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.actionDataGridViewTextBoxColumn.DataPropertyName = "Action";
            this.actionDataGridViewTextBoxColumn.FillWeight = 50F;
            this.actionDataGridViewTextBoxColumn.HeaderText = "注销";
            this.actionDataGridViewTextBoxColumn.Name = "actionDataGridViewTextBoxColumn";
            this.actionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // stateDataGridViewTextBoxColumn
            // 
            this.stateDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.stateDataGridViewTextBoxColumn.DataPropertyName = "State";
            this.stateDataGridViewTextBoxColumn.FillWeight = 50F;
            this.stateDataGridViewTextBoxColumn.HeaderText = "状态";
            this.stateDataGridViewTextBoxColumn.Name = "stateDataGridViewTextBoxColumn";
            this.stateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // noteDataGridViewTextBoxColumn
            // 
            this.noteDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.noteDataGridViewTextBoxColumn.DataPropertyName = "Note";
            this.noteDataGridViewTextBoxColumn.FillWeight = 150F;
            this.noteDataGridViewTextBoxColumn.HeaderText = "备注";
            this.noteDataGridViewTextBoxColumn.Name = "noteDataGridViewTextBoxColumn";
            this.noteDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // QueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(830, 204);
            this.Controls.Add(this.checkIn_StudentDetailsDataGridView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QueryForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查询窗体";
            this.Load += new System.EventHandler(this.QueryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.checkIn_StudentDetailsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkInStudentDetailsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentDetailsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.workOnlineDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private WorkOnlineDataSet workOnlineDataSet;
        private System.Windows.Forms.BindingSource studentDetailsBindingSource;
        private WorkOnlineDataSetTableAdapters.CheckIn_StudentDetailsTableAdapter checkIn_StudentDetailsTableAdapter;
        private WorkOnlineDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.DataGridView checkIn_StudentDetailsDataGridView;
        private System.Windows.Forms.BindingSource checkInStudentDetailsBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn signInTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn signOutTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn actionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn noteDataGridViewTextBoxColumn;
    }
}