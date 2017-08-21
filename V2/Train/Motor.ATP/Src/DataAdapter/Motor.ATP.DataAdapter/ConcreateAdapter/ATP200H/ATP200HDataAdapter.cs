using System;
using System.Globalization;
using System.Linq;
using System.Text;
using CommonUtil.Controls;
using MMI.Facility.Interface.Data;
using Motor.ATP.DataAdapter.Extension;
using Motor.ATP.DataAdapter.Resource.Keys;
using Motor.ATP.Infrasturcture.Control.Infomation;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.BrakeTest;
using Motor.ATP.Infrasturcture.Interface.Extension;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Extension;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.DataAdapter.Util;
using Motor.ATP.Infrasturcture.Interface.Infomation;

namespace Motor.ATP.DataAdapter.ConcreateAdapter.ATP200H
{
    public class ATP200HDataAdapter : DataAdapterBase
    {

        protected SignalDataIn200H SignalDataIn200H { get; set; }
        protected SignalDataIn200H SignalDataIn200HOld { get; set; }
        protected SignalDataOut200H SignalDataOut200H { get; set; }
        protected SignalDataOut200H SignalDataOut200HOld { get; set; }

        //private bool StartCtcsSelected = false;

        public ATP200HDataAdapter(ATPDomain adaptTarget)
            : base(adaptTarget, new SendInterface200H(new SignalDataOut200H()))
        {
            SignalDataIn200H = new SignalDataIn200H();
            SignalDataIn200HOld = new SignalDataIn200H();
            SignalDataIn200HOld = (SignalDataIn200H) SignalDataIn200H.Clone();
            SignalDataIn = SignalDataIn200H;
            SignalDataInOld = SignalDataIn200HOld;

            SignalDataOut200H = (SignalDataOut200H)SignalDataOut;
            SignalDataOut200HOld = (SignalDataOut200H)SignalDataOutOld;
            SignalDataOut200HOld = (SignalDataOut200H)SignalDataOut200H.Clone();
            SignalDataOut200H.SignalDataIn = SignalDataIn200H;
            SignalDataOut200H.ATP = ATP;
            SignalDataOut = SignalDataOut200H;
            SignalDataOutOld = SignalDataOut200HOld;
        }

        public override void OnBoolChangedImp(object sender, CommunicationDataChangedWrapperArgs<bool> dataChangedArgs)
        {
            base.OnBoolChangedImp(sender, dataChangedArgs);
            //黑屏
            //dataChangedArgs.UpdateIfContains(InBoolKeys.ATP电源, b => SignalDataIn300s.ScreenBlackFlag = b);

            //请缓解列车制动
          //  dataChangedArgs.UpdateIfContains(InBoolKeys.请求_请缓解列车制动, b => SignalDataIn200H.TrainBrakeReleaseAck = b);
            //目视警惕确认
          //  dataChangedArgs.UpdateIfContains(InBoolKeys.请求_目视警惕确认, b => SignalDataIn200H.OSAlarmAck = b);
            //引导警惕确认
          //  dataChangedArgs.UpdateIfContains(InBoolKeys.请求_引导警惕确认, b => SignalDataIn200H.SRAlarmAck = b);
            //目视使能
            dataChangedArgs.UpdateIfContains(InBoolKeys.目视使能, b => SignalDataIn200H.OSButtonStatus = b);
            //辅屏控制
            dataChangedArgs.UpdateIfContains(InBoolKeys.辅屏控制, b => SignalDataIn200H.AuxiliaryScreenControl = b);


            FeedBackInfo(sender, dataChangedArgs);
        }

