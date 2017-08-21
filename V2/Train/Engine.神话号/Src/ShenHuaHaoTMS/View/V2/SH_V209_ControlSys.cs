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
    public partial class SH_V209_ControlSys : UserControl, IView, IDisposable, IDataListener
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
        private List<ILogic> _statesControls = new List<ILogic>();

        public SH_V209_ControlSys()
        {
            InitializeComponent();
        }

        public SH_V209_ControlSys(Int32 id, ViewManager viewManager, ICommunicationDataService dataService, BlackView bv,
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

            _viewBtns = new List<DefButton>()
            {
                _btn_2,
                _btn_1,
                _btn_4,
                _btn_5,
                _btn_9,
                _btn_10
            };

            _statesControls = new List<ILogic>()
            {
                _defPanelState_MVB_LineA,
                _defPanelState_MVB_LineB,
                _defPanelState_WTB_LineA,
                _defPanelState_WTB_LineB,
                _defState_1,
                _defState_2,
                _defState_3,
                _defState_4,
                _defState_5,
                _defState_6,
                _defState_7,
                _defState_8,
                _defState_9,
                _defState_10,
                _defState_11,
                _defState_12,
                _defState_13,
                _defState_14,
                _defState_15,
                _defState_16,
                _defState_17,
                _defState_18,
                _defState_19,
                _defState_20,
                _defState_21,
                _defState_22,
                _defState_23,
                _defState_24,
                _defState_25,
                _defState_26,
                _defState_27,
                _defState_28,
                _defState_29,
                _defState_30,
                _defState_31,
                _defState_32,
                _defState_33,
                _defState_34,
                _defState_35,
                _defState_36,
                _defState_37,
                _defState_38,
                _defState_39,
                _defState_40,
                _defState_41,
                _defState_42,
                defState1
            };

            _title_TrainName.DefText = "神华号 " + bv.LocalTrainInfo.RealTrainID + "A";

            initParam.DataPackage.ServiceManager.GetService<IDisposeService>().RegistDisposableObject(this);
        }

        void onViewChanged(object sender, CommunicationDataChangedArgs<bool> e)
        {
            foreach (var cmd in e.NewValue)
            {
                if (cmd.Key >= 800 && cmd.Key < 825) //按钮命令
                {
                    if (cmd.Value && _isShow)
                    {
                        DefButton btn = _viewBtns.Find(a => a.ID == cmd.Key);
                        if (btn != null)
                        {
                            btn.IsDown = true;
                            ViewManger.CurrentViewID = btn.ViewID;
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
                    item.SetState(cmd.Key, cmd.Value);
                    //ILogic logic = null;
                    //foreach (var item1 in item.DataConfigInfoCollection)
                    //{
                    //    if (item1.BoolLogic == cmd.Key)
                    //    {
                    //        logic = item;
                    //        logic.CurrentDataConfigInfo = item1;
                    //        break;
                    //    }
                    //}
                    //if (logic != null)
                    //{
                    //    break;
                    //}
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

        private void gridVisibleTableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DefButtonClick(object sender, ButtonClickArgs e)
        {
            ViewManger.CurrentViewID = e.ViewID;
        }

        public void InvalidateNew()
        {
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
