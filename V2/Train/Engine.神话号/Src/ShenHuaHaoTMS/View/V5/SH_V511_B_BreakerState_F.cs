using CommonControls;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using ShenHuaHaoTMS.DefControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using CommonControls.Extensions;
using MMI.Facility.Interface.Extension;
using ShenHuaHaoTMS.Common;
using ShenHuaHaoTMS.View.V4;
using YunDa.JC.MMI.Common;
using YunDa.JC.MMI.Common.Extensions;

namespace ShenHuaHaoTMS.View
{
    public partial class SH_V511_B_BreakerState_F : UserControl, IView,IDisposable, IDataListener
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
                        _btn_Null_3.DefText = "机车_" + _dataService.ReadService.ReadOnlyFloatDictionary[_bv.LocalTrainInfo.ControlType == ControlType.主控 ? 477 : 427] + "A";
                        _btn_Null_4.DefText = "机车_" + _dataService.ReadService.ReadOnlyFloatDictionary[_bv.LocalTrainInfo.ControlType == ControlType.主控 ? 477 : 427] + "B";
                        _btn_Null_1.DefText = "机车_"+_bv.LocalTrainInfo.RealTrainID+"A";
                        _btn_2.DefText = "机车_" + _bv.LocalTrainInfo.RealTrainID + "B";
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
        private List<DefButton> _founctionBtns = new List<DefButton>();
        private List<ILogic> _statesControls = new List<ILogic>();
        private BlackView _bv = null;

        public SH_V511_B_BreakerState_F()
        {
            InitializeComponent();
        }

        public void SetData(DataSet ds)
        {
            List<LogicInfo> logicInfos = new List<LogicInfo>();
            DataRowCollection drc = ds.Tables["从车B-主断"].Rows;
            foreach (DataRow item in drc)
            {
                object o1 = item[0];
                object o2 = item[1];
                logicInfos.Add(new LogicInfo(Convert.ToInt32(item[0]), (String)item[1]));
            }
            defTable.InvokeSetLogicInfo(logicInfos);
        }

        public SH_V511_B_BreakerState_F(Int32 id, ViewManager viewManager, ICommunicationDataService dataService, BlackView bv, SubsystemInitParam initParam)
        {
            InitializeComponent();

            _dataService = dataService;
            m_ListenViewChanged.BoolChanged += onViewChanged;
            Dock = DockStyle.Fill;
            Margin = new Padding(0);

            ID = id;
            ViewManger = viewManager;
            ViewManger.Register(this);
                        GlobalParam.Instance.InitParam.RegistDataListener(this);

            _viewBtns = new List<DefButton>()
            {
                _btn_Null_1,
                _btn_Null_3,
                _btn_Null_5,
                _btn_2,
                _btn_7,
                _btn_10
            };
            _founctionBtns = new List<DefButton>()
            {
                _btn_LastPage,
                _btn_NextPage
            };

            _statesControls = new List<ILogic>()
            {
                defState1
            };

            _title_TrainName.DefText = "神华号 " + bv.LocalTrainInfo.RealTrainID + "A";

            _bv = bv;

            initParam.DataPackage.ServiceManager.GetService<IDisposeService>().RegistDisposableObject(this);
        }

        void onViewChanged(object sender, CommunicationDataChangedArgs<bool> e)
        {
            foreach (var cmd in e.NewValue)
            {
                if (cmd.Key >= 800)//按钮命令
                {
                    if (cmd.Value && _isShow)
                    {
                        //页面切换
                        DefButton btn = _viewBtns.Find(a => a.ID == cmd.Key);
                        if (btn != null)
                        {
                            btn.IsDown = true;
                            ViewManger.CurrentViewID = btn.ViewID;
                        }

                        //功能按键
                        DefButton founctionBtn = _founctionBtns.Find(a => a.ID == cmd.Key);
                        if (founctionBtn != null)
                        {
                            switch (founctionBtn.ViewID)
                            {
                                case 1://上一页
                                    defTable.LastPage();
                                    _btn_NextPage.InvokeShow();;
                                    if (defTable.IsTheFirstPage)
                                    {
                                        _btn_LastPage.InvokeHide();;
                                    }
                                    break;
                                case 2://下一页
                                    defTable.NextPage();
                                    _btn_LastPage.InvokeShow();;
                                    if (defTable.IsTheLastPage)
                                    {
                                        _btn_NextPage.InvokeHide();;
                                    }
                                    break;
                            }
                        }
                    }
                }

                if (cmd.Key == 1323)
                {
                    if (!cmd.Value)
                    {
                        ViewManger.CurrentViewID = 505;
                    }
                }
            }
        }

        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> e)
        {
            foreach (var cmd in e.NewValue)
            {
                defTable.SetRowState(cmd.Key, cmd.Value);
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

        public void InvalidateNew()
        {
        }
        public void Dispose()
        {
            _statesControls.ForEach(a => ((DefState)a).Dispose());
        }
    }
}
