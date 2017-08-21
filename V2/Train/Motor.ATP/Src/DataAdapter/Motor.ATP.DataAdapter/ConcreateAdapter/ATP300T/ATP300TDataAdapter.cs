using System;
using CommonUtil.Controls;
using MMI.Facility.Interface.Data;
using Motor.ATP.DataAdapter.Extension;
using Motor.ATP.DataAdapter.Resource.Keys;
using Motor.ATP.Infrasturcture.Control.Infomation;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.BrakeTest;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Interface.Extension;
using Motor.ATP.Infrasturcture.Model;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Extension;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.DataAdapter.Util;
namespace Motor.ATP.DataAdapter.ConcreateAdapter.ATP300T
{
    public class ATP300TDataAdapter : DataAdapterBase
    {

        protected SignalDataIn300T SignalDataIn300t { get; set; }
        protected SignalDataIn300T SignalDataIn300tOld { get; set; }
        protected SignalDataOut300T SignalDataOut300t { get; set; }
        protected SignalDataOut300T SignalDataOut300tOld { get; set; }


        public ATP300TDataAdapter(ATPDomain adaptTarget)
            : base(adaptTarget, new SendInterface300T(new SignalDataOut300T()))
        {
            SignalDataIn300t = new SignalDataIn300T();
            SignalDataIn300tOld = new SignalDataIn300T();
            SignalDataIn300tOld = (SignalDataIn300T)SignalDataIn300t.Clone();
            SignalDataIn = SignalDataIn300t;
            SignalDataInOld = SignalDataIn300tOld;

            SignalDataOut300t = (SignalDataOut300T) SignalDataOut;
            SignalDataOut300tOld = (SignalDataOut300T)SignalDataOutOld;
            SignalDataOut300tOld = (SignalDataOut300T)SignalDataOut300t.Clone();
            SignalDataOut300t.SignalDataIn = SignalDataIn300t;
            SignalDataOut300t.ATP = ATP;
            SignalDataOut = SignalDataOut300t;
            SignalDataOutOld = SignalDataOut300tOld;
        }

        public override void OnBoolChangedImp(object sender, CommunicationDataChangedWrapperArgs<bool> dataChangedArgs)
        {
            base.OnBoolChangedImp(sender,dataChangedArgs);

            //黑屏
            dataChangedArgs.UpdateIfContains(InBoolKeys.ATP电源, b => SignalDataIn300t.ScreenBlackFlag = b);

            //常用制动故障确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_常用制动故障确认, b => SignalDataIn300t.FaultNormalBreakAck = b);
          
            ////车载设备自动换系
            //dataChangedArgs.UpdateIfContains(InBoolKeys.车载设备自动换系, b => SignalDataIn300t.ChangeHostAuto = b);

            //应答器丢失确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_应答器信息丢失确认, b => SignalDataIn300t.EnterBaliseLostAck = b);

            //启动流程相关
            dataChangedArgs.UpdateIfContains(InBoolKeys.启动_列车数据已选择, b => SignalDataIn300t.StartTrainDataSelected = b);
            dataChangedArgs.UpdateIfContains(InBoolKeys.启动_载频已选择, b => SignalDataIn300t.StartFrequencySelected = b);
           

            FeedBackInfo(sender, dataChangedArgs);


        }

        public override void OnDataChangedImp(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
            //1、信号-屏适配
            BasicAndTrainInfo();
            ScreenDistanceInfo();
            ScreenSpeedInfo();
            ScreenPlanInfo();

            ScreenBtnEnable(); //按钮使能
            ScreenTextInfo(); //E区文本
            ScreenControl(); //F区流程控制

            //2、屏-信号适配
            ScreenToSignalInfo();

            //3、清除信息
            if ((SignalDataIn300tOld.ATPPowerFlag && !SignalDataIn300t.ATPPowerFlag)
                || (!SignalDataIn300tOld.ATPBypassFlag && SignalDataIn300t.ATPBypassFlag))
            {
                ClearInfos();
            }


            SignalDataIn300tOld = (SignalDataIn300T) SignalDataIn300t.Clone();
            SignalDataOut300tOld = (SignalDataOut300T)SignalDataOut300t.Clone();
          

        }

        public override bool FeedBackInfo(object sender, CommunicationDataChangedWrapperArgs<bool> dataChangedArgs)
        {
            base.FeedBackInfo(sender, dataChangedArgs);


            //反馈_启动确认_没有消息提示与确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_启动确认_没有消息提示与确认, b =>
            {
                if (b)
                {
                    SignalDataOut300t.StartSign = false;
                }
            });


            //反馈_确认常用制动故障
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_确认常用制动故障, b =>
            {
                if (b)
                {
                    SignalDataOut300t.FaultNormalBreakSign = false;
                }
            });

           

