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
    public partial class SH_V601_TrianParam : UserControl, IView,IDisposable, IDataListener
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
                        _defBtn_OK_13.IsDown = false;
                    }
                }
            }
        }
        private bool _isShow = false;

        public ViewManager ViewManger { get; set; }
        private ICommunicationDataService _dataService;

        private List<DefButton> _viewBtns = new List<DefButton>();//跳转页面的按钮
        private List<DefButton> _intputBtns = new List<DefButton>();//数字键与右侧功能键
        private List<ISetValue> _defSetValues = new List<ISetValue>();//设置框集合
        private ISetValue _currentSetValue;
        private List<ILogic> _statesControls = new List<ILogic>();

        public SH_V601_TrianParam()
        {
            InitializeComponent();
        }

        public SH_V601_TrianParam(Int32 id, ViewManager viewManager, ICommunicationDataService dataService, BlackView bv, SubsystemInitParam initParam)
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
            Margin = new Padding(0);
            _dataService = dataService;
            GlobalParam.Instance.InitParam.RegistDataListener(this);m_ListenViewChanged.BoolChanged += onViewChanged;

            ID = id;
            ViewManger = viewManager;
            ViewManger.Register(this);

            _viewBtns = new List<DefButton>()
            {
                _DefBtn_Return_0
            };
            _intputBtns = new List<DefButton>()
            {
                _btn_Data_1,_btn_Data_2,_btn_Data_3,_btn_Data_4,_btn_Data_5,_btn_Data_6,_btn_Data_7,_btn_Data_8,_btn_Data_9,_btn_Data_10,
                _defBtn_Back_11,_defBtn_Front_12,_defBtn_OK_13
            };

            _defSetValues = new List<ISetValue>()
            {
                _defSetValue_Weight_1,_defSetValue_TrainType_2,_defSetValue_TrainAxis_3,_defSetValue_LinkSpeed_4
            };
            _defSetValue_Weight_1.NextISetValue = _defSetValue_TrainType_2;
            _defSetValue_TrainType_2.NextISetValue = _defSetValue_TrainAxis_3;
            _defSetValue_TrainAxis_3.NextISetValue = _defSetValue_LinkSpeed_4;
            _defSetValue_LinkSpeed_4.NextISetValue = _defSetValue_Weight_1;
            _defSetValue_Weight_1.LastISetValue = _defSetValue_LinkSpeed_4;
            _defSetValue_LinkSpeed_4.LastISetValue = _defSetValue_TrainAxis_3;
            _defSetValue_TrainAxis_3.LastISetValue = _defSetValue_TrainType_2;
            _defSetValue_TrainType_2.LastISetValue = _defSetValue_Weight_1;
            _currentSetValue = _defSetValue_Weight_1;


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

                if (cmd.Key == 1300 && cmd.Value)
                {
                    Int32 trainWeight = _defSetValue_Weight_1.GetValue();
                    Int32 trianType = _defSetValue_TrainType_2.GetValue();
                    Int32 trainAxisWeight = _defSetValue_TrainAxis_3.GetValue();
                    Int32 speed = _defSetValue_LinkSpeed_4.GetValue();
                    _dataService.WriteService.ChangeFloat(50, trainWeight);
                    _dataService.WriteService.ChangeFloat(51, trianType);
                    _dataService.WriteService.ChangeFloat(52, trainAxisWeight);
                    _dataService.WriteService.ChangeFloat(53, speed);
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
                                case 11://后退
                                    _currentSetValue.Last();
                                    _currentSetValue = _defSetValues.Find(a => a.CurrentLabelID != -1);
                                    break;
                                case 12://前进
                                    _currentSetValue.Next();
                                    _currentSetValue = _defSetValues.Find(a => a.CurrentLabelID != -1);
                                    break;
                                case 13://确认
                                    Int32 trainWeight = _defSetValue_Weight_1.GetValue();
                                    Int32 trianType = _defSetValue_TrainType_2.GetValue();
                                    Int32 trainAxisWeight = _defSetValue_TrainAxis_3.GetValue();
                                    Int32 speed = _defSetValue_LinkSpeed_4.GetValue();
                                    _dataService.WriteService.ChangeFloat(50, trainWeight);
                                    _dataService.WriteService.ChangeFloat(51, trianType);
                                    _dataService.WriteService.ChangeFloat(52, trainAxisWeight);
                                    _dataService.WriteService.ChangeFloat(53, speed);

                                    _defBtn_OK_13.IsDown = true;

                                    break;
                                default://数字
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
            DefButton btn = (DefButton)sender;
            switch (btn.ViewID)
            {
                case 11://后退
                    _currentSetValue.Last();
                    _currentSetValue = _defSetValues.Find(a => a.CurrentLabelID != -1);
                    break;
                case 12://前进
                    _currentSetValue.Next();
                    _currentSetValue = _defSetValues.Find(a => a.CurrentLabelID != -1);
                    break;
                case 13://确认
                    Int32 trainWeight = _defSetValue_Weight_1.GetValue();
                    Int32 trianType = _defSetValue_TrainType_2.GetValue();
                    Int32 trainAxisWeight = _defSetValue_TrainAxis_3.GetValue();
                    Int32 speed = _defSetValue_LinkSpeed_4.GetValue();
                    _dataService.WriteService.ChangeFloat(50, trainWeight);
                    _dataService.WriteService.ChangeFloat(51, trianType);
                    _dataService.WriteService.ChangeFloat(52, trainAxisWeight);
                    _dataService.WriteService.ChangeFloat(53, speed);

                    _defBtn_OK_13.IsDown = true;

                    break;
                default://数字
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
