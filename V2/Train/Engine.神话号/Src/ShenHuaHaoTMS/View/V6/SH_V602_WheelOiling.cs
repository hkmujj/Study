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
    public partial class SH_V602_WheelOiling : UserControl, IView, IDisposable, IDataListener
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

        private List<DefButton> _viewBtns = new List<DefButton>(); //跳转页面的按钮
        private List<DefButton> _intputBtns = new List<DefButton>(); //数字键与右侧功能键
        private List<ISetValue> _defSetValues = new List<ISetValue>(); //设置框集合
        private ISetValue _currentSetValue;
        private List<ILogic> _statesControls = new List<ILogic>();

        public SH_V602_WheelOiling()
        {
            InitializeComponent();
        }

        public SH_V602_WheelOiling(Int32 id, ViewManager viewManager, ICommunicationDataService dataService,
            BlackView bv, SubsystemInitParam initParam)
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
            Margin = new Padding(0);
            _dataService = dataService;
            GlobalParam.Instance.InitParam.RegistDataListener(this);
            m_ListenViewChanged.BoolChanged += onViewChanged;

            ID = id;
            ViewManger = viewManager;
            ViewManger.Register(this);

            _viewBtns = new List<DefButton>()
            {
                _defBtn_Return_0
            };
            _intputBtns = new List<DefButton>()
            {
                _btn_Data_1,
                _btn_Data_2,
                _btn_Data_3,
                _btn_Data_4,
                _btn_Data_5,
                _btn_Data_6,
                _btn_Data_7,
                _btn_Data_8,
                _btn_Data_9,
                _btn_Data_10,
                _defBtn_ModeSelect,
                _defBtn_OK,
                _defBtn_Test
            };

            _defSetValues = new List<ISetValue>()
            {
                _defSetValue_1,
                _defSetValue_2
            };
            _defSetValue_1.NextISetValue = _defSetValue_2;
            _defSetValue_1.LastISetValue = _defSetValue_2;
            _defSetValue_2.NextISetValue = _defSetValue_1;
            _defSetValue_2.LastISetValue = _defSetValue_1;
            _currentSetValue = _defSetValue_1;

            _statesControls = new List<ILogic>()
            {
                defState1
            };

            _title_TrainName.DefText = "神华号 " + bv.LocalTrainInfo.RealTrainID + "A";

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


                        //功能键输入
                        DefButton founctionBtn = _intputBtns.Find(a => a.ID == cmd.Key);
                        if (founctionBtn != null)
                        {
                            switch (founctionBtn.ViewID)
                            {
                                case 11: //模式选择
                                    defSetMode1.SetMode();
                                    break;
                                case 12: //确认
                                    Int32 mode = defSetMode1.GetMode();
                                    //_dataService.WriteService.ChangeBool(0, true);

                                    Int32 value1 = _defSetValue_1.GetValue();
                                    _dataService.WriteService.ChangeFloat(54, value1);

                                    Int32 value2 = _defSetValue_2.GetValue();
                                    _dataService.WriteService.ChangeFloat(55, value2);
                                    break;
                                case 13: //开始测试
                                    break;
                                default: //数字
                                    _currentSetValue.SetValue(founctionBtn.ViewID);
                                    _currentSetValue = _defSetValues.Find(a => a.CurrentLabelID != -1);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private void DefButtonClick(object sender, ButtonClickArgs e)
        {
            ViewManger.CurrentViewID = e.ViewID;
        }

        private void DataBtnClick(object sender, ButtonClickArgs e)
        {
            DefButton btn = (DefButton) sender;
            switch (btn.ViewID)
            {
                case 11: //模式选择
                    defSetMode1.SetMode();
                    break;
                case 12: //确认
                    Int32 mode = defSetMode1.GetMode();
                    //_dataService.WriteService.ChangeBool(0, true);

                    Int32 value1 = _defSetValue_1.GetValue();
                    _dataService.WriteService.ChangeFloat(54, value1);

                    Int32 value2 = _defSetValue_2.GetValue();
                    _dataService.WriteService.ChangeFloat(55, value2);
                    break;
                case 13: //开始测试
                    break;
                default: //数字
                    _currentSetValue.SetValue(btn.ViewID);
                    _currentSetValue = _defSetValues.Find(a => a.CurrentLabelID != -1);
                    break;
            }
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
