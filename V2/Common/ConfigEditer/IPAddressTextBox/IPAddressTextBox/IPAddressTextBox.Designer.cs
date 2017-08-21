namespace IPAddressTextBox
{
    partial class IPAddressTextBox
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
            this.IPAddressControl = new System.Windows.Forms.Panel();
            this.fourthTextBox = new System.Windows.Forms.TextBox();
            this.thirdLabel = new System.Windows.Forms.Label();
            this.thirdTextBox = new System.Windows.Forms.TextBox();
            this.secondLabel = new System.Windows.Forms.Label();
            this.secondTextBox = new System.Windows.Forms.TextBox();
            this.firstLabel = new System.Windows.Forms.Label();
            this.firstTextBox = new System.Windows.Forms.TextBox();
            this.IPAddressControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // IPAddressControl
            // 
            this.IPAddressControl.BackColor = System.Drawing.Color.White;
            this.IPAddressControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.IPAddressControl.Controls.Add(this.fourthTextBox);
            this.IPAddressControl.Controls.Add(this.thirdLabel);
            this.IPAddressControl.Controls.Add(this.thirdTextBox);
            this.IPAddressControl.Controls.Add(this.secondLabel);
            this.IPAddressControl.Controls.Add(this.secondTextBox);
            this.IPAddressControl.Controls.Add(this.firstLabel);
            this.IPAddressControl.Controls.Add(this.firstTextBox);
            this.IPAddressControl.Location = new System.Drawing.Point(0, 0);
            this.IPAddressControl.Name = "IPAddressControl";
            this.IPAddressControl.Size = new System.Drawing.Size(153, 21);
            this.IPAddressControl.TabIndex = 0;
            this.IPAddressControl.SizeChanged += new System.EventHandler(this.IPAddressControl_SizeChanged);
            // 
            // fourthTextBox
            // 
            this.fourthTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fourthTextBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fourthTextBox.Location = new System.Drawing.Point(119, 3);
            this.fourthTextBox.MaxLength = 3;
            this.fourthTextBox.Name = "fourthTextBox";
            this.fourthTextBox.Size = new System.Drawing.Size(30, 14);
            this.fourthTextBox.TabIndex = 6;
            this.fourthTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.fourthTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.fourthTextBox_KeyPress);
            // 
            // thirdLabel
            // 
            this.thirdLabel.AutoSize = true;
            this.thirdLabel.Location = new System.Drawing.Point(112, 5);
            this.thirdLabel.Name = "thirdLabel";
            this.thirdLabel.Size = new System.Drawing.Size(11, 12);
            this.thirdLabel.TabIndex = 5;
            this.thirdLabel.Text = ".";
            this.thirdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // thirdTextBox
            // 
            this.thirdTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.thirdTextBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.thirdTextBox.Location = new System.Drawing.Point(80, 3);
            this.thirdTextBox.MaxLength = 3;
            this.thirdTextBox.Name = "thirdTextBox";
            this.thirdTextBox.Size = new System.Drawing.Size(30, 14);
            this.thirdTextBox.TabIndex = 4;
            this.thirdTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.thirdTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.thirdTextBox_KeyPress);
            // 
            // secondLabel
            // 
            this.secondLabel.AutoSize = true;
            this.secondLabel.Location = new System.Drawing.Point(72, 5);
            this.secondLabel.Name = "secondLabel";
            this.secondLabel.Size = new System.Drawing.Size(11, 12);
            this.secondLabel.TabIndex = 3;
            this.secondLabel.Text = ".";
            this.secondLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // secondTextBox
            // 
            this.secondTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.secondTextBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.secondTextBox.Location = new System.Drawing.Point(41, 3);
            this.secondTextBox.MaxLength = 3;
            this.secondTextBox.Name = "secondTextBox";
            this.secondTextBox.Size = new System.Drawing.Size(30, 14);
            this.secondTextBox.TabIndex = 2;
            this.secondTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.secondTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.secondTextBox_KeyPress);
            // 
            // firstLabel
            // 
            this.firstLabel.AutoSize = true;
            this.firstLabel.Location = new System.Drawing.Point(32, 3);
            this.firstLabel.Name = "firstLabel";
            this.firstLabel.Size = new System.Drawing.Size(11, 12);
            this.firstLabel.TabIndex = 1;
            this.firstLabel.Text = ".";
            this.firstLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // firstTextBox
            // 
            this.firstTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.firstTextBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.firstTextBox.Location = new System.Drawing.Point(2, 3);
            this.firstTextBox.MaxLength = 3;
            this.firstTextBox.Name = "firstTextBox";
            this.firstTextBox.Size = new System.Drawing.Size(30, 14);
            this.firstTextBox.TabIndex = 0;
            this.firstTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.firstTextBox.Leave += new System.EventHandler(this.firstTextBox_Leave);
            this.firstTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.firstTextBox_KeyPress);
            // 
            // IPAddressTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.IPAddressControl);
            this.Name = "IPAddressTextBox";
            this.Size = new System.Drawing.Size(158, 21);
            this.IPAddressControl.ResumeLayout(false);
            this.IPAddressControl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel IPAddressControl;
        private System.Windows.Forms.TextBox firstTextBox;
        private System.Windows.Forms.Label firstLabel;
        private System.Windows.Forms.TextBox fourthTextBox;
        private System.Windows.Forms.Label thirdLabel;
        private System.Windows.Forms.TextBox thirdTextBox;
        private System.Windows.Forms.Label secondLabel;
        private System.Windows.Forms.TextBox secondTextBox;
    }
}
