namespace WindowsApplication1
{
    partial class Form_SubConfig
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
            this.checkBox_isModel = new System.Windows.Forms.CheckBox();
            this.checkBox_isTop = new System.Windows.Forms.CheckBox();
            this.label_AppStartID = new System.Windows.Forms.Label();
            this.textBox_AppStartID = new System.Windows.Forms.TextBox();
            this.label_ClassStartID = new System.Windows.Forms.Label();
            this.textBox_ClassStartID = new System.Windows.Forms.TextBox();
            this.label_ClassEndID = new System.Windows.Forms.Label();
            this.textBox_ClassEndID = new System.Windows.Forms.TextBox();
            this.label_Screen_X = new System.Windows.Forms.Label();
            this.label_Screen_Y = new System.Windows.Forms.Label();
            this.label_Screen_Width = new System.Windows.Forms.Label();
            this.label_Screen_Hight = new System.Windows.Forms.Label();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.textBox_Screen_X = new System.Windows.Forms.TextBox();
            this.textBox_Screen_Y = new System.Windows.Forms.TextBox();
            this.textBox_Screen_Width = new System.Windows.Forms.TextBox();
            this.textBox_Screen_Hight = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.fileName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkBox_isModel
            // 
            this.checkBox_isModel.AutoSize = true;
            this.checkBox_isModel.Location = new System.Drawing.Point(167, 13);
            this.checkBox_isModel.Name = "checkBox_isModel";
            this.checkBox_isModel.Size = new System.Drawing.Size(72, 16);
            this.checkBox_isModel.TabIndex = 0;
            this.checkBox_isModel.Text = "调试模式";
            this.checkBox_isModel.UseVisualStyleBackColor = true;
            // 
            // checkBox_isTop
            // 
            this.checkBox_isTop.AutoSize = true;
            this.checkBox_isTop.Location = new System.Drawing.Point(13, 13);
            this.checkBox_isTop.Name = "checkBox_isTop";
            this.checkBox_isTop.Size = new System.Drawing.Size(114, 16);
            this.checkBox_isTop.TabIndex = 1;
            this.checkBox_isTop.Text = "MMI窗体前端显示";
            this.checkBox_isTop.UseVisualStyleBackColor = true;
            // 
            // label_AppStartID
            // 
            this.label_AppStartID.AutoSize = true;
            this.label_AppStartID.Location = new System.Drawing.Point(7, 56);
            this.label_AppStartID.Name = "label_AppStartID";
            this.label_AppStartID.Size = new System.Drawing.Size(89, 12);
            this.label_AppStartID.TabIndex = 2;
            this.label_AppStartID.Text = "视图的启动编号";
            // 
            // textBox_AppStartID
            // 
            this.textBox_AppStartID.Location = new System.Drawing.Point(14, 71);
            this.textBox_AppStartID.Name = "textBox_AppStartID";
            this.textBox_AppStartID.Size = new System.Drawing.Size(75, 21);
            this.textBox_AppStartID.TabIndex = 3;
            this.textBox_AppStartID.TextChanged += new System.EventHandler(this.textBox_AppStartID_TextChanged);
            // 
            // label_ClassStartID
            // 
            this.label_ClassStartID.AutoSize = true;
            this.label_ClassStartID.Location = new System.Drawing.Point(106, 56);
            this.label_ClassStartID.Name = "label_ClassStartID";
            this.label_ClassStartID.Size = new System.Drawing.Size(77, 12);
            this.label_ClassStartID.TabIndex = 4;
            this.label_ClassStartID.Text = "课程开始视图";
            // 
            // textBox_ClassStartID
            // 
            this.textBox_ClassStartID.Location = new System.Drawing.Point(108, 71);
            this.textBox_ClassStartID.Name = "textBox_ClassStartID";
            this.textBox_ClassStartID.Size = new System.Drawing.Size(75, 21);
            this.textBox_ClassStartID.TabIndex = 5;
            this.textBox_ClassStartID.TextChanged += new System.EventHandler(this.textBox_ClassStartID_TextChanged);
            // 
            // label_ClassEndID
            // 
            this.label_ClassEndID.AutoSize = true;
            this.label_ClassEndID.Location = new System.Drawing.Point(204, 56);
            this.label_ClassEndID.Name = "label_ClassEndID";
            this.label_ClassEndID.Size = new System.Drawing.Size(77, 12);
            this.label_ClassEndID.TabIndex = 6;
            this.label_ClassEndID.Text = "课程结束视图";
            // 
            // textBox_ClassEndID
            // 
            this.textBox_ClassEndID.Location = new System.Drawing.Point(206, 71);
            this.textBox_ClassEndID.Name = "textBox_ClassEndID";
            this.textBox_ClassEndID.Size = new System.Drawing.Size(75, 21);
            this.textBox_ClassEndID.TabIndex = 7;
            this.textBox_ClassEndID.TextChanged += new System.EventHandler(this.textBox_ClassEndID_TextChanged);
            // 
            // label_Screen_X
            // 
            this.label_Screen_X.AutoSize = true;
            this.label_Screen_X.Location = new System.Drawing.Point(3, 140);
            this.label_Screen_X.Name = "label_Screen_X";
            this.label_Screen_X.Size = new System.Drawing.Size(59, 12);
            this.label_Screen_X.TabIndex = 8;
            this.label_Screen_X.Text = "屏幕坐标X";
            // 
            // label_Screen_Y
            // 
            this.label_Screen_Y.AutoSize = true;
            this.label_Screen_Y.Location = new System.Drawing.Point(148, 140);
            this.label_Screen_Y.Name = "label_Screen_Y";
            this.label_Screen_Y.Size = new System.Drawing.Size(59, 12);
            this.label_Screen_Y.TabIndex = 9;
            this.label_Screen_Y.Text = "屏幕坐标Y";
            // 
            // label_Screen_Width
            // 
            this.label_Screen_Width.AutoSize = true;
            this.label_Screen_Width.Location = new System.Drawing.Point(3, 194);
            this.label_Screen_Width.Name = "label_Screen_Width";
            this.label_Screen_Width.Size = new System.Drawing.Size(53, 12);
            this.label_Screen_Width.TabIndex = 10;
            this.label_Screen_Width.Text = "屏幕宽度";
            // 
            // label_Screen_Hight
            // 
            this.label_Screen_Hight.AutoSize = true;
            this.label_Screen_Hight.Location = new System.Drawing.Point(148, 194);
            this.label_Screen_Hight.Name = "label_Screen_Hight";
            this.label_Screen_Hight.Size = new System.Drawing.Size(53, 12);
            this.label_Screen_Hight.TabIndex = 11;
            this.label_Screen_Hight.Text = "屏幕高度";
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(14, 238);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 12;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(205, 238);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 13;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox_Screen_X
            // 
            this.textBox_Screen_X.Location = new System.Drawing.Point(66, 136);
            this.textBox_Screen_X.Name = "textBox_Screen_X";
            this.textBox_Screen_X.Size = new System.Drawing.Size(78, 21);
            this.textBox_Screen_X.TabIndex = 14;
            this.textBox_Screen_X.TextChanged += new System.EventHandler(this.textBox_Screen_X_TextChanged);
            // 
            // textBox_Screen_Y
            // 
            this.textBox_Screen_Y.Location = new System.Drawing.Point(211, 136);
            this.textBox_Screen_Y.Name = "textBox_Screen_Y";
            this.textBox_Screen_Y.Size = new System.Drawing.Size(78, 21);
            this.textBox_Screen_Y.TabIndex = 15;
            this.textBox_Screen_Y.TextChanged += new System.EventHandler(this.textBox_Screen_Y_TextChanged);
            // 
            // textBox_Screen_Width
            // 
            this.textBox_Screen_Width.Location = new System.Drawing.Point(66, 190);
            this.textBox_Screen_Width.Name = "textBox_Screen_Width";
            this.textBox_Screen_Width.Size = new System.Drawing.Size(78, 21);
            this.textBox_Screen_Width.TabIndex = 16;
            this.textBox_Screen_Width.TextChanged += new System.EventHandler(this.textBox_Screen_Width_TextChanged);
            // 
            // textBox_Screen_Hight
            // 
            this.textBox_Screen_Hight.Location = new System.Drawing.Point(211, 190);
            this.textBox_Screen_Hight.Name = "textBox_Screen_Hight";
            this.textBox_Screen_Hight.Size = new System.Drawing.Size(78, 21);
            this.textBox_Screen_Hight.TabIndex = 17;
            this.textBox_Screen_Hight.TextChanged += new System.EventHandler(this.textBox_Screen_Hight_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(10, 112);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(127, 14);
            this.label8.TabIndex = 18;
            this.label8.Text = "显示屏坐标与长宽";
            // 
            // fileName
            // 
            this.fileName.AutoSize = true;
            this.fileName.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fileName.Location = new System.Drawing.Point(126, 237);
            this.fileName.MaximumSize = new System.Drawing.Size(88, 25);
            this.fileName.MinimumSize = new System.Drawing.Size(36, 24);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(36, 24);
            this.fileName.TabIndex = 19;
            this.fileName.Text = "ND";
            this.fileName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form_SubConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.ControlBox = false;
            this.Controls.Add(this.fileName);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox_Screen_Hight);
            this.Controls.Add(this.textBox_Screen_Width);
            this.Controls.Add(this.textBox_Screen_Y);
            this.Controls.Add(this.textBox_Screen_X);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.label_Screen_Hight);
            this.Controls.Add(this.label_Screen_Width);
            this.Controls.Add(this.label_Screen_Y);
            this.Controls.Add(this.label_Screen_X);
            this.Controls.Add(this.textBox_ClassEndID);
            this.Controls.Add(this.label_ClassEndID);
            this.Controls.Add(this.textBox_ClassStartID);
            this.Controls.Add(this.label_ClassStartID);
            this.Controls.Add(this.textBox_AppStartID);
            this.Controls.Add(this.label_AppStartID);
            this.Controls.Add(this.checkBox_isTop);
            this.Controls.Add(this.checkBox_isModel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_SubConfig";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form_SubConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_isModel;
        private System.Windows.Forms.CheckBox checkBox_isTop;
        private System.Windows.Forms.Label label_AppStartID;
        private System.Windows.Forms.TextBox textBox_AppStartID;
        private System.Windows.Forms.Label label_ClassStartID;
        private System.Windows.Forms.TextBox textBox_ClassStartID;
        private System.Windows.Forms.Label label_ClassEndID;
        private System.Windows.Forms.TextBox textBox_ClassEndID;
        private System.Windows.Forms.Label label_Screen_X;
        private System.Windows.Forms.Label label_Screen_Y;
        private System.Windows.Forms.Label label_Screen_Width;
        private System.Windows.Forms.Label label_Screen_Hight;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.TextBox textBox_Screen_X;
        private System.Windows.Forms.TextBox textBox_Screen_Y;
        private System.Windows.Forms.TextBox textBox_Screen_Width;
        private System.Windows.Forms.TextBox textBox_Screen_Hight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label fileName;
    }
}