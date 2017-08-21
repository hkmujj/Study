using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using DevExpress.Mvvm.Native;
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
    public class UpdateEventInfoViewDataProvider : UpdateDataProviderBase
    {

        //private readonly EventManagerService m_EventManagerService;

        [ImportingConstructor]
        public UpdateEventInfoViewDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel) : base(eventAggregator, viewModel)
        {
            //m_EventManagerService =
            //    GlobalParam.Instance.InitParam?.DataPackage.ServiceManager.GetService<EventManagerService>();
        }

        public override void Initalize(bool isDebugModel)
        {
            
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var EventInfoModel = ViewModel.EventInfoViewModel.Model;

            args.ChangedBools.NewValue.Where(b =>b.Value).ForEach(b => EventInfoModel.EventManagerService.HappenFault(b.Key));

        }
    }
}
