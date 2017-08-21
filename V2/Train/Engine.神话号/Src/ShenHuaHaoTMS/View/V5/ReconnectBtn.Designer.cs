namespace ShenHuaHaoTMS.View.V5
{
    partial class ReconnectBtn
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
            this.defButton1 = new CommonControls.DefButton();
            ((System.ComponentModel.ISupportInitialize)(this.defButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // defButton1
            // 
            this.defButton1.DefBorderWidth = 1;
            this.defButton1.DefButtonBorder = false;
            this.defButton1.DefText = "";
            this.defButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.defButton1.DownColor = System.Drawing.Color.White;
            this.defButton1.DownImage = null;
            this.defButton1.FocusImage = null;
            this.defButton1.ID = 1;
            this.defButton1.IsDown = false;
            this.defButton1.IsEnable = true;
            this.defButton1.IsFocused = false;
            this.defButton1.IsSelfResetBtn = true;
            this.defButton1.Location = new System.Drawing.Point(0, 0);
            this.defButton1.Margin = new System.Windows.Forms.Padding(0);
            this.defButton1.Name = "defButton1";
            this.defButton1.Size = new System.Drawing.Size(68, 60);
            this.defButton1.TabIndex = 0;
            this.defButton1.TabStop = false;
            this.defButton1.TextFont = new System.Drawing.Font("宋体", 13F);
            this.defButton1.UpColor = System.Drawing.Color.White;
            this.defButton1.UpImage = null;
            this.defButton1.ViewID = 1;
            this.defButton1.YOffset = 0F;
            // 
            // ReconnectBtn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.defButton1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ReconnectBtn";
            this.Size = new System.Drawing.Size(68, 60);
            ((System.ComponentModel.ISupportInitialize)(this.defButton1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CommonControls.DefButton defButton1;
    }
}
