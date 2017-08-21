using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.ShenZhenLine9.Extention;
using Subway.ShenZhenLine9.Resource.Keys;
using Subway.ShenZhenLine9.Envets;
using Subway.ShenZhenLine9.ViewModels;

namespace Subway.ShenZhenLine9.Controller.ViewModelController
{
    /// <summary>
    /// 站点控制
    /// </summary>
    [Export]
    public class StationController : ControllerBase<StationViewModel>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public StationController()
        {
            ChangedStation = new DelegateCommand<string>((ChangedStationMethod));
            Confirm = new DelegateCommand((ConfirmData));
            ConfirmStation = new DelegateCommand((ConfirmStationMethod));
        }

        private void ConfirmStationMethod()
        {
            var station = ViewModel.AllStation.FirstOrDefault(f => f.IsChecked);
            if (station != null)
            {
                if (m_Index == 1)
                {
                    ViewModel.StartStation = station.Name;
                }
                else if (m_Index == 2)
                {
                    ViewModel.NextStation = station.Name;
                }
                else if (m_Index == 3)
                {
                    ViewModel.EndStation = station.Name;
                }

                ViewModel.AllStation.ForEach(f => f.IsChecked = false);
            }
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToKeyEvent>().Publish(StateKeys.广播);
        }

        private void ConfirmData()
        {
            var start = ViewModel.StationManagerService.GetStation(ViewModel.StartStation);
            var next = ViewModel.StationManagerService.GetStation(ViewModel.NextStation);
            var end = ViewModel.StationManagerService.GetStation(ViewModel.EndStation);
            OutFloatKeys.手动始发站.SendFloatValue(start?.Index ?? 0);
            OutFloatKeys.手动下一站.SendFloatValue(next?.Index ?? 0);
            OutFloatKeys.手动终点站.SendFloatValue(end?.Index ?? 0);
        }

        private void ChangedStationMethod(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return;
            }
            int.TryParse(s, out m_Index);
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToKeyEvent>().Publish(StateKeys.站点设置);
        }

        /// <summary>
        /// 模式更改
        /// </summary>
        public void ModelChanged(int index)
        {
            if (index == 1)//自动
            {
                OutBoolKeys.广播模式自动.SendBoolValue(true);
                OutBoolKeys.广播模式手动.SendBoolValue(false);
                OutBoolKeys.广播模式库内报站.SendBoolValue(false);
            }
            else if (index == 2)//手动
            {
                OutBoolKeys.广播模式自动.SendBoolValue(false);
                OutBoolKeys.广播模式手动.SendBoolValue(true);
                OutBoolKeys.广播模式库内报站.SendBoolValue(false);
            }
            else if (index == 3)//库内
            {
                OutBoolKeys.广播模式自动.SendBoolValue(false);
                OutBoolKeys.广播模式手动.SendBoolValue(false);
                OutBoolKeys.广播模式库内报站.SendBoolValue(true);
            }
        }
        // 1 起始 2 下一站 3 终点站
        private int m_Index = 0;
        /// <summary>
        /// 确认
        /// </summary>
        public ICommand Confirm { get; private set; }
        /// <summary>
        /// 站点选择确认
        /// </summary>
        public ICommand ConfirmStation { get; private set; }
        /// <summary>
        /// 改变站点
        /// </summary>
        public ICommand ChangedStation { get; private set; }
    }
}