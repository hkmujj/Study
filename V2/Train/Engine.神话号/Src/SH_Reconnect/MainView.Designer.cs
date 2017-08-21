namespace SH_Reconnect
{
    partial class MainView
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
            this._viewParent = new CommonControls.GridVisibleTableLayoutPanel();
            this.SuspendLayout();
            // 
            // _viewParent
            // 
            this._viewParent.BorderColor = System.Drawing.Color.Empty;
            this._viewParent.ColumnCount = 1;
            this._viewParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._viewParent.Location = new System.Drawing.Point(0, 0);
            this._viewParent.Margin = new System.Windows.Forms.Padding(0);
            this._viewParent.Name = "_viewParent";
            this._viewParent.RowCount = 1;
            this._viewParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._viewParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 596F));
            this._viewParent.Size = new System.Drawing.Size(796, 596);
            this._viewParent.TabIndex = 15;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(900, 800);
            this.Controls.Add(this._viewParent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainView";
            this.Text = "重联屏";
            this.ResumeLayout(false);

        }

        #endregion

        private CommonControls.GridVisibleTableLayoutPanel _viewParent;
    }
}