using System;
using System.ComponentModel;
using System.Windows.Forms;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.CommonView.View.RegionE
{
    public partial class DateTimeControl : UserControl
    {
        private IATP m_ATP;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public IATP ATP
        {
            set
            {
                m_ATP = value;
                tableLayoutPanel1.AddOpuaqueLayer(ATP.Other);
                dateLable.AddOpuaqueLayer(ATP.Other);
                constInfoLab.AddOpuaqueLayer(ATP.Other);
                timeLable.AddOpuaqueLayer(ATP.Other);
                Invalidate();
            }
            get { return m_ATP; }
        }

        public DateTimeControl()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            var timer = new Timer { Interval = 1000, Enabled = true};
            timer.Tick += TimerOnTick;
            timer.Start();
            components  = new Container();
            components.Add(timer);
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            UpdateDateTime();
        }

        private void UpdateDateTime()
        {
            if (m_ATP != null)
            {
                var now = m_ATP.Other.ShowingDateTime;
                dateLable.Text = now.Date.ToString("yy-MM-dd");
                timeLable.Text = now.ToString("HH:mm:ss");
                
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            constInfoLab.Font = Font;
            dateLable.Font = Font;
            timeLable.Font = Font;
            base.OnFontChanged(e);

            Invalidate();
        }


        protected override void OnBackColorChanged(EventArgs e)
        {
            constInfoLab.BackColor = BackColor;
            dateLable.BackColor = BackColor;
            timeLable.BackColor = BackColor;

            base.OnBackColorChanged(e);

            Invalidate();
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            constInfoLab.ForeColor = ForeColor;
            dateLable.ForeColor = ForeColor;
            timeLable.ForeColor = ForeColor;

            base.OnForeColorChanged(e);

            Invalidate();
        }
    }
}
