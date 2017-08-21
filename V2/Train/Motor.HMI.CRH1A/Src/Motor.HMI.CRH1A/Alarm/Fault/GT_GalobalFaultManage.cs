using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using CommonUtil.Util;
using Excel.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH1A.Alarm.FaultPopupView;
using Motor.HMI.CRH1A.Alarm.VigilantFault;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Config;
using Motor.HMI.CRH1A.Common.Fault;
using Motor.HMI.CRH1A.Common.Global;
using Motor.HMI.CRH1A.Train;

namespace Motor.HMI.CRH1A.Alarm.Fault
{
    /// <summary>
    /// 功能描述 GT_MainAeroB类 
    ///     故障信息全局管理方法
    /// 创建人：袁 凯    
    /// 创建时间：2013-12-23
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class GT_GalobalFaultManage : CRH1BaseClass
    {
        #region 私有字段

        private readonly FaultQueue m_FaultQueue;

        #endregion

        #region 公共静态字段

        /// <summary>
        /// 故障所属单元号以及对应的单元名称字典
        /// </summary>
        public SortedList<int, string> FaultUinitDictionary { private set; get; }


        /// <summary>
        /// 单例
        /// </summary>
        public static GT_GalobalFaultManage Instance { private set; get; }

        private readonly List<ExceptionData> m_TotalEvnetList;

        /// <summary>
        /// 能否响应A类故障
        /// </summary>
        public bool CanResponseFaultA { set; get; }

        public bool CanResponsePopupFault { set; get; }

        public GT_GalobalFaultManage()
        {
            m_FaultQueue = new FaultQueue();
            FaultUinitDictionary = new SortedList<int, string>();
            CanResponseFaultA = false;
            CanResponsePopupFault = true;
            m_TotalEvnetList = new List<ExceptionData>();
        }

        #endregion

        /// <summary>
        /// 是否有故障没有确认
        /// </summary>
        /// <returns></returns>
        public bool HasFault()
        {
            return m_FaultQueue.Any();
        }

        /// <summary>
        /// 有没有弹黑屏的故障
        /// </summary>
        public bool HasPupupFault
        {
            get
            {
                var falut = m_FaultQueue.PeekFault();
                if (falut == null)
                {
                    return false;
                }
                return falut.ExType == FaultType.Warning || falut.ExType == FaultType.Info;
            }
        }

        /// <summary>
        /// 是否有手动故障
        /// </summary>
        /// <returns></returns>
        public bool HasManaulFault()
        {
            return m_FaultQueue.AllManualFault.Any();
        }

        /// <summary>
        /// 是否有已确认的,但没有解决的故障
        /// </summary>
        /// <returns></returns>
        public bool HasSuredActiveFault()
        {
            return m_FaultQueue.AllSuredActiveFault.Count != 0;
        }

        public ReadOnlyCollection<ExceptionData> FaultHistory
        {
            get { return m_FaultQueue.FaultHistory; }
        }

        public ReadOnlyCollection<ExceptionData> ManaulFault { get { return m_FaultQueue.AllManualFault; } }

        /// <summary>
        /// 所有的没有处理的故障
        /// </summary>
        public ReadOnlyCollection<ExceptionData> AllFaults
        {
            get { return m_FaultQueue.GetAllFault(); }
        }

        /// <summary>
        /// 所有已确认的, 没有解决的 故障
        /// </summary>
        public ReadOnlyCollection<ExceptionData> AllActiveFaults
        {
            get { return m_FaultQueue.AllSuredActiveFault; }
        }

        /// <summary>
        /// 获取图元对象名称
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "故障管理类";
        }


        /// <summary>
        /// 图元初始化过程执行过程 (只在初始化过程执行一次)
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool Initialize()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            LoadFaultLogicInfo();

            // LoadFaultHelpInfo();

            LoadFaultUnitInfo();

            EnqueueManualFault();

            UIObj.InBoolList.AddRange(m_TotalEvnetList.Select(s => s.ExLogNo));

