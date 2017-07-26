
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using CommonUtil.Model;
using Excel.Interface;
using Microsoft.Practices.Prism;
using Mmi.Common.DateTimeInterpreter;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Service;
using Motor.ATP.DataAdapter.ConcreateAdapter;
using Motor.ATP.DataAdapter.Model;
using Motor.ATP.DataAdapter.Resource.Keys;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Extension;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model;
using Motor.ATP.Infrasturcture.Model.Config;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Extension;

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


namespace Motor.ATP.DataAdapter
{
    public abstract class DataAdapterBase : IDataListener
    {
        private ICourseService m_CourseService;

        private IInterfaceAdapterService m_InterfaceAdapterService;
        private IClearDataService m_ClearDataService;
        protected ICommunicationDataService DataService { get; set; }
        protected ATPDomain ATP { get; private set; }
        protected SignalDataIn SignalDataIn { get; set; }
        protected SignalDataIn SignalDataInOld { get; set; }
        protected SignalDataOut SignalDataOut { get; set; }
        protected SignalDataOut SignalDataOutOld { get; set; }

        private readonly IDateTimeInterpreter m_DateTimeInterpreter;

        private readonly List<DistanceSpeedPoint> m_PlanzoneDataBuffer = new List<DistanceSpeedPoint>();

        public CommonUtil.Model.IReadOnlyDictionary<ulong, StationConfig> StationConfigDictionary { get; private set; }

        public bool InCalculateFlag { protected set; get; }

        private readonly Action<DataAdapterBase, object, CommunicationDataChangedWrapperArgs<bool>> m_UpdateBoolAction =
            (obj, sender, args) => obj.OnBoolChangedImp(sender, args);

        private readonly Action<DataAdapterBase, object, CommunicationDataChangedWrapperArgs<float>> m_UpdateFloatAction
            =
            (obj, sender, args) => obj.OnFloatChangedImp(sender, args);

        private readonly Action<DataAdapterBase, object, CommunicationDataChangedArgs> m_UpdateDataAction =
            (obj, sender, args) => obj.OnDataChangedImp(sender, args);

        protected ICourseService CourseService
        {
            get
            {
                return m_CourseService ?? (m_CourseService = App.Current.ServiceManager.GetService<ICourseService>());
            }
        }

        private IClearDataService ClearDataService
        {
            get
            {
                return m_ClearDataService ??
                       (m_ClearDataService =
                           App.Current.ServiceManager.GetService<IClearDataService>(ATP.ATPType.ToString()));
            }
        }

        protected IInterfaceAdapterService InterfaceAdapterService
        {
            get
            {
                return m_InterfaceAdapterService ??
                       (m_InterfaceAdapterService =
                           App.Current.ServiceManager.GetService<IInterfaceAdapterService>(ATP.ATPType.ToString()));
            }
        }


        protected DataAdapterBase(ATPDomain adaptTarget, SendInterfaceBase sendInterface)
        {
            Contract.Requires(sendInterface != null);
            ATP = adaptTarget;
            ATP.SendInterface = sendInterface;
            SignalDataOut = sendInterface.DataOut;

            InCalculateFlag = false;
            DataService = adaptTarget.DataService;
            StationConfigDictionary = new ReadOnlyDictionary<ulong, StationConfig>(ExcelParser.Parser<StationConfig>(
                    adaptTarget.InitParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory)
                .ToDictionary(kvp => kvp.StationKey, kvp => kvp));
            m_DateTimeInterpreter = DateTimeInterpreterFactory.Instance.GetInterpreter(RawDataType.DateTime);
        }

        protected void NotifyClearData()
        {
            ClearDataService.NotifyClearData(this);
        }

        public virtual void OnBoolChangedImp(object sender, CommunicationDataChangedWrapperArgs<bool> dataChangedArgs)
        {
            //按键更新
            if (SignalDataIn.ATPPowerFlag)
            {

                for (int i = 0; i < SignalDataIn.ScreenBotton.Length; i++)
                {
                    dataChangedArgs.UpdateIfContains(i + 1, b => SignalDataIn.ScreenBotton[i] = b);
                }
            }

            //故障更新
            for (int i = InterfaceAdapterService.InBoolDictionary[InBoolKeys.故障1];
                i < (SignalDataIn.Fault.Length + InterfaceAdapterService.InBoolDictionary[InBoolKeys.故障1]);
                i++)
            {
                dataChangedArgs.UpdateIfContains(i,
                    b => SignalDataIn.Fault[i - InterfaceAdapterService.InBoolDictionary[InBoolKeys.故障1]] = b);
            }

            //黑屏
            dataChangedArgs.UpdateIfContains(InBoolKeys.黑屏, b => ATP.Visible = b);

            //定位建立
            dataChangedArgs.UpdateIfContains(InBoolKeys.定位建立, b => SignalDataIn.LocationBuildFlag = b);

            //列车完整性
            dataChangedArgs.UpdateIfContains(InBoolKeys.列车完整性, b => SignalDataIn.TrainCompleteFlag = b);

            //留意防护
            dataChangedArgs.UpdateIfContains(InBoolKeys.溜逸防护, b => SignalDataIn.SlideProtectFlag = b);

            //启动条件
            dataChangedArgs.UpdateIfContains(InBoolKeys.启动条件, b => SignalDataIn.StartConditionFlag = b);

            //启动完成
            dataChangedArgs.UpdateIfContains(InBoolKeys.启动完成, b => SignalDataIn.StartFinishFlag = b);

            //越行提示
            dataChangedArgs.UpdateIfContains(InBoolKeys.越行提示, b => SignalDataIn.ExceTipsFlag = b);

            //冒进提示
            dataChangedArgs.UpdateIfContains(InBoolKeys.冒进提示, b => SignalDataIn.MJTipsFlag = b);

            //禁止调车
            dataChangedArgs.UpdateIfContains(InBoolKeys.禁止调车_应答器数据包, b => SignalDataIn.SHForbidFlag = b);

            //司机室激活
            dataChangedArgs.UpdateIfContains(InBoolKeys.驾驶室激活, b => SignalDataIn.DriverActFlag = b);

            //ATP电源
            dataChangedArgs.UpdateIfContains(InBoolKeys.ATP电源, b => SignalDataIn.ATPPowerFlag = b);

            //ATP隔离
            dataChangedArgs.UpdateIfContains(InBoolKeys.ATP隔离, b => SignalDataIn.ATPBypassFlag = b);

            //公里标是否可见
            dataChangedArgs.UpdateIfContains(InBoolKeys.公里标是否可见, b => SignalDataIn.IsGLBVisible = b);

            //屏启动完成标志
            dataChangedArgs.UpdateIfContains(InBoolKeys.屏启动完成, b => SignalDataIn.ScreenStartFinishFlag = b);

            //绝对停车信息包
            dataChangedArgs.UpdateIfContains(InBoolKeys.绝对停车_应答器数据包, b => SignalDataIn.AbsoluteStopFlag = b);

            //ATP允许缓解
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_允许缓解, b => SignalDataIn.BrakeReleaseAck = b);

