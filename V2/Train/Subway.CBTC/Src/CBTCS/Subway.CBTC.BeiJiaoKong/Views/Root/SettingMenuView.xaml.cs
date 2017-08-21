using System;
using System.Windows;
using System.Windows.Controls;
using CBTC.Infrasturcture.Model.Others;
using CBTC.Infrasturcture.Model.Send;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.CBTC.BeiJiaoKong.Constance;
using Subway.CBTC.BeiJiaoKong.ViewModel;

namespace Subway.CBTC.BeiJiaoKong.Views.Root
{
    /// <summary>
    /// SettingMenuView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RootRegion)]
    public partial class SettingMenuView : UserControl
    {
        public SettingMenuView()
        {
            InitializeComponent();
        }

        private void OnInputWord(string word)
        {
            Other Other = ((BeiJiaoKongViewModel)this.DataContext).CBTC.Other;
            ISendInterface SendData = ((BeiJiaoKongViewModel)this.DataContext).CBTC.SendInterface;
            switch (word)
            {
                case "增大":
                    Other.Volumne = Math.Min(Other.Volumne + 10, 100);
                    SendData.InputVolumn(new SendModel<double>(Other.Volumne));
                    break;
                case "减小":
                    Other.Volumne = Math.Max(Other.Volumne + 10, 0);
                    SendData.InputVolumn(new SendModel<double>(Other.Volumne));
                    break;
                case "1级":
                    Other.Light = 40;
                    SendData.InputLight(new SendModel<double>(Other.Light));
                    break;
                case "2级":
                    Other.Light = 60;
                    SendData.InputLight(new SendModel<double>(Other.Light));
                    break;
                case "3级":
                    Other.Light = 80;
                    SendData.InputLight(new SendModel<double>(Other.Light));
                    break;
                case "4级":
                    Other.Light = 100;
                    SendData.InputLight(new SendModel<double>(Other.Light));
                    break;
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                OnInputWord(btn.Content as string);
            }
        }
    }
}
