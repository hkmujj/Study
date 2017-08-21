using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Urban.ATC.CommonView.View
{
    partial class TestControl
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
            this.components = new Container();
            this.flowLayoutPanel1 = new FlowLayoutPanel();
            this.label1 = new Label();
            this.label2 = new Label();
            this.m_Timer = new Timer(this.components);
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Dock = DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new Size(234, 96);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 21F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new Point(10, 10);
            this.label1.Margin = new Padding(10, 10, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0, 28);
            this.label1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new Font("宋体", 13.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new Point(10, 38);
            this.label2.Margin = new Padding(10, 0, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0, 18);
            this.label2.TabIndex = 1;
            // 
            // m_Timer
            // 
            this.m_Timer.Interval = 1000;
            this.m_Timer.Tick += new EventHandler(this.m_Timer_Tick);
            // 
            // TestControl
            // 
            this.AutoScaleDimensions = new SizeF(6F, 12F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "TestControl";
            this.Size = new Size(234, 96);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private Label label2;
        private Timer m_Timer;
    }
}
