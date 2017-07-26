using System;
using CommonUtil.Controls;
using MMI.Facility.Interface.Data;
using Motor.ATP.DataAdapter.Resource.Keys;
using Motor.ATP.Infrasturcture.Control.Infomation;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.BrakeTest;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Extension;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP.DataAdapter.ConcreateAdapter.ATP300S
{
    public class ATP300SDataAdapter : DataAdapterBase
    {

        protected SignalDataIn300S SignalDataIn300s { get; set; }
        protected SignalDataIn300S SignalDataIn300sOld { get; set; }
        protected SignalDataOut300S SignalDataOut300s { get; set; }


        public ATP300SDataAdapter(ATPDomain adaptTarget)
            : base(adaptTarget, new SendInterface300S(new SignalDataOut300S()))
        {
            SignalDataIn300s = new SignalDataIn300S();
            SignalDataIn300sOld = new SignalDataIn300S();
            SignalDataIn300sOld = (SignalDataIn300S)SignalDataIn300s.Clone();
            SignalDataIn = SignalDataIn300s;
            SignalDataInOld = SignalDataIn300sOld;

            SignalDataOut300s = (SignalDataOut300S) SignalDataOut;
            SignalDataOut300s.SignalDataIn = SignalDataIn300s;
            SignalDataOut300s.ATP = ATP;
            SignalDataOut = SignalDataOut300s;
        }

        public override void OnBoolChangedImp(object sender, CommunicationDataChangedWrapperArgs<bool> dataChangedArgs)
        {
            base.OnBoolChangedImp(sender,dataChangedArgs);

            //黑屏
            dataChangedArgs.UpdateIfContains(InBoolKeys.ATP电源, b => SignalDataIn300s.ScreenBlackFlag = b);

            //常用制动故障确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_常用制动故障确认, b => SignalDataIn300s.FaultNormalBreakAck = b);

            //已选择越行，确认越行
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_已选择越行确认越行, b => SignalDataIn300s.ExceSelAck = b);

            //退行防护确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_退行防护制动确认, b => SignalDataIn300s.BackProtectAck = b);

            //车载设备自动换系
            dataChangedArgs.UpdateIfContains(InBoolKeys.车载设备自动换系, b => SignalDataIn300s.ChangeHostAuto = b);

            //进入目视模式确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_进入目视模式, b => SignalDataIn300s.EnterOSAck = b);

            //启动完成确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_启动完成确认, b => SignalDataIn300s.StartFinishAck = b);

            //紧急制动缓解确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_确认紧急制动缓解, b => SignalDataIn300s.EBrakeRelAck = b);

            //常用制动缓解确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_确认常用制动缓解, b => SignalDataIn300s.CBrakeRelAck = b);

            //应答器丢失确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_应答器信息丢失确认, b => SignalDataIn300s.EnterBaliseLostAck = b);

            //系统自检状态
            dataChangedArgs.UpdateIfContains(InBoolKeys.车载设备自检状态, b => SignalDataIn300s.SelfCheckStatus = b);

            //启动流程相关
            dataChangedArgs.UpdateIfContains(InBoolKeys.启动_司机号已确认, b => SignalDataIn300s.StartDriverNumConfirmed = b);
            dataChangedArgs.UpdateIfContains(InBoolKeys.启动_等级已选择, b => SignalDataIn300s.StartCtcsSelected = b);
            dataChangedArgs.UpdateIfContains(InBoolKeys.启动_车次号已确认, b => SignalDataIn300s.StartTrainNumConfirmed = b);
            dataChangedArgs.UpdateIfContains(InBoolKeys.启动_列车数据已选择, b => SignalDataIn300s.StartTrainDataSelected = b);
            dataChangedArgs.UpdateIfContains(InBoolKeys.启动_载频已选择, b => SignalDataIn300s.StartFrequencySelected = b);
            dataChangedArgs.UpdateIfContains(InBoolKeys.启动_载频已确认, b => SignalDataIn300s.StartFrequencyConfirmed = b);
            dataChangedArgs.UpdateIfContains(InBoolKeys.启动_列车数据已确认, b => SignalDataIn300s.StartTrainDataConfirmed = b);

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
            if ((SignalDataIn300sOld.ATPPowerFlag && !SignalDataIn300s.ATPPowerFlag)
                || (!SignalDataIn300sOld.ATPBypassFlag && SignalDataIn300s.ATPBypassFlag))
            {
                ClearInfos();
            }


            //4、保存上周期状态 
            //if (SignalDataIn300sOld.ControlMode != SignalDataIn300s.ControlMode)
            //{
            //    Trace.WriteLine("old data：" + SignalDataIn300sOld.ControlMode.ToString());
            //    Trace.WriteLine("new data：" + SignalDataIn300s.ControlMode.ToString());
            //}
            SignalDataIn300sOld = (SignalDataIn300S) SignalDataIn300s.Clone();

            //GC.Collect();

        }

        public override bool FeedBackInfo(object sender, CommunicationDataChangedWrapperArgs<bool> dataChangedArgs)
        {
            base.FeedBackInfo(sender, dataChangedArgs);


            //反馈_启动确认_没有消息提示与确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_启动确认_没有消息提示与确认, b =>
            {
                if (b)
                {
                    SignalDataOut300s.StartSign = false;
                }
            });

           
            //反馈_确认常用制动故障
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_确认常用制动故障, b =>
            {
                if (b)
                {
                    SignalDataOut300s.FaultNormalBreakSign = false;
                }
            });

            //反馈_确认溜逸防护制动
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_确认溜逸防护制动, b =>
            {
                if (b)
                {
                    SignalDataOut300s.SlideConfirmSign = false;
                }
            });

            //反馈_确认紧急制动缓解
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_确认紧急制动缓解, b =>
            {
                if (b)
                {
                    SignalDataOut300s.EBRelSign = false;
                }
            });

            //反馈_确认常用制动缓解
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_确认常用制动缓解, b =>
            {
                if (b)
                {
                    SignalDataOut300s.CBRelSign = false;
                }
            });


            //反馈_应答器丢失确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_应答器丢失确认, b =>
            {
                if (b)
                {
                    SignalDataOut300s.BaliseLostSign = false;
                }
            });

            //反馈_确认退行防护制动
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_确认退行防护制动, b =>
            {
                if (b)
                {
                    SignalDataOut300s.BackProtectSign = false;
                }
            });

            //反馈_确认选择越行
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_确认选择越行, b =>
            {
                if (b)
                {
                    SignalDataOut300s.ExceSelSign = false;
                }
            });



            //反馈_启动列车数据确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_启动列车数据已确认, b =>
            {
                if (b)
                {
                    SignalDataOut300s.StartTrainDataSign = false;
                }
            });

            //反馈_启动载频确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_启动载频已确认, b =>
            {
                if (b)
                {
                    SignalDataOut300s.StartFreqSign = false;
                }
            });
            //反馈_取消确认越性选择
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_取消确认选择越行, b =>
            {
                if (b)
                {
                    SignalDataOut300s.CancleExceSelSign = false;
                }
            });


            return true;
        }

        public override bool ScreenToSignalInfo()
        {
            base.ScreenToSignalInfo();
            //bool

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.启动确认_没有消息提示与确认],
                SignalDataOut300s.StartSign);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认常用制动故障],
                SignalDataOut300s.FaultNormalBreakSign);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认溜逸防护制动],
                SignalDataOut300s.SlideConfirmSign);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认紧急制动缓解],
                SignalDataOut300s.EBRelSign);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认常用制动缓解],
                SignalDataOut300s.CBRelSign);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.屏幕启动过程],
                SignalDataOut300s.StartProcessFlag);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.应答器丢失确认],
                SignalDataOut300s.BaliseLostSign);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认退行防护制动],
                SignalDataOut300s.BackProtectSign);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认选择越行],
                SignalDataOut300s.ExceSelSign);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.启动_确认列车数据],
                SignalDataOut300s.StartTrainDataSign);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.启动_确认载频],
                SignalDataOut300s.StartFreqSign);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.取消确认选择越行],
                SignalDataOut300s.CancleExceSelSign);


            return true;
        }

        public override bool BasicAndTrainInfo()
        {
            ATP.Other.NowInATP = SignalDataIn300s.NowTime;
            //速度及限速信息
            ATP.TrainInfo.Speed.Visible = !SignalDataIn300s.ChangeHostAuto;

            ATP.TrainInfo.Speed.CurrentSpeed.Value = Math.Abs(SignalDataIn300s.TrainSpeed);

            ATP.TrainInfo.Speed.ServiceBrakeInterventionSpeed.Value = SignalDataIn300s.SBISpeed;
            ATP.TrainInfo.Speed.EmergencyBrakeInterventionSpeed.Value = SignalDataIn300s.EBISpeed;
            ATP.TrainInfo.Speed.PermittedLimitSpeed.Value = SignalDataIn300s.TrainLimitSpeed;
            ATP.TrainInfo.Speed.IntervertionSpeed.Value = SignalDataIn300s.IntervertionSpeed;
            ATP.TrainInfo.Speed.WarningLimitSpeed.Value = SignalDataIn300s.WarningSpeed;
            ATP.TrainInfo.Speed.TargetSpeed.Value = SignalDataIn300s.ControlMode != (int) ControlType.FullSupervision &&
                                                    SignalDataIn300s.ControlMode != (int) ControlType.CallingOn
                ? SignalDataIn300s.TrainLimitSpeed - 5
                : (SignalDataIn300s.TrainGoalSpeed > SignalDataIn300s.TrainLimitSpeed
                    ? SignalDataIn300s.TrainLimitSpeed
                    : SignalDataIn300s.TrainGoalSpeed);
            //制动信息
            ATP.TrainInfo.Brake.Visible = SignalDataIn300s.BrakeStatus > 1 && SignalDataIn300s.BrakeStatus < 7 &&
                                          SignalDataIn300s.BrakeTestStatus != (int) BrakeTestStatus.BrakeTesting;
            ATP.TrainInfo.Brake.BrakeType = (BrakeType) SignalDataIn300s.BrakeStatus;

            //车站
            ATP.TrainInfo.Station.CurrentStation = SignalDataIn300s.StationNo > 0
                ? GetStationName((ulong)SignalDataIn300s.StationNo)
                : "";
            //公里标
            ATP.TrainInfo.KilometerPost.Visible = SignalDataIn300s.IsGLBVisible;
            if (ATP.TrainInfo.KilometerPost.Visible)
            {
                ATP.TrainInfo.KilometerPost.Kilometer = Math.Truncate(SignalDataIn300s.TrainGLB / 1000);
                ATP.TrainInfo.KilometerPost.Meter = SignalDataIn300s.TrainGLB%1000;
            }
            else
            {
                ATP.TrainInfo.KilometerPost.Kilometer = 0;
                ATP.TrainInfo.KilometerPost.Meter = 0;
            }
            //GSM && RBC
            ATP.TrainInfo.ConnectState.GSMRState = !SignalDataIn300s.SelfCheckStatus
                ? GSMRState.Invalidate
                : (GSMRState) SignalDataIn300s.GSMStatus;

            ATP.TrainInfo.ConnectState.RBCConnectState = SignalDataIn300s.RBCStatus <= 0
                ? RBCConnectState.Invalidate
                : (RBCConnectState) SignalDataIn300s.RBCStatus;

            ATP.TrainInfo.ConnectState.Visible = true;
            //等级
            ATP.CTCS.CurrentCTCSType = (CTCSType) SignalDataIn300s.RunLevel;
            ATP.CTCS.Visible = ATP.CTCS.CurrentCTCSType == CTCSType.CTCS2 || ATP.CTCS.CurrentCTCSType == CTCSType.CTCS3;
            //模式
            ATP.ControlModel.InEffect = true;
            ATP.ControlModel.CurrentControlType = (ControlType) SignalDataIn300s.ControlMode;
            //TODO 增加其它模式
            ATP.SpeedMonitoringSection.ZoomVisible = ATP.ControlModel.CurrentControlType == ControlType.FullSupervision;
            ATP.RegionFStateProvier.ShuntingStateProvider.EnterOrQuitState = (ControlType) SignalDataIn300s.ControlMode ==
                                                                             ControlType.Shunting
                ? EnterOrQuit.Quit
                : EnterOrQuit.Enter;
            ATP.RegionFStateProvier.CabsignalStateProvider.EnterOrQuitState = (ControlType) SignalDataIn300s.ControlMode ==
                                                                              ControlType.LKJ
                ? EnterOrQuit.Quit
                : EnterOrQuit.Enter;

            //机车信号
            ATP.CabSignal.Code = (CabSignalCode)SignalDataIn300s.SignalCode;
            ATP.CabSignal.Visible = ATP.CabSignal.Code > 0;
            //机控人控
            ATP.TrainControlType = (TrainControlType)SignalDataIn300s.BrakeMode;
            ATP.TrainControlTypeVisible = ATP.TrainControlType != TrainControlType.Unkown;
            //电源
            ATP.ATPPower.ATPPowerState = SignalDataIn300s.ATPPowerFlag ? ATPPowerState.Started : ATPPowerState.Stopped;

            //司机号、车次号
            ATP.TrainInfo.Driver.TrainId = SignalDataOut300s.TrainCode;
            ATP.TrainInfo.Driver.DriverId = SignalDataOut300s.DriverCode;

            //时间日期
            ATP.Other.TimeVisible = SignalDataIn300s.SelfCheckStatus;
            ATP.Other.DateVisible = SignalDataIn300s.StartFinishFlag ||
                                    (BrakeTestStatus)SignalDataIn300s.BrakeTestStatus == BrakeTestStatus.BrakeTestSucceed;

            return true;

        }


        public override bool ScreenBtnEnable()
        {
            //按钮触发状态

            for (int i = 0; i < SignalDataIn300s.ScreenBotton.Length; i++)
            {
                HardwareButton HBInfo = ATP.ATPHardwareButton.HardwareButtonCollection[i];
                HBInfo.Type = (UserActionType) (i + 1);
                HBInfo.MouseState = SignalDataIn300s.ScreenBotton[i] ? MouseState.MouseDown : MouseState.MouseUp;
            }
            //按钮使能状态
            //0-数据，1-模式，2-载频，3-等级，4-其他，5-启动，6-缓解,7-警惕,8-司机号,9-车次号,10-列车数据,11-RBC数据，12-调车，13-目视，14-机信
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

            if (!SignalDataIn300s.SelfCheckStatus)
            {
                for (int i = 0; i < 8; i++)
                {
                    BtnEnable[i] = false;
                }
            }
            else
            {
                if ((CTCSType) SignalDataIn300s.RunLevel < CTCSType.CTCS2)
                {
                    BtnEnable[3] = false;
                    BtnEnable[5] = false;
                    BtnEnable[6] = false;
                    BtnEnable[7] = false;
                }
                else if ((CTCSType) SignalDataIn300s.RunLevel == CTCSType.CTCS2)
                {
                    if (!SignalDataIn300s.StartConditionFlag && !SignalDataIn300s.StartFinishFlag)
                    {
                        BtnEnable[5] = false;
                    }
                }
                else if ((CTCSType) SignalDataIn300s.RunLevel == CTCSType.CTCS3)
                {
                    BtnEnable[3] = false;
                    BtnEnable[12] = false;
                    BtnEnable[14] = false;
                    //C3下，1、目视且速度为0 2、待机且启动条件满足 3、冒后模式下 启动键可按
                    BtnEnable[5] = ((ControlType) SignalDataIn300s.ControlMode == ControlType.OnSight &&
                                    SignalDataIn300s.TrainSpeed <= 0.5)
                                   || (ControlType) SignalDataIn300s.ControlMode == ControlType.PostTrip
                                   ||
                                   ((ControlType) SignalDataIn300s.ControlMode == ControlType.StandBy &&
                                    SignalDataIn300s.StartConditionFlag);
                    //C3下允许缓解后，缓解键可按
                    if (SignalDataIn300s.BrakeStatus != (int) BrakeType.AllowRelease)
                    {
                        BtnEnable[6] = false;
                    }
                    //C3下目视及引导，警惕键可按
                    if ((ControlType) SignalDataIn300s.ControlMode != ControlType.CallingOn &&
                        (ControlType) SignalDataIn300s.ControlMode != ControlType.OnSight)
                    {
                        BtnEnable[7] = false;
                    }
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




            //司机室激活
            if (SignalDataIn300sOld.DriverActFlag != SignalDataIn300s.DriverActFlag)
            {
                if (SignalDataIn300s.DriverActFlag)
                {
                    InfoCreater.UpdateInfomation(14);
                }
            }

            //系统自检
            //if (SignalDataIn300sOld.ScreenStartFinishFlag!=SignalDataIn300s.ScreenStartFinishFlag)
            //{
            //    if (SignalDataIn300s.ScreenStartFinishFlag)
            //    {
            //        InfoCreater.UpdateInfomation(15, true);
            //    }
            //}
            if (SignalDataIn300sOld.ATPPowerFlag != SignalDataIn300s.ATPPowerFlag)
            {
                if (SignalDataIn300s.ATPPowerFlag)
                {
                    InfoCreater.UpdateInfomation(15);
                }
            }
            if (SignalDataIn300sOld.ATPBypassFlag != SignalDataIn300s.ATPBypassFlag)
            {
                if (!SignalDataIn300s.ATPBypassFlag && SignalDataIn300s.ATPPowerFlag)
                {
                    InfoCreater.UpdateInfomation(15);
                }
            }
            //制动测试
            if (SignalDataIn300sOld.BrakeTestStatus != SignalDataIn300s.BrakeTestStatus)
            {
                switch ((BrakeTestStatus) SignalDataIn300s.BrakeTestStatus)
                {
                    case BrakeTestStatus.BrakeTesting:
                        InfoCreater.UpdateInfomation(16);
                        break;
                    case BrakeTestStatus.BrakeTestSucceed:
                        InfoCreater.UpdateInfomation(17);
                        //ATP.DriverInterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_数据_司机号));
                        break;
                    case BrakeTestStatus.BrakeTestFail:
                        InfoCreater.UpdateInfomation(52);
                        InfoCreater.UpdateInfomation(33);
                        break;
                }
            }
            //制动缓解
            //if (SignalDataIn300sOld.BrakeStatus == (int) BrakeType.AllowRelease &&
            //    SignalDataIn300s.BrakeStatus == (int) BrakeType.None)
            //{
            //    switch (SignalDataIn300s.BrakeReleaseType)
            //    {
            //        case (int) BrakeType.MaxNormal:
            //            InfoCreater.UpdateInfomation(18, true);
            //            break;
            //        case (int) BrakeType.Emergent:
            //            InfoCreater.UpdateInfomation(19, true);
            //            break;
            //    }
            //}
            //禁止调车
            if (SignalDataIn300sOld.SHForbidFlag != SignalDataIn300s.SHForbidFlag)
            {
                if (SignalDataIn300s.SHForbidFlag && SignalDataIn300s.ControlMode == (int) ControlType.Shunting)
                {
                    InfoCreater.UpdateInfomation(20);
                }
            }
            //RBC允许调车
            if (SignalDataIn300sOld.RBCAllowSH != SignalDataIn300s.RBCAllowSH)
            {
                switch (SignalDataIn300s.RBCAllowSH)
                {
                    case 1:
                        InfoCreater.UpdateInfomation(22);
                        break;
                    case 2:
                        InfoCreater.UpdateInfomation(21);
                        break;
                }
            }
            if (SignalDataIn300sOld.RBCStatus != SignalDataIn300s.RBCStatus)
            {
                if ((RBCConnectState)SignalDataIn300s.RBCStatus == RBCConnectState.Connecting)
                {
                    InfoCreater.UpdateInfomation(53);
                }
            }


            //列车数据录入
            if (!SignalDataIn300s.StartFinishFlag)
            {
                //if (SignalDataIn300sOld.RunLevel < (int) CTCSType.CTCS2 &&
                //    (SignalDataIn300s.RunLevel == (int) CTCSType.CTCS2 || SignalDataIn300s.RunLevel == (int) CTCSType.CTCS3))
                //{
                //    InfoCreater.UpdateInfomation(23, true);
                //}

                if (!SignalDataIn300sOld.StartCtcsSelected && SignalDataIn300s.StartCtcsSelected)
                {
                    InfoCreater.UpdateInfomation(23);
                }
            }

            //请选择任务开始
            if (!SignalDataIn300s.StartFinishFlag)
            {
                if (SignalDataIn300sOld.StartConditionFlag != SignalDataIn300s.StartConditionFlag)
                {
                    if (SignalDataIn300s.StartConditionFlag)
                    {
                        //InfoCreater.UpdateInfomation(24, true);
                        InfoCreater.UpdateInfomation(48);
                    }
                }
            }

            //收到初始请求
            if (!SignalDataIn300sOld.StartFinishFlag && SignalDataIn300s.StartFinishFlag && (RBCConnectState)SignalDataIn300s.RBCStatus == RBCConnectState.Connected)
            {
                InfoCreater.UpdateInfomation(54);
            }

            //请求制动测试
            if (!SignalDataIn300sOld.BrakeTestAck && SignalDataIn300s.BrakeTestAck)
            {
                InfoCreater.UpdateInfomation(35);
            }



            //警惕、冒进、越行、引导确认
            if (!SignalDataIn300sOld.AlertAck && SignalDataIn300s.AlertAck)
            {
                InfoCreater.UpdateInfomation(37);
            }
            if (!SignalDataIn300sOld.MJAck && SignalDataIn300s.MJAck)
            {
                InfoCreater.UpdateInfomation(39);
            }
            if (!SignalDataIn300sOld.ExceAck && SignalDataIn300s.ExceAck)
            {
                InfoCreater.UpdateInfomation(40);
            }
            if (SignalDataIn300sOld.ExceAck && !SignalDataIn300s.ExceAck)
            {
                InfoCreater.UpdateInfomation(40, InformationUpdateType.Remove);
            }
            if (!SignalDataIn300sOld.ExceSelAck && SignalDataIn300s.ExceSelAck)
            {
                InfoCreater.UpdateInfomation(45);
            }
            if (SignalDataIn300sOld.ExceSelAck && !SignalDataIn300s.ExceSelAck)
            {
                InfoCreater.UpdateInfomation(45, InformationUpdateType.Remove);
            }
            if (!SignalDataIn300sOld.SRAck && SignalDataIn300s.SRAck)
            {
                InfoCreater.UpdateInfomation(41);
            }


            //等级转换确认
            if (!SignalDataIn300sOld.EnterC2Ack && SignalDataIn300s.EnterC2Ack)
            {
                InfoCreater.UpdateInfomation(42);
            }
            if (!SignalDataIn300sOld.EnterC3Ack && SignalDataIn300s.EnterC3Ack)
            {
                InfoCreater.UpdateInfomation(43);
            }

            //常用制动故障确认
            if (!SignalDataIn300sOld.FaultNormalBreakAck && SignalDataIn300s.FaultNormalBreakAck)
            {
                InfoCreater.UpdateInfomation(44);
            }
            //退行或溜逸防护确认
            if (!SignalDataIn300sOld.SlideProtectAck && SignalDataIn300s.SlideProtectAck)
            {
                InfoCreater.UpdateInfomation(46);
            }
            if (!SignalDataIn300sOld.BackProtectAck && SignalDataIn300s.BackProtectAck)
            {
                InfoCreater.UpdateInfomation(47);
            }
            //进入目视模式确认
            if (!SignalDataIn300sOld.EnterOSAck && SignalDataIn300s.EnterOSAck)
            {
                InfoCreater.UpdateInfomation(51);
            }

            //等级转换文本
            if (SignalDataIn300sOld.RunLevel != SignalDataIn300s.RunLevel && SignalDataIn300s.StartConditionFlag)
            {
                if (SignalDataIn300s.RunLevel == (int) CTCSType.CTCS2)
                {
                    InfoCreater.UpdateInfomation(10);
                }
                else if (SignalDataIn300s.RunLevel == (int) CTCSType.CTCS3)
                {
                    InfoCreater.UpdateInfomation(11);
                }
            }
            //等级转换预告文本
            if (SignalDataIn300sOld.YGLevelInfo != SignalDataIn300s.YGLevelInfo)
            {
                if (SignalDataIn300s.YGLevelInfo == (int)LevelChangeType.YG_CTCS3TOCTCS2)
                {
                    InfoCreater.UpdateInfomation(12);
                }
                if (SignalDataIn300s.YGLevelInfo == (int)LevelChangeType.YG_CTCS2TOCTCS3)
                {
                    InfoCreater.UpdateInfomation(13);
                }
            }

            //模式转换文本
            if (SignalDataIn300sOld.ControlMode != SignalDataIn300s.ControlMode)
            {
                switch ((ControlType) SignalDataIn300s.ControlMode)
                {
                    case ControlType.FullSupervision:
                        InfoCreater.UpdateInfomation(7);
                        break;
                    case ControlType.StandBy:
                        InfoCreater.UpdateInfomation(1);
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

            ////请求制动缓解 300S允许缓解屏幕F区不弹框，司机手按缓解键弹窗
            if (!SignalDataIn300sOld.CBrakeRelAck && SignalDataIn300s.CBrakeRelAck)
            {
                InfoCreater.UpdateInfomation(36);
            }
            if (!SignalDataIn300sOld.EBrakeRelAck && SignalDataIn300s.EBrakeRelAck)
            {
                InfoCreater.UpdateInfomation(36);
            }

            //故障文本
            if (!SignalDataIn300sOld.Fault[2] && SignalDataIn300s.Fault[2])
            {
                InfoCreater.UpdateInfomation(33);
                InfoCreater.UpdateInfomation(34);
            }
            if (!SignalDataIn300sOld.Fault[3] && SignalDataIn300s.Fault[3])
            {
                InfoCreater.UpdateInfomation(32);
            }
            if (!SignalDataIn300sOld.Fault[4] && SignalDataIn300s.Fault[4])
            {
                InfoCreater.UpdateInfomation(27);
            }
            if (!SignalDataIn300sOld.Fault[5] && SignalDataIn300s.Fault[5] && SignalDataIn300s.RunLevel == (int) CTCSType.CTCS3
                && SignalDataIn300s.ControlMode != (int) ControlType.OnSight)
            {
                InfoCreater.UpdateInfomation(28);
            }
            if (!SignalDataIn300sOld.Fault[6] && SignalDataIn300s.Fault[6])
            {
                InfoCreater.UpdateInfomation(30);
            }
            if (!SignalDataIn300sOld.Fault[7] && SignalDataIn300s.Fault[7] && SignalDataIn300s.RunLevel == (int) CTCSType.CTCS3)
            {
                InfoCreater.UpdateInfomation(47);
            }
            if (!SignalDataIn300sOld.Fault[8] && SignalDataIn300s.Fault[8])
            {
                InfoCreater.UpdateInfomation(31);
            }
            if (!SignalDataIn300sOld.Fault[10] && SignalDataIn300s.Fault[10])
            {
                InfoCreater.UpdateInfomation(55);
            }


            InfoCreater.FlushInfomation();

            return true;
        }

        public override bool ScreenControl()
        {
            //启动流程控制
            if (!SignalDataIn300s.StartFinishFlag && !SignalDataIn300s.StartConditionFlag)
            {
                //请求输入司机号
                if (SignalDataIn300sOld.BrakeTestStatus != SignalDataIn300s.BrakeTestStatus &&
                    !SignalDataIn300s.StartDriverNumConfirmed)
                {
                    if ((BrakeTestStatus) SignalDataIn300s.BrakeTestStatus == BrakeTestStatus.BrakeTestSucceed)
                    {
                        ATP.DriverInterfaceController.UpdateDriverInterface(
                            DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_数据_司机号));
                    }
                }
                //请求等级
                if (!SignalDataIn300sOld.StartDriverNumConfirmed && SignalDataIn300s.StartDriverNumConfirmed)
                {
                    ATP.DriverInterfaceController.UpdateDriverInterface(
                        DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_等级));
                }
                //if (SignalDataIn300sOld.RunLevel < (int)CTCSType.CTCS2 &&
                //    (SignalDataIn300s.RunLevel == (int)CTCSType.CTCS2 || SignalDataIn300s.RunLevel == (int)CTCSType.CTCS3))
                if (!SignalDataIn300sOld.StartCtcsSelected && SignalDataIn300s.StartCtcsSelected)
                {
                    ATP.DriverInterfaceController.UpdateDriverInterface(
                        DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_数据_车次号));
                }
                if (!SignalDataIn300sOld.StartTrainNumConfirmed && SignalDataIn300s.StartTrainNumConfirmed)
                {
                    ATP.DriverInterfaceController.UpdateDriverInterface(
                        DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_数据_列车数据));
                }
                if (!SignalDataIn300sOld.StartTrainDataSelected && SignalDataIn300s.StartTrainDataSelected)
                {
                    ATP.DriverInterfaceController.UpdateDriverInterface(
                        DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_载频));
                }
                if (!SignalDataIn300sOld.StartFrequencySelected && SignalDataIn300s.StartFrequencySelected)
                {
                    ATP.DriverInterfaceController.UpdateDriverInterface(
                        DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_确认取消载频));
                }
                if (!SignalDataIn300sOld.StartFrequencyConfirmed && SignalDataIn300s.StartFrequencyConfirmed)
                {
                    ATP.DriverInterfaceController.UpdateDriverInterface(
                        DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_确认取消列车数据));
                }

            }
            //RBC输入流程
            if (SignalDataOut300s.C3SelFlag)
            {
                ATP.DriverInterfaceController.UpdateDriverInterface(
                    DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_数据_RBC数据));
                SignalDataOut300s.C3SelFlag = false;
            }
            return true;
        }

        public override bool  ClearInfos()
        {
            InfomationCreater InfoCreater =
                (InfomationCreater)ATP.InfomationService.GetInformationCreater();
            InfoCreater.ClearShowingItems();
            //int TextInfoNum = 100;
            ////清除屏幕消息,然而并没有软用
            //for (int i = 0; i < TextInfoNum; i++)
            //{
            //    InfoCreater.UpdateInfomation(i, false);
            //}
            //InfoCreater.FlushInfomation();

            //清除输入信息
            SignalDataIn300s.ClearInfo();
            SignalDataOut300s.ClearInfo();
            return true;
        }
    }
}

