using System.Windows;
using LightRail.HMI.SZLHLF.Constant;
using MMI.Facility.WPFInfrastructure.Behaviors;
using System.Windows.Controls;
using System.Windows.Media;
using LightRail.HMI.SZLHLF.Extension;
using LightRail.HMI.SZLHLF.Model;
using LightRail.HMI.SZLHLF.Resources.Keys;

namespace LightRail.HMI.SZLHLF.View.Common
{
    /// <summary>
    /// BroadcastControlView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentUpContent)]
    public partial class BroadcastControlView : UserControl
    {
        public BroadcastControlView()
        {
            InitializeComponent();
        }

        private void OnInputWord(string word)
        {
            //EnmergencyTalkModel EnmergencyTalkModel = ((SZLHLFViewModel)this.DataContext).TrainInfoViewModel.EnmergencyTalkModel;
            switch (word)
            {
                case "上一站":
                    //EnmergencyTalkModel.LastStaion = !EnmergencyTalkModel.LastStaion;
                    //ButtonDownChangeColor(this.LastStation, EnmergencyTalkModel.LastStaion);
                    OutBoolKey.上一站.SendBoolData(true,true);
                    break;
                case "下一站":
                    //EnmergencyTalkModel.NextStation = !EnmergencyTalkModel.NextStation;
                    //ButtonDownChangeColor(this.NextStation, EnmergencyTalkModel.NextStation);
                    OutBoolKey.下一站.SendBoolData(true, true);
                    break;
                case "进站广播":
                    //EnmergencyTalkModel.ArriveStationBroadcast = !EnmergencyTalkModel.ArriveStationBroadcast;
                    //ButtonDownChangeColor(this.ArriveStationBroadcast, EnmergencyTalkModel.ArriveStationBroadcast);
                    OutBoolKey.进站广播.SendBoolData(true, true);
                    break;
                case "离站广播":
                    //EnmergencyTalkModel.LeaveStationBroadcast = !EnmergencyTalkModel.LeaveStationBroadcast;
                    //ButtonDownChangeColor(this.LeaveStationBroadcast, EnmergencyTalkModel.LeaveStationBroadcast);
                    OutBoolKey.离站广播.SendBoolData(true, true);
                    break;
                case "报站取消":
                    //EnmergencyTalkModel.CancelStationBroadcast = !EnmergencyTalkModel.CancelStationBroadcast;
                    //ButtonDownChangeColor(this.CancelStationBroadcast, EnmergencyTalkModel.CancelStationBroadcast);
                    OutBoolKey.报站取消.SendBoolData(true, true);
                    break;
            }
            if (GlobalParam.Instance.InitParam != null)
            {
                GlobalParam.Instance.InitParam.CommunicationDataService.ReadService.RaiseAllDataChanged();
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

        //Button按下背景色转换
        private void ButtonDownChangeColor(Button btn, bool bButtonDown)
        {
            if (bButtonDown)
            {
                btn.Background = new SolidColorBrush(Color.FromArgb(255, 128, 139, 158));
            }
            else
            {
                btn.Background = new SolidColorBrush(Color.FromArgb(255, 0, 85, 127));
            }
        }
    }
}
