using System;
using System.Windows.Forms;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using System.Collections.Generic;
using CommonControls;
using YunDa.JC.MMI.Common;
using System.Drawing;
using MMI.Facility.Interface.Extension;
using ShenHuaHaoTMS.Common;
using YunDa.JC.MMI.Common.Extensions;
using ShenHuaHaoTMS.DefControls;
using ShenHuaHaoTMS.View.V1;

namespace ShenHuaHaoTMS.View
{
    public partial class SH_V1_MainView : UserControl, IView, IDisposable, IDataListener
    {
        public int ID { get; set; }

        private ListenViewChangedBridge m_ListenView;

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

                viewControl();
            }
        }

        private void viewControl()
        {
            if (_isShow) //显示
            {
                if (!ViewManger.Contains(this))
                {
                    _allFaluts.ForEach(a =>
                    {
                        if (a.IsOK && _allFaluts.Count > 1)
                        {
                            _allFaluts.Remove(a);
                        }
                    });
                    ViewManger.Add(this);
                    GlobalParam.Instance.InitParam.RegistDataListener(m_ListenView);
                    this.InvokeShow();;
                    this.InvokeInvalidate();

                    gridVisibleTableLayoutPanel9.InvokeInvalidate();
                    var c = gridVisibleTableLayoutPanel9.Controls[0];
                    c.InvokeInvalidate();

                    //IsListening = true;
                    _dataService.WriteService.ChangeBool(808, true);
                }
            }
            else //隐藏
            {
                if (ViewManger.Contains(this))
                {
                    //_timer.Stop();
                    ViewManger.Remove(this);
                    this.InvokeHide();;
                    _viewBtns.ForEach(a => a.IsDown = false);
                    GlobalParam.Instance.InitParam.UnregistDataListener(m_ListenView);
                    //IsListening = false;
                    _dataService.WriteService.ChangeBool(808, false);
                }
            }
        }

        private bool _isShow = false;

        public ViewManager ViewManger { get; set; }
        private BlackView _bv = null;

        private List<DefButton> _viewBtns = new List<DefButton>();
        private List<DefColumnChart> _defColumnChart = new List<DefColumnChart>();
        private ICommunicationDataService _dataService;
        private List<ILogic> _statesControls = new List<ILogic>();
        private List<FalutInfo> _allFaluts = new List<FalutInfo>();
        private SH_V1_Main_C1 _c1 = null;
        private SH_V1_Main_C2 _c2 = null;

        public SH_V1_MainView()
        {
            InitializeComponent();
        }

        public SH_V1_MainView(Int32 id, ViewManager viewManager, ICommunicationDataService dataService, BlackView bv,
            SubsystemInitParam initParam)
        {
            InitializeComponent();
            Load += SH_V1_MainView_Load;

            SetStyle(
                ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer, true);

            Dock = DockStyle.Fill;
            Margin = new Padding(0);

            _c1 = new SH_V1_Main_C1(dataService);
            _c2 = new SH_V1_Main_C2(dataService);

            ID = id;
            ViewManger = viewManager;
            ViewManger.Register(this);

            _dataService = dataService;

            m_ListenView.BoolChanged += onViewChanged;
            GlobalParam.Instance.InitParam.RegistDataListener(this);

            _viewBtns = new List<DefButton>()
            {
                _btn_1,
                _btn_2,
                _btn_3,
                _btn_4,
                _btn_5,
                _btn_6,
                _btn_7
            };

            _defColumnChart = new List<DefColumnChart>()
            {
                _columnChart_Voltage,
                _columnChart_Electric,
                _columnChart_Traction
            };

            _statesControls = new List<ILogic>()
            {
                _defState_3,
                _defState_4,
                _defState_5,
                _defState_6,
                _defState_7,
                _defState_8,
                _defState_9,
                _defState_10,
                _defState_12,
                _defState_13,
                _defState_14,
                defState1
            };

            _bv = bv;
            _bv.FalutChangedA += _bv_FalutChangedA;
            _bv.FalutChangedB += _bv_FalutChangedB;
            _bv.LeftInfoChanged += _bv_LeftInfoChanged;
            _bv.MiddleInfoChanged += _bv_MiddleInfoChanged;

            _title_TrainName.DefText = "神华号 " + _bv.LocalTrainInfo.RealTrainID + "A";


            GlobalTimer.Instance.TimersElapsed500MS += _timer_Elapsed;

            if (_panel.Controls.Contains(_c2))
            {
                _panel.InvokeRemoveChild(_c2);
            }
            if (!_panel.Controls.Contains(_c1))
            {
                _panel.InvokeAddChild(_c1);
            }

            initParam.DataPackage.ServiceManager.GetService<IDisposeService>().RegistDisposableObject(this);
        }

        void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //故障
            if (_allFaluts == null || _allFaluts.Count == 0)
            {
                FalutPointOut = "";
                defLabel3.DefText = "";
                defLabel3.BackColorNew = Color.Black;
            }
            else
            {
                if (_allFaluts.Count == 1 && _allFaluts[0].IsOK)
                {
                    defLabel3.DefText = _allFaluts[0].PointOut;
                    defLabel3.BackColorNew = Color.Red;
                }
                else
                {
                    if (_falutIndex >= _allFaluts.Count)
                    {
                        _falutIndex = _allFaluts.Count - 1;
                    }
                    if (_index < 10)
                    {
                        defLabel3.DefText = _falutText;
                        defLabel3.BackColorNew = Color.Red;
                    }
                    else
                    {
                        defLabel3.DefText = "";
                        defLabel3.BackColorNew = Color.Black;
                    }
                    _falutText = _allFaluts[_falutIndex].PointOut;
                    FalutPointOut = _allFaluts[_falutIndex].PointOut;
                    if (_index == 19)
                    {
                        _falutIndex = (_falutIndex + 1)%_allFaluts.Count;
                    }
                }
            }

            //左边
            if (_leftInfos == null || _leftInfos.Count == 0)
            {
                defLabel1.DefText = "";
                defLabel1.BackColorNew = Color.Black;
            }
            else
            {
                if (_leftInfoIndex >= _leftInfos.Count)
                {
                    _leftInfoIndex = _leftInfos.Count - 1;
                }
                defLabel1.BackColorNew = Color.FromArgb(255, 144, 0);
                defLabel1.DefText = _leftInfos[_leftInfoIndex].Description;
                if (_index == 19)
                {
                    _leftInfoIndex = (_leftInfoIndex + 1)%_leftInfos.Count;
                }
            }

            //中间
            if (_middleInfos == null || _middleInfos.Count == 0)
            {
                defLabel2.DefText = "";
                defLabel2.BackColorNew = Color.Black;
            }
            else
            {
                if (_middleInfoIndex >= _middleInfos.Count)
                {
                    _middleInfoIndex = _middleInfos.Count - 1;
                }
                defLabel2.BackColorNew = Color.White;
                defLabel2.DefText = _middleInfos[_middleInfoIndex].Description;
                if (_index == 19)
                {
                    _middleInfoIndex = (_middleInfoIndex + 1)%_middleInfos.Count;
                }
            }
            _index = (_index + 1)%20;
        }

        private List<Info> _middleInfos = new List<Info>();

        void _bv_MiddleInfoChanged(Info info)
        {
            if (info == null)
            {
                return;
            }

            Info io = _middleInfos.Find(a => a.LogicID == info.LogicID);
            if (info.IsSet) //设置
            {
                if (io == null)
                {
                    _middleInfos.Add(info);
                }
            }
            else
            {
                if (io != null)
                {
                    _middleInfos.Remove(io);
                }
            }
        }

        private List<Info> _leftInfos = new List<Info>();

        void _bv_LeftInfoChanged(Info info)
        {
            if (info == null)
            {
                return;
            }

            Info io = _leftInfos.Find(a => a.LogicID == info.LogicID);
            if (info.IsSet) //设置
            {
                if (io == null)
                {
                    _leftInfos.Add(info);
                }
            }
            else
            {
                if (io != null)
                {
                    _leftInfos.Remove(io);
                }
            }
        }

        private Int32 _falutIndex = 0;
        private Int32 _leftInfoIndex = 0;
        private Int32 _middleInfoIndex = 0;
        private Int32 _falutBackIndex = 0;
        private Int32 _index = 0;

        public static String FalutPointOut = "";

        private String _falutText = "";

        void _timer_Tick(object sender, EventArgs e)
        {
            //故障
            if (_allFaluts == null || _allFaluts.Count == 0)
            {
                FalutPointOut = "";
                defLabel3.DefText = "";
                defLabel3.BackColorNew = Color.Black;
            }
            else
            {
                if (_allFaluts.Count == 1 && _allFaluts[0].IsOK)
                {
                    defLabel3.DefText = _allFaluts[0].PointOut;
                    defLabel3.BackColorNew = Color.Red;
                }
                else
                {
                    if (_falutIndex >= _allFaluts.Count)
                    {
                        _falutIndex = _allFaluts.Count - 1;
                    }
                    if (_index < 10)
                    {
                        defLabel3.DefText = _falutText;
                        defLabel3.BackColorNew = Color.Red;
                    }
                    else
                    {
                        defLabel3.DefText = "";
                        defLabel3.BackColorNew = Color.Black;
                    }
                    _falutText = _allFaluts[_falutIndex].PointOut;
                    FalutPointOut = _allFaluts[_falutIndex].PointOut;
                    if (_index == 19)
                    {
                        _falutIndex = (_falutIndex + 1)%_allFaluts.Count;
                    }
                }
            }

            //左边
            if (_leftInfos == null || _leftInfos.Count == 0)
            {
                defLabel1.DefText = "";
                defLabel1.BackColorNew = Color.Black;
            }
            else
            {
                if (_leftInfoIndex >= _leftInfos.Count)
                {
                    _leftInfoIndex = _leftInfos.Count - 1;
                }
                defLabel1.BackColorNew = Color.FromArgb(255, 144, 0);
                defLabel1.DefText = _leftInfos[_leftInfoIndex].Description;
                if (_index == 19)
                {
                    _leftInfoIndex = (_leftInfoIndex + 1)%_leftInfos.Count;
                }
            }

            //中间
            if (_middleInfos == null || _middleInfos.Count == 0)
            {
                defLabel2.DefText = "";
                defLabel2.BackColorNew = Color.Black;
            }
            else
            {
                if (_middleInfoIndex >= _middleInfos.Count)
                {
                    _middleInfoIndex = _middleInfos.Count - 1;
                }
                defLabel2.BackColorNew = Color.White;
                defLabel2.DefText = _middleInfos[_middleInfoIndex].Description;
                if (_index == 19)
                {
                    _middleInfoIndex = (_middleInfoIndex + 1)%_middleInfos.Count;
                }
            }
            _index = (_index + 1)%20;
        }

        void _bv_FalutChangedB(FalutState fs)
        {
            if (fs == null)
            {
                return;
            }

            FalutInfo fi = _allFaluts.Find(a => a.LogicID == fs.CurrentFalut.LogicID);
            if (fs.IsSet)
            {
                if (fi == null)
                {
                    _allFaluts.Add(fs.CurrentFalut);
                }
            }
            else
            {
                if (fi != null)
                {
                    _allFaluts.Remove(fi);
                }
            }
        }

        void _bv_FalutChangedA(FalutState fs)
        {
            if (fs == null)
            {
                return;
            }

            FalutInfo fi = _allFaluts.Find(a => a.LogicID == fs.CurrentFalut.LogicID);
            if (fs.IsSet)
            {
                if (fi == null)
                {
                    _allFaluts.Add(fs.CurrentFalut);
                }
            }
            else
            {
                if (fi != null)
                {
                    _allFaluts.Remove(fi);
                }
            }
        }

        void SH_V1_MainView_Load(object sender, EventArgs e)
        {
            gridVisibleTableLayoutPanel9.Invalidate();
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
                if (cmd.Key >= 50 && cmd.Key < 825)
                {
                    for (int i = 0; i < _defColumnChart.Count; i++)
                    {
                        _defColumnChart[i].SetValue(cmd.Key, cmd.Value);
                    }

                    foreach (var item in _statesControls)
                    {
                        item.SetValue(cmd.Key, cmd.Value);
                    }
                }
            }
        }

        #region 定速

        public bool IsLimitSpeedBlack
        {
            set
            {
                if (_isLimitSpeedBlack == value)
                {
                    return;
                }

                _isLimitSpeedBlack = value;

                if (value) //显示黑底定速
                {
                    _btn_8.DefText = "定速";
                    _btn_8.IsDown = false;
                    _btn_8.InvokeInvalidate();
                }
                else
                {
                    if (!_isLimitSpeedYellow)
                    {
                        _btn_8.DefText = "";
                        _btn_8.IsDown = false;
                        _btn_8.InvokeInvalidate();
                    }
                    else
                    {
                        _btn_8.DefText = "定速";
                        _btn_8.IsDown = true;
                        _btn_8.InvokeInvalidate();
                    }
                }
            }
        }

        private bool _isLimitSpeedBlack = false;

        public bool IsLimitSpeedYellow
        {
            set
            {
                if (_isLimitSpeedYellow == value)
                {
                    return;
                }

                _isLimitSpeedYellow = value;

                if (value) //显示黑底定速
                {
                    _btn_8.DefText = "定速";
                    _btn_8.IsDown = true;
                    _btn_8.InvokeInvalidate();
                }
                else
                {
                    if (!_isLimitSpeedBlack)
                    {
                        _btn_8.DefText = "";
                        _btn_8.IsDown = false;
                        _btn_8.InvokeInvalidate();
                    }
                    else
                    {
                        _btn_8.DefText = "定速";
                        _btn_8.IsDown = false;
                        _btn_8.InvokeInvalidate();
                    }
                }
            }
        }

        private bool _isLimitSpeedYellow = false;

        #endregion

        #region 连挂

        public bool IsConnectedBlack
        {
            set
            {
                if (_isConnectedBlack == value)
                {
                    return;
                }

                _isConnectedBlack = value;

                if (value) //显示黑底定速
                {
                    _btn_9.DefText = "联挂";
                    _btn_9.IsDown = false;
                    _btn_9.InvokeInvalidate();
                }
                else if (!_isConnectedYellow)
                {
                    _btn_9.DefText = "";
                    _btn_9.IsDown = false;
                    _btn_9.InvokeInvalidate();
                }
            }
        }

        private bool _isConnectedBlack = false;

        public bool IsConnectedYellow
        {
            set
            {
                if (_isConnectedYellow == value)
                {
                    return;
                }

                _isConnectedYellow = value;

                if (value) //显示黑底定速
                {
                    _btn_9.DefText = "联挂";
                    _btn_9.IsDown = true;
                    _btn_9.InvokeInvalidate();
                }
                else if (!_isConnectedBlack)
                {
                    _btn_9.DefText = "";
                    _btn_9.IsDown = false;
                    _btn_9.InvokeInvalidate();
                }
            }
        }

        private bool _isConnectedYellow = false;

        #endregion

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

                for (int i = 0; i < _defColumnChart.Count; i++)
                {
                    _defColumnChart[i].SetValue(cmd.Key, cmd.Value);
                }

                if (cmd.Key == 1311) //定速黑底显示
                {
                    IsLimitSpeedBlack = cmd.Value;
                }
                else if (cmd.Key == 1312) //定速黄底显示
                {
                    IsLimitSpeedYellow = cmd.Value;
                }
                else if (cmd.Key == 1315) //连挂黑底显示
                {
                    IsConnectedBlack = cmd.Value;
                }
                else if (cmd.Key == 1316) //连挂黄底显示
                {
                    IsConnectedYellow = cmd.Value;
                }

                else if (cmd.Key == 1300)
                {
                    IsLimitSpeedBlack = false;
                    IsLimitSpeedYellow = false;
                    IsConnectedBlack = false;
                    IsConnectedYellow = false;
                }

                if (cmd.Key == 1323 && cmd.Value) //编组成功
                {
                    if (_panel.Controls.Contains(_c1))
                    {
                        _panel.InvokeRemoveChild(_c1); ;
                    }
                    if (!_panel.Controls.Contains(_c2))
                    {
                        _panel.InvokeAddChild(_c2);
                    }
                    _columnChart_Traction.SingleData = 20;
                    _columnChart_Traction.ColumnConfigInfos[0].Name = "车1-A";
                    _columnChart_Traction.ColumnConfigInfos[0].SetLogicID(416, 415, 420, 419);
                    _columnChart_Traction.ColumnConfigInfos[0].SetBoolLogicID(3344, 3344, 3345, 3345);
                    //_columnChart_Traction.ColumnConfigInfos[0].ColumnChartLogicInfos[0].FloatLogicID = 416;
                    //_columnChart_Traction.ColumnConfigInfos[0].ColumnChartLogicInfos[1].FloatLogicID = 415;
                    //_columnChart_Traction.ColumnConfigInfos[0].ColumnChartLogicInfos[2].FloatLogicID = 420;
                    //_columnChart_Traction.ColumnConfigInfos[0].ColumnChartLogicInfos[3].FloatLogicID = 419;
                    _columnChart_Traction.ColumnConfigInfos[1].Name = "车1-B";
                    _columnChart_Traction.ColumnConfigInfos[1].SetLogicID(418, 417, 422, 421);
                    _columnChart_Traction.ColumnConfigInfos[1].SetBoolLogicID(3744, 3744, 3745, 3745);
                    //_columnChart_Traction.ColumnConfigInfos[1].ColumnChartLogicInfos[0].FloatLogicID = 418;
                    //_columnChart_Traction.ColumnConfigInfos[1].ColumnChartLogicInfos[1].FloatLogicID = 417;
                    //_columnChart_Traction.ColumnConfigInfos[1].ColumnChartLogicInfos[2].FloatLogicID = 422;
                    //_columnChart_Traction.ColumnConfigInfos[1].ColumnChartLogicInfos[3].FloatLogicID = 421;
                    _columnChart_Traction.ColumnConfigInfos[2].Name = "车2-A";
                    _columnChart_Traction.ColumnConfigInfos[2].SetLogicID(466, 465, 470, 469);
                    _columnChart_Traction.ColumnConfigInfos[2].SetBoolLogicID(4144, 4144, 4145, 4145);
                    //_columnChart_Traction.ColumnConfigInfos[2].ColumnChartLogicInfos[0].FloatLogicID = 466;
                    //_columnChart_Traction.ColumnConfigInfos[2].ColumnChartLogicInfos[1].FloatLogicID = 465;
                    //_columnChart_Traction.ColumnConfigInfos[2].ColumnChartLogicInfos[2].FloatLogicID = 470;
                    //_columnChart_Traction.ColumnConfigInfos[2].ColumnChartLogicInfos[3].FloatLogicID = 469;
                    _columnChart_Traction.ColumnConfigInfos[3].Name = "车2-B";
                    _columnChart_Traction.ColumnConfigInfos[3].SetLogicID(468, 467, 472, 471);
                    _columnChart_Traction.ColumnConfigInfos[3].SetBoolLogicID(4544, 4544, 4545, 4545);
                    //_columnChart_Traction.ColumnConfigInfos[3].ColumnChartLogicInfos[0].FloatLogicID = 468;
                    //_columnChart_Traction.ColumnConfigInfos[3].ColumnChartLogicInfos[1].FloatLogicID = 467;
                    //_columnChart_Traction.ColumnConfigInfos[3].ColumnChartLogicInfos[2].FloatLogicID = 472;
                    //_columnChart_Traction.ColumnConfigInfos[3].ColumnChartLogicInfos[3].FloatLogicID = 471;
                    //_columnChart_Traction.InvokeInvalidate();
                    _columnChart_Traction.SetValue(416, _dataService.ReadService.ReadOnlyFloatDictionary[416]);
                    _columnChart_Traction.SetValue(415, _dataService.ReadService.ReadOnlyFloatDictionary[415]);
                    _columnChart_Traction.SetValue(420, _dataService.ReadService.ReadOnlyFloatDictionary[420]);
                    _columnChart_Traction.SetValue(419, _dataService.ReadService.ReadOnlyFloatDictionary[419]);
                    _columnChart_Traction.SetValue(418, _dataService.ReadService.ReadOnlyFloatDictionary[418]);
                    _columnChart_Traction.SetValue(417, _dataService.ReadService.ReadOnlyFloatDictionary[417]);
                    _columnChart_Traction.SetValue(422, _dataService.ReadService.ReadOnlyFloatDictionary[422]);
                    _columnChart_Traction.SetValue(421, _dataService.ReadService.ReadOnlyFloatDictionary[421]);
                    _columnChart_Traction.SetValue(466, _dataService.ReadService.ReadOnlyFloatDictionary[466]);
                    _columnChart_Traction.SetValue(465, _dataService.ReadService.ReadOnlyFloatDictionary[465]);
                    _columnChart_Traction.SetValue(470, _dataService.ReadService.ReadOnlyFloatDictionary[470]);
                    _columnChart_Traction.SetValue(469, _dataService.ReadService.ReadOnlyFloatDictionary[469]);
                    _columnChart_Traction.SetValue(468, _dataService.ReadService.ReadOnlyFloatDictionary[468]);
                    _columnChart_Traction.SetValue(467, _dataService.ReadService.ReadOnlyFloatDictionary[467]);
                    _columnChart_Traction.SetValue(472, _dataService.ReadService.ReadOnlyFloatDictionary[472]);
                    _columnChart_Traction.SetValue(471, _dataService.ReadService.ReadOnlyFloatDictionary[471]);
                    _columnChart_Traction.InvokeInvalidate();
                }
                else if (cmd.Key == 1323 && !cmd.Value) //未编组
                {
                    if (_panel.Controls.Contains(_c2))
                    {
                        _panel.InvokeRemoveChild(_c2);
                    }
                    if (!_panel.Controls.Contains(_c1))
                    {
                        _panel.InvokeAddChild(_c1);
                    }
                    _columnChart_Traction.SingleData = 10;
                    _columnChart_Traction.ColumnConfigInfos[0].Name = "A-1";
                    _columnChart_Traction.ColumnConfigInfos[0].SetLogicID(108, 107, 110, 109);
                    _columnChart_Traction.ColumnConfigInfos[0].SetBoolLogicID(1298, 1298, 1299, 1299);
                    //_columnChart_Traction.ColumnConfigInfos[0].ColumnChartLogicInfos[0].FloatLogicID = 108;
                    //_columnChart_Traction.ColumnConfigInfos[0].ColumnChartLogicInfos[1].FloatLogicID = 107;
                    //_columnChart_Traction.ColumnConfigInfos[0].ColumnChartLogicInfos[2].FloatLogicID = 110;
                    //_columnChart_Traction.ColumnConfigInfos[0].ColumnChartLogicInfos[3].FloatLogicID = 109;
                    _columnChart_Traction.ColumnConfigInfos[1].Name = "A-2";
                    _columnChart_Traction.ColumnConfigInfos[1].SetLogicID(112, 111, 114, 113);
                    _columnChart_Traction.ColumnConfigInfos[1].SetBoolLogicID(1298, 1298, 1299, 1299);
                    //_columnChart_Traction.ColumnConfigInfos[1].ColumnChartLogicInfos[0].FloatLogicID = 112;
                    //_columnChart_Traction.ColumnConfigInfos[1].ColumnChartLogicInfos[1].FloatLogicID = 111;
                    //_columnChart_Traction.ColumnConfigInfos[1].ColumnChartLogicInfos[2].FloatLogicID = 114;
                    //_columnChart_Traction.ColumnConfigInfos[1].ColumnChartLogicInfos[3].FloatLogicID = 113;
                    _columnChart_Traction.ColumnConfigInfos[2].Name = "A-3";
                    _columnChart_Traction.ColumnConfigInfos[2].SetLogicID(116, 115, 118, 117);
                    _columnChart_Traction.ColumnConfigInfos[2].SetBoolLogicID(1298, 1298, 1299, 1299);
                    //_columnChart_Traction.ColumnConfigInfos[2].ColumnChartLogicInfos[0].FloatLogicID = 116;
                    //_columnChart_Traction.ColumnConfigInfos[2].ColumnChartLogicInfos[1].FloatLogicID = 115;
                    //_columnChart_Traction.ColumnConfigInfos[2].ColumnChartLogicInfos[2].FloatLogicID = 118;
                    //_columnChart_Traction.ColumnConfigInfos[2].ColumnChartLogicInfos[3].FloatLogicID = 117;
                    _columnChart_Traction.ColumnConfigInfos[3].Name = "A-4";
                    _columnChart_Traction.ColumnConfigInfos[3].SetLogicID(120, 119, 122, 121);
                    _columnChart_Traction.ColumnConfigInfos[3].SetBoolLogicID(1298, 1298, 1299, 1299);
                    //_columnChart_Traction.ColumnConfigInfos[3].ColumnChartLogicInfos[0].FloatLogicID = 120;
                    //_columnChart_Traction.ColumnConfigInfos[3].ColumnChartLogicInfos[1].FloatLogicID = 119;
                    //_columnChart_Traction.ColumnConfigInfos[3].ColumnChartLogicInfos[2].FloatLogicID = 122;
                    //_columnChart_Traction.ColumnConfigInfos[3].ColumnChartLogicInfos[3].FloatLogicID = 121;
                    _columnChart_Traction.InvokeInvalidate();
                    _columnChart_Traction.SetValue(108, _dataService.ReadService.ReadOnlyFloatDictionary[108]);
                    _columnChart_Traction.SetValue(107, _dataService.ReadService.ReadOnlyFloatDictionary[107]);
                    _columnChart_Traction.SetValue(110, _dataService.ReadService.ReadOnlyFloatDictionary[110]);
                    _columnChart_Traction.SetValue(109, _dataService.ReadService.ReadOnlyFloatDictionary[109]);
                    _columnChart_Traction.SetValue(112, _dataService.ReadService.ReadOnlyFloatDictionary[112]);
                    _columnChart_Traction.SetValue(111, _dataService.ReadService.ReadOnlyFloatDictionary[111]);
                    _columnChart_Traction.SetValue(114, _dataService.ReadService.ReadOnlyFloatDictionary[114]);
                    _columnChart_Traction.SetValue(113, _dataService.ReadService.ReadOnlyFloatDictionary[113]);
                    _columnChart_Traction.SetValue(116, _dataService.ReadService.ReadOnlyFloatDictionary[116]);
                    _columnChart_Traction.SetValue(115, _dataService.ReadService.ReadOnlyFloatDictionary[115]);
                    _columnChart_Traction.SetValue(118, _dataService.ReadService.ReadOnlyFloatDictionary[118]);
                    _columnChart_Traction.SetValue(117, _dataService.ReadService.ReadOnlyFloatDictionary[117]);
                    _columnChart_Traction.SetValue(120, _dataService.ReadService.ReadOnlyFloatDictionary[120]);
                    _columnChart_Traction.SetValue(119, _dataService.ReadService.ReadOnlyFloatDictionary[119]);
                    _columnChart_Traction.SetValue(122, _dataService.ReadService.ReadOnlyFloatDictionary[122]);
                    _columnChart_Traction.SetValue(121, _dataService.ReadService.ReadOnlyFloatDictionary[121]);
                    _columnChart_Traction.InvokeInvalidate();
                }
            }
        }

        private void DefClick(object sender, ButtonClickArgs e)
        {
            ViewManger.CurrentViewID = e.ViewID;
        }

        public void InvalidateNew()
        {
            gridVisibleTableLayoutPanel10.InvokeInvalidate();
            gridVisibleTableLayoutPanel9.InvokeInvalidate();
        }

        private void gridVisibleTableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        public void Dispose()
        {
            _statesControls.ForEach(a => ((DefState) a).Dispose());
        }

        public bool IsListening { get; private set; }

        public void OnBoolItemChanged(ref KeyValuePair<int, bool> it)
        {
            foreach (var item in _statesControls)
            {
                if (item is DefState)
                {
                    var temp = item as DefState;
                    temp.InvokeIfNeed(new Action<Int32, Boolean>(item.SetState), it.Key, it.Value);
                }
            }

            for (int i = 0; i < _defColumnChart.Count; i++)
            {
                _defColumnChart[i].SetValue(it.Key, it.Value);
            }

            if (it.Key == 1311) //定速黑底显示
            {
                IsLimitSpeedBlack = it.Value;
            }
            else if (it.Key == 1312) //定速黄底显示
            {
                IsLimitSpeedYellow = it.Value;
            }
            else if (it.Key == 1315) //连挂黑底显示
            {
                IsConnectedBlack = it.Value;
            }
            else if (it.Key == 1316) //连挂黄底显示
            {
                IsConnectedYellow = it.Value;
            }

            else if (it.Key == 1300)
            {
                IsLimitSpeedBlack = false;
                IsLimitSpeedYellow = false;
                IsConnectedBlack = false;
                IsConnectedYellow = false;
            }

            if (it.Key == 1323 && it.Value) //编组成功
            {
                if (_panel.Controls.Contains(_c1))
                {
                    _panel.InvokeRemoveChild(_c1);
                }
                if (!_panel.Controls.Contains(_c2))
                {
                    _panel.InvokeAddChild(_c2);
                }
                _columnChart_Traction.SingleData = 20;
                _columnChart_Traction.ColumnConfigInfos[0].Name = "车1-A";
                _columnChart_Traction.ColumnConfigInfos[0].SetLogicID(416, 415, 420, 419);
                _columnChart_Traction.ColumnConfigInfos[0].SetBoolLogicID(3344, 3344, 3345, 3345);
                _columnChart_Traction.ColumnConfigInfos[1].Name = "车1-B";
                _columnChart_Traction.ColumnConfigInfos[1].SetLogicID(418, 417, 422, 421);
                _columnChart_Traction.ColumnConfigInfos[1].SetBoolLogicID(3744, 3744, 3745, 3745);
                _columnChart_Traction.ColumnConfigInfos[2].Name = "车2-A";
                _columnChart_Traction.ColumnConfigInfos[2].SetLogicID(466, 465, 470, 469);
                _columnChart_Traction.ColumnConfigInfos[2].SetBoolLogicID(4144, 4144, 4145, 4145);
                _columnChart_Traction.ColumnConfigInfos[3].Name = "车2-B";
                _columnChart_Traction.ColumnConfigInfos[3].SetLogicID(468, 467, 472, 471);
                _columnChart_Traction.ColumnConfigInfos[3].SetBoolLogicID(4544, 4544, 4545, 4545);
                _columnChart_Traction.SetValue(416, _dataService.ReadService.ReadOnlyFloatDictionary[416]);
                _columnChart_Traction.SetValue(415, _dataService.ReadService.ReadOnlyFloatDictionary[415]);
                _columnChart_Traction.SetValue(420, _dataService.ReadService.ReadOnlyFloatDictionary[420]);
                _columnChart_Traction.SetValue(419, _dataService.ReadService.ReadOnlyFloatDictionary[419]);
                _columnChart_Traction.SetValue(418, _dataService.ReadService.ReadOnlyFloatDictionary[418]);
                _columnChart_Traction.SetValue(417, _dataService.ReadService.ReadOnlyFloatDictionary[417]);
                _columnChart_Traction.SetValue(422, _dataService.ReadService.ReadOnlyFloatDictionary[422]);
                _columnChart_Traction.SetValue(421, _dataService.ReadService.ReadOnlyFloatDictionary[421]);
                _columnChart_Traction.SetValue(466, _dataService.ReadService.ReadOnlyFloatDictionary[466]);
                _columnChart_Traction.SetValue(465, _dataService.ReadService.ReadOnlyFloatDictionary[465]);
                _columnChart_Traction.SetValue(470, _dataService.ReadService.ReadOnlyFloatDictionary[470]);
                _columnChart_Traction.SetValue(469, _dataService.ReadService.ReadOnlyFloatDictionary[469]);
                _columnChart_Traction.SetValue(468, _dataService.ReadService.ReadOnlyFloatDictionary[468]);
                _columnChart_Traction.SetValue(467, _dataService.ReadService.ReadOnlyFloatDictionary[467]);
                _columnChart_Traction.SetValue(472, _dataService.ReadService.ReadOnlyFloatDictionary[472]);
                _columnChart_Traction.SetValue(471, _dataService.ReadService.ReadOnlyFloatDictionary[471]);
                _columnChart_Traction.InvokeInvalidate();
            }
            else if (it.Key == 1323 && !it.Value) //未编组
            {
                if (_panel.Controls.Contains(_c2))
                {
                    _panel.InvokeRemoveChild(_c2);
                }
                if (!_panel.Controls.Contains(_c1))
                {
                    _panel.InvokeAddChild(_c1);
                }
                _columnChart_Traction.SingleData = 10;
                _columnChart_Traction.ColumnConfigInfos[0].Name = "A-1";
                _columnChart_Traction.ColumnConfigInfos[0].SetLogicID(108, 107, 110, 109);
                _columnChart_Traction.ColumnConfigInfos[0].SetBoolLogicID(1298, 1298, 1299, 1299);
                _columnChart_Traction.ColumnConfigInfos[1].Name = "A-2";
                _columnChart_Traction.ColumnConfigInfos[1].SetLogicID(112, 111, 114, 113);
                _columnChart_Traction.ColumnConfigInfos[1].SetBoolLogicID(1298, 1298, 1299, 1299);
                _columnChart_Traction.ColumnConfigInfos[2].Name = "A-3";
                _columnChart_Traction.ColumnConfigInfos[2].SetLogicID(116, 115, 118, 117);
                _columnChart_Traction.ColumnConfigInfos[2].SetBoolLogicID(1298, 1298, 1299, 1299);
                _columnChart_Traction.ColumnConfigInfos[3].Name = "A-4";
                _columnChart_Traction.ColumnConfigInfos[3].SetLogicID(120, 119, 122, 121);
                _columnChart_Traction.ColumnConfigInfos[3].SetBoolLogicID(1298, 1298, 1299, 1299);
                _columnChart_Traction.InvokeInvalidate();
                _columnChart_Traction.SetValue(108, _dataService.ReadService.ReadOnlyFloatDictionary[108]);
                _columnChart_Traction.SetValue(107, _dataService.ReadService.ReadOnlyFloatDictionary[107]);
                _columnChart_Traction.SetValue(110, _dataService.ReadService.ReadOnlyFloatDictionary[110]);
                _columnChart_Traction.SetValue(109, _dataService.ReadService.ReadOnlyFloatDictionary[109]);
                _columnChart_Traction.SetValue(112, _dataService.ReadService.ReadOnlyFloatDictionary[112]);
                _columnChart_Traction.SetValue(111, _dataService.ReadService.ReadOnlyFloatDictionary[111]);
                _columnChart_Traction.SetValue(114, _dataService.ReadService.ReadOnlyFloatDictionary[114]);
                _columnChart_Traction.SetValue(113, _dataService.ReadService.ReadOnlyFloatDictionary[113]);
                _columnChart_Traction.SetValue(116, _dataService.ReadService.ReadOnlyFloatDictionary[116]);
                _columnChart_Traction.SetValue(115, _dataService.ReadService.ReadOnlyFloatDictionary[115]);
                _columnChart_Traction.SetValue(118, _dataService.ReadService.ReadOnlyFloatDictionary[118]);
                _columnChart_Traction.SetValue(117, _dataService.ReadService.ReadOnlyFloatDictionary[117]);
                _columnChart_Traction.SetValue(120, _dataService.ReadService.ReadOnlyFloatDictionary[120]);
                _columnChart_Traction.SetValue(119, _dataService.ReadService.ReadOnlyFloatDictionary[119]);
                _columnChart_Traction.SetValue(122, _dataService.ReadService.ReadOnlyFloatDictionary[122]);
                _columnChart_Traction.SetValue(121, _dataService.ReadService.ReadOnlyFloatDictionary[121]);
                _columnChart_Traction.InvokeInvalidate();
            }
        }

        public void OnFloatItemChanged(ref KeyValuePair<int, float> it)
        {
            if (it.Key >= 50 && it.Key < 825)
            {
                for (int i = 0; i < _defColumnChart.Count; i++)
                {
                    _defColumnChart[i].SetValue(it.Key, it.Value);
                }

                foreach (var item in _statesControls)
                {
                    item.SetValue(it.Key, it.Value);
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
