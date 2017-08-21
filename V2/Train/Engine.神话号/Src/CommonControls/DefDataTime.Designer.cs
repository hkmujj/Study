namespace CommonControls
{
    partial class DefDataTime
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
            this.gridVisibleTableLayoutPanel1 = new CommonControls.GridVisibleTableLayoutPanel();
            this._defLabel_Time = new CommonControls.DefLabel();
            this.gridVisibleTableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridVisibleTableLayoutPanel1
            // 
            this.gridVisibleTableLayoutPanel1.BorderColor = System.Drawing.Color.Empty;
            this.gridVisibleTableLayoutPanel1.ColumnCount = 1;
            this.gridVisibleTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.gridVisibleTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.gridVisibleTableLayoutPanel1.Controls.Add(this._defLabel_Time, 0, 0);
            this.gridVisibleTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridVisibleTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.gridVisibleTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.gridVisibleTableLayoutPanel1.Name = "gridVisibleTableLayoutPanel1";
            this.gridVisibleTableLayoutPanel1.RowCount = 1;
            this.gridVisibleTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.gridVisibleTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.gridVisibleTableLayoutPanel1.Size = new System.Drawing.Size(150, 150);
            this.gridVisibleTableLayoutPanel1.TabIndex = 0;
            // 
            // _defLabel_Time
            // 
            this._defLabel_Time.BackColor = System.Drawing.Color.Black;
            this._defLabel_Time.BorderColor = System.Drawing.Color.White;
            this._defLabel_Time.BorderWidth = 1F;
            this._defLabel_Time.DefText = "111";
            this._defLabel_Time.Dock = System.Windows.Forms.DockStyle.Fill;
            this._defLabel_Time.FontBrush = System.Drawing.Color.White;
            this._defLabel_Time.Horizontal = CommonControls.Alignment.Center;
            this._defLabel_Time.Location = new System.Drawing.Point(0, 0);
            this._defLabel_Time.Margin = new System.Windows.Forms.Padding(0);
            this._defLabel_Time.Name = "_defLabel_Time";
            this._defLabel_Time.Size = new System.Drawing.Size(150, 150);
            this._defLabel_Time.TabIndex = 0;
            this._defLabel_Time.Vertical = CommonControls.Alignment.Center;
            this._defLabel_Time.YOffset = 0F;
            // 
            // DefDataTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridVisibleTableLayoutPanel1);
            this.Name = "DefDataTime";
            this.gridVisibleTableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GridVisibleTableLayoutPanel gridVisibleTableLayoutPanel1;
        private DefLabel _defLabel_Time;
    }
}
