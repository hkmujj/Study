using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CBTC.Infrasturcture.Model.Send;
using CBTC.Infrasturcture.Model.Test;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.CBTC.BeiJiaoKong.Constance;
using Subway.CBTC.BeiJiaoKong.Models;
using Subway.CBTC.BeiJiaoKong.ViewModel;

namespace Subway.CBTC.BeiJiaoKong.Views.Root
{
    /// <summary>
    /// DailyTestView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RootRegion)]
    public partial class DailyTestView : UserControl
    {
        public DailyTestView()
        {
            InitializeComponent();
        }

        //private void OnInputWord(string word)
        //{
        //    TestInfo testInfo = ((BeiJiaoKongViewModel)this.DataContext).CBTC.TestInfo;
        //    ISendInterface SendData = ((BeiJiaoKongViewModel) this.DataContext).CBTC.SendInterface;
        //    switch (word)
        //    {
        //        case "试闸":
        //            testInfo.ButtonDownBrakeTest = !testInfo.ButtonDownBrakeTest;
        //            ButtonDownChangeColor(this.BrakeTestButton, testInfo.ButtonDownBrakeTest);
        //            SendData.InputBrakeTestSwitch(new SendModel<bool>(testInfo.ButtonDownBrakeTest));
        //            break;
        //        case "缓解":
        //            testInfo.ButtonDownRemit = !testInfo.ButtonDownRemit;
        //            ButtonDownChangeColor(this.RelaseButton, testInfo.ButtonDownRemit);
        //            SendData.InputBrakeTestRelease(new SendModel<bool>(testInfo.ButtonDownRemit));
        //            break;
        //        case "广播\r测试":
        //            testInfo.ButtonDownBroadcastTest = !testInfo.ButtonDownBroadcastTest;
        //            ButtonDownChangeColor(this.BroadcastTestButton, testInfo.ButtonDownBroadcastTest);
        //            SendData.InputBroadcastTest(new SendModel<bool>(testInfo.ButtonDownBroadcastTest));
        //            break;
        //        case "点灯\r测试":
        //            testInfo.ButtonDownLightTest = !testInfo.ButtonDownLightTest;
        //            ButtonDownChangeColor(this.LightTestButton, testInfo.ButtonDownLightTest);
        //            SendData.InputLampTest(new SendModel<bool>(testInfo.ButtonDownLightTest));
        //            break;
        //    }
        //    GlobalParams.Instance.InitParam.CommunicationDataService.ReadService.RaiseAllDataChanged();
        //}
        //private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        //{
        //    var btn = sender as Button;
        //    if (btn != null)
        //    {
        //        OnInputWord(btn.Content as string);
        //    }
        //}

        ////Button按下背景色转换
        //private void ButtonDownChangeColor(Button btn, bool bButtonDown)
        //{
        //    if (!bButtonDown)
        //    {
        //        btn.Background = new SolidColorBrush(Color.FromArgb(255, 128, 139, 158));
        //    }
        //    else
        //    {
        //        btn.Background = new SolidColorBrush(Color.FromArgb(255, 8, 75, 195));
        //    }
        //}
    }
}
