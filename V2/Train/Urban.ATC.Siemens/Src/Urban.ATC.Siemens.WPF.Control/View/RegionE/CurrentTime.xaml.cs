using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Urban.ATC.Siemens.WPF.Control.View.RegionE
{
    /// <summary>
    /// CurrentTime.xaml 的交互逻辑
    /// </summary>
    public partial class CurrentTime : UserControl
    {
        private DispatcherTimer m_Timer;

        public CurrentTime()
        {
            InitializeComponent();
            m_Timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1) };
            m_Timer.Tick += m_Timer_Tick;
            m_Timer.Start();
        }

        private void m_Timer_Tick(object sender, EventArgs e)
        {
            this.LblTime.Text = string.Format("{0}:{1}:{2}", DateTime.Now.Hour.ToString().PadLeft(2, '0'), DateTime.Now.Minute.ToString().PadLeft(2, '0'),
                DateTime.Now.Second.ToString().PadLeft(2, '0'));
        }
    }
}