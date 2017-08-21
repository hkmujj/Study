namespace MMI.Facility.View.Views.DebugViews
{
    partial class MMILogicForm
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
            this.tabCtlLogicView = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabCtlLogicView.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCtlLogicView
            // 
            this.tabCtlLogicView.Controls.Add(this.tabPage1);
            this.tabCtlLogicView.Location = new System.Drawing.Point(13, 12);
            this.tabCtlLogicView.Name = "tabCtlLogicView";
            this.tabCtlLogicView.SelectedIndex = 0;
            this.tabCtlLogicView.Size = new System.Drawing.Size(595, 232);
            this.tabCtlLogicView.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(587, 206);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // MMILogicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 273);
            this.Controls.Add(this.tabCtlLogicView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MMILogicForm";
            this.Text = "逻辑设置";
            this.Load += new System.EventHandler(this.MMILogicForm_Load);
            this.tabCtlLogicView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabCtlLogicView;
        private System.Windows.Forms.TabPage tabPage1;
    }
}