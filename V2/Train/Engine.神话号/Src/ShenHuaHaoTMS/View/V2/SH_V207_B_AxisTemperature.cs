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
    public partial class SH_V207_B_AxisTemperature : UserControl, IView,IDisposable, IDataListener
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
                        _btn_1.DefText = "机车_" + _bv.LocalTrainInfo.RealTrainID + "A";
                        _btn_Null_2.DefText = "机车_" + _bv.LocalTrainInfo.RealTrainID + "B";
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
        private BlackView _bv = null;

        public SH_V207_B_AxisTemperature()
        {
            InitializeComponent();
        }

        public SH_V207_B_AxisTemperature(Int32 id, ViewManager viewManager, ICommunicationDataService dataService, BlackView bv, SubsystemInitParam initParam)
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
                _btn_10
            };

            _statesControls = new List<ILogic>()
            {
                defState1,defState2,defState3,defState4,defState5,defState6,defState7,defState8,defState9,defState10,
                defState11,defState12,defState13,defState14,defState15,defState16,defState17,defState18,defState19,defState20,
                defState21,defState22,defState23,defState24,defState25,defState26,defState27,defState28,defState29,defState30,
                defState31,defState32,defState33,defState34,defState35,defState36,defState37,defState38,defState39,defState40,
                defState41,defState42,defState43,defState44,defState45,defState46,defState47,defState48,defState49
            };

            _bv = bv;

            _title_TrainName.DefText = "神华号 " + bv.LocalTrainInfo.RealTrainID + "B";

            initParam.DataPackage.ServiceManager.GetService<IDisposeService>().RegistDisposableObject(this);
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

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
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
                            DataConfigInfo dci = new DataConfigInfo();
                            dci.DefText = cmd.Value.ToString();
                            if (item1.DefText != null && item1.DefText != "")
                            {
                                if (item1.DefText.Contains("{") && item1.DefText.Contains("}"))
                                {
                                    int indexStart = item1.DefText.IndexOf('{');
                                    int indexEnd = item1.DefText.IndexOf('}');
                                    String str1 = item1.DefText.Substring(0, indexStart);
                                    String str2 = item1.DefText.Substring(indexStart + 1, indexEnd - indexStart - 1);
                                    String str3 = item1.DefText.Substring(indexEnd + 1);
                                    dci.DefText = str1 + cmd.Value.ToString(str2) + str3;
                                }
                            }
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
            _statesControls.ForEach(a => ((DefState)a).Dispose());
        }
    }
}
