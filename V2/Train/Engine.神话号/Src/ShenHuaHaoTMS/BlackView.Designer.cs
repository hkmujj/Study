using System.Drawing;
using CommonControls;

namespace ShenHuaHaoTMS
{
    partial class BlackView
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
            this.button1 = new System.Windows.Forms.Button();
            this._panel = new CommonControls.GridVisibleTableLayoutPanel();
            this._viewParent = new CommonControls.GridVisibleTableLayoutPanel();
            this._panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            // 
            // _panel
            // 
            this._panel.BorderColor = System.Drawing.Color.Empty;
            this._panel.ColumnCount = 1;
            this._panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._panel.Controls.Add(this._viewParent, 0, 0);
            this._panel.Location = new System.Drawing.Point(2, 2);
            this._panel.Name = "_panel";
            this._panel.RowCount = 1;
            this._panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 596F));
            this._panel.Size = new System.Drawing.Size(796, 596);
            this._panel.TabIndex = 17;
            // 
            // _viewParent
            // 
            this._viewParent.BorderColor = System.Drawing.Color.Empty;
            this._viewParent.ColumnCount = 1;
            this._viewParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._viewParent.Cursor = System.Windows.Forms.Cursors.Default;
            this._viewParent.Location = new System.Drawing.Point(0, 0);
            this._viewParent.Margin = new System.Windows.Forms.Padding(0);
            this._viewParent.Name = "_viewParent";
            this._viewParent.RowCount = 1;
            this._viewParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._viewParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 596F));
            this._viewParent.Size = new System.Drawing.Size(796, 596);
            this._viewParent.TabIndex = 14;
            // 
            // BlackView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this._panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BlackView";
            this.Text = "车辆屏";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BlackView_FormClosing);
            this._panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private CommonControls.GridVisibleTableLayoutPanel gridVisibleTableLayoutPanel1;
        private CommonControls.GridVisibleTableLayoutPanel _viewParent;
        private CommonControls.GridVisibleTableLayoutPanel _panel;
    }
}
