using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Subway.TCMS.LanZhou.Extension;
using Subway.TCMS.LanZhou.Model.Domain;
using Subway.TCMS.LanZhou.Model.Domain.Constant;
using Subway.TCMS.LanZhou.Resources.Keys;
using Subway.TCMS.LanZhou.ViewModel;

namespace Subway.TCMS.LanZhou.Adapter.UpdateDataProviders
{
    [Export(typeof (IUpdateDataProvider))]
    class TrainModelUpdateDataProvider : UpdateDataProviderBase
    {

        private TrainModel Model
        {
            get { return ViewModel.TrainViewModel.Model; }
        }

        [ImportingConstructor]
        public TrainModelUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel) : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            Model.TouchNetVoltage = DataService.ReadService.GetInFloatOf(InfKeys.Inf运行网压);
            Model.CurrentSpeed = DataService.ReadService.GetInFloatOf(InfKeys.Inf运行速度);

            Model.CarTowBrakePercentData.Status = DataService.ReadService.GetInBoolOf(InbKeys.Inb牵引百分比牵引状态) ? CarTowBrakeStatus.Tow : CarTowBrakeStatus.Brake;
            Model.CarTowBrakePercentData.TowBrakePercentValue = DataService.ReadService.GetInFloatOf(Model.CarTowBrakePercentData.Status == CarTowBrakeStatus.Tow ? InfKeys.Inf牵引百分比 : InfKeys.Inf制动百分比);
        }
        
    }
}