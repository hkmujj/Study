using System.ComponentModel.Composition;
using DevExpress.Mvvm.Native;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH380D.Extension;
using Motor.HMI.CRH380D.Models.State;
using Motor.HMI.CRH380D.Resources.Keys;
using Motor.HMI.CRH380D.ViewModel;

namespace Motor.HMI.CRH380D.Adapter
{
    [Export(typeof(IUpdateDataProvider))]
    public class UpdateDomainViewDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public UpdateDomainViewDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void Initalize(bool isDebugModel)
        {

        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            //司机室占用状态
            UpdateDatasVOBCModel(sender,args);
            //激活界面
            UpdateDatasActivateModel(sender, args);
            //车门界面
            UpdateDatasDoorModel(sender, args);
            //车站界面
            UpdateDatasStationModel(sender, args);
            //高压界面
            UpdateDatasHighVoltyageModel(sender, args);
            //界面抬头
            UpdateDatasTitleModel(sender, args);
            //牵引界面
            UpdateDatasTractionModel(sender, args);
            //制动界面
            UpdateDatasBreakModel(sender, args);
            //客室舒适度界面
            UpdateDatasCarComfortModel(sender, args);
            //司机室舒适度界面
            UpdateDatasDriverComfortModel(sender, args);
            //供风界面
            UpdateDatasAirSupplyModel(sender, args);
            //转向架界面
            UpdateDatasBogieModel(sender, args);
            //牵引/制动界面
            UpdateDatasTractionAndBreakModel(sender, args);
            //卫生设备界面
            UpdateDatasWCDeviceModel(sender, args);
            //制动测试界面
            UpdateDatasBreakTestModel(sender, args);
            //手柄测试界面
            UpdateDatasHandleTestModel(sender, args);
            //列车状态界面
            UpdateDatasTrainStatusModel(sender, args);
            //直流供电界面
            UpdateDatasDCModel(sender, args);
        }


