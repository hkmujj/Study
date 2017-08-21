using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.ShenZhenLine9.Extention;
using Subway.ShenZhenLine9.Resource.Keys;
using Subway.ShenZhenLine9.Envets;
using Subway.ShenZhenLine9.Models;
using Subway.ShenZhenLine9.Service;
using Subway.ShenZhenLine9.ViewModels;

namespace Subway.ShenZhenLine9.Controller.ViewModelController
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    public class BaseInfoController : ControllerBase<BaseInfoViewModel>
    {
        private readonly StationManagerService m_StationManagerService;
        /// <summary>
        /// 
        /// </summary>
        public BaseInfoController()
        {
            ServiceLocator.Current.GetInstance<IEventAggregator>()
                .GetEvent<FloatDataChangedEvent>()
                .Subscribe((FloatChanged));
            m_StationManagerService = GlobalParam.Instance.InitParam?.DataPackage.ServiceManager.GetService<StationManagerService>();
        }

        private void FloatChanged(DataChangedEventArgs<float> dataChangedEventArgs)
        {
            dataChangedEventArgs.Data.UpdateIfContain(InFloatKeys.当前站, f => ViewModel.CurrenStation = m_StationManagerService.GetStation((int)f)?.Name);
            dataChangedEventArgs.Data.UpdateIfContain(InFloatKeys.下一站, f => ViewModel.NextStation = m_StationManagerService.GetStation((int)f)?.Name);
            dataChangedEventArgs.Data.UpdateIfContain(InFloatKeys.终点站, f => ViewModel.EndStation = m_StationManagerService.GetStation((int)f)?.Name);
            dataChangedEventArgs.Data.UpdateIfContain(InFloatKeys.网压, f => ViewModel.NetPress = f.ToString("F0") + "V");
            dataChangedEventArgs.Data.UpdateIfContain(InFloatKeys.蓄电池电压, f => ViewModel.Storage = f.ToString("F0") + "V");
            dataChangedEventArgs.Data.UpdateIfContain(InFloatKeys.主风管压力, f => ViewModel.MasterPress = f.ToString("F0") + "KPa");
        }
    }
}