using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommonControls;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using ShenHuaHaoTMS.Common;
using YunDa.JC.MMI.Common;
using ShenHuaHaoTMS.DefControls;
using YunDa.JC.MMI.Common.Extensions;

namespace ShenHuaHaoTMS.View
{
    public partial class SH_V4_Connect : UserControl, IView, IDisposable, IDataListener
    {
        public int ID { get; set; }

        private readonly ListenViewChangedBridge m_ListenViewChanged = new ListenViewChangedBridge();

        public bool IsShow
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
                        defLabel26.DefText = _bv.LocalTrainInfo.TrainType.ToString();
                        defLabel27.DefText = _bv.LocalTrainInfo.TrainID.ToString();
                        defLabel28.DefText = _bv.LocalTrainInfo.ControlType == V4.ControlType.主控
                            ? "主车"
                            : _bv.LocalTrainInfo.Number.ToString();
                        defLabel29.DefText = _bv.LocalTrainInfo.ControlType == V4.ControlType.主控
                            ? "主车"
                            : "从车";
                        defLabel30.DefText = _bv.LocalTrainInfo.Count.ToString();
                        ViewManger.Add(this);
                        this.InvokeShow();
                        ;
                        GlobalParam.Instance.InitParam.RegistDataListener(m_ListenViewChanged);
                    }
                }
                else //隐藏
                {
                    if (ViewManger.Contains(this))
                    {
                        ViewManger.Remove(this);
                        this.InvokeHide();
                        ;
                        _viewBtns.ForEach(a => a.IsDown = false);
                        GlobalParam.Instance.InitParam.UnregistDataListener(m_ListenViewChanged);
                    }
                }
            }
        }

        private bool _isShow = false;

        public ViewManager ViewManger { get; set; }
        private ICommunicationDataService _dataService;

        private List<DefButton> _viewBtns = new List<DefButton>();
        private BlackView _bv = null;
        private List<ILogic> _statesControls = new List<ILogic>();

        public SH_V4_Connect()
        {
            InitializeComponent();
        }

        public SH_V4_Connect(Int32 id, ViewManager viewManager, ICommunicationDataService dataService, BlackView bv,
            SubsystemInitParam initParam)
        {
            InitializeComponent();

            _dataService = dataService;

            Dock = DockStyle.Fill;
            Margin = new Padding(0);

            ID = id;
            ViewManger = viewManager;
            ViewManger.Register(this);
            GlobalParam.Instance.InitParam.RegistDataListener(this);
            m_ListenViewChanged.BoolChanged += onViewChanged;

            _viewBtns = new List<DefButton>()
            {
                _btn_1,
                _btn_6,
                _btn_4,
                _btn_8,
                _btn_10
            };

            _bv = bv;

            _statesControls = new List<ILogic>()
            {
                defState1,
                defState2,
                defState3,
                defPanelState1,
                defPanelState2,
                defState4,
                defState5,
                defState6,
                defState7,
                defState8,
                defState9,
                defState10,
                defState11,
                defState12,
                defState13,
                defState14,
                defState15,
                defState16,
                defState17,
                defState18,
                defState19,
                defState20,
                defState21,
                defState22,
                defState23,
                defState24,
                defState25,
                defState26,
                defState27,
                defState28,
                defState29,
                defState30,
                defState31
            };

            initParam.DataPackage.ServiceManager.GetService<IDisposeService>().RegistDisposableObject(this);
        }

        void ReadService_FloatChanged(object sender, CommunicationDataChangedArgs<float> e)
        {
            foreach (var cmd in e.NewValue)
            {
                if (cmd.Key >= 50 && cmd.Key < 825)
                {
                    foreach (var item in _statesControls)
                    {
                        if (item is DefState)
                        {
                            var temp = item as DefState;
                            temp.InvokeIfNeed(new Action<Int32, float>(item.SetValue), cmd.Key, cmd.Value);
                        }
                    }
                }
            }
        }

        void onViewChanged(object sender, CommunicationDataChangedArgs<bool> e)
        {
            foreach (var cmd in e.NewValue)
            {
                if (cmd.Key >= 800) //按钮命令
                {
                    if (cmd.Value && _isShow)
                    {
                        if (cmd.Key == 801)
                        {
                            _dataService.WriteService.ChangeBool(815, true);
                        }
                        else
                        {
                            //页面切换
                            DefButton btn = _viewBtns.Find(a => a.ID == cmd.Key);
                            if (btn != null)
                            {
                                btn.IsDown = true;
                                ViewManger.CurrentViewID = btn.ViewID;
                            }
                        }
                    }
                    else if (!cmd.Value && _isShow)
                    {
                        if (cmd.Key == 801)
                        {
                            _dataService.WriteService.ChangeBool(815, false);
                        }
                    }
                }
            }
        }

        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> e)
        {
            foreach (var cmd in e.NewValue)
            {
                foreach (var item in _statesControls)
                {
                    if (item is DefState)
                    {
                        var temp = item as DefState;
                        temp.InvokeSetState(temp, cmd.Key, cmd.Value);
                    }
                    if (item is DefPanelState)
                    {
                        var temp = item as DefPanelState;
                        temp.InvokeSetState(temp, cmd.Key, cmd.Value);
                    }
                }
            }
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

        }

        private void DefButtonClick(object sender, ButtonClickArgs e)
        {
            ViewManger.CurrentViewID = e.ViewID;
        }

        private void GroupBtnClick(object sender, ButtonClickArgs e)
        {
            _dataService.WriteService.ChangeBool(815, false);
        }

        private void _btn_2_DefMouseDown(object sender, ButtonClickArgs e)
        {
            _dataService.WriteService.ChangeBool(815, true);
        }

        public void InvalidateNew()
        {
            gridVisibleTableLayoutPanel2.InvokeInvalidate();
            gridVisibleTableLayoutPanel5.InvokeInvalidate();
            gridVisibleTableLayoutPanel7.InvokeInvalidate();
            gridVisibleTableLayoutPanel9.InvokeInvalidate();
            gridVisibleTableLayoutPanel11.InvokeInvalidate();
            gridVisibleTableLayoutPanel14.InvokeInvalidate();
            gridVisibleTableLayoutPanel15.InvokeInvalidate();
            gridVisibleTableLayoutPanel16.InvokeInvalidate();
            gridVisibleTableLayoutPanel21.InvokeInvalidate();
            gridVisibleTableLayoutPanel22.InvokeInvalidate();
            gridVisibleTableLayoutPanel23.InvokeInvalidate();
        }

        public void Dispose()
        {
            _statesControls.ForEach(a =>
            {
                var state = a as DefState;
                if (state != null)
                {
                    state.Dispose();
                }
            });
        }
    }
}
