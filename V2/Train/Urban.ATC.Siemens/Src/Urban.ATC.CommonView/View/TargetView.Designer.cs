using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CommonUtil.Model;
using Motor.ATP.CommonView.View;
using Motor.ATP.CommonView.View.RegionA;

namespace Urban.ATC.CommonView.View
{
    partial class TargetView
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private IContainer components = null;

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
            this.gridVisibleTableLayoutPanel1 = new GridVisibleTableLayoutPanel();
            this.gridVisibleTableLayoutPanel2 = new GridVisibleTableLayoutPanel();
            this.targetDistanceDegreeControl1 = new TargetDistanceDegreeTextControl();
            this.distanceProgressBar1 = new DistanceProgressBar();
            this.distanceLable = new Label();
            this.gridVisibleTableLayoutPanel1.SuspendLayout();
            this.gridVisibleTableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridVisibleTableLayoutPanel1
            // 
            this.gridVisibleTableLayoutPanel1.ColumnCount = 1;
            this.gridVisibleTableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.gridVisibleTableLayoutPanel1.Controls.Add(this.gridVisibleTableLayoutPanel2, 0, 1);
            this.gridVisibleTableLayoutPanel1.Controls.Add(this.distanceLable, 0, 0);
            this.gridVisibleTableLayoutPanel1.Dock = DockStyle.Fill;
            this.gridVisibleTableLayoutPanel1.Location = new Point(0, 0);
            this.gridVisibleTableLayoutPanel1.Margin = new Padding(0);
            this.gridVisibleTableLayoutPanel1.Name = "gridVisibleTableLayoutPanel1";
            this.gridVisibleTableLayoutPanel1.RowCount = 2;
            this.gridVisibleTableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 22F));
            this.gridVisibleTableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 78F));
            this.gridVisibleTableLayoutPanel1.Size = new Size(72, 308);
            this.gridVisibleTableLayoutPanel1.TabIndex = 0;
            // 
            // gridVisibleTableLayoutPanel2
            // 
            this.gridVisibleTableLayoutPanel2.ColumnCount = 2;
            this.gridVisibleTableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 76F));
            this.gridVisibleTableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));
            this.gridVisibleTableLayoutPanel2.Controls.Add(this.targetDistanceDegreeControl1, 0, 0);
            this.gridVisibleTableLayoutPanel2.Controls.Add(this.distanceProgressBar1, 1, 0);
            this.gridVisibleTableLayoutPanel2.Dock = DockStyle.Fill;
            this.gridVisibleTableLayoutPanel2.Location = new Point(0, 67);
            this.gridVisibleTableLayoutPanel2.Margin = new Padding(0);
            this.gridVisibleTableLayoutPanel2.Name = "gridVisibleTableLayoutPanel2";
            this.gridVisibleTableLayoutPanel2.RowCount = 1;
            this.gridVisibleTableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.gridVisibleTableLayoutPanel2.Size = new Size(72, 241);
            this.gridVisibleTableLayoutPanel2.TabIndex = 0;
            // 
            // targetDistanceDegreeControl1
            // 
            this.targetDistanceDegreeControl1.Dock = DockStyle.Fill;
            this.targetDistanceDegreeControl1.Font = new Font("Calibri", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.targetDistanceDegreeControl1.Location = new Point(4, 4);
            this.targetDistanceDegreeControl1.Margin = new Padding(4, 4, 4, 4);
            this.targetDistanceDegreeControl1.Name = "targetDistanceDegreeControl1";
            this.targetDistanceDegreeControl1.Size = new Size(46, 233);
            this.targetDistanceDegreeControl1.TabIndex = 0;
            this.targetDistanceDegreeControl1.TextPercent = 0.6F;
            // 
            // distanceProgressBar1
            // 
            this.distanceProgressBar1.Direction = Direction.Up;
            this.distanceProgressBar1.Dock = DockStyle.Fill;
            this.distanceProgressBar1.Location = new Point(54, 10);
            this.distanceProgressBar1.Margin = new Padding(0, 10, 4, 4);
            this.distanceProgressBar1.Name = "distanceProgressBar1";
            this.distanceProgressBar1.ScaleValue = 0D;
            this.distanceProgressBar1.Size = new Size(14, 227);
            this.distanceProgressBar1.TabIndex = 1;
            // 
            // distanceLable
            // 
            this.distanceLable.AutoSize = true;
            this.distanceLable.Dock = DockStyle.Fill;
            this.distanceLable.Font = new Font("新宋体", 12F);
            this.distanceLable.ForeColor = Color.LightGray;
            this.distanceLable.Location = new Point(4, 0);
            this.distanceLable.Margin = new Padding(4, 0, 4, 0);
            this.distanceLable.Name = "distanceLable";
            this.distanceLable.Size = new Size(64, 67);
            this.distanceLable.TabIndex = 1;
            this.distanceLable.Text = "0";
            this.distanceLable.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TargetView
            // 
            this.AutoScaleDimensions = new SizeF(8F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.gridVisibleTableLayoutPanel1);
            this.Margin = new Padding(0);
            this.Name = "TargetView";
            this.Size = new Size(72, 308);
            this.gridVisibleTableLayoutPanel1.ResumeLayout(false);
            this.gridVisibleTableLayoutPanel1.PerformLayout();
            this.gridVisibleTableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GridVisibleTableLayoutPanel gridVisibleTableLayoutPanel1;
        private GridVisibleTableLayoutPanel gridVisibleTableLayoutPanel2;
        private TargetDistanceDegreeTextControl targetDistanceDegreeControl1;
        private DistanceProgressBar distanceProgressBar1;
        private Label distanceLable;
    }
}
