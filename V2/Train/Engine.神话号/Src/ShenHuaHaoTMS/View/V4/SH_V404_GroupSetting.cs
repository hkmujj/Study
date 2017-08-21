using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommonControls;
using CommonControls.Extensions;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using ShenHuaHaoTMS.Common;
using ShenHuaHaoTMS.Properties;
using YunDa.JC.MMI.Common;
using ShenHuaHaoTMS.DefControls;
using ShenHuaHaoTMS.View.V4;
using YunDa.JC.MMI.Common.Extensions;

namespace ShenHuaHaoTMS.View
{
    public partial class SH_V404_GroupSetting : UserControl, IView,IDisposable, IDataListener
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
                        defLabel12.InvokeSetDefText("");
                        this.InvokeHide();;
                        _viewBtns.ForEach(a => a.IsDown = false);
                        GlobalParam.Instance.InitParam.UnregistDataListener(m_ListenViewChanged);
                        _defBtn_Test.IsDown = false;
                    }
                }
            }
        }
        private bool _isShow = false;

        public ViewManager ViewManger { get; set; }
        private ICommunicationDataService _dataService;

        private List<DefButton> _viewBtns = new List<DefButton>();//跳转页面的按钮
        private List<DefButton> _intputBtns = new List<DefButton>();//数字键与右侧功能键
        private List<ILogic> _statesControls = new List<ILogic>();
        private List<DefLabel> _labels = new List<DefLabel>();
        private Int32 _index = 0;
        private BlackView _bv = null;
        private bool _isRe = true;
        private Int32 _count = 0;
        private SH_V401_StateShow _ss = null;

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


        public SH_V404_GroupSetting()
        {
            InitializeComponent();
        }

        public SH_V404_GroupSetting(Int32 id, ViewManager viewManager, ICommunicationDataService dataService, BlackView bv, SH_V401_StateShow ss, SubsystemInitParam initParam)
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
            Margin = new Padding(0);
            _dataService = dataService;
            GlobalParam.Instance.InitParam.RegistDataListener(this);
            m_ListenViewChanged.BoolChanged += onViewChanged;
            _bv = bv;

            ID = id;
            ViewManger = viewManager;
            ViewManger.Register(this);

            _viewBtns = new List<DefButton>()
            {
                _defBtn_Return_0
            };
            _intputBtns = new List<DefButton>()
            {
                _btn_Data_1,_btn_Data_2,_btn_Data_3,_btn_Data_4,_btn_Data_5,_btn_Data_6,_btn_Data_7,_btn_Data_8,_btn_Data_9,_btn_Data_10,
                _defBtn_ModeSelect,defButton1,defButton2,_defBtn_OK,_defBtn_Test
            };

            _statesControls = new List<ILogic>()
            {
                defState1
            };

            _labels = new List<DefLabel>()
            {
                _defLabel1,
                _defLabel2,
                _defLabel3,
                _defLabel4,
                _defLabel6,
                _defLabel7,
                _defLabel8,
                _defLabel9
            };
            _panel.Controls.Remove(defLabel11);
            _panel.Controls.Remove(_defLabel5);
            _panel.Controls.Remove(defLabel9);
            _panel.Controls.Remove(_defLabel6);

            _panel.Controls.Add(defLabel9, 0, 5);
            _panel.Controls.Add(_defLabel6, 1, 5);

            _title_TrainName.DefText = "神华号 " + bv.LocalTrainInfo.RealTrainID + "A";

            _bv.LocalTrainInfo.TrainID = _bv.LocalTrainInfo.RealTrainID;
            _defLabel2.DefText = _bv.LocalTrainInfo.TrainID.ToString();

            _ss = ss;

            IsTimerStartted = true;

            initParam.DataPackage.ServiceManager.GetService<IDisposeService>().RegistDisposableObject(this);
        }

        void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_isEable)
            {
                _isEable = false;
                _count++;
                if (_count == 3)
                {
                    defLabel12.InvokeSetDefText("设置成功");
                }
                else if (_count >= 4)
                {
                    _count = 0;
                    IsTimerStartted = false;
                    ViewManger.CurrentViewID = 4;
                }
                _isEable = true;
            }
        }

        private Boolean _isEable = true;
        private bool m_IsTimerStartted;

        void _timer_Tick(object sender, EventArgs e)
        {
            if (_isEable)
            {
                _isEable = false;
                _count++;
                if (_count == 3)
                {
                    defLabel12.InvokeSetDefText("设置成功");
                }
                else if (_count >= 4)
                {
                    _count = 0;
                    IsTimerStartted = false;
                    ViewManger.CurrentViewID = 4;
                }
                _isEable = true;
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

                if (cmd.Key == 1300 && cmd.Value)
                {
                    if (_bv.LocalTrainInfo.ControlType == ControlType.主控)
                    {
                        _dataService.WriteService.ChangeBool(809, true);
                    }
                    else
                    {
                        _bv.LocalTrainInfo.Number = Convert.ToInt32(_defLabel5.DefText);
                        _dataService.WriteService.ChangeBool(809 + _bv.LocalTrainInfo.Number, true);
                    }

                    _labels[_index].BackgroundImage = Resources.btn_White;
                    _index = 0;
                    _labels[_index].BackgroundImage = Resources.btn_Blue1;
                    _panel.InvokeRemoveChild(defLabel11);
                    _panel.InvokeRemoveChild(_defLabel5);
                    _panel.InvokeRemoveChild(_defLabel6);
                    _panel.InvokeRemoveChild(defLabel9);
                    _panel.InvokeIfNeed(new Action<Control, int, int>(_panel.Controls.Add), defLabel9, 0, 5);
                    _panel.InvokeIfNeed(new Action<Control, int, int>(_panel.Controls.Add), _defLabel6, 1, 5);
                    if (_labels.Contains(_defLabel5))
                    {
                        _labels.Remove(_defLabel5);
                    }
                    defLabel10.DefText = "从1机车";

                    //机车类型标志
                    _defLabel1.DefText = TrainType.SH8.ToString();
                    _bv.LocalTrainInfo.TrainType = (TrainType)Enum.Parse(typeof(TrainType), _defLabel1.DefText);
                    _dataService.WriteService.ChangeBool(813 + (Int32)_bv.LocalTrainInfo.TrainType, true);

                    //重联配置的机车号
                    _bv.LocalTrainInfo.TrainID = Convert.ToInt32(_defLabel2.DefText);
                    _dataService.WriteService.ChangeFloat(58, _bv.LocalTrainInfo.TrainID);

                    //实际机车号
                    _dataService.WriteService.ChangeFloat(56, _bv.LocalTrainInfo.RealTrainID);

                    //编组数量
                    _defLabel6.DefText = "2";
                    _bv.LocalTrainInfo.Count = Convert.ToInt32(_defLabel6.DefText);
                    _dataService.WriteService.ChangeFloat(57, _bv.LocalTrainInfo.Count);

                    //编组序号
                    _defLabel5.DefText = "1";

                    //主从设置
                    _defLabel4.DefText = ControlType.主控.ToString();
                }
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
                            btn.IsDown = true;
                            ViewManger.CurrentViewID = btn.ViewID;
                        }


                        //功能键输入
                        DefButton founctionBtn = _intputBtns.Find(a => a.ID == cmd.Key);
                        if (founctionBtn != null)
                        {
                            switch (founctionBtn.ViewID)
                            {
                                case 11://模式选择1
                                    if (_index == 0)
                                    {
                                        _bv.LocalTrainInfo.TrainType = _bv.LocalTrainInfo.TrainType == TrainType.SH8
                                            ? TrainType.SS4B
                                            : TrainType.SH8;
                                        _defLabel1.DefText = _bv.LocalTrainInfo.TrainType.ToString();
                                    }

                                    if (_index == 3)
                                    {
                                        _bv.LocalTrainInfo.ControlType = _bv.LocalTrainInfo.ControlType == ControlType.主控
                                            ? ControlType.从控
                                            : ControlType.主控;
                                        _defLabel4.DefText = _bv.LocalTrainInfo.ControlType.ToString();

                                        if (_bv.LocalTrainInfo.ControlType == ControlType.主控)
                                        {
                                            _panel.InvokeRemoveChild(defLabel11);
                                            _panel.InvokeRemoveChild(_defLabel5);
                                            _panel.InvokeRemoveChild(_defLabel6);
                                            _panel.InvokeRemoveChild(defLabel9);
                                            _panel.InvokeIfNeed(new Action<Control, int, int>(_panel.Controls.Add), defLabel9, 0, 5);
                                            _panel.InvokeIfNeed(new Action<Control, int, int>(_panel.Controls.Add), _defLabel6, 1, 5);

                                            if (_labels.Contains(_defLabel5))
                                            {
                                                _labels.Remove(_defLabel5);
                                            }
                                            defLabel10.DefText = "从1机车";
                                        }
                                        else
                                        {
                                            _panel.InvokeRemoveChild(defLabel11);
                                            _panel.InvokeRemoveChild(_defLabel5);
                                            _panel.InvokeRemoveChild(_defLabel6);
                                            _panel.InvokeRemoveChild(defLabel9);
                                            _panel.InvokeIfNeed(new Action<Control, int, int>(_panel.Controls.Add), defLabel11, 0, 5);
                                            _panel.InvokeIfNeed(new Action<Control, int, int>(_panel.Controls.Add), _defLabel5, 1, 5);
                                            _panel.InvokeIfNeed(new Action<Control, int, int>(_panel.Controls.Add), defLabel9, 0, 6);
                                            _panel.InvokeIfNeed(new Action<Control, int, int>(_panel.Controls.Add), _defLabel6, 1, 6);

                                            if (!_labels.Contains(_defLabel5))
                                            {
                                                _labels.Insert(4, _defLabel5);
                                            }
                                            defLabel10.DefText = "主控机车";
                                        }
                                    }

                                    if (_index == 2)
                                    {
                                        _bv.LocalTrainInfo.Direction = _bv.LocalTrainInfo.Direction == Direction.上行
                                            ? Direction.下行
                                            : Direction.上行;
                                        _defLabel3.DefText = _bv.LocalTrainInfo.Direction.ToString();
                                    }

                                    if (_bv.LocalTrainInfo.ControlType == ControlType.主控)
                                    {
                                        if (_index == 5)
                                        {
                                            _defLabel7.DefText = (_defLabel7.DefText == "SH8" ? "SS4B" : "SH8");
                                        }
                                    }
                                    else
                                    {
                                        if (_index == 6)
                                        {
                                            _defLabel7.DefText = (_defLabel7.DefText == "SH8" ? "SS4B" : "SH8");
                                        }
                                    }
                                    break;
                                case 12://模式选择2
                                    if (_index == 0)
                                    {
                                        _bv.LocalTrainInfo.TrainType = _bv.LocalTrainInfo.TrainType == TrainType.SH8
                                            ? TrainType.SS4B
                                            : TrainType.SH8;
                                        _defLabel1.DefText = _bv.LocalTrainInfo.TrainType.ToString();
                                    }

                                    if (_index == 3)
                                    {
                                        _bv.LocalTrainInfo.ControlType = _bv.LocalTrainInfo.ControlType == ControlType.主控
                                            ? ControlType.从控
                                            : ControlType.主控;
                                        _defLabel4.DefText = _bv.LocalTrainInfo.ControlType.ToString();

                                        if (_bv.LocalTrainInfo.ControlType == ControlType.主控)
                                        {
                                            _panel.InvokeRemoveChild(defLabel11);
                                            _panel.InvokeRemoveChild(_defLabel5);
                                            _panel.InvokeRemoveChild(_defLabel6);
                                            _panel.InvokeRemoveChild(defLabel9);
                                            _panel.InvokeIfNeed(new Action<Control, int, int>(_panel.Controls.Add), defLabel9, 0, 5);
                                            _panel.InvokeIfNeed(new Action<Control, int, int>(_panel.Controls.Add), _defLabel6, 1, 5);
                                            if (_labels.Contains(_defLabel5))
                                            {
                                                _labels.Remove(_defLabel5);
                                            }
                                            defLabel10.DefText = "从1机车";
                                        }
                                        else
                                        {
                                            _panel.InvokeRemoveChild(defLabel11);
                                            _panel.InvokeRemoveChild(_defLabel5);
                                            _panel.InvokeRemoveChild(_defLabel6);
                                            _panel.InvokeRemoveChild(defLabel9);
                                            _panel.InvokeIfNeed(new Action<Control, int, int>(_panel.Controls.Add), defLabel11, 0, 5);
                                            _panel.InvokeIfNeed(new Action<Control, int, int>(_panel.Controls.Add), _defLabel5, 1, 5);
                                            _panel.InvokeIfNeed(new Action<Control, int, int>(_panel.Controls.Add), defLabel9, 0, 6);
                                            _panel.InvokeIfNeed(new Action<Control, int, int>(_panel.Controls.Add), _defLabel6, 1, 6);
                                            if (!_labels.Contains(_defLabel5))
                                            {
                                                _labels.Insert(4, _defLabel5);
                                            }

                                            defLabel10.DefText = "主控机车";
                                        }
                                    }

                                    if (_index == 2)
                                    {
                                        _bv.LocalTrainInfo.Direction = _bv.LocalTrainInfo.Direction == Direction.上行
                                            ? Direction.下行
                                            : Direction.上行;
                                        _defLabel3.DefText = _bv.LocalTrainInfo.Direction.ToString();
                                    }

                                    if (_bv.LocalTrainInfo.ControlType == ControlType.主控)
                                    {
                                        if (_index == 5)
                                        {
                                            _defLabel7.DefText = (_defLabel7.DefText == "SH8" ? "SS4B" : "SH8");
                                        }
                                    }
                                    else
                                    {
                                        if (_index == 6)
                                        {
                                            _defLabel7.DefText = (_defLabel7.DefText == "SH8" ? "SS4B" : "SH8");
                                        }
                                    }
                                    break;
                                case 13://后退
                                    _labels[_index].BackgroundImage = Resources.btn_White;

                                    if (_index == 0)
                                    {
                                        _index = _labels.Count - 1;
                                    }
                                    else
                                    {
                                        _index--;
                                    }

                                    _labels[_index].BackgroundImage = Resources.btn_Blue1;

                                    _isRe = true;
                                    break;
                                case 14://前进
                                    _labels[_index].BackgroundImage = Resources.btn_White;

                                    if (_index == _labels.Count - 1)
                                    {
                                        _index = 0;
                                    }
                                    else
                                    {
                                        _index++;
                                    }

                                    _labels[_index].BackgroundImage = Resources.btn_Blue1;

                                    _isRe = true;
                                    break;
                                case 15://确认
                                    //主从车标志
                                    if (_bv.LocalTrainInfo.ControlType == ControlType.主控)
                                    {
                                        _dataService.WriteService.ChangeBool(809, true);
                                        for (int i = 1; i < 3; i++)
                                        {
                                            _dataService.WriteService.ChangeBool(809 + i, false);
                                        }
                                    }
                                    else
                                    {
                                        _bv.LocalTrainInfo.Number = Convert.ToInt32(_defLabel5.DefText);
                                        _dataService.WriteService.ChangeBool(809, false);
                                        for (int i = 1; i < 3; i++)
                                        {
                                            if (i == _bv.LocalTrainInfo.Number)
                                            {
                                                _dataService.WriteService.ChangeBool(809 + i, true);
                                            }
                                            else
                                            {
                                                _dataService.WriteService.ChangeBool(809 + i, false);
                                            }
                                        }
                                    }

                                    //机车类型标志
                                    _bv.LocalTrainInfo.TrainType = (TrainType)Enum.Parse(typeof(TrainType), _defLabel1.DefText);
                                    _dataService.WriteService.ChangeBool(813 + (Int32)_bv.LocalTrainInfo.TrainType, true);
                                    _dataService.WriteService.ChangeBool(813 + ((Int32)_bv.LocalTrainInfo.TrainType+1)%2, false);

                                    //重联配置的机车号
                                    _bv.LocalTrainInfo.TrainID = Convert.ToInt32(_defLabel2.DefText);
                                    _dataService.WriteService.ChangeFloat(58, _bv.LocalTrainInfo.TrainID);

                                    //实际机车号
                                    _dataService.WriteService.ChangeFloat(56, _bv.LocalTrainInfo.RealTrainID);

                                    //编组数量
                                    _bv.LocalTrainInfo.Count = Convert.ToInt32(_defLabel6.DefText);
                                    _dataService.WriteService.ChangeFloat(57, _bv.LocalTrainInfo.Count);

                                    //上下行
                                    _bv.LocalTrainInfo.Direction = (Direction)Enum.Parse(typeof(Direction), _defLabel3.DefText);
                                    _dataService.WriteService.ChangeBool(817 + (Int32)_bv.LocalTrainInfo.Direction, true);
                                    _dataService.WriteService.ChangeBool(817 + ((Int32)_bv.LocalTrainInfo.Direction + 1) % 2, false);

                                    _ss.SetMode(_bv.LocalTrainInfo.ControlType == ControlType.主控 ? 0 : _bv.LocalTrainInfo.Number);

                                    //从车车型
                                    TrainType fTrainType = (TrainType)Enum.Parse(typeof(TrainType), _defLabel7.DefText);
                                    _dataService.WriteService.ChangeBool(819 + (Int32)fTrainType, true);
                                    _dataService.WriteService.ChangeBool(819 + ((Int32)fTrainType + 1) % 2, false);

                                    //从车车号
                                    _dataService.WriteService.ChangeFloat(60, Convert.ToInt32(_defLabel8.DefText));

                                    //车间距
                                    _dataService.WriteService.ChangeFloat(59, Convert.ToInt32(_defLabel9.DefText));

                                    defLabel12.InvokeSetDefText("正在通信...请等待");
                                    IsTimerStartted = true;
                                    break;
                                default://数字
                                    if (_index == 1)
                                    {
                                        if (_isRe)
                                        {
                                            _isRe = false;
                                            _bv.LocalTrainInfo.TrainID = 0;
                                        }
                                        _bv.LocalTrainInfo.TrainID = _bv.LocalTrainInfo.TrainID * 10 + founctionBtn.ViewID;
                                        string temp = _bv.LocalTrainInfo.TrainID.ToString();
                                        if (temp.Length > 4)
                                        {
                                            temp = temp.Substring(temp.Length - 1, 1);
                                        }
                                        _defLabel2.DefText = temp;
                                        _bv.LocalTrainInfo.TrainID = Convert.ToInt32(temp);
                                    }

                                    if (_bv.LocalTrainInfo.ControlType == ControlType.主控)
                                    {
                                        if (_index == 4)
                                        {
                                            _bv.LocalTrainInfo.Count = founctionBtn.ViewID;
                                            _defLabel6.DefText = founctionBtn.ViewID.ToString();
                                        }
                                        if (_index == 6)
                                        {
                                            Int32 trainID = Convert.ToInt32(_defLabel8.DefText) * 10 + founctionBtn.ViewID;
                                            string temp = trainID.ToString();
                                            if (temp.Length > 4)
                                            {
                                                temp = temp.Substring(temp.Length - 1, 1);
                                            }
                                            _defLabel8.DefText = temp;
                                        }
                                        else if (_index == 7)
                                        {
                                            Int32 distance = Convert.ToInt32(_defLabel9.DefText) * 10 + founctionBtn.ViewID;
                                            string temp = distance.ToString();
                                            if (temp.Length > 4)
                                            {
                                                temp = temp.Substring(temp.Length - 1, 1);
                                            }
                                            _defLabel9.DefText = temp;
                                        }
                                    }
                                    else
                                    {
                                        if (_index == 4)
                                        {
                                            if (founctionBtn.ViewID <= 3)
                                            {
                                                _bv.LocalTrainInfo.Number = founctionBtn.ViewID;
                                                _defLabel5.DefText = founctionBtn.ViewID.ToString();
                                            }
                                        }
                                        if (_index == 5)
                                        {
                                            _bv.LocalTrainInfo.Count = founctionBtn.ViewID;
                                            _defLabel6.DefText = founctionBtn.ViewID.ToString();
                                        }
                                        if (_index == 7)
                                        {
                                            Int32 trainID = Convert.ToInt32(_defLabel8.DefText) * 10 + founctionBtn.ViewID;
                                            string temp = trainID.ToString();
                                            if (temp.Length > 4)
                                            {
                                                temp = temp.Substring(temp.Length - 1, 1);
                                            }
                                            _defLabel8.DefText = temp;
                                        }
                                        else if (_index == 8)
                                        {
                                            Int32 distance = Convert.ToInt32(_defLabel9.DefText) * 10 + founctionBtn.ViewID;
                                            string temp = distance.ToString();
                                            if (temp.Length > 4)
                                            {
                                                temp = temp.Substring(temp.Length - 1, 1);
                                            }
                                            _defLabel9.DefText = temp;
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private void DefClick(object sender, ButtonClickArgs e)
        {
            switch (e.ViewID)
            {
                case 11://模式选择1
                    if (_index == 0)
                    {
                        _bv.LocalTrainInfo.TrainType = _bv.LocalTrainInfo.TrainType == TrainType.SH8
                            ? TrainType.SS4B
                            : TrainType.SH8;
                        _defLabel1.DefText = _bv.LocalTrainInfo.TrainType.ToString();
                    }

                    if (_index == 3)
                    {
                        _bv.LocalTrainInfo.ControlType = _bv.LocalTrainInfo.ControlType == ControlType.主控
                            ? ControlType.从控
                            : ControlType.主控;
                        _defLabel4.DefText = _bv.LocalTrainInfo.ControlType.ToString();

                        if (_bv.LocalTrainInfo.ControlType == ControlType.主控)
                        {
                            _panel.Controls.Remove(defLabel11);
                            _panel.Controls.Remove(_defLabel5);
                            _panel.Controls.Remove(defLabel9);
                            _panel.Controls.Remove(_defLabel6);

                            _panel.Controls.Add(defLabel9, 0, 5);
                            _panel.Controls.Add(_defLabel6, 1, 5);

                            if (_labels.Contains(_defLabel5))
                            {
                                _labels.Remove(_defLabel5);
                            }
                            defLabel10.DefText = "从1机车";
                        }
                        else
                        {
                            _panel.Controls.Remove(defLabel11);
                            _panel.Controls.Remove(_defLabel5);
                            _panel.Controls.Remove(defLabel9);
                            _panel.Controls.Remove(_defLabel6);

                            _panel.Controls.Add(defLabel11, 0, 5);
                            _panel.Controls.Add(_defLabel5, 1, 5);
                            _panel.Controls.Add(defLabel9, 0, 6);
                            _panel.Controls.Add(_defLabel6, 1, 6);

                            if (!_labels.Contains(_defLabel5))
                            {
                                _labels.Insert(4, _defLabel5);
                            }
                            defLabel10.DefText = "主控机车";
                        }
                    }

                    if (_index == 2)
                    {
                        _bv.LocalTrainInfo.Direction = _bv.LocalTrainInfo.Direction == Direction.上行
                            ? Direction.下行
                            : Direction.上行;
                        _defLabel3.DefText = _bv.LocalTrainInfo.Direction.ToString();
                    }

                    if (_bv.LocalTrainInfo.ControlType == ControlType.主控)
                    {
                        if (_index == 5)
                        {
                            _defLabel7.DefText = (_defLabel7.DefText == "SH8" ? "SS4B" : "SH8");
                        }
                    }
                    else
                    {
                        if (_index == 6)
                        {
                            _defLabel7.DefText = (_defLabel7.DefText == "SH8" ? "SS4B" : "SH8");
                        }
                    }
                    break;
                case 12://模式选择2
                    if (_index == 0)
                    {
                        _bv.LocalTrainInfo.TrainType = _bv.LocalTrainInfo.TrainType == TrainType.SH8
                            ? TrainType.SS4B
                            : TrainType.SH8;
                        _defLabel1.DefText = _bv.LocalTrainInfo.TrainType.ToString();
                    }

                    if (_index == 3)
                    {
                        _bv.LocalTrainInfo.ControlType = _bv.LocalTrainInfo.ControlType == ControlType.主控
                            ? ControlType.从控
                            : ControlType.主控;
                        _defLabel4.DefText = _bv.LocalTrainInfo.ControlType.ToString();

                        if (_bv.LocalTrainInfo.ControlType == ControlType.主控)
                        {
                            _panel.Controls.Remove(defLabel11);
                            _panel.Controls.Remove(_defLabel5);
                            _panel.Controls.Remove(defLabel9);
                            _panel.Controls.Remove(_defLabel6);

                            _panel.Controls.Add(defLabel9, 0, 5);
                            _panel.Controls.Add(_defLabel6, 1, 5);
                            if (_labels.Contains(_defLabel5))
                            {
                                _labels.Remove(_defLabel5);
                            }
                            defLabel10.DefText = "从1机车";
                        }
                        else
                        {
                            _panel.Controls.Remove(defLabel11);
                            _panel.Controls.Remove(_defLabel5);
                            _panel.Controls.Remove(defLabel9);
                            _panel.Controls.Remove(_defLabel6);

                            _panel.Controls.Add(defLabel11, 0, 5);
                            _panel.Controls.Add(_defLabel5, 1, 5);
                            _panel.Controls.Add(defLabel9, 0, 6);
                            _panel.Controls.Add(_defLabel6, 1, 6);
                            if (!_labels.Contains(_defLabel5))
                            {
                                _labels.Insert(4, _defLabel5);
                            }

                            defLabel10.DefText = "主控机车";
                        }
                    }

                    if (_index == 2)
                    {
                        _bv.LocalTrainInfo.Direction = _bv.LocalTrainInfo.Direction == Direction.上行
                            ? Direction.下行
                            : Direction.上行;
                        _defLabel3.DefText = _bv.LocalTrainInfo.Direction.ToString();
                    }

                    if (_bv.LocalTrainInfo.ControlType == ControlType.主控)
                    {
                        if (_index == 5)
                        {
                            _defLabel7.DefText = (_defLabel7.DefText == "SH8" ? "SS4B" : "SH8");
                        }
                    }
                    else
                    {
                        if (_index == 6)
                        {
                            _defLabel7.DefText = (_defLabel7.DefText == "SH8" ? "SS4B" : "SH8");
                        }
                    }
                    break;
                case 13://后退
                    _labels[_index].BackgroundImage = Resources.btn_White;

                    if (_index == 0)
                    {
                        _index = _labels.Count - 1;
                    }
                    else
                    {
                        _index--;
                    }

                    _labels[_index].BackgroundImage = Resources.btn_Blue1;

                    _isRe = true;
                    break;
                case 14://前进
                     _labels[_index].BackgroundImage = Resources.btn_White;

                     if (_index == _labels.Count - 1)
                     {
                         _index = 0;
                     }
                     else
                     {
                         _index++;
                     }

                    _labels[_index].BackgroundImage = Resources.btn_Blue1;

                    _isRe = true;
                    break;
                case 15://确认
                    //主从车标志
                    if (_bv.LocalTrainInfo.ControlType == ControlType.主控)
                    {
                        _dataService.WriteService.ChangeBool(809, true);
                        for (int i = 1; i < 3; i++)
                        {
                            _dataService.WriteService.ChangeBool(809 + i, false);
                        }
                    }
                    else
                    {
                        _bv.LocalTrainInfo.Number = Convert.ToInt32(_defLabel5.DefText);
                        _dataService.WriteService.ChangeBool(809, false);
                        for (int i = 1; i < 3; i++)
                        {
                            if (i == _bv.LocalTrainInfo.Number)
                            {
                                _dataService.WriteService.ChangeBool(809 + i, true);
                            }
                            else
                            {
                                _dataService.WriteService.ChangeBool(809 + i, false);
                            }
                        }
                    }

                    //机车类型标志
                    _bv.LocalTrainInfo.TrainType = (TrainType)Enum.Parse(typeof(TrainType), _defLabel1.DefText);
                    _dataService.WriteService.ChangeBool(813 + (Int32)_bv.LocalTrainInfo.TrainType, true);
                    _dataService.WriteService.ChangeBool(813 + ((Int32)_bv.LocalTrainInfo.TrainType + 1) % 2, false);

                    //重联配置的机车号
                    _bv.LocalTrainInfo.TrainID = Convert.ToInt32(_defLabel2.DefText);
                    _dataService.WriteService.ChangeFloat(58, _bv.LocalTrainInfo.TrainID);

                    //实际机车号
                    _dataService.WriteService.ChangeFloat(56, _bv.LocalTrainInfo.RealTrainID);

                    //编组数量
                    _bv.LocalTrainInfo.Count = Convert.ToInt32(_defLabel6.DefText);
                    _dataService.WriteService.ChangeFloat(57, _bv.LocalTrainInfo.Count);

                    //上下行
                    _bv.LocalTrainInfo.Direction = (Direction)Enum.Parse(typeof(Direction), _defLabel3.DefText);
                    _dataService.WriteService.ChangeBool(817 + (Int32)_bv.LocalTrainInfo.Direction, true);
                    _dataService.WriteService.ChangeBool(817 + ((Int32)_bv.LocalTrainInfo.Direction + 1) % 2, false);

                    _ss.SetMode(_bv.LocalTrainInfo.ControlType == ControlType.主控 ? 0 : _bv.LocalTrainInfo.Number);

                    //从车车型
                    TrainType fTrainType = (TrainType)Enum.Parse(typeof(TrainType), _defLabel7.DefText);
                    _dataService.WriteService.ChangeBool(819 + (Int32)fTrainType, true);
                    _dataService.WriteService.ChangeBool(819 + ((Int32)fTrainType + 1) % 2, false);
                    
                    //从车车号
                    _dataService.WriteService.ChangeFloat(60, Convert.ToInt32(_defLabel8.DefText));
                    
                    //车间距
                    _dataService.WriteService.ChangeFloat(59, Convert.ToInt32(_defLabel9.DefText));

                    defLabel12.InvokeSetDefText( "正在通信...请等待");
                    IsTimerStartted = true;
                    break;
                default://数字
                    if (_index == 1)
                    {
                        if (_isRe)
                        {
                            _isRe = false;
                            _bv.LocalTrainInfo.TrainID = 0;
                        }
                        _bv.LocalTrainInfo.TrainID = _bv.LocalTrainInfo.TrainID*10 + e.ViewID;
                        string temp = _bv.LocalTrainInfo.TrainID.ToString();
                        if (temp.Length > 4)
                        {
                            temp = temp.Substring(temp.Length - 1, 1);
                        }
                        _defLabel2.DefText = temp;
                        _bv.LocalTrainInfo.TrainID = Convert.ToInt32(temp);
                    }

                    if (_bv.LocalTrainInfo.ControlType == ControlType.主控)
                    {
                        if (_index == 4)
                        {
                            _bv.LocalTrainInfo.Count = e.ViewID;
                            _defLabel6.DefText = e.ViewID.ToString();
                        }
                        if (_index == 6)
                        {
                            Int32 trainID = Convert.ToInt32(_defLabel8.DefText) * 10 + e.ViewID;
                            string temp = trainID.ToString();
                            if (temp.Length > 4)
                            {
                                temp = temp.Substring(temp.Length - 1, 1);
                            }
                            _defLabel8.DefText = temp;
                        }
                        else if (_index == 7)
                        {
                            Int32 distance = Convert.ToInt32(_defLabel9.DefText) * 10 + e.ViewID;
                            string temp = distance.ToString();
                            if (temp.Length > 4)
                            {
                                temp = temp.Substring(temp.Length - 1, 1);
                            }
                            _defLabel9.DefText = temp;
                        }
                    }
                    else
                    {
                        if (_index == 4)
                        {
                            _bv.LocalTrainInfo.Number = e.ViewID;
                            _defLabel5.DefText = e.ViewID.ToString();
                        }
                        if (_index == 5)
                        {
                            _bv.LocalTrainInfo.Count = e.ViewID;
                            _defLabel6.DefText = e.ViewID.ToString();
                        }
                        if (_index == 7)
                        {
                            Int32 trainID = Convert.ToInt32(_defLabel8.DefText) * 10 + e.ViewID;
                            string temp = trainID.ToString();
                            if (temp.Length > 4)
                            {
                                temp = temp.Substring(temp.Length - 1, 1);
                            }
                            _defLabel8.DefText = temp;
                        }
                        else if (_index == 8)
                        {
                            Int32 distance = Convert.ToInt32(_defLabel9.DefText) * 10 + e.ViewID;
                            string temp = distance.ToString();
                            if (temp.Length > 4)
                            {
                                temp = temp.Substring(temp.Length - 1, 1);
                            }
                            _defLabel9.DefText = temp;
                        }
                    }
                    break;
            }
        }

        private void DefClick_ViewChang(object sender, ButtonClickArgs e)
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
    }
}
