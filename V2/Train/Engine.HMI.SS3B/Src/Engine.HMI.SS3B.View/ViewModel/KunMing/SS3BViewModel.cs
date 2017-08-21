using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using CommonUtil.Util;
using Engine.HMI.SS3B.Interface;
using Engine.HMI.SS3B.Interface.ViewState;
using Engine.HMI.SS3B.View.Config;
using Engine.HMI.SS3B.View.Constance;
using Engine.HMI.SS3B.View.Model;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace Engine.HMI.SS3B.View.ViewModel.KunMing
{
    [Export]
    public class SS3BViewModel : NotificationObject, ISS3BViewModel
    {
        private TitleViewModel m_Title;
        private MasterViewModel m_MasterViewModel;
        private NetPageViewModel m_NetPageViewModel;
        private AssistSysytemPageViewModel m_AssistSysytemPageViewModel;
        private StatusViewModel m_StatusViewModel;
        private MainCircuitPageViewModel m_MainCircuitPageViewModel;
        private AxleTemperatureViewModel m_AxleTemperatureViewModel;
        private Visibility m_MMIBlack;
        private MessageMgr m_MessageMgr;
        private List<MessageInfo> m_CurrentMessageInfo;
        private int m_CurrentFault;
        private int m_CurrentPage;
        private int m_AllPage;
        private IRegionManager m_RegiionManager;
        private IEventAggregator m_EventAggregator;
        public Dictionary<int, string> AllCommandInfo { get; private set; }
        [ImportingConstructor]
        public SS3BViewModel(IRegionManager regionManger, IEventAggregator eventer)
        {
            m_RegiionManager = regionManger;
            m_EventAggregator = eventer;
            Title = new TitleViewModel();
            MasterViewModel = new MasterViewModel();
            NetPageViewModel = new NetPageViewModel();
            AssistSysytemPageViewModel = new AssistSysytemPageViewModel();
            StatusViewModel = new StatusViewModel();
            MainCircuitPageViewModel = new MainCircuitPageViewModel();
            AxleTemperatureViewModel = new AxleTemperatureViewModel();
            PasswordInputViewModel = new PasswordInputViewModel();
            MessageMgr = new MessageMgr(15);
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\message.txt");
            MessageMgr.LoadFile(file);
            MMIBlack = Visibility.Hidden;
            AllCommandInfo =
                File.ReadAllLines(Path.Combine(GlobalParam.Instance.InitParam.AppConfig.AppPaths.ConfigDirectory, "命令文本显示.txt"),
                    Encoding.Default)
                    .Where(w => !w.StartsWith(";;;;"))
                    .Select(s => s.Split(';'))
                    .Where(w => w.Length == 2)
                    .ToDictionary(t => Convert.ToInt32(t[0]), t => t[1]);
            CheckInfos = MessageMgr.GetCheckModelFault();
        }

        public List<MessageInfo> CurrentMessageInfo
        {
            get { return m_CurrentMessageInfo; }
            set
            {
                if (Equals(value, m_CurrentMessageInfo))
                {
                    return;
                }
                m_CurrentMessageInfo = value;
                RaisePropertyChanged(() => CurrentMessageInfo);
            }
        }
        /// <summary>
        /// 当前故障数
        /// </summary>
        public int CurrentFault
        {
            get { return m_CurrentFault; }
            set
            {
                if (value == m_CurrentFault)
                {
                    return;
                }
                m_CurrentFault = value;
                RaisePropertyChanged(() => CurrentFault);
            }
        }
        /// <summary>
        /// 故障名称
        /// </summary>
        public string FaultName
        {
            get { return m_FaultName; }
            set
            {
                if (value == m_FaultName)
                {
                    return;
                }
                m_FaultName = value;
                RaisePropertyChanged(() => FaultName);
            }
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage
        {
            get { return m_CurrentPage; }
            set
            {
                if (value == m_CurrentPage)
                {
                    return;
                }
                m_CurrentPage = value;
                RaisePropertyChanged(() => CurrentPage);
            }
        }
        /// <summary>
        /// 所有页
        /// </summary>
        public int AllPage
        {
            get { return m_AllPage; }
            set
            {
                if (value == m_AllPage)
                {
                    return;
                }
                m_AllPage = value;
                RaisePropertyChanged(() => AllPage);
            }
        }

        public IList<MessageInfo> Infos
        {
            get { return m_Infos; }
            set
            {
                if (Equals(value, m_Infos))
                {
                    return;
                }
                m_Infos = value;
                RaisePropertyChanged(() => Infos);
            }
        }

        /// <summary>
        /// 故障管理类
        /// </summary>
        public MessageMgr MessageMgr
        {
            get { return m_MessageMgr; }
            set
            {
                if (Equals(value, m_MessageMgr))
                {
                    return;
                }
                m_MessageMgr = value;
                m_MessageMgr.FaultChanged += m_MessageMgr_FaultChanged;
                RaisePropertyChanged(() => MessageMgr);
            }
        }

        void m_MessageMgr_FaultChanged()
        {
            CurrentFault = MessageMgr.CurrentInfo.Count;
            CurrentPage = MessageMgr.CurrentPage;
            AllPage = MessageMgr.MaxPage;
            FaultName = MessageMgr.GetCurrent().Count != 0 ? MessageMgr.GetCurrent()[0].Content : "";
            var newInfo = MessageMgr.GetNewInfo();
            if (newInfo != null)
            {
                FaultInfoStr = newInfo.Level + " " + newInfo.Content;
            }
        }
        /// <summary>
        /// 黑屏标志
        /// </summary>
        public Visibility MMIBlack
        {
            get { return m_MMIBlack; }
            set
            {
                if (value == m_MMIBlack)
                {
                    return;
                }
                m_MMIBlack = value;
                RaisePropertyChanged(() => MMIBlack);
            }
        }

        public PasswordInputViewModel PasswordInputViewModel
        {
            get { return m_PasswordInputViewModel; }
            private set
            {
                if (Equals(value, m_PasswordInputViewModel))
                {
                    return;
                }
                m_PasswordInputViewModel = value;
                RaisePropertyChanged(() => PasswordInputViewModel);
            }
        }

        /// <summary>
        /// 轴温viewModel
        /// </summary>
        public AxleTemperatureViewModel AxleTemperatureViewModel
        {
            get { return m_AxleTemperatureViewModel; }
            set
            {
                if (Equals(value, m_AxleTemperatureViewModel))
                {
                    return;
                }
                m_AxleTemperatureViewModel = value;
                RaisePropertyChanged(() => AxleTemperatureViewModel);
            }
        }
        /// <summary>
        /// 主电路viewModel
        /// </summary>
        public MainCircuitPageViewModel MainCircuitPageViewModel
        {
            get { return m_MainCircuitPageViewModel; }
            set
            {
                if (Equals(value, m_MainCircuitPageViewModel))
                {
                    return;
                }
                m_MainCircuitPageViewModel = value;
                RaisePropertyChanged(() => MainCircuitPageViewModel);
            }
        }
        /// <summary>
        /// 状态viewModel
        /// </summary>
        public StatusViewModel StatusViewModel
        {
            get { return m_StatusViewModel; }
            set
            {
                if (Equals(value, m_StatusViewModel))
                {
                    return;
                }
                m_StatusViewModel = value;
                RaisePropertyChanged(() => StatusViewModel);
            }
        }
        /// <summary>
        /// 辅系统viewModel
        /// </summary>
        public AssistSysytemPageViewModel AssistSysytemPageViewModel
        {
            get { return m_AssistSysytemPageViewModel; }
            set
            {
                if (Equals(value, m_AssistSysytemPageViewModel))
                {
                    return;
                }
                m_AssistSysytemPageViewModel = value;
                RaisePropertyChanged(() => AssistSysytemPageViewModel);
            }
        }
        /// <summary>
        /// 网络viewModel
        /// </summary>
        public NetPageViewModel NetPageViewModel
        {
            get { return m_NetPageViewModel; }
            set
            {
                if (Equals(value, m_NetPageViewModel))
                {
                    return;
                }
                m_NetPageViewModel = value;
                RaisePropertyChanged(() => NetPageViewModel);
            }
        }
        /// <summary>
        /// 主画面
        /// </summary>
        public MasterViewModel MasterViewModel
        {
            get { return m_MasterViewModel; }
            set
            {
                if (Equals(value, m_MasterViewModel))
                {
                    return;
                }
                m_MasterViewModel = value;
                RaisePropertyChanged(() => MasterViewModel);
            }
        }
        /// <summary>
        /// 标题栏viewModel
        /// </summary>
        public TitleViewModel Title
        {
            get { return m_Title; }
            set
            {
                if (Equals(value, m_Title))
                {
                    return;
                }
                m_Title = value;
                RaisePropertyChanged(() => Title);
            }
        }
        /// <summary>
        /// 按钮viewModel
        /// </summary>
        [Import]
        public BottomButtonViewModel BottomButtonViewModel { private set; get; }
        /// <summary>
        /// 内容控件变换接口
        /// </summary>
        public IContentViewNavigater ContentViewNavigater { set; get; }
        /// <summary>
        /// 视图变换接口
        /// </summary>
        public IViewNavigater ViewNavigater { set; get; }
        /// <summary>
        /// 数据变化触发方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void DataChange(object sender, CommunicationDataChangedArgs args)
        {
            MMIBlackChanged(args.ChangedBools);
            Button(args);
            MasterMainStatusChanged(args);
            NetPageStatusChanged(args.ChangedBools);
            AssistSysytemPageChanged(args.ChangedBools);
            StatusViewModelChanged(args.ChangedBools);
            MainCircuitChanged(args);
            AxleTemperatureFloatChange(args.ChangedFloats);
            FaultInit(args.ChangedBools);
            CommandChanged(args.ChangedBools);
        }

        public string CommandStr
        {
            get { return m_CommandStr; }
            set
            {
                if (value == m_CommandStr)
                {
                    return;
                }
                m_CommandStr = value;
                RaisePropertyChanged(() => CommandStr);
            }
        }

        public string FaultInfoStr
        {
            get { return m_FaultInfoStr; }
            set
            {
                if (value == m_FaultInfoStr)
                {
                    return;
                }
                m_FaultInfoStr = value;
                RaisePropertyChanged(() => FaultInfoStr);
            }
        }

        private List<string> ComandStrs = new List<string>();

        private void CommandChanged()
        {
            CommandStr = ComandStrs.Count != 0 ? ComandStrs[ComandStrs.Count - 1] : string.Empty;
        }

        void CommandChanged(CommunicationDataChangedArgs<bool> args)
        {
            foreach (var b in args.NewValue.Where(w => AllCommandInfo.ContainsKey(w.Key)))
            {
                if (b.Value)
                {

                    ComandStrs.Add(AllCommandInfo[b.Key]);
                    CommandChanged();
                    //CommandStr = AllCommandInfo[b.Key];
                }
                else
                {
                    ComandStrs.Remove(AllCommandInfo[b.Key]);
                    CommandChanged();
                }
            }
        }
        private void FaultInit(CommunicationDataChangedArgs<bool> changedBools)
        {
            foreach (var key in changedBools.NewValue.Keys.Where(key => MessageMgr.AllInfo.ContainsKey(key)))
            {
                if (changedBools.NewValue[key])
                {
                    MessageMgr.Add(key);
                    Infos = MessageMgr.GetCurrent();
                    CheckInfos = MessageMgr.GetCheckModelFault();
                }
                else
                {
                    MessageMgr.Remove(key);

                    Infos = MessageMgr.GetCurrent();
                    CheckInfos = MessageMgr.GetCheckModelFault();
                }
            }
        }

        public IList<MessageInfo> CheckInfos
        {
            get { return m_CheckInfos; }
            set
            {
                if (Equals(value, m_CheckInfos))
                {
                    return;
                }
                m_CheckInfos = value;
                RaisePropertyChanged(() => CheckInfos);
            }
        }



        // ReSharper disable once FunctionComplexityOverflow
        private void AxleTemperatureFloatChange(CommunicationDataChangedArgs<float> args)
        {
            var index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车环境温度];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.EnvironmentTemperatureOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴1轴箱左];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis1AxisBoxLeftOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴1抱箱左];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis1HeldBoxLeftOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴1轴箱右];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis1AxisBoxRighrOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴1抱箱右];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis1HeldBoxRighrOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴2轴箱左];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis2AxisBoxLeftOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴2抱箱左];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis2HeldBoxLeftOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴2轴箱右];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis2AxisBoxRighrOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴2抱箱右];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis2HeldBoxRighrOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴3轴箱左];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis3AxisBoxLeftOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴3抱箱左];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis3HeldBoxLeftOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴3轴箱右];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis3AxisBoxRighrOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴3抱箱右];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis3HeldBoxRighrOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴4轴箱左];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis4AxisBoxLeftOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴4抱箱左];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis4HeldBoxLeftOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴4轴箱右];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis4AxisBoxRighrOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴4抱箱右];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis4HeldBoxRighrOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴5轴箱左];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis5AxisBoxLeftOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴5抱箱左];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis5HeldBoxLeftOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴5轴箱右];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis5AxisBoxRighrOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴5抱箱右];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis5HeldBoxRighrOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴6轴箱左];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis6AxisBoxLeftOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴6抱箱左];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis6HeldBoxLeftOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴6轴箱右];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis6AxisBoxRighrOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴6抱箱右];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis6HeldBoxRighrOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车轴温最大值];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.MaxAxisTemperatureOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车最大轴温位置];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.MaxAxisTemperatureLocationOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车传感器个数];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.SensorNumOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车环境温度];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.EnvironmentTemperatureOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴1轴箱左];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis1AxisBoxLeftOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴1抱箱左];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis1HeldBoxLeftOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴1轴箱右];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis1AxisBoxRighrOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴1抱箱右];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis1HeldBoxRighrOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴2轴箱左];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis2AxisBoxLeftOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴2抱箱左];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis2HeldBoxLeftOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴2轴箱右];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis2AxisBoxRighrOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴2抱箱右];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis2HeldBoxRighrOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴3轴箱左];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis3AxisBoxLeftOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴3抱箱左];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis3HeldBoxLeftOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴3轴箱右];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis3AxisBoxRighrOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴3抱箱右];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis3HeldBoxRighrOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴4轴箱左];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis4AxisBoxLeftOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴4抱箱左];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis4HeldBoxLeftOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴4轴箱右];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis4AxisBoxRighrOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴4抱箱右];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis4HeldBoxRighrOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴5轴箱左];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis5AxisBoxLeftOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴5抱箱左];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis5HeldBoxLeftOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴5轴箱右];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis5AxisBoxRighrOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴5抱箱右];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis5HeldBoxRighrOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴6轴箱左];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis6AxisBoxLeftOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴6抱箱左];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis6HeldBoxLeftOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴6轴箱右];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis6AxisBoxRighrOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴6抱箱右];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.Axis6HeldBoxRighrOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车轴温最大值];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.MaxAxisTemperatureOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车最大轴温位置];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.MaxAxisTemperatureLocationOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车传感器个数];
            if (args.NewValue.ContainsKey(index))
            {
                AxleTemperatureViewModel.SensorNumOther = args.NewValue[index];
            }

        }

        private void MainCircuitChanged(CommunicationDataChangedArgs args)
        {
            MainCircuitBoolChanged(args.ChangedBools);
            MainCircuitFloadChanged(args.ChangedFloats);
        }

        private void MainCircuitFloadChanged(CommunicationDataChangedArgs<float> args)
        {
            var index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车架1电机电压];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.Frame1MOtorVoltageOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车架2电机电压];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.Frame2MotorVoltageOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车励磁电流];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.MagneticGalvanicOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车电机电流1];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.MotorGalvanic1Origin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车电机电流2];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.MotorGalvanic2Origin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车电机电流3];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.MotorGalvanic3Origin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车电机电流4];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.MotorGalvanic4Origin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车电机电流5];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.MotorGalvanic5Origin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车电机电流6];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.MotorGalvanic6Origin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车网压];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.NetPressOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车架1电机电压];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.Frame1MOtorVoltageOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车架2电机电压];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.Frame2MotorVoltageOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车励磁电流];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.MagneticGalvanicOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车电机电流1];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.MotorGalvanic1Other = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车电机电流2];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.MotorGalvanic2Other = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车电机电流3];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.MotorGalvanic3Other = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车电机电流4];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.MotorGalvanic4Other = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车电机电流5];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.MotorGalvanic5Other = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车电机电流6];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.MotorGalvanic6Other = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车网压];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.NetPressOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车1架网压];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.NetPressOneOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车2架网压];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.NetPressTwoOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车1架网压];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.NetPressOneOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车2架网压];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.NetPressTwoOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.运行级位];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.LevelOrigin = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车级位];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.LevelOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车削磁级数];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.MagicOther = args.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.磁削级数];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.MagicOrigin = args.NewValue[index];
            }
        }

        private void MainCircuitBoolChanged(CommunicationDataChangedArgs<bool> args)
        {
            var index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车牵引];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.TowOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车制动];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.BrakeOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车1架隔离];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.Frame1FaultOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车2架隔离];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.Frame2FaultOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车降弓分主断];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.BowDownMasterOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车它车禁止主断];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.BanMasterOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车合主断];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.MasterClosingOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车主断已合];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.MasterClosedOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车线路接触器1];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.LineTouch1Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车线路接触器23];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.LineTouch23Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车线路接触器4];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.LineTouch4Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车线路接触器56];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.LineTouch56Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车励磁接触器];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.MagneticTouchOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车位1牵引];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.Bit1TowOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车位1制动];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.Bit1BrakeOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车位2牵引];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.Bit2TowOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车位2制动];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.Bit2BrakeOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车级13磁削];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.Level13MagneticOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车级23磁削];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.Level23MagneticOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车司机钥匙];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.DriverKeyOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车牵引];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.TowOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车制动];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.BrakeOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车架1隔离];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.Frame1FaultOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车架2隔离];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.Frame2FaultOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车降弓分主断];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.BowDownMasterOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车它车禁止主断];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.BanMasterOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车合主断];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.MasterClosingOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车主断已合];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.MasterClosedOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车线路接触器1];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.LineTouch1Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车线路接触器23];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.LineTouch23Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车线路接触器4];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.LineTouch4Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车线路接触器56];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.LineTouch56Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车励磁接触器];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.MagneticTouchOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车位1牵引];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.Bit1TowOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车位1制动];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.Bit1BrakeOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车位2牵引];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.Bit2TowOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车位2制动];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.Bit2BrakeOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车级13磁削];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.Level13MagneticOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车级23磁削];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.Level23MagneticOther = BoolToDouble(args.NewValue[index]);
            }

            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车分主段];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.MasterOpenOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.它车分主段];
            if (args.NewValue.ContainsKey(index))
            {
                MainCircuitPageViewModel.MasterOpenOther = BoolToDouble(args.NewValue[index]);
            }

        }

        private void StatusViewModelChanged(CommunicationDataChangedArgs<bool> args)
        {
            var index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车线路接触器故障1];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.LineTouchFault1Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车线路接触器故障2];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.LineTouchFault2Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车线路接触器故障隔离3];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.LineTouchFault3Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车线路接触器故障隔离4];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.LineTouchFault4Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车线路接触器故障隔离5];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.LineTouchFault5Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车线路接触器故障隔离6];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.LineTouchFault6Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车风速继电器故障隔离1];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.FanSpeedRelayFault1Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车风速继电器故障隔离2];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.FanSpeedRelayFault2Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车劈相机故障隔离1];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.CameraFault1Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车劈相机故障隔离2];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.CameraFault2Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车压缩机故障隔离];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.CompressFaultOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车牵引风机故障隔离1];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.TractionFanFault1Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车牵引风机故障隔离2];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.TractionFanFault2Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车牵引风机故障隔离3];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.TractionFanFault3Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车牵引风机故障隔离4];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.TractionFanFault4Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车制动风机故障隔离1];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.BrakeFanFault1Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车制动风机故障隔离2];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.BrakeFanFault2Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车变压器风机故障隔离];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.TransformerFanFaultOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车油泵故障隔离];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.OilPumpFaultOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车零压故障隔离];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.ZeroPressFaultOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车库用开关1];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.StoreroomSwitch1Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车库用开关2];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.StoreroomSwitch2Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车空载试验开关1];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.EmptyTestSwitch1Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车空载试验开关2];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.EmptyTestSwitch2Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车受电弓风压隔离];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.BowFaultOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车主断隔离开关];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.MasterFaultOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车线路接触器故障1];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.LineTouchFault1Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车线路接触器故障2];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.LineTouchFault2Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车线路接触器故障隔离3];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.LineTouchFault3Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车线路接触器故障隔离4];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.LineTouchFault4Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车线路接触器故障隔离5];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.LineTouchFault5Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车线路接触器故障隔离6];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.LineTouchFault6Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车风速继电器故障隔离1];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.FanSpeedRelayFault1Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车风速继电器故障隔离2];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.FanSpeedRelayFault2Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车劈相机故障隔离1];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.CameraFault1Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车劈相机故障隔离2];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.CameraFault2Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车压缩机故障隔离];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.CompressFaultOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车牵引风机故障隔离1];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.TractionFanFault1Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车牵引风机故障隔离2];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.TractionFanFault2Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车牵引风机故障隔离3];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.TractionFanFault3Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车牵引风机故障隔离4];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.TractionFanFault4Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车制动风机故障隔离1];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.BrakeFanFault1Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车制动风机故障隔离2];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.BrakeFanFault2Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车变压器风机故障隔离];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.TransformerFanFaultOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车油泵故障隔离];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.OilPumpFaultOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车零压故障隔离];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.ZeroPressFaultOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车库用开关1];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.StoreroomSwitch1Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车库用开关2];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.StoreroomSwitch2Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车空载试验开关1];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.EmptyTestSwitch1Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车空载试验开关2];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.EmptyTestSwitch2Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车受电弓风压隔离];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.BowFaultOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车主断隔离开关];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.MasterFaultOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车单机运行开关];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.StandAloneRunOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车重联运行];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.ReconnexionOrgin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.它车重联运行];
            if (args.NewValue.ContainsKey(index))
            {
                StatusViewModel.ReconnexionOther = BoolToDouble(args.NewValue[index]);
            }
        }

        private void AssistSysytemPageChanged(CommunicationDataChangedArgs<bool> args)
        {
            var index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车起牵引风机1];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.TractionFan1Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车起牵引风机3];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.TractionFan3Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车起牵引风机2];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.TractionFan2Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车起牵引风机4];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.TractionFan4Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车起制动风机1];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.BrakeFan1Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车起制动风机2];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.BrakeFan2Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车起变压器风机];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.TransformerFanOpenOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车起油泵];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.OilPumpOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车起劈相机1];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.Camera1Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车起劈相机2];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.Camera2Origin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车合劈相机启动继电器];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.CameraRelayOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车起压缩机];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.CompressOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车起干燥器];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.DesiccationOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车起轮喷];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.SpurtWheelOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车牵引1风机接触器闭合];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.Traction1FanTouchOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车牵引3风机接触器闭合];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.Traction3FanTouchOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车牵引2风机接触器闭合];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.Traction2FanTouchOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车牵引4风机接触器闭合];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.Traction4FanTouchOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车制动1风机接触器闭合];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.Brake1FanTouchOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车制动2风机接触器闭合];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.Brake2FanTouchOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车变压器风机];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.TransformerFanOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车油泵继电器合];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.OilPumpRelayOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车劈相机1接触器闭合];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.Camera1TouchOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车劈相机2接触器闭合];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.Camera2TouchOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车劈相机启动继电器合];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.CameraRelayOpenOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车压缩机接触器闭合];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.CompressTouchOrigin = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车起牵引风机1];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.TractionFan1Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车起牵引风机3];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.TractionFan3Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车起牵引风机2];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.TractionFan2Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车起牵引风机4];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.TractionFan4Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车起制动风机1];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.BrakeFan1Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车起制动风机2];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.BrakeFan2Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车起变压器风机];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.TransformerFanOpenOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车起油泵];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.OilPumpOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车起劈相机1];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.Camera1Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车起劈相机2];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.Camera2Other = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车合劈相机启动继电器];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.CameraRelayOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车起压缩机];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.CompressOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车起干燥器];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.DesiccationOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车起轮喷];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.SpurtWheelOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车牵引风机1接触器闭合];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.Traction1FanTouchOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车牵引风机3接触器闭合];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.Traction3FanTouchOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车牵引风机2接触器闭合];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.Traction2FanTouchOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车牵引风机4接触器闭合];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.Traction4FanTouchOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车制动风机1接触器闭合];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.Brake1FanTouchOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车制动风机2接触器闭合];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.Brake2FanTouchOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车变压器风机];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.TransformerFanOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车油泵继电器合];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.OilPumpRelayOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车劈相机1接触器闭合];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.Camera1TouchOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车劈相机2接触器闭合];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.Camera2TouchOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车劈相机启动继电器合];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.CameraRelayOpenOther = BoolToDouble(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车压缩机接触器闭合];
            if (args.NewValue.ContainsKey(index))
            {
                AssistSysytemPageViewModel.CompressTouchOther = BoolToDouble(args.NewValue[index]);
            }
        }

        static double BoolToDouble(bool bPara)
        {
            return bPara ? 1 : 0;
        }
        private void NetPageStatusChanged(CommunicationDataChangedArgs<bool> args)
        {
            var index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车CCU正常];
            var index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车CCU异常];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2))
            {
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    NetPageViewModel.CCUColorOringin = NetPageColor.Normal;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    NetPageViewModel.CCUColorOringin = NetPageColor.Abnormity;
                }
                else
                {
                    NetPageViewModel.CCUColorOringin = NetPageColor.None;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车CCU正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车CCU异常];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2))
            {
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    NetPageViewModel.CCUColorOther = NetPageColor.Normal;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    NetPageViewModel.CCUColorOther = NetPageColor.Abnormity;
                }
                else
                {
                    NetPageViewModel.CCUColorOther = NetPageColor.None;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车IDU2正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车IDU2异常];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2))
            {
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    NetPageViewModel.IDU2ColorOther = NetPageColor.Normal;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    NetPageViewModel.IDU2ColorOther = NetPageColor.Abnormity;
                }
                else
                {
                    NetPageViewModel.IDU2ColorOther = NetPageColor.None;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车IDU2正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车IDU2异常];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2))
            {
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    NetPageViewModel.IDU2ColorOringin = NetPageColor.Normal;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    NetPageViewModel.IDU2ColorOringin = NetPageColor.Abnormity;
                }
                else
                {
                    NetPageViewModel.IDU2ColorOringin = NetPageColor.None;
                }
            }

            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车TAX2正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车TAX2异常];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2))
            {
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    NetPageViewModel.TAX2ColorOringin = NetPageColor.Normal;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    NetPageViewModel.TAX2ColorOringin = NetPageColor.Abnormity;
                }
                else
                {
                    NetPageViewModel.TAX2ColorOringin = NetPageColor.None;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车TAX2正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车TAX2异常];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2))
            {
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    NetPageViewModel.TAX2ColorOther = NetPageColor.Normal;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    NetPageViewModel.TAX2ColorOther = NetPageColor.Abnormity;
                }
                else
                {
                    NetPageViewModel.TAX2ColorOther = NetPageColor.None;
                }
            }

            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车LCU2正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车LCU2异常];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2))
            {
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    NetPageViewModel.LCU2ColorOther = NetPageColor.Normal;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    NetPageViewModel.LCU2ColorOther = NetPageColor.Abnormity;
                }
                else
                {
                    NetPageViewModel.LCU2ColorOther = NetPageColor.None;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车LCU2正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车LCU2异常];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2))
            {
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    NetPageViewModel.LCU2ColorOringin = NetPageColor.Normal;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    NetPageViewModel.LCU2ColorOringin = NetPageColor.Abnormity;
                }
                else
                {
                    NetPageViewModel.LCU2ColorOringin = NetPageColor.None;
                }
            }

            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车SCU正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车SCU异常];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2))
            {
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    NetPageViewModel.SCUColorOringin = NetPageColor.Normal;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    NetPageViewModel.SCUColorOringin = NetPageColor.Abnormity;
                }
                else
                {
                    NetPageViewModel.SCUColorOringin = NetPageColor.None;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车SCU正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车SCU异常];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2))
            {
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    NetPageViewModel.SCUColorOther = NetPageColor.Normal;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    NetPageViewModel.SCUColorOther = NetPageColor.Abnormity;
                }
                else
                {
                    NetPageViewModel.SCUColorOther = NetPageColor.None;
                }
            }

            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车IDU1正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车IDU1异常];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2))
            {
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    NetPageViewModel.IDU1ColorOringin = NetPageColor.Normal;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    NetPageViewModel.IDU1ColorOringin = NetPageColor.Abnormity;
                }
                else
                {
                    NetPageViewModel.IDU1ColorOringin = NetPageColor.None;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车IDU1正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车IDU1异常];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2))
            {
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    NetPageViewModel.IDU1ColorOther = NetPageColor.Normal;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    NetPageViewModel.IDU1ColorOther = NetPageColor.Abnormity;
                }
                else
                {
                    NetPageViewModel.IDU1ColorOther = NetPageColor.None;
                }
            }

            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车DCU1正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车DCU1异常];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2))
            {
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    NetPageViewModel.DCU1ColorOringin = NetPageColor.Normal;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    NetPageViewModel.DCU1ColorOringin = NetPageColor.Abnormity;
                }
                else
                {
                    NetPageViewModel.DCU1ColorOringin = NetPageColor.None;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车DCU1正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车DCU1异常];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2))
            {
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    NetPageViewModel.DCU1ColorOther = NetPageColor.Normal;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    NetPageViewModel.DCU1ColorOther = NetPageColor.Abnormity;
                }
                else
                {
                    NetPageViewModel.DCU1ColorOther = NetPageColor.None;
                }
            }

            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车DCU2正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车DCU2异常];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2))
            {
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    NetPageViewModel.DCU2ColorOringin = NetPageColor.Normal;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    NetPageViewModel.DCU2ColorOringin = NetPageColor.Abnormity;
                }
                else
                {
                    NetPageViewModel.DCU2ColorOringin = NetPageColor.None;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车DCU2正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车DCU2异常];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2))
            {
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    NetPageViewModel.DCU2ColorOther = NetPageColor.Normal;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    NetPageViewModel.DCU2ColorOther = NetPageColor.Abnormity;
                }
                else
                {
                    NetPageViewModel.DCU2ColorOther = NetPageColor.None;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车LCU1正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车LCU1异常];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2))
            {
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    NetPageViewModel.LCU1ColorOringin = NetPageColor.Normal;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    NetPageViewModel.LCU1ColorOringin = NetPageColor.Abnormity;
                }
                else
                {
                    NetPageViewModel.LCU1ColorOringin = NetPageColor.None;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车LCU1正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车LCU1异常];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2))
            {
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    NetPageViewModel.LCU1ColorOther = NetPageColor.Normal;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    NetPageViewModel.LCU1ColorOther = NetPageColor.Abnormity;
                }
                else
                {
                    NetPageViewModel.LCU1ColorOther = NetPageColor.None;
                }
            }
        }

        private void MasterMainStatusChanged(CommunicationDataChangedArgs args)
        {
            MasterMainBoolChange(args.ChangedBools);
            MasterMainFloatChange(args.ChangedFloats);
        }

        private void MasterMainFloatChange(CommunicationDataChangedArgs<float> changedFloats)
        {
            var index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.机车速度];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.TrainSpeed = changedFloats.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.运行级位];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.BrakeLevel = changedFloats.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.工况];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.Worke = (WorkModle)changedFloats.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.磁削级数];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.MagneticLevel = changedFloats.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.方向];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.Direction = (Direction)changedFloats.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.A车电机电压V1];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.MotorVoltageOneOrigin = changedFloats.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.A车电机电压V2];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.MotorVoltageTwoOrigin = changedFloats.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.B车电机电压V1];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.MotorVoltageOneOther = changedFloats.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.B车电机电压V2];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.MotorVoltageTwoOther = changedFloats.NewValue[index];
            }



            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.励磁电流A1];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.MagneticGalvanicOrigin = changedFloats.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.励磁电流B1];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.MagneticGalvanicOther = changedFloats.NewValue[index];
            }



            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.A车电机电流I1];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.MotorGalvanicOneOrigin = changedFloats.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.A车电机电流I2];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.MotorGalvanicTwoOrigin = changedFloats.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.A车电机电流I3];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.MotorGalvanicThreeOrigin = changedFloats.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.A车电机电流I4];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.MotorGalvanicFourOrigin = changedFloats.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.A车电机电流I5];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.MotorGalvanicFiveOrigin = changedFloats.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.A车电机电流I6];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.MotorGalvanicSixOrigin = changedFloats.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.B车电机电流I1];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.MotorGalvanicOneOther = changedFloats.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.B车电机电流I2];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.MotorGalvanicTwoOther = changedFloats.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.B车电机电流I3];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.MotorGalvanicThreeOther = changedFloats.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.B车电机电流I4];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.MotorGalvanicFourOther = changedFloats.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.B车电机电流I5];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.MotorGalvanicFiveOther = changedFloats.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.B车电机电流I6];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.MotorGalvanicSixOther = changedFloats.NewValue[index];
            }




            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车阀缸压力1];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.ValvePressureOneOrigin = changedFloats.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.本车阀缸压力2];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.ValvePressureTwoOrigin = changedFloats.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车阀缸压力1];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.ValvePressureOneOther = changedFloats.NewValue[index];
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.从车阀缸压力2];
            if (changedFloats.NewValue.ContainsKey(index))
            {
                MasterViewModel.ValvePressureTwoOther = changedFloats.NewValue[index];
            }

        }

        private void MasterMainBoolChange(CommunicationDataChangedArgs<bool> changedBools)
        {
            var index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车主断正常];
            var index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车主断故障];
            if (changedBools.NewValue.ContainsKey(index) || changedBools.NewValue.ContainsKey(index2))
            {
                if (changedBools.NewValue.ContainsKey(index) && changedBools.NewValue[index])
                {
                    MasterViewModel.MasterOther = ColorLevel.Green;

                }
                else if (changedBools.NewValue.ContainsKey(index2) && changedBools.NewValue[index2])
                {
                    MasterViewModel.MasterOther = ColorLevel.Red;
                }
                else
                {
                    MasterViewModel.MasterOther = ColorLevel.DarkGray;
                }


            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车主断正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车主断故障];
            if (changedBools.NewValue.ContainsKey(index) || changedBools.NewValue.ContainsKey(index2))
            {
                if (changedBools.NewValue.ContainsKey(index) && changedBools.NewValue[index])
                {
                    MasterViewModel.MasterOrigin = ColorLevel.Green;

                }
                else if (changedBools.NewValue.ContainsKey(index2) && changedBools.NewValue[index2])
                {
                    MasterViewModel.MasterOrigin = ColorLevel.Red;
                }
                else
                {
                    MasterViewModel.MasterOrigin = ColorLevel.DarkGray;
                }
            }

            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车预备正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车预备故障];
            if (changedBools.NewValue.ContainsKey(index) || changedBools.NewValue.ContainsKey(index2))
            {
                if (changedBools.NewValue.ContainsKey(index) && changedBools.NewValue[index])
                {
                    MasterViewModel.DestineOrigin = ColorLevel.Green;

                }
                else if (changedBools.NewValue.ContainsKey(index2) && changedBools.NewValue[index2])
                {
                    MasterViewModel.DestineOrigin = ColorLevel.Red;
                }
                else
                {
                    MasterViewModel.DestineOrigin = ColorLevel.DarkGray;
                }
            }

            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车预备正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车预备故障];
            if (changedBools.NewValue.ContainsKey(index) || changedBools.NewValue.ContainsKey(index2))
            {
                if (changedBools.NewValue.ContainsKey(index) && changedBools.NewValue[index])
                {
                    MasterViewModel.DestineOther = ColorLevel.Green;

                }
                else if (changedBools.NewValue.ContainsKey(index2) && changedBools.NewValue[index2])
                {
                    MasterViewModel.DestineOther = ColorLevel.Red;
                }
                else
                {
                    MasterViewModel.DestineOther = ColorLevel.DarkGray;
                }
            }

            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车通讯1正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车通讯1故障];
            if (changedBools.NewValue.ContainsKey(index) || changedBools.NewValue.ContainsKey(index2))
            {
                if (changedBools.NewValue.ContainsKey(index) && changedBools.NewValue[index])
                {
                    MasterViewModel.NetOneOther = ColorLevel.Green;

                }
                else if (changedBools.NewValue.ContainsKey(index2) && changedBools.NewValue[index2])
                {
                    MasterViewModel.NetOneOther = ColorLevel.Red;
                }
                else
                {
                    MasterViewModel.NetOneOther = ColorLevel.DarkGray;
                }
            }

            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车通讯2正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车通讯2故障];
            if (changedBools.NewValue.ContainsKey(index) || changedBools.NewValue.ContainsKey(index2))
            {
                if (changedBools.NewValue.ContainsKey(index) && changedBools.NewValue[index])
                {
                    MasterViewModel.NetTwoOther = ColorLevel.Green;

                }
                else if (changedBools.NewValue.ContainsKey(index2) && changedBools.NewValue[index2])
                {
                    MasterViewModel.NetTwoOther = ColorLevel.Red;
                }
                else
                {
                    MasterViewModel.NetTwoOther = ColorLevel.DarkGray;
                }
            }

            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车通讯1正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车通讯1故障];
            if (changedBools.NewValue.ContainsKey(index) || changedBools.NewValue.ContainsKey(index2))
            {
                if (changedBools.NewValue.ContainsKey(index) && changedBools.NewValue[index])
                {
                    MasterViewModel.NetOneOrigin = ColorLevel.Green;

                }
                else if (changedBools.NewValue.ContainsKey(index2) && changedBools.NewValue[index2])
                {
                    MasterViewModel.NetOneOrigin = ColorLevel.Red;
                }
                else
                {
                    MasterViewModel.NetOneOrigin = ColorLevel.DarkGray;
                }
            }

            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车通讯2正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车通讯2故障];
            if (changedBools.NewValue.ContainsKey(index) || changedBools.NewValue.ContainsKey(index2))
            {
                if (changedBools.NewValue.ContainsKey(index) && changedBools.NewValue[index])
                {
                    MasterViewModel.NetTwoOrigin = ColorLevel.Green;

                }
                else if (changedBools.NewValue.ContainsKey(index2) && changedBools.NewValue[index2])
                {
                    MasterViewModel.NetTwoOrigin = ColorLevel.Red;
                }
                else
                {
                    MasterViewModel.NetTwoOrigin = ColorLevel.DarkGray;
                }
            }



            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车劈相机1正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车劈相机1故障];
            if (changedBools.NewValue.ContainsKey(index) || changedBools.NewValue.ContainsKey(index2))
            {
                if (changedBools.NewValue.ContainsKey(index) && changedBools.NewValue[index])
                {
                    MasterViewModel.CameraOneOther = ColorLevel.Green;

                }
                else if (changedBools.NewValue.ContainsKey(index2) && changedBools.NewValue[index2])
                {
                    MasterViewModel.CameraOneOther = ColorLevel.Red;
                }
                else
                {
                    MasterViewModel.CameraOneOther = ColorLevel.DarkGray;
                }
            }

            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车劈相机2正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.从车劈相机2故障];
            if (changedBools.NewValue.ContainsKey(index) || changedBools.NewValue.ContainsKey(index2))
            {
                if (changedBools.NewValue.ContainsKey(index) && changedBools.NewValue[index])
                {
                    MasterViewModel.CameraTwoOther = ColorLevel.Green;

                }
                else if (changedBools.NewValue.ContainsKey(index2) && changedBools.NewValue[index2])
                {
                    MasterViewModel.CameraTwoOther = ColorLevel.Red;
                }
                else
                {
                    MasterViewModel.CameraTwoOther = ColorLevel.DarkGray;
                }
            }

            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车劈相机1正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车劈相机1故障];
            if (changedBools.NewValue.ContainsKey(index) || changedBools.NewValue.ContainsKey(index2))
            {
                if (changedBools.NewValue.ContainsKey(index) && changedBools.NewValue[index])
                {
                    MasterViewModel.CameraOneOrigin = ColorLevel.Green;

                }
                else if (changedBools.NewValue.ContainsKey(index2) && changedBools.NewValue[index2])
                {
                    MasterViewModel.CameraOneOrigin = ColorLevel.Red;
                }
                else
                {
                    MasterViewModel.CameraOneOrigin = ColorLevel.DarkGray;
                }
            }

            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车劈相机2正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.本车劈相机2故障];
            if (changedBools.NewValue.ContainsKey(index) || changedBools.NewValue.ContainsKey(index2))
            {
                if (changedBools.NewValue.ContainsKey(index) && changedBools.NewValue[index])
                {
                    MasterViewModel.CameraTwoOrigin = ColorLevel.Green;

                }
                else if (changedBools.NewValue.ContainsKey(index2) && changedBools.NewValue[index2])
                {
                    MasterViewModel.CameraTwoOrigin = ColorLevel.Red;
                }
                else
                {
                    MasterViewModel.CameraTwoOrigin = ColorLevel.DarkGray;
                }
            }
        }

        private IList<MessageInfo> m_Infos;
        private string m_FaultName;
        private string m_CommandStr;
        private string m_FaultInfoStr;
        private IList<MessageInfo> m_CheckInfos;
        private PasswordInputViewModel m_PasswordInputViewModel;

        private void MMIBlackChanged(CommunicationDataChangedArgs<bool> changedBools)
        {
            var index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.黑屏标志];
            if (changedBools.NewValue.ContainsKey(index))
            {
                MMIBlack = changedBools.NewValue[index] ? Visibility.Visible : Visibility.Hidden;
                if (MMIBlack == Visibility.Visible)
                {
                    BottomButtonViewModel.ChangedMainContent(KunMingViewFullNames.ModelSelectPage);
                }
                LogMgr.Info(string.Format("MMIBlack {0}", MMIBlack));
            }
        }
        void Button(CommunicationDataChangedArgs args)
        {
            if (BottomButtonViewModel.ViewName == KunMingViewFullNames.ReconditionMainView)
            {
                ReconditionMainView(args);
            }
            if (BottomButtonViewModel.ViewName == KunMingViewFullNames.CheckModelFault)
            {
                var index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.确认按下状态MMIE确认键按下状态];
                if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
                {
                    BottomButtonViewModel.ChangedMainContent(KunMingViewFullNames.ReconditionMainView);
                }
                index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.向后6按下状态MMI6键按下状态];
                if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
                {
                    MessageMgr.LastPage();
                    CheckInfos = MessageMgr.GetCheckModelFault();
                }
                index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.开车7按下状态MMI7键按下状态];
                if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
                {
                    MessageMgr.NextPage();
                    CheckInfos = MessageMgr.GetCheckModelFault();
                }
            }
            if (BottomButtonViewModel.ViewName == KunMingViewFullNames.ModelSelectPage)
            {
                var index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.解锁按下状态];
                if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
                {
                    BottomButtonViewModel.ChangedMainContent(KunMingViewFullNames.Main);
                    BottomButtonViewModel.ChangedViewContent(KunMingViewFullNames.MasterMainPage);
                    return;
                }
                index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.向前1按下状态MMI1键按下状态];
                if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
                {
                    BottomButtonViewModel.ChangedMainContent(KunMingViewFullNames.ReconditionMainView);
                    PasswordInputViewModel.Reset();
                }
            }
            if (BottomButtonViewModel.ViewName == KunMingViewFullNames.Main)
            {
                MainView(args);
            }

        }

        private void MainView(CommunicationDataChangedArgs args)
        {
            int index;
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.向前1按下状态MMI1键按下状态];
            if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
            {
                BottomButtonViewModel.ChangedViewContent(KunMingViewFullNames.MainNetPage);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.解锁按下状态];
            if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
            {
                BottomButtonViewModel.ChangedViewContent(KunMingViewFullNames.MasterMainPage);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.方向向前按下状态MMI向上按键按下状态];
            if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
            {
                BottomButtonViewModel.ChangedViewContent(KunMingViewFullNames.FaultView);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.调车2按下状态MMI2键按下状态];
            if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
            {
                BottomButtonViewModel.ChangedViewContent(KunMingViewFullNames.AssistSysytemPage);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车位3按下状态MMI3键按下状态];
            if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
            {
                BottomButtonViewModel.ChangedViewContent(KunMingViewFullNames.StatusPage);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.进路号4按下状态MMI4键按下状态];
            if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
            {
                BottomButtonViewModel.ChangedViewContent(KunMingViewFullNames.MainCircuitPage);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.定标5按下状态MMI5键按下状态];
            if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
            {
                BottomButtonViewModel.ChangedViewContent(KunMingViewFullNames.AxleTemperaturePage);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.查询按钮按下];
            if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
            {
                BottomButtonViewModel.ChangedViewContent(KunMingViewFullNames.Tax2Page);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.返回按钮按下];
            if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
            {
                BottomButtonViewModel.ChangedMainContent(KunMingViewFullNames.ModelSelectPage);
            }

            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.方向向左按下状态MMI向左按键按下状态];
            if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
            {
                MessageMgr.LastPage();
                Infos = MessageMgr.GetCurrent();
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.方向向右按下状态MMI向右按键按下状态];
            if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
            {
                MessageMgr.NextPage();
                Infos = MessageMgr.GetCurrent();
            }
        }

        private void ReconditionMainView(CommunicationDataChangedArgs args)
        {
            var index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.向前1按下状态MMI1键按下状态];
            if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
            {
                PasswordInputViewModel.InputChar(2);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.解锁按下状态];
            if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
            {
                PasswordInputViewModel.InputChar(1);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.方向向前按下状态MMI向上按键按下状态];
            if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
            {
                PasswordInputViewModel.InputChar(8);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.调车2按下状态MMI2键按下状态];
            if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
            {
                PasswordInputViewModel.InputChar(3);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车位3按下状态MMI3键按下状态];
            if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
            {
                PasswordInputViewModel.InputChar(4);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.进路号4按下状态MMI4键按下状态];
            if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
            {
                PasswordInputViewModel.InputChar(5);
                if (PasswordInputViewModel.PasswordVisibility == Visibility.Hidden)
                {
                    BottomButtonViewModel.ChangedMainContent(KunMingViewFullNames.CheckModelFault);
                }
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.确认按下状态MMIE确认键按下状态];
            if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
            {
                PasswordInputViewModel.InputChar(9);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.定标5按下状态MMI5键按下状态];
            if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
            {
                PasswordInputViewModel.InputChar(6);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.查询按钮按下];
            if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
            {
                PasswordInputViewModel.InputChar(7);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.返回按钮按下];
            if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
            {
                PasswordInputViewModel.InputChar(0);
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.向后6按下状态MMI6键按下状态];
            if (args.ChangedBools.NewValue.ContainsKey(index) && args.ChangedBools.NewValue[index])
            {
                BottomButtonViewModel.ChangedMainContent(KunMingViewFullNames.ModelSelectPage);
            }
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
            DataChange(sender, dataChangedArgs);
        }
    }
}