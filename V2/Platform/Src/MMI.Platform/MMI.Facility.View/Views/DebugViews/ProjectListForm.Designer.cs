namespace MMI.Facility.View.Views.DebugViews
{
    partial class ProjectListForm
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
            this.tabControlProjcetInfo = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // tabControlProjcetInfo
            // 
            this.tabControlProjcetInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlProjcetInfo.Location = new System.Drawing.Point(0, 0);
            this.tabControlProjcetInfo.Name = "tabControlProjcetInfo";
            this.tabControlProjcetInfo.SelectedIndex = 0;
            this.tabControlProjcetInfo.Size = new System.Drawing.Size(517, 473);
            this.tabControlProjcetInfo.TabIndex = 0;
            // 
            // ProjectListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 473);
            this.ControlBox = false;
            this.Controls.Add(this.tabControlProjcetInfo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectListForm";
            this.ShowIcon = false;
            this.Text = "ProjectListForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlProjcetInfo;
    }
}