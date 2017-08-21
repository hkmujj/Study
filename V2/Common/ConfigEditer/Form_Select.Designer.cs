namespace WindowsApplication1
{
    partial class Form_Select
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.configlistBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // configlistBox
            // 
            this.configlistBox.FormattingEnabled = true;
            this.configlistBox.HorizontalScrollbar = true;
            this.configlistBox.ItemHeight = 12;
            this.configlistBox.Location = new System.Drawing.Point(12, 59);
            this.configlistBox.Name = "configlistBox";
            this.configlistBox.Size = new System.Drawing.Size(177, 100);
            this.configlistBox.TabIndex = 0;
            this.configlistBox.DoubleClick += new System.EventHandler(this.configlistBox_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(28, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "双击要修改的文本";
            // 
            // Form_Select
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(201, 182);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.configlistBox);
            this.MaximizeBox = false;
            this.Name = "Form_Select";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Config修改器";
            this.Load += new System.EventHandler(this.Config_Modifications_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox configlistBox;
        private System.Windows.Forms.Label label1;

    }
}

