using System.ComponentModel.Composition;
using LightRail.HMI.SZLHLF.ViewModel;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using LightRail.HMI.SZLHLF.Resources.Keys;
using LightRail.HMI.SZLHLF.Extension;
using LightRail.HMI.SZLHLF.Model;
using LightRail.HMI.SZLHLF.Service;

namespace LightRail.HMI.SZLHLF.Adapter
{
    [Export(typeof(IUpdateDataProvider))]
    public class UpdateOtherViewDataProvider : UpdateDataProviderBase
    {
        private readonly StationManagerService m_StationManagerService;

        [ImportingConstructor]
        public UpdateOtherViewDataProvider(IEventAggregator eventAggregator, SZLHLFViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
            if (GlobalParam.Instance.InitParam != null)
                m_StationManagerService =
                    GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<StationManagerService>();
        }

        public override void Initalize(bool isDebugModel)
        {

        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var OtherModel = ViewModel.OtherViewModel.Model;

            args.ChangedFloats.UpdateIfContains(InFolatKey.下一站, f => OtherModel.NextStation = m_StationManagerService.GetStation((int)f) != null ? m_StationManagerService.GetStation((int)f).Name : null);
            args.ChangedFloats.UpdateIfContains(InFolatKey.终点站, f => OtherModel.EndStation = m_StationManagerService.GetStation((int)f) != null ? m_StationManagerService.GetStation((int)f).Name : null);
            args.ChangedFloats.UpdateIfContains(InFolatKey.电流, f => OtherModel.Electricity = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.电压, f => OtherModel.Voltage = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.速度, f => OtherModel.Speed = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.列车号, f => OtherModel.TrainNum = f);

            args.ChangedBools.UpdateIfContains(InBoolKey.黑屏, b => OtherModel.HMIBlack = b);
        }
    }
}
