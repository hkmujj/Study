namespace MMI.Facility.View.Views.DebugViews
{
    partial class ObjectAttribForm
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
            this.tabControlChildAttribute = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // tabControlChildAttribute
            // 
            this.tabControlChildAttribute.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlChildAttribute.Location = new System.Drawing.Point(12, 12);
            this.tabControlChildAttribute.Name = "tabControlChildAttribute";
            this.tabControlChildAttribute.SelectedIndex = 0;
            this.tabControlChildAttribute.Size = new System.Drawing.Size(341, 431);
            this.tabControlChildAttribute.TabIndex = 0;
            // 
            // ObjectAttribForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 455);
            this.Controls.Add(this.tabControlChildAttribute);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ObjectAttribForm";
            this.Text = "UIObject attribute";
            this.Load += new System.EventHandler(this.ObjectAttribForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlChildAttribute;

    }
}