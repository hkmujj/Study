using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Motor.TCMS.CRH400BF.Extension;
using Motor.TCMS.CRH400BF.Model.Train;
using Motor.TCMS.CRH400BF.Resources.Keys;

namespace Motor.TCMS.CRH400BF.Adapter.UpdateDataProviders
{


    [Export(typeof (IUpdateDataProvider))]
    internal class CarEmptySpringPressUpDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarEmptySpringPressUpDataProvider(IEventAggregator eventAggregator, TrainModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {

            var d = TrainModel;

            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车1空簧1压力数值, f => d.CarCollection[0].BrakeInfoPage.Value.EmptySpring1Press.State = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车2空簧1压力数值, f => d.CarCollection[1].BrakeInfoPage.Value.EmptySpring1Press.State = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车3空簧1压力数值, f => d.CarCollection[2].BrakeInfoPage.Value.EmptySpring1Press.State = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车4空簧1压力数值, f => d.CarCollection[3].BrakeInfoPage.Value.EmptySpring1Press.State = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车5空簧1压力数值, f => d.CarCollection[4].BrakeInfoPage.Value.EmptySpring1Press.State = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车6空簧1压力数值, f => d.CarCollection[5].BrakeInfoPage.Value.EmptySpring1Press.State = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车7空簧1压力数值, f => d.CarCollection[6].BrakeInfoPage.Value.EmptySpring1Press.State = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车8空簧1压力数值, f => d.CarCollection[7].BrakeInfoPage.Value.EmptySpring1Press.State = f);

            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车1空簧2压力数值, f => d.CarCollection[0].BrakeInfoPage.Value.EmptySpring2Press.State = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车2空簧2压力数值, f => d.CarCollection[1].BrakeInfoPage.Value.EmptySpring2Press.State = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车3空簧2压力数值, f => d.CarCollection[2].BrakeInfoPage.Value.EmptySpring2Press.State = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车4空簧2压力数值, f => d.CarCollection[3].BrakeInfoPage.Value.EmptySpring2Press.State = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车5空簧2压力数值, f => d.CarCollection[4].BrakeInfoPage.Value.EmptySpring2Press.State = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车6空簧2压力数值, f => d.CarCollection[5].BrakeInfoPage.Value.EmptySpring2Press.State = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车7空簧2压力数值, f => d.CarCollection[6].BrakeInfoPage.Value.EmptySpring2Press.State = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf车8空簧2压力数值, f => d.CarCollection[7].BrakeInfoPage.Value.EmptySpring2Press.State = f);
        }
    }
}
