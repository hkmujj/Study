using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using DevExpress.Mvvm.Native;
using LightRail.HMI.GZYGDC.Extension;
using LightRail.HMI.GZYGDC.Model;
using LightRail.HMI.GZYGDC.Model.State;
using LightRail.HMI.GZYGDC.Resources.Keys;
using LightRail.HMI.GZYGDC.Service;
using LightRail.HMI.GZYGDC.ViewModel;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;

namespace LightRail.HMI.GZYGDC.Adapter
{
    [Export(typeof(IUpdateDataProvider))]
    public class UpdateBroadcastControlViewDataProvider : UpdateDataProviderBase
    {

        private readonly StationManagerService m_StationManagerService;

        [ImportingConstructor]
        public UpdateBroadcastControlViewDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel) : base(eventAggregator, viewModel)
        {
            m_StationManagerService =
                GlobalParam.Instance.InitParam?.DataPackage.ServiceManager.GetService<StationManagerService>();
        }

        public override void Initalize(bool isDebugModel)
        {
            
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var BroadcastControlModel = ViewModel.BroadcastControlViewModel.Model;


            BroadcastControlModel.EnmergencyTalkUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));

        }
    }
}