        public override void OnFloatChangedImp(object sender, CommunicationDataChangedWrapperArgs<float> dataChangedArgs)
        {
            base.OnFloatChangedImp(sender, dataChangedArgs);
            //紧急制动测试状态
          //  dataChangedArgs.UpdateIfContains(InFloatKeys.紧急制动测试状态, f => SignalDataIn200H.nBreakTestStatus_Emerg = (int)f);
            //最大常用制动测试状态
          //  dataChangedArgs.UpdateIfContains(InFloatKeys.最大常用制动测试状态, f => SignalDataIn200H.nBreakTestStatus_Break7 = (int)f);
            //LKJ车次号头
            dataChangedArgs.UpdateIfContains(InFloatKeys.LKJ车次号头, f => SignalDataIn200H.LKJTrainNumHead = f);
            //LKJ车次号
            dataChangedArgs.UpdateIfContains(InFloatKeys.LKJ车次号, f => SignalDataIn200H.LKJTrainNum = f);
            //LKJ司机号
            dataChangedArgs.UpdateIfContains(InFloatKeys.LKJ司机号, f => SignalDataIn200H.LKJDriverNum = f);
            //辅屏_上行指示灯状态
            dataChangedArgs.UpdateIfContains(InFloatKeys.辅屏_上行指示灯状态, f => SignalDataIn200H.AuxiliaryScreen_FrequencyUp = (int)f);
            //辅屏_下行指示灯状态
            dataChangedArgs.UpdateIfContains(InFloatKeys.辅屏_下行指示灯状态, f => SignalDataIn200H.AuxiliaryScreen_FrequencyDown = (int)f);
            //辅屏_分相有效指示灯状态
            dataChangedArgs.UpdateIfContains(InFloatKeys.辅屏_分相有效指示灯状态, f => SignalDataIn200H.AuxiliaryScreen_DFXValid = (int)f);
            //辅屏_下行指示灯状态
            dataChangedArgs.UpdateIfContains(InFloatKeys.辅屏_分相执行指示灯状态, f => SignalDataIn200H.AuxiliaryScreen_DFXExecute = (int)f);
            //辅屏_限制速度
            dataChangedArgs.UpdateIfContains(InFloatKeys.辅屏_限制速度, f => SignalDataIn200H.AuxiliaryScreen_dATPSpeedLimit = f);
            //辅屏_当前速度
            dataChangedArgs.UpdateIfContains(InFloatKeys.辅屏_当前速度, f => SignalDataIn200H.AuxiliaryScreen_TrainSpeed = f);
            //辅屏_目标速度
            dataChangedArgs.UpdateIfContains(InFloatKeys.辅屏_目标速度, f => SignalDataIn200H.AuxiliaryScreen_GoalSpeed = f);
            //辅屏_目标距离
            dataChangedArgs.UpdateIfContains(InFloatKeys.辅屏_目标距离, f => SignalDataIn200H.AuxiliaryScreen_GoalDistance = f);
        }

        public override void OnDataChangedImp(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
            //1、信号-屏适配
            ScreenDistanceInfo();
            ScreenSpeedInfo();
            ScreenPlanInfo();
            ScreenBtnEnable(); //按钮使能
            ScreenTextInfo(); //E区文本
            ScreenControl(); //F区流程控制
            BasicAndTrainInfo();
            //2、屏-信号适配
            ScreenToSignalInfo();

            //3、清除信息
            if ((SignalDataIn200HOld.ATPPowerFlag && !SignalDataIn200H.ATPPowerFlag))
            {
                ClearInfos();
            }


          
            SignalDataIn200HOld = (SignalDataIn200H) SignalDataIn200H.Clone();
            SignalDataOut200HOld = (SignalDataOut200H)SignalDataOut200H.Clone();

        }

        public override bool FeedBackInfo(object sender, CommunicationDataChangedWrapperArgs<bool> dataChangedArgs)
        {
            base.FeedBackInfo(sender, dataChangedArgs);
          
            return true;
        }

