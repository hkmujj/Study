using CommonControls;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using ShenHuaHaoTMS.DefControls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MMI.Facility.Interface.Extension;
using ShenHuaHaoTMS.Common;
using YunDa.JC.MMI.Common;
using YunDa.JC.MMI.Common.Extensions;

namespace ShenHuaHaoTMS.View
{
    public partial class SH_V401_StateShow : UserControl, IView,IDisposable, IDataListener
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
        private DefStateShow _dss;

        public SH_V401_StateShow()
        {
            InitializeComponent();
        }

        public SH_V401_StateShow(Int32 id, ViewManager viewManager, ICommunicationDataService dataService, BlackView bv, SubsystemInitParam initParam)
        {
            InitializeComponent();

            SetStyle(
                ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer, true);

            _dataService = dataService;
            GlobalParam.Instance.InitParam.RegistDataListener(this);
            m_ListenViewChanged.BoolChanged += onViewChanged;

            Dock = DockStyle.Fill;
            Margin = new Padding(0);

            _dss = new DefStateShow(dataService,bv);
            _panel.Controls.Add(_dss);

            ID = id;
            ViewManger = viewManager;
            ViewManger.Register(this);

            _viewBtns = new List<DefButton>()
            {
                _btn_1,
                _btn_3,
                _btn_4,
                _btn_10
            };
            _statesControls = new List<ILogic>()
            {
                defState1,defState2,defState3,defState4,defState5,defState6,defState7
            };

            initParam.DataPackage.ServiceManager.GetService<IDisposeService>().RegistDisposableObject(this);
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
                    }
                }
            }
        }

        private void DefButtonClick(object sender, ButtonClickArgs e)
        {
            ViewManger.CurrentViewID = e.ViewID;
        }

        public void SetMode(Int32 trainID)
        {
            switch (trainID)
            {
                case 0:
                    defState1.DataConfigInfoCollection[0].BoolLogic = 3337;
                    defState1.DataConfigInfoCollection[0].BoolValue =
                        _dataService.ReadService.ReadOnlyBoolDictionary[3337];
                    defState1.DataConfigInfoCollection[1].BoolLogic = 3338;
                    defState1.DataConfigInfoCollection[1].BoolValue =
                        _dataService.ReadService.ReadOnlyBoolDictionary[3338];
                    defState1.DataConfigInfoCollection[2].BoolLogic = 3339;
                    defState1.DataConfigInfoCollection[2].BoolValue =
                        _dataService.ReadService.ReadOnlyBoolDictionary[3339];
                    break;
                case 1:
                    defState1.DataConfigInfoCollection[0].BoolLogic = 3737;
                    defState1.DataConfigInfoCollection[0].BoolValue =
                        _dataService.ReadService.ReadOnlyBoolDictionary[3737];
                    defState1.DataConfigInfoCollection[1].BoolLogic = 3738;
                    defState1.DataConfigInfoCollection[1].BoolValue =
                        _dataService.ReadService.ReadOnlyBoolDictionary[3738];
                    defState1.DataConfigInfoCollection[2].BoolLogic = 3739;
                    defState1.DataConfigInfoCollection[2].BoolValue =
                        _dataService.ReadService.ReadOnlyBoolDictionary[3739];
                    break;
                case 2:
                    defState1.DataConfigInfoCollection[0].BoolLogic = 4137;
                    defState1.DataConfigInfoCollection[0].BoolValue =
                        _dataService.ReadService.ReadOnlyBoolDictionary[4137];
                    defState1.DataConfigInfoCollection[1].BoolLogic = 4138;
                    defState1.DataConfigInfoCollection[1].BoolValue =
                        _dataService.ReadService.ReadOnlyBoolDictionary[4138];
                    defState1.DataConfigInfoCollection[2].BoolLogic = 4139;
                    defState1.DataConfigInfoCollection[2].BoolValue =
                        _dataService.ReadService.ReadOnlyBoolDictionary[4139];
                    break;
                case 3:
                    defState1.DataConfigInfoCollection[0].BoolLogic = 4537;
                    defState1.DataConfigInfoCollection[0].BoolValue =
                        _dataService.ReadService.ReadOnlyBoolDictionary[4537];
                    defState1.DataConfigInfoCollection[1].BoolLogic = 4538;
                    defState1.DataConfigInfoCollection[1].BoolValue =
                        _dataService.ReadService.ReadOnlyBoolDictionary[4538];
                    defState1.DataConfigInfoCollection[2].BoolLogic = 4539;
                    defState1.DataConfigInfoCollection[2].BoolValue =
                        _dataService.ReadService.ReadOnlyBoolDictionary[4539];
                    break;
            }

            defState1.InvokeInvalidate();
        }

        public void InvalidateNew()
        {
            _dss.InvalidateNew();
        }
        public void Dispose()
        {
            _statesControls.ForEach(a => ((DefState)a).Dispose());
            if (_dss != null)
            {
                _dss.Dispose();
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
    }
}
