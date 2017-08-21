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
    public partial class SH_V2_MainData : UserControl, IView, IDisposable, IDataListener
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

        public SH_V2_MainData()
        {
            InitializeComponent();
        }

        public SH_V2_MainData(Int32 id, ViewManager viewManager, ICommunicationDataService dataService, BlackView bv,
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
                _btn_2,
                _btn_3,
                _btn_4,
                _btn_5,
                _btn_6,
                _btn_7
            };

            _statesControls = new List<ILogic>()
            {
                defState1,
                defState2,
                defState3,
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
            }
        }

        private void DefClick(object sender, ButtonClickArgs e)
        {
            ViewManger.CurrentViewID = e.ViewID;
        }

        public void InvalidateNew()
        {
        }

        public void Dispose()
        {
            _statesControls.ForEach(a => ((DefState) a).Dispose());
        }
    }
}
