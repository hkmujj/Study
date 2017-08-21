using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Motor.TCMS.CRH400BF.Extension;
using Motor.TCMS.CRH400BF.Model.Train;
using Motor.TCMS.CRH400BF.Resources.Keys;

namespace Motor.TCMS.CRH400BF.Adapter.UpdateDataProviders
{
    [Export(typeof(IUpdateDataProvider))]
    internal class CarParkingCylinderPressUpDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarParkingCylinderPressUpDataProvider(IEventAggregator eventAggregator, TrainModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {

            var d = TrainModel;

            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车1停放缸压力数值, f => d.CarCollection[0].BrakeInfoPage.Value.ParkingCylinderPress.State = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车3停放缸压力数值, f => d.CarCollection[2].BrakeInfoPage.Value.ParkingCylinderPress.State = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车6停放缸压力数值, f => d.CarCollection[5].BrakeInfoPage.Value.ParkingCylinderPress.State = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车8停放缸压力数值, f => d.CarCollection[7].BrakeInfoPage.Value.ParkingCylinderPress.State = f);
        }
    }
}