        private void UpdateDatasDCModel(object sender, CommunicationDataChangedArgs args)
        {
            var DCModel = ViewModel.Model.DCModel;

            //设备1状态
            DCModel.DCDevice1Unit.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));

            //设备2状态
            DCModel.DCDevice2Unit.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));

            //设备3状态
            DCModel.DCDevice3Unit.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));

            //电流
            //直流供电_5车位置1电流
            args.ChangedFloats.UpdateIfContains(InFolatKey.直流供电_5车位置1电流, f => DCModel.ElectronFlow51 = f);
            //直流供电_5车位置2电流
            args.ChangedFloats.UpdateIfContains(InFolatKey.直流供电_5车位置2电流, f => DCModel.ElectronFlow52 = f);
            //直流供电_4车位置1电流
            args.ChangedFloats.UpdateIfContains(InFolatKey.直流供电_4车位置1电流, f => DCModel.ElectronFlow41 = f);
            //直流供电_4车位置2电流
            args.ChangedFloats.UpdateIfContains(InFolatKey.直流供电_4车位置2电流, f => DCModel.ElectronFlow42 = f);

            //电压
            //直流供电_5车位置1电压
            args.ChangedFloats.UpdateIfContains(InFolatKey.直流供电_5车位置1电压, f => DCModel.DCVoltage51 = f);
            //直流供电_5车位置2电压
            args.ChangedFloats.UpdateIfContains(InFolatKey.直流供电_5车位置2电压, f => DCModel.DCVoltage52 = f);
            //直流供电_4车位置1电压
            args.ChangedFloats.UpdateIfContains(InFolatKey.直流供电_4车位置1电压, f => DCModel.DCVoltage41 = f);
            //直流供电_4车位置2电压
            args.ChangedFloats.UpdateIfContains(InFolatKey.直流供电_4车位置2电压, f => DCModel.DCVoltage42 = f);
            //直流供电_平均电压
            args.ChangedFloats.UpdateIfContains(InFolatKey.直流供电_平均电压, f => DCModel.DCVoltage = f);
        }

        private void UpdateDatasTrainStatusModel(object sender, CommunicationDataChangedArgs args)
        {
            var TrainStatusModel = ViewModel.Model.TrainStatusModel;

            //列车状态_牵引力
            args.ChangedFloats.UpdateIfContains(InFolatKey.列车状态_牵引力, f => TrainStatusModel.TractionValue = f);
            //列车状态_制动力
            args.ChangedFloats.UpdateIfContains(InFolatKey.列车状态_制动力, f => TrainStatusModel.BreakValue = f);
            //列车状态_网侧电流
            args.ChangedFloats.UpdateIfContains(InFolatKey.列车状态_网侧电流, f => TrainStatusModel.ElectronFlow = f);
            //列车状态_限速
            args.ChangedFloats.UpdateIfContains(InFolatKey.列车状态_限速, f => TrainStatusModel.LimitSpeed = f);

            //列车状态_紧急制动回路开启
            args.ChangedBools.UpdateIfContains(InBoolKey.列车状态_紧急制动回路开启, b => TrainStatusModel.EmergencyLoop = b);
            //列车状态_安全制动回路开启
            args.ChangedBools.UpdateIfContains(InBoolKey.列车状态_安全制动回路开启, b => TrainStatusModel.SafeBreakLoop = b);
            //列车状态_施加停放制动
            args.ChangedBools.UpdateIfContains(InBoolKey.列车状态_施加停放制动, b => TrainStatusModel.ParkBreak = b);
            //列车状态_牵引阻断开启
            args.ChangedBools.UpdateIfContains(InBoolKey.列车状态_牵引阻断开启, b => TrainStatusModel.StopTraction = b);
            //列车状态_主断路器打开
            args.ChangedBools.UpdateIfContains(InBoolKey.列车状态_主断路器打开, b => TrainStatusModel.LCB = b);
            //列车状态_主风缸压力正常
            args.ChangedBools.UpdateIfContains(InBoolKey.列车状态_主风缸压力正常, b => TrainStatusModel.PrimaryAirCylinder = b);
            //列车状态_防滑保护激活
            args.ChangedBools.UpdateIfContains(InBoolKey.列车状态_防滑保护激活, b => TrainStatusModel.AvoidSlip = b);
            //列车状态_救援回送
            args.ChangedBools.UpdateIfContains(InBoolKey.列车状态_救援回送, b => TrainStatusModel.RescueLoopBack = b);
            //列车状态_车门全部关闭
            args.ChangedBools.UpdateIfContains(InBoolKey.列车状态_车门全部关闭, b => TrainStatusModel.OpenAllDoor = b);
        }

        private void UpdateDatasHandleTestModel(object sender, CommunicationDataChangedArgs args)
        {
            var HandleTestModel = ViewModel.Model.HandleTestModel;
            //手柄测试状态
            //车0手柄测试状态
            HandleTestModel.Handle0TestState1 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制3级增速位置1正常, () => HandleTestModel.Handle0TestState1 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制3级增速位置1故障, () => HandleTestModel.Handle0TestState1 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle0TestState2 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制2级增速位置1正常, () => HandleTestModel.Handle0TestState2 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制2级增速位置1故障, () => HandleTestModel.Handle0TestState2 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle0TestState3 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制1级增速位置1正常, () => HandleTestModel.Handle0TestState3 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制1级增速位置1故障, () => HandleTestModel.Handle0TestState3 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle0TestState4 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制恒速位置1正常, () => HandleTestModel.Handle0TestState4 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制恒速位置1故障, () => HandleTestModel.Handle0TestState4 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle0TestState5 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制1级减速位置1正常, () => HandleTestModel.Handle0TestState5 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制1级减速位置1故障, () => HandleTestModel.Handle0TestState5 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle0TestState6 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制2级减速位置1正常, () => HandleTestModel.Handle0TestState6 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制2级减速位置1故障, () => HandleTestModel.Handle0TestState6 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle0TestState7 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制3级减速位置1正常, () => HandleTestModel.Handle0TestState7 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制3级减速位置1故障, () => HandleTestModel.Handle0TestState7 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle0TestState8 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制0位置1正常, () => HandleTestModel.Handle0TestState8 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制0位置1故障, () => HandleTestModel.Handle0TestState8 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle0TestState9 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制1级制动位置1正常, () => HandleTestModel.Handle0TestState9 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制1级制动位置1故障, () => HandleTestModel.Handle0TestState9 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle0TestState10 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制2级制动位置1正常, () => HandleTestModel.Handle0TestState10 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制2级制动位置1故障, () => HandleTestModel.Handle0TestState10 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle0TestState11 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制3级制动位置1正常, () => HandleTestModel.Handle0TestState11 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制3级制动位置1故障, () => HandleTestModel.Handle0TestState11 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle0TestState12 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制4级制动位置1正常, () => HandleTestModel.Handle0TestState12 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制4级制动位置1故障, () => HandleTestModel.Handle0TestState12 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle0TestState13 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制5级制动位置1正常, () => HandleTestModel.Handle0TestState13 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制5级制动位置1故障, () => HandleTestModel.Handle0TestState13 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle0TestState14 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制6级制动位置1正常, () => HandleTestModel.Handle0TestState14 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制6级制动位置1故障, () => HandleTestModel.Handle0TestState14 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle0TestState15 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制7级制动位置1正常, () => HandleTestModel.Handle0TestState15 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制7级制动位置1故障, () => HandleTestModel.Handle0TestState15 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle0TestState16 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制紧急制动位置1正常, () => HandleTestModel.Handle0TestState16 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制紧急制动位置1故障, () => HandleTestModel.Handle0TestState16 = HandleTestState.HandleTestFailed);

            //车1手柄测试状态
            HandleTestModel.Handle1TestState1 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制3级增速位置2正常, () => HandleTestModel.Handle1TestState1 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制3级增速位置2故障, () => HandleTestModel.Handle1TestState1 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle1TestState2 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制2级增速位置2正常, () => HandleTestModel.Handle1TestState2 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制2级增速位置2故障, () => HandleTestModel.Handle1TestState2 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle1TestState3 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制1级增速位置2正常, () => HandleTestModel.Handle1TestState3 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制1级增速位置2故障, () => HandleTestModel.Handle1TestState3 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle1TestState4 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制恒速位置2正常, () => HandleTestModel.Handle1TestState4 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制恒速位置2故障, () => HandleTestModel.Handle1TestState4 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle1TestState5 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制1级减速位置2正常, () => HandleTestModel.Handle1TestState5 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制1级减速位置2故障, () => HandleTestModel.Handle1TestState5 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle1TestState6 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制2级减速位置2正常, () => HandleTestModel.Handle1TestState6 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制2级减速位置2故障, () => HandleTestModel.Handle1TestState6 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle1TestState7 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制3级减速位置2正常, () => HandleTestModel.Handle1TestState7 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制3级减速位置2故障, () => HandleTestModel.Handle1TestState7 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle1TestState8 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制0位置2正常, () => HandleTestModel.Handle1TestState8 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制0位置2故障, () => HandleTestModel.Handle1TestState8 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle1TestState9 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制1级制动位置2正常, () => HandleTestModel.Handle1TestState9 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制1级制动位置2故障, () => HandleTestModel.Handle1TestState9 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle1TestState10 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制2级制动位置2正常, () => HandleTestModel.Handle1TestState10 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制2级制动位置2故障, () => HandleTestModel.Handle1TestState10 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle1TestState11 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制3级制动位置2正常, () => HandleTestModel.Handle1TestState11 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制3级制动位置2故障, () => HandleTestModel.Handle1TestState11 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle1TestState12 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制4级制动位置2正常, () => HandleTestModel.Handle1TestState12 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制4级制动位置2故障, () => HandleTestModel.Handle1TestState12 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle1TestState13 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制5级制动位置2正常, () => HandleTestModel.Handle1TestState13 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制5级制动位置2故障, () => HandleTestModel.Handle1TestState13 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle1TestState14 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制6级制动位置2正常, () => HandleTestModel.Handle1TestState14 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制6级制动位置2故障, () => HandleTestModel.Handle1TestState14 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle1TestState15 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制7级制动位置2正常, () => HandleTestModel.Handle1TestState15 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制7级制动位置2故障, () => HandleTestModel.Handle1TestState15 = HandleTestState.HandleTestFailed);
            HandleTestModel.Handle1TestState16 = HandleTestState.HandleTestNotStart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制紧急制动位置2正常, () => HandleTestModel.Handle1TestState16 = HandleTestState.HandleTestSuccess);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄控制紧急制动位置2故障, () => HandleTestModel.Handle1TestState16 = HandleTestState.HandleTestFailed);

            //手柄测试提示文本
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本1, () => HandleTestModel.HandleTestInfoNumDisplay = 1);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本2, () => HandleTestModel.HandleTestInfoNumDisplay = 2);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本3, () => HandleTestModel.HandleTestInfoNumDisplay = 3);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本4, () => HandleTestModel.HandleTestInfoNumDisplay = 4);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本5, () => HandleTestModel.HandleTestInfoNumDisplay = 5);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本6, () => HandleTestModel.HandleTestInfoNumDisplay = 6);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本7, () => HandleTestModel.HandleTestInfoNumDisplay = 7);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本8, () => HandleTestModel.HandleTestInfoNumDisplay = 8);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本9, () => HandleTestModel.HandleTestInfoNumDisplay = 9);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本10, () => HandleTestModel.HandleTestInfoNumDisplay = 10);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本11, () => HandleTestModel.HandleTestInfoNumDisplay = 11);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本12, () => HandleTestModel.HandleTestInfoNumDisplay = 12);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本13, () => HandleTestModel.HandleTestInfoNumDisplay = 13);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本14, () => HandleTestModel.HandleTestInfoNumDisplay = 14);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本15, () => HandleTestModel.HandleTestInfoNumDisplay = 15);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本16, () => HandleTestModel.HandleTestInfoNumDisplay = 16);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本17, () => HandleTestModel.HandleTestInfoNumDisplay = 17);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本18, () => HandleTestModel.HandleTestInfoNumDisplay = 18);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本19, () => HandleTestModel.HandleTestInfoNumDisplay = 19);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本20, () => HandleTestModel.HandleTestInfoNumDisplay = 20);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本21, () => HandleTestModel.HandleTestInfoNumDisplay = 21);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本22, () => HandleTestModel.HandleTestInfoNumDisplay = 22);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本23, () => HandleTestModel.HandleTestInfoNumDisplay = 23);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本24, () => HandleTestModel.HandleTestInfoNumDisplay = 24);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本25, () => HandleTestModel.HandleTestInfoNumDisplay = 25);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本26, () => HandleTestModel.HandleTestInfoNumDisplay = 26);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本27, () => HandleTestModel.HandleTestInfoNumDisplay = 27);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本28, () => HandleTestModel.HandleTestInfoNumDisplay = 28);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本29, () => HandleTestModel.HandleTestInfoNumDisplay = 29);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.手柄测试提示文本30, () => HandleTestModel.HandleTestInfoNumDisplay = 30);
        }


        private void UpdateDatasBreakTestModel(object sender, CommunicationDataChangedArgs args)
        {
            var BreakTestModel = ViewModel.Model.BreakTestModel;

            //司机室激活
            BreakTestModel.TrainModel.MC00 = VOBCState.NoActivate;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车0司机室激活, () => BreakTestModel.TrainModel.MC00 = VOBCState.Activate);

            BreakTestModel.TrainModel.MC01 = VOBCState.NoActivate;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1司机室激活, () => BreakTestModel.TrainModel.MC01 = VOBCState.Activate);

            //制动试验状态
            //车0制动试验状态
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车0制动试验缓解, () => BreakTestModel.TrainModel.CarModel0.State = CarState.BreakTestRemit);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车0制动试验施加, () => BreakTestModel.TrainModel.CarModel0.State = CarState.BreakTestForce);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车0制动试验故障, () => BreakTestModel.TrainModel.CarModel0.State = CarState.BreakTestFault);
            //车1制动试验状态
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1制动试验缓解, () => BreakTestModel.TrainModel.CarModel1.State = CarState.BreakTestRemit);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1制动试验施加, () => BreakTestModel.TrainModel.CarModel1.State = CarState.BreakTestForce);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1制动试验故障, () => BreakTestModel.TrainModel.CarModel1.State = CarState.BreakTestFault);
            //车2制动试验状态
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车2制动试验缓解, () => BreakTestModel.TrainModel.CarModel2.State = CarState.BreakTestRemit);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车2制动试验施加, () => BreakTestModel.TrainModel.CarModel2.State = CarState.BreakTestForce);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车2制动试验故障, () => BreakTestModel.TrainModel.CarModel2.State = CarState.BreakTestFault);
            //车3制动试验状态
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车3制动试验缓解, () => BreakTestModel.TrainModel.CarModel3.State = CarState.BreakTestRemit);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车3制动试验施加, () => BreakTestModel.TrainModel.CarModel3.State = CarState.BreakTestForce);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车3制动试验故障, () => BreakTestModel.TrainModel.CarModel3.State = CarState.BreakTestFault);
            //车4制动试验状态
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车4制动试验缓解, () => BreakTestModel.TrainModel.CarModel4.State = CarState.BreakTestRemit);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车4制动试验施加, () => BreakTestModel.TrainModel.CarModel4.State = CarState.BreakTestForce);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车4制动试验故障, () => BreakTestModel.TrainModel.CarModel4.State = CarState.BreakTestFault);
            //车5制动试验状态
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车5制动试验缓解, () => BreakTestModel.TrainModel.CarModel5.State = CarState.BreakTestRemit);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车5制动试验施加, () => BreakTestModel.TrainModel.CarModel5.State = CarState.BreakTestForce);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车5制动试验故障, () => BreakTestModel.TrainModel.CarModel5.State = CarState.BreakTestFault);
            //车6制动试验状态
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车6制动试验缓解, () => BreakTestModel.TrainModel.CarModel6.State = CarState.BreakTestRemit);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车6制动试验施加, () => BreakTestModel.TrainModel.CarModel6.State = CarState.BreakTestForce);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车6制动试验故障, () => BreakTestModel.TrainModel.CarModel6.State = CarState.BreakTestFault);
            //车7制动试验状态
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车7制动试验缓解, () => BreakTestModel.TrainModel.CarModel7.State = CarState.BreakTestRemit);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车7制动试验施加, () => BreakTestModel.TrainModel.CarModel7.State = CarState.BreakTestForce);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车7制动试验故障, () => BreakTestModel.TrainModel.CarModel7.State = CarState.BreakTestFault);

            //制动试验提示文本
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本1, () => BreakTestModel.BreakTestInfoNumDisplay = 1);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本2, () => BreakTestModel.BreakTestInfoNumDisplay = 2);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本3, () => BreakTestModel.BreakTestInfoNumDisplay = 3);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本4, () => BreakTestModel.BreakTestInfoNumDisplay = 4);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本5, () => BreakTestModel.BreakTestInfoNumDisplay = 5);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本6, () => BreakTestModel.BreakTestInfoNumDisplay = 6);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本7, () => BreakTestModel.BreakTestInfoNumDisplay = 7);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本8, () => BreakTestModel.BreakTestInfoNumDisplay = 8);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本9, () => BreakTestModel.BreakTestInfoNumDisplay = 9);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本10, () => BreakTestModel.BreakTestInfoNumDisplay = 10);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本11, () => BreakTestModel.BreakTestInfoNumDisplay = 11);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本12, () => BreakTestModel.BreakTestInfoNumDisplay = 12);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本13, () => BreakTestModel.BreakTestInfoNumDisplay = 13);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本14, () => BreakTestModel.BreakTestInfoNumDisplay = 14);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本15, () => BreakTestModel.BreakTestInfoNumDisplay = 15);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本16, () => BreakTestModel.BreakTestInfoNumDisplay = 16);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本17, () => BreakTestModel.BreakTestInfoNumDisplay = 17);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本18, () => BreakTestModel.BreakTestInfoNumDisplay = 18);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本19, () => BreakTestModel.BreakTestInfoNumDisplay = 19);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本20, () => BreakTestModel.BreakTestInfoNumDisplay = 20);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本21, () => BreakTestModel.BreakTestInfoNumDisplay = 21);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本22, () => BreakTestModel.BreakTestInfoNumDisplay = 22);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本23, () => BreakTestModel.BreakTestInfoNumDisplay = 23);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本24, () => BreakTestModel.BreakTestInfoNumDisplay = 24);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本25, () => BreakTestModel.BreakTestInfoNumDisplay = 25);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本26, () => BreakTestModel.BreakTestInfoNumDisplay = 26);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本27, () => BreakTestModel.BreakTestInfoNumDisplay = 27);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本28, () => BreakTestModel.BreakTestInfoNumDisplay = 28);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本29, () => BreakTestModel.BreakTestInfoNumDisplay = 29);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.制动试验提示文本30, () => BreakTestModel.BreakTestInfoNumDisplay = 30);
        }

        private void UpdateDatasWCDeviceModel(object sender, CommunicationDataChangedArgs args)
        {
            var WCDeviceModel = ViewModel.Model.WCDeviceModel;


            //司机室激活
            WCDeviceModel.TrainModel.MC00 = VOBCState.NoActivate;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车0司机室激活, () => WCDeviceModel.TrainModel.MC00 = VOBCState.Activate);

            WCDeviceModel.TrainModel.MC01 = VOBCState.NoActivate;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1司机室激活, () => WCDeviceModel.TrainModel.MC01 = VOBCState.Activate);

            //水箱水量
            //车0水箱水量
            args.ChangedFloats.UpdateIfContains(InFolatKey.车0水箱水量, f => WCDeviceModel.WaterValue0 = f);
            //车1水箱水量
            args.ChangedFloats.UpdateIfContains(InFolatKey.车1水箱水量, f => WCDeviceModel.WaterValue1 = f);
            //车2水箱水量
            args.ChangedFloats.UpdateIfContains(InFolatKey.车2水箱水量, f => WCDeviceModel.WaterValue2 = f);
            //车3水箱水量
            args.ChangedFloats.UpdateIfContains(InFolatKey.车3水箱水量, f => WCDeviceModel.WaterValue3 = f);
            //车4水箱水量
            args.ChangedFloats.UpdateIfContains(InFolatKey.车4水箱水量, f => WCDeviceModel.WaterValue4 = f);
            //车5水箱水量
            args.ChangedFloats.UpdateIfContains(InFolatKey.车5水箱水量, f => WCDeviceModel.WaterValue5 = f);
            //车6水箱水量
            args.ChangedFloats.UpdateIfContains(InFolatKey.车6水箱水量, f => WCDeviceModel.WaterValue6 = f);
            //车7水箱水量
            args.ChangedFloats.UpdateIfContains(InFolatKey.车7水箱水量, f => WCDeviceModel.WaterValue7 = f);

            //污物箱量
            //车2污物箱量
            args.ChangedFloats.UpdateIfContains(InFolatKey.车2污物箱量, f => WCDeviceModel.DirtValue2 = f);
            //车3污物箱量
            args.ChangedFloats.UpdateIfContains(InFolatKey.车3污物箱量, f => WCDeviceModel.DirtValue3 = f);
            //车5污物箱量
            args.ChangedFloats.UpdateIfContains(InFolatKey.车5污物箱量, f => WCDeviceModel.DirtValue5 = f);
            //车6污物箱量
            args.ChangedFloats.UpdateIfContains(InFolatKey.车6污物箱量, f => WCDeviceModel.DirtValue6 = f);
            //车7污物箱量
            args.ChangedFloats.UpdateIfContains(InFolatKey.车7污物箱量, f => WCDeviceModel.DirtValue7 = f);

            //卫生间使用状态
            WCDeviceModel.WCDevice21 = WCDeviceState.None;
            WCDeviceModel.WCDevice21 = WCDeviceState.None;
            //车2卫生间1使用中
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车2卫生间1使用中, () => WCDeviceModel.WCDevice21 = WCDeviceState.Using);
            //车2卫生间1故障
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车2卫生间1故障, () => WCDeviceModel.WCDevice21 = WCDeviceState.Fault);
            //车2卫生间2使用中
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车2卫生间2使用中, () => WCDeviceModel.WCDevice22 = WCDeviceState.Using);
            //车2卫生间2故障
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车2卫生间2故障, () => WCDeviceModel.WCDevice22 = WCDeviceState.Fault);
            WCDeviceModel.WCDevice31 = WCDeviceState.None;
            WCDeviceModel.WCDevice32 = WCDeviceState.None;
            //车3卫生间1使用中
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车3卫生间1使用中, () => WCDeviceModel.WCDevice31 = WCDeviceState.Using);
            //车3卫生间1故障
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车3卫生间1故障, () => WCDeviceModel.WCDevice31 = WCDeviceState.Fault);
            //车3卫生间2使用中
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车3卫生间2使用中, () => WCDeviceModel.WCDevice32 = WCDeviceState.Using);
            //车3卫生间2故障
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车3卫生间2故障, () => WCDeviceModel.WCDevice32 = WCDeviceState.Fault);
            WCDeviceModel.WCDevice51 = WCDeviceState.None;
            WCDeviceModel.WCDevice52 = WCDeviceState.None;
            //车5卫生间1使用中
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车5卫生间1使用中, () => WCDeviceModel.WCDevice51 = WCDeviceState.Using);
            //车5卫生间1故障
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车5卫生间1故障, () => WCDeviceModel.WCDevice51 = WCDeviceState.Fault);
            //车5卫生间2使用中
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车5卫生间2使用中, () => WCDeviceModel.WCDevice52 = WCDeviceState.Using);
            //车5卫生间2故障
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车5卫生间2故障, () => WCDeviceModel.WCDevice52 = WCDeviceState.Fault);
            WCDeviceModel.WCDevice61 = WCDeviceState.None;
            WCDeviceModel.WCDevice62 = WCDeviceState.None;
            //车6卫生间1使用中
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车6卫生间1使用中, () => WCDeviceModel.WCDevice61 = WCDeviceState.Using);
            //车6卫生间1故障
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车6卫生间1故障, () => WCDeviceModel.WCDevice61 = WCDeviceState.Fault);
            //车6卫生间2使用中
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车6卫生间2使用中, () => WCDeviceModel.WCDevice62 = WCDeviceState.Using);
            //车6卫生间2故障
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车6卫生间2故障, () => WCDeviceModel.WCDevice62 = WCDeviceState.Fault);
            WCDeviceModel.WCDevice71 = WCDeviceState.None;
            WCDeviceModel.WCDevice72 = WCDeviceState.None;
            //车7卫生间1使用中
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车7卫生间1使用中, () => WCDeviceModel.WCDevice71 = WCDeviceState.Using);
            //车7卫生间1故障
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车7卫生间1故障, () => WCDeviceModel.WCDevice71 = WCDeviceState.Fault);
            //车7卫生间2使用中
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车7卫生间2使用中, () => WCDeviceModel.WCDevice72 = WCDeviceState.Using);
            //车7卫生间2故障
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车7卫生间2故障, () => WCDeviceModel.WCDevice72 = WCDeviceState.Fault);
        }

        private void UpdateDatasTractionAndBreakModel(object sender, CommunicationDataChangedArgs args)
        {
            var TractionAndBreakModel = ViewModel.Model.TractionAndBreakModel;

            //牵引力
            //车0牵引力
            args.ChangedFloats.UpdateIfContains(InFolatKey.车0牵引力百分比, f => TractionAndBreakModel.TractionValue0 = f);
            //车1牵引力
            args.ChangedFloats.UpdateIfContains(InFolatKey.车1牵引力百分比, f => TractionAndBreakModel.TractionValue1 = f);
            //车2牵引力
            args.ChangedFloats.UpdateIfContains(InFolatKey.车2牵引力百分比, f => TractionAndBreakModel.TractionValue2 = f);
            //车3牵引力
            args.ChangedFloats.UpdateIfContains(InFolatKey.车3牵引力百分比, f => TractionAndBreakModel.TractionValue3 = f);
            //车4牵引力
            args.ChangedFloats.UpdateIfContains(InFolatKey.车4牵引力百分比, f => TractionAndBreakModel.TractionValue4 = f);
            //车5牵引力
            args.ChangedFloats.UpdateIfContains(InFolatKey.车5牵引力百分比, f => TractionAndBreakModel.TractionValue5 = f);
            //车6牵引力
            args.ChangedFloats.UpdateIfContains(InFolatKey.车6牵引力百分比, f => TractionAndBreakModel.TractionValue6 = f);
            //车7牵引力
            args.ChangedFloats.UpdateIfContains(InFolatKey.车7牵引力百分比, f => TractionAndBreakModel.TractionValue7 = f);

            //制动力
            //车0制动力
            args.ChangedFloats.UpdateIfContains(InFolatKey.车0制动力百分比, f => TractionAndBreakModel.BreakValue0 = f);
            //车1制动力
            args.ChangedFloats.UpdateIfContains(InFolatKey.车1制动力百分比, f => TractionAndBreakModel.BreakValue1 = f);
            //车2制动力
            args.ChangedFloats.UpdateIfContains(InFolatKey.车2制动力百分比, f => TractionAndBreakModel.BreakValue2 = f);
            //车3制动力
            args.ChangedFloats.UpdateIfContains(InFolatKey.车3制动力百分比, f => TractionAndBreakModel.BreakValue3 = f);
            //车4制动力
            args.ChangedFloats.UpdateIfContains(InFolatKey.车4制动力百分比, f => TractionAndBreakModel.BreakValue4 = f);
            //车5制动力
            args.ChangedFloats.UpdateIfContains(InFolatKey.车5制动力百分比, f => TractionAndBreakModel.BreakValue5 = f);
            //车6制动力
            args.ChangedFloats.UpdateIfContains(InFolatKey.车6制动力百分比, f => TractionAndBreakModel.BreakValue6 = f);
            //车7制动力
            args.ChangedFloats.UpdateIfContains(InFolatKey.车7制动力百分比, f => TractionAndBreakModel.BreakValue7 = f);

            //加速度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车辆加速度, f => TractionAndBreakModel.Acc = f);
        }

        private void UpdateDatasBogieModel(object sender, CommunicationDataChangedArgs args)
        {
            var BogieModel = ViewModel.Model.BogieModel;

            //司机室激活
            BogieModel.TrainModel.MC00 = VOBCState.NoActivate;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车0司机室激活, () => BogieModel.TrainModel.MC00 = VOBCState.Activate);

            BogieModel.TrainModel.MC01 = VOBCState.NoActivate;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1司机室激活, () => BogieModel.TrainModel.MC01 = VOBCState.Activate);

            //转向架状态
            //车0转向架状态
            args.ChangedBools.UpdateIfContains(InBoolKey.车0转向架超温, b =>
            {
                BogieModel.TrainModel.CarModel0.State = b ? CarState.BogieOverHeat : CarState.BogieNormal;
            } );
            //车1转向架状态
            args.ChangedBools.UpdateIfContains(InBoolKey.车1转向架超温, b =>
            {
                BogieModel.TrainModel.CarModel1.State = b ? CarState.BogieOverHeat : CarState.BogieNormal;
            });
            //车2转向架状态
            args.ChangedBools.UpdateIfContains(InBoolKey.车2转向架超温, b =>
            {
                BogieModel.TrainModel.CarModel2.State = b ? CarState.BogieOverHeat : CarState.BogieNormal;
            });
            //车3转向架状态
            args.ChangedBools.UpdateIfContains(InBoolKey.车3转向架超温, b =>
            {
                BogieModel.TrainModel.CarModel3.State = b ? CarState.BogieOverHeat : CarState.BogieNormal;
            });
            //车4转向架状态
            args.ChangedBools.UpdateIfContains(InBoolKey.车4转向架超温, b =>
            {
                BogieModel.TrainModel.CarModel4.State = b ? CarState.BogieOverHeat : CarState.BogieNormal;
            });
            //车5转向架状态
            args.ChangedBools.UpdateIfContains(InBoolKey.车5转向架超温, b =>
            {
                BogieModel.TrainModel.CarModel5.State = b ? CarState.BogieOverHeat : CarState.BogieNormal;
            });
            //车6转向架状态
            args.ChangedBools.UpdateIfContains(InBoolKey.车6转向架超温, b =>
            {
                BogieModel.TrainModel.CarModel6.State = b ? CarState.BogieOverHeat : CarState.BogieNormal;
            });
            //车7转向架状态
            args.ChangedBools.UpdateIfContains(InBoolKey.车7转向架超温, b =>
            {
                BogieModel.TrainModel.CarModel7.State = b ? CarState.BogieOverHeat : CarState.BogieNormal;
            });

            //转向架温度
            //车0转向架温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车0转向架温度, f => BogieModel.BogieTemperature0 = f);
            //车1转向架温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车1转向架温度, f => BogieModel.BogieTemperature1 = f);
            //车2转向架温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车2转向架温度, f => BogieModel.BogieTemperature2 = f);
            //车3转向架温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车3转向架温度, f => BogieModel.BogieTemperature3 = f);
            //车4转向架温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车4转向架温度, f => BogieModel.BogieTemperature4 = f);
            //车5转向架温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车5转向架温度, f => BogieModel.BogieTemperature5 = f);
            //车6转向架温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车6转向架温度, f => BogieModel.BogieTemperature6 = f);
            //车7转向架温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车7转向架温度, f => BogieModel.BogieTemperature7 = f);

            //IMS状态
            //车0IMS1状态
            args.ChangedBools.UpdateIfContains(InBoolKey.车0IMS1失稳, b =>
            {
                BogieModel.BogieModel0.IMSState1 = b ? IMSState.IMSStable : IMSState.IMSUnStable;
            }); 
            //车0IMS2状态
            args.ChangedBools.UpdateIfContains(InBoolKey.车0IMS2失稳, b =>
            {
                BogieModel.BogieModel0.IMSState2 = b ? IMSState.IMSStable : IMSState.IMSUnStable;
            });
            //车1IMS1状态
            args.ChangedBools.UpdateIfContains(InBoolKey.车1IMS1失稳, b =>
            {
                BogieModel.BogieModel1.IMSState1 = b ? IMSState.IMSStable : IMSState.IMSUnStable;
            });
            //车1IMS2状态
            args.ChangedBools.UpdateIfContains(InBoolKey.车1IMS2失稳, b =>
            {
                BogieModel.BogieModel1.IMSState2 = b ? IMSState.IMSStable : IMSState.IMSUnStable;
            });
            //车2IMS1状态
            args.ChangedBools.UpdateIfContains(InBoolKey.车2IMS1失稳, b =>
            {
                BogieModel.BogieModel2.IMSState1 = b ? IMSState.IMSStable : IMSState.IMSUnStable;
            });
            //车2IMS2状态
            args.ChangedBools.UpdateIfContains(InBoolKey.车2IMS2失稳, b =>
            {
                BogieModel.BogieModel2.IMSState2 = b ? IMSState.IMSStable : IMSState.IMSUnStable;
            });
            //车3IMS1状态
            args.ChangedBools.UpdateIfContains(InBoolKey.车3IMS1失稳, b =>
            {
                BogieModel.BogieModel3.IMSState1 = b ? IMSState.IMSStable : IMSState.IMSUnStable;
            });
            //车3IMS2状态
            args.ChangedBools.UpdateIfContains(InBoolKey.车3IMS2失稳, b =>
            {
                BogieModel.BogieModel3.IMSState2 = b ? IMSState.IMSStable : IMSState.IMSUnStable;
            });
            //车4IMS1状态
            args.ChangedBools.UpdateIfContains(InBoolKey.车4IMS1失稳, b =>
            {
                BogieModel.BogieModel4.IMSState1 = b ? IMSState.IMSStable : IMSState.IMSUnStable;
            });
            //车4IMS2状态
            args.ChangedBools.UpdateIfContains(InBoolKey.车4IMS2失稳, b =>
            {
                BogieModel.BogieModel4.IMSState2 = b ? IMSState.IMSStable : IMSState.IMSUnStable;
            });
            //车5IMS1状态
            args.ChangedBools.UpdateIfContains(InBoolKey.车5IMS1失稳, b =>
            {
                BogieModel.BogieModel5.IMSState1 = b ? IMSState.IMSStable : IMSState.IMSUnStable;
            });
            //车5IMS2状态
            args.ChangedBools.UpdateIfContains(InBoolKey.车5IMS2失稳, b =>
            {
                BogieModel.BogieModel5.IMSState2 = b ? IMSState.IMSStable : IMSState.IMSUnStable;
            });
            //车6IMS1状态
            args.ChangedBools.UpdateIfContains(InBoolKey.车6IMS1失稳, b =>
            {
                BogieModel.BogieModel6.IMSState1 = b ? IMSState.IMSStable : IMSState.IMSUnStable;
            });
            //车6IMS2状态
            args.ChangedBools.UpdateIfContains(InBoolKey.车6IMS2失稳, b =>
            {
                BogieModel.BogieModel6.IMSState2 = b ? IMSState.IMSStable : IMSState.IMSUnStable;
            });
            //车7IMS1状态
            args.ChangedBools.UpdateIfContains(InBoolKey.车7IMS1失稳, b =>
            {
                BogieModel.BogieModel7.IMSState1 = b ? IMSState.IMSStable : IMSState.IMSUnStable;
            });
            //车7IMS2状态
            args.ChangedBools.UpdateIfContains(InBoolKey.车7IMS2失稳, b =>
            {
                BogieModel.BogieModel7.IMSState2 = b ? IMSState.IMSStable : IMSState.IMSUnStable;
            });

            //轴温
            //车0轴1温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车0轴1温度, f => BogieModel.BogieModel0.AxleTemperature1 = f);
            //车0轴2温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车0轴2温度, f => BogieModel.BogieModel0.AxleTemperature2 = f);
            //车1轴1温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车1轴1温度, f => BogieModel.BogieModel1.AxleTemperature1 = f);
            //车1轴2温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车1轴2温度, f => BogieModel.BogieModel1.AxleTemperature2 = f);
            //车2轴1温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车2轴1温度, f => BogieModel.BogieModel2.AxleTemperature1 = f);
            //车2轴2温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车2轴2温度, f => BogieModel.BogieModel2.AxleTemperature2 = f);
            //车3轴1温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车3轴1温度, f => BogieModel.BogieModel3.AxleTemperature1 = f);
            //车3轴2温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车3轴2温度, f => BogieModel.BogieModel3.AxleTemperature2 = f);
            //车4轴1温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车4轴1温度, f => BogieModel.BogieModel4.AxleTemperature1 = f);
            //车4轴2温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车4轴2温度, f => BogieModel.BogieModel4.AxleTemperature2 = f);
            //车5轴1温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车5轴1温度, f => BogieModel.BogieModel5.AxleTemperature1 = f);
            //车5轴2温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车5轴2温度, f => BogieModel.BogieModel5.AxleTemperature2 = f);
            //车6轴1温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车6轴1温度, f => BogieModel.BogieModel6.AxleTemperature1 = f);
            //车6轴2温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车6轴2温度, f => BogieModel.BogieModel6.AxleTemperature2 = f);
            //车7轴1温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车7轴1温度, f => BogieModel.BogieModel7.AxleTemperature1 = f);
            //车7轴2温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车7轴2温度, f => BogieModel.BogieModel7.AxleTemperature2 = f);

            //齿轮箱温度
            //车0齿轮箱1温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车0齿轮箱1温度, f => BogieModel.BogieModel0.GearTemperature1 = f);
            //车0齿轮箱2温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车0齿轮箱2温度, f => BogieModel.BogieModel0.GearTemperature2 = f);
            //车0齿轮箱3温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车0齿轮箱3温度, f => BogieModel.BogieModel0.GearTemperature3 = f);
            //车0齿轮箱4温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车0齿轮箱4温度, f => BogieModel.BogieModel0.GearTemperature4 = f);
            //车0齿轮箱5温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车0齿轮箱5温度, f => BogieModel.BogieModel0.GearTemperature5 = f);
            //车0齿轮箱6温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车0齿轮箱6温度, f => BogieModel.BogieModel0.GearTemperature6 = f);
            //车0齿轮箱7温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车0齿轮箱7温度, f => BogieModel.BogieModel0.GearTemperature7 = f);
            //车0齿轮箱8温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车0齿轮箱8温度, f => BogieModel.BogieModel0.GearTemperature8 = f);
            //车1齿轮箱1温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车1齿轮箱1温度, f => BogieModel.BogieModel1.GearTemperature1 = f);
            //车1齿轮箱2温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车1齿轮箱2温度, f => BogieModel.BogieModel1.GearTemperature2 = f);
            //车1齿轮箱3温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车1齿轮箱3温度, f => BogieModel.BogieModel1.GearTemperature3 = f);
            //车1齿轮箱4温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车1齿轮箱4温度, f => BogieModel.BogieModel1.GearTemperature4 = f);
            //车1齿轮箱5温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车1齿轮箱5温度, f => BogieModel.BogieModel1.GearTemperature5 = f);
            //车1齿轮箱6温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车1齿轮箱6温度, f => BogieModel.BogieModel1.GearTemperature6 = f);
            //车1齿轮箱7温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车1齿轮箱7温度, f => BogieModel.BogieModel1.GearTemperature7 = f);
            //车1齿轮箱8温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车1齿轮箱8温度, f => BogieModel.BogieModel1.GearTemperature8 = f);
            //车2齿轮箱1温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车2齿轮箱1温度, f => BogieModel.BogieModel2.GearTemperature1 = f);
            //车2齿轮箱2温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车2齿轮箱2温度, f => BogieModel.BogieModel2.GearTemperature2 = f);
            //车2齿轮箱3温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车2齿轮箱3温度, f => BogieModel.BogieModel2.GearTemperature3 = f);
            //车2齿轮箱4温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车2齿轮箱4温度, f => BogieModel.BogieModel2.GearTemperature4 = f);
            //车2齿轮箱5温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车2齿轮箱5温度, f => BogieModel.BogieModel2.GearTemperature5 = f);
            //车2齿轮箱6温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车2齿轮箱6温度, f => BogieModel.BogieModel2.GearTemperature6 = f);
            //车2齿轮箱7温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车2齿轮箱7温度, f => BogieModel.BogieModel2.GearTemperature7 = f);
            //车2齿轮箱8温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车2齿轮箱8温度, f => BogieModel.BogieModel2.GearTemperature8 = f);
            //车3齿轮箱1温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车3齿轮箱1温度, f => BogieModel.BogieModel3.GearTemperature1 = f);
            //车3齿轮箱2温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车3齿轮箱2温度, f => BogieModel.BogieModel3.GearTemperature2 = f);
            //车3齿轮箱3温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车3齿轮箱3温度, f => BogieModel.BogieModel3.GearTemperature3 = f);
            //车3齿轮箱4温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车3齿轮箱4温度, f => BogieModel.BogieModel3.GearTemperature4 = f);
            //车3齿轮箱5温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车3齿轮箱5温度, f => BogieModel.BogieModel3.GearTemperature5 = f);
            //车3齿轮箱6温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车3齿轮箱6温度, f => BogieModel.BogieModel3.GearTemperature6 = f);
            //车3齿轮箱7温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车3齿轮箱7温度, f => BogieModel.BogieModel3.GearTemperature7 = f);
            //车3齿轮箱8温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车3齿轮箱8温度, f => BogieModel.BogieModel3.GearTemperature8 = f);
            //车4齿轮箱1温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车4齿轮箱1温度, f => BogieModel.BogieModel4.GearTemperature1 = f);
            //车4齿轮箱2温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车4齿轮箱2温度, f => BogieModel.BogieModel4.GearTemperature2 = f);
            //车4齿轮箱3温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车4齿轮箱3温度, f => BogieModel.BogieModel4.GearTemperature3 = f);
            //车4齿轮箱4温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车4齿轮箱4温度, f => BogieModel.BogieModel4.GearTemperature4 = f);
            //车4齿轮箱5温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车4齿轮箱5温度, f => BogieModel.BogieModel4.GearTemperature5 = f);
            //车4齿轮箱6温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车4齿轮箱6温度, f => BogieModel.BogieModel4.GearTemperature6 = f);
            //车4齿轮箱7温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车4齿轮箱7温度, f => BogieModel.BogieModel4.GearTemperature7 = f);
            //车4齿轮箱8温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车4齿轮箱8温度, f => BogieModel.BogieModel4.GearTemperature8 = f);
            //车5齿轮箱1温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车5齿轮箱1温度, f => BogieModel.BogieModel5.GearTemperature1 = f);
            //车5齿轮箱2温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车5齿轮箱2温度, f => BogieModel.BogieModel5.GearTemperature2 = f);
            //车5齿轮箱3温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车5齿轮箱3温度, f => BogieModel.BogieModel5.GearTemperature3 = f);
            //车5齿轮箱4温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车5齿轮箱4温度, f => BogieModel.BogieModel5.GearTemperature4 = f);
            //车5齿轮箱5温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车5齿轮箱5温度, f => BogieModel.BogieModel5.GearTemperature5 = f);
            //车5齿轮箱6温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车5齿轮箱6温度, f => BogieModel.BogieModel5.GearTemperature6 = f);
            //车5齿轮箱7温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车5齿轮箱7温度, f => BogieModel.BogieModel5.GearTemperature7 = f);
            //车5齿轮箱8温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车5齿轮箱8温度, f => BogieModel.BogieModel5.GearTemperature8 = f);
            //车6齿轮箱1温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车6齿轮箱1温度, f => BogieModel.BogieModel6.GearTemperature1 = f);
            //车6齿轮箱2温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车6齿轮箱2温度, f => BogieModel.BogieModel6.GearTemperature2 = f);
            //车6齿轮箱3温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车6齿轮箱3温度, f => BogieModel.BogieModel6.GearTemperature3 = f);
            //车6齿轮箱4温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车6齿轮箱4温度, f => BogieModel.BogieModel6.GearTemperature4 = f);
            //车6齿轮箱5温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车6齿轮箱5温度, f => BogieModel.BogieModel6.GearTemperature5 = f);
            //车6齿轮箱6温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车6齿轮箱6温度, f => BogieModel.BogieModel6.GearTemperature6 = f);
            //车6齿轮箱7温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车6齿轮箱7温度, f => BogieModel.BogieModel6.GearTemperature7 = f);
            //车6齿轮箱8温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车6齿轮箱8温度, f => BogieModel.BogieModel6.GearTemperature8 = f);
            //车7齿轮箱1温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车7齿轮箱1温度, f => BogieModel.BogieModel7.GearTemperature1 = f);
            //车7齿轮箱2温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车7齿轮箱2温度, f => BogieModel.BogieModel7.GearTemperature2 = f);
            //车7齿轮箱3温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车7齿轮箱3温度, f => BogieModel.BogieModel7.GearTemperature3 = f);
            //车7齿轮箱4温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车7齿轮箱4温度, f => BogieModel.BogieModel7.GearTemperature4 = f);
            //车7齿轮箱5温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车7齿轮箱5温度, f => BogieModel.BogieModel7.GearTemperature5 = f);
            //车7齿轮箱6温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车7齿轮箱6温度, f => BogieModel.BogieModel7.GearTemperature6 = f);
            //车7齿轮箱7温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车7齿轮箱7温度, f => BogieModel.BogieModel7.GearTemperature7 = f);
            //车7齿轮箱8温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车7齿轮箱8温度, f => BogieModel.BogieModel7.GearTemperature8 = f);
        }

        private void UpdateDatasAirSupplyModel(object sender, CommunicationDataChangedArgs args)
        {
            var AirSupplyModel = ViewModel.Model.AirSupplyModel;

            //主压缩机状态
            AirSupplyModel.PrimaryCompressorUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));

            //辅助压缩机状态
            AirSupplyModel.SubCompressorUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));

            //压力传感器状态
            AirSupplyModel.PressureSensorUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));

            //车0压力传感器压力
            args.ChangedFloats.UpdateIfContains(InFolatKey.车0压力传感器压力值, f => AirSupplyModel.PressureSensorValue0 = f);
            //车5压力传感器压力
            args.ChangedFloats.UpdateIfContains(InFolatKey.车5压力传感器压力值, f => AirSupplyModel.PressureSensorValue5 = f);
            //车4压力传感器压力
            args.ChangedFloats.UpdateIfContains(InFolatKey.车4压力传感器压力值, f => AirSupplyModel.PressureSensorValue4 = f);
            //车1压力传感器压力
            args.ChangedFloats.UpdateIfContains(InFolatKey.车1压力传感器压力值, f => AirSupplyModel.PressureSensorValue1 = f);
        }

        private void UpdateDatasDriverComfortModel(object sender, CommunicationDataChangedArgs args)
        {
            var DriverComfortModel = ViewModel.Model.DriverComfortModel;

            //空调状态
            DriverComfortModel.CarComfortUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));

            //外部温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.外部温度, f => DriverComfortModel.OutTemperature = f);
        }
        
        private void UpdateDatasCarComfortModel(object sender, CommunicationDataChangedArgs args)
        {
            var CarComfortModel = ViewModel.Model.CarComfortModel;
            
            //司机室激活
            CarComfortModel.TrainModel.MC00 = VOBCState.NoActivate;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车0司机室激活, () => CarComfortModel.TrainModel.MC00 = VOBCState.Activate);

            CarComfortModel.TrainModel.MC01 = VOBCState.NoActivate;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1司机室激活, () => CarComfortModel.TrainModel.MC01 = VOBCState.Activate);

            //温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.车0温度, f => CarComfortModel.Temperature0= f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车7温度, f => CarComfortModel.Temperature7 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车6温度, f => CarComfortModel.Temperature6 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车5温度, f => CarComfortModel.Temperature5 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车4温度, f => CarComfortModel.Temperature4 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车3温度, f => CarComfortModel.Temperature3 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车2温度, f => CarComfortModel.Temperature2 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车1温度, f => CarComfortModel.Temperature1 = f);

            //外部温度
            args.ChangedFloats.UpdateIfContains(InFolatKey.外部温度, f => CarComfortModel.OutTemperature = f);

            //空调状态
            CarComfortModel.CarComfortUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));

            //车辆状态
            //车0客室舒适度状态
            CarComfortModel.TrainModel.CarModel0.State = CarState.None;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车0司机室舒适度车辆切除, () => CarComfortModel.TrainModel.CarModel0.State = CarState.CarComforCut);
            //车1客室舒适度状态
            CarComfortModel.TrainModel.CarModel1.State = CarState.None;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1司机室舒适度车辆切除, () => CarComfortModel.TrainModel.CarModel1.State = CarState.CarComforCut);
            //车2客室舒适度状态
            CarComfortModel.TrainModel.CarModel2.State = CarState.None;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车2客室舒适度车辆切除, () => CarComfortModel.TrainModel.CarModel2.State = CarState.CarComforCut);
            //车3客室舒适度状态
            CarComfortModel.TrainModel.CarModel3.State = CarState.None;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车3客室舒适度车辆切除, () => CarComfortModel.TrainModel.CarModel3.State = CarState.CarComforCut);
            //车4客室舒适度状态
            CarComfortModel.TrainModel.CarModel4.State = CarState.None;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车4客室舒适度车辆切除, () => CarComfortModel.TrainModel.CarModel4.State = CarState.CarComforCut);
            //车5客室舒适度状态
            CarComfortModel.TrainModel.CarModel5.State = CarState.None;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车5客室舒适度车辆切除, () => CarComfortModel.TrainModel.CarModel5.State = CarState.CarComforCut);
            //车6客室舒适度状态
            CarComfortModel.TrainModel.CarModel6.State = CarState.None;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车6客室舒适度车辆切除, () => CarComfortModel.TrainModel.CarModel6.State = CarState.CarComforCut);
            //车7客室舒适度状态
            CarComfortModel.TrainModel.CarModel7.State = CarState.None;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车7客室舒适度车辆切除, () => CarComfortModel.TrainModel.CarModel7.State = CarState.CarComforCut);
        }

        private void UpdateDatasBreakModel(object sender, CommunicationDataChangedArgs args)
        {
            var BreakModel = ViewModel.Model.BreakModel;

            //制动力
            args.ChangedFloats.UpdateIfContains(InFolatKey.车0制动力, f => BreakModel.BreakValue0 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车7制动力, f => BreakModel.BreakValue7 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车6制动力, f => BreakModel.BreakValue6 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车5制动力, f => BreakModel.BreakValue5 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车4制动力, f => BreakModel.BreakValue4 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车3制动力, f => BreakModel.BreakValue3 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车2制动力, f => BreakModel.BreakValue2 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车1制动力, f => BreakModel.BreakValue1 = f);

            //BCU状态
            BreakModel.BCUUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));

            //停放制动状态
            BreakModel.ParkBreakUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));
             
            //紧急制动安全制动状态
            BreakModel.EmergencyBreakUnits.ForEach(b=>b.DataChanged(args.ChangedBools.NewValue));
        }

        private void UpdateDatasTractionModel(object sender, CommunicationDataChangedArgs args)
        {
            var TractionModel = ViewModel.Model.TractionModel;

            //司机室激活
            TractionModel.TrainModel.MC00 = VOBCState.NoActivate;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车0司机室激活, () => TractionModel.TrainModel.MC00 = VOBCState.Activate);

            TractionModel.TrainModel.MC01 = VOBCState.NoActivate;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1司机室激活, () => TractionModel.TrainModel.MC01 = VOBCState.Activate);

            //ACM
            TractionModel.ACMUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));

            //LCM
            TractionModel.LCMUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));

            //MCM
            TractionModel.MCMUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));

            args.ChangedFloats.UpdateIfContains(InFolatKey.网侧变流器电压, f => TractionModel.SideInverterVoltage = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.辅助变流器电压, f => TractionModel.SubInverterVoltage = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.牵引变流器1力, f => TractionModel.TractionInverterForce1 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.牵引变流器2力, f => TractionModel.TractionInverterForce2 = f);

            //车0牵引状态
            TractionModel.TrainModel.CarModel0.State = CarState.None;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车0牵引故障, () => TractionModel.TrainModel.CarModel0.State = CarState.TractionFault);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车0牵引切除, () => TractionModel.TrainModel.CarModel0.State = CarState.TractionCut);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车0牵引运行, () => TractionModel.TrainModel.CarModel0.State = CarState.TractionTurnOn);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车0牵引未运行, () => TractionModel.TrainModel.CarModel0.State = CarState.TractionTurnOff);

            //车7牵引状态
            TractionModel.TrainModel.CarModel7.State = CarState.None;

            //车6牵引状态
            TractionModel.TrainModel.CarModel6.State = CarState.None;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车6牵引故障, () => TractionModel.TrainModel.CarModel6.State = CarState.TractionFault);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车6牵引切除, () => TractionModel.TrainModel.CarModel6.State = CarState.TractionCut);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车6牵引运行, () => TractionModel.TrainModel.CarModel6.State = CarState.TractionTurnOn);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车6牵引未运行, () => TractionModel.TrainModel.CarModel6.State = CarState.TractionTurnOff);

            //车5牵引状态
            TractionModel.TrainModel.CarModel5.State = CarState.None;

            //车4牵引状态
            TractionModel.TrainModel.CarModel4.State = CarState.None;

            //车3牵引状态
            TractionModel.TrainModel.CarModel3.State = CarState.None;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车3牵引故障, () => TractionModel.TrainModel.CarModel3.State = CarState.TractionFault);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车3牵引切除, () => TractionModel.TrainModel.CarModel3.State = CarState.TractionCut);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车3牵引运行, () => TractionModel.TrainModel.CarModel3.State = CarState.TractionTurnOn);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车3牵引未运行, () => TractionModel.TrainModel.CarModel3.State = CarState.TractionTurnOff);

            //车2牵引状态
            TractionModel.TrainModel.CarModel2.State = CarState.None;

            //车1牵引状态
            TractionModel.TrainModel.CarModel1.State = CarState.None;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1牵引故障, () => TractionModel.TrainModel.CarModel1.State = CarState.TractionFault);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1牵引切除, () => TractionModel.TrainModel.CarModel1.State = CarState.TractionCut);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1牵引运行, () => TractionModel.TrainModel.CarModel1.State = CarState.TractionTurnOn);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1牵引未运行, () => TractionModel.TrainModel.CarModel1.State = CarState.TractionTurnOff);
        }

        private void UpdateDatasVOBCModel(object sender, CommunicationDataChangedArgs args)
        {
            var VOBCModel = ViewModel.Model.VOBCModel;

            //司机室激活
            VOBCModel.MC00 = VOBCState.NoActivate;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车0司机室激活, () => VOBCModel.MC00 = VOBCState.Activate);
            
            VOBCModel.MC01 = VOBCState.NoActivate;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1司机室激活, () => VOBCModel.MC01 = VOBCState.Activate);
        }

        private void UpdateDatasTitleModel(object sender, CommunicationDataChangedArgs args)
        {
            var TitleModel = ViewModel.Model.TitleModel;

            //电压
            args.ChangedFloats.UpdateIfContains(InFolatKey.电压, f => TitleModel.Voltage = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.速度, f => TitleModel.Speed = f);
        }

        private void UpdateDatasHighVoltyageModel(object sender, CommunicationDataChangedArgs args)
        {
            var HighVoltyageModel = ViewModel.Model.HighVoltyageModel;

            //受电弓
            HighVoltyageModel.PantographUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));

            //LCB
            HighVoltyageModel.LCBUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));

            //高速断路器
            HighVoltyageModel.QuickBreakUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));

            //接地开关
            HighVoltyageModel.GroundingUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));

            //电压
            args.ChangedFloats.UpdateIfContains(InFolatKey.车7电压, f => HighVoltyageModel.Voltage1 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车2电压, f => HighVoltyageModel.Voltage2 = f);

            //电流
            args.ChangedFloats.UpdateIfContains(InFolatKey.车7电流, f => HighVoltyageModel.Electricity1 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车2电流, f => HighVoltyageModel.Electricity2 = f);
        }

        private void UpdateDatasDoorModel(object sender, CommunicationDataChangedArgs args)
        {
            var DoorModel = ViewModel.Model.DoorModel;
            //外门
            DoorModel.DoorUnits.ForEach(b=>b.DataChanged(args.ChangedBools.NewValue));
        }

        private void UpdateDatasStationModel(object sender, CommunicationDataChangedArgs args)
        {
            var StationModel = ViewModel.Model.StationModel;

            //司机室激活
            StationModel.TrainModel.MC00 = VOBCState.NoActivate;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车0司机室激活, () => StationModel.TrainModel.MC00 = VOBCState.Activate);

            StationModel.TrainModel.MC01 = VOBCState.NoActivate;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1司机室激活, () => StationModel.TrainModel.MC01 = VOBCState.Activate);

            //车辆状态
            //车辆0状态
            StationModel.TrainModel.CarModel0.State = CarState.None;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车0车辆正常, () => StationModel.TrainModel.CarModel0.State = CarState.StationNormal);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车0车辆超载, () => StationModel.TrainModel.CarModel0.State = CarState.StationOverLoad);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车0车辆严重超载, () => StationModel.TrainModel.CarModel0.State = CarState.StationSeriousOverLoad);
            //车辆7状态
            StationModel.TrainModel.CarModel7.State = CarState.None;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车7车辆正常, () => StationModel.TrainModel.CarModel7.State = CarState.StationNormal);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车7车辆超载, () => StationModel.TrainModel.CarModel7.State = CarState.StationOverLoad);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车7车辆严重超载, () => StationModel.TrainModel.CarModel7.State = CarState.StationSeriousOverLoad);
            //车辆6状态
            StationModel.TrainModel.CarModel6.State = CarState.None;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车6车辆正常, () => StationModel.TrainModel.CarModel6.State = CarState.StationNormal);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车6车辆超载, () => StationModel.TrainModel.CarModel6.State = CarState.StationOverLoad);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车6车辆严重超载, () => StationModel.TrainModel.CarModel6.State = CarState.StationSeriousOverLoad);
            //车辆5状态
            StationModel.TrainModel.CarModel5.State = CarState.None;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车5车辆正常, () => StationModel.TrainModel.CarModel5.State = CarState.StationNormal);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车5车辆超载, () => StationModel.TrainModel.CarModel5.State = CarState.StationOverLoad);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车5车辆严重超载, () => StationModel.TrainModel.CarModel5.State = CarState.StationSeriousOverLoad);
            //车辆4状态
            StationModel.TrainModel.CarModel4.State = CarState.None;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车4车辆正常, () => StationModel.TrainModel.CarModel4.State = CarState.StationNormal);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车4车辆超载, () => StationModel.TrainModel.CarModel4.State = CarState.StationOverLoad);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车4车辆严重超载, () => StationModel.TrainModel.CarModel4.State = CarState.StationSeriousOverLoad);
            //车辆3状态
            StationModel.TrainModel.CarModel3.State = CarState.None;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车3车辆正常, () => StationModel.TrainModel.CarModel3.State = CarState.StationNormal);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车3车辆超载, () => StationModel.TrainModel.CarModel3.State = CarState.StationOverLoad);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车3车辆严重超载, () => StationModel.TrainModel.CarModel3.State = CarState.StationSeriousOverLoad);
            //车辆2状态
            StationModel.TrainModel.CarModel2.State = CarState.None;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车2车辆正常, () => StationModel.TrainModel.CarModel2.State = CarState.StationNormal);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车2车辆超载, () => StationModel.TrainModel.CarModel2.State = CarState.StationOverLoad);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车2车辆严重超载, () => StationModel.TrainModel.CarModel2.State = CarState.StationSeriousOverLoad);
            //车辆1状态
            StationModel.TrainModel.CarModel1.State = CarState.None;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1车辆正常, () => StationModel.TrainModel.CarModel1.State = CarState.StationNormal);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1车辆超载, () => StationModel.TrainModel.CarModel1.State = CarState.StationOverLoad);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1车辆严重超载, () => StationModel.TrainModel.CarModel1.State = CarState.StationSeriousOverLoad);

            //门状态
            StationModel.StationUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));

            //乘客紧急对讲单元
            args.ChangedBools.UpdateIfContains(InBoolKey.车0乘客紧急对讲单元激活, b => StationModel.PassengerTalk0 = b);
            args.ChangedBools.UpdateIfContains(InBoolKey.车7乘客紧急对讲单元激活, b => StationModel.PassengerTalk7 = b);
            args.ChangedBools.UpdateIfContains(InBoolKey.车6乘客紧急对讲单元激活, b => StationModel.PassengerTalk6 = b);
            args.ChangedBools.UpdateIfContains(InBoolKey.车5乘客紧急对讲单元激活, b => StationModel.PassengerTalk5 = b);
            args.ChangedBools.UpdateIfContains(InBoolKey.车4乘客紧急对讲单元激活, b => StationModel.PassengerTalk4 = b);
            args.ChangedBools.UpdateIfContains(InBoolKey.车3乘客紧急对讲单元激活, b => StationModel.PassengerTalk3 = b);
            args.ChangedBools.UpdateIfContains(InBoolKey.车2乘客紧急对讲单元激活, b => StationModel.PassengerTalk2 = b);
            args.ChangedBools.UpdateIfContains(InBoolKey.车1乘客紧急对讲单元激活, b => StationModel.PassengerTalk1 = b);
        }

        private void UpdateDatasActivateModel(object sender, CommunicationDataChangedArgs args)
        {
            var ActivateModel = ViewModel.Model.ActivateModel;

            //受电弓
            ActivateModel.PantographUnits.ForEach(b=>b.DataChanged(args.ChangedBools.NewValue));

            //LCB
            ActivateModel.LCBUnits.ForEach(b=>b.DataChanged(args.ChangedBools.NewValue));

            //逆变器
            ActivateModel.InverterState00 = InverterState.TurnOff;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车0逆变器工作, () => ActivateModel.InverterState00 = InverterState.TurnOn);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车0逆变器停止, () => ActivateModel.InverterState00 = InverterState.TurnOff);

            ActivateModel.InverterState06 = InverterState.TurnOff;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车6逆变器工作, () => ActivateModel.InverterState06 = InverterState.TurnOn);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车6逆变器停止, () => ActivateModel.InverterState06 = InverterState.TurnOff);

            ActivateModel.InverterState03 = InverterState.TurnOff;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车3逆变器工作, () => ActivateModel.InverterState03 = InverterState.TurnOn);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车3逆变器停止, () => ActivateModel.InverterState03 = InverterState.TurnOff);

            ActivateModel.InverterState01 = InverterState.TurnOff;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1逆变器工作, () => ActivateModel.InverterState01 = InverterState.TurnOn);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1逆变器停止, () => ActivateModel.InverterState01 = InverterState.TurnOff);


            //充电机
            ActivateModel.BatteryChargerState05 = BatteryChargerState.None;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车5充电机未知, () => ActivateModel.BatteryChargerState05 = BatteryChargerState.None);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车5充电机工作, () => ActivateModel.BatteryChargerState05 = BatteryChargerState.TurnOn);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车5充电机停止, () => ActivateModel.BatteryChargerState05 = BatteryChargerState.TurnOff);

            ActivateModel.BatteryChargerState04 = BatteryChargerState.None;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车4充电机未知, () => ActivateModel.BatteryChargerState04 = BatteryChargerState.None);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车4充电机工作, () => ActivateModel.BatteryChargerState04 = BatteryChargerState.TurnOn);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车4充电机停止, () => ActivateModel.BatteryChargerState04 = BatteryChargerState.TurnOff);
        }
    }
}