        public override bool ScreenToSignalInfo()
        {
            base.ScreenToSignalInfo();


     
            return true;
        }
        public override bool ScreenPlanInfo()
        {
            base.ScreenPlanInfo();
            //200H只有待机、隔离、LKJ才不显示计划区，其他模式都显示
            if ((ControlType)SignalDataIn.ControlMode == ControlType.StandBy || (ControlType)SignalDataIn.ControlMode == ControlType.Isolated 
                || (ControlType)SignalDataIn.ControlMode == ControlType.LKJ || (ControlType)SignalDataIn.ControlMode == ControlType.Unknown)
            {
                ATP.SpeedMonitoringSection.Visible = false;
            }
            else
            {
                ATP.SpeedMonitoringSection.Visible = true;
            }
            return true;
        }
        public override bool ScreenSpeedInfo()
        {
            base.ScreenSpeedInfo();
            //C2LKJ控车无限速处理
            if (SignalDataIn.ControlMode == (int)ControlType.LKJ )
            {
                ATP.TrainInfo.Speed.CurrentSpeed.SpeedColor = ATPColor.Grey;
                ATP.TrainInfo.Speed.TargetSpeed.SpeedColor = ATPColor.None;
                ATP.TrainInfo.Speed.PermittedLimitSpeed.SpeedColor = ATPColor.None;
                ATP.TrainInfo.Speed.ServiceBrakeInterventionSpeed.SpeedColor = ATPColor.None;
                ATP.TrainInfo.Speed.EmergencyBrakeInterventionSpeed.SpeedColor = ATPColor.None;
               
             }
             return true;
        }
        public override bool ScreenDistanceInfo()
        {
            base.ScreenDistanceInfo();
            //200H只有完全模式显示预警时间，只有完全且在TSM区才显示目标距离
            if(SignalDataIn.ControlMode != (int)ControlType.FullSupervision)
            {
                ATP.WarningIntervention.TargetDistanceVisible = false;
                ATP.WarningIntervention.BrakeWarningVisible = false;
                ATP.WarningIntervention.BrakeWarningLevel = BrakeWarningLevel.Level0;
                ATP.WarningIntervention.BrakeWaringColor = ATPColor.None;
               
            }else if(SignalDataIn.QMArea == 1)
            {
                ATP.WarningIntervention.TargetDistanceVisible = false;
            }
            return true;
        }
        public override bool BasicAndTrainInfo()
        {

            ATP.Other.NowInATP = SignalDataIn200H.NowTime;

            //速度及限速信息
            ATP.TrainInfo.Speed.Visible = !SignalDataIn200H.ChangeHostAuto;

            ATP.TrainInfo.Speed.CurrentSpeed.Value = Math.Abs(SignalDataIn200H.TrainSpeed);

            ATP.TrainInfo.Speed.ServiceBrakeInterventionSpeed.Value = SignalDataIn200H.SBISpeed;
            ATP.TrainInfo.Speed.EmergencyBrakeInterventionSpeed.Value = SignalDataIn200H.EBISpeed;
            ATP.TrainInfo.Speed.PermittedLimitSpeed.Value = SignalDataIn200H.TrainLimitSpeed;
            ATP.TrainInfo.Speed.IntervertionSpeed.Value = SignalDataIn200H.IntervertionSpeed;
            ATP.TrainInfo.Speed.WarningLimitSpeed.Value = SignalDataIn200H.WarningSpeed;
            ATP.TrainInfo.Speed.TargetSpeed.Value = SignalDataIn200H.ControlMode != (int)ControlType.FullSupervision &&
                                                    SignalDataIn200H.ControlMode != (int)ControlType.CallingOn
                ? SignalDataIn200H.TrainLimitSpeed - 5
                : (SignalDataIn200H.TrainGoalSpeed > SignalDataIn200H.TrainLimitSpeed
                    ? SignalDataIn200H.TrainLimitSpeed
                    : SignalDataIn200H.TrainGoalSpeed);
            //制动信息
            ATP.TrainInfo.Brake.Visible = SignalDataIn200H.BrakeStatus > (int)BrakeStatus.PowerLoseSign && SignalDataIn200H.BrakeStatus <= (int)BrakeStatus.CommonBreak_R &&
                                          SignalDataIn200H.BrakeTestStatus != (int)BrakeTestStatus.BrakeTesting;
            ATP.TrainInfo.Brake.BrakeType = (BrakeType)SignalDataIn200H.BrakeStatus;
            //公里标
            ATP.TrainInfo.KilometerPost.Visible = SignalDataIn200H.IsGLBVisible;
            if (ATP.TrainInfo.KilometerPost.Visible)
            {
                ATP.TrainInfo.KilometerPost.Kilometer = Math.Truncate(SignalDataIn200H.TrainGLB / 1000);
                ATP.TrainInfo.KilometerPost.Meter = SignalDataIn200H.TrainGLB % 1000;
            }
            //GSM && RBC
            ATP.TrainInfo.ConnectState.Visible = false;


            //车站
            ATP.TrainInfo.Station.Visible = true;
            ATP.TrainInfo.Station.CurrentStation = SignalDataIn200H.StationNo > 0
                ? GetStationName((ulong)SignalDataIn200H.StationNo)
                : "";
            //等级
            ATP.CTCS.CurrentCTCSType = (CTCSType)SignalDataIn200H.RunLevel;
            ATP.CTCS.Visible = ATP.CTCS.CurrentCTCSType == CTCSType.CTCS2 || ATP.CTCS.CurrentCTCSType == CTCSType.CTCS0;
            //模式
            ATP.ControlModel.Visible = true;
            ATP.ControlModel.CurrentControlType = (ControlType)SignalDataIn200H.ControlMode;
            
            //放大、缩小
            ATP.SpeedMonitoringSection.ZoomVisible = false;
            if ((ControlType)SignalDataIn200H.ControlMode == ControlType.FullSupervision)
            {
                ATP.SpeedMonitoringSection.ZoomVisible = true;
            }
            //TODO 增加其它模式
            ATP.RegionFStateProvier.ShuntingStateProvider.EnterOrQuitState = (ControlType)SignalDataIn200H.ControlMode ==
                                                                             ControlType.Shunting
                ? EnterOrQuit.Quit
                : EnterOrQuit.Enter;
            ATP.RegionFStateProvier.CabsignalStateProvider.EnterOrQuitState = (ControlType)SignalDataIn200H.ControlMode ==
                                                                              ControlType.LKJ
                ? EnterOrQuit.Quit
                : EnterOrQuit.Enter;

            //机车信号
            ATP.CabSignal.Code = (CabSignalCode)SignalDataIn200H.SignalCode;
            ATP.CabSignal.Visible = ATP.CabSignal.Code > 0;
            //机控人控
            ATP.TrainControlType = (TrainControlType)SignalDataIn200H.BrakeMode;
            ATP.TrainControlTypeVisible = ATP.TrainControlType != TrainControlType.Unkown;
            //电源
            ATP.ATPPower.ATPPowerState = SignalDataIn200H.ATPPowerFlag ? ATPPowerState.Started : ATPPowerState.Stopped;


            //司机号、车次号
            ATP.TrainInfo.Driver.TrainIdVisible = true;
            //同步LKJ司机号和车次号 
            ATP.TrainInfo.Driver.TrainId = SignalDataOut200H.TrainCode;
            if (SignalDataIn200H.LKJTrainNum > 0)
            {
                ATP.TrainInfo.Driver.TrainId = DecodeTrainId(SignalDataIn200H.LKJTrainNumHead, SignalDataIn200H.LKJTrainNum);
            }
            ATP.TrainInfo.Driver.DriverId = SignalDataOut200H.DriverCode;
            if (SignalDataIn200H.LKJDriverNum > 0)
            {
                ATP.TrainInfo.Driver.DriverId = DecodeDriverId(SignalDataIn200H.LKJDriverNum);
            }

            //时间日期
            ATP.Other.TimeVisible = true;  
            ATP.Other.DateVisible = true;
            ATP.Other.DateTimeTitleVisible = true;
            //文本消息
            ATP.Message.Visible = true;
           
            
            if ((SelfCheckStatus)SignalDataIn200H.nSelfCheckStatus != SelfCheckStatus.CheckStatus_Success)
            {
                ATP.Other.TimeVisible = false;
                ATP.Other.DateVisible = false;
                ATP.CTCS.Visible = false;
                ATP.TrainInfo.Station.Visible = false;
                ATP.TrainInfo.KilometerPost.Visible = false;
                ATP.TrainInfo.Brake.Visible = false;
                ATP.CabSignal.Visible = false;
                ATP.TrainControlTypeVisible = false;

            }
            //200H不显示列车车长

            //5、安全计算机双系故障;6、STM双系故障；7、测速系统异常；8、ATP设备故障；9、BTM故障,DMI上A\B\D区不显示；
            //辅屏控制时,DMI上A\B\D区不显示；
            if (SignalDataIn200H.Fault[5] || SignalDataIn200H.Fault[6]|| SignalDataIn200H.Fault[7]
                || SignalDataIn200H.Fault[8] || SignalDataIn200H.Fault[9] || SignalDataIn200H.AuxiliaryScreenControl)
            {
                ATP.ControlModel.Visible = false;
                ATP.SpeedMonitoringSection.Visible = false;
                ATP.WarningIntervention.TargetDistanceVisible = false;
                ATP.TrainInfo.Speed.Visible = false;
                ATP.WarningIntervention.BrakeWarningVisible = false;
                ATP.CabSignal.Visible = false;
                // 命令不显示
                for (int i = 0; i < 3; i++)
                {
                    SignalDataIn.OrderList[i] = 0;
                    ATP.ForecastInformation.ForecastInformationItems[10 + i].Update(
                        (ForecastInformationType)SignalDataIn200H.OrderList[i], 0);
                }
            }
            //当切换至辅显示屏时，辅显示屏全部显示为“0”并持续 1s，之后按 ATP 发送的数据显示；LED 指示灯全部点亮，并持续 1s。
            //在辅显示区，当 CTCS-0 级以及 CTCS-2 级待机模式及隔离模式时，辅显示屏仅显示当前速度；
            //CTCS-2 级非完全模式时，辅显示屏仅显示当前速度和限制速度
            //CTCS-2 级完全模式时，辅显示屏显示当前速度、限制速度、目标速度和目标距离。
            //载频上下行标志，即信号屏上下行指示灯  add by hcz 2015-10-31
            //上下行指示灯为在辅显的情况下，若选择了上行/下行，相应的指示灯以 1HZ 的频率闪 10s，若 10s 内没有按压“确定”键，则 10s 后熄灭，若 10s 内按压了“确定”按键，则常亮 60s 后熄灭。 
            ATP.Other.Parent.AssistDisplayInfo.Visible = true;
            if (!SignalDataIn200H.AuxiliaryScreenControl)
            {
                ATP.Other.Parent.AssistDisplayInfo.FrequencyUp = false;
                ATP.Other.Parent.AssistDisplayInfo.FrequencyDown = false;
                ATP.Other.Parent.AssistDisplayInfo.SplitPhaseEnable = false;
                ATP.Other.Parent.AssistDisplayInfo.SplitPhaseExecute = false;
                ATP.Other.Parent.AssistDisplayInfo.CurrentSpeed = "";
                ATP.Other.Parent.AssistDisplayInfo.LimitedSpeed = "";
                ATP.Other.Parent.AssistDisplayInfo.TargetDistance = "";
                ATP.Other.Parent.AssistDisplayInfo.CabSignalCodeTargetSpeedPair = "";
                return true;

            }
            ATP.Other.Parent.AssistDisplayInfo.FrequencyUp = SignalDataIn200H.AuxiliaryScreen_FrequencyUp > 0 ? true:false ;
            ATP.Other.Parent.AssistDisplayInfo.FrequencyDown = SignalDataIn200H.AuxiliaryScreen_FrequencyDown > 0 ? true : false;
            ATP.Other.Parent.AssistDisplayInfo.SplitPhaseEnable = SignalDataIn200H.AuxiliaryScreen_DFXValid > 0 ? true : false;
            ATP.Other.Parent.AssistDisplayInfo.SplitPhaseExecute = SignalDataIn200H.AuxiliaryScreen_DFXExecute > 0 ? true : false;

            if ((ControlType)SignalDataIn200H.ControlMode == ControlType.Isolated || (ControlType)SignalDataIn200H.ControlMode == ControlType.Unknown
                || (ControlType)SignalDataIn200H.ControlMode == ControlType.LKJ)
            {
                ATP.Other.Parent.AssistDisplayInfo.CurrentSpeed = SignalDataIn200H.AuxiliaryScreen_TrainSpeed.ToString();
                ATP.Other.Parent.AssistDisplayInfo.LimitedSpeed = "";
                ATP.Other.Parent.AssistDisplayInfo.TargetDistance = "";
                ATP.Other.Parent.AssistDisplayInfo.CabSignalCodeTargetSpeedPair = "";
            }else if ((ControlType)SignalDataIn200H.ControlMode != ControlType.FullSupervision)
            {
                ATP.Other.Parent.AssistDisplayInfo.CurrentSpeed = SignalDataIn200H.AuxiliaryScreen_TrainSpeed.ToString();
                ATP.Other.Parent.AssistDisplayInfo.LimitedSpeed = SignalDataIn200H.AuxiliaryScreen_dATPSpeedLimit.ToString();
                ATP.Other.Parent.AssistDisplayInfo.TargetDistance = "";
                ATP.Other.Parent.AssistDisplayInfo.CabSignalCodeTargetSpeedPair = "";
            }else
            {
                ATP.Other.Parent.AssistDisplayInfo.CurrentSpeed = SignalDataIn200H.AuxiliaryScreen_TrainSpeed.ToString();
                ATP.Other.Parent.AssistDisplayInfo.LimitedSpeed = SignalDataIn200H.AuxiliaryScreen_dATPSpeedLimit.ToString();
                ATP.Other.Parent.AssistDisplayInfo.TargetDistance = SignalDataIn200H.AuxiliaryScreen_GoalDistance.ToString();
                ATP.Other.Parent.AssistDisplayInfo.CabSignalCodeTargetSpeedPair = SignalDataIn200H.AuxiliaryScreen_GoalSpeed.ToString();
            }
            return true;

        }

