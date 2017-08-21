using System.Windows;
using LightRail.HMI.SZLHLF.Constant;
using MMI.Facility.WPFInfrastructure.Behaviors;
using System.Windows.Controls;
using LightRail.HMI.SZLHLF.Controller;
using LightRail.HMI.SZLHLF.Extension;
using LightRail.HMI.SZLHLF.Model;
using LightRail.HMI.SZLHLF.Resources.Keys;
using LightRail.HMI.SZLHLF.ViewModel;

namespace LightRail.HMI.SZLHLF.View.Common
{
    /// <summary>
    /// EmergencyBroadcastView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentUpContent)]
    public partial class EmergencyBroadcastView : UserControl
    {
        public EmergencyBroadcastView()
        {
            InitializeComponent();
        }

        private void OnInputWord(string word)
        {
            EmergencyBroadcastController EmergencyBroadcastController =
                ((SZLHLFViewModel) this.DataContext).TrainInfoViewModel.EmergencyBroadcastInfoModel.EmergencyBroadcastController;
            switch (word)
            {
                case "LastPage":
                    EmergencyBroadcastController.LastPage();
                    break;
                case "NextPage":
                    EmergencyBroadcastController.NextPage();
                    break;
                case "Stop":
                    OutBoolKey.紧急广播停止.SendBoolData(true, true);
                    OutFolatKey.紧急广播号.SendFloatData(0);
                    break;
            }
            this.CurrentPage.Text = EmergencyBroadcastController.CurPageNum.ToString();
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
                OnInputWord(btn.Name as string);
            }
        }
    }
}
