using System;
using System.Diagnostics.Contracts;
using System.Linq;
using CommonUtil.Model;
using Excel.Interface;
using Mmi.Common.DateTimeInterpreter;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Service;
using Tram.CBTC.DataAdapter.ConcreateAdapter;
using Tram.CBTC.DataAdapter.ConcreateAdapter.CASCO;
using Tram.CBTC.DataAdapter.Model;
using Tram.CBTC.DataAdapter.Resource.Keys;
using Tram.CBTC.Infrasturcture.Events;
using Tram.CBTC.Infrasturcture.Extension;
using Tram.CBTC.Infrasturcture.Model.Road;
using Tram.CBTC.Infrasturcture.Service;

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


namespace Tram.CBTC.DataAdapter
{
    public abstract class DataAdapterBase : IDataListener
    {
        private readonly IDateTimeInterpreter m_DateTimeInterpreter;
        private readonly IDateTimeInterpreter m_DateTimeInterpreterFormSec;

        private readonly Action<DataAdapterBase, object, CommunicationDataChangedWrapperArgs<bool>> m_UpdateBoolAction =
            (obj, sender, args) => obj.OnBoolChangedImp(sender, args);

        private readonly Action<DataAdapterBase, object, CommunicationDataChangedWrapperArgs<bool>,
            CommunicationDataChangedWrapperArgs<float>> m_UpdateDataAction =
            (obj, sender, args1, args2) => obj.OnDataChangedImp(sender, args1, args2);

        private readonly Action<DataAdapterBase, object, CommunicationDataChangedWrapperArgs<float>> m_UpdateFloatAction
            = (obj, sender, args) => obj.OnFloatChangedImp(sender, args);

        private ICourseService m_CourseService;

        private IInterfaceAdapterService m_InterfaceAdapterService;

        private readonly IStationNameProviderService m_StationNameProviderService;

        public CommonUtil.Model.IReadOnlyDictionary<ulong, StationConfig> StationConfigDictionary { get; }


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

            m_DateTimeInterpreter = DateTimeInterpreterFactory.Instance.GetInterpreter(RawDataType.DateTime);

            m_DateTimeInterpreterFormSec = DateTimeInterpreterFactory.Instance.GetInterpreter(RawDataType.Second);


            StationConfigDictionary = new ReadOnlyDictionary<ulong, StationConfig>(ExcelParser.Parser<StationConfig>(
                        adaptTarget.InitParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory)
                    .ToDictionary(kvp => kvp.StationKey, kvp => kvp));
        }


        /// <summary>
        /// 设置站点路过状态
        /// </summary>
        /// <param name="lastID">上一站ID</param>
        /// <param name="nextID">下一站ID</param>
        /// <param name="offset">偏移了多少个点</param>
        public bool SetPassed(ulong lastID, ulong nextID, int offset)
        {
            var index1 = CBTC.RoadInfo.StationInfo.StationPasses.IndexOf(CBTC.RoadInfo.StationInfo.StationPasses.FirstOrDefault(w => w.ID == lastID && w.IsStation));
            var index2 = CBTC.RoadInfo.StationInfo.StationPasses.IndexOf(CBTC.RoadInfo.StationInfo.StationPasses.FirstOrDefault(w => w.ID == nextID && w.IsStation));
            //到终点站，前方无站,全变红
            if (lastID > 0 && nextID == 0)
            {
                for (int i = 0; i < CBTC.RoadInfo.StationInfo.StationPasses.Count; ++i)
                {
                    CBTC.RoadInfo.StationInfo.StationPasses[i].Passed = true;
                    CBTC.RoadInfo.StationInfo.StationPasses[i].Flicker = false;

                }
                return true;
            }
            if (lastID == 0)//开始没有站,全是绿的
            {
                for (int i = 0; i < CBTC.RoadInfo.StationInfo.StationPasses.Count; ++i)
                {
                    CBTC.RoadInfo.StationInfo.StationPasses[i].Passed = false;
                    CBTC.RoadInfo.StationInfo.StationPasses[i].Flicker = false;

                }
                return true;
            }

            if (index1 > index2)
            {

                for (int i = index1 - offset; i < CBTC.RoadInfo.StationInfo.StationPasses.Count; ++i)
                {

                    CBTC.RoadInfo.StationInfo.StationPasses[i].Passed = true;

                    if (i == index1 - offset)
                    {
                        if (i <= CBTC.RoadInfo.StationInfo.StationPasses.Count - 2)
                        {
                            CBTC.RoadInfo.StationInfo.StationPasses[i + 1].Flicker = false;
                        }
                        CBTC.RoadInfo.StationInfo.StationPasses[i].Flicker = true;
                    }
                }

                for (int j = 0; j < index1 - offset; ++j)
                {
                    CBTC.RoadInfo.StationInfo.StationPasses[j].Passed = false;
                    CBTC.RoadInfo.StationInfo.StationPasses[j].Flicker = false;
                }
            }
            else
            {
                for (int i = 0; i <= index1 + offset; ++i)
                {
                    CBTC.RoadInfo.StationInfo.StationPasses[i].Passed = true;

                    if (i == index1 + offset)
                    {
                        if (i != 0)
                        {
                            CBTC.RoadInfo.StationInfo.StationPasses[i - 1].Flicker = false;
                        }
                        CBTC.RoadInfo.StationInfo.StationPasses[i].Flicker = true;
                    }
                }
                for (int j = index1 + offset + 1; j < CBTC.RoadInfo.StationInfo.StationPasses.Count; ++j)
                {
                    CBTC.RoadInfo.StationInfo.StationPasses[j].Passed = false;
                    CBTC.RoadInfo.StationInfo.StationPasses[j].Flicker = false;
                }
            }

            return true;
        }

