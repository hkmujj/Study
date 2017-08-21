using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Windows.Threading;
using DevExpress.XtraPrinting.Native;
using Engine.TCMS.HXD3.Event;
using Engine.TCMS.HXD3.Extension;
using Engine.TCMS.HXD3.Model;
using Engine.TCMS.HXD3.Model.ConfigModel;
using Engine.TCMS.HXD3.Model.Interface;
using Engine.TCMS.HXD3.Model.TCMS.Domain;
using Engine.TCMS.HXD3.Model.TCMS.Domain.Constant;
using Engine.TCMS.HXD3.Resource.Keys;
using Engine.TCMS.HXD3.ViewModel;
using Engine.TCMS.HXD3.ViewModel.Domain;
using Engine.TCMS.HXD3.ViewModel.Domain.Detail;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Service;

namespace Engine.TCMS.HXD3.DataAdapter
{
    [Export]
    public class HXD3Adapter : IDataListener
    {
        [DebuggerStepThrough]
        [ImportingConstructor]
        public HXD3Adapter(HXD3TCMSViewModel tcmsModel, IEventAggregator eventAggregator)
        {
            m_EventAggregator = eventAggregator;
            tcmsModel.Controller.DataSender = new DataSender();
            TCMS = tcmsModel.TCMS;
            ViewModel = tcmsModel;

            eventAggregator.GetEvent<OpenEvent>().Subscribe(OnOpenStateEvent);
            eventAggregator.GetEvent<StartupTestRunEvent>().Subscribe(OnStartupTestRunEvent);

            GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<ICourseService>().CourseStateChanged +=
                OnCourseStateChanged;

        }


        [ImportMany] private List<Lazy<IResetSupport>> m_ResetObjs;

        protected ICommunicationDataReadService DataReadService
        {
            get { return GlobalParam.Instance.InitParam.CommunicationDataService.ReadService; }
        }

        protected ICommunicationDataWriteService DataWriteService
        {
            get { return GlobalParam.Instance.InitParam.CommunicationDataService.WriteService; }
        }

        // ReSharper disable once InconsistentNaming
        protected IEventAggregator m_EventAggregator;

        private UpdateViewAsyc m_UpdateViewAsycEvent;

        // ReSharper disable once NotAccessedField.Local
        private DispatcherTimer m_UpdateTimeTimer;

        protected IProjectIndexDescriptionConfig IndexDescription { private set; get; }

        public HXD3TCMSViewModel ViewModel { private set; get; }

        public TCMSViewModel TCMS { private set; get; }

        public void Initalize(bool needDataMonitor = true)
        {
            IndexDescription = GlobalParam.Instance.IndexDescription;
            m_UpdateViewAsycEvent = m_EventAggregator.GetEvent<UpdateViewAsyc>();
            m_UpdateViewAsycEvent.Subscribe(s => OnUpdateViewModels(s.DataChangedArgs), ThreadOption.UIThread, true);
            GlobalParam.Instance.InitParam.RegistDataListener(this);
            m_UpdateTimeTimer = new DispatcherTimer(new TimeSpan(0, 0, 0, 1), DispatcherPriority.Background, UpdateTime,
                Dispatcher.CurrentDispatcher);
        }

        private void UpdateTime(object sender, EventArgs e)
        {
            TCMS.Parent.Model.Other.SimTime = DateTime.Now;
        }

        /// <summary>bool 值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
        }

