namespace ShenHuaHaoTMS
{
    partial class NoBodyVigilantView
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
            this.defLabel1 = new CommonControls.DefLabel();
            this.defLabel2 = new CommonControls.DefLabel();
            this.SuspendLayout();
            // 
            // defLabel1
            // 
            this.defLabel1.BorderColor = System.Drawing.Color.Transparent;
            this.defLabel1.BorderWidth = 1F;
            this.defLabel1.DefText = "警惕";
            this.defLabel1.DefUnit = "";
            this.defLabel1.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold);
            this.defLabel1.FontBrush = System.Drawing.Color.Red;
            this.defLabel1.Horizontal = CommonControls.Alignment.Center;
            this.defLabel1.Location = new System.Drawing.Point(109, 25);
            this.defLabel1.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.defLabel1.Name = "defLabel1";
            this.defLabel1.ObliqueLine = false;
            this.defLabel1.Size = new System.Drawing.Size(121, 46);
            this.defLabel1.TabIndex = 2;
            this.defLabel1.Vertical = CommonControls.Alignment.Center;
            this.defLabel1.XOffset = 0F;
            this.defLabel1.YOffset = 0F;
            // 
            // defLabel2
            // 
            this.defLabel2.BorderColor = System.Drawing.Color.Transparent;
            this.defLabel2.BorderWidth = 1F;
            this.defLabel2.DefText = "15";
            this.defLabel2.DefUnit = "";
            this.defLabel2.Font = new System.Drawing.Font("宋体", 60F, System.Drawing.FontStyle.Bold);
            this.defLabel2.FontBrush = System.Drawing.Color.Red;
            this.defLabel2.Horizontal = CommonControls.Alignment.Center;
            this.defLabel2.Location = new System.Drawing.Point(27, 69);
            this.defLabel2.Margin = new System.Windows.Forms.Padding(18, 19, 18, 19);
            this.defLabel2.Name = "defLabel2";
            this.defLabel2.ObliqueLine = false;
            this.defLabel2.Size = new System.Drawing.Size(276, 112);
            this.defLabel2.TabIndex = 3;
            this.defLabel2.Vertical = CommonControls.Alignment.Center;
            this.defLabel2.XOffset = 0F;
            this.defLabel2.YOffset = 0F;
            // 
            // NoBodyVigilantView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Yellow;
            this.ClientSize = new System.Drawing.Size(341, 211);
            this.Controls.Add(this.defLabel2);
            this.Controls.Add(this.defLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NoBodyVigilantView";
            this.Text = "NoBodyVigilantView";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NoBodyVigilantView_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private CommonControls.DefLabel defLabel1;
        private CommonControls.DefLabel defLabel2;


    }
}