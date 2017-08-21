namespace MyControls
{
    partial class DefDisplay
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
            this.SuspendLayout();
            // 
            // DefDisplay
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::MyControls.Properties.Resources.指示灯图片;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DoubleBuffered = true;
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.MaximumSize = new System.Drawing.Size(200, 200);
            this.MinimumSize = new System.Drawing.Size(40, 40);
            this.Name = "DefDisplay";
            this.Size = new System.Drawing.Size(200, 200);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