        /// <summary>
        /// 获取1970-01-01 08:00:00至dateTime的毫秒数
        /// </summary>
        public long GetTimestamp(DateTime dateTime)
        {
            DateTime baseTime = Convert.ToDateTime("1970-1-1 8:00:00");
            TimeSpan ts = dateTime - baseTime;
            long intervel = (long)ts.TotalSeconds;

            return intervel;
        }


        /// <summary>
        /// 根据时间戳timestamp（单位毫秒）计算日期
        /// </summary>
        public DateTime NewDate(long timestamp)
        {
            DateTime baseTime = Convert.ToDateTime("1970-1-1 8:00:00");
            TimeSpan ts = new TimeSpan(0, 0, (int)timestamp);
            DateTime curTime = baseTime + ts;

            return curTime;
        }


        protected ICommunicationDataService DataService { get; set; }
        protected Infrasturcture.Model.CBTC CBTC { get; }
        protected SignalDataIn SignalDataIn { get; set; }
        protected SignalDataIn SignalDataInOld { get; set; }
        protected SignalDataOut SignalDataOut { get; set; }

        public bool InCalculateFlag { protected set; get; }

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

        /// <summary>bool 值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            //    m_UpdateBoolAction.Invoke(this, sender,
            //new CommunicationDataChangedWrapperArgs<bool>(dataChangedArgs, CBTC, InterfaceAdapterService));
            App.Current.GetMainDispatcher()
                .Invoke(m_UpdateBoolAction, this, sender,
                    new CommunicationDataChangedWrapperArgs<bool>(dataChangedArgs, CBTC, InterfaceAdapterService));
        }

        /// <summary>float值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            //    m_UpdateFloatAction.Invoke(this, sender,
            //new CommunicationDataChangedWrapperArgs<float>(dataChangedArgs, CBTC, InterfaceAdapterService));
            App.Current.GetMainDispatcher()
                .Invoke(m_UpdateFloatAction, this, sender,
                    new CommunicationDataChangedWrapperArgs<float>(dataChangedArgs, CBTC, InterfaceAdapterService));
        }

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
            //    m_UpdateDataAction.Invoke(this, sender,
            //new CommunicationDataChangedWrapperArgs<bool>(dataChangedArgs.ChangedBools, CBTC,
            //    InterfaceAdapterService),
            //new CommunicationDataChangedWrapperArgs<float>(dataChangedArgs.ChangedFloats, CBTC,
            //    InterfaceAdapterService));
            App.Current.GetMainDispatcher()
                .Invoke(m_UpdateDataAction, this, sender,
                    new CommunicationDataChangedWrapperArgs<bool>(dataChangedArgs.ChangedBools, CBTC,
                        InterfaceAdapterService),
                    new CommunicationDataChangedWrapperArgs<float>(dataChangedArgs.ChangedFloats, CBTC,
                        InterfaceAdapterService));
        }


        //bool量输入
        public virtual void OnBoolChangedImp(object sender, CommunicationDataChangedWrapperArgs<bool> dataChangedArgs)
        {


            //黑屏
            dataChangedArgs.UpdateIfContains(InbKeys.亮屏, b => SignalDataIn.PowerFlag = b);

            //定位建立
            dataChangedArgs.UpdateIfContains(InbKeys.定位建立, b => SignalDataIn.LocationBuildFlag = b);

            //列车完整性
            dataChangedArgs.UpdateIfContains(InbKeys.列车完整性, b => SignalDataIn.TrainCompleteFlag = b);


            //停车对标状态
            dataChangedArgs.UpdateIfContains(InbKeys.停车对标状态, b => SignalDataIn.BStopRight = b);


            //列车在站内
            dataChangedArgs.UpdateIfContains(InbKeys.列车在站内, b => SignalDataIn.BAtStation = b);



            //屏保
            dataChangedArgs.UpdateIfContains(InbKeys.屏保, b => SignalDataIn.ScreenProtection = b);



            //车载设备电源
            dataChangedArgs.UpdateIfContains(InbKeys.车载设备电源, b => SignalDataIn.DevicePower = b);

            //电钥匙
            dataChangedArgs.UpdateIfContains(InbKeys.电钥匙, b => SignalDataIn.KeySwitch = b);

            //ATC切除开关
            dataChangedArgs.UpdateIfContains(InbKeys.ATC切除开关, b => SignalDataIn.ATCPass = b);

            CBTC.Message.MessageFactory.CreateMessage(2, DateTime.Now);
            CBTC.Message.MessageFactory.CreateMessage(51, DateTime.Now);

        }

        //float输入
        public virtual void OnFloatChangedImp(object sender, CommunicationDataChangedWrapperArgs<float> dataChangedArgs)
        {

            dataChangedArgs.UpdateIfContains(InfKeys.显示的日期,
             f => SignalDataIn.NowTime = m_DateTimeInterpreter.Interpreter(f, GetInFloatAt(InfKeys.显示的时间)));

            dataChangedArgs.UpdateIfContains(InfKeys.显示的时间,
                f => SignalDataIn.NowTime = m_DateTimeInterpreter.Interpreter(GetInFloatAt(InfKeys.显示的日期), f));
            //发车时间
           // CBTC.RoadInfo.StationInfo.PSD.DepartTime = m_DateTimeInterpreter.Interpreter(SignalDataIn.DepartTime, SignalDataIn.DepartTime);

            ///到站时间 
           // CBTC.RoadInfo.StationInfo.PSD.ArriveTime = m_DateTimeInterpreter.Interpreter(SignalDataIn.ArrivedTime, SignalDataIn.ArrivedTime);

            //距前车运行时分
            CBTC.SignalInfo.ForwardCarTime = m_DateTimeInterpreterFormSec.Interpreter(SignalDataIn.FrontTrainRuntimeInterval);

            ///距后车运行时分 
            CBTC.SignalInfo.AfterCarTime = m_DateTimeInterpreterFormSec.Interpreter(SignalDataIn.BackTrainTuntimeInterval);

            ////发车时间
            CBTC.RoadInfo.StationInfo.PSD.DepartTime = m_DateTimeInterpreterFormSec.Interpreter(SignalDataIn.DepartTime);

            /////到站时间 
            CBTC.RoadInfo.StationInfo.PSD.ArriveTime = m_DateTimeInterpreterFormSec.Interpreter(SignalDataIn.ArrivedTime);

            ////目标距离
            dataChangedArgs.UpdateIfContains( (string)InfKeys.目标距离, f => SignalDataIn.GoalDistance = f);

            //列车速度
            dataChangedArgs.UpdateIfContains(InfKeys.列车运行速度, f => SignalDataIn.TrainSpeed = (int)f);
            if (SignalDataIn.TrainSpeed < 0)
                SignalDataIn.TrainSpeed = -1 * SignalDataIn.TrainSpeed;

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

            ////控制模式
            //dataChangedArgs.UpdateIfContains( (string)InfKeys.控制模式, f => SignalDataIn.ControlMode = (int)f);

            ////运行等级
            //dataChangedArgs.UpdateIfContains( (string)InfKeys.运行等级, f => SignalDataIn.RunLevel = (int)f);

            //制动状态
            dataChangedArgs.UpdateIfContains(InfKeys.信号制动状态, f => SignalDataIn.BrakeStatus = (int)f);

            //CBTC连接状态
            dataChangedArgs.UpdateIfContains(InfKeys.CBTC连接状态, f => SignalDataIn.CBTCStatus = (int)f);

            //列车当前限速 ATP当前限速
            dataChangedArgs.UpdateIfContains(InfKeys.ATP当前限速, f => SignalDataIn.ATPSpeedLimit = (int)f);

            //切除牵引速度
            dataChangedArgs.UpdateIfContains(InfKeys.切除牵引速度, f => SignalDataIn.CutOffPowerSpeed = (int)f);

            //当前站
            dataChangedArgs.UpdateIfContains(InfKeys.当前站, f => SignalDataIn.CurrentStationNo = (int)f);

            //下一站
            dataChangedArgs.UpdateIfContains(InfKeys.下一站, f => SignalDataIn.NextStationNo = (int)f);

            //终点站
            dataChangedArgs.UpdateIfContains(InfKeys.终点站, f => SignalDataIn.EndStationNo = (int)f);

            //车首OBCU
            dataChangedArgs.UpdateIfContains(InfKeys.车首OBCU, f => SignalDataIn.OBCUHeadStatus = (int)f);

            //车尾OBCU
            dataChangedArgs.UpdateIfContains(InfKeys.车尾OBCU, f => SignalDataIn.OBCUTailStatus = (int)f);

            //左车门允许
            dataChangedArgs.UpdateIfContains(InfKeys.左车门允许, f => SignalDataIn.DoorLeftPermit = (int)f);

            //右车门允许
            dataChangedArgs.UpdateIfContains(InfKeys.右车门允许, f => SignalDataIn.DoorRightPermit = (int)f);

            //停车状态
            dataChangedArgs.UpdateIfContains(InfKeys.停车状态, f => SignalDataIn.DockedStatus = (int)f);

            //速度指示
            dataChangedArgs.UpdateIfContains(InfKeys.速度指示, f => SignalDataIn.SpeedStatus = (int)f);

            //空转指示
            dataChangedArgs.UpdateIfContains(InfKeys.空转指示, f => SignalDataIn.IdlingStatus = (int)f);

            ////门控模式
            //dataChangedArgs.UpdateIfContains( (string)InfKeys.门控模式, f => SignalDataIn.DoorOperationMode = (int)f);

            //车站控车状态
            dataChangedArgs.UpdateIfContains(InfKeys.车站控车状态, f => SignalDataIn.StationControlTrainStatus = (int)f);

            //停站时间
            dataChangedArgs.UpdateIfContains(InfKeys.停站时间, f => SignalDataIn.StationStopTime = (int)f);



            //车次号
            dataChangedArgs.UpdateIfContains(InfKeys.车次号, f => SignalDataIn.TrainNum = (int)f);



            //列车下一区段所在区域
            dataChangedArgs.UpdateIfContains(InfKeys.列车下一区段所在区域, f => SignalDataIn.TrainNextAreaType = (int)f);

            //列车前一区段所在区域
            dataChangedArgs.UpdateIfContains(InfKeys.列车前一区段所在区域, f => SignalDataIn.TrainPreAreaType = (int)f);

            //列车当前所在区域
            dataChangedArgs.UpdateIfContains(InfKeys.列车当前所在区域, f => SignalDataIn.TrainCurAreaType = (int)f);

            ////前车距离
            //dataChangedArgs.UpdateIfContains(InfKeys.前车距离, f => SignalDataIn.FrontTrainDis = (int)f);
            //折返信息
            dataChangedArgs.UpdateIfContains(InfKeys.折返信息, f => SignalDataIn.TurnBackStatus = (int)f);

            //前方信号机状态
            dataChangedArgs.UpdateIfContains(InfKeys.前方信号机状态, f => SignalDataIn.FrontSignalStatus = (int)f);

            //距前车运行时分
            dataChangedArgs.UpdateIfContains(InfKeys.距前车运行时分, f => SignalDataIn.FrontTrainRuntimeInterval = (int)f);
            //距后车运行时分
            dataChangedArgs.UpdateIfContains(InfKeys.距后车运行时分, f => SignalDataIn.BackTrainTuntimeInterval = (int)f);

        }

        //功能实现并输出
        public virtual void OnDataChangedImp(object sender, CommunicationDataChangedWrapperArgs<bool> changedBools,
            CommunicationDataChangedWrapperArgs<float> changedFloats)
        {

        }

        //文本反馈
        public virtual bool FeedBackInfo(object sender, CommunicationDataChangedWrapperArgs<bool> dataChangedArgs)
        {

            return true;
        }

        //输出适配
        public virtual bool ScreenToSignalInfo()
        {
            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OuBKeys.RADAR声音开关], SignalDataOut.OpenRadar);


            return true;
        }

        //基础信息
        public abstract bool BasicAndTrainInfo();


        //速度表盘控制
        public virtual bool ScreenSpeedInfo()
        {

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
            return DataService.WriteService.ReadOnlyFloatDictionary
                [InterfaceAdapterService.OutFloatDictionary[keyName]];
        }

        protected bool GetOutBoolAt(string keyName)
        {
            return DataService.WriteService.ReadOnlyBoolDictionary[InterfaceAdapterService.OutBoolDictionary[keyName]];
        }

        public string GetStationName(ulong stationNo)
        {
            if (m_StationNameProviderService.StationDictionary != null
                && m_StationNameProviderService.StationDictionary.ContainsKey((int)stationNo))
                return m_StationNameProviderService.StationDictionary[(int)stationNo].Name;

            StationConfig item;
            return StationConfigDictionary.TryGetValue(stationNo, out item) ? item.Staion : string.Empty;
        }
    }
}