            //警惕确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_警惕确认, b => SignalDataIn.AlertAck = b);

            //冒进确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_冒进确认, b => SignalDataIn.MJAck = b);

            //越行确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_确认越行, b => SignalDataIn.ExceAck = b);

            //引导确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_引导确认, b => SignalDataIn.SRAck = b);

            //进入C0等级确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_进入C0等级确认, b => SignalDataIn.EnterC0Ack = b);

            //进入C2等级确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_进入C2等级确认, b => SignalDataIn.EnterC2Ack = b);

            //进入C3等级确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_进入C3等级确认, b => SignalDataIn.EnterC3Ack = b);

            //制动测试请求
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_制动测试请求, b => SignalDataIn.BrakeTestAck = b);

        }

        public virtual void OnFloatChangedImp(object sender, CommunicationDataChangedWrapperArgs<float> dataChangedArgs)
        {
            // 当前时间
            dataChangedArgs.UpdateIfContains(InFloatKeys.显示的日期,
                f => SignalDataIn.NowTime = m_DateTimeInterpreter.Interpreter(f, GetInFloatAt(InFloatKeys.显示的时间)));

            dataChangedArgs.UpdateIfContains(InFloatKeys.显示的时间,
                f => SignalDataIn.NowTime = m_DateTimeInterpreter.Interpreter(GetInFloatAt(InFloatKeys.显示的日期), f));

            //制动预警时间
            dataChangedArgs.UpdateIfContains(InFloatKeys.制动预警时间, f => SignalDataIn.BrakeWarningTime = f);

            //目标距离
            dataChangedArgs.UpdateIfContains(InFloatKeys.目标距离, f => SignalDataIn.GoalDistance = f);

            //列车公里标
            dataChangedArgs.UpdateIfContains(InFloatKeys.列车位置公里标, f => SignalDataIn.TrainGLB = f);

            //列车速度
            dataChangedArgs.UpdateIfContains(InFloatKeys.列车运行速度, f => SignalDataIn.TrainSpeed = f);
            if (SignalDataIn.TrainSpeed<0)
            {
                SignalDataIn.TrainSpeed = -1 * SignalDataIn.TrainSpeed;
            }

            //限制速度
            dataChangedArgs.UpdateIfContains(InFloatKeys.列车允许速度, f => SignalDataIn.TrainLimitSpeed = f);

            //目标速度
            dataChangedArgs.UpdateIfContains(InFloatKeys.列车目标速度, f => SignalDataIn.TrainGoalSpeed = f);

            //最大常用制动速度
            dataChangedArgs.UpdateIfContains(InFloatKeys.列车SBI速度, f => SignalDataIn.SBISpeed = f);

            //紧急制动速度
            dataChangedArgs.UpdateIfContains(InFloatKeys.列车EBI速度, f => SignalDataIn.EBISpeed = f);

            //开口速度
            dataChangedArgs.UpdateIfContains(InFloatKeys.列车开口速度, f => SignalDataIn.TrainOpeningSpeed = f);

            //控制模式
            dataChangedArgs.UpdateIfContains(InFloatKeys.控制模式, f => SignalDataIn.ControlMode = (int) f);

            //运行等级
            dataChangedArgs.UpdateIfContains(InFloatKeys.运行等级, f => SignalDataIn.RunLevel = (int) f);

            //等级预告
            dataChangedArgs.UpdateIfContains(InFloatKeys.等级转换预告, f => SignalDataIn.YGLevelInfo = (int) f);

            //机车信号
            dataChangedArgs.UpdateIfContains(InFloatKeys.机车信号, f => SignalDataIn.SignalCode = (int) f);

            //载频
            dataChangedArgs.UpdateIfContains(InFloatKeys.载频, f => SignalDataIn.Frequency = (int) f);

            //起模点位置
            dataChangedArgs.UpdateIfContains(InFloatKeys.起模点位置, f => SignalDataIn.QMPos = f);

            //起模点结束位置
            dataChangedArgs.UpdateIfContains(InFloatKeys.起模点结束位置, f => SignalDataIn.QMEndPos = f);

            //CSM TSM RSM区域
            dataChangedArgs.UpdateIfContains(InFloatKeys.当前运行区域, f => SignalDataIn.QMArea = (int) f);

            //制动状态
            dataChangedArgs.UpdateIfContains(InFloatKeys.制动状态, f => SignalDataIn.BrakeStatus = (int) f);

            //制动控制模式
            dataChangedArgs.UpdateIfContains(InFloatKeys.制动工作模式, f => SignalDataIn.BrakeMode = (int) f);

            //站台侧
            dataChangedArgs.UpdateIfContains(InFloatKeys.站台侧, f => SignalDataIn.StationSide = (int) f);

            //制动测试状态
            dataChangedArgs.UpdateIfContains(InFloatKeys.制动测试状态, f => SignalDataIn.BrakeTestStatus = (int) f);

            //GSM状态
            dataChangedArgs.UpdateIfContains(InFloatKeys.GSM连接状态, f => SignalDataIn.GSMStatus = (int) f);

            //RBC状态
            dataChangedArgs.UpdateIfContains(InFloatKeys.RBC连接状态, f => SignalDataIn.RBCStatus = (int) f);

            //RBC连接方式
            dataChangedArgs.UpdateIfContains(InFloatKeys.RBC连接方式, f => SignalDataIn.RBCLinkType = (int) f);

            //RBC允许调车状态
            dataChangedArgs.UpdateIfContains(InFloatKeys.RBC允许调车状态, f => SignalDataIn.RBCAllowSH = (int) f);

            //紧急消息
            dataChangedArgs.UpdateIfContains(InFloatKeys.紧急消息, f => SignalDataIn.EmergencyInfo = (int) f);

            //车站
            dataChangedArgs.UpdateIfContains(InFloatKeys.车站,
                f => SignalDataIn.StationNo = BitConverter.ToInt32(BitConverter.GetBytes(f), 0));

            //干预速度
            dataChangedArgs.UpdateIfContains(InFloatKeys.列车干预速度, f => SignalDataIn.IntervertionSpeed = f);

            //报警速度
            dataChangedArgs.UpdateIfContains(InFloatKeys.列车报警速度, f => SignalDataIn.WarningSpeed = f);

            //限速信息
            int BeginNum = InterfaceAdapterService.InFloatDictionary[InFloatKeys.限速速度的距离信息1];
            for (int i = InterfaceAdapterService.InFloatDictionary[InFloatKeys.限速速度的距离信息1];
                i < InterfaceAdapterService.InFloatDictionary[InFloatKeys.坡度的距离信息1];
                i++)
            {
                dataChangedArgs.UpdateIfContains(i, f => SignalDataIn.SpeedZoneInfo[i - BeginNum] = f);
            }

            //坡道信息
            BeginNum = InterfaceAdapterService.InFloatDictionary[InFloatKeys.坡度的距离信息1];
            for (int i = InterfaceAdapterService.InFloatDictionary[InFloatKeys.坡度的距离信息1];
                i < InterfaceAdapterService.InFloatDictionary[InFloatKeys.预告的距离信息1];
                i++)
            {
                dataChangedArgs.UpdateIfContains(i, f => SignalDataIn.RampInfo[i - BeginNum] = f);
            }

            //预告信息
            BeginNum = InterfaceAdapterService.InFloatDictionary[InFloatKeys.预告的距离信息1];
            for (int i = InterfaceAdapterService.InFloatDictionary[InFloatKeys.预告的距离信息1];
                i < InterfaceAdapterService.InFloatDictionary[InFloatKeys.预告信息10] + 1;
                i++)
            {
                dataChangedArgs.UpdateIfContains(i, f => SignalDataIn.YGInfo[i - BeginNum] = f);
            }

            //命令列表
            BeginNum = InterfaceAdapterService.InFloatDictionary[InFloatKeys.命令列表1];
            for (int i = InterfaceAdapterService.InFloatDictionary[InFloatKeys.命令列表1];
                i < InterfaceAdapterService.InFloatDictionary[InFloatKeys.命令列表4];
                i++)
            {
                dataChangedArgs.UpdateIfContains(i, f => SignalDataIn.OrderList[i - BeginNum] = (int) f);
            }
            //坡道信息2
            BeginNum = InterfaceAdapterService.InFloatDictionary[InFloatKeys.坡度的距离信息6];
            for (int i = InterfaceAdapterService.InFloatDictionary[InFloatKeys.坡度的距离信息6];
                i < InterfaceAdapterService.InFloatDictionary[InFloatKeys.坡度信息10] + 1;
                i++)
            {
                dataChangedArgs.UpdateIfContains(i, f => SignalDataIn.RampInfo[i - BeginNum + 10] = f);
            }
            //紧急制动类型
            dataChangedArgs.UpdateIfContains(InFloatKeys.紧急制动类型, f => SignalDataIn.EBType = (int)f);

            //系统自检状态
            dataChangedArgs.UpdateIfContains(InFloatKeys.系统自检状态, f => SignalDataIn.nSelfCheckStatus = (int)f);
        }

        public virtual void OnDataChangedImp(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {

        }

        public virtual bool FeedBackInfo(object sender, CommunicationDataChangedWrapperArgs<bool> dataChangedArgs)
        {
            //反馈_启动确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_启动确认, b =>
            {
                if (b)
                {
                    SignalDataOut.StartConfirm = false;
                }
            });

            //反馈_执行制动测试
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_执行制动测试, b =>
            {
                if (b)
                {
                    SignalDataOut.BrakeTest = false;
                }
            });

            //反馈_确认CTCS0
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_确认CTCS0, b =>
            {
                if (b)
                {
                    SignalDataOut.C0ConfirmSign = false;
                }
            });


            //反馈_确认CTCS2
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_确认CTCS2, b =>
            {
                if (b)
                {
                    SignalDataOut.C2ConfirmSign = false;
                }
            });

            //反馈_确认CTCS3
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_确认CTCS3, b =>
            {
                if (b)
                {
                    SignalDataOut.C3ConfirmSign = false;
                }
            });

            //反馈_载频上行
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_载频上行, b =>
            {
                if (b)
                {
                    SignalDataOut.FrequencyUp = false;
                }
            });

            //反馈_载频下行
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_载频下行, b =>
            {
                if (b)
                {
                    SignalDataOut.FrequencyDown = false;
                }
            });

            //反馈_允许缓解
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_允许缓解, b =>
            {
                if (b)
                {
                    SignalDataOut.ReleaseSign = false;
                }
            });

            //反馈_调车模式
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_调车模式, b =>
            {
                if (b)
                {
                    SignalDataOut.SHModeSel = false;
                }
            });

            //反馈_目视模式
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_目视模式, b =>
            {
                if (b)
                {
                    SignalDataOut.OSModeSel = false;
                }
            });

            //反馈_机信模式
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_机信模式, b =>
            {
                if (b)
                {
                    SignalDataOut.CSModeSel = false;
                }
            });

            //反馈_退出调车模式
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_退出调车模式, b =>
            {
                if (b)
                {
                    SignalDataOut.SHModeExit = false;
                }
            });

            //反馈_退出目视模式
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_退出目视模式, b =>
            {
                if (b)
                {
                    SignalDataOut.OSModeExit = false;
                }
            });

            //反馈_退出机信模式
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_退出机信模式, b =>
            {
                if (b)
                {
                    SignalDataOut.CSModeExit = false;
                }
            });

            //反馈_确认进入目视模式
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_确认进入目视模式, b =>
            {
                if (b)
                {
                    SignalDataOut.EnterOSSign = false;
                }
            });

            //反馈_警惕确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_警惕确认, b =>
            {
                if (b)
                {
                    SignalDataOut.AlertSign = false;
                }
            });

            //反馈_电话号码发送确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_电话号码发送确认, b =>
            {
                if (b)
                {
                    SignalDataOut.RBCCodeSign = false;
                }
            });

            //反馈_重新进行制动测试
            //dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_启动确认, b => { if (b) SignalDataOut.StartConfirm = false; });

            //反馈_冒进确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_冒进确认, b =>
            {
                if (b)
                {
                    SignalDataOut.MJSign = false;
                }
            });

            //反馈_确认越行
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_确认越行, b =>
            {
                if (b)
                {
                    SignalDataOut.ExeSign = false;
                }
            });
            
            //反馈_进入CTCS0级
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_进入C0等级确认, b =>
            {
                if (b)
                {
                    SignalDataOut.EnterC0Sign = false;
                }
            });

            //反馈_进入CTCS2级
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_进入CTCS2级, b =>
            {
                if (b)
                {
                    SignalDataOut.EnterC2Sign = false;
                }
            });
            //反馈_进入CTCS3级
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_进入CTCS3级, b =>
            {
                if (b)
                {
                    SignalDataOut.EnterC3Sign = false;
                }
            });

            //反馈_引导确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_引导确认, b =>
            {
                if (b)
                {
                    SignalDataOut.YDConfirmSign = false;
                }
            });

            //反馈_人工RBC连接请求
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_人工RBC连接请求, b =>
            {
                if (b)
                {
                    SignalDataOut.RBCLinkSign = false;
                }
            });

            //反馈_警惕按钮
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_警惕按钮, b =>
            {
                if (b)
                {
                    SignalDataOut.AlertBotton = false;
                }
            });

            //反馈_取消确认越性
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_取消确认越行, b =>
            {
                if (b)
                {
                    SignalDataOut.CancleExeSign = false;
                }
            });

            //反馈_确认制动测试失败
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_制动测试失败, b =>
            {
                if (b)
                {
                    SignalDataOut.BrakeTestFailSign = false;
                }
            });

            return true;

        }

        public virtual bool ScreenToSignalInfo()
        {
            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.启动确认],
                SignalDataOut.StartConfirm);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.执行制动测试],
                SignalDataOut.BrakeTest);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认CTCS0],
                SignalDataOut.C0ConfirmSign);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认CTCS2],
                SignalDataOut.C2ConfirmSign);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认CTCS3],
                SignalDataOut.C3ConfirmSign);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.载频上行],
                SignalDataOut.FrequencyUp);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.载频下行],
                SignalDataOut.FrequencyDown);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.允许缓解],
                SignalDataOut.ReleaseSign);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.调车模式],
                SignalDataOut.SHModeSel);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.目视模式],
                SignalDataOut.OSModeSel);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.机信模式],
                SignalDataOut.CSModeSel);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.退出调车模式],
                SignalDataOut.SHModeExit);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.退出目视模式],
                SignalDataOut.OSModeExit);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.退出机信模式],
                SignalDataOut.CSModeExit);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认进入目视模式],
                SignalDataOut.EnterOSSign);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.警惕确认],
                SignalDataOut.AlertSign);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.电话号码发送确认],
                SignalDataOut.RBCCodeSign);

            //DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.重新进行制动测试],
            //    SignalDataOut.);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.冒进确认],
                SignalDataOut.MJSign);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认越行],
                SignalDataOut.ExeSign);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.取消确认越行],
                SignalDataOut.CancleExeSign);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认进入C0等级],
                SignalDataOut.EnterC0Sign);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.进入CTCS2级],
                SignalDataOut.EnterC2Sign);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.进入CTCS3级],
                SignalDataOut.EnterC3Sign);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.引导确认],
                SignalDataOut.YDConfirmSign);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.人工RBC连接请求],
                SignalDataOut.RBCLinkSign);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.警惕按钮],
                SignalDataOut.AlertBotton);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认制动测试失败],
             SignalDataOut.BrakeTestFailSign);

            //float
            //DataService.WriteService.ChangeFloat(InterfaceAdapterService.OutFloatDictionary[OutFloatKeys.司机号字母],
            //    SignalDataOut.DriverCode);

            DataService.WriteService.ChangeFloat(InterfaceAdapterService.OutFloatDictionary[OutFloatKeys.司机号数字],
                SignalDataOut.DriverNum);

            //DataService.WriteService.ChangeFloat(InterfaceAdapterService.OutFloatDictionary[OutFloatKeys.车次号字母],
            //    SignalDataOut.TrainCode);

            DataService.WriteService.ChangeFloat(InterfaceAdapterService.OutFloatDictionary[OutFloatKeys.车次号数字],
                SignalDataOut.TrainNum);

            DataService.WriteService.ChangeFloat(InterfaceAdapterService.OutFloatDictionary[OutFloatKeys.列车数据],
                SignalDataOut.TrainLength);

            DataService.WriteService.ChangeFloat(InterfaceAdapterService.OutFloatDictionary[OutFloatKeys.RBC_ID],
                SignalDataOut.RBCID);

            DataService.WriteService.ChangeFloat(InterfaceAdapterService.OutFloatDictionary[OutFloatKeys.电话号码],
                SignalDataOut.RBCNum);

            DataService.WriteService.ChangeFloat(InterfaceAdapterService.OutFloatDictionary[OutFloatKeys.ATP音量大小],
                SignalDataOut.ATPVolume);

            DataService.WriteService.ChangeFloat(InterfaceAdapterService.OutFloatDictionary[OutFloatKeys.RBC_ID2],
                SignalDataOut.RBCID2);

            DataService.WriteService.ChangeFloat(InterfaceAdapterService.OutFloatDictionary[OutFloatKeys.电话号码2],
                SignalDataOut.RBCNum2);

            DataService.WriteService.ChangeFloat(InterfaceAdapterService.OutFloatDictionary[OutFloatKeys.RBC_ID3],
                SignalDataOut.RBCID3);

            DataService.WriteService.ChangeFloat(InterfaceAdapterService.OutFloatDictionary[OutFloatKeys.电话号码3],
                SignalDataOut.RBCNum3);

           

            return true;
        }

        public abstract bool BasicAndTrainInfo();

        public virtual bool ScreenSpeedInfo()
        {
            //速度指针与环形速度光带
            float currentSpeed = SignalDataIn.TrainSpeed;
            float targetSpeed = SignalDataIn.TrainGoalSpeed;
            float permittedLimitSpeed = SignalDataIn.TrainLimitSpeed;
            float serviceBrakeInterventionSpeed = SignalDataIn.SBISpeed;
            float emergencyBrakeInterventionSpeed = SignalDataIn.EBISpeed;
            ATP.TrainInfo.Speed.IntervertionSpeed.SpeedColor = ATPColor.None;

            if (SignalDataIn.ControlMode == (int) ControlType.PartialSupervision ||
                SignalDataIn.ControlMode == (int) ControlType.OnSight
                || SignalDataIn.ControlMode == (int) ControlType.Shunting 
                || SignalDataIn.ControlMode == (int) ControlType.LKJ)
            {
                if (currentSpeed <= permittedLimitSpeed) //小于允许速度
                {
                    ATP.TrainInfo.Speed.CurrentSpeed.SpeedColor = ATPColor.Grey;
                    ATP.TrainInfo.Speed.TargetSpeed.SpeedColor = ATPColor.None;
                    ATP.TrainInfo.Speed.PermittedLimitSpeed.SpeedColor = ATPColor.Grey;
                    ATP.TrainInfo.Speed.ServiceBrakeInterventionSpeed.SpeedColor = ATPColor.None;
                    ATP.TrainInfo.Speed.EmergencyBrakeInterventionSpeed.SpeedColor = ATPColor.None;
                }
                else if (currentSpeed > permittedLimitSpeed &&
                         currentSpeed <= serviceBrakeInterventionSpeed) //大于允许速度小于SBI速度
                {
                    ATP.TrainInfo.Speed.CurrentSpeed.SpeedColor = ATPColor.Orange;
                    ATP.TrainInfo.Speed.TargetSpeed.SpeedColor = ATPColor.None;
                    ATP.TrainInfo.Speed.PermittedLimitSpeed.SpeedColor = ATPColor.Grey;
                    ATP.TrainInfo.Speed.ServiceBrakeInterventionSpeed.SpeedColor = ATPColor.Orange;
                    ATP.TrainInfo.Speed.EmergencyBrakeInterventionSpeed.SpeedColor = ATPColor.None;
                }
                if (currentSpeed > serviceBrakeInterventionSpeed) //大于SBI速度
                {
                    ATP.TrainInfo.Speed.CurrentSpeed.SpeedColor = ATPColor.Red;
                    ATP.TrainInfo.Speed.TargetSpeed.SpeedColor = ATPColor.None;
                    ATP.TrainInfo.Speed.PermittedLimitSpeed.SpeedColor = ATPColor.Grey;
                    ATP.TrainInfo.Speed.ServiceBrakeInterventionSpeed.SpeedColor = ATPColor.Red;
                    ATP.TrainInfo.Speed.EmergencyBrakeInterventionSpeed.SpeedColor = ATPColor.Red;
                }
            }
            else if (SignalDataIn.ControlMode == (int) ControlType.FullSupervision ||
                     SignalDataIn.ControlMode == (int) ControlType.CallingOn)
            {
                if (SignalDataIn.QMArea == 1)
                {
                    if (currentSpeed <= permittedLimitSpeed)
                    {
                        //ATP.TrainInfo.Speed.CurrentSpeed.SpeedColor = currentSpeed > targetSpeed
                        //    ? ATPColor.Grey
                        //    : ATPColor.LightGrey;
                        ATP.TrainInfo.Speed.CurrentSpeed.SpeedColor = ATPColor.Grey;
                        ATP.TrainInfo.Speed.TargetSpeed.SpeedColor = ATPColor.LightGrey;
                        ATP.TrainInfo.Speed.PermittedLimitSpeed.SpeedColor = ATPColor.Grey;
                        ATP.TrainInfo.Speed.ServiceBrakeInterventionSpeed.SpeedColor = ATPColor.None;
                        ATP.TrainInfo.Speed.EmergencyBrakeInterventionSpeed.SpeedColor = ATPColor.None;
                    }
                    else if (currentSpeed > permittedLimitSpeed &&
                             currentSpeed <= serviceBrakeInterventionSpeed)
                    {
                        ATP.TrainInfo.Speed.CurrentSpeed.SpeedColor = ATPColor.Orange;
                        ATP.TrainInfo.Speed.TargetSpeed.SpeedColor = ATPColor.LightGrey;
                        ATP.TrainInfo.Speed.PermittedLimitSpeed.SpeedColor = ATPColor.Yellow;
                        ATP.TrainInfo.Speed.ServiceBrakeInterventionSpeed.SpeedColor = ATPColor.Orange;
                        ATP.TrainInfo.Speed.EmergencyBrakeInterventionSpeed.SpeedColor = ATPColor.None;
                    }
                    if (currentSpeed > serviceBrakeInterventionSpeed)
                    {
                        ATP.TrainInfo.Speed.CurrentSpeed.SpeedColor = ATPColor.Red;
                        ATP.TrainInfo.Speed.TargetSpeed.SpeedColor = ATPColor.LightGrey;
                        ATP.TrainInfo.Speed.PermittedLimitSpeed.SpeedColor = ATPColor.Yellow;
                        ATP.TrainInfo.Speed.ServiceBrakeInterventionSpeed.SpeedColor = ATPColor.Red;
                        ATP.TrainInfo.Speed.EmergencyBrakeInterventionSpeed.SpeedColor = ATPColor.Red;
                    }
                }
                else if (SignalDataIn.QMArea == 2)
                {
                    if (currentSpeed <= permittedLimitSpeed)
                    {
                        //ATP.TrainInfo.Speed.CurrentSpeed.SpeedColor = currentSpeed > targetSpeed
                        //    ? ATPColor.Yellow
                        //    : ATPColor.LightGrey;
                        ATP.TrainInfo.Speed.CurrentSpeed.SpeedColor = ATPColor.Grey;
                        ATP.TrainInfo.Speed.TargetSpeed.SpeedColor = ATPColor.LightGrey;
                        ATP.TrainInfo.Speed.PermittedLimitSpeed.SpeedColor = ATPColor.Yellow;
                        ATP.TrainInfo.Speed.ServiceBrakeInterventionSpeed.SpeedColor = ATPColor.None;
                        ATP.TrainInfo.Speed.EmergencyBrakeInterventionSpeed.SpeedColor = ATPColor.None;
                    }
                    else if (currentSpeed > permittedLimitSpeed &&
                             currentSpeed <= serviceBrakeInterventionSpeed)
                    {
                        ATP.TrainInfo.Speed.CurrentSpeed.SpeedColor = ATPColor.Orange;
                        ATP.TrainInfo.Speed.TargetSpeed.SpeedColor = ATPColor.LightGrey;
                        ATP.TrainInfo.Speed.PermittedLimitSpeed.SpeedColor = ATPColor.Yellow;
                        ATP.TrainInfo.Speed.ServiceBrakeInterventionSpeed.SpeedColor = ATPColor.Orange;
                        ATP.TrainInfo.Speed.EmergencyBrakeInterventionSpeed.SpeedColor = ATPColor.None;
                    }
                    if (currentSpeed > serviceBrakeInterventionSpeed)
                    {
                        ATP.TrainInfo.Speed.CurrentSpeed.SpeedColor = ATPColor.Red;
                        ATP.TrainInfo.Speed.TargetSpeed.SpeedColor = ATPColor.LightGrey;
                        ATP.TrainInfo.Speed.PermittedLimitSpeed.SpeedColor = ATPColor.Yellow;
                        ATP.TrainInfo.Speed.ServiceBrakeInterventionSpeed.SpeedColor = ATPColor.Red;
                        ATP.TrainInfo.Speed.EmergencyBrakeInterventionSpeed.SpeedColor = ATPColor.Red;
                    }
                }
                if (targetSpeed >= permittedLimitSpeed)
                {
                    ATP.TrainInfo.Speed.TargetSpeed.SpeedColor = ATPColor.None;
                }
            }
            else
            {
                ATP.TrainInfo.Speed.CurrentSpeed.SpeedColor = ATPColor.Grey;
                ATP.TrainInfo.Speed.TargetSpeed.SpeedColor = ATPColor.None;
                ATP.TrainInfo.Speed.PermittedLimitSpeed.SpeedColor = ATPColor.None;
                ATP.TrainInfo.Speed.ServiceBrakeInterventionSpeed.SpeedColor = ATPColor.None;
                ATP.TrainInfo.Speed.EmergencyBrakeInterventionSpeed.SpeedColor = ATPColor.None;
            }

            //命令列表

            return true;
        }

        public virtual bool ScreenDistanceInfo()
        {

            ATP.WarningIntervention.WarningTime = SignalDataIn.BrakeWarningTime;
            ATP.WarningIntervention.TargetDistance = SignalDataIn.GoalDistance;

            if ((SignalDataIn.ControlMode != (int) ControlType.FullSupervision) &&
                (SignalDataIn.ControlMode != (int) ControlType.CallingOn))
            {
                ATP.WarningIntervention.TargetDistanceVisible = false;
                ATP.WarningIntervention.BrakeWarningVisible = false;
                ATP.WarningIntervention.BrakeWarningLevel = BrakeWarningLevel.Level0;
                ATP.WarningIntervention.BrakeWaringColor = ATPColor.None;
                return true;
            }
            else
            {
                if (SignalDataIn.QMArea == 2)
                {
                    //TSM区显示目标距离
                    ATP.WarningIntervention.TargetDistanceVisible = true;
                    ATP.WarningIntervention.BrakeWarningVisible = true;
                    //预警图标大小
                    if (SignalDataIn.BrakeWarningTime >= 8)
                    {
                        ATP.WarningIntervention.BrakeWarningLevel = BrakeWarningLevel.Level1;
                    }
                    else if (SignalDataIn.BrakeWarningTime < 8 && SignalDataIn.BrakeWarningTime > 4)
                    {
                        ATP.WarningIntervention.BrakeWarningLevel = BrakeWarningLevel.Level2;
                    }
                    else if (SignalDataIn.BrakeWarningTime <= 4 && SignalDataIn.BrakeWarningTime > 0)
                    {
                        ATP.WarningIntervention.BrakeWarningLevel = BrakeWarningLevel.Level3;
                    }
                    else if (SignalDataIn.BrakeWarningTime <= 0)
                    {
                        ATP.WarningIntervention.BrakeWarningLevel = BrakeWarningLevel.LevelFull;
                    }
                    //预警图标颜色
                    if (SignalDataIn.TrainSpeed > SignalDataIn.SBISpeed)
                    {
                        ATP.WarningIntervention.BrakeWaringColor = ATPColor.Red;
                    }
                    else if (SignalDataIn.TrainSpeed > SignalDataIn.TrainLimitSpeed)
                    {
                        ATP.WarningIntervention.BrakeWaringColor = ATPColor.Orange;
                    }
                    else
                    {
                        ATP.WarningIntervention.BrakeWaringColor = ATPColor.Yellow;
                    }
                }
                else if (SignalDataIn.QMArea == 1)
                {
                    //CSM区不显示目标距离
                    ATP.WarningIntervention.TargetDistanceVisible = false;
                    ATP.WarningIntervention.BrakeWarningVisible = SignalDataIn.BrakeWarningTime < 8;
                    //预警图标大小
                    if (SignalDataIn.BrakeWarningTime > 8)
                    {
                        ATP.WarningIntervention.BrakeWarningLevel = BrakeWarningLevel.Level0;
                    }
                    if (SignalDataIn.BrakeWarningTime == 8)
                    {
                        ATP.WarningIntervention.BrakeWarningLevel = BrakeWarningLevel.Level1;
                    }
                    else if (SignalDataIn.BrakeWarningTime < 8 && SignalDataIn.BrakeWarningTime > 4)
                    {
                        ATP.WarningIntervention.BrakeWarningLevel = BrakeWarningLevel.Level2;
                    }
                    else if (SignalDataIn.BrakeWarningTime <= 4 && SignalDataIn.BrakeWarningTime > 0)
                    {
                        ATP.WarningIntervention.BrakeWarningLevel = BrakeWarningLevel.Level3;
                    }
                    else if (SignalDataIn.BrakeWarningTime <= 0)
                    {
                        ATP.WarningIntervention.BrakeWarningLevel = BrakeWarningLevel.LevelFull;
                    }
                    //预警图标颜色
                    if (SignalDataIn.TrainSpeed > SignalDataIn.SBISpeed)
                    {
                        ATP.WarningIntervention.BrakeWaringColor = ATPColor.Red;
                    }
                    else if (SignalDataIn.TrainSpeed > SignalDataIn.TrainLimitSpeed)
                    {
                        ATP.WarningIntervention.BrakeWaringColor = ATPColor.Orange;
                    }
                    else
                    {
                        ATP.WarningIntervention.BrakeWaringColor = ATPColor.Grey;
                    }
                }
                else
                {
                    //不显示目标距离
                    ATP.WarningIntervention.TargetDistanceVisible = false;
                    ATP.WarningIntervention.BrakeWarningVisible = false;
                    ATP.WarningIntervention.BrakeWarningLevel = BrakeWarningLevel.Level0;
                    ATP.WarningIntervention.BrakeWaringColor = ATPColor.None;
                    return false;
                }
            }

            return true;
        }

        public virtual bool ScreenPlanInfo()
        {
            //站台显示
            ATP.TrainInfo.Station.CurrentOpenDoorLocation = (OpenDoorLocation) SignalDataIn.StationSide;
            // 预告信息
            UpdateForecastInformation();
            if ((SignalDataIn.ControlMode != (int) ControlType.FullSupervision) &&
                (SignalDataIn.ControlMode != (int) ControlType.CallingOn) &&
                (SignalDataIn.ControlMode != (int) ControlType.PartialSupervision) &&
                (SignalDataIn.ControlMode != (int) ControlType.OnSight) &&
                (SignalDataIn.ControlMode != (int) ControlType.Shunting) &&
                (SignalDataIn.ControlMode != (int) ControlType.LKJ) &&
                (SignalDataIn.ControlMode != (int) ControlType.Trip) &&
                (SignalDataIn.ControlMode != (int) ControlType.PostTrip))
            {
                ATP.SpeedMonitoringSection.Visible = false;
                return false;
            }

            if ((SignalDataIn.ControlMode == (int) ControlType.PartialSupervision) ||
                (SignalDataIn.ControlMode == (int) ControlType.OnSight) ||
                (SignalDataIn.ControlMode == (int) ControlType.Shunting) ||
                (SignalDataIn.ControlMode == (int) ControlType.LKJ) ||
                (SignalDataIn.ControlMode == (int) ControlType.Trip) ||
                (SignalDataIn.ControlMode == (int) ControlType.PostTrip)
                || SignalDataIn.DriverActFlag)
            {
                ATP.SpeedMonitoringSection.Type = SpeedMonitoringSectionType.None;
                ATP.SpeedMonitoringSection.BrakingStartPoint = 0;
                //Array.Clear(SignalDataIn.YGInfo, 0, SignalDataIn.YGInfo.Length);
                //Array.Clear(SignalDataIn.SpeedZoneInfo, 0, SignalDataIn.SpeedZoneInfo.Length);
                //Array.Clear(SignalDataIn.RampInfo, 0, SignalDataIn.RampInfo.Length);
                //Array.Clear(SignalDataIn.OrderList, 0, SignalDataIn.OrderList.Length);
            }
            else
            {
                ATP.SpeedMonitoringSection.Type = (SpeedMonitoringSectionType) SignalDataIn.QMArea;
                ATP.SpeedMonitoringSection.BrakingStartPoint = SignalDataIn.QMPos;
            }
            ATP.SpeedMonitoringSection.Visible = SignalDataIn.StartFinishFlag;

            //计划区MRSP曲线绘制尚不清楚屏需要的数据结构，暂定 亟待完善 暂先把限速直接赋值过来
            //ATP.SpeedMonitoringSection.SpeedCurve.CurvePointCollection.Clear();

            m_PlanzoneDataBuffer.Clear();

            UpdatePlanzoneBuffer();

            ATP.SpeedMonitoringSection.SpeedCurve.CurvePointCollection.Clear();
            ATP.SpeedMonitoringSection.SpeedCurve.CurvePointCollection.AddRange(m_PlanzoneDataBuffer);
            ATP.SpeedMonitoringSection.SpeedCurve.RaiseCurvePointCollectionChanged();








            //坡道信息

            UpdateGradientInfomation();

            //降速点信息，依据MRSP曲线确定
            UpdateDecelerateInfos();

            return true;
        }

        public virtual void UpdateForecastInformation()
        {
            //预告信息
            for (int i = 0; i < 10; i++)
            {
                ATP.ForecastInformation.ForecastInformationItems[i].Update(
                    (ForecastInformationType) SignalDataIn.YGInfo[i*2 + 1], SignalDataIn.YGInfo[i*2]);
            }
            // 命令
            for (int i = 0; i < 3; i++)
            {
                ATP.ForecastInformation.ForecastInformationItems[10 + i].Update(
                    (ForecastInformationType) SignalDataIn.OrderList[i], 0);
            }
        }


        /// <summary>
        /// 更新坡度信息
        /// </summary>
        public virtual void UpdateGradientInfomation()
        {
            float StartPos, EndPos;
            StartPos = EndPos = 0;
            bool EndFlag = false;
            var idx = 0;
            for (int i = 0; i < 10; i++)
            {
                var slopeInfo = ATP.GradientInfomation.GradientInfomationItemItems[idx++];
                StartPos = EndPos;
                EndPos = SignalDataIn.RampInfo[2*i];
                if (SignalDataIn.RampInfo[2*i + 1] > 0)
                {
                    slopeInfo.Type = GradientType.Upslope;
                }
                else if (SignalDataIn.RampInfo[2*i + 1] < 0)
                {
                    slopeInfo.Type = GradientType.Downslope;
                }
                else
                {
                    slopeInfo.Type = GradientType.None;
                }

                slopeInfo.StartDistance = StartPos;
                slopeInfo.EndDistance = EndPos;

                if (ATP.SpeedMonitoringSection.SpeedCurve.CurvePointCollection.Count > 0
                    &&
                    slopeInfo.EndDistance >=
                    ATP.SpeedMonitoringSection.SpeedCurve.CurvePointCollection.Max(Dsp => Dsp.Distance))
                {
                    slopeInfo.EndDistance =
                        ATP.SpeedMonitoringSection.SpeedCurve.CurvePointCollection.Max(Dsp => Dsp.Distance);
                    EndFlag = true;
                }
                slopeInfo.SlopeValue = SignalDataIn.RampInfo[2*i + 1];
                if (EndFlag)
                {
                    break;
                }
            }
            for (; idx < ATP.GradientInfomation.GradientInfomationItemItems.Count; ++idx)
            {
                ATP.GradientInfomation.GradientInfomationItemItems[idx].Type = GradientType.None;
                ATP.GradientInfomation.GradientInfomationItemItems[idx].StartDistance = 0;
                ATP.GradientInfomation.GradientInfomationItemItems[idx].EndDistance = 0;
            }
        }

        public virtual void UpdatePlanzoneBuffer()
        {
            for (int i = 0; i < 10; i++)
            {
                DistanceSpeedPoint dspInfo = new DistanceSpeedPoint();
                DistanceSpeedPoint dspInfo1 = new DistanceSpeedPoint();
                dspInfo.Distance = SignalDataIn.SpeedZoneInfo[i*2];
                dspInfo.Speed = SignalDataIn.SpeedZoneInfo[i*2 + 1];
                if (dspInfo.Distance > 0 || dspInfo.Speed > 0)
                {
                        if (0 == i)
                        {
                            m_PlanzoneDataBuffer.Add(dspInfo);
                        }
                        else
                        {
                            if (SignalDataIn.QMPos > SignalDataIn.SpeedZoneInfo[(i - 1)*2] &&
                                SignalDataIn.QMPos <= SignalDataIn.SpeedZoneInfo[i*2])
                            {
                                dspInfo1.Distance = SignalDataIn.QMPos;
                                dspInfo1.Speed = SignalDataIn.SpeedZoneInfo[(i - 1)*2 + 1];
                                m_PlanzoneDataBuffer.Add(dspInfo1);
                                m_PlanzoneDataBuffer.Add(dspInfo);
                          
                            }
                            else if (SignalDataIn.QMPos < SignalDataIn.SpeedZoneInfo[i * 2] && SignalDataIn.QMEndPos>i)//解决起模点和起模结束点之间多个限速问题
                            {
                            }
                            else if(SignalDataIn.QMEndPos == i)//解决SignalDataIn.QMPos == SignalDataIn.SpeedZoneInfo[i*2]时多赋值问题
                        {
                                m_PlanzoneDataBuffer.Add(dspInfo);
                            }
                            else
                            {
                                dspInfo1.Distance = SignalDataIn.SpeedZoneInfo[i*2];
                                dspInfo1.Speed = SignalDataIn.SpeedZoneInfo[(i - 1)*2 + 1];
                                m_PlanzoneDataBuffer.Add(dspInfo1);
                                m_PlanzoneDataBuffer.Add(dspInfo);
                            }
                        }
                    }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 更新速度变化信息
        /// </summary>
        public virtual void UpdateDecelerateInfos()
        {
            int idx = 0;
            for (int i = 0; i < ATP.SpeedMonitoringSection.SpeedCurve.CurvePointCollection.Count; i++)
            {
                if (ATP.SpeedMonitoringSection.SpeedCurve.CurvePointCollection[i].Distance >= 0)
                {
                    var SCInfo = ATP.ForecastInformation.DecelerateInfos[idx];
                    if (i == 0)
                    {
                        continue;
                    }
                    else if (ATP.SpeedMonitoringSection.SpeedCurve.CurvePointCollection[i].Speed >
                             ATP.SpeedMonitoringSection.SpeedCurve.CurvePointCollection[i - 1].Speed)
                    {
                        SCInfo.SpeedChangeType = SpeedChangeType.Acceleration;
                        SCInfo.Distance = ATP.SpeedMonitoringSection.SpeedCurve.CurvePointCollection[i].Distance;
                    }
                    else if (ATP.SpeedMonitoringSection.SpeedCurve.CurvePointCollection[i].Speed <
                             ATP.SpeedMonitoringSection.SpeedCurve.CurvePointCollection[i - 1].Speed)
                    {
                        if (ATP.SpeedMonitoringSection.SpeedCurve.CurvePointCollection[i].Speed > 0)
                        {
                            SCInfo.SpeedChangeType = SpeedChangeType.Decelerate;
                        }
                        else
                        {
                            SCInfo.SpeedChangeType = SpeedChangeType.DecelerateToZero;
                        }
                        SCInfo.Distance = ATP.SpeedMonitoringSection.SpeedCurve.CurvePointCollection[i].Distance;
                    }
                    else
                    {
                        SCInfo.SpeedChangeType = SpeedChangeType.None;
                    }

                    if (SCInfo.SpeedChangeType != SpeedChangeType.None)
                    {
                        ++idx;
                    }
                }
            }

            for (; idx < ATP.ForecastInformation.DecelerateInfos.Count; ++idx)
            {
                ATP.ForecastInformation.DecelerateInfos[idx].SpeedChangeType = SpeedChangeType.None;
            }
        }

        public abstract bool ScreenBtnEnable();
        public abstract bool ScreenTextInfo();
        public abstract bool ScreenControl();

        public abstract bool ClearInfos();


        public virtual void ClearDataOnCourseStop()
        {
            ATP.Other.ShowingTimeDifference = TimeSpan.Zero;

            ATP.GetInterfaceController().UpdateDriverInterface(DriverInterfaceKey.Root);

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
            App.Current.GetMainDispatcher()
                .Invoke(m_UpdateBoolAction, this, sender,
                    new CommunicationDataChangedWrapperArgs<bool>(dataChangedArgs, ATP, InterfaceAdapterService));
        }

        /// <summary>float值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            App.Current.GetMainDispatcher()
                .Invoke(m_UpdateFloatAction, this, sender,
                    new CommunicationDataChangedWrapperArgs<float>(dataChangedArgs, ATP, InterfaceAdapterService));
        }

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
            App.Current.GetMainDispatcher().Invoke(m_UpdateDataAction, this, sender, dataChangedArgs);
        }

        public string GetStationName(ulong stationNo)
        {
            StationConfig item;
            return StationConfigDictionary.TryGetValue(stationNo, out item) ? item.Staion : string.Empty;
        }
    }
}
