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
    public class UpdateAirConditionViewDataProvider : UpdateDataProviderBase
    {

        private readonly StationManagerService m_StationManagerService;

        [ImportingConstructor]
        public UpdateAirConditionViewDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel) : base(eventAggregator, viewModel)
        {
            m_StationManagerService =
                GlobalParam.Instance.InitParam?.DataPackage.ServiceManager.GetService<StationManagerService>();
        }

        public override void Initalize(bool isDebugModel)
        {
            
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var AirConditionModel = ViewModel.AirConditionViewModel.Model;


            AirConditionModel.AirConditionUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));


            //默认为无模式
            AirConditionModel.AirConditionInfoCar1.ConditionMode = AirConditionMode.None;
            AirConditionModel.AirConditionInfoCar2.ConditionMode = AirConditionMode.None;
            AirConditionModel.AirConditionInfoCar3.ConditionMode = AirConditionMode.None;
            AirConditionModel.AirConditionInfoCar4.ConditionMode = AirConditionMode.None;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.车1空调模式_自动模式, () => AirConditionModel.AirConditionInfoCar1.ConditionMode = AirConditionMode.Auto);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.车1空调模式_通风模式, () => AirConditionModel.AirConditionInfoCar1.ConditionMode = AirConditionMode.Ventilation);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.车1空调模式_关闭预冷, () => AirConditionModel.AirConditionInfoCar1.ConditionMode = AirConditionMode.ClosePrecold);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.车1空调模式_测试模式, () => AirConditionModel.AirConditionInfoCar1.ConditionMode = AirConditionMode.Test);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.车1空调模式_火灾模式, () => AirConditionModel.AirConditionInfoCar1.ConditionMode = AirConditionMode.Fire);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.车2空调模式_自动模式, () => AirConditionModel.AirConditionInfoCar2.ConditionMode = AirConditionMode.Auto);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.车2空调模式_通风模式, () => AirConditionModel.AirConditionInfoCar2.ConditionMode = AirConditionMode.Ventilation);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.车2空调模式_关闭预冷, () => AirConditionModel.AirConditionInfoCar2.ConditionMode = AirConditionMode.ClosePrecold);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.车2空调模式_测试模式, () => AirConditionModel.AirConditionInfoCar2.ConditionMode = AirConditionMode.Test);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.车2空调模式_火灾模式, () => AirConditionModel.AirConditionInfoCar2.ConditionMode = AirConditionMode.Fire);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.车3空调模式_自动模式, () => AirConditionModel.AirConditionInfoCar3.ConditionMode = AirConditionMode.Auto);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.车3空调模式_通风模式, () => AirConditionModel.AirConditionInfoCar3.ConditionMode = AirConditionMode.Ventilation);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.车3空调模式_关闭预冷, () => AirConditionModel.AirConditionInfoCar3.ConditionMode = AirConditionMode.ClosePrecold);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.车3空调模式_测试模式, () => AirConditionModel.AirConditionInfoCar3.ConditionMode = AirConditionMode.Test);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.车3空调模式_火灾模式, () => AirConditionModel.AirConditionInfoCar3.ConditionMode = AirConditionMode.Fire);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.车4空调模式_自动模式, () => AirConditionModel.AirConditionInfoCar4.ConditionMode = AirConditionMode.Auto);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.车4空调模式_通风模式, () => AirConditionModel.AirConditionInfoCar4.ConditionMode = AirConditionMode.Ventilation);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.车4空调模式_关闭预冷, () => AirConditionModel.AirConditionInfoCar4.ConditionMode = AirConditionMode.ClosePrecold);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.车4空调模式_测试模式, () => AirConditionModel.AirConditionInfoCar4.ConditionMode = AirConditionMode.Test);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.车4空调模式_火灾模式, () => AirConditionModel.AirConditionInfoCar4.ConditionMode = AirConditionMode.Fire);




            args.ChangedFloats.UpdateIfContains(InFloatKeys.车1车内温度, f => AirConditionModel.AirConditionInfoCar1.SettingTemperature = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.车1设定温度, f => AirConditionModel.AirConditionInfoCar1.InCarTemperature = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.车2车内温度, f => AirConditionModel.AirConditionInfoCar2.SettingTemperature = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.车2设定温度, f => AirConditionModel.AirConditionInfoCar2.InCarTemperature = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.车3车内温度, f => AirConditionModel.AirConditionInfoCar3.SettingTemperature = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.车3设定温度, f => AirConditionModel.AirConditionInfoCar3.InCarTemperature = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.车4车内温度, f => AirConditionModel.AirConditionInfoCar4.SettingTemperature = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.车4设定温度, f => AirConditionModel.AirConditionInfoCar4.InCarTemperature = f);
        }
    }
}
