namespace MMI.Facility.View.Views.Common
{
    partial class ChildAttributeControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView_inBool = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView_inFloat = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataGridView_other = new System.Windows.Forms.DataGridView();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dataGridView_para = new System.Windows.Forms.DataGridView();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_inBool)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_inFloat)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_other)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_para)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(431, 402);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView_inBool);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(423, 376);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "输入Bool";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView_inBool
            // 
            this.dataGridView_inBool.AllowUserToAddRows = false;
            this.dataGridView_inBool.AllowUserToDeleteRows = false;
            this.dataGridView_inBool.AllowUserToResizeRows = false;
            this.dataGridView_inBool.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedVertical;
            this.dataGridView_inBool.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView_inBool.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_inBool.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView_inBool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_inBool.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_inBool.Name = "dataGridView_inBool";
            this.dataGridView_inBool.ReadOnly = true;
            this.dataGridView_inBool.RowHeadersVisible = false;
            this.dataGridView_inBool.RowTemplate.Height = 23;
            this.dataGridView_inBool.Size = new System.Drawing.Size(417, 370);
            this.dataGridView_inBool.TabIndex = 1;
            this.dataGridView_inBool.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_inBool_CellDoubleClick);
            this.dataGridView_inBool.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_inBool_CellMouseDown);
            this.dataGridView_inBool.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_inBool_CellMouseUp);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "行号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 60;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "取值位置";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "备注";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView_inFloat);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(423, 376);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "输入Float";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView_inFloat
            // 
            this.dataGridView_inFloat.AllowUserToAddRows = false;
            this.dataGridView_inFloat.AllowUserToDeleteRows = false;
            this.dataGridView_inFloat.AllowUserToResizeColumns = false;
            this.dataGridView_inFloat.AllowUserToResizeRows = false;
            this.dataGridView_inFloat.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedVertical;
            this.dataGridView_inFloat.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView_inFloat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_inFloat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column5,
            this.Column6});
            this.dataGridView_inFloat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_inFloat.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_inFloat.Name = "dataGridView_inFloat";
            this.dataGridView_inFloat.ReadOnly = true;
            this.dataGridView_inFloat.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView_inFloat.RowHeadersVisible = false;
            this.dataGridView_inFloat.RowTemplate.Height = 23;
            this.dataGridView_inFloat.ShowCellErrors = false;
            this.dataGridView_inFloat.ShowCellToolTips = false;
            this.dataGridView_inFloat.ShowRowErrors = false;
            this.dataGridView_inFloat.Size = new System.Drawing.Size(417, 370);
            this.dataGridView_inFloat.TabIndex = 1;
            this.dataGridView_inFloat.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_inFloat_CellDoubleClick);
            this.dataGridView_inFloat.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_inFloat_CellMouseDown);
            this.dataGridView_inFloat.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_inFloat_CellMouseUp);
            // 
            // Column4
            // 
            this.Column4.HeaderText = "行号";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 60;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "取值位置";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "备注";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column6.Width = 150;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dataGridView_other);
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(423, 376);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "视图";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridView_other
            // 
            this.dataGridView_other.AllowUserToAddRows = false;
            this.dataGridView_other.AllowUserToDeleteRows = false;
            this.dataGridView_other.AllowUserToResizeColumns = false;
            this.dataGridView_other.AllowUserToResizeRows = false;
            this.dataGridView_other.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedVertical;
            this.dataGridView_other.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView_other.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_other.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column7,
            this.Column8});
            this.dataGridView_other.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_other.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_other.Name = "dataGridView_other";
            this.dataGridView_other.ReadOnly = true;
            this.dataGridView_other.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView_other.RowHeadersVisible = false;
            this.dataGridView_other.RowTemplate.Height = 23;
            this.dataGridView_other.ShowCellErrors = false;
            this.dataGridView_other.ShowCellToolTips = false;
            this.dataGridView_other.ShowRowErrors = false;
            this.dataGridView_other.Size = new System.Drawing.Size(417, 370);
            this.dataGridView_other.TabIndex = 1;
            this.dataGridView_other.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_other_CellMouseUp);
            // 
            // Column7
            // 
            this.Column7.HeaderText = "视图编号";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "可视状态";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dataGridView_para);
            this.tabPage4.Location = new System.Drawing.Point(4, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(423, 376);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "参数信息";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dataGridView_para
            // 
            this.dataGridView_para.AllowUserToAddRows = false;
            this.dataGridView_para.AllowUserToDeleteRows = false;
            this.dataGridView_para.AllowUserToResizeRows = false;
            this.dataGridView_para.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedVertical;
            this.dataGridView_para.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView_para.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_para.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column9,
            this.Column10,
            this.Column11});
            this.dataGridView_para.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_para.EnableHeadersVisualStyles = false;
            this.dataGridView_para.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_para.MultiSelect = false;
            this.dataGridView_para.Name = "dataGridView_para";
            this.dataGridView_para.ReadOnly = true;
            this.dataGridView_para.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView_para.RowHeadersVisible = false;
            this.dataGridView_para.RowTemplate.Height = 23;
            this.dataGridView_para.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect;
            this.dataGridView_para.ShowCellErrors = false;
            this.dataGridView_para.ShowCellToolTips = false;
            this.dataGridView_para.ShowEditingIcon = false;
            this.dataGridView_para.ShowRowErrors = false;
            this.dataGridView_para.Size = new System.Drawing.Size(417, 370);
            this.dataGridView_para.TabIndex = 1;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "行";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column9.Width = 50;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "值";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column10.Width = 150;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "备注";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column11.Width = 120;
            // 
            // ChildAttributeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "ChildAttributeControl";
            this.Size = new System.Drawing.Size(431, 402);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_inBool)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_inFloat)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_other)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_para)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView_inBool;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView_inFloat;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridView_other;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dataGridView_para;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
    }
}