        /// <summary>float值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            
        }

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
            ReadServiceOnDataChanged(sender, dataChangedArgs);
        }

        private void OnCourseStateChanged(object sender, CourseStateChangedArgs courseStateChangedArgs)
        {
            if (courseStateChangedArgs.OldCourseState == CourseState.Stoped)
            {
                m_ResetObjs.ForEach(e => e.Value.Reset());
            }
        }

        private void OnUpdateViewModels(CommunicationDataChangedArgs args)
        {
            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_亮屏, b => TCMS.Visible = b);

            UpdateTestRun(args);
            UpdateDriver(args);
            UpdateTowEleAsync(args);
            UpdateAxis(args, TCMS.TrainViewModel);
            UpdatePantogroph(args);
            UpdateAirMachine(args);
            UpdateSwitch(args);
            UpdateMainData(args);
            UpdateTrain(args);
            UpdateTowBrake(args);
            UpdateOpenStates(args);
            UpdateSignalInfo(args);
            UpdateAssistPower(args);
            UpdateAirBrake(args);
            UpdateTest(args);
            UpdateFault(args);
        }

        private void UpdateTest(CommunicationDataChangedArgs args)
        {
            UpdateStartupTest(args);
            UpdateMainControlTest(args);
            UpdataAssistPowerTest(args);
            UpdateZeroLevelTest(args);
            UpdateDisplayLightTest(args);
        }

        private void UpdateDisplayLightTest(CommunicationDataChangedArgs args)
        {
            if (ViewModel.TCMS.DisplayLightViewModel.State == TestState.Started)
            {
                DataWriteService.ChangeBool(IndexDescription.OutBoolDescriptionDictionary[Oub.测试画面_显示灯实验确认键], true);
                bool isended;
                ViewModel.TCMS.DisplayLightViewModel.Tips =
                    GetTestContent(GlobalParam.Instance.DisplayLightTestCollection,
                        out isended);

                if (isended)
                {
                    ViewModel.TCMS.DisplayLightViewModel.State = TestState.Ended;
                    DataWriteService.ChangeBool(IndexDescription.OutBoolDescriptionDictionary[Oub.测试画面_显示灯实验确认键], false);
                }
            }
        }

        private void UpdateZeroLevelTest(CommunicationDataChangedArgs args)
        {
            if (ViewModel.TCMS.ZeroLevelTestViewModel.State == TestState.Started)
            {
                DataWriteService.ChangeBool(IndexDescription.OutBoolDescriptionDictionary[Oub.测试画面_0级位实验确认键], true);

                ViewModel.TCMS.ZeroLevelTestViewModel.TestResult1 = GetTestResult(Inb.零级位试验画面_CI1异常,
                    Inb.零级位试验画面_CI1正常);
                ViewModel.TCMS.ZeroLevelTestViewModel.TestResult2 = GetTestResult(Inb.零级位试验画面_CI2异常,
                    Inb.零级位试验画面_CI2正常);
                ViewModel.TCMS.ZeroLevelTestViewModel.TestResult3 = GetTestResult(Inb.零级位试验画面_CI3异常,
                    Inb.零级位试验画面_CI3正常);
                ViewModel.TCMS.ZeroLevelTestViewModel.TestResult4 = GetTestResult(Inb.零级位试验画面_CI4异常,
                    Inb.零级位试验画面_CI4正常);
                ViewModel.TCMS.ZeroLevelTestViewModel.TestResult5 = GetTestResult(Inb.零级位试验画面_CI5异常,
                    Inb.零级位试验画面_CI5正常);

                bool isended;
                ViewModel.TCMS.ZeroLevelTestViewModel.Tips = GetTestContent(
                    GlobalParam.Instance.ZeroLevelTestCollection,
                    out isended);

                if (isended && ViewModel.TCMS.ZeroLevelTestViewModel.TestResult1 != TestResult.Unkown)
                {
                    DataWriteService.ChangeBool(IndexDescription.OutBoolDescriptionDictionary[Oub.测试画面_0级位实验确认键], false);
                    ViewModel.TCMS.ZeroLevelTestViewModel.State = TestState.Ended;
                }
            }
        }

        private void UpdataAssistPowerTest(CommunicationDataChangedArgs args)
        {
            if (ViewModel.TCMS.AssistPowerTestViewModel.State == TestState.Started)
            {
                DataWriteService.ChangeBool(IndexDescription.OutBoolDescriptionDictionary[Oub.测试画面_辅助电源实验确认键], true);
                ViewModel.TCMS.AssistPowerTestViewModel.APU1.TestResult = GetTestResult(Inb.辅助电源装置试验画面_辅助电源装置1异常,
                    Inb.辅助电源装置试验画面_辅助电源装置1正常);

                ViewModel.TCMS.AssistPowerTestViewModel.APU2.TestResult = GetTestResult(Inb.辅助电源装置试验画面_辅助电源装置2异常,
                    Inb.辅助电源装置试验画面_辅助电源装置2正常);

                bool isended;
                ViewModel.TCMS.AssistPowerTestViewModel.Tips =
                    GetTestContent(GlobalParam.Instance.AssistPowerTestCollection,
                        out isended);

                if (isended && ViewModel.TCMS.AssistPowerTestViewModel.APU1.TestResult != TestResult.Unkown)
                {
                    ViewModel.TCMS.AssistPowerTestViewModel.State = TestState.Ended;
                    ViewModel.TCMS.AssistPowerTestViewModel.APU1.Visible = true;
                    ViewModel.TCMS.AssistPowerTestViewModel.APU2.Visible = true;
                    ViewModel.TCMS.AssistPowerTestViewModel.APU1.APU.CopyDataFrom(
                        ViewModel.TCMS.AssitPowerViewModel.Apus[0]);
                    ViewModel.TCMS.AssistPowerTestViewModel.APU2.APU.CopyDataFrom(
                        ViewModel.TCMS.AssitPowerViewModel.Apus[1]);
                    DataWriteService.ChangeBool(IndexDescription.OutBoolDescriptionDictionary[Oub.测试画面_辅助电源实验确认键], false);
                }
            }
        }

        private void UpdateMainControlTest(CommunicationDataChangedArgs args)
        {
            if (ViewModel.TCMS.MasterDriverControlViewModel.State == TestState.Started)
            {
                DataWriteService.ChangeBool(IndexDescription.OutBoolDescriptionDictionary[Oub.测试画面_主司机控制器实验确认键], true);

                ViewModel.TCMS.MasterDriverControlViewModel.Result1 = GetTestResult(Inb.主司控器实验画面_项目1结果异常,
                    Inb.主司控器实验画面_项目1结果正常);

                ViewModel.TCMS.MasterDriverControlViewModel.Result2 = GetTestResult(Inb.主司控器实验画面_项目2结果异常,
                    Inb.主司控器实验画面_项目2结果正常);

                bool isended;
                ViewModel.TCMS.MasterDriverControlViewModel.Tips =
                    GetTestContent(GlobalParam.Instance.MasterDriverControlUnitCollection,
                        out isended);

                if (isended && ViewModel.TCMS.MasterDriverControlViewModel.Result1 != TestResult.Unkown)
                {
                    ViewModel.TCMS.MasterDriverControlViewModel.State = TestState.Ended;
                    DataWriteService.ChangeBool(IndexDescription.OutBoolDescriptionDictionary[Oub.测试画面_主司机控制器实验确认键],
                        false);
                }
            }
        }

        private void UpdateStartupTest(CommunicationDataChangedArgs args)
        {
            if (ViewModel.TCMS.StartTestViewModel.State == TestState.Started)
            {
                DataWriteService.ChangeBool(IndexDescription.OutBoolDescriptionDictionary[Oub.测试画面_启动实验确认键], true);

                ViewModel.TCMS.StartTestViewModel.ResultCI1 = GetTestResult(Inb.启动试验画面_CI1异常, Inb.启动试验画面_CI1正常);
                ViewModel.TCMS.StartTestViewModel.ResultCI2 = GetTestResult(Inb.启动试验画面_CI2异常, Inb.启动试验画面_CI2正常);
                ViewModel.TCMS.StartTestViewModel.ResultCI3 = GetTestResult(Inb.启动试验画面_CI3异常, Inb.启动试验画面_CI3正常);
                ViewModel.TCMS.StartTestViewModel.ResultCI4 = GetTestResult(Inb.启动试验画面_CI4异常, Inb.启动试验画面_CI4正常);
                ViewModel.TCMS.StartTestViewModel.ResultCI5 = GetTestResult(Inb.启动试验画面_CI5异常, Inb.启动试验画面_CI5正常);
                ViewModel.TCMS.StartTestViewModel.ResultCI6 = GetTestResult(Inb.启动试验画面_CI6异常, Inb.启动试验画面_CI6正常);

                bool isended;
                ViewModel.TCMS.StartTestViewModel.Tips = GetTestContent(GlobalParam.Instance.StartTestUnitCollection,
                    out isended);

                if (isended && ViewModel.TCMS.StartTestViewModel.ResultCI1 != TestResult.Unkown)
                {
                    ViewModel.TCMS.StartTestViewModel.CuurentCI1 =
                        ViewModel.TCMS.TowEleMachineViewModel.TowEleMachineCollection[1].Values[0].ToString("0");
                    ViewModel.TCMS.StartTestViewModel.CuurentCI2 =
                        ViewModel.TCMS.TowEleMachineViewModel.TowEleMachineCollection[1].Values[1].ToString("0");
                    ViewModel.TCMS.StartTestViewModel.CuurentCI3 =
                        ViewModel.TCMS.TowEleMachineViewModel.TowEleMachineCollection[1].Values[2].ToString("0");
                    ViewModel.TCMS.StartTestViewModel.CuurentCI4 =
                        ViewModel.TCMS.TowEleMachineViewModel.TowEleMachineCollection[1].Values[3].ToString("0");
                    ViewModel.TCMS.StartTestViewModel.CuurentCI5 =
                        ViewModel.TCMS.TowEleMachineViewModel.TowEleMachineCollection[1].Values[4].ToString("0");
                    ViewModel.TCMS.StartTestViewModel.CuurentCI6 =
                        ViewModel.TCMS.TowEleMachineViewModel.TowEleMachineCollection[1].Values[5].ToString("0");

                    ViewModel.TCMS.StartTestViewModel.State = TestState.Ended;
                    DataWriteService.ChangeBool(IndexDescription.OutBoolDescriptionDictionary[Oub.测试画面_启动实验确认键], false);
                }
            }
        }

        private string GetTestContent(IEnumerable<TestUnitBase> testUnitBases, out bool isended)
        {
            isended = false;

            var idx = 0;
            foreach (var unitBase in testUnitBases)
            {
                if (DataReadService.GetInBoolOf(unitBase.LogicKey))
                {
                    if (idx == 0)
                    {
                        isended = true;
                    }
                    return unitBase.Content;
                }

                ++ idx;
            }

            return "";
        }

        private TestResult GetTestResult(string abnormal, string normal)
        {
            if (DataReadService.GetInBoolOf(abnormal))
            {
                return TestResult.Abnormal;
            }

            if (DataReadService.GetInBoolOf(normal))
            {
                return TestResult.Normal;
            }

            return TestResult.Unkown;
        }

        private void UpdateTestRun(CommunicationDataChangedArgs args)
        {
            args.ChangedFloats.UpdateIfContains(Inf.试运行画面_加速度, f => TCMS.TestRunViewModel.Accelerated = f);
            args.ChangedFloats.UpdateIfContains(Inf.试运行画面_走行距离, f => TCMS.TestRunViewModel.WalkDistance = f);
            args.ChangedFloats.UpdateIfContains(Inf.试运行画面_走行时间, f => TCMS.TestRunViewModel.RecordSecond = f);
        }

        private void OnOpenStateEvent(OpenEvent.EventArgs eventArgs)
        {
            foreach (var it in ViewModel.TCMS.OpenStateViewModel.StateItems)
            {
                DataWriteService.ChangeBool(IndexDescription.OutBoolDescriptionDictionary[it.Config.OutIndex],
                    it.IsSelected);
            }
        }

        private void OnStartupTestRunEvent(StartupTestRunEvent.EventArgs obj)
        {
            DataWriteService.ChangeBool(IndexDescription.OutBoolDescriptionDictionary[Oub.主界面_试运行_起动键], obj.IsSet);
        }

        private void UpdatePantogroph(CommunicationDataChangedArgs args)
        {
            var train = TCMS.TrainViewModel;

            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_受电弓1升,
                b => train.Cars[0].PantographState = b ? PantographState.Up : PantographState.Down);
            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_受电弓2升,
                b => train.Cars[1].PantographState = b ? PantographState.Up : PantographState.Down);
        }

        private void UpdateAirBrake(CommunicationDataChangedArgs args)
        {
            // TODO 
            var ab = TCMS.AirBrakeViewModel;

            var state = AirBrakeItemState.Unkown;
            args.ChangedBools.UpdateIfContains(Inb.空置状态画面_停车闸绿, b => state = b ? AirBrakeItemState.Normal : state);
            args.ChangedBools.UpdateIfContains(Inb.空置状态画面_停车闸红, b => state = b ? AirBrakeItemState.Fault : state);

            ab.PackingValve = UpdateAirBrakeItemState(args, Inb.空置状态画面_停车闸绿, Inb.空置状态画面_停车闸红, ab.PackingValve);
            ab.Sand = UpdateAirBrakeItemState(args, Inb.空置状态画面_撒沙绿, Inb.空置状态画面_撒沙红, ab.Sand);
            ab.Pantograph = UpdateAirBrakeItemState(args, Inb.空置状态画面_受电弓绿, Inb.空置状态画面_受电弓红, ab.Pantograph);
            ab.ValveCrock = UpdateAirBrakeItemState(args, Inb.空置状态画面_闸缸绿, Inb.空置状态画面_闸缸红, ab.ValveCrock);
            ab.Compressor1 = UpdateAirBrakeItemState(args, Inb.空置状态画面_CMP1绿, Inb.空置状态画面_CMP1红, ab.Compressor1);
            ab.Compressor2 = UpdateAirBrakeItemState(args, Inb.空置状态画面_CMP2绿, Inb.空置状态画面_CMP2红, ab.Compressor2);
        }

        private AirBrakeItemState UpdateAirBrakeItemState(CommunicationDataChangedArgs args, string normal, string fault,
            AirBrakeItemState old)
        {
            var state = old;

            var ni = IndexDescription.InBoolDescriptionDictionary[normal];
            var fi = IndexDescription.InBoolDescriptionDictionary[fault];

            if (args.ChangedBools.NewValue.ContainsKey(ni) || args.ChangedBools.NewValue.ContainsKey(fi))
            {
                if (DataReadService.GetBoolAt(fi))
                {
                    state = AirBrakeItemState.Fault;
                }
                else if (DataReadService.GetBoolAt(ni))
                {
                    state = AirBrakeItemState.Normal;
                }
                else
                {
                    state = AirBrakeItemState.Unkown;
                }
            }
            return state;
        }

        private void UpdateAssistPower(CommunicationDataChangedArgs args)
        {
            var aps = TCMS.AssitPowerViewModel.Apus;
            args.ChangedFloats.UpdateIfContains(Inf.辅助电源画面_辅助变流器1输出电压左, f => aps[0].OutputVoltage = f);
            args.ChangedFloats.UpdateIfContains(Inf.辅助电源画面_辅助变流器1输出电流左, f => aps[0].OutputCurrent = f);
            args.ChangedFloats.UpdateIfContains(Inf.辅助电源画面_辅助变流器1输出频率左, f => aps[0].OutputFreq = f);
            args.ChangedBools.UpdateIfContains(Inb.辅助电源画面_APU1_OFF红, b => aps[0].State = b ? APUWorkState.OFF : APUWorkState.ON);
            
            args.ChangedFloats.UpdateIfContains(Inf.辅助电源画面_辅助变流器2输出电压右, f => aps[1].OutputVoltage = f);
            args.ChangedFloats.UpdateIfContains(Inf.辅助电源画面_辅助变流器2输出电流右, f => aps[1].OutputCurrent = f);
            args.ChangedFloats.UpdateIfContains(Inf.辅助电源画面_辅助变流器2输出频率右, f => aps[1].OutputFreq = f);
            args.ChangedBools.UpdateIfContains(Inb.辅助电源画面_APU2_OFF红, b => aps[1].State = b ? APUWorkState.OFF : APUWorkState.ON);

            var apv = TCMS.AssitPowerViewModel;
            args.ChangedBools.UpdateIfContains(Inb.辅助电源画面_PSU1, b => apv.Psu1 = b);
            args.ChangedBools.UpdateIfContains(Inb.辅助电源画面_PSU2, b => apv.Psu2 = b);

            args.ChangedBools.UpdateIfContains(Inb.辅助电源画面_绿色连接左, b => TCMS.AssitPowerViewModel.TouchMachineLeft = b);
            args.ChangedBools.UpdateIfContains(Inb.辅助电源画面_绿色连接上, b => TCMS.AssitPowerViewModel.TouchMachineMiddle = b);
            args.ChangedBools.UpdateIfContains(Inb.辅助电源画面_绿色连接右, b => TCMS.AssitPowerViewModel.TouchMachineRight = b);
        }

        private void UpdateOpenStates(CommunicationDataChangedArgs args)
        {
            TCMS.OpenStateViewModel.StateItems.ForEach(
                e =>
                    args.ChangedBools.UpdateIfContains(e.Config.InIndex,
                        b => e.State = b ? OpenStateState.Open : OpenStateState.Normal));
        }

        private void UpdateSignalInfo(CommunicationDataChangedArgs args)
        {
            var si = TCMS.SignalInfoViewModel;
            foreach (var unit in si.AllSignalInfo)
            {
                args.ChangedBools.UpdateIfContains(unit.SignalOpen,
                    b => unit.State = b ? SignalInfoState.Open : SignalInfoState.Normal);
            }
        }

        private void UpdateFault(CommunicationDataChangedArgs args)
        {
            var occuse =
                args.ChangedBools.NewValue.Keys.Intersect(GlobalParam.Instance.FaultItemConfigs.Select(s => s.LogicIndex));
            foreach (var it in occuse)
            {
                if (args.ChangedBools.NewValue[it])
                {
                    TCMS.FaultViewModel.FaultItemCollection.Insert(0,
                        new FaultItem(GlobalParam.Instance.FaultItemConfigs.Find(f => f.LogicIndex == it))
                        {
                            OccuseDateTime = ViewModel.Model.Other.ShowTime,
                            Speed = TCMS.MainDataViewModel.CurrentSpeed,
                            Level = TCMS.MainDataViewModel.LevelValue,
                        });
                }
                else
                {
                    var fault = TCMS.FaultViewModel.FaultItemCollection.Where(w => w.ItemConfig.LogicIndex == it);
                    fault.ForEach(e => e.ResumeDateTime = ViewModel.Model.Other.ShowTime);
                }
            }
        }

        private void ReadServiceOnDataChanged(object sender, CommunicationDataChangedArgs args)
        {
            //OnUpdateViewModels(args);
            m_UpdateViewAsycEvent.Publish(new UpdateViewAsyc.EventArgs(args));
        }

        private void UpdateTowBrake(CommunicationDataChangedArgs args)
        {
            var tb = TCMS.TowBrakeViewModel;
            args.ChangedFloats.UpdateIfContains(Inf.原边电流A, f => tb.RawSideCurrent = f);
            args.ChangedFloats.UpdateIfContains(Inf.网压原边电压kv, f => tb.RawSideVoltage = f);
            args.ChangedFloats.UpdateIfContains(Inf.控制电压V, f => tb.ControlVoltage = f);
            args.ChangedFloats.UpdateIfContains(Inf.主变流器_牵引电机画面_牵引_制动kN01, f => tb.TowBrake1 = f);
            args.ChangedFloats.UpdateIfContains(Inf.主变流器_牵引电机画面_牵引_制动kN02, f => tb.TowBrake2 = f);
            args.ChangedFloats.UpdateIfContains(Inf.主变流器_牵引电机画面_牵引_制动kN03, f => tb.TowBrake3 = f);
            args.ChangedFloats.UpdateIfContains(Inf.主变流器_牵引电机画面_牵引_制动kN04, f => tb.TowBrake4 = f);
            args.ChangedFloats.UpdateIfContains(Inf.主变流器_牵引电机画面_牵引_制动kN05, f => tb.TowBrake5 = f);
            args.ChangedFloats.UpdateIfContains(Inf.主变流器_牵引电机画面_牵引_制动kN06, f => tb.TowBrake6 = f);
        }

        private void UpdateTrain(CommunicationDataChangedArgs args)
        {
            var train = TCMS.TrainViewModel;

            UpdateDirection(args, train);

        }

        private void UpdateDriver(CommunicationDataChangedArgs args)
        {
            var train = TCMS.TrainViewModel;
            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_1端, b => train.Cars[0].IsDriver = b);
            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_2端, b => train.Cars[1].IsDriver = b);


        }

        private void UpdateDirection(CommunicationDataChangedArgs args, TrainViewModel trainViewModel)
        {
            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_黄色箭头向左, b =>
            {
                if (b)
                {
                    trainViewModel.RunDirection = RunDirection.Left;
                }
            });
            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_黄色箭头向右, b =>
            {
                if (b)
                {
                    trainViewModel.RunDirection = RunDirection.Right;
                }
            });
        }

        private void UpdateAxis(CommunicationDataChangedArgs args, TrainViewModel trainViewModel)
        {
            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_轴1变成紫色,
                b => trainViewModel.Cars[0].AxisStates[0] = b ? AxisState.Violet : AxisState.Yellow);
            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_轴2变成紫色,
                b => trainViewModel.Cars[0].AxisStates[1] = b ? AxisState.Violet : AxisState.Blue);
            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_轴3变成紫色,
                b => trainViewModel.Cars[0].AxisStates[2] = b ? AxisState.Violet : AxisState.Green);
            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_轴4变成紫色,
                b => trainViewModel.Cars[1].AxisStates[0] = b ? AxisState.Violet : AxisState.Yellow);
            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_轴5变成紫色,
                b => trainViewModel.Cars[1].AxisStates[1] = b ? AxisState.Violet : AxisState.Blue);
            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_轴6变成紫色,
                b => trainViewModel.Cars[1].AxisStates[2] = b ? AxisState.Violet : AxisState.Green);
        }

        private void UpdateMainData(CommunicationDataChangedArgs args)
        {
            var data = TCMS.MainDataViewModel;
            args.ChangedFloats.UpdateIfContains(Inf.速度, f => data.CurrentSpeed = f);
            args.ChangedFloats.UpdateIfContains(Inf.牵引制动级位, f => data.LevelValue = f);
            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_定速,
                b => data.ConstantSpeedState = b ? ConstantSpeedState.Normal : ConstantSpeedState.None);

            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_停放制动黄色闪烁, b => data.IsPackingBraking = b);

            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_主断分, b =>
            {
                if (b)
                {
                    data.MainSwitchState = MainSwitchState.SwitchOff;
                }
            });
            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_主断合, b =>
            {
                if (b)
                {
                    data.MainSwitchState = MainSwitchState.SwitchOn;
                }
            });

            UpdateWorkState(args, data);

            UpdateNobodyWarningLevel(args, data);

            UpdateMainSwitchNotification(args);
        }

        private void UpdateMainSwitchNotification(CommunicationDataChangedArgs args)
        {
            TCMS.MainDataViewModel.MainSwitchNotification = string.Empty;

            foreach (var notifyConfig in GlobalParam.Instance.MainControlNotifyConfigCollection)
            {
                if (DataReadService.GetBoolAt(notifyConfig.LogicKey))
                {
                    TCMS.MainDataViewModel.MainSwitchNotification = notifyConfig.Content;
                    return;
                }
            }
        }

        private void UpdateWorkState(CommunicationDataChangedArgs args, MainDataViewModel dataViewModel)
        {
            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_牵引, b =>
            {
                if (b)
                {
                    dataViewModel.TowBrakeWorkState = TowBrakeWorkState.Tow;
                }
            });
            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_制动, b =>
            {
                if (b)
                {
                    dataViewModel.TowBrakeWorkState = TowBrakeWorkState.Brake;
                }
            });
            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_惰行, b =>
            {
                if (b)
                {
                    dataViewModel.TowBrakeWorkState = TowBrakeWorkState.Slow;
                }
            });
        }

        private void UpdateNobodyWarningLevel(CommunicationDataChangedArgs args, MainDataViewModel dataViewModel)
        {
            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_无人警惕黑底白字, b =>
            {
                if (b)
                {
                    dataViewModel.NobodyWarningLevel = NobodyWarningLevel.None;
                }
            });
            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_无人警惕绿色, b =>
            {
                if (b)
                {
                    dataViewModel.NobodyWarningLevel = NobodyWarningLevel.Green;
                }
            });
            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_无人警惕黄色闪烁, b =>
            {
                if (b)
                {
                    dataViewModel.NobodyWarningLevel = NobodyWarningLevel.YellowFlick;
                }
            });
            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_无人警惕红色闪烁, b =>
            {
                if (b)
                {
                    dataViewModel.NobodyWarningLevel = NobodyWarningLevel.RedFlick;
                }
            });
            args.ChangedBools.UpdateIfContains(Inb.主界面_牵引制动画面_无人警惕红色, b =>
            {
                if (b)
                {
                    dataViewModel.NobodyWarningLevel = NobodyWarningLevel.Red;
                }
            });
        }

        private void UpdateTowEleAsync(CommunicationDataChangedArgs args)
        {
            foreach (var item in TCMS.TowEleMachineViewModel.TowEleMachineCollection.Take(4))
            {
                item.Values[0] = GetInFloatValue(item.Config.IndexName1);
                item.Values[1] = GetInFloatValue(item.Config.IndexName2);
                item.Values[2] = GetInFloatValue(item.Config.IndexName3);
                item.Values[3] = GetInFloatValue(item.Config.IndexName4);
                item.Values[4] = GetInFloatValue(item.Config.IndexName5);
                item.Values[5] = GetInFloatValue(item.Config.IndexName6);
            }

            foreach (var item in TCMS.TowEleMachineViewModel.TowEleMachineCollection.Skip(4))
            {
                item.States[0] = GetInBoolValue(item.Config.IndexName1) ? TochMahineState.Work : TochMahineState.NotWork;
                item.States[1] = GetInBoolValue(item.Config.IndexName2) ? TochMahineState.Work : TochMahineState.NotWork;
                item.States[2] = GetInBoolValue(item.Config.IndexName3) ? TochMahineState.Work : TochMahineState.NotWork;
                item.States[3] = GetInBoolValue(item.Config.IndexName4) ? TochMahineState.Work : TochMahineState.NotWork;
                item.States[4] = GetInBoolValue(item.Config.IndexName5) ? TochMahineState.Work : TochMahineState.NotWork;
                item.States[5] = GetInBoolValue(item.Config.IndexName6) ? TochMahineState.Work : TochMahineState.NotWork;
            }
        }

        private void UpdateSwitch(CommunicationDataChangedArgs args)
        {
            TCMS.SwitchStateViewModel.SwitchItems.Where(w => w.Config.Index != NameIndex.InvalidateValue)
                .ForEach(e => e.State = GetSwitchItemState(e.Config));
        }

        private SwitchItemState GetSwitchItemState(SwitchStateConfig config)
        {
            var state = SwitchItemState.None;

            if (DataReadService.GetInBoolOf(config.IndexRed))
            {
                state = SwitchItemState.Red;
            }
            else if(DataReadService.GetInBoolOf(config.Index))
            {
                state = SwitchItemState.Blue;
            }
            return state;
        }

        private void UpdateAirMachine(CommunicationDataChangedArgs args)
        {
            TCMS.WindMachineStateViewModel.WindMachineItems.ForEach(e => e.Value = GetInBoolValue(e.Config.Index));
        }

        protected bool GetInBoolValue(string indexName)
        {
            return DataReadService.GetInBoolOf(indexName);
        }

        protected float GetInFloatValue(string indexName)
        {
            return DataReadService.GetInFloatOf(indexName);
        }

   
    }
}