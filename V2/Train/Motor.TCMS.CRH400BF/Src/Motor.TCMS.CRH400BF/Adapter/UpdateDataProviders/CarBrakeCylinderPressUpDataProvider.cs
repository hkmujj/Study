using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Motor.TCMS.CRH400BF.Extension;
using Motor.TCMS.CRH400BF.Model.Train;
using Motor.TCMS.CRH400BF.Resources.Keys;

namespace Motor.TCMS.CRH400BF.Adapter.UpdateDataProviders
{

    [Export(typeof(IUpdateDataProvider))]
    internal class CarBrakeCylinderPressUpDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarBrakeCylinderPressUpDataProvider(IEventAggregator eventAggregator, TrainModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = TrainModel;

            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车1制动缸压力数值, f => d.CarCollection[0].BrakeInfoPage.Value.BrakeCylinderPress.State = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车2制动缸压力数值, f => d.CarCollection[1].BrakeInfoPage.Value.BrakeCylinderPress.State = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车3制动缸压力数值, f => d.CarCollection[2].BrakeInfoPage.Value.BrakeCylinderPress.State = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车4制动缸压力数值, f => d.CarCollection[3].BrakeInfoPage.Value.BrakeCylinderPress.State = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车5制动缸压力数值, f => d.CarCollection[4].BrakeInfoPage.Value.BrakeCylinderPress.State = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车6制动缸压力数值, f => d.CarCollection[5].BrakeInfoPage.Value.BrakeCylinderPress.State = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车7制动缸压力数值, f => d.CarCollection[6].BrakeInfoPage.Value.BrakeCylinderPress.State = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车8制动缸压力数值, f => d.CarCollection[7].BrakeInfoPage.Value.BrakeCylinderPress.State = f);
        }
    }
}
