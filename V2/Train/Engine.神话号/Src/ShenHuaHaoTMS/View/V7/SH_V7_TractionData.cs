using System;
using System.Drawing;
using System.Windows.Forms;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using System.Collections.Generic;
using CommonControls;
using MMI.Facility.Interface.Extension;
using ShenHuaHaoTMS.Common;
using YunDa.JC.MMI.Common;
using ShenHuaHaoTMS.DefControls;
using YunDa.JC.MMI.Common.Extensions;

namespace ShenHuaHaoTMS.View
{
    public partial class SH_V7_TractionData : UserControl, IView, IDisposable, IDataListener
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
        private List<ILogic> _statesControls = new List<ILogic>();
        private List<DefButton> _viewBtns = new List<DefButton>();
        private List<DefColumnChart> _defColumnChart = new List<DefColumnChart>();
        private List<FalutInfo> _allFaluts = new List<FalutInfo>();
        private BlackView _bv = null;
        private Int32 _falutIndex = 0;
        private Int32 _leftInfoIndex = 0;
        private Int32 _middleInfoIndex = 0;
        private Int32 _falutBackIndex = 0;
        private Int32 _index = 0;
        private String _falutText = "";

        public SH_V7_TractionData()
        {
            InitializeComponent();
        }

        public SH_V7_TractionData(Int32 id, ViewManager viewManager, ICommunicationDataService dataService, BlackView bv,
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
                _btn_1
            };

            _defColumnChart = new List<DefColumnChart>()
            {
                _columnChart_Traction_A,
                _columnChart_Traction_B
            };

            _statesControls = new List<ILogic>()
            {
                defState1
            };

            _bv = bv;
            _bv.FalutChangedA += _bv_FalutChangedA;
            _bv.FalutChangedB += _bv_FalutChangedB;
            _bv.LeftInfoChanged += _bv_LeftInfoChanged;
            _bv.MiddleInfoChanged += _bv_MiddleInfoChanged;

            GlobalTimer.Instance.TimersElapsed500MS += _timer_Elapsed;

            _title_TrainName.DefText = "神华号 " + bv.LocalTrainInfo.RealTrainID + "A";

            initParam.DataPackage.ServiceManager.GetService<IDisposeService>().RegistDisposableObject(this);
        }

        void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //故障
            if (_allFaluts == null || _allFaluts.Count == 0)
            {
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

        void _timer_Tick(object sender, EventArgs e)
        {
            //故障
            if (_allFaluts == null || _allFaluts.Count == 0)
            {
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
                if (cmd.Key >= 50 && cmd.Key < 825) //按钮命令
                {
                    for (int i = 0; i < _defColumnChart.Count; i++)
                    {
                        _defColumnChart[i].SetValue(cmd.Key, cmd.Value);
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
                for (int i = 0; i < _defColumnChart.Count; i++)
                {
                    _defColumnChart[i].SetValue(cmd.Key, cmd.Value);
                }

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
}
