using System.ComponentModel.Composition;
using DevExpress.Mvvm.Native;
using LightRail.HMI.GZYGDC.Model;
using LightRail.HMI.GZYGDC.Service;
using LightRail.HMI.GZYGDC.ViewModel;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;

namespace LightRail.HMI.GZYGDC.Adapter
{
    [Export(typeof(IUpdateDataProvider))]
    public class UpdateNetTopologyViewDataProvider : UpdateDataProviderBase
    {

        private readonly StationManagerService m_StationManagerService;

        [ImportingConstructor]
        public UpdateNetTopologyViewDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel) : base(eventAggregator, viewModel)
        {
            m_StationManagerService =
                GlobalParam.Instance.InitParam?.DataPackage.ServiceManager.GetService<StationManagerService>();
        }

        public override void Initalize(bool isDebugModel)
        {
            
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var NetTopologyModel = ViewModel.NetTopologyViewModel.Model;


            NetTopologyModel.NetTopologyUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));

        }
    }
}
