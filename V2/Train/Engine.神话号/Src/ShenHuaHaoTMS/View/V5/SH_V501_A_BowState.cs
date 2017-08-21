using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommonControls;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using ShenHuaHaoTMS.Properties;
using ShenHuaHaoTMS.View.V4;
using YunDa.JC.MMI.Common;
using ShenHuaHaoTMS.DefControls;
using System.Data;
using System.Drawing;
using CommonControls.Extensions;
using MMI.Facility.Interface.Extension;
using ShenHuaHaoTMS.Common;
using YunDa.JC.MMI.Common.Extensions;

namespace ShenHuaHaoTMS.View
{
    public partial class SH_V501_A_BowState : UserControl, IView, IDisposable, IDataListener
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
                        _defBtnA.DefText = "机车_" +
                                           _dataService.ReadService.ReadOnlyFloatDictionary[
                                               _bv.LocalTrainInfo.ControlType == ControlType.主控 ? 477 : 427] + "A";
                        _defBtnB.DefText = "机车_" +
                                           _dataService.ReadService.ReadOnlyFloatDictionary[
                                               _bv.LocalTrainInfo.ControlType == ControlType.主控 ? 477 : 427] + "B";
                        _btn_Null_1.DefText = "机车_" + _bv.LocalTrainInfo.RealTrainID + "A";
                        _btn_2.DefText = "机车_" + _bv.LocalTrainInfo.RealTrainID + "B";
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
        private List<DefButton> _founctionBtns = new List<DefButton>();
        private List<ILogic> _statesControls = new List<ILogic>();
        private BlackView _bv = null;
        private DefButton _defBtnA = null;
        private DefButton _defBtnB = null;

        public SH_V501_A_BowState()
        {
            InitializeComponent();
        }

        public void SetData(DataSet ds)
        {
            List<LogicInfo> logicInfos = new List<LogicInfo>();
            DataRowCollection drc = ds.Tables["A车-受电弓"].Rows;
            foreach (DataRow item in drc)
            {
                object o1 = item[0];
                object o2 = item[1];
                logicInfos.Add(new LogicInfo(Convert.ToInt32(item[0]), (String) item[1]));
            }

            defTable.InvokeSetLogicInfo(logicInfos);
        }

        public SH_V501_A_BowState(Int32 id, ViewManager viewManager, ICommunicationDataService dataService, BlackView bv,
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
                _btn_6,
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

            _defBtnA = new DefButton();
            _defBtnA.Size = new System.Drawing.Size(68, 60);
            _defBtnA.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            _defBtnA.Dock = DockStyle.Fill;
            _defBtnA.BackColor = Color.Black;
            _defBtnA.DownColor = Color.Black;
            _defBtnA.DownImage = Resources.btn_Dwon;
            _defBtnA.ID = 802;
            _defBtnA.IsEnable = true;
            _defBtnA.IsSelfResetBtn = true;
            _defBtnA.TextFont = new Font("宋体", 13);
            _defBtnA.UpColor = Color.White;
            _defBtnA.UpImage = Resources.btn_Up;
            _defBtnA.ViewID = 507;
            _defBtnA.YOffset = 2;
            _defBtnA.DefClick += DefButtonClick;

            _defBtnB = new DefButton();
            _defBtnB.Size = new System.Drawing.Size(68, 60);
            _defBtnB.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            _defBtnB.Dock = DockStyle.Fill;
            _defBtnB.BackColor = Color.Black;
            _defBtnB.DownColor = Color.Black;
            _defBtnB.DownImage = Resources.btn_Dwon;
            _defBtnB.ID = 803;
            _defBtnB.IsEnable = true;
            _defBtnB.IsSelfResetBtn = true;
            _defBtnB.TextFont = new Font("宋体", 13);
            _defBtnB.UpColor = Color.White;
            _defBtnB.UpImage = Resources.btn_Up;
            _defBtnB.ViewID = 510;
            _defBtnB.YOffset = 2;
            _defBtnB.DefClick += DefButtonClick;

            initParam.DataPackage.ServiceManager.GetService<IDisposeService>().RegistDisposableObject(this);
        }

        void onViewChanged(object sender, CommunicationDataChangedArgs<bool> e)
        {
            foreach (var cmd in e.NewValue)
            {
                if (cmd.Key >= 800) //按钮命令
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
                                case 1: //上一页
                                    defTable.LastPage();
                                    _btn_NextPage.InvokeShow();
                                    if (defTable.IsTheFirstPage)
                                    {
                                        _btn_LastPage.InvokeHide();
                                    }
                                    break;
                                case 2: //下一页
                                    defTable.NextPage();
                                    _btn_LastPage.InvokeShow();
                                    if (defTable.IsTheLastPage)
                                    {
                                        _btn_NextPage.InvokeHide();
                                    }
                                    break;
                            }
                        }
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

                if (cmd.Key == 1323)
                {
                    if (cmd.Value) //编组成功
                    {
                        _panel_ViewBtns.InvokeIfNeed(new Action<Control>(_panel_ViewBtns.Controls.Remove), _btn_Null_3);
                        _panel_ViewBtns.InvokeIfNeed(new Action<Control, int, int>(_panel_ViewBtns.Controls.Add),
                            _defBtnA, 2, 0);
                        _viewBtns.Add(_defBtnA);

                        _panel_ViewBtns.InvokeRemoveChild(_btn_Null_4);
                        _panel_ViewBtns.InvokeIfNeed(new Action<Control, int, int>(_panel_ViewBtns.Controls.Add),
                            _defBtnB, 3, 0);
                        _viewBtns.Add(_defBtnB);
                    }
                    else
                    {
                        _panel_ViewBtns.InvokeRemoveChild(_defBtnA);
                        _panel_ViewBtns.InvokeIfNeed(new Action<Control, int, int>(_panel_ViewBtns.Controls.Add),
                            _btn_Null_3, 2, 0);
                        _viewBtns.Remove(_defBtnA);

                        _panel_ViewBtns.InvokeRemoveChild(_defBtnB);
                        _panel_ViewBtns.InvokeIfNeed(new Action<Control, int, int>(_panel_ViewBtns.Controls.Add),
                            _btn_Null_4, 3, 0);
                        _viewBtns.Remove(_defBtnB);
                    }
                }
                if (cmd.Key == 1300)
                {
                    if (!cmd.Value)
                    {
                        if (_panel_ViewBtns.Controls.Contains(_defBtnA))
                        {
                            _panel_ViewBtns.InvokeRemoveChild(_defBtnA);
                            _viewBtns.Remove(_defBtnA);
                        }
                        if (!_panel_ViewBtns.Controls.Contains(_btn_Null_3))
                        {
                            _panel_ViewBtns.InvokeIfNeed(new Action<Control, int, int>(_panel_ViewBtns.Controls.Add),
                                _btn_Null_3, 2, 0);
                        }

                        if (_panel_ViewBtns.Controls.Contains(_defBtnB))
                        {
                            _panel_ViewBtns.InvokeRemoveChild(_defBtnB);
                            _viewBtns.Remove(_defBtnB);
                        }
                        if (!_panel_ViewBtns.Controls.Contains(_btn_Null_4))
                        {
                            _panel_ViewBtns.InvokeIfNeed(new Action<Control, int, int>(_panel_ViewBtns.Controls.Add),
                                _btn_Null_4, 3, 0);
                        }
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
            _statesControls.ForEach(a => ((DefState) a).Dispose());
        }
    }

    public class TableStateInfo
    {
        public Int32 ID { get; set; }
        public Int32 LogicID { get; set; }
        public String Description { get; set; }
    }
}
