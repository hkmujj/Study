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
    public partial class SH_V6_ParamConfig : UserControl, IView,IDisposable, IDataListener
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

        private List<DefButton> _viewBtns = new List<DefButton>();
        private List<ILogic> _statesControls = new List<ILogic>();

        private SH_V6_ParamConfig_C1 _c1 = null;
        private SH_V6_ParamConfig_C2 _c2 = null;

        public SH_V6_ParamConfig()
        {
            InitializeComponent();
        }

        public SH_V6_ParamConfig(Int32 id, ViewManager viewManager, ICommunicationDataService dataService, BlackView bv, SubsystemInitParam initParam)
        {
            InitializeComponent();
            _dataService = dataService;

            Dock = DockStyle.Fill;
            Margin = new Padding(0);

            _c1 = new SH_V6_ParamConfig_C1(dataService, initParam);
            _c2 = new SH_V6_ParamConfig_C2(dataService, initParam);
            _panel.Controls.Add(_c1);

            ID = id;
            ViewManger = viewManager;
            ViewManger.Register(this);
                        GlobalParam.Instance.InitParam.RegistDataListener(this);
            m_ListenViewChanged.BoolChanged += onViewChanged;

            _viewBtns = new List<DefButton>()
            {
                _btn_2,
                _btn_3,
                _btn_4,
                _btn_5,
                _btn_7
            };

            _statesControls = new List<ILogic>()
            {
                defState3
            };

            _title_TrainName.DefText = "神华号 " + bv.LocalTrainInfo.RealTrainID + "A";

            initParam.DataPackage.ServiceManager.GetService<IDisposeService>().RegistDisposableObject(this);
        }

        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> e)
        {
            foreach (var cmd in e.NewValue)
            {
                foreach (var item in _statesControls)
                {
                    ILogic logic = null;
                    foreach (var item1 in item.DataConfigInfoCollection)
                    {
                        if (item1.FloatLogic == cmd.Key)
                        {
                            logic = item;
                            DataConfigInfo dci = item1;
                            dci.DefText = cmd.Value.ToString();
                            logic.CurrentDataConfigInfo = dci;
                            break;
                        }
                    }
                    if (logic != null)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
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
                    if (item is DefState)
                    {
                        var temp = item as DefState;
                                                temp.InvokeSetState(temp, cmd.Key, cmd.Value);
                    }
                }

                if (cmd.Key == 1323 && cmd.Value)
                {
                    if (_panel.Controls.Contains(_c1))
                    {
                        _panel.InvokeRemoveChild(_c1);
                    }
                    //_panel.Controls.Remove(_c1);
                    if (!_panel.Controls.Contains(_c2))
                    {
                        _panel.InvokeIfNeed(new Action<Control>(_panel.Controls.Add), _c2);
                    }
                }
                if (cmd.Key == 1324 && cmd.Value)
                {
                    if (_panel.Controls.Contains(_c2))
                    {
                        _panel.InvokeRemoveChild(_c2);
                    }
                    if (!_panel.Controls.Contains(_c1))
                    {
                        _panel.InvokeAddChild(_c1);
                    }
                }
            }
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
            _statesControls.ForEach(a => ((DefState)a).Dispose());
        }
    }
}