        public override bool ScreenBtnEnable()
        {
            //按钮触发状态

            for (int i = 0; i < SignalDataIn200H.ScreenBotton.Length; i++)
            {
                HardwareButton HBInfo = ATP.ATPHardwareButton.HardwareButtonCollection[i];
                HBInfo.Type = (UserActionType)(i + 1);
                HBInfo.MouseState = SignalDataIn200H.ScreenBotton[i] ? MouseState.MouseDown : MouseState.MouseUp;
            }
            //按钮使能状态
            //0-数据，1-模式，2-载频，3-等级，4-其他，5-启动，6-缓解,7-警惕,8-司机号,9-车次号,10-列车数据,11-RBC数据，12-调车，13-目视，14-机信，300h(15-制动测试执行，16-制动测试略过
            //17-RBCID，18-RBC电话号码，19-RBC网络号码)，20-制动测试，21-DMI测试,22-C0，23-C2,24-C3,25-列车数据，26-常用制动测试，27-紧急制动测试
            /*********300S按钮使能***********
            1、启动完成前按钮状态
            （1）屏自检完成以前，所有按钮灰色
            （2）未进入任何等级时，等级、启动、缓解、警惕按钮灰色，其他亮
            （3）C2等级，启动条件不满足时，启动按钮为灰色，其他亮
            （4）C3等级，等级、启动、缓解均为灰色,调车及机信灰色
            ********************************/
            bool[] BtnEnable = new bool[50];
            for (int i = 0; i < BtnEnable.Length; i++)
            {
                BtnEnable[i] = true;
            }
           
           
            //CO模式下模式、启动、缓解、警惕灰
            if (SignalDataIn200H.ControlMode == (int)ControlType.LKJ || SignalDataIn200H.ControlMode == (int)ControlType.Isolated)
            {
                BtnEnable[1] = false;
                BtnEnable[5] = false;
                BtnEnable[6] = false;
                BtnEnable[7] = false;
            }
            //只有在待机下启动和列车长度使能
            if(SignalDataIn200H.ControlMode == (int)ControlType.StandBy || SignalDataIn200H.ControlMode == (int)ControlType.Unknown)
            {

            }else
            {
                BtnEnable[5] = false;
                BtnEnable[25] = false;
            }
          
            //模式为false ，下面目录也是false
            if (!BtnEnable[1])
            {
                BtnEnable[12] = false;
                BtnEnable[13] = false;
            }

            //按钮使能转换
            for (int i = 0; i < BtnEnable.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        ATP.RegionFStateProvier.DataStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 1:
                        ATP.RegionFStateProvier.ControlModelStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 2:
                        ATP.RegionFStateProvier.FreqStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 3:
                        ATP.RegionFStateProvier.CTCSStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 4:
                        ATP.RegionFStateProvier.OtherStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 5:
                        ATP.RegionFStateProvier.StartStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 6:
                        ATP.RegionFStateProvier.RelaseStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 7:
                        ATP.RegionFStateProvier.VigilantStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 8:
                        ATP.RegionFStateProvier.DriverIdStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 9:
                        ATP.RegionFStateProvier.TrainIdStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 10:
                        ATP.RegionFStateProvier.TrainDataStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 11:
                        ATP.RegionFStateProvier.RBCDataStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 12:
                        ATP.RegionFStateProvier.ShuntingStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 13:
                        ATP.RegionFStateProvier.OnSightStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 14:
                        ATP.RegionFStateProvier.CabsignalStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 20:
                        ATP.RegionFStateProvier.RunBrakeTestStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 21:
                        ATP.RegionFStateProvier.DMITestStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 22:
                        ATP.RegionFStateProvier.CTCS0StateProvider.Enabled = BtnEnable[i];
                        break;
                    case 23:
                        ATP.RegionFStateProvier.CTCS2StateProvider.Enabled = BtnEnable[i];
                        break;
                    case 25:
                        ATP.RegionFStateProvier.TrainDataStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 26:
                        ATP.RegionFStateProvier.RunNormalBrakeTestStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 27:
                        ATP.RegionFStateProvier.RunEmergencyBrakeTestStateProvider.Enabled = BtnEnable[i];
                        break;
                    default:
                        break;
                }
            }

