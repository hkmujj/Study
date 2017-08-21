using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using CBTC.DataAdapter.ConcreateAdapter;
using CBTC.DataAdapter.Model;
using CBTC.Infrasturcture.Events;
using CBTC.Infrasturcture.Model.Road;
using CBTC.Infrasturcture.Service;
using CommonUtil.Model;
using Excel.Interface;
using Mmi.Common.DateTimeInterpreter;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Service;
using CBTC.DataAdapter.Resource.Keys;
using CBTC.Infrasturcture.Extension;
using CBTC.Infrasturcture.Model.Constant;
/**********适配原则 liyuezong 20160204
 
 * 基本原则：
   1、底层信号数据更新，适配层立刻适配更新，无计算周期。
   2、界面刷新与数据更新同步（WPF），谨慎处理数据更新，切不可先清除数据再赋值。
   3、文本控制切记保证只发送一次。
 
 * 详细原则
   1、信号->适配层->屏   信号底层提供基础条件（包含需要确认的条件等），适配层接收平台信息后更新ATP领域模型内部状态
   2、屏->适配层->信号   屏上进行操作以后调用适配层相应的方法，改变条件。然后适配层根据条件变化，更新平台数据
   3、E区文本            E区文本提示信息生成均在适配层生成并控制
   4、F区文本            F区文本自动弹出的文本统一由适配层适配，司机主动操作的流程由屏端自己控制
   5、按钮使能           按钮使能在适配层判断，基于现有条件
   6、反馈机制           所有信号生成的确认请求，信号底层必须进行反馈，形成数据传输闭环控制，解决丢失数据问题

*********/


namespace CBTC.DataAdapter
{
    public abstract class DataAdapterBase : IDataListener
    {
        //private readonly IDateTimeInterpreter m_DateTimeInterpreter;
        private readonly IDateTimeInterpreter m_DateTimeInterpreterFormSec;

        private ICourseService m_CourseService;

        private IInterfaceAdapterService m_InterfaceAdapterService;

        protected ICommunicationDataService DataService { get; set; }
        protected CBTC.Infrasturcture.Model.CBTC CBTC { get; private set; }
        protected SignalDataIn SignalDataIn { get; set; }
        protected SignalDataIn SignalDataInOld { get; set; }
        protected SignalDataOut SignalDataOut { get; set; }

        protected readonly IDateTimeInterpreter m_DateTimeInterpreter;

        private IStationNameProviderService m_StationNameProviderService;

        public CommonUtil.Model.IReadOnlyDictionary<ulong, StationConfig> StationConfigDictionary { get; private set; }

        public bool InCalculateFlag { protected set; get; }

        private readonly Action<DataAdapterBase, object, CommunicationDataChangedWrapperArgs<bool>> m_UpdateBoolAction =
            (obj, sender, args) => obj.OnBoolChangedImp(sender, args);

        private readonly Action<DataAdapterBase, object, CommunicationDataChangedWrapperArgs<float>> m_UpdateFloatAction
            =
            (obj, sender, args) => obj.OnFloatChangedImp(sender, args);

        private readonly Action<DataAdapterBase, object, CommunicationDataChangedWrapperArgs<bool>, CommunicationDataChangedWrapperArgs<float>> m_UpdateDataAction =
            (obj, sender, args1, args2) => obj.OnDataChangedImp(sender, args1, args2);

        protected ICourseService CourseService
        {
            get
            {
                return m_CourseService ?? (m_CourseService = App.Current.ServiceManager.GetService<ICourseService>());
            }
        }

        protected IInterfaceAdapterService InterfaceAdapterService
        {
            get
            {
                return m_InterfaceAdapterService ??
                       (m_InterfaceAdapterService =
                           App.Current.ServiceManager.GetService<IInterfaceAdapterService>(CBTC.Type.ToString()));
            }
        }


