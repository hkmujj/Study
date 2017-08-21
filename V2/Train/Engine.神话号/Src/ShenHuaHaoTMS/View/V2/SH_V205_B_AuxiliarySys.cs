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
    public partial class SH_V205_B_AuxiliarySys : UserControl, IView,IDisposable, IDataListener
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

        public SH_V205_B_AuxiliarySys()
        {
            InitializeComponent();
        }

        public SH_V205_B_AuxiliarySys(Int32 id, ViewManager viewManager, ICommunicationDataService dataService, BlackView bv, SubsystemInitParam initParam)
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
                _defState_31_K11,_defState_31_K01,_defState_34_K01,_defState_34_K03,_defState_31_K13,_defState_31_K05,_defState_31_K12,_defState_31_K02,
                _defState_34_K81,_defState_34_K71,_defState_34_K10,
                _defState_34_Q06,_defState_34_Q11,_defState_34_Q12,_defState_34_Q13,_defState_34_Q15,_defState_34_Q21,_defState_62_Q01,_defState_32_Q01,
                _defState_34_Q01,_defState_34_Q02,_defState_34_Q03,_defState_34_Q04,_defState_34_Q10,_defState_34_Q05,_defState_34_Q20,_defState_77_Q01,
                _defState_31_T01,_defState_31_T02,
                _defState_1,_defState_2,_defState_3,_defState_4,_defState_5,_defState_6,defState1
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

        /// <summary>bool 值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            foreach (var cmd in dataChangedArgs.NewValue)
            {
                foreach (var item in _statesControls)
                {
                    item.SetState(cmd.Key, cmd.Value);
                    //ILogic logic = null;
                    //if (item.DataConfigInfoCollection[0].BoolLogic == cmd.Key)
                    //{
                    //    item.CurrentDataConfigInfo = item.DataConfigInfoCollection[cmd.Value ? 0 : 1];
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
            foreach (var cmd in dataChangedArgs.NewValue)
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
    }
}
