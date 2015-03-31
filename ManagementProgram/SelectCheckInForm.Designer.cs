namespace ManagementProgram
{
    partial class SelectCheckInForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectCheckInForm));
            this.studentDetailsBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.studentDetailsBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.checkIn_StudentDetailsDataGridView = new System.Windows.Forms.DataGridView();
            this.checkInStudentDetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.studentDetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.workOnlineDataSet = new ManagementProgram.WorkOnlineDataSet();
            this.checkIn_StudentDetailsTableAdapter = new ManagementProgram.WorkOnlineDataSetTableAdapters.CheckIn_StudentDetailsTableAdapter();
            this.tableAdapterManager = new ManagementProgram.WorkOnlineDataSetTableAdapters.TableAdapterManager();
            this.idTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lessonDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.signInTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.changeTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.signOutTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keepTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.signInStateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.studentDetailsBindingNavigator)).BeginInit();
            this.studentDetailsBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkIn_StudentDetailsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkInStudentDetailsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentDetailsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.workOnlineDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // studentDetailsBindingNavigator
            // 
            this.studentDetailsBindingNavigator.AddNewItem = null;
            this.studentDetailsBindingNavigator.BindingSource = this.studentDetailsBindingSource;
            this.studentDetailsBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.studentDetailsBindingNavigator.DeleteItem = null;
            this.studentDetailsBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.studentDetailsBindingNavigatorSaveItem,
            this.idTextBox,
            this.toolStripButton1});
            this.studentDetailsBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.studentDetailsBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.studentDetailsBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.studentDetailsBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.studentDetailsBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.studentDetailsBindingNavigator.Name = "studentDetailsBindingNavigator";
            this.studentDetailsBindingNavigator.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.studentDetailsBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.studentDetailsBindingNavigator.Size = new System.Drawing.Size(887, 27);
            this.studentDetailsBindingNavigator.TabIndex = 0;
            this.studentDetailsBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 24);
            this.bindingNavigatorMoveFirstItem.Text = "移到第一条记录";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 24);
            this.bindingNavigatorMovePreviousItem.Text = "移到上一条记录";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "位置";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(73, 27);
            this.bindingNavigatorPositionItem.Text = "1";
            this.bindingNavigatorPositionItem.ToolTipText = "当前位置";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(38, 24);
            this.bindingNavigatorCountItem.Text = "/ {0}";
            this.bindingNavigatorCountItem.ToolTipText = "总项数";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 24);
            this.bindingNavigatorMoveNextItem.Text = "移到下一条记录";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 24);
            this.bindingNavigatorMoveLastItem.Text = "移到最后一条记录";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // studentDetailsBindingNavigatorSaveItem
            // 
            this.studentDetailsBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.studentDetailsBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("studentDetailsBindingNavigatorSaveItem.Image")));
            this.studentDetailsBindingNavigatorSaveItem.Name = "studentDetailsBindingNavigatorSaveItem";
            this.studentDetailsBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 24);
            this.studentDetailsBindingNavigatorSaveItem.Text = "保存数据";
            this.studentDetailsBindingNavigatorSaveItem.Click += new System.EventHandler(this.studentDetailsBindingNavigatorSaveItem_Click_1);
            // 
            // checkIn_StudentDetailsDataGridView
            // 
            this.checkIn_StudentDetailsDataGridView.AutoGenerateColumns = false;
            this.checkIn_StudentDetailsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.checkIn_StudentDetailsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.Column1,
            this.lessonDataGridViewTextBoxColumn,
            this.iPDataGridViewTextBoxColumn,
            this.signInTimeDataGridViewTextBoxColumn,
            this.changeTimeDataGridViewTextBoxColumn,
            this.signOutTimeDataGridViewTextBoxColumn,
            this.keepTimeDataGridViewTextBoxColumn,
            this.signInStateDataGridViewTextBoxColumn,
            this.actionDataGridViewTextBoxColumn,
            this.stateDataGridViewTextBoxColumn,
            this.noteDataGridViewTextBoxColumn});
            this.checkIn_StudentDetailsDataGridView.DataSource = this.checkInStudentDetailsBindingSource;
            this.checkIn_StudentDetailsDataGridView.Location = new System.Drawing.Point(13, 32);
            this.checkIn_StudentDetailsDataGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkIn_StudentDetailsDataGridView.Name = "checkIn_StudentDetailsDataGridView";
            this.checkIn_StudentDetailsDataGridView.RowTemplate.Height = 27;
            this.checkIn_StudentDetailsDataGridView.Size = new System.Drawing.Size(861, 538);
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
            this.tableAdapterManager.UpdateOrder = ManagementProgram.WorkOnlineDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // idTextBox
            // 
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(100, 27);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(73, 24);
            this.toolStripButton1.Text = "查询学号";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "学号";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Name";
            this.Column1.HeaderText = "姓名";
            this.Column1.Name = "Column1";
            // 
            // lessonDataGridViewTextBoxColumn
            // 
            this.lessonDataGridViewTextBoxColumn.DataPropertyName = "Lesson";
            this.lessonDataGridViewTextBoxColumn.HeaderText = "节次";
            this.lessonDataGridViewTextBoxColumn.Name = "lessonDataGridViewTextBoxColumn";
            // 
            // iPDataGridViewTextBoxColumn
            // 
            this.iPDataGridViewTextBoxColumn.DataPropertyName = "IP";
            this.iPDataGridViewTextBoxColumn.HeaderText = "签到IP";
            this.iPDataGridViewTextBoxColumn.Name = "iPDataGridViewTextBoxColumn";
            // 
            // signInTimeDataGridViewTextBoxColumn
            // 
            this.signInTimeDataGridViewTextBoxColumn.DataPropertyName = "SignInTime";
            this.signInTimeDataGridViewTextBoxColumn.HeaderText = "签到时间";
            this.signInTimeDataGridViewTextBoxColumn.Name = "signInTimeDataGridViewTextBoxColumn";
            this.signInTimeDataGridViewTextBoxColumn.Width = 120;
            // 
            // changeTimeDataGridViewTextBoxColumn
            // 
            this.changeTimeDataGridViewTextBoxColumn.DataPropertyName = "ChangeTime";
            this.changeTimeDataGridViewTextBoxColumn.HeaderText = "换机时间";
            this.changeTimeDataGridViewTextBoxColumn.Name = "changeTimeDataGridViewTextBoxColumn";
            this.changeTimeDataGridViewTextBoxColumn.Width = 120;
            // 
            // signOutTimeDataGridViewTextBoxColumn
            // 
            this.signOutTimeDataGridViewTextBoxColumn.DataPropertyName = "SignOutTime";
            this.signOutTimeDataGridViewTextBoxColumn.HeaderText = "签退时间";
            this.signOutTimeDataGridViewTextBoxColumn.Name = "signOutTimeDataGridViewTextBoxColumn";
            this.signOutTimeDataGridViewTextBoxColumn.Width = 120;
            // 
            // keepTimeDataGridViewTextBoxColumn
            // 
            this.keepTimeDataGridViewTextBoxColumn.DataPropertyName = "KeepTime";
            this.keepTimeDataGridViewTextBoxColumn.HeaderText = "保持在线";
            this.keepTimeDataGridViewTextBoxColumn.Name = "keepTimeDataGridViewTextBoxColumn";
            this.keepTimeDataGridViewTextBoxColumn.Width = 120;
            // 
            // signInStateDataGridViewTextBoxColumn
            // 
            this.signInStateDataGridViewTextBoxColumn.DataPropertyName = "SignInState";
            this.signInStateDataGridViewTextBoxColumn.HeaderText = "签到状态";
            this.signInStateDataGridViewTextBoxColumn.Name = "signInStateDataGridViewTextBoxColumn";
            this.signInStateDataGridViewTextBoxColumn.Width = 120;
            // 
            // actionDataGridViewTextBoxColumn
            // 
            this.actionDataGridViewTextBoxColumn.DataPropertyName = "Action";
            this.actionDataGridViewTextBoxColumn.HeaderText = "注销状态";
            this.actionDataGridViewTextBoxColumn.Name = "actionDataGridViewTextBoxColumn";
            this.actionDataGridViewTextBoxColumn.Width = 120;
            // 
            // stateDataGridViewTextBoxColumn
            // 
            this.stateDataGridViewTextBoxColumn.DataPropertyName = "State";
            this.stateDataGridViewTextBoxColumn.HeaderText = "当前状态";
            this.stateDataGridViewTextBoxColumn.Name = "stateDataGridViewTextBoxColumn";
            this.stateDataGridViewTextBoxColumn.Width = 120;
            // 
            // noteDataGridViewTextBoxColumn
            // 
            this.noteDataGridViewTextBoxColumn.DataPropertyName = "Note";
            this.noteDataGridViewTextBoxColumn.HeaderText = "备注";
            this.noteDataGridViewTextBoxColumn.Name = "noteDataGridViewTextBoxColumn";
            // 
            // SelectCheckInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 584);
            this.Controls.Add(this.checkIn_StudentDetailsDataGridView);
            this.Controls.Add(this.studentDetailsBindingNavigator);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectCheckInForm";
            this.ShowIcon = false;
            this.Text = "SelectCheckInForm";
            ((System.ComponentModel.ISupportInitialize)(this.studentDetailsBindingNavigator)).EndInit();
            this.studentDetailsBindingNavigator.ResumeLayout(false);
            this.studentDetailsBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkIn_StudentDetailsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkInStudentDetailsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentDetailsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.workOnlineDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WorkOnlineDataSet workOnlineDataSet;
        private System.Windows.Forms.BindingSource studentDetailsBindingSource;
        private WorkOnlineDataSetTableAdapters.CheckIn_StudentDetailsTableAdapter checkIn_StudentDetailsTableAdapter;
        private WorkOnlineDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator studentDetailsBindingNavigator;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton studentDetailsBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView checkIn_StudentDetailsDataGridView;
        private System.Windows.Forms.BindingSource checkInStudentDetailsBindingSource;
        private System.Windows.Forms.ToolStripTextBox idTextBox;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn lessonDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn signInTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn changeTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn signOutTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn keepTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn signInStateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn actionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn noteDataGridViewTextBoxColumn;

    }
}