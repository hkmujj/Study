namespace MMI.Facility.View.Views.DebugViews
{
    partial class PorjectViewControl
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
            this.dataGridView_views = new System.Windows.Forms.DataGridView();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView_class = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridView_object = new System.Windows.Forms.DataGridView();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridView_configInfo = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_views)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_class)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_object)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_configInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(636, 361);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView_configInfo);
            this.tabPage1.Size = new System.Drawing.Size(628, 335);
            this.tabPage1.Text = "配置信息";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dataGridView_class);
            this.tabPage3.Size = new System.Drawing.Size(628, 335);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dataGridView_object);
            this.tabPage4.Size = new System.Drawing.Size(628, 335);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView_views);
            this.tabPage2.Size = new System.Drawing.Size(628, 335);
            // 
            // dataGridView_views
            // 
            this.dataGridView_views.AllowUserToAddRows = false;
            this.dataGridView_views.AllowUserToDeleteRows = false;
            this.dataGridView_views.AllowUserToResizeColumns = false;
            this.dataGridView_views.AllowUserToResizeRows = false;
            this.dataGridView_views.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_views.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.Column9,
            this.Column13});
            this.dataGridView_views.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_views.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_views.MultiSelect = false;
            this.dataGridView_views.Name = "dataGridView_views";
            this.dataGridView_views.RowHeadersVisible = false;
            this.dataGridView_views.RowTemplate.Height = 23;
            this.dataGridView_views.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_views.ShowCellToolTips = false;
            this.dataGridView_views.Size = new System.Drawing.Size(622, 329);
            this.dataGridView_views.TabIndex = 1;
            this.dataGridView_views.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView_views_MouseDoubleClick);
            // 
            // Column8
            // 
            this.Column8.HeaderText = "编号";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "背景图";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 150;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "背景颜色";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Width = 120;
            // 
            // dataGridView_class
            // 
            this.dataGridView_class.AllowUserToAddRows = false;
            this.dataGridView_class.AllowUserToDeleteRows = false;
            this.dataGridView_class.AllowUserToResizeColumns = false;
            this.dataGridView_class.AllowUserToResizeRows = false;
            this.dataGridView_class.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_class.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column4,
            this.Column10,
            this.Column11});
            this.dataGridView_class.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_class.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_class.MultiSelect = false;
            this.dataGridView_class.Name = "dataGridView_class";
            this.dataGridView_class.RowHeadersVisible = false;
            this.dataGridView_class.RowTemplate.Height = 23;
            this.dataGridView_class.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_class.ShowCellToolTips = false;
            this.dataGridView_class.Size = new System.Drawing.Size(622, 329);
            this.dataGridView_class.TabIndex = 2;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "名称";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 120;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "用途";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 150;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "来源";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 280;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "使用";
            this.Column11.Name = "Column11";
            this.Column11.Width = 50;
            // 
            // dataGridView_object
            // 
            this.dataGridView_object.AllowUserToAddRows = false;
            this.dataGridView_object.AllowUserToDeleteRows = false;
            this.dataGridView_object.AllowUserToResizeColumns = false;
            this.dataGridView_object.AllowUserToResizeRows = false;
            this.dataGridView_object.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_object.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column7,
            this.Column6,
            this.Column5});
            this.dataGridView_object.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_object.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_object.MultiSelect = false;
            this.dataGridView_object.Name = "dataGridView_object";
            this.dataGridView_object.RowHeadersVisible = false;
            this.dataGridView_object.RowTemplate.Height = 23;
            this.dataGridView_object.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_object.ShowCellErrors = false;
            this.dataGridView_object.ShowCellToolTips = false;
            this.dataGridView_object.ShowEditingIcon = false;
            this.dataGridView_object.ShowRowErrors = false;
            this.dataGridView_object.Size = new System.Drawing.Size(622, 329);
            this.dataGridView_object.TabIndex = 3;
            this.dataGridView_object.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_object_CellContentClick);
            this.dataGridView_object.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_object_CellDoubleClick);
            this.dataGridView_object.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_object_CellMouseDown);
            this.dataGridView_object.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_object_CellMouseUp);
            this.dataGridView_object.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_object_CellValueChanged);
            // 
            // Column7
            // 
            this.Column7.HeaderText = "名称";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column7.Width = 150;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "编号";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "可视状态";
            this.Column5.Name = "Column5";
            // 
            // dataGridView_configInfo
            // 
            this.dataGridView_configInfo.AllowUserToAddRows = false;
            this.dataGridView_configInfo.AllowUserToDeleteRows = false;
            this.dataGridView_configInfo.AllowUserToResizeColumns = false;
            this.dataGridView_configInfo.AllowUserToResizeRows = false;
            this.dataGridView_configInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_configInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridView_configInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_configInfo.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_configInfo.MultiSelect = false;
            this.dataGridView_configInfo.Name = "dataGridView_configInfo";
            this.dataGridView_configInfo.RowHeadersVisible = false;
            this.dataGridView_configInfo.RowTemplate.Height = 23;
            this.dataGridView_configInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_configInfo.ShowCellToolTips = false;
            this.dataGridView_configInfo.Size = new System.Drawing.Size(622, 329);
            this.dataGridView_configInfo.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "名称";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 300;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "值";
            this.Column2.Name = "Column2";
            this.Column2.Width = 200;
            // 
            // PorjectViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Name = "PorjectViewControl";
            this.Size = new System.Drawing.Size(636, 386);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_views)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_class)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_object)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_configInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_views;
        private System.Windows.Forms.DataGridView dataGridView_class;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column11;
        private System.Windows.Forms.DataGridView dataGridView_object;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridView dataGridView_configInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;

    }
}