        protected DataAdapterBase(Infrasturcture.Model.CBTC adaptTarget, SendInterfaceBase sendInterface)
        {
            Contract.Requires(sendInterface != null);
            CBTC = adaptTarget;
            CBTC.SendInterface = sendInterface;
            m_StationNameProviderService =
                adaptTarget.InitParam.DataPackage.ServiceManager.GetService<IStationNameProviderService>();
            SignalDataOut = sendInterface.DataOut;

            InCalculateFlag = false;
            DataService = adaptTarget.DataService;
            StationConfigDictionary = new ReadOnlyDictionary<ulong, StationConfig>(ExcelParser.Parser<StationConfig>(
                    adaptTarget.InitParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory)
                .ToDictionary(kvp => kvp.StationKey, kvp => kvp));
            m_DateTimeInterpreter = DateTimeInterpreterFactory.Instance.GetInterpreter(RawDataType.DateTime);
            m_DateTimeInterpreterFormSec = DateTimeInterpreterFactory.Instance.GetInterpreter(RawDataType.Second);

        }


        //bool量输入
        public virtual void OnBoolChangedImp(object sender, CommunicationDataChangedWrapperArgs<bool> dataChangedArgs)
        {
            ////故障更新
            for (int i = InterfaceAdapterService.InBoolDictionary[InbKeys.故障1];
                i < (SignalDataIn.Fault.Length + InterfaceAdapterService.InBoolDictionary[InbKeys.故障1]);
                i++)
            {
                dataChangedArgs.UpdateIfContains(i,
                    b => SignalDataIn.Fault[i - InterfaceAdapterService.InBoolDictionary[InbKeys.故障1]] = b);
            }

            //消息文本
            for (int i = InterfaceAdapterService.InBoolDictionary[InbKeys.消息1];
             i < (SignalDataIn.MessageInfo.Length + InterfaceAdapterService.InBoolDictionary[InbKeys.消息1]);
              i++)
            {
                dataChangedArgs.UpdateIfContains(i,
                    b => SignalDataIn.MessageInfo[i - InterfaceAdapterService.InBoolDictionary[InbKeys.消息1]] = b);
            }

            //确认按钮
            dataChangedArgs.UpdateIfContains(InbKeys.确认按钮, b => SignalDataIn.TxtConfirmFlag = b);

            //黑屏
            dataChangedArgs.UpdateIfContains(InbKeys.亮屏, b => SignalDataIn.PowerFlag = b);

            //定位建立
            dataChangedArgs.UpdateIfContains(InbKeys.定位建立, b => SignalDataIn.LocationBuildFlag = b);

            //列车完整性
            dataChangedArgs.UpdateIfContains(InbKeys.列车完整性, b => SignalDataIn.TrainCompleteFlag = b);

            //确认折返
            dataChangedArgs.UpdateIfContains(InbKeys.确认折返, b => SignalDataIn.TurnBackConfirmFlag = b);

            //进入车辆段
            dataChangedArgs.UpdateIfContains(InbKeys.进入车辆段, b => SignalDataIn.DepotEnterFlag = b);

            //所有门关闭状态
            dataChangedArgs.UpdateIfContains(InbKeys.所有门关闭状态, b => SignalDataIn.AllDoorCloseSign = b);

            //屏蔽门全部关闭标志
            dataChangedArgs.UpdateIfContains(InbKeys.屏蔽门全部关闭标志, b => SignalDataIn.AllPSDCloseSign = b);

            //停车对标状态
            dataChangedArgs.UpdateIfContains(InbKeys.停车对标状态, b => SignalDataIn.BStopRight = b);

            //自动折返允许
            dataChangedArgs.UpdateIfContains(InbKeys.自动折返允许, b => SignalDataIn.BPermitATB = b);

            //人工折返允许
            dataChangedArgs.UpdateIfContains(InbKeys.人工折返允许, b => SignalDataIn.BPermitMTB = b);

            //列车在站内
            dataChangedArgs.UpdateIfContains(InbKeys.列车在站内, b => SignalDataIn.BAtStation = b);

            //列车正在折返
            dataChangedArgs.UpdateIfContains(InbKeys.列车正在折返, b => SignalDataIn.BATBing = b);

            //屏保
            dataChangedArgs.UpdateIfContains(InbKeys.屏保, b => SignalDataIn.ScreenProtection = b);

            //警惕按钮
            dataChangedArgs.UpdateIfContains(InbKeys.警惕按钮, b => SignalDataIn.AlertButton = b);

            //模式闪烁标志
            dataChangedArgs.UpdateIfContains(InbKeys.模式闪烁标志, b => SignalDataIn.ModeFlashFlag = b);

            //车载设备电源
            dataChangedArgs.UpdateIfContains(InbKeys.车载设备电源, b => SignalDataIn.DevicePower = b);

            //电钥匙
            dataChangedArgs.UpdateIfContains(InbKeys.电钥匙, b => SignalDataIn.KeySwitch = b);

            //ATC切除开关
            dataChangedArgs.UpdateIfContains(InbKeys.ATC切除开关, b => SignalDataIn.ATCPass = b);

            //ATB按钮
            dataChangedArgs.UpdateIfContains(InbKeys.ATB按钮, b => SignalDataIn.AtbButton = b);

            //进站标志
            dataChangedArgs.UpdateIfContains(InbKeys.进站标志, b => SignalDataIn.StoppingStationFlag = b);




        }
        //float输入
        public virtual void OnFloatChangedImp(object sender, CommunicationDataChangedWrapperArgs<float> dataChangedArgs)
        {

            // 当前时间
            dataChangedArgs.UpdateIfContains(InfKeys.显示的日期,
                f => SignalDataIn.NowTime = m_DateTimeInterpreter.Interpreter(f, GetInFloatAt(InfKeys.显示的时间)));

            dataChangedArgs.UpdateIfContains(InfKeys.显示的时间,
                f => SignalDataIn.NowTime = m_DateTimeInterpreter.Interpreter(GetInFloatAt(InfKeys.显示的日期), f));

            //发车时间
            dataChangedArgs.UpdateIfContains(InfKeys.停站时间, f => SignalDataIn.StationStopTime = (int)f);
            CBTC.RoadInfo.StationInfo.PSD.DepartTime = m_DateTimeInterpreterFormSec.Interpreter(SignalDataIn.StationStopTime);

            //发车时间
            // dataChangedArgs.UpdateIfContains(InfKeys.停站时间, f => SignalDataIn.StationStopTime = (int)f);
            //CBTC.RoadInfo.StationInfo.PSD.DepartTime = m_DateTimeInterpreterFormSec.Interpreter(SignalDataIn.StationStopTime);
            //dataChangedArgs.UpdateIfContains(InfKeys.显示的时间,
            // f => CBTC.RoadInfo.StationInfo.PSD.DepartTime = m_DateTimeInterpreter.Interpreter(GetInFloatAt(InfKeys.显示的日期), 
            //   SignalDataIn.StationStopTime));
            //dataChangedArgs.UpdateIfContains(InfKeys.显示的日期,
            //   f => SignalDataIn.NowTime = m_DateTimeInterpreter.Interpreter(f, GetInFloatAt(InfKeys.显示的时间)));
            //CBTC.RoadInfo.StationInfo.PSD.DepartTime = m_DateTimeInterpreter.Interpreter(SignalDataIn.StationStopTime,
            //    SignalDataIn.StationStopTime);

            var dt = new DateTime();

            var strTime = CBTC.RoadInfo.StationInfo.PSD.DepartTime.ToString("hh时mm分ss秒");
            //目标距离ss
            dataChangedArgs.UpdateIfContains(InfKeys.目标距离, f => SignalDataIn.GoalDistance = (int)f);


            //列车速度
            dataChangedArgs.UpdateIfContains(InfKeys.列车运行速度, f => SignalDataIn.TrainSpeed = (int)f);
            if (SignalDataIn.TrainSpeed < 0)
            {
                SignalDataIn.TrainSpeed = -1 * SignalDataIn.TrainSpeed;
            }

            //列车限制速度
            dataChangedArgs.UpdateIfContains(InfKeys.列车限制速度, f => SignalDataIn.TrainLimitSpeed = (int)f);

            //列车推荐速度
            dataChangedArgs.UpdateIfContains(InfKeys.列车推荐速度, f => SignalDataIn.TrainRecommendSpeed = (int)f);

            //列车目标速度
            dataChangedArgs.UpdateIfContains(InfKeys.列车目标速度, f => SignalDataIn.TrainGoalSpeed = (int)f);

            //列车报警速度
            dataChangedArgs.UpdateIfContains(InfKeys.列车报警速度, f => SignalDataIn.TrainWarningSpeed = (int)f);

            //列车紧急速度
            dataChangedArgs.UpdateIfContains(InfKeys.列车紧急速度, f => SignalDataIn.TrainEmergSpeed = (int)f);

            //控制模式
            dataChangedArgs.UpdateIfContains(InfKeys.控制模式, f => SignalDataIn.ControlMode = (int)f);

            //运行等级
            dataChangedArgs.UpdateIfContains(InfKeys.运行等级, f => SignalDataIn.RunLevel = (int)f);

            //制动状态
            dataChangedArgs.UpdateIfContains(InfKeys.制动状态, f => SignalDataIn.BrakeStatus = (int)f);

            //CBTC连接状态
            dataChangedArgs.UpdateIfContains(InfKeys.CBTC连接状态, f => SignalDataIn.CBTCStatus = (int)f);

            //下一站
            // dataChangedArgs.UpdateIfContains(InfKeys.下一站, f => SignalDataIn.NextStationNo = BitConverter.ToInt32(BitConverter.GetBytes(f), 0));

            //终点站
            // dataChangedArgs.UpdateIfContains(InfKeys.终点站, f => SignalDataIn.EndStationNo = BitConverter.ToInt32(BitConverter.GetBytes(f), 0));

            //下一站
            dataChangedArgs.UpdateIfContains(InfKeys.下一站, f => SignalDataIn.NextStationNo = (int)f);

            //终点站
            dataChangedArgs.UpdateIfContains(InfKeys.终点站, f => SignalDataIn.EndStationNo = (int)f);

            //工况
            dataChangedArgs.UpdateIfContains(InfKeys.工况, f => SignalDataIn.WorkStatus = (int)f);

            //最高可用模式
            dataChangedArgs.UpdateIfContains(InfKeys.最高可用模式, f => SignalDataIn.MaxPermitMode = (int)f);

            //车首OBCU
            dataChangedArgs.UpdateIfContains(InfKeys.车首OBCU, f => SignalDataIn.OBCUHeadStatus = (int)f);

            //车尾OBCU
            dataChangedArgs.UpdateIfContains(InfKeys.车尾OBCU, f => SignalDataIn.OBCUTailStatus = (int)f);

            //进入折返区域
            dataChangedArgs.UpdateIfContains(InfKeys.进入折返区域, f => SignalDataIn.TurnBackAreaStatus = (int)f);

            //左车门允许
            dataChangedArgs.UpdateIfContains(InfKeys.左车门允许, f => SignalDataIn.DoorLeftPermit = (int)f);

            //右车门允许
            dataChangedArgs.UpdateIfContains(InfKeys.右车门允许, f => SignalDataIn.DoorRightPermit = (int)f);

            //左侧屏蔽门允许
            dataChangedArgs.UpdateIfContains(InfKeys.左侧屏蔽门允许, f => SignalDataIn.PSDLeftPermit = (int)f);

            //右侧屏蔽门允许
            dataChangedArgs.UpdateIfContains(InfKeys.右侧屏蔽门允许, f => SignalDataIn.PSDRightPermit = (int)f);

            //左侧车门状态
            dataChangedArgs.UpdateIfContains(InfKeys.左侧车门状态, f => SignalDataIn.DoorLeftStatus = (int)f);

            //右侧车门状态
            dataChangedArgs.UpdateIfContains(InfKeys.右侧车门状态, f => SignalDataIn.DoorRightStatus = (int)f);

            //左侧屏蔽门状态
            dataChangedArgs.UpdateIfContains(InfKeys.左侧屏蔽门状态, f => SignalDataIn.PSDLeftStatus = (int)f);

            //右侧屏蔽门状态
            dataChangedArgs.UpdateIfContains(InfKeys.右侧屏蔽门状态, f => SignalDataIn.PSDRightStatus = (int)f);

            //停车状态
            dataChangedArgs.UpdateIfContains(InfKeys.停车状态, f => SignalDataIn.DockedStatus = (int)f);

            //速度指示
            dataChangedArgs.UpdateIfContains(InfKeys.速度指示, f => SignalDataIn.SpeedStatus = (int)f);

            //空转指示
            dataChangedArgs.UpdateIfContains(InfKeys.空转指示, f => SignalDataIn.IdlingStatus = (int)f);

            //门控模式
            dataChangedArgs.UpdateIfContains(InfKeys.门控模式, f => SignalDataIn.DoorOperationMode = (int)f);

            //子系统故障
            dataChangedArgs.UpdateIfContains(InfKeys.子系统故障, f => SignalDataIn.SubsystemFault = (int)f);

            //车站控车状态
            dataChangedArgs.UpdateIfContains(InfKeys.车站控车状态, f => SignalDataIn.StationControlTrainStatus = (int)f);

            //停站时间
            dataChangedArgs.UpdateIfContains(InfKeys.停站时间, f => SignalDataIn.StationStopTime = f);

            //发车状态
            dataChangedArgs.UpdateIfContains(InfKeys.发车状态, f => SignalDataIn.DepartStatus = (int)f);

            //运行方向
            dataChangedArgs.UpdateIfContains(InfKeys.运行方向, f => SignalDataIn.RunDirection = (int)f);

            //司机号字母
            dataChangedArgs.UpdateIfContains(InfKeys.司机号字母, f => SignalDataIn.DriverLetter = (int)f);

            //司机号
            dataChangedArgs.UpdateIfContains(InfKeys.司机号, f => SignalDataIn.DriverNum = (int)f);

            //车次号字母
            dataChangedArgs.UpdateIfContains(InfKeys.车次号字母, f => SignalDataIn.TrainLetter = (int)f);

            //车次号
            dataChangedArgs.UpdateIfContains(InfKeys.车次号, f => SignalDataIn.TrainNum = (int)f);

            //编组
            dataChangedArgs.UpdateIfContains(InfKeys.编组, f => SignalDataIn.GroupNum = (int)f);

            //目的地号字母
            dataChangedArgs.UpdateIfContains(InfKeys.目的地号字母, f => SignalDataIn.DestLetter = (int)f);

            //目的地号
            dataChangedArgs.UpdateIfContains(InfKeys.目的地号, f => SignalDataIn.DestNum = (int)f);

            //VOBC状态
            dataChangedArgs.UpdateIfContains(InfKeys.VOBC状态, f => SignalDataIn.VOBCStatus = (int)f);

            //ATO允许状态
            dataChangedArgs.UpdateIfContains(InfKeys.ATO允许状态, f => SignalDataIn.ATOPermitStatus = (int)f);

            //ATP允许状态
            dataChangedArgs.UpdateIfContains(InfKeys.ATP允许状态, f => SignalDataIn.ATPPermitStatus = (int)f);

            //IATO允许状态
            dataChangedArgs.UpdateIfContains(InfKeys.IATO允许状态, f => SignalDataIn.IATOPermitStatus = (int)f);

            //IATP允许状态
            dataChangedArgs.UpdateIfContains(InfKeys.IATP允许状态, f => SignalDataIn.IATPPermitStatus = (int)f);

            //离站时间
            dataChangedArgs.UpdateIfContains(InfKeys.离站时间, f => SignalDataIn.LeftTime = (int)f);

            //后备模式
            dataChangedArgs.UpdateIfContains(InfKeys.后备模式, f => SignalDataIn.BMMode = (int)f);

            //ATC请求状态
            dataChangedArgs.UpdateIfContains(InfKeys.ATC请求状态, f => SignalDataIn.ATCRequestStatus = (int)f);

            //下一停车点类型
            dataChangedArgs.UpdateIfContains(InfKeys.下一停车点类型, f => SignalDataIn.TrainLMAType = (int)f);

            //列车下一区段所在区域
            dataChangedArgs.UpdateIfContains(InfKeys.列车下一区段所在区域, f => SignalDataIn.TrainNextAreaType = (int)f);

            //列车前一区段所在区域
            dataChangedArgs.UpdateIfContains(InfKeys.列车前一区段所在区域, f => SignalDataIn.TrainPreAreaType = (int)f);

            //列车当前所在区域
            dataChangedArgs.UpdateIfContains(InfKeys.列车当前所在区域, f => SignalDataIn.TrainCurAreaType = (int)f);

            //控车类型
            dataChangedArgs.UpdateIfContains(InfKeys.控车类型, f => SignalDataIn.BrakeMode = (int)f);

            //机车信号码
            dataChangedArgs.UpdateIfContains(InfKeys.机车信号码, f => SignalDataIn.CabSignalCode = (int)f);

            //前方信号机状态
            dataChangedArgs.UpdateIfContains(InfKeys.前方信号机状态, f => SignalDataIn.FrontSignalStatus = (int)f);

            //列车长度
            dataChangedArgs.UpdateIfContains(InfKeys.列车长度, f => SignalDataIn.TrainLength = (int)f);

            //服务号
            dataChangedArgs.UpdateIfContains(InfKeys.服务号, f => SignalDataIn.ServiceNum = (int)f);

            //列车当前限速 ATP当前限速
            dataChangedArgs.UpdateIfContains(InfKeys.ATP当前限速, f => SignalDataIn.ATPSpeedLimit = f);

            //切除牵引速度
            dataChangedArgs.UpdateIfContains(InfKeys.切除牵引速度, f => SignalDataIn.CutOffPowerSpeed = f);

            //RM允许状态
            dataChangedArgs.UpdateIfContains(InfKeys.RM允许状态, f => SignalDataIn.RMPermitStatus = (int)f);

            //移动授权
            dataChangedArgs.UpdateIfContains(InfKeys.移动授权, f => SignalDataIn.MoveAllow = (int)f);

            //车辆实时运行工况
            dataChangedArgs.UpdateIfContains(InfKeys.车辆实时运行工况, f => SignalDataIn.RealTimeWorkStatus = f);

        }
        //功能实现并输出
        public virtual void OnDataChangedImp(object sender, CommunicationDataChangedWrapperArgs<bool> changedBools, CommunicationDataChangedWrapperArgs<float> changedFloats)
        {

        }
        //文本反馈
        public virtual bool FeedBackInfo(object sender, CommunicationDataChangedWrapperArgs<bool> dataChangedArgs)
        {

            ////反馈_取消确认越性
            //dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_取消确认越行, b =>
            //{
            //    if (b)
            //    {
            //        SignalDataOut.CancleExeSign = false;
            //    }
            //});
            return true;

        }

