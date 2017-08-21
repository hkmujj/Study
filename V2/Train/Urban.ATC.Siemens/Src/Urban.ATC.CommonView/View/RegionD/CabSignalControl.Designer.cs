using Motor.ATP.CommonResource;

namespace Motor.ATP.CommonView.View.RegionD
{
    partial class CabSignalControl
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

            if (m_FlickerTimer != null)
            {
                m_FlickerTimer.Dispose();
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
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // CabSignalControl
            // 
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Image = ATPCommonResource.CabSignal_L;
            this.InitialImage = null;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Size = new System.Drawing.Size(50, 50);
            this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
