using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Motor.TCMS.CRH400BF.Extension;
using Motor.TCMS.CRH400BF.Model.Constant;
using Motor.TCMS.CRH400BF.Model.Train;
using Motor.TCMS.CRH400BF.Resources.Keys;

namespace Motor.TCMS.CRH400BF.Adapter.UpdateDataProviders
{
  
    [Export(typeof(IUpdateDataProvider))]
    public class TrainTowBrakePrecentUpDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public TrainTowBrakePrecentUpDataProvider(IEventAggregator eventAggregator, TrainModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf2车实际制动百分比 , f => TrainModel.CarCollection[1].BrakePercentNow = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf2车实际牵引百分比, f => TrainModel.CarCollection[1].TowPercentNow = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf4车实际制动百分比, f => TrainModel.CarCollection[3].BrakePercentNow = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf4车实际牵引百分比, f => TrainModel.CarCollection[3].TowPercentNow = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf5车实际制动百分比, f => TrainModel.CarCollection[4].BrakePercentNow = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf5车实际牵引百分比, f => TrainModel.CarCollection[4].TowPercentNow = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf7车实际制动百分比, f => TrainModel.CarCollection[6].BrakePercentNow = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf7车实际牵引百分比, f => TrainModel.CarCollection[6].TowPercentNow = f);


            args.ChangedFloats.UpdateIfContains(InfKeys.Inf2车设定制动百分比, f => TrainModel.CarCollection[1].BrakePercentSet = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf2车设定牵引百分比, f => TrainModel.CarCollection[1].TowPercentSet = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf4车设定制动百分比, f => TrainModel.CarCollection[3].BrakePercentSet = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf4车设定牵引百分比, f => TrainModel.CarCollection[3].TowPercentSet = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf5车设定制动百分比, f => TrainModel.CarCollection[4].BrakePercentSet = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf5车设定牵引百分比, f => TrainModel.CarCollection[4].TowPercentSet = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf7车设定制动百分比, f => TrainModel.CarCollection[6].BrakePercentSet = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf7车设定牵引百分比, f => TrainModel.CarCollection[6].TowPercentSet = f);

            var d = TrainModel;
            d.TrainWorkConditionState = GetTrainWorkConditionState();
            d.CarCollection[1].ShowPercentNow = GetShowOnePercentNow();
            d.CarCollection[3].ShowPercentNow = GetShowThreePercentNow();
            d.CarCollection[4].ShowPercentNow = GetShowFourPercentNow();
            d.CarCollection[6].ShowPercentNow = GetShowSixPercentNow();
           
        }

        private float GetShowOnePercentNow()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb列车工况制动))
            {
                return TrainModel.CarCollection[1].BrakePercentNow;
            }
            return TrainModel.CarCollection[1].TowPercentNow;
        }

        private float GetShowThreePercentNow()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb列车工况制动))
            {
                return TrainModel.CarCollection[3].BrakePercentNow;
            }
            return TrainModel.CarCollection[3].TowPercentNow;
        }

        private float GetShowFourPercentNow()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb列车工况制动))
            {
                return TrainModel.CarCollection[4].BrakePercentNow;
            }
            return TrainModel.CarCollection[4].TowPercentNow;
        }

        private float GetShowSixPercentNow()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb列车工况制动))
            {
                return TrainModel.CarCollection[6].BrakePercentNow;
            }
            return TrainModel.CarCollection[6].TowPercentNow;
        }

        private TrainWorkConditionState GetTrainWorkConditionState()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb列车工况制动))
            {
                return TrainWorkConditionState.Brake;
            }
            return TrainWorkConditionState.Tow;
        }

    }
}
