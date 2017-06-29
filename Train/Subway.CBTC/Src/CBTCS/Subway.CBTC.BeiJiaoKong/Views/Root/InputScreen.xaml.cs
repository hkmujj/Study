using System;
using System.Windows.Threading;
using Subway.CBTC.BeiJiaoKong.Interfaces;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.CBTC.BeiJiaoKong.Constance;
using Subway.CBTC.BeiJiaoKong.Models.Domain;
using Subway.CBTC.BeiJiaoKong.ViewModel;


namespace Subway.CBTC.BeiJiaoKong.Views.Root
{
    /// <summary>
    /// InputScreen.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.General)]
    public partial class InputScreen : IInputContent
    {
        private static bool bTimeFlick = false;
        private DispatcherTimer ShowTimer;

        public InputScreen()
        {
            InitializeComponent();

            ShowTimer = new DispatcherTimer();
            ShowTimer.Tick += new EventHandler(ShowCurTimer);
            ShowTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            ShowTimer.Start();
        }
        public void Sunbmit(string sPara)
        {
            ((BeiJiaoKongViewModel)(this.DataContext)).InputScreen.Submit.Execute(sPara);
        }

        public void Input(string sPara)
        {
            ((BeiJiaoKongViewModel)(this.DataContext)).InputScreen.Input.Execute(sPara);
        }

        public void ShowCurTimer(object sender, EventArgs e)
        {
            if (this.DataContext == null)
            {
                return;
            }
            string strTempNumber = ((BeiJiaoKongViewModel) (this.DataContext)).InputScreen.DisplayNumber;

            this.TextFlick.Text = "";
            if (strTempNumber != null)
            {
                if (strTempNumber.Length < (int) DriverNumber.TCTDriverNumber)
                {
                    for (int i = 0; i < strTempNumber.Length; i++)
                    {
                        this.TextFlick.Text += "　";
                    }
                }
                else
                {
                    return;
                }
            }

            if (bTimeFlick)
            {
                this.TextFlick.Text += "－";
            }
            else
            {
                this.TextFlick.Text += "　";
            }

            bTimeFlick = !bTimeFlick;
        }

    }
}
