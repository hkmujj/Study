using System;
using CommonUtil.Controls;
using MMI.Facility.Interface.Data;
using Motor.ATP.DataAdapter.Extension;
using Motor.ATP.DataAdapter.Resource.Keys;
using Motor.ATP.DataAdapter.Util;
using Motor.ATP.Infrasturcture.Control.Infomation;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.BrakeTest;
using Motor.ATP.Infrasturcture.Interface.Extension;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Extension;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP.DataAdapter.ConcreateAdapter.ATP300H
{   
   
    public class ATP300HDataAdapter : DataAdapterBase
    {

        protected SignalDataIn300H SignalDataIn300h { get; set; }
        protected SignalDataIn300H SignalDataIn300hOld { get; set; }
        protected SignalDataOut300H SignalDataOut300h { get; set; }

        //private bool StartCtcsSelected = false;

        public ATP300HDataAdapter(ATPDomain adaptTarget)
            : base(adaptTarget, new SendInterface300H(new SignalDataOut300H()))
        {
            SignalDataIn300h = new SignalDataIn300H();
            SignalDataIn300hOld = new SignalDataIn300H();
            SignalDataIn300hOld = (SignalDataIn300H) SignalDataIn300h.Clone();
            SignalDataIn = SignalDataIn300h;
            SignalDataInOld = SignalDataIn300hOld;

            SignalDataOut300h = (SignalDataOut300H)SignalDataOut;
            SignalDataOut300h.SignalDataIn = SignalDataIn300h;
            SignalDataOut300h.ATP = ATP;
            SignalDataOut = SignalDataOut300h;
        }

        public override void OnBoolChangedImp(object sender, CommunicationDataChangedWrapperArgs<bool> dataChangedArgs)
        {
            base.OnBoolChangedImp(sender,dataChangedArgs);
            ////黑屏
            //dataChangedArgs.UpdateIfContains(InBoolKeys.ATP电源, b => SignalDataIn300s.ScreenBlackFlag = b);

            ////常用制动故障确认
            //dataChangedArgs.UpdateIfContains(InBoolKeys.请求_常用制动故障确认, b => SignalDataIn300s.FaultNormalBreakAck = b);

            ////已选择越行，确认越行
            //dataChangedArgs.UpdateIfContains(InBoolKeys.请求_已选择越行确认越行, b => SignalDataIn300s.ExceSelAck = b);

            ////退行防护确认
            //dataChangedArgs.UpdateIfContains(InBoolKeys.请求_退行防护制动确认, b => SignalDataIn300s.BackProtectAck = b);

            ////车载设备自动换系
            //dataChangedArgs.UpdateIfContains(InBoolKeys.车载设备自动换系, b => SignalDataIn300s.ChangeHostAuto = b);

            ////进入目视模式确认
            //dataChangedArgs.UpdateIfContains(InBoolKeys.请求_进入目视模式, b => SignalDataIn300s.EnterOSAck = b);

            ////启动完成确认
            //dataChangedArgs.UpdateIfContains(InBoolKeys.请求_启动完成确认, b => SignalDataIn300s.StartFinishAck = b);

            ////紧急制动缓解确认
            //dataChangedArgs.UpdateIfContains(InBoolKeys.请求_确认紧急制动缓解, b => SignalDataIn300s.EBrakeRelAck = b);

            ////常用制动缓解确认
            //dataChangedArgs.UpdateIfContains(InBoolKeys.请求_确认常用制动缓解, b => SignalDataIn300s.CBrakeRelAck = b);

            ////应答器丢失确认
            //dataChangedArgs.UpdateIfContains(InBoolKeys.请求_应答器信息丢失确认, b => SignalDataIn300s.EnterBaliseLostAck = b);

            //系统自检状态
            //dataChangedArgs.UpdateIfContains(InBoolKeys.车载设备自检状态, b => SignalDataIn300s.SelfCheckStatus = b);

            //启动流程相关
            dataChangedArgs.UpdateIfContains(InBoolKeys.启动_司机号已确认, b => SignalDataIn300h.StartDriverNumConfirmed = b);
            dataChangedArgs.UpdateIfContains(InBoolKeys.启动_等级已选择, b => SignalDataIn300h.StartCtcsSelected = b);
            //dataChangedArgs.UpdateIfContains(InBoolKeys.启动_车次号已确认, b => SignalDataIn300s.StartTrainNumConfirmed = b);
            //dataChangedArgs.UpdateIfContains(InBoolKeys.启动_列车数据已选择, b => SignalDataIn300s.StartTrainDataSelected = b);
            //dataChangedArgs.UpdateIfContains(InBoolKeys.启动_载频已选择, b => SignalDataIn300s.StartFrequencySelected = b);
            //dataChangedArgs.UpdateIfContains(InBoolKeys.启动_载频已确认, b => SignalDataIn300s.StartFrequencyConfirmed = b);
            //dataChangedArgs.UpdateIfContains(InBoolKeys.启动_列车数据已确认, b => SignalDataIn300s.StartTrainDataConfirmed = b);

            //输入车次号请求
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_输入车次号, b => SignalDataIn300h.StartTrainNumAck = b);
            //输入列车长度请求
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_输入列车长度, b => SignalDataIn300h.StartTrainLengthAck = b);
            dataChangedArgs.UpdateIfContains(InBoolKeys.启动_司机号已确认, b => SignalDataIn300h.StartDriverNumConfirmed = b);
            //启动流程相关
            dataChangedArgs.UpdateIfContains(InBoolKeys.启动_等级已选择, b => SignalDataIn300h.StartCtcsSelected = b);
            //输入RBC连接失败
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_RBC呼叫失败, b => SignalDataIn300h.RBCConnectFailAck = b);
            //输入制动测试失败
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_制动测试失败, b => SignalDataIn300h.BrakeTestFailAck = b);

            FeedBackInfo(sender, dataChangedArgs);
        }

        public override void OnFloatChangedImp(object sender, CommunicationDataChangedWrapperArgs<float> dataChangedArgs)
        {
            base.OnFloatChangedImp(sender, dataChangedArgs);
           
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
            if ((SignalDataIn300hOld.ATPPowerFlag && !SignalDataIn300h.ATPPowerFlag))//|| (!SignalDataIn300hOld.ATPBypassFlag && SignalDataIn300h.ATPBypassFlag)
            {
                ClearInfos();
            }


            //4、保存上周期状态 
            //if (SignalDataIn300hOld.ControlMode != SignalDataIn.ControlMode)
            //{
            //    Trace.WriteLine("old data：" + SignalDataIn300hOld.ControlMode.ToString());
            //    Trace.WriteLine("new data：" + SignalDataIn.ControlMode.ToString());
            //}
            SignalDataIn300hOld = (SignalDataIn300H) SignalDataIn300h.Clone();

            //GC.Collect();

        }

        public override bool FeedBackInfo(object sender, CommunicationDataChangedWrapperArgs<bool> dataChangedArgs)
        {
            base.FeedBackInfo(sender, dataChangedArgs);
            //反馈_确认输入车次号
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_输入车次号, b =>
            {
                if (b)
                {
                    SignalDataOut300h.StartTrainNumSign = false;
                }
            });
            //反馈_确认输入列车长度
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_输入列车长度, b =>
            {
                if (b)
                {
                    SignalDataOut300h.StartTrainLengthSign = false;
                }
            });
            //反馈_确认RBC连接失败
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_RBC呼叫失败, b =>
            {
                if (b)
                {
                    SignalDataOut300h.RBCConnectFailSign = false;
                }
            });
            //反馈_确认制动测试失败
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_制动测试失败, b =>
            {
                if (b)
                {
                    SignalDataOut300h.BrakeTestFailSign = false;
                }
            });
            ////反馈_启动确认_没有消息提示与确认
            //dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_启动确认_没有消息提示与确认, b =>
            //{
            //    if (b)
            //        SignalDataOut300s.StartSign = false;
            //});


            ////反馈_确认常用制动故障
            //dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_确认常用制动故障, b =>
            //{
            //    if (b)
            //        SignalDataOut300s.FaultNormalBreakSign = false;
            //});

            ////反馈_确认溜逸防护制动
            //dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_确认溜逸防护制动, b =>
            //{
            //    if (b)
            //        SignalDataOut300s.SlideConfirmSign = false;
            //});

            ////反馈_确认紧急制动缓解
            //dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_确认紧急制动缓解, b =>
            //{
            //    if (b)
            //        SignalDataOut300s.EBRelSign = false;
            //});

            ////反馈_确认常用制动缓解
            //dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_确认常用制动缓解, b =>
            //{
            //    if (b)
            //        SignalDataOut300s.CBRelSign = false;
            //});


            ////反馈_应答器丢失确认
            //dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_应答器丢失确认, b =>
            //{
            //    if (b)
            //        SignalDataOut300s.BaliseLostSign = false;
            //});

            ////反馈_确认退行防护制动
            //dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_确认退行防护制动, b =>
            //{
            //    if (b)
            //        SignalDataOut300s.BackProtectSign = false;
            //});

            ////反馈_确认选择越行
            //dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_确认选择越行, b =>
            //{
            //    if (b)
            //        SignalDataOut300s.ExceSelSign = false;
            //});



            ////反馈_启动列车数据确认
            //dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_启动列车数据已确认, b =>
            //{
            //    if (b)
            //        SignalDataOut300s.StartTrainDataSign = false;
            //});

            ////反馈_启动载频确认
            //dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_启动载频已确认, b =>
            //{
            //    if (b)
            //        SignalDataOut300s.StartFreqSign = false;
            //});
            ////反馈_取消确认越性选择
            //dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_取消确认选择越行, b =>
            //{
            //    if (b)
            //        SignalDataOut300s.CancleExceSelSign = false;
            //});
            return true;
        }

        public override bool ScreenToSignalInfo()
        {
            base.ScreenToSignalInfo();
            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认输入车次号],
               SignalDataOut300h.StartTrainNumSign);
            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认输入列车长度],
                SignalDataOut300h.StartTrainLengthSign);
            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认RBC呼叫失败],
               SignalDataOut300h.RBCConnectFailSign);
            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认制动测试失败],
               SignalDataOut300h.BrakeTestFailSign);

            //DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.启动确认_没有消息提示与确认],
            //  SignalDataOut300s.StartSign);

            //DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认常用制动故障],
            //    SignalDataOut300s.FaultNormalBreakSign);

            //DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认溜逸防护制动],
            //    SignalDataOut300s.SlideConfirmSign);

            //DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认紧急制动缓解],
            //    SignalDataOut300s.EBRelSign);

            //DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认常用制动缓解],
            //    SignalDataOut300s.CBRelSign);

            //DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.屏幕启动过程],
            //    SignalDataOut300s.StartProcessFlag);

            //DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.应答器丢失确认],
            //    SignalDataOut300s.BaliseLostSign);

            //DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认退行防护制动],
            //    SignalDataOut300s.BackProtectSign);

            //DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认选择越行],
            //    SignalDataOut300s.ExceSelSign);

            //DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.启动_确认列车数据],
            //    SignalDataOut300s.StartTrainDataSign);

            //DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.启动_确认载频],
            //    SignalDataOut300s.StartFreqSign);

            //DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.取消确认选择越行],
            //    SignalDataOut300s.CancleExceSelSign);

            return true;
        }
        public override bool ScreenPlanInfo()
        {
            base.ScreenPlanInfo();
            if ((SignalDataIn.ControlMode != (int)ControlType.FullSupervision) &&
                (SignalDataIn.ControlMode != (int)ControlType.CallingOn) &&
                (SignalDataIn.ControlMode != (int)ControlType.PartialSupervision) &&
                (SignalDataIn.ControlMode != (int)ControlType.OnSight) &&
                (SignalDataIn.ControlMode != (int)ControlType.Shunting) &&
                (SignalDataIn.ControlMode != (int)ControlType.LKJ) &&
                (SignalDataIn.ControlMode != (int)ControlType.Trip) &&
                (SignalDataIn.ControlMode != (int)ControlType.PostTrip))
            {
                ATP.SpeedMonitoringSection.Visible = false;
               
            }
            else
            {
                ATP.SpeedMonitoringSection.Visible = true;
            }
            return true;
        }
        public override bool BasicAndTrainInfo()
        {

            ATP.Other.NowInATP = SignalDataIn300h.NowTime;

            //速度及限速信息
            ATP.TrainInfo.Speed.Visible = !SignalDataIn300h.ChangeHostAuto;

            ATP.TrainInfo.Speed.CurrentSpeed.Value = Math.Abs(SignalDataIn300h.TrainSpeed);

            ATP.TrainInfo.Speed.ServiceBrakeInterventionSpeed.Value = SignalDataIn300h.SBISpeed;
            ATP.TrainInfo.Speed.EmergencyBrakeInterventionSpeed.Value = SignalDataIn300h.EBISpeed;
            ATP.TrainInfo.Speed.PermittedLimitSpeed.Value = SignalDataIn300h.TrainLimitSpeed;
            ATP.TrainInfo.Speed.IntervertionSpeed.Value = SignalDataIn300h.IntervertionSpeed;
            ATP.TrainInfo.Speed.WarningLimitSpeed.Value = SignalDataIn300h.WarningSpeed;
            ATP.TrainInfo.Speed.TargetSpeed.Value = SignalDataIn300h.ControlMode != (int) ControlType.FullSupervision &&
                                                    SignalDataIn300h.ControlMode != (int) ControlType.CallingOn
                ? SignalDataIn300h.TrainLimitSpeed - 5
                : (SignalDataIn300h.TrainGoalSpeed > SignalDataIn300h.TrainLimitSpeed
                    ? SignalDataIn300h.TrainLimitSpeed
                    : SignalDataIn300h.TrainGoalSpeed);
            //制动信息
            ATP.TrainInfo.Brake.Visible = SignalDataIn300h.BrakeStatus > 1 && SignalDataIn300h.BrakeStatus < 7 &&
                                          SignalDataIn300h.BrakeTestStatus != (int) BrakeTestStatus.BrakeTesting;
            ATP.TrainInfo.Brake.BrakeType = (BrakeType) SignalDataIn300h.BrakeStatus;
            //公里标
            ATP.TrainInfo.KilometerPost.Visible = SignalDataIn300h.IsGLBVisible;
            if (ATP.TrainInfo.KilometerPost.Visible)
            {
                ATP.TrainInfo.KilometerPost.Kilometer = Math.Truncate(SignalDataIn300h.TrainGLB / 1000);
                ATP.TrainInfo.KilometerPost.Meter = SignalDataIn300h.TrainGLB % 1000;
            }
            //GSM && RBC
            ATP.TrainInfo.ConnectState.GSMRState = SignalDataIn300h.nSelfCheckStatus != (int)SelfCheckStatus.CheckStatus_Success
                ? GSMRState.Invalidate
                : (GSMRState) SignalDataIn300h.GSMStatus;

            ATP.TrainInfo.ConnectState.RBCConnectState = SignalDataIn300h.RBCStatus <0
                ? RBCConnectState.Invalidate
                : (RBCConnectState) SignalDataIn300h.RBCStatus;

            ATP.TrainInfo.ConnectState.Visible = SignalDataIn300h.nSelfCheckStatus == (int)SelfCheckStatus.CheckStatus_Success;
          
           
            //车站
            ATP.TrainInfo.Station.Visible = true;
            ATP.TrainInfo.Station.CurrentStation = SignalDataIn300h.StationNo > 0
                ? GetStationName((ulong) SignalDataIn300h.StationNo)
                : "";
            //等级
            ATP.CTCS.CurrentCTCSType = (CTCSType) SignalDataIn300h.RunLevel;
            ATP.CTCS.Visible = ATP.CTCS.CurrentCTCSType == CTCSType.CTCS2 || ATP.CTCS.CurrentCTCSType == CTCSType.CTCS3;
            //模式
            ATP.ControlModel.InEffect = true;
            ATP.ControlModel.CurrentControlType = (ControlType) SignalDataIn300h.ControlMode;
            //TODO 增加其它模式
            ATP.SpeedMonitoringSection.ZoomVisible = ATP.ControlModel.CurrentControlType == ControlType.FullSupervision;
            ATP.RegionFStateProvier.ShuntingStateProvider.EnterOrQuitState = (ControlType) SignalDataIn300h.ControlMode ==
                                                                             ControlType.Shunting
                ? EnterOrQuit.Quit
                : EnterOrQuit.Enter;
            ATP.RegionFStateProvier.CabsignalStateProvider.EnterOrQuitState = (ControlType) SignalDataIn300h.ControlMode ==
                                                                              ControlType.LKJ
                ? EnterOrQuit.Quit
                : EnterOrQuit.Enter;

            //机车信号
            ATP.CabSignal.Code = (CabSignalCode) SignalDataIn300h.SignalCode;
            ATP.CabSignal.Visible = ATP.CabSignal.Code > 0;
            //机控人控
            ATP.TrainControlType = (TrainControlType) SignalDataIn300h.BrakeMode;
            ATP.TrainControlTypeVisible = ATP.TrainControlType != TrainControlType.Unkown;
            //电源
            ATP.ATPPower.ATPPowerState = SignalDataIn300h.ATPPowerFlag ? ATPPowerState.Started : ATPPowerState.Stopped;


            //司机号、车次号
            ATP.TrainInfo.Driver.TrainIdVisible = true;
            ATP.TrainInfo.Driver.TrainId = SignalDataOut300h.TrainCode;
            ATP.TrainInfo.Driver.DriverId = SignalDataOut300h.DriverCode;

            //时间日期
            ATP.Other.TimeVisible = true ;
            ATP.Other.DateVisible = true;
            ATP.Other.DateTimeTitleVisible = true;
            //文本消息
            ATP.Message.Visible = true;
            //隔离模式不显示
            if (SignalDataIn300h.ControlMode == (int)ControlType.Isolated)
            {
                
                ATP.Other.TimeVisible = false;
                ATP.Other.DateVisible = false;
                ATP.Other.DateTimeTitleVisible = false;

                ATP.SpeedMonitoringSection.ZoomVisible = false;

                ATP.CTCS.Visible = false;

                ATP.TrainInfo.Driver.TrainIdVisible = false;
                ATP.TrainInfo.Station.Visible = false;
                ATP.TrainInfo.ConnectState.Visible = false;
                ATP.TrainInfo.KilometerPost.Visible = false;
                ATP.TrainInfo.Brake.Visible = false;
                ATP.TrainInfo.Speed.Visible = false;
                ATP.Message.Visible = false;
                ATP.CabSignal.Visible = false;

                ATP.UpdateDriverInterface(DriverInterfaceKeys.Root_隔离状态);
            }
            //列车车长
            ATP.TrainInfo.CurrentTrainGroupCount = 2;
            //ATP.TrainInfo.TrainLegth.Clear();//车长不一致时赋值
            return true;

        }

        public override bool ScreenBtnEnable()
        {
            //按钮触发状态

            for (int i = 0; i < SignalDataIn300h.ScreenBotton.Length; i++)
            {
                HardwareButton HBInfo = ATP.ATPHardwareButton.HardwareButtonCollection[i];
                HBInfo.Type = (UserActionType) (i + 1);
                HBInfo.MouseState = SignalDataIn300h.ScreenBotton[i] ? MouseState.MouseDown : MouseState.MouseUp;
            }
            //按钮使能状态
            //0-数据，1-模式，2-载频，3-等级，4-其他，5-启动，6-缓解,7-警惕,8-司机号,9-车次号,10-列车数据,11-RBC数据，12-调车，13-目视，14-机信，15-制动测试执行，16-制动测试略过
            //17-RBCID，18-RBC电话号码，19-RBC网络号码
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
            //自检时所有
            if (SignalDataIn300h.nSelfCheckStatus != (int)SelfCheckStatus.CheckStatus_Success  || SignalDataIn300h.ControlMode == (int)ControlType.Isolated)
            {
                for (int i = 0; i < 8; i++)
                {
                    BtnEnable[i] = false;
                }
            }
            else
            {
                //等级按键：只有在待机下选择等级后和启动前显示
                //在列车停车的状态下，且车载设备处于 CTCS-3 等级的 FS、OS、SB、CO 模式,司机可通过手动选择 CTCS-2 等级来执行 CTCS-3 到 CTCS-2 级的切换。 
                BtnEnable[3] = false;
                if (SignalDataIn300h.StartCtcsSelected && !SignalDataIn300h.StartConditionFlag)
                {
                    BtnEnable[3] = true;
                }
                else if (SignalDataIn300h.RunLevel == (int)CTCSType.CTCS3 && SignalDataIn300h.TrainSpeed<0.5)
                {
                    if (SignalDataIn300h.ControlMode == (int)ControlType.FullSupervision || SignalDataIn300h.ControlMode == (int)ControlType.OnSight || SignalDataIn300h.ControlMode == (int)ControlType.CallingOn)
                    {
                        BtnEnable[3] = true;
                    }
                }
                //制动测试略过和测试按钮
                if (SignalDataIn300h.ControlMode != (int)ControlType.StandBy)
                {
                    BtnEnable[15] = false;
                    BtnEnable[16] = false;
                }else if (SignalDataIn300h.BrakeTestStatus != (int)BrakeTestStatus.BrakeTestSucceed)
                {
                    BtnEnable[16] = false;
                }

                var needRBC = SignalDataIn300h.RBCStatus != (int) RBCConnectState.Connected &&
                              SignalDataIn300h.ControlMode == (int) ControlType.StandBy &&
                              SignalDataIn300h.RunLevel == (int) CTCSType.CTCS3;
                //RBC数据3个按钮
                BtnEnable[17] = needRBC;
                BtnEnable[18] = needRBC;
                BtnEnable[19] = false;
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
                    case 15:
                        ATP.RegionFStateProvier.RunBrakeTestStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 16:
                        ATP.RegionFStateProvier.SkipRunBrakeTestStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 17:
                        ATP.RegionFStateProvier.RBCIDStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 18:
                        ATP.RegionFStateProvier.TelNumberStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 19:
                        ATP.RegionFStateProvier.NetNumberStateProvider.Enabled = BtnEnable[i];
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
            ////司机室激活
            //if (SignalDataIn300hOld.DriverActFlag != SignalDataIn300h.DriverActFlag)
            //{
            //    if (SignalDataIn300h.DriverActFlag)
            //    {
            //        InfoCreater.UpdateInfomation(14, true);
            //    }
            //}

            //系统自检
            //if (SignalDataIn300hOld.ScreenStartFinishFlag!=SignalDataIn300h.ScreenStartFinishFlag)
            //{
            //    if (SignalDataIn300h.ScreenStartFinishFlag)
            //    {
            //        InfoCreater.UpdateInfomation(15, true);
            //    }
            //}
            if (SignalDataIn300hOld.ATPPowerFlag != SignalDataIn300h.ATPPowerFlag)
            {
                if (SignalDataIn300h.ATPPowerFlag)
                {
                    InfoCreater.UpdateInfomation(15);
                }
            }
            //自检成功
            if (SignalDataIn300hOld.nSelfCheckStatus != (int)SelfCheckStatus.CheckStatus_Success && SignalDataIn300h.nSelfCheckStatus == (int)SelfCheckStatus.CheckStatus_Success)
            {
                InfoCreater.UpdateInfomation(52);
            }
            //制动测试:制动测试无效时才显示
            if (SignalDataIn300hOld.nSelfCheckStatus != (int)SelfCheckStatus.CheckStatus_Success && SignalDataIn300h.nSelfCheckStatus == (int)SelfCheckStatus.CheckStatus_Success
                && (BrakeTestStatus)SignalDataIn300h.BrakeTestStatus != BrakeTestStatus.BrakeTestSucceed)
            {
                InfoCreater.UpdateInfomation(54);
            }
            if (SignalDataIn300hOld.BrakeTestStatus != SignalDataIn300h.BrakeTestStatus)
            {
                switch ((BrakeTestStatus) SignalDataIn300h.BrakeTestStatus)
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
                }
            }
            //请求输入车次号
            if (!SignalDataIn300hOld.StartTrainNumAck && SignalDataIn300h.StartTrainNumAck)
            {
                InfoCreater.UpdateInfomation(55);
            }
            //请求输入列车车长
            if (!SignalDataIn300hOld.StartTrainLengthAck && SignalDataIn300h.StartTrainLengthAck)
            {
                InfoCreater.UpdateInfomation(56);
            }
            //请选择任务开始
             if (!SignalDataIn300hOld.StartConditionFlag  && SignalDataIn300h.StartConditionFlag)
             {
                 InfoCreater.UpdateInfomation(57);
             }
    //启动过程文本—start—————————————————————————————————————————————
            
            //禁止调车
            if (SignalDataIn300hOld.SHForbidFlag != SignalDataIn300h.SHForbidFlag)
            {
                if (SignalDataIn300h.SHForbidFlag && SignalDataIn300h.ControlMode == (int) ControlType.Shunting)
                {
                    InfoCreater.UpdateInfomation(20);
                }
            }
            //RBC允许调车
            if (SignalDataIn300hOld.RBCAllowSH != SignalDataIn300h.RBCAllowSH)
            {
                switch (SignalDataIn300h.RBCAllowSH)
                {
                    case 1:
                        InfoCreater.UpdateInfomation(22);
                        break;
                    case 2:
                        InfoCreater.UpdateInfomation(21);
                        break;
                }
            }
            //RBC连接
            if (SignalDataIn300hOld.RBCStatus != SignalDataIn300h.RBCStatus)
            {
                if ((RBCConnectState)SignalDataIn300h.RBCStatus == RBCConnectState.Connecting)
                {
                    InfoCreater.UpdateInfomation(60);
                }
            }
            //确认RBC请求失败
            if (!SignalDataIn300hOld.RBCConnectFailAck && SignalDataIn300h.RBCConnectFailAck)
            {
                InfoCreater.UpdateInfomation(58);
            }
            //警惕、冒进、越行、引导确认
            if (!SignalDataIn300hOld.AlertAck && SignalDataIn300h.AlertAck)
            {
                InfoCreater.UpdateInfomation(37);
            }
            if (!SignalDataIn300hOld.MJAck && SignalDataIn300h.MJAck)
            {
                InfoCreater.UpdateInfomation(39);
            }
            //if (!SignalDataIn300hOld.ExceAck && SignalDataIn300h.ExceAck)
            //{
            //    InfoCreater.UpdateInfomation(40, true);
            //}
            //if (SignalDataIn300hOld.ExceAck && !SignalDataIn300h.ExceAck)
            //{
            //    InfoCreater.UpdateInfomation(40, false);
            //}
            //if (!SignalDataIn300hOld.ExceSelAck && SignalDataIn300h.ExceSelAck)
            //{
            //    InfoCreater.UpdateInfomation(45, true);
            //}
            //if (SignalDataIn300hOld.ExceSelAck && !SignalDataIn300h.ExceSelAck)
            //{
            //    InfoCreater.UpdateInfomation(45, false);
            //}
            if (!SignalDataIn300hOld.SRAck && SignalDataIn300h.SRAck)
            {
                InfoCreater.UpdateInfomation(41);
            }


            //等级转换确认
            if (!SignalDataIn300hOld.EnterC2Ack && SignalDataIn300h.EnterC2Ack)
            {
                InfoCreater.UpdateInfomation(42);
            }
            if (!SignalDataIn300hOld.EnterC3Ack && SignalDataIn300h.EnterC3Ack)
            {
                InfoCreater.UpdateInfomation(43);
            }

            //常用制动故障确认
            if (!SignalDataIn300hOld.FaultNormalBreakAck && SignalDataIn300h.FaultNormalBreakAck)
            {
                InfoCreater.UpdateInfomation(44);
            }
            //退行或溜逸防护确认
            if (!SignalDataIn300hOld.SlideProtectAck && SignalDataIn300h.SlideProtectAck)
            {
                InfoCreater.UpdateInfomation(46);
            }
            if (!SignalDataIn300hOld.BackProtectAck && SignalDataIn300h.BackProtectAck)
            {
                InfoCreater.UpdateInfomation(47);
            }
            //进入目视模式确认
            if (!SignalDataIn300hOld.EnterOSAck && SignalDataIn300h.EnterOSAck)
            {
                InfoCreater.UpdateInfomation(51);
            }

            //等级转换文本
            if (SignalDataIn300hOld.RunLevel != SignalDataIn300h.RunLevel && SignalDataIn300h.StartConditionFlag)
            {
                if (SignalDataIn300h.RunLevel == (int) CTCSType.CTCS2)
                {
                    InfoCreater.UpdateInfomation(10);
                }
                else if (SignalDataIn300h.RunLevel == (int) CTCSType.CTCS3)
                {
                    InfoCreater.UpdateInfomation(11);
                }
            }
            //等级转换预告文本
            if (SignalDataIn300hOld.YGLevelInfo != SignalDataIn300h.YGLevelInfo)
            {
                if (SignalDataIn300h.YGLevelInfo == (int)LevelChangeType.YG_CTCS3TOCTCS2)
                {
                    InfoCreater.UpdateInfomation(12);
                }
                if (SignalDataIn300h.YGLevelInfo == (int)LevelChangeType.YG_CTCS2TOCTCS3)
                {
                    InfoCreater.UpdateInfomation(13);
                }
            }

            //模式转换文本
            if (SignalDataIn300hOld.ControlMode != SignalDataIn300h.ControlMode)
            {
                switch ((ControlType) SignalDataIn300h.ControlMode)
                {
                    case ControlType.FullSupervision:
                        InfoCreater.UpdateInfomation(7);
                        break;
                    case ControlType.StandBy:
                        if (SignalDataIn300hOld.ControlMode != (int)ControlType.Unknown)
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

            ////请求制动缓解 300S允许缓解屏幕F区不弹框，司机手按缓解键弹窗
            if (!SignalDataIn300hOld.CBrakeRelAck && SignalDataIn300h.CBrakeRelAck)
            {
                InfoCreater.UpdateInfomation(36);
            }
            if (!SignalDataIn300hOld.EBrakeRelAck && SignalDataIn300h.EBrakeRelAck)
            {
                InfoCreater.UpdateInfomation(36);
            }

            //故障文本
            if (!SignalDataIn300hOld.Fault[2] && SignalDataIn300h.Fault[2])
            {
                InfoCreater.UpdateInfomation(33);
                InfoCreater.UpdateInfomation(34);
            }
            if (!SignalDataIn300hOld.Fault[3] && SignalDataIn300h.Fault[3])
            {
                InfoCreater.UpdateInfomation(32);
            }
            if (!SignalDataIn300hOld.Fault[4] && SignalDataIn300h.Fault[4])
            {
                InfoCreater.UpdateInfomation(27);
            }
            if (!SignalDataIn300hOld.Fault[5] && SignalDataIn300h.Fault[5] && SignalDataIn300h.RunLevel == (int) CTCSType.CTCS3
                && SignalDataIn300h.ControlMode != (int) ControlType.OnSight)
            {
                InfoCreater.UpdateInfomation(28);
            }
            if (!SignalDataIn300hOld.Fault[6] && SignalDataIn300h.Fault[6])
            {
                InfoCreater.UpdateInfomation(30);
            }
            if (!SignalDataIn300hOld.Fault[7] && SignalDataIn300h.Fault[7] && SignalDataIn300h.RunLevel == (int) CTCSType.CTCS3)
            {
                InfoCreater.UpdateInfomation(47);
            }
            if (!SignalDataIn300hOld.Fault[8] && SignalDataIn300h.Fault[8])
            {
                InfoCreater.UpdateInfomation(31);
            }
            


            InfoCreater.FlushInfomation();

            return true;
        }

        public override bool ClearInfos()
        {
            InfomationCreater InfoCreater =
                (InfomationCreater) ATP.InfomationService.GetInformationCreater();
            InfoCreater.ClearShowingItems();

            ATP.Other.ShowingTimeDifference = TimeSpan.Zero;
            ATP.GetInterfaceController().UpdateDriverInterface(DriverInterfaceKey.Root);

            //清除输入信息
            SignalDataIn300h.ClearInfo();
            SignalDataOut300h.ClearInfo();
            return true;
        }

        public override bool ScreenControl()
        {
            //启动流程控制：整个过程只执行一次
            if (!SignalDataIn300h.StartCtcsSelected)
            {
                //自检成功请求制动测试
                if (SignalDataIn300hOld.nSelfCheckStatus != SignalDataIn300h.nSelfCheckStatus && SignalDataIn300h.nSelfCheckStatus == (int)SelfCheckStatus.CheckStatus_Success)
                {
                    ATP.DriverInterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_制动测试选择));
                   
                }
            
                //请求输入司机号
                if (SignalDataIn300hOld.BrakeTestStatus != SignalDataIn300h.BrakeTestStatus && (BrakeTestStatus)SignalDataIn300h.BrakeTestStatus == BrakeTestStatus.BrakeTestSucceed)
                {
                    ATP.DriverInterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_数据_司机号查看));
                }
                //请求等级
                if (!SignalDataIn300hOld.StartDriverNumConfirmed && SignalDataIn300h.StartDriverNumConfirmed)
                {
                    ATP.DriverInterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_等级));
                }
               

            }
            //进入待机后重新启动启动流程
            if (!SignalDataIn300h.StartConditionFlag && SignalDataIn300hOld.StartConditionFlag)
            {
                SignalDataOut300h.TrainNum = -1;
                SignalDataOut300h.TrainLength = -1;
            }
            ////RBC输入流程
            //if (SignalDataOut300h.C3SelFlag)
            //{
            //    ATP.DriverInterfaceController.UpdateDriverInterface(
            //        DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_数据_RBC数据));
            //    SignalDataOut300h.C3SelFlag = false;
            //}
            return true;
        }

    }
}

