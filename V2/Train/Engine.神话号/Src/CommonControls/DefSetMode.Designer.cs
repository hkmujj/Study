namespace CommonControls
{
    partial class DefSetMode
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
            this._panel = new CommonControls.GridVisibleTableLayoutPanel();
            this.gridVisibleTableLayoutPanel8 = new CommonControls.GridVisibleTableLayoutPanel();
            this._defRadioBtn_1 = new CommonControls.DefRadioButton();
            this._defLabel_Title = new CommonControls.DefLabel();
            this._defRadioBtn_2 = new CommonControls.DefRadioButton();
            this._panel.SuspendLayout();
            this.gridVisibleTableLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._defLabel_Title)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // _panel
            // 
            this._panel.BorderColor = System.Drawing.Color.White;
            this._panel.ColumnCount = 1;
            this._panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._panel.Controls.Add(this.gridVisibleTableLayoutPanel8, 0, 0);
            this._panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panel.Location = new System.Drawing.Point(0, 0);
            this._panel.Margin = new System.Windows.Forms.Padding(0);
            this._panel.Name = "_panel";
            this._panel.RowCount = 1;
            this._panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 148F));
            this._panel.Size = new System.Drawing.Size(687, 150);
            this._panel.TabIndex = 1;
            // 
            // gridVisibleTableLayoutPanel8
            // 
            this.gridVisibleTableLayoutPanel8.BorderColor = System.Drawing.Color.Empty;
            this.gridVisibleTableLayoutPanel8.ColumnCount = 3;
            this.gridVisibleTableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.09224F));
            this.gridVisibleTableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.60029F));
            this.gridVisibleTableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.45388F));
            this.gridVisibleTableLayoutPanel8.Controls.Add(this._defRadioBtn_1, 0, 0);
            this.gridVisibleTableLayoutPanel8.Controls.Add(this._defLabel_Title, 0, 0);
            this.gridVisibleTableLayoutPanel8.Controls.Add(this._defRadioBtn_2, 1, 0);
            this.gridVisibleTableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridVisibleTableLayoutPanel8.Location = new System.Drawing.Point(2, 2);
            this.gridVisibleTableLayoutPanel8.Margin = new System.Windows.Forms.Padding(2);
            this.gridVisibleTableLayoutPanel8.Name = "gridVisibleTableLayoutPanel8";
            this.gridVisibleTableLayoutPanel8.RowCount = 1;
            this.gridVisibleTableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.gridVisibleTableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 146F));
            this.gridVisibleTableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 146F));
            this.gridVisibleTableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 146F));
            this.gridVisibleTableLayoutPanel8.Size = new System.Drawing.Size(683, 146);
            this.gridVisibleTableLayoutPanel8.TabIndex = 0;
            // 
            // _defRadioBtn_1
            // 
            this._defRadioBtn_1.AutoSize = true;
            this._defRadioBtn_1.BackPoint = new System.Drawing.Rectangle(0, 60, 18, 18);
            this._defRadioBtn_1.DefRect = new System.Drawing.Rectangle(30, 47, 100, 50);
            this._defRadioBtn_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._defRadioBtn_1.DownImage = global::CommonControls.Properties.Resources.RadioUp;
            this._defRadioBtn_1.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold);
            this._defRadioBtn_1.ForeColor = System.Drawing.Color.White;
            this._defRadioBtn_1.Location = new System.Drawing.Point(266, 0);
            this._defRadioBtn_1.Margin = new System.Windows.Forms.Padding(0);
            this._defRadioBtn_1.Name = "_defRadioBtn_1";
            this._defRadioBtn_1.Size = new System.Drawing.Size(208, 146);
            this._defRadioBtn_1.TabIndex = 2;
            this._defRadioBtn_1.TabStop = true;
            this._defRadioBtn_1.Text = "模式一";
            this._defRadioBtn_1.UpImage = global::CommonControls.Properties.Resources.RadioDown;
            this._defRadioBtn_1.UseVisualStyleBackColor = true;
            // 
            // _defLabel_Title
            // 
            this._defLabel_Title.BackColor = System.Drawing.Color.Black;
            this._defLabel_Title.BackColorNew = System.Drawing.Color.Black;
            this._defLabel_Title.BorderColor = System.Drawing.Color.Transparent;
            this._defLabel_Title.BorderWidth = 1F;
            this._defLabel_Title.DefText = "模式标题";
            this._defLabel_Title.DefUnit = "";
            this._defLabel_Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this._defLabel_Title.Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this._defLabel_Title.FontBrush = System.Drawing.Color.White;
            this._defLabel_Title.Horizontal = CommonControls.Alignment.Center;
            this._defLabel_Title.Location = new System.Drawing.Point(0, 0);
            this._defLabel_Title.Margin = new System.Windows.Forms.Padding(0);
            this._defLabel_Title.Name = "_defLabel_Title";
            this._defLabel_Title.ObliqueLine = false;
            this._defLabel_Title.Size = new System.Drawing.Size(266, 146);
            this._defLabel_Title.TabIndex = 0;
            this._defLabel_Title.TabStop = false;
            this._defLabel_Title.Vertical = CommonControls.Alignment.Center;
            this._defLabel_Title.XOffset = 0F;
            this._defLabel_Title.YOffset = 0F;
            // 
            // _defRadioBtn_2
            // 
            this._defRadioBtn_2.AutoSize = true;
            this._defRadioBtn_2.BackPoint = new System.Drawing.Rectangle(0, 60, 18, 18);
            this._defRadioBtn_2.DefRect = new System.Drawing.Rectangle(30, 47, 100, 50);
            this._defRadioBtn_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._defRadioBtn_2.DownImage = global::CommonControls.Properties.Resources.RadioUp;
            this._defRadioBtn_2.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold);
            this._defRadioBtn_2.ForeColor = System.Drawing.Color.White;
            this._defRadioBtn_2.Location = new System.Drawing.Point(474, 0);
            this._defRadioBtn_2.Margin = new System.Windows.Forms.Padding(0);
            this._defRadioBtn_2.Name = "_defRadioBtn_2";
            this._defRadioBtn_2.Size = new System.Drawing.Size(209, 146);
            this._defRadioBtn_2.TabIndex = 1;
            this._defRadioBtn_2.TabStop = true;
            this._defRadioBtn_2.Text = "模式二";
            this._defRadioBtn_2.UpImage = global::CommonControls.Properties.Resources.RadioDown;
            this._defRadioBtn_2.UseVisualStyleBackColor = true;
            // 
            // DefSetMode
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this._panel);
            this.Size = new System.Drawing.Size(687, 150);
            this._panel.ResumeLayout(false);
            this.gridVisibleTableLayoutPanel8.ResumeLayout(false);
            this.gridVisibleTableLayoutPanel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._defLabel_Title)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GridVisibleTableLayoutPanel _panel;
        private GridVisibleTableLayoutPanel gridVisibleTableLayoutPanel8;
        private DefLabel _defLabel_Title;
        private DefRadioButton _defRadioBtn_2;
        private DefRadioButton _defRadioBtn_1;

    }
}