            //反馈_应答器丢失确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_应答器丢失确认, b =>
            {
                if (b)
                {
                    SignalDataOut300t.BaliseLostSign = false;
                }
            });




            //反馈_快捷键
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_快捷键, b =>
            {
                if (b)
                {
                    SignalDataOut300t.FastButton = false;
                }
            });
            return true;
        }

        public override bool ScreenToSignalInfo()
        {
            base.ScreenToSignalInfo();
            //bool

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.启动确认_没有消息提示与确认],
                SignalDataOut300t.StartSign);
            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认常用制动故障],
                SignalDataOut300t.FaultNormalBreakSign);
            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.应答器丢失确认],
                SignalDataOut300t.BaliseLostSign);
            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.快捷键],
                SignalDataOut300t.FastButton);
            DataService.WriteService.ChangeFloat(InterfaceAdapterService.OutFloatDictionary[OutFloatKeys.系统自检计时],
                SignalDataOut300t.TimeSpan);

            return true;
        }
        public override bool ScreenPlanInfo()
        {
            base.ScreenPlanInfo();
            if (SignalDataIn.ControlMode == (int)ControlType.FullSupervision ||
                (SignalDataIn.ControlMode == (int)ControlType.CallingOn && (CTCSType)SignalDataIn300t.RunLevel == CTCSType.CTCS3))
            {
                ATP.SpeedMonitoringSection.Visible = true;
            }
            else
            {
                
                ATP.SpeedMonitoringSection.Visible = false;
            }
            return true;
        }
        public override bool ScreenSpeedInfo()
        {
            base.ScreenSpeedInfo();
           
            return true;
        }
        public override bool ScreenDistanceInfo()
        {
            base.ScreenDistanceInfo();
            if ((SignalDataIn.ControlMode == (int)ControlType.FullSupervision) ||
               ((SignalDataIn.ControlMode == (int)ControlType.CallingOn) && (CTCSType)SignalDataIn300t.RunLevel == CTCSType.CTCS2))
            {
                ATP.WarningIntervention.TargetDistanceVisible = true;
               
            }
            else
            {
                ATP.WarningIntervention.TargetDistanceVisible = false;
                ATP.WarningIntervention.BrakeWarningVisible = false;
                ATP.WarningIntervention.BrakeWarningLevel = BrakeWarningLevel.Level0;
                ATP.WarningIntervention.BrakeWaringColor = ATPColor.None;
                return true;
            }
            return true;
        }
        public override bool BasicAndTrainInfo()
        {
            ATP.Other.NowInATP = SignalDataIn300t.NowTime;
            //速度及限速信息
            ATP.TrainInfo.Speed.Visible = !SignalDataIn300t.ChangeHostAuto;

            ATP.TrainInfo.Speed.CurrentSpeed.Value = Math.Abs(SignalDataIn300t.TrainSpeed);

            ATP.TrainInfo.Speed.ServiceBrakeInterventionSpeed.Value = SignalDataIn300t.SBISpeed;
            ATP.TrainInfo.Speed.EmergencyBrakeInterventionSpeed.Value = SignalDataIn300t.EBISpeed;
            ATP.TrainInfo.Speed.PermittedLimitSpeed.Value = SignalDataIn300t.TrainLimitSpeed;
            ATP.TrainInfo.Speed.IntervertionSpeed.Value = SignalDataIn300t.IntervertionSpeed;
            ATP.TrainInfo.Speed.WarningLimitSpeed.Value = SignalDataIn300t.WarningSpeed;
            ATP.TrainInfo.Speed.TargetSpeed.Value = SignalDataIn300t.ControlMode != (int) ControlType.FullSupervision &&
                                                    SignalDataIn300t.ControlMode != (int) ControlType.CallingOn
                ? SignalDataIn300t.TrainLimitSpeed - 5
                : (SignalDataIn300t.TrainGoalSpeed > SignalDataIn300t.TrainLimitSpeed
                    ? SignalDataIn300t.TrainLimitSpeed
                    : SignalDataIn300t.TrainGoalSpeed);
            //制动信息
            ATP.TrainInfo.Brake.Visible = SignalDataIn300t.BrakeStatus > (int)BrakeStatus.PowerLoseSign && SignalDataIn300t.BrakeStatus <= (int)BrakeStatus.CommonBreak_R &&
                                          SignalDataIn300t.BrakeTestStatus != (int)BrakeTestStatus.BrakeTesting;
            ATP.TrainInfo.Brake.BrakeType = (BrakeType) SignalDataIn300t.BrakeStatus;

            //车站
            ATP.TrainInfo.Station.Visible = true;
            ATP.TrainInfo.Station.CurrentStation = SignalDataIn300t.StationNo > 0
                ? GetStationName((ulong)SignalDataIn300t.StationNo)
                : "";
            //公里标
            ATP.TrainInfo.KilometerPost.Visible = SignalDataIn300t.IsGLBVisible;
            if (ATP.TrainInfo.KilometerPost.Visible)
            {
                ATP.TrainInfo.KilometerPost.Kilometer = Math.Truncate(SignalDataIn300t.TrainGLB / 1000);
                ATP.TrainInfo.KilometerPost.Meter = SignalDataIn300t.TrainGLB%1000;
            }
            else
            {
                ATP.TrainInfo.KilometerPost.Kilometer = 0;
                ATP.TrainInfo.KilometerPost.Meter = 0;
            }
            //GSM && RBC
            ATP.TrainInfo.ConnectState.GSMRState = SignalDataIn300t.nSelfCheckStatus != (int)SelfCheckStatus.CheckStatus_Success
              ? GSMRState.Invalidate
              : (GSMRState)SignalDataIn300t.GSMStatus;

            ATP.TrainInfo.ConnectState.RBCConnectState = SignalDataIn300t.RBCStatus < 0
                ? RBCConnectState.Invalidate
                : (RBCConnectState)SignalDataIn300t.RBCStatus;
            //GSM无连接，RBC不显示
            if ((GSMRState)SignalDataIn300t.GSMStatus == GSMRState.None)
            {
                ATP.TrainInfo.ConnectState.RBCConnectState = RBCConnectState.Invalidate;
            }

            ATP.TrainInfo.ConnectState.Visible = SignalDataIn300t.nSelfCheckStatus == (int)SelfCheckStatus.CheckStatus_Success;

            //等级
            ATP.CTCS.CurrentCTCSType = (CTCSType) SignalDataIn300t.RunLevel;
            ATP.CTCS.Visible = ATP.CTCS.CurrentCTCSType == CTCSType.CTCS2 || ATP.CTCS.CurrentCTCSType == CTCSType.CTCS3;
            //模式
            ATP.ControlModel.InEffect = true;
            ATP.ControlModel.Visible = true;
            ATP.ControlModel.CurrentControlType = (ControlType) SignalDataIn300t.ControlMode;
            //TODO 增加其它模式
            ATP.SpeedMonitoringSection.ZoomVisible = ATP.ControlModel.CurrentControlType == ControlType.FullSupervision;
            ATP.RegionFStateProvier.ShuntingStateProvider.EnterOrQuitState = (ControlType) SignalDataIn300t.ControlMode ==
                                                                             ControlType.Shunting
                ? EnterOrQuit.Quit
                : EnterOrQuit.Enter;
            ATP.RegionFStateProvier.CabsignalStateProvider.EnterOrQuitState = (ControlType) SignalDataIn300t.ControlMode ==
                                                                              ControlType.LKJ
                ? EnterOrQuit.Quit
                : EnterOrQuit.Enter;

            //机车信号
            ATP.CabSignal.Code = (CabSignalCode)SignalDataIn300t.SignalCode;
            ATP.CabSignal.Visible = ATP.CabSignal.Code > 0;

            //紧急消息
            ATP.EmergencyInfo.InfoType = (EmergencyInfoType)SignalDataIn300t.EmergencyInfo;
            ATP.EmergencyInfo.Visible = ATP.EmergencyInfo.InfoType > 0;

            //机控人控
            ATP.TrainControlType = (TrainControlType)SignalDataIn300t.BrakeMode;
            ATP.TrainControlTypeVisible = ATP.TrainControlType != TrainControlType.Unkown;
            //电源
            ATP.ATPPower.ATPPowerState = SignalDataIn300t.ATPPowerFlag ? ATPPowerState.Started : ATPPowerState.Stopped;

           

            //司机号、车次号
            ATP.TrainInfo.Driver.TrainIdVisible = true;
            ATP.TrainInfo.Driver.TrainId = SignalDataOut300t.TrainCode;
            ATP.TrainInfo.Driver.DriverId = SignalDataOut300t.DriverCode;

            //时间日期
            ATP.Other.TimeVisible = true;
            ATP.Other.DateVisible = true;
            ATP.Other.DateTimeTitleVisible = true;

            //消息
            ATP.Message.Visible = true;
            ATP.Message.CanNavigateDownVisible = true;
            ATP.Message.CanNavigateUpVisible = true;

           
            //ATP启动请等待
            if (SignalDataIn300t.nSelfCheckStatus != (int)SelfCheckStatus.CheckStatus_Success)
            {

                ATP.Other.TimeVisible = false;
                ATP.Other.DateVisible = false;
                ATP.Other.DateTimeTitleVisible = false;
                ATP.SpeedMonitoringSection.ZoomVisible = false;


                ATP.TrainInfo.Driver.TrainIdVisible = false;
                ATP.TrainInfo.Station.Visible = false;
                ATP.TrainInfo.ConnectState.Visible = false;
                ATP.TrainInfo.KilometerPost.Visible = false;
                ATP.TrainInfo.Brake.Visible = false;
                ATP.TrainInfo.Speed.Visible = false;;
                ATP.CabSignal.Visible = false;
                ATP.CTCS.Visible = false;

                ATP.Message.CanNavigateDownVisible = false;
                ATP.Message.CanNavigateUpVisible = false;

                ATP.ControlModel.Visible = false;

                ATP.ControlModel.InEffect = false;
                ATP.TrainInfo.ConnectState.RBCConnectState = RBCConnectState.Invalidate;
                ATP.TrainInfo.ConnectState.GSMRState = GSMRState.Invalidate;
                ATP.TrainInfo.Brake.Visible = false;
                ATP.TrainControlTypeVisible = false;
            }
            else if (SignalDataIn300t.DriverActFlag || (ControlType)SignalDataIn300t.ControlMode == ControlType.Isolated)//驾驶室未激活
            {

                ATP.TrainInfo.Driver.TrainIdVisible = false;
                ATP.TrainInfo.Station.Visible = false;
                ATP.TrainInfo.ConnectState.Visible = false;
                ATP.TrainInfo.KilometerPost.Visible = false;
                ATP.TrainInfo.Brake.Visible = false;
                ATP.CabSignal.Visible = false;
                ATP.CTCS.Visible = false;
                ATP.TrainInfo.ConnectState.RBCConnectState = RBCConnectState.Invalidate;
                ATP.TrainInfo.ConnectState.GSMRState = GSMRState.Invalidate;
                ATP.TrainInfo.Brake.Visible = false;
                ATP.TrainControlTypeVisible = false;
            }
            else if ((ControlType)SignalDataIn300t.ControlMode == ControlType.Sleep)
            {
                ATP.TrainInfo.Driver.TrainIdVisible = false;
                ATP.TrainInfo.Station.Visible = false;
                ATP.TrainInfo.ConnectState.Visible = false;
                ATP.TrainInfo.KilometerPost.Visible = false;
                ATP.TrainInfo.Brake.Visible = false;
                ATP.Message.Visible = false;
                ATP.CabSignal.Visible = false;
                ATP.CTCS.Visible = false;
                ATP.TrainInfo.ConnectState.RBCConnectState = RBCConnectState.Invalidate;
                ATP.TrainInfo.ConnectState.GSMRState = GSMRState.Invalidate;
                ATP.TrainInfo.Brake.Visible = false;
                ATP.TrainControlTypeVisible = false;
            }
            

            return true;

        }


        public override bool ScreenBtnEnable()
        {
            //按钮触发状态

            for (int i = 0; i < SignalDataIn300t.ScreenBotton.Length; i++)
            {
                HardwareButton HBInfo = ATP.ATPHardwareButton.HardwareButtonCollection[i];
                HBInfo.Type = (UserActionType) (i + 1);
                HBInfo.MouseState = SignalDataIn300t.ScreenBotton[i] ? MouseState.MouseDown : MouseState.MouseUp;
            }
            //按钮使能状态
            //0-数据，1-模式，2-载频，3-等级，4-其他，5-启动，6-缓解,7-警惕,8-司机号,9-车次号,10-列车数据,11-RBC数据，12-调车，13-目视，14-机信，300h(15-制动测试执行，16-制动测试略过
            //17-RBCID，18-RBC电话号码，19-RBC网络号码)，20-制动测试，21-DMI测试,22-C0，23-C2,24-C3,25-列车数据，26-常用制动测试，27-紧急制动测试
            /*********300T按钮使能***********
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
            //屏自检完成以前，所有按钮灰色
            if (SignalDataIn300t.nSelfCheckStatus != (int)SelfCheckStatus.CheckStatus_Success || SignalDataIn300t.ControlMode == (int)ControlType.Isolated
                || SignalDataIn300t.DriverActFlag || SignalDataIn300t.ControlMode == (int)ControlType.Sleep)
            {
                for (int i = 0; i < 8; i++)
                {
                    BtnEnable[i] = false;
                }
            }
            else
            {   
                //启动完成前模式、等级、启动为灰色
               
                 if (!SignalDataIn300t.StartFinishFlag)
                {
                    if ((BrakeTestStatus)SignalDataIn300t.BrakeTestStatus != BrakeTestStatus.BrakeTestSucceed)
                    {
                        BtnEnable[1] = false;
                    }
                  
                    BtnEnable[3] = false;

                    if (!SignalDataIn300t.StartConditionFlag)
                    {
                        BtnEnable[3] = false;
                        BtnEnable[5] = false;
                    }
                     //c3模式下制动测试成功、选择了载频和列车数据等级使能
                    if ((CTCSType)SignalDataIn300t.RunLevel == CTCSType.CTCS3  )
                     {
                         if (SignalDataIn300t.StartTrainDataSelected && SignalDataIn300t.StartFrequencySelected &&
                             (BrakeTestStatus)SignalDataIn300t.BrakeTestStatus == BrakeTestStatus.BrakeTestSucceed)
                        {
                            BtnEnable[3] = true;
                        }
                     }
                }
                //速度大于0，速度大于零，载频、等级均不允许转换，司机号、列车数据不可更该
                if (SignalDataIn300t.TrainSpeed >0.5)
                {
                    BtnEnable[2] = false;
                    BtnEnable[3] = false;
                    BtnEnable[8] = false;
                    BtnEnable[10] = false;
                }
                //冒进和冒进、调车后，等级灰
                if ((ControlType)SignalDataIn300t.ControlMode == ControlType.PostTrip || (ControlType)SignalDataIn300t.ControlMode == ControlType.Trip 
                    || (ControlType)SignalDataIn300t.ControlMode == ControlType.Shunting)
                {
                    BtnEnable[3] = false;
                }
                //完全模式下载频键不可用
                if ((ControlType)SignalDataIn300t.ControlMode == ControlType.FullSupervision)
                {
                    BtnEnable[2] = false;
                }
                //C3完全模式启动不可用
                if ((ControlType)SignalDataIn300t.ControlMode == ControlType.FullSupervision && (CTCSType)SignalDataIn300t.RunLevel == CTCSType.CTCS3)
                {
                     BtnEnable[5] = false;
                }

               //C3下，调车及机信不可按
                if ((CTCSType)SignalDataIn300t.RunLevel == CTCSType.CTCS3)
                {
                    BtnEnable[12] = false;
                    BtnEnable[14] = false;
                }
            }
            //如果模式为灰，二级目录也置为灰
            if ( !BtnEnable[1])
            {
                BtnEnable[12] = false;
                BtnEnable[13] = false;
                BtnEnable[14] = false;
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
                    default:
                        break;
                }
            }

            return true;
        }

        public override bool ScreenTextInfo()
        {
            InfomationCreater InfoCreater =
                (InfomationCreater) ATP.InfomationService.GetInformationCreater();
            InfoCreater.ClearContents();

           
            //启动过程文本—start—————————————————————————————————————————————

           

           

            //制动测试
            if (SignalDataIn300tOld.BrakeTestStatus != SignalDataIn300t.BrakeTestStatus && SignalDataIn300t.nSelfCheckStatus == (int)SelfCheckStatus.CheckStatus_Success)
            {
                switch ((BrakeTestStatus) SignalDataIn300t.BrakeTestStatus)
                {
                    case BrakeTestStatus.BrakeTesting:
                        InfoCreater.UpdateInfomation(16);
                        break;
                    case BrakeTestStatus.BrakeTestSucceed:
                        InfoCreater.UpdateInfomation(17);
                        break;
                    case BrakeTestStatus.BrakeTestFail:
                        InfoCreater.UpdateInfomation(59);
                        break;
                    case BrakeTestStatus.BrakeTestCannotStart:
                        InfoCreater.UpdateInfomation(84);
                        break;
                }
            }

            if (!SignalDataIn300t.StartConditionFlag && !SignalDataIn300t.StartFinishFlag)
            {
                //1、ATP启动,请等待，正常等待1分42秒，按快捷键跳转
                if (SignalDataIn300tOld.ATPPowerFlag != SignalDataIn300t.ATPPowerFlag && SignalDataIn300t.ATPPowerFlag)
                {
                    InfoCreater.UpdateInfomation(15);
                }
                if (SignalDataIn300t.nSelfCheckStatus == (int)SelfCheckStatus.CheckStatus_Success && SignalDataIn300tOld.nSelfCheckStatus != (int)SelfCheckStatus.CheckStatus_Success)
                {
                    InfoCreater.UpdateInfomation(15, InformationUpdateType.Remove);
                }


                //制动测试请求：
                if (SignalDataOut300t.ConfirmOrCancelDriveDataInputSign == MessageConfirmType.MessageConfirmType_Confirm )
                {
                    SignalDataOut300t.ConfirmOrCancelDriveDataInputSign = 0;
                    InfoCreater.UpdateInfomation(35);
                }

                if (((SignalDataOut300tOld.BrakeTest && !SignalDataOut300t.BrakeTest)
                || SignalDataOut300t.CancleBrakeTest)
                && SignalDataIn300t.RunLevel == (int)CTCSType.CTCS2)
                {
                    InfoCreater.UpdateInfomation(70);
                    //SignalDataOut300t.BrakeTest = false;
                    SignalDataOut300t.CancleBrakeTest = false;
                }
               
                else if (SignalDataOut300t.ConfirmOrCancelTrainDataInputSign == MessageConfirmType.MessageConfirmType_Confirm)
                {
                    InfoCreater.UpdateInfomation(23, InformationUpdateType.Remove);
                }
                //确认C2
                if (SignalDataOut300tOld.C2ConfirmSign && !SignalDataOut300t.C2ConfirmSign)
                {
                    InfoCreater.UpdateInfomation(23);
                }
                //取消确认RBC
                if (SignalDataOut300t.ConfirmOrCancelRBCInputSign == MessageConfirmType.MessageConfirmType_Confirm
                    && ((RBCConnectState)SignalDataIn300t.RBCStatus == RBCConnectState.ConnectFault || (RBCConnectState)SignalDataIn300t.RBCStatus == RBCConnectState.Connected))
                {
                    InfoCreater.UpdateInfomation(23);
                }
                else if (SignalDataOut300t.ConfirmOrCancelRBCInputSign == MessageConfirmType.MessageConfirmType_Cancel)
                {
                    SignalDataOut300t.ConfirmOrCancelRBCInputSign = 0;
                    InfoCreater.UpdateInfomation(83);
                }
            }
            //7、确认启动
            if (!SignalDataIn300t.StartFinishFlag)
            {
                if (SignalDataIn300t.StartConditionFlag && !SignalDataIn300tOld.StartConditionFlag)
                {
                    InfoCreater.UpdateInfomation(57);
                }
            }
            
            //启动过程文本—end—————————————————————————————————————————————

            //司机室激活
            if ((SignalDataIn300tOld.DriverActFlag != SignalDataIn300t.DriverActFlag && SignalDataIn300t.DriverActFlag && SignalDataIn300t.nSelfCheckStatus == (int)SelfCheckStatus.CheckStatus_Success)||
                (SignalDataIn300t.DriverActFlag && SignalDataIn300t.nSelfCheckStatus == (int)SelfCheckStatus.CheckStatus_Success && SignalDataIn300tOld.nSelfCheckStatus != (int)SelfCheckStatus.CheckStatus_Success))
            {
               
                InfoCreater.UpdateInfomation(14);
               
            }
            if (SignalDataIn300tOld.DriverActFlag != SignalDataIn300t.DriverActFlag && !SignalDataIn300t.DriverActFlag)
            {
                InfoCreater.UpdateInfomation(14, InformationUpdateType.Remove);
            }
            //等级选择后出现的确认文本
            if (SignalDataOut300t.ConfirmOrCancelLevelSign == MessageConfirmType.MessageConfirmType_Confirm)
           {
               SignalDataOut300t.ConfirmOrCancelLevelSign = 0;
               InfoCreater.UpdateInfomation(81);
           }
            else if (SignalDataOut300t.ConfirmOrCancelLevelSign == MessageConfirmType.MessageConfirmType_Cancel)
            {
                SignalDataOut300t.ConfirmOrCancelLevelSign = 0;
                InfoCreater.UpdateInfomation(82);
            }
           //常用制动缓解和紧急制动缓解
            if ((BrakeStatus)SignalDataIn300t.BrakeStatus == BrakeStatus.CommonBreak_R)
            {
                if ((BrakeStatus)SignalDataIn300tOld.BrakeStatus == BrakeStatus.CommonBreak_E)
                {
                    InfoCreater.UpdateInfomation(19);
                }
                else if ((BrakeStatus)SignalDataIn300tOld.BrakeStatus <= BrakeStatus.CommonBreak_7 && (BrakeStatus)SignalDataIn300tOld.BrakeStatus >= BrakeStatus.CommonBreak_1)
                {
                    InfoCreater.UpdateInfomation(18);
                }
            }

            //退行或溜逸防护
            if (SignalDataIn300tOld.BrakeStatus != (int)BrakeStatus.CommonBreak_E && SignalDataIn300t.BrakeStatus == (int)BrakeStatus.CommonBreak_E
                && SignalDataIn300t.EBType == (int)EBType.EBTYPE_SlideProtect)
            {
                if (SignalDataIn300t.ControlMode == (int)ControlType.StandBy)
                {
                    InfoCreater.UpdateInfomation(72);
                }
                else
                {
                    InfoCreater.UpdateInfomation(47);
                }
            }

            //禁止调车
            if (SignalDataIn300tOld.SHForbidFlag != SignalDataIn300t.SHForbidFlag)
            {
                if (SignalDataIn300t.SHForbidFlag && SignalDataIn300t.ControlMode == (int) ControlType.Shunting)
                {
                    InfoCreater.UpdateInfomation(20);
                }
            }
            //RBC允许调车
            if (SignalDataIn300tOld.RBCAllowSH != SignalDataIn300t.RBCAllowSH)
            {
                switch (SignalDataIn300t.RBCAllowSH)
                {
                    case 1:
                        InfoCreater.UpdateInfomation(22);
                        break;
                    case 2:
                        InfoCreater.UpdateInfomation(21);
                        break;
                }
            }

            //绝对停车

            //RBC状态
            if (SignalDataIn300tOld.RBCStatus != SignalDataIn300t.RBCStatus)
            {
                if ((RBCConnectState)SignalDataIn300t.RBCStatus == RBCConnectState.Connecting)
                {
                    InfoCreater.UpdateInfomation(60);
                }
                else if ((RBCConnectState)SignalDataIn300t.RBCStatus == RBCConnectState.Connected)
                {
                    //InfoCreater.UpdateInfomation();
                }
                else if ((RBCConnectState)SignalDataIn300t.RBCStatus == RBCConnectState.ConnectFault)
                {
                    InfoCreater.UpdateInfomation(58);
                }
            }



            //闪烁文本—start—————————————————————————————————————————————

            //警惕、冒进、越行、引导确认
            if (!SignalDataIn300tOld.AlertAck && SignalDataIn300t.AlertAck)
            {
                InfoCreater.UpdateInfomation(37);
            }
            
            if (!SignalDataIn300tOld.ExceAck && SignalDataIn300t.ExceAck )
            {   
                InfoCreater.UpdateInfomation(40);
            }
            if (SignalDataIn300tOld.ExceAck && !SignalDataIn300t.ExceAck)
            {    
                InfoCreater.UpdateInfomation(40, InformationUpdateType.Ensure);
            }
           
          
            if (!SignalDataIn300tOld.SRAck && SignalDataIn300t.SRAck)
            {
                if ((CTCSType)SignalDataIn300t.RunLevel == CTCSType.CTCS3)
                {
                    InfoCreater.UpdateInfomation(41);
                }
                else if ((CTCSType)SignalDataIn300t.RunLevel == CTCSType.CTCS2)
                {
                    InfoCreater.UpdateInfomation(69);
                }
            }

            if (!SignalDataIn300tOld.MJAck && SignalDataIn300t.MJAck)
            {
                InfoCreater.UpdateInfomation(91);
            }

            //等级转换确认
            if (!SignalDataIn300tOld.EnterC2Ack && SignalDataIn300t.EnterC2Ack)
            {
                InfoCreater.UpdateInfomation(42);
            }
            if (!SignalDataIn300tOld.EnterC3Ack && SignalDataIn300t.EnterC3Ack)
            {
                InfoCreater.UpdateInfomation(43);
            }

            
           
            //进入目视模式确认
            if (!SignalDataIn300tOld.EnterOSAck && SignalDataIn300t.EnterOSAck)
            {
                InfoCreater.UpdateInfomation(51);
            }
           
            //等级转换文本
            if (SignalDataIn300tOld.RunLevel != SignalDataIn300t.RunLevel && SignalDataIn300t.StartConditionFlag)
            {
                if (SignalDataIn300t.RunLevel == (int) CTCSType.CTCS2)
                {
                    InfoCreater.UpdateInfomation(10);
                }
                else if (SignalDataIn300t.RunLevel == (int) CTCSType.CTCS3)
                {
                    InfoCreater.UpdateInfomation(11);
                }
            }
            //闪烁文本—end—————————————————————————————————————————————
            //退行或溜逸防护确认
            if (!SignalDataIn300tOld.SlideProtectAck && SignalDataIn300t.SlideProtectAck)
            {
                InfoCreater.UpdateInfomation(46);
            }
            if (!SignalDataIn300tOld.BackProtectAck && SignalDataIn300t.BackProtectAck)
            {
                InfoCreater.UpdateInfomation(47);
            }
            //等级转换预告文本
            if (SignalDataIn300tOld.YGLevelInfo != SignalDataIn300t.YGLevelInfo)
            {
                if (SignalDataIn300t.YGLevelInfo == (int)LevelChangeType.YG_CTCS3TOCTCS2)
                {
                    InfoCreater.UpdateInfomation(12);
                }
                if (SignalDataIn300t.YGLevelInfo == (int)LevelChangeType.YG_CTCS2TOCTCS3)
                {
                    InfoCreater.UpdateInfomation(13, InformationUpdateType.Remove);
                }
            }
      

            //模式转换文本
            if (SignalDataIn300tOld.ControlMode != SignalDataIn300t.ControlMode)
            {
                switch ((ControlType) SignalDataIn300t.ControlMode)
                {
                    case ControlType.FullSupervision:
                        InfoCreater.UpdateInfomation(7);
                        break;
                    case ControlType.StandBy:
                        if (SignalDataIn300tOld.ControlMode != (int)ControlType.Unknown)
                        {
                            InfoCreater.UpdateInfomation(1);
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
                    case ControlType.LKJ:
                        InfoCreater.UpdateInfomation(6);
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
                    default:
                        break;
                }
            }
            //越行提示
            if (!SignalDataIn300tOld.ExceTipsFlag && SignalDataIn300t.ExceTipsFlag)
            {
                InfoCreater.UpdateInfomation(92);
            }
            if (SignalDataIn300tOld.ExceTipsFlag && !SignalDataIn300t.ExceTipsFlag)
            {
                InfoCreater.UpdateInfomation(92, InformationUpdateType.Remove);
            }
            //故障文本—————————start—————————————————————————————————
            //故障闪烁文本
            //常用制动故障确认
            if (!SignalDataIn300tOld.FaultNormalBreakAck && SignalDataIn300t.FaultNormalBreakAck)
            {
                InfoCreater.UpdateInfomation(44);
            }
            //应答器信息丢失
            if (!SignalDataIn300tOld.EnterBaliseLostAck && SignalDataIn300t.EnterBaliseLostAck)
            {
                InfoCreater.UpdateInfomation(87);
            }
            //故障普通文本
            //“地面设备故障”
            if (!SignalDataIn300tOld.Fault[0] && SignalDataIn300t.Fault[0])
            {
                InfoCreater.UpdateInfomation(85);
            }
            //“速度传感器故障”
            if (!SignalDataIn300tOld.Fault[1] && SignalDataIn300t.Fault[1])
            {
                InfoCreater.UpdateInfomation(27);
            }
            //“ATPCU故障”
            if (!SignalDataIn300tOld.Fault[2] && SignalDataIn300t.Fault[2])
            {
                InfoCreater.UpdateInfomation(33);
            }
            //“安全软件故障”
            if (!SignalDataIn300tOld.Fault[3] && SignalDataIn300t.Fault[3])
            {
                InfoCreater.UpdateInfomation(29);
            }
            //“DMI与主机通信中断”
            if (!SignalDataIn300tOld.Fault[4] && SignalDataIn300t.Fault[4])
            {
                InfoCreater.UpdateInfomation(28);
            }
            //“轨道电路传输模块故障”
            if (!SignalDataIn300tOld.Fault[5] && SignalDataIn300t.Fault[5])
            {
                InfoCreater.UpdateInfomation(86);
            }
            //“应答器报文错误”
            if (!SignalDataIn300tOld.Fault[6] && SignalDataIn300t.Fault[6])
            {
                InfoCreater.UpdateInfomation(30);
            }
            //“无线连接超时”
            if (!SignalDataIn300tOld.Fault[7] && SignalDataIn300t.Fault[7])
            {
                InfoCreater.UpdateInfomation(31);
            }
            //“CTCS2故障”
            if (!SignalDataIn300tOld.Fault[8] && SignalDataIn300t.Fault[8])
            {
                InfoCreater.UpdateInfomation(34);
            }
            //“BTM故障”
            if (!SignalDataIn300tOld.Fault[9] && SignalDataIn300t.Fault[9])
            {
                InfoCreater.UpdateInfomation(32);
            }
            //“测速系统故障”
            if (!SignalDataIn300tOld.Fault[10] && SignalDataIn300t.Fault[10])
            {
                InfoCreater.UpdateInfomation(78);
            }
            //“不在RBC管辖范围”
            if (!SignalDataIn300tOld.Fault[11] && SignalDataIn300t.Fault[11])
            {
                InfoCreater.UpdateInfomation(88);
            }
            //“应答器信息丢失”
            if (!SignalDataIn300tOld.Fault[12] && SignalDataIn300t.Fault[12])
            {
                InfoCreater.UpdateInfomation(89);
            }
            //“绝对停车”
            if (!SignalDataIn300tOld.Fault[13] && SignalDataIn300t.Fault[13])
            {
                InfoCreater.UpdateInfomation(90);
            }
            //故障文本—————————end—————————————————————————————————
            InfoCreater.FlushInfomation();

            return true;
        }

        public override bool ScreenControl()
        {
            //启动流程控制
            //1、在启动等待1分多钟

            if (!SignalDataIn300t.StartConditionFlag && !SignalDataIn300t.DriverActFlag && !SignalDataIn300t.ATPBypassFlag)
            {
                // 2、司机号和车次号输入:SendDriverData中处理
                //确定进入第二步
                //取消继续显示驾驶数据
                if (SignalDataIn300tOld.nSelfCheckStatus != SignalDataIn300t.nSelfCheckStatus && SignalDataIn300t.nSelfCheckStatus == (int)SelfCheckStatus.CheckStatus_Success)
                   
                {
                    ATP.DriverInterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_驾驶数据));
                }
                //3、制动测试：
                if (SignalDataOut300t.ConfirmOrCancelDriveDataInputSign == MessageConfirmType.MessageConfirmType_Confirm)
                {
                    SignalDataOut300t.ConfirmOrCancelDriveDataInputSign = 0;

                }else if(SignalDataOut300t.ConfirmOrCancelDriveDataInputSign == MessageConfirmType.MessageConfirmType_Cancel)
                {
                    SignalDataOut300t.ConfirmOrCancelDriveDataInputSign = 0;
                    ATP.DriverInterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_驾驶数据));
                }

                //4、等级选择
                //5、列车数据
                if (SignalDataOut300t.ConfirmOrCancelC2Sign == MessageConfirmType.MessageConfirmType_Confirm )
                {
                   
                    SignalDataOut300t.ConfirmOrCancelC2Sign = 0;
                    ATP.DriverInterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_数据_列车数据));
                }
                else if (SignalDataOut300t.ConfirmOrCancelC2Sign == MessageConfirmType.MessageConfirmType_Cancel)
                {
                    SignalDataOut300t.ConfirmOrCancelC2Sign = 0;
                    ATP.DriverInterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_等级));

                }
                
                //取消确认C3
                if (SignalDataOut300t.ConfirmOrCancelC3Sign == MessageConfirmType.MessageConfirmType_Confirm)
                {
                    SignalDataOut300t.ConfirmOrCancelC3Sign = 0;
                    ATP.DriverInterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_数据_RBC数据));
                }
                else if (SignalDataOut300t.ConfirmOrCancelC3Sign == MessageConfirmType.MessageConfirmType_Cancel)
                {
                    SignalDataOut300t.ConfirmOrCancelC3Sign = 0;
                    ATP.DriverInterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_等级));

                }
                //C3起机制动测试后
                if (((SignalDataOut300tOld.BrakeTest && !SignalDataOut300t.BrakeTest)
                || SignalDataOut300t.CancleBrakeTest)
                && SignalDataIn300t.RunLevel == (int)CTCSType.CTCS3)
                {
                    SignalDataOut300t.CancleBrakeTest = false;
                    ATP.DriverInterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_等级));
                }
                //确认C2/C3   
                if (SignalDataOut300tOld.C2ConfirmSign && !SignalDataOut300t.C2ConfirmSign)
                {
                   
                    ATP.DriverInterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_数据_列车数据));
                }
               

                //取消确认RBC
                if (SignalDataOut300t.ConfirmOrCancelRBCInputSign == MessageConfirmType.MessageConfirmType_Confirm
                    && ((RBCConnectState)SignalDataIn300t.RBCStatus == RBCConnectState.ConnectFault || (RBCConnectState)SignalDataIn300t.RBCStatus == RBCConnectState.Connected))
                {
                    SignalDataOut300t.ConfirmOrCancelRBCInputSign = 0;
                 
                   ATP.DriverInterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_数据_列车数据));
                }
                
                //6、载频
                if (SignalDataOut300t.ConfirmOrCancelTrainDataInputSign == MessageConfirmType.MessageConfirmType_Confirm)
                {
                    SignalDataOut300t.ConfirmOrCancelTrainDataInputSign = 0;
                    ATP.DriverInterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_载频));
                }

            }



            if (SignalDataOut300tOld.C3ConfirmSign && !SignalDataOut300t.C3ConfirmSign)
            {
                ATP.DriverInterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_数据_RBC数据));
            }
            //退出调车进入待机，输入驾驶数据
            if (!SignalDataOut300tOld.SHModeExit && SignalDataOut300t.SHModeExit)
            {
                ATP.DriverInterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_驾驶数据));
            }
            return true;
        }

        public override bool  ClearInfos()
        {
            InfomationCreater InfoCreater =
                (InfomationCreater)ATP.InfomationService.GetInformationCreater();
            InfoCreater.ClearShowingItems();

            ATP.Other.ShowingTimeDifference = TimeSpan.Zero;

            ATP.GetInterfaceController().UpdateDriverInterface(DriverInterfaceKey.Root);
           
            //清除输入信息

            SignalDataIn300t.ClearInfo();
            SignalDataOut300t.ClearInfo();
            return true;
        }
    }
}

