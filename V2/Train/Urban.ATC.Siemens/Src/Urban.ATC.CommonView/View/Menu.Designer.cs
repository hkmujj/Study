using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Urban.ATC.Common.Control
{
    partial class Menu
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
            this.SuspendLayout();
 
            this.Font = new Font("宋体", 13.5F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new Padding(4, 4, 4, 4);
            this.Name = "Menu";
            this.Size = new Size(250, 225);
            this.ResumeLayout(false);

        }

        #endregion

    }
}
