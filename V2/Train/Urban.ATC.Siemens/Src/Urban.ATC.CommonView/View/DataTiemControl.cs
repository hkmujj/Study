using System;
using Urban.ATC.CommonView.Model;

namespace Urban.ATC.CommonView.View
{
    public partial class DataTiemControl : TextBase
    {
        public DataTiemControl()
        {
            InitializeComponent();
            ChangeTextColor(GDICommon.LightGreyColor);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var tmp = string.Format("{0}:{1}:{2}", DateTime.Now.Hour.ToString("00"), DateTime.Now.Minute.ToString("00"),
                DateTime.Now.Second.ToString("00"));
            ChangeText(tmp);
        }
    }
}