        //输出适配
        public virtual bool ScreenToSignalInfo()
        {
            //DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary["OutBoolKeys.启动确认"],
            //    SignalDataOut.StartConfirm);
            //if (BrakeTestRelease)
            //{
            //    BrakeTestRelease = false;
            //}

            return true;
        }

        //基础信息
        public abstract bool BasicAndTrainInfo();



        //速度表盘控制
        public virtual bool ScreenSpeedInfo()
        {
            //CBTC.SignalInfo.Speed.SpeedDialPlate
            CBTC.SignalInfo.Speed.CurrentSpeed.Visible = true;
            CBTC.SignalInfo.Speed.CurrentSpeed.Value = SignalDataIn.TrainSpeed;


            if (SignalDataIn.TrainSpeed > SignalDataIn.TrainEmergSpeed)
            {
                CBTC.SignalInfo.Speed.CurrentSpeed.SpeedColor = CBTCColor.Red;
            }

            CBTC.SignalInfo.Speed.EmergencyBrakeInterventionSpeed.Visible = true;
            CBTC.SignalInfo.Speed.EmergencyBrakeInterventionSpeed.Value = SignalDataIn.TrainEmergSpeed;
            CBTC.SignalInfo.Speed.EmergencyBrakeInterventionSpeed.SpeedColor = CBTCColor.Red;
            return true;
        }

        //计划区
        public virtual bool ScreenPlanInfo()
        {

            return true;
        }

        //目标距离控制
        public virtual bool ScreenDistanceInfo()
        {
            if ((TrainOperatingMode)SignalDataIn.ControlMode != TrainOperatingMode.AM
                && (TrainOperatingMode)SignalDataIn.ControlMode != TrainOperatingMode.CM)
            {
                CBTC.SignalInfo.WarningIntervention.TargetDistanceVisible = false;
                return false;
            }
            CBTC.SignalInfo.WarningIntervention.TargetDistanceVisible = true;
            CBTC.SignalInfo.WarningIntervention.TargetDistance = SignalDataIn.GoalDistance;
            //CBTC.SignalInfo.WarningIntervention.TargitDistanceScale;
            if (SignalDataIn.GoalDistance > 300)
            {
                CBTC.SignalInfo.WarningIntervention.WarningColor = CBTCColor.Green;
            }
            else if (SignalDataIn.GoalDistance <= 300 && SignalDataIn.GoalDistance > 150)
            {
                if (SignalDataIn.TrainGoalSpeed >= 25)
                {
                    CBTC.SignalInfo.WarningIntervention.WarningColor = CBTCColor.Green;
                }
                else
                {
                    CBTC.SignalInfo.WarningIntervention.WarningColor = CBTCColor.Yellow;
                }
            }
            else
            {
                if (SignalDataIn.TrainGoalSpeed >= 60)
                {
                    CBTC.SignalInfo.WarningIntervention.WarningColor = CBTCColor.Green;
                }
                else if (SignalDataIn.TrainGoalSpeed > 0.5)
                {
                    CBTC.SignalInfo.WarningIntervention.WarningColor = CBTCColor.Yellow;
                }
                else
                {
                    CBTC.SignalInfo.WarningIntervention.WarningColor = CBTCColor.Red;
                }
            }

            CBTC.SignalInfo.Speed.TargetSpeed.Value = SignalDataIn.TrainGoalSpeed;
            CBTC.SignalInfo.Speed.TargetSpeed.Visible = true;
            return true;
        }
        //按键使能
        public abstract bool ScreenBtnEnable();
        //屏文本控制
        public abstract bool ScreenTextInfo();
        //屏其他控制(启机流程)
        public abstract bool ScreenControl();
        //清理
        public abstract bool ClearInfos();

