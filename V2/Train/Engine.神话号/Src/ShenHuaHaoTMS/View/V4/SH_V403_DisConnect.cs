using CommonControls;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MMI.Facility.Interface.Extension;
using ShenHuaHaoTMS.Common;
using YunDa.JC.MMI.Common;
using YunDa.JC.MMI.Common.Extensions;

namespace ShenHuaHaoTMS.View
{
    public partial class SH_V403_DisConnect : UserControl, IView,IDisposable
    {
        public int ID { get; set; }

        private readonly ListenViewChangedBridge m_ListenViewChanged = new ListenViewChangedBridge();public bool IsShow
        {
            get { return _isShow; }
            set
            {
                if (_isShow == value)
                {
                    return;
                }

                _isShow = value;

                if (_isShow) //显示
                {
                    if (!ViewManger.Contains(this))
                    {
                        ViewManger.Add(this);
                        this.InvokeShow();;
                        GlobalParam.Instance.InitParam.RegistDataListener(m_ListenViewChanged);
                    }
                }
                else//隐藏
                {
                    if (ViewManger.Contains(this))
                    {
                        ViewManger.Remove(this);
                        this.InvokeHide();;
                        _viewBtns.ForEach(a => a.IsDown = false);
                        GlobalParam.Instance.InitParam.UnregistDataListener(m_ListenViewChanged);
                    }
                }
            }
        }
        private bool _isShow = false;

        public ViewManager ViewManger { get; set; }
        private ICommunicationDataService _dataService;
        private Int32 _count = 0;
        private Int32 _countDown = 10;

        private bool IsTimerStartted
        {
            set
            {
                lock (this)
                {
                    if (IsTimerStartted == value)
                    {
                        return;
                    }

                    m_IsTimerStartted = value;
                    GlobalTimer.Instance.TimersElapsed1000MS -= _timer_Elapsed;
                    if (IsTimerStartted)
                    {
                        GlobalTimer.Instance.TimersElapsed1000MS += _timer_Elapsed;
                    }
                }
            }
            get { return m_IsTimerStartted; }
        }

        private List<DefButton> _viewBtns = new List<DefButton>();
        private bool m_IsTimerStartted;

        public SH_V403_DisConnect()
        {
            InitializeComponent();
        }

        public SH_V403_DisConnect(Int32 id, ViewManager viewManager, ICommunicationDataService dataService, BlackView bv, SubsystemInitParam initParam)
        {
            InitializeComponent();
            _dataService = dataService;

            Dock = DockStyle.Fill;
            Margin = new Padding(0);
            m_ListenViewChanged.BoolChanged += onViewChanged;
            ID = id;
            ViewManger = viewManager;
            ViewManger.Register(this);

            _viewBtns = new List<DefButton>()
            {
                defButton1,defButton2
            };

            IsTimerStartted = true;

            _countDown = bv.LocalTrainInfo.CountDwon;

            initParam.DataPackage.ServiceManager.GetService<IDisposeService>().RegistDisposableObject(this);
        }

        void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _count++;
            if (_count >= _countDown)
            {
                _dataService.WriteService.ChangeBool(816, false);
                IsTimerStartted = false;
            }
        }


        void onViewChanged(object sender, CommunicationDataChangedArgs<bool> e)
        {
            foreach (var cmd in e.NewValue)
            {
                if (cmd.Key >= 800 && cmd.Key < 825)//按钮命令
                {
                    if (cmd.Value && _isShow)
                    {
                        DefButton btn = _viewBtns.Find(a => a.ID == cmd.Key);
                        if (btn != null)
                        {
                            if (btn.ID == 815)
                            {
                                _dataService.WriteService.ChangeBool(816, true);
                                _count = 0;
                                IsTimerStartted = true;
                            }
                            btn.IsDown = true;
                            ViewManger.CurrentViewID = btn.ViewID;
                        }
                    }
                }
            }
        }

        private void DefButtonClick(object sender, ButtonClickArgs e)
        {
            ViewManger.CurrentViewID = e.ViewID;
        }

        private void defButton1_DefMouseDown(object sender, ButtonClickArgs e)
        {
            _dataService.WriteService.ChangeBool(816, true);
            _count = 0;
            IsTimerStartted = true;
        }

        public void InvalidateNew()
        {
        }

        public void Dispose()
        {
        }
    }
}
