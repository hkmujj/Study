namespace ShenHuaHaoTMS.View.V4
{
    partial class SH_V404_C1
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
            this.defLabel1 = new CommonControls.DefLabel();
            this._defLabel5 = new CommonControls.DefLabel();
            this.gridVisibleTableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridVisibleTableLayoutPanel1
            // 
            this.gridVisibleTableLayoutPanel1.BorderColor = System.Drawing.Color.Empty;
            this.gridVisibleTableLayoutPanel1.ColumnCount = 2;
            this.gridVisibleTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.gridVisibleTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.gridVisibleTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.gridVisibleTableLayoutPanel1.Controls.Add(this._defLabel5, 1, 0);
            this.gridVisibleTableLayoutPanel1.Controls.Add(this.defLabel1, 0, 0);
            this.gridVisibleTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridVisibleTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.gridVisibleTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.gridVisibleTableLayoutPanel1.Name = "gridVisibleTableLayoutPanel1";
            this.gridVisibleTableLayoutPanel1.RowCount = 1;
            this.gridVisibleTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.gridVisibleTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.gridVisibleTableLayoutPanel1.Size = new System.Drawing.Size(204, 33);
            this.gridVisibleTableLayoutPanel1.TabIndex = 0;
            // 
            // defLabel1
            // 
            this.defLabel1.BackColorNew = System.Drawing.Color.Black;
            this.defLabel1.BackgroundImage = global::ShenHuaHaoTMS.Properties.Resources.btn_Gray;
            this.defLabel1.BorderColor = System.Drawing.Color.Transparent;
            this.defLabel1.BorderWidth = 0F;
            this.defLabel1.DefText = "编组数量";
            this.defLabel1.DefUnit = "";
            this.defLabel1.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold);
            this.defLabel1.FontBrush = System.Drawing.Color.Black;
            this.defLabel1.Horizontal = CommonControls.Alignment.Center;
            this.defLabel1.Location = new System.Drawing.Point(5, 0);
            this.defLabel1.Margin = new System.Windows.Forms.Padding(5, 0, 2, 0);
            this.defLabel1.Name = "defLabel1";
            this.defLabel1.ObliqueLine = false;
            this.defLabel1.Size = new System.Drawing.Size(95, 33);
            this.defLabel1.TabIndex = 0;
            this.defLabel1.Vertical = CommonControls.Alignment.Center;
            this.defLabel1.XOffset = 0F;
            this.defLabel1.YOffset = 2F;
            // 
            // _defLabel5
            // 
            this._defLabel5.BackColor = System.Drawing.Color.Black;
            this._defLabel5.BackColorNew = System.Drawing.Color.Black;
            this._defLabel5.BackgroundImage = global::ShenHuaHaoTMS.Properties.Resources.btn_White;
            this._defLabel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._defLabel5.BorderColor = System.Drawing.Color.Transparent;
            this._defLabel5.BorderWidth = 1F;
            this._defLabel5.DefText = "2";
            this._defLabel5.DefUnit = "";
            this._defLabel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this._defLabel5.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold);
            this._defLabel5.FontBrush = System.Drawing.Color.Black;
            this._defLabel5.Horizontal = CommonControls.Alignment.Center;
            this._defLabel5.Location = new System.Drawing.Point(104, 0);
            this._defLabel5.Margin = new System.Windows.Forms.Padding(2, 0, 1, 0);
            this._defLabel5.Name = "_defLabel5";
            this._defLabel5.ObliqueLine = false;
            this._defLabel5.Size = new System.Drawing.Size(99, 33);
            this._defLabel5.TabIndex = 15;
            this._defLabel5.Vertical = CommonControls.Alignment.Center;
            this._defLabel5.XOffset = 0F;
            this._defLabel5.YOffset = 2F;
            // 
            // SH_V404_C1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridVisibleTableLayoutPanel1);
            this.Name = "SH_V404_C1";
            this.Size = new System.Drawing.Size(204, 33);
            this.gridVisibleTableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CommonControls.GridVisibleTableLayoutPanel gridVisibleTableLayoutPanel1;
        private CommonControls.DefLabel defLabel1;
        private CommonControls.DefLabel _defLabel5;
    }
}