            return true;
        }

        private void EnqueueManualFault()
        {
            var faults = GlobalInfo.Instance.CRH1ADetailConfig.DefaultFaultLogicCollection.Select(GetCloneFault).Where(w => w != null).ToList();
            foreach (var notExist in GlobalInfo.Instance.CRH1ADetailConfig.DefaultFaultLogicCollection.Except(faults.Select(s => s.ExLogNo)))
            {
                LogMgr.Warn(string.Format("Can not found fault of Logic index = {0} when EnqueueManualFault", notExist));
            }
            faults.ForEach(e => e.SenderType = FaultSenderType.Manaul);
            m_FaultQueue.EnqueueManualFault(faults);
        }

        private void LoadFaultUnitInfo()
        {

            var s = Enum.GetValues(typeof(FaultEnum.FaultUnit))
                .Cast<FaultEnum.FaultUnit>()
                .ToDictionary(t => (int)t, t => EnumUtil.GetDescription(t).First());

            FaultUinitDictionary = new SortedList<int, string>(s);

        }

        static void LoadFaultHelpInfo(string line, int index)
        {
            var str = line.Split(';');
            if (str.Length == 3)//故障帮助信息)
            {
                if (!EventStatic.HelpInfos.ContainsKey(index))
                {
                    var helpInfo = new FaultHelpInfo
                    {
                        HelpDescription = str[0].Replace("#", "\n"),
                        HelpBackground = str[1].Replace("#", "\n"),
                        HelpXianXiang = str[2].Replace("#", "\n")
                    };
                    EventStatic.HelpInfos.Add(index, helpInfo);
                }
            }
        }


        static int FinValue(string name, FaultEnum.FaultUnit[] faut)
        {
            var value = -1;
            for (int i = 0; i < faut.Count(); i++)
            {
                if (EnumUtil.GetDescription(faut[i]).FirstOrDefault().Equals(name))
                {
                    value = (int)faut[i];
                    break;
                }
            }
            return value;
        }
        private void LoadFaultLogicInfo()
        {
            var excel = Path.Combine(RecPath, @"..\config\FaultInfo.xls");
            var config = new ExcelReaderConfig()
            {
                File = excel,
                Coloumns = new List<ColoumnConfig>() { new ColoumnConfig() { Name = "*", IsPrimaryKey = false } },
                SheetNames = new List<string>() { "Fault" }
            };
            var dt = config.Adapter();
            var type =
                Enum.GetValues(typeof(FaultEnum.FaultUnit))
                    .Cast<FaultEnum.FaultUnit>()
                    .ToArray();
            int index = 0;
            foreach (DataRow row in dt.Tables[0].Rows)
            {
                if (IsInvalid(row)) continue;
                var exData = new ExceptionData();
                exData.ExLogNo = Convert.ToInt32(row[0].ToString());
                exData.ExUnit = FinValue(row[1].ToString(), type);
                exData.ExCarId = Convert.ToInt32(row[2]);
                if (exData.ExCarId < 4)
                {
                    exData.ExCarUnit = 1; //1 2 3号车厢为1单元
                }
                else if (exData.ExCarId == 4 || exData.ExCarId == 5)
                {
                    exData.ExCarUnit = 2; // 4 5号车厢为2单元
                }
                else
                {
                    exData.ExCarUnit = 3; // 6 7 8号车厢为3单元
                }
                exData.ExId = Convert.ToInt32(row[3]);
                exData.ExText = row[4].ToString();
                exData.ExType = (FaultType)Convert.ToInt32(row[5]);
                exData.ExSuggestId = index;
                if (!m_TotalEvnetList.Contains(exData, new ExceptionDataLogicCompare()))
                {
                    m_TotalEvnetList.Add(exData);
                }
                LoadFaultHelpInfo(row[6].ToString(), index);
                index++;
            }


        }
        /// <summary>
        /// 判断该行是否有效
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private static bool IsInvalid(DataRow row)
        {
            if (string.IsNullOrEmpty(row[0].ToString())
                || string.IsNullOrEmpty(row[1].ToString())
                || string.IsNullOrEmpty(row[2].ToString())
                || string.IsNullOrEmpty(row[3].ToString())
                || string.IsNullOrEmpty(row[4].ToString())
                || string.IsNullOrEmpty(row[5].ToString()))
                //|| //Convert.ToInt32(row[3]) == -1 )//|| row[1].ToString().Equals("无效"))
            {
                return true;
            }
            return false;
        }

        protected override void NavigateFrom(ViewConfig from)
        {
            if (from == ViewConfig.Start)
            {
                return;
            }

            Refresh();
        }

        protected override void NavigateInCurrent(ViewConfig current)
        {
            Refresh();
        }

        public void Refresh()
        {
            RefreshActiveFaults();
            //RefreshUnConfigActivalFault();
            //RefreshAfaultInfo();
            //RefreshUnConfigAfaults();
            ManageFault();
        }

        #region 私有方法
        /// <summary>
        /// 刷新活动故障集合
        /// </summary>
        private void RefreshActiveFaults()
        {
            foreach (var data in m_TotalEvnetList)
            {
                // 加入新的
                if (BoolList[data.ExLogNo])
                {
                    AddNewFault(data);
                }
                else
                {
                    // 解决老的
                    FixFault(data);
                }
            }

        }

        private void FixFault(ExceptionData data)
        {
            if (m_FaultQueue.Contains(data))
            {
                //m_FaultQueue.FixFault(m_FaultQueue.FindByLogicNo(data.ExLogNo));
                m_FaultQueue.FindByLogicNo(data.ExLogNo).IsFixed = true;
            }
        }

        private ExceptionData GetCloneFault(int logicId)
        {
            var data = m_TotalEvnetList.FirstOrDefault(f => f.ExLogNo == logicId);
            if (data != null)
            {
                data = DeepCopy.Copy(data);
                data.Startdate = CurrenTime;
            }
            return data;
        }

        private void AddNewFault(ExceptionData data)
        {
            if (!m_FaultQueue.Contains(data))
            {
                var newEx = DeepCopy.Copy(data);
                newEx.Startdate = CurrenTime;
                m_FaultQueue.EnqueueFault(newEx);
                newEx.StateChanged += OnFaultDataStateChanged;
            }
        }

        private void OnFaultDataStateChanged(object sender, EventArgs args)
        {
            var sData = sender as ExceptionData;
            Debug.Assert(sData != null, "sData != null");
            switch (sData.State)
            {
                case FaultState.New:
                    break;
                case FaultState.Sure:
                    m_FaultQueue.SureFault();
                    break;
                case FaultState.Fixed:
                    m_FaultQueue.FixFault(sData);
                    sData.StateChanged = null;
                    break;
                case FaultState.SureAndFixed:
                    m_FaultQueue.FixFault(sData);
                    sData.StateChanged = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 针对当前活动故障进行管理
        /// </summary>
        private void ManageFault()
        {
            var cur = m_FaultQueue.PeekFault();

            Train3.SelectedIndex = -1;
            VigilantFaultView.Instance.ClearCurrentFault();

            GT_AlarmInfo.CurrentShowExceptionData = null;
            UserDefineFaultView.Instance.CurrentFault = null;

            if (cur != null)
            {
                switch (cur.ExType)
                {

                    case FaultType.Passenger:
                        break;

                    case FaultType.OperError:
                        ChangeToVigilantFaultPage(cur, Brushes.Black);
                        break;
                    case FaultType.A:
                        ChangeToVigilantFaultPage(cur, Brushes.Red);
                        break;
                    case FaultType.B:
                        GT_AlarmInfo.CurrentShowExceptionData = cur;
                        break;
                    case FaultType.E:
                        GT_AlarmInfo.CurrentShowExceptionData = cur;
                        break;
                    case FaultType.Manaul:
                        GT_AlarmInfo.CurrentShowExceptionData = cur;
                        break;
                    case FaultType.Event:
                        GT_AlarmInfo.CurrentShowExceptionData = cur;
                        break;
                    case FaultType.Warning:

                    case FaultType.Info:
                        GT_FaultPopupView.Instance.CurrntExceptionData = cur;
                        if (CanResponsePopupFault)
                        {
                            OnPost(CmdType.ChangePage, (int)ViewConfig.FaultPopup, 0, 0);
                        }
                        break;
                    case FaultType.UserDefinedSystemSleep:
                        UserDefineFaultView.Instance.CurrentFault = cur;
                        break;
                    case FaultType.UserDefinedSystemHalted:
                        UserDefineFaultView.Instance.CurrentFault = cur;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void ChangeToVigilantFaultPage(ExceptionData cur, Brush brush)
        {
            VigilantFaultView.Instance.SetCurrentShowFault(cur);
            //列车状态显示故障所在车厢位置
            Train3.SelectedIndex = cur.ExCarId - 1;
            Train3.SelectedBrush = brush as SolidBrush;
            if (CanResponseFaultA)
            {
                OnPost(CmdType.ChangePage, 36, 0, 0);
            }
        }

        #endregion
    }
}
