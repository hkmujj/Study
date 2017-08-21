using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.WuHanLine6.Events;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Models.Model;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.Controller
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    public class StationController : ControllerBase<StationModel>
    {

        /// <summary>
        /// 
        /// </summary>
        public StationController()
        {
            m_EventAggregator = ServiceLocator.Current.TryResolve<IEventAggregator>();
            Confirm = new DelegateCommand((ConfirmAction));
            SelectStation = new DelegateCommand<string>((SelectStationAction));
            StaionName = new DelegateCommand<string>((StattionName));
        }

        private void StattionName(string s)
        {
            if (m_IsStart)
            {
                ViewModel.NewStartStation = s;
            }
            else
            {
                ViewModel.NewEndStation = s;
            }
        }

        private void SelectStationAction(string s)
        {
            m_EventAggregator.GetEvent<NavigatorKeyEvent>().Publish(StateInterfaceKeys.车站选择);
            m_IsStart = s == "始发站";
        }

        private void ConfirmAction()
        {
            var index = ViewModel.StationManager.GetStationIndex(ViewModel.NewStartStation);
            if (index != 0)
            {
                OutFloatKeys.OutF始发站.SendFloatData(index);
            }
            var index1 = ViewModel.StationManager.GetStationIndex(ViewModel.NewEndStation);
            if (index1 != 0)
            {
                OutFloatKeys.OutF终点站.SendFloatData(index1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ModelChanged()
        {
            ViewModel.Model = ViewModel.IsAuto ? Models.States.StationModel.Auto : Models.States.StationModel.Manual;
            if (ViewModel.IsAuto)
            {
                OutBoolKeys.OutBo自动模式.SendBoolData(true);
                OutBoolKeys.OutBo手动模式.SendBoolData(false);
            }
            else
            {
                OutBoolKeys.OutBo自动模式.SendBoolData(false);
                OutBoolKeys.OutBo手动模式.SendBoolData(true);
            }
        }

        private bool m_IsStart;
        /// <summary>
        /// 
        /// </summary>
        public ICommand StaionName { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public ICommand SelectStation { get; private set; }
        private readonly IEventAggregator m_EventAggregator;
        /// <summary>
        /// 确认
        /// </summary>
        public ICommand Confirm { get; private set; }
    }
}