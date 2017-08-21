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
    public partial class SH_V803_A_AllFalut : UserControl, IView, IDisposable, IDataListener
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
                        _btn_0.DefText = "机车_" + _bv.LocalTrainInfo.RealTrainID + "A";
                        _btn_1.DefText = "机车_" + _bv.LocalTrainInfo.RealTrainID + "B";
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
        private List<FalutInfo> _faluts = new List<CommonControls.FalutInfo>();
        private BlackView _bv = null;

        public SH_V803_A_AllFalut()
        {
            InitializeComponent();
        }

        public SH_V803_A_AllFalut(Int32 id, ViewManager viewManager, ICommunicationDataService dataService, BlackView bv,
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
                _btn_1,
                _btn_6,
                _btn_8,
                _btn_9
            };

            _statesControls = new List<ILogic>()
            {
                defState1
            };

            _bv = bv;
            _bv.FalutChangedA += _bv_FalutChangedA;
            _title_TrainName.DefText = "神华号 " + bv.LocalTrainInfo.RealTrainID + "A";

            initParam.DataPackage.ServiceManager.GetService<IDisposeService>().RegistDisposableObject(this);
        }

        void _bv_FalutChangedA(FalutState fs)
        {
            if (fs == null)
            {
                return;
            }

            if (fs.IsSet)
            {
                defFalut1.AddFalut(fs.CurrentFalut);
            }
            else
            {
                defFalut1.SetEndTime(fs.CurrentFalut);
            }
        }

        public void SetData(List<FalutInfo> faluts)
        {
            _faluts = faluts;
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

                        if (cmd.Key == 811)
                        {
                            defFalut1.Up();
                        }
                        else if (cmd.Key == 812)
                        {
                            defFalut1.Down();
                        }

                        else if (cmd.Key == 824) //处理信息按钮
                        {
                            //进入V=0的处理界面
                            ViewManger.CurrentViewID = 807;
                        }
                    }
                }
            }
        }

        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> e)
        {
            foreach (var cmd in e.NewValue)
            {
                if (cmd.Key == 1300) //结束课程
                {
                    if (!cmd.Value)
                    {
                        defFalut1.Reset();
                    }
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

        public FalutInfo GetCurrentFalut()
        {
            return defFalut1.GetCurrentFalut();
        }

        private void BtnClick(object sender, ButtonClickArgs e)
        {
            ViewManger.CurrentViewID = ((DefButton) sender).ViewID;
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