        //故障
        public virtual bool UpdateEqpmtFault()
        {
            return false;
        }

        //其他页面显示信息
        public virtual bool OtherPageShowControlInfo()
        {
            return false;
        }

        public virtual void ClearDataOnCourseStop()
        {
            CBTC.Other.ShowingTimeDifference = TimeSpan.Zero;

            ClearInfos();
        }

        protected float GetInFloatAt(string keyName)
        {
            return DataService.ReadService.GetFloatAt(InterfaceAdapterService.InFloatDictionary[keyName]);
        }

        protected bool GetInBoolAt(string keyName)
        {
            return DataService.ReadService.GetBoolAt(InterfaceAdapterService.InBoolDictionary[keyName]);
        }

        protected float GetOutFloatAt(string keyName)
        {
            return DataService.WriteService.ReadOnlyFloatDictionary[InterfaceAdapterService.OutFloatDictionary[keyName]];
        }

        protected bool GetOutBoolAt(string keyName)
        {
            return DataService.WriteService.ReadOnlyBoolDictionary[InterfaceAdapterService.OutBoolDictionary[keyName]];
        }

        /// <summary>bool 值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            m_UpdateBoolAction.Invoke(this, sender,
                    new CommunicationDataChangedWrapperArgs<bool>(dataChangedArgs, CBTC, InterfaceAdapterService));
            //App.Current.GetMainDispatcher()
            //    .Invoke(m_UpdateBoolAction, this, sender,
            //        new CommunicationDataChangedWrapperArgs<bool>(dataChangedArgs, CBTC, InterfaceAdapterService));
        }

        /// <summary>float值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            m_UpdateFloatAction.Invoke(this, sender,
                    new CommunicationDataChangedWrapperArgs<float>(dataChangedArgs, CBTC, InterfaceAdapterService));
            //App.Current.GetMainDispatcher()
            //    .Invoke(m_UpdateFloatAction, this, sender,
            //        new CommunicationDataChangedWrapperArgs<float>(dataChangedArgs, CBTC, InterfaceAdapterService));
        }

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
            m_UpdateDataAction.Invoke(this, sender,
                    new CommunicationDataChangedWrapperArgs<bool>(dataChangedArgs.ChangedBools, CBTC,
                        InterfaceAdapterService),
                    new CommunicationDataChangedWrapperArgs<float>(dataChangedArgs.ChangedFloats, CBTC,
                        InterfaceAdapterService));
            //App.Current.GetMainDispatcher()
            //    .Invoke(m_UpdateDataAction, this, sender,
            //        new CommunicationDataChangedWrapperArgs<bool>(dataChangedArgs.ChangedBools, CBTC,
            //            InterfaceAdapterService),
            //        new CommunicationDataChangedWrapperArgs<float>(dataChangedArgs.ChangedFloats, CBTC,
            //            InterfaceAdapterService));
        }

        public string GetStationName(ulong stationNo)
        {
            if (m_StationNameProviderService.StationDictionary != null && m_StationNameProviderService.StationDictionary.ContainsKey((int)stationNo))
            {
                return m_StationNameProviderService.StationDictionary[(int)stationNo].Name;
            }

            StationConfig item;
            return StationConfigDictionary.TryGetValue(stationNo, out item) ? item.Staion : string.Empty;
        }
    }
}
