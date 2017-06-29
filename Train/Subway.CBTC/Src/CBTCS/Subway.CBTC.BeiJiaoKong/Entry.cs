using CBTC.Infrasturcture.Model.Constant;
using CBTC.Infrasturcture.Service;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Subway.CBTC.BeiJiaoKong.Models;
using Subway.CBTC.BeiJiaoKong.ViewModel;
using Subway.CBTC.BeiJiaoKong.Views.App;
using CBTC.DataAdapter;

namespace Subway.CBTC.BeiJiaoKong
{
    /// <summary>
    /// 创 建 者：谭栋炜
    /// 创建时间：2017/1/16
    /// 修 改 者：谭栋炜
    /// 修改时间：2017/1/16
    /// </summary>
    [SubsystemExport(typeof(Entry))]
    public class Entry : ISubsystem
    {
        /// <summary/>
        /// <param name="initParam"/>
        public void Initalize(SubsystemInitParam initParam)
        {
            GlobalParams.Instance.Initialize(initParam);

            InitalizeServices(initParam);

            var viewService = initParam.DataPackage.ServiceManager.GetService<IViewService>();
            var frm = new WpfHostForm(initParam);
            viewService.Regist(frm.AppName, frm);
            viewService.Active(frm.AppName);
            var domainVm = ServiceLocator.Current.GetInstance<BeiJiaoKongViewModel>();
            var adapter =
                DataAdapterFactory.Instance.CreateDataAdapter(domainVm.CBTC);
            initParam.DataPackage.ServiceManager.GetService<IDataChangeListenService>()
                .RegistListener(adapter, initParam.AppConfig);

            GlobalParams.Instance.InitParam.CommunicationDataService.ReadService.RaiseAllDataChanged();
        }

        private void InitalizeServices(SubsystemInitParam initParam)
        {
            var serviceManager = initParam.DataPackage.ServiceManager;

            var signalType = SignalType.TCT.ToString();

            var interfaceService = new InterfaceAdapterService();
            interfaceService.Initalize(
                initParam.DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(
                    new CommunicationDataKey(initParam.AppConfig)));

            serviceManager.RegistService<IInterfaceAdapterService>(signalType, interfaceService);
        }
    }
}