namespace Motor.ATP.CommonView.View.RegionE
{
    partial class DateTimeControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.constInfoLab = new System.Windows.Forms.Label();
            this.timeLable = new System.Windows.Forms.Label();
            this.dateLable = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.constInfoLab, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.timeLable, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dateLable, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(150, 150);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // constInfoLab
            // 
            this.constInfoLab.AutoSize = true;
            this.constInfoLab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.constInfoLab.Font = new System.Drawing.Font("幼圆", 14F);
            this.constInfoLab.Location = new System.Drawing.Point(3, 0);
            this.constInfoLab.Name = "constInfoLab";
            this.constInfoLab.Size = new System.Drawing.Size(144, 50);
            this.constInfoLab.TabIndex = 0;
            this.constInfoLab.Text = "时间日期";
            this.constInfoLab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timeLable
            // 
            this.timeLable.AutoSize = true;
            this.timeLable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeLable.Font = new System.Drawing.Font("幼圆", 14F);
            this.timeLable.Location = new System.Drawing.Point(3, 50);
            this.timeLable.Name = "timeLable";
            this.timeLable.Size = new System.Drawing.Size(144, 50);
            this.timeLable.TabIndex = 1;
            this.timeLable.Text = "label2";
            this.timeLable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateLable
            // 
            this.dateLable.AutoSize = true;
            this.dateLable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateLable.Font = new System.Drawing.Font("幼圆", 14F);
            this.dateLable.Location = new System.Drawing.Point(3, 100);
            this.dateLable.Name = "dateLable";
            this.dateLable.Size = new System.Drawing.Size(144, 50);
            this.dateLable.TabIndex = 2;
            this.dateLable.Text = "label3";
            this.dateLable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DateTimeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DateTimeControl";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label constInfoLab;
        private System.Windows.Forms.Label timeLable;
        private System.Windows.Forms.Label dateLable;
    }
}
