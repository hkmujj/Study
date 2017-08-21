namespace Motor.ATP.CommonView.View.RegionA
{
    partial class TargetDistanceMonitorControl
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
            this.rootTabPanel = new Motor.ATP.CommonView.View.GridVisibleTableLayoutPanel();
            this.distanceLable = new System.Windows.Forms.Label();
            this.targetDistanceDegreeControl1 = new Motor.ATP.CommonView.View.RegionA.TargetDistanceDegreeControl();
            this.distanceProgressBar1 = new Motor.ATP.CommonView.View.RegionA.DistanceProgressBar();
            this.rootTabPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // rootTabPanel
            // 
            this.rootTabPanel.ColumnCount = 2;
            this.rootTabPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.rootTabPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.rootTabPanel.Controls.Add(this.distanceLable, 0, 0);
            this.rootTabPanel.Controls.Add(this.targetDistanceDegreeControl1, 0, 1);
            this.rootTabPanel.Controls.Add(this.distanceProgressBar1, 1, 1);
            this.rootTabPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootTabPanel.Location = new System.Drawing.Point(0, 0);
            this.rootTabPanel.Name = "rootTabPanel";
            this.rootTabPanel.RowCount = 2;
            this.rootTabPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.54237F));
            this.rootTabPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.45763F));
            this.rootTabPanel.Size = new System.Drawing.Size(67, 295);
            this.rootTabPanel.TabIndex = 0;
            // 
            // distanceLable
            // 
            this.distanceLable.AutoSize = true;
            this.rootTabPanel.SetColumnSpan(this.distanceLable, 2);
            this.distanceLable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.distanceLable.Font = new System.Drawing.Font("新宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.distanceLable.Location = new System.Drawing.Point(3, 0);
            this.distanceLable.Name = "distanceLable";
            this.distanceLable.Size = new System.Drawing.Size(61, 36);
            this.distanceLable.TabIndex = 0;
            this.distanceLable.Text = "1000";
            this.distanceLable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // targetDistanceDegreeControl1
            // 
            this.targetDistanceDegreeControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.targetDistanceDegreeControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.targetDistanceDegreeControl1.Location = new System.Drawing.Point(2, 38);
            this.targetDistanceDegreeControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 0);
            this.targetDistanceDegreeControl1.Name = "targetDistanceDegreeControl1";
            this.targetDistanceDegreeControl1.Size = new System.Drawing.Size(29, 257);
            this.targetDistanceDegreeControl1.TabIndex = 1;
            // 
            // distanceProgressBar1
            // 
            this.distanceProgressBar1.Direction = CommonUtil.Model.Direction.Up;
            this.distanceProgressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.distanceProgressBar1.ForeColor = System.Drawing.Color.White;
            this.distanceProgressBar1.Location = new System.Drawing.Point(35, 38);
            this.distanceProgressBar1.Margin = new System.Windows.Forms.Padding(2, 2, 4, 0);
            this.distanceProgressBar1.Name = "distanceProgressBar1";
            this.distanceProgressBar1.ScaleValue = 0.2D;
            this.distanceProgressBar1.Size = new System.Drawing.Size(28, 257);
            this.distanceProgressBar1.TabIndex = 2;
            // 
            // TargetDistanceMonitorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rootTabPanel);
            this.Name = "TargetDistanceMonitorControl";
            this.Size = new System.Drawing.Size(67, 295);
            this.rootTabPanel.ResumeLayout(false);
            this.rootTabPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GridVisibleTableLayoutPanel rootTabPanel;
        private System.Windows.Forms.Label distanceLable;
        private TargetDistanceDegreeControl targetDistanceDegreeControl1;
        private DistanceProgressBar distanceProgressBar1;
    }
}