            return true;
        }

        public override bool ScreenTextInfo()
        {
            InfomationCreater InfoCreater =
                (InfomationCreater)ATP.InfomationService.GetInformationCreater();
            InfoCreater.ClearContents();



            //启动过程文本—start—————————————————————————————————————————————
            //自检
            if (SignalDataIn200HOld.nSelfCheckStatus != SignalDataIn200H.nSelfCheckStatus)
            {   
                switch ((SelfCheckStatus)SignalDataIn200H.nSelfCheckStatus)
                {   
                    case SelfCheckStatus.CheckStatus_Checking://系统自检请等待
                        {
                            InfoCreater.UpdateInfomation(15);
                        }
                        break;
                    case SelfCheckStatus.CheckStatus_Success:
                        {
                            InfoCreater.UpdateInfomation(52);   //测试结果正常      
                        }
                        break;
                    case SelfCheckStatus.CheckStatus_Fail:
                        {
                            InfoCreater.UpdateInfomation(53);   //测试结果异常
                        }
                        break;
                }
            }

            //启动过程文本—start—————————————————————————————————————————————

            //闪烁文本—start—————————————————————————————————————————————
            //警惕、冒进、越行、引导确认
           
        
              //引导
            if (!SignalDataIn200HOld.SRAck && SignalDataIn200H.SRAck)
            {
                InfoCreater.UpdateInfomation(41);
            }

            /*//等级转换确认
            if (!SignalDataIn200HOld.EnterC2Ack && SignalDataIn200H.EnterC2Ack)
            {
                InfoCreater.UpdateInfomation(42);
            }
            if (!SignalDataIn200HOld.EnterC0Ack && SignalDataIn200H.EnterC0Ack)
            {
                InfoCreater.UpdateInfomation(75);
            }*/

            //闪烁文本—end—————————————————————————————————————————————

            //允许缓解
            if (SignalDataIn200HOld.BrakeStatus != (int)BrakeStatus.CommonBreak_R && SignalDataIn200H.BrakeStatus == (int)BrakeStatus.CommonBreak_R)
            {
                InfoCreater.UpdateInfomation(36);
            }
            //禁止调车
            if(SignalDataIn200H.SHForbidFlag && !SignalDataIn200HOld.SHForbidFlag && (ControlType)SignalDataIn200H.ControlMode == ControlType.Shunting)
            {
                InfoCreater.UpdateInfomation(20);
            }
            //绝对停车：冒进信号紧急制动
            if (SignalDataIn200H.AbsoluteStopFlag && !SignalDataIn200HOld.AbsoluteStopFlag)
            {
                InfoCreater.UpdateInfomation(90);
            }
            //切换至辅显示屏
            if(SignalDataOut200H.ScreenSwitchFlag && !SignalDataOut200HOld.ScreenSwitchFlag)
            {
                InfoCreater.UpdateInfomation(93);
            }
           
            //退行或溜逸防护
            //if (!SignalDataIn200HOld.BackProtectAck && SignalDataIn200H.BackProtectAck)
            //{
            //    InfoCreater.UpdateInfomation(47, true);
            //}

       
            //等级转换文本
            if (SignalDataIn200HOld.RunLevel != SignalDataIn200H.RunLevel)
            {
                if (SignalDataIn200H.RunLevel == (int)CTCSType.CTCS0)
                {
                    InfoCreater.UpdateInfomation(68);
                }
                else if (SignalDataIn200H.RunLevel == (int)CTCSType.CTCS2)
                {
                    InfoCreater.UpdateInfomation(10);
                }
            }
            //等级转换预告文本
            if (SignalDataIn200HOld.YGLevelInfo != SignalDataIn200H.YGLevelInfo)
            {
                if (SignalDataIn200H.YGLevelInfo == (int)LevelChangeType.YG_CTCS0TOCTCS2)
                {
                    InfoCreater.UpdateInfomation(12);
                }
                if (SignalDataIn200H.YGLevelInfo == (int)LevelChangeType.YG_CTCS2TOCTCS0)
                {
                    InfoCreater.UpdateInfomation(71);
                }
            }

            //模式转换文本
            if (SignalDataIn200HOld.ControlMode != SignalDataIn200H.ControlMode)
            {
                switch ((ControlType)SignalDataIn200H.ControlMode)
                {
                    case ControlType.FullSupervision:
                        InfoCreater.UpdateInfomation(7);
                        break;
                    case ControlType.StandBy:
                        if(SignalDataIn200H.ATPPowerFlag)
                        { InfoCreater.UpdateInfomation(1);
                        }
                        
                        break;
                    case ControlType.PartialSupervision:
                        InfoCreater.UpdateInfomation(2);
                        break;
                    case ControlType.Shunting:
                        InfoCreater.UpdateInfomation(4);
                        break;
                    case ControlType.OnSight:
                        InfoCreater.UpdateInfomation(3);
                        break;
                    case ControlType.CallingOn:
                        InfoCreater.UpdateInfomation(5);
                        break;
                    case ControlType.Isolated:
                        InfoCreater.UpdateInfomation(8);
                        break;
                    case ControlType.Sleep:
                        InfoCreater.UpdateInfomation(9);
                        break;
                    case ControlType.Trip:
                        InfoCreater.UpdateInfomation(73);
                        break;
                    case ControlType.PostTrip:
                        InfoCreater.UpdateInfomation(74);
                        break;
                    default:
                        break;
                }
            }
            //故障文本—start—————————————————————————————————————————————
            //“ATP主机双系故障”
            if (!SignalDataIn200HOld.Fault[0] && SignalDataIn200H.Fault[0])
            {
                InfoCreater.UpdateInfomation(76);
            }
            //“与安全计算机1系通信故障”
            if (!SignalDataIn200HOld.Fault[1] && SignalDataIn200H.Fault[1])
            {
                InfoCreater.UpdateInfomation(94);
        
            }
            //“安全计算机1系故障”
            if (!SignalDataIn200HOld.Fault[2] && SignalDataIn200H.Fault[2])
            {
                InfoCreater.UpdateInfomation(29);
              
            }
            //“STM1故障”
            if (!SignalDataIn200HOld.Fault[3] && SignalDataIn200H.Fault[3])
            {
                InfoCreater.UpdateInfomation(95);
               
            }
            //“与安全计算机双系通信故障”
            if (!SignalDataIn200HOld.Fault[4] && SignalDataIn200H.Fault[4])
            {
                InfoCreater.UpdateInfomation(96);
            }

            //“安全计算机双系故障”
            if (!SignalDataIn200HOld.Fault[5] && SignalDataIn200H.Fault[5])
            {
                InfoCreater.UpdateInfomation(97);
            }
            //“STM双系故障”
            if (!SignalDataIn200HOld.Fault[6] && SignalDataIn200H.Fault[6])
            {
                InfoCreater.UpdateInfomation(98);

            }
            //“测速系统异常”
            if (!SignalDataIn200HOld.Fault[7] && SignalDataIn200H.Fault[7])
            {
                InfoCreater.UpdateInfomation(78);

            }
            //“ATP设备故障”
            if (!SignalDataIn200HOld.Fault[8] && SignalDataIn200H.Fault[8])
            {
                InfoCreater.UpdateInfomation(33);

            }
            //“BTM故障”
            if (!SignalDataIn200HOld.Fault[9] && SignalDataIn200H.Fault[9])
            {
                InfoCreater.UpdateInfomation(32);
            }
            //“DMI双系全部不工作”
            if (!SignalDataIn200HOld.Fault[10] && SignalDataIn200H.Fault[10])
            {
                InfoCreater.UpdateInfomation(99);

            }
            //“显示器故障”
            if (!SignalDataIn200HOld.Fault[11] && SignalDataIn200H.Fault[11])
            {
                InfoCreater.UpdateInfomation(100);

            }
            //“显示不正常”
            if (!SignalDataIn200HOld.Fault[12] && SignalDataIn200H.Fault[12])
            {
                InfoCreater.UpdateInfomation(101);
            }
            //故障文本—end—————————————————————————————————————————————


            InfoCreater.FlushInfomation();

            return true;
        }

