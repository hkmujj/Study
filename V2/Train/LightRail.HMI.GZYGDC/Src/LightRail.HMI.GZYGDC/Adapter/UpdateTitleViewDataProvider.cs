using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using LightRail.HMI.GZYGDC.Extension;
using LightRail.HMI.GZYGDC.Model;
using LightRail.HMI.GZYGDC.Resources.Keys;
using LightRail.HMI.GZYGDC.Service;
using LightRail.HMI.GZYGDC.ViewModel;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;

namespace LightRail.HMI.GZYGDC.Adapter
{
    [Export(typeof(IUpdateDataProvider))]
    public class UpdateTitleViewDataProvider : UpdateDataProviderBase
    {

        private readonly StationManagerService m_StationManagerService;

        [ImportingConstructor]
        public UpdateTitleViewDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel) : base(eventAggregator, viewModel)
        {
            m_StationManagerService =
                GlobalParam.Instance.InitParam?.DataPackage.ServiceManager.GetService<StationManagerService>();
        }

        public override void Initalize(bool isDebugModel)
        {
            
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var TitleModel = ViewModel.TitleViewModel.Model;

            args.ChangedFloats.UpdateIfContains(InFloatKeys.下一站, f => TitleModel.NextStation = m_StationManagerService.GetStation((int)f)?.Name);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.终点站, f => TitleModel.EndStation = m_StationManagerService.GetStation((int)f)?.Name);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.列车号, f => TitleModel.TrainNum = string.Format("T {0:D3}", Convert.ToInt32(f)));
            args.ChangedFloats.UpdateIfContains(InFloatKeys.电流, f => TitleModel.ElectricalCurrent = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.电压, f => TitleModel.Voltage = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.速度, f => TitleModel.Speed = f);
        }
    }
}