        public override bool ClearInfos()
        {
            InfomationCreater InfoCreater =
                (InfomationCreater)ATP.InfomationService.GetInformationCreater();
            InfoCreater.ClearShowingItems();

            ATP.Other.ShowingTimeDifference = TimeSpan.Zero;
            ATP.GetInterfaceController().UpdateDriverInterface(DriverInterfaceKey.Root);

            //清除输入信息
            SignalDataIn200H.ClearInfo();
            SignalDataOut200H.ClearInfo();
            return true;
        }

        public override bool ScreenControl()
        {   
          
        
            return true;
        }

        private string DecodeDriverId(params float[] values)
        {
            if (values == null || !values.Any())
            {
                return "";
            }

            return Math.Truncate(values[0]).ToString("#######");
        }

        /// <summary>
        /// // 车次号
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private string DecodeTrainId(params float[] values)
        {
            if (values == null || values.Length < 2)
            {
                return "";
            }
            
            var sb = new StringBuilder();

            var head = Convert.ToChar(BitConverter.GetBytes(values[0])[0]);

            var c = Convert.ToChar(head);
            if (char.IsLower(c) || char.IsUpper(c))
            {
                sb.Append(c.ToString(CultureInfo.InvariantCulture).ToUpper());
            }

            return sb.Append(Math.Truncate(values[1]).ToString("####")).ToString();
        }
    }
}