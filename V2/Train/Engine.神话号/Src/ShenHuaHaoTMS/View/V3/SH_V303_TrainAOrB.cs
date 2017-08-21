using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommonControls;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Service;
using ShenHuaHaoTMS.Common;
using YunDa.JC.MMI.Common;
using YunDa.JC.MMI.Common.Extensions;

namespace ShenHuaHaoTMS.View
{
    public partial class SH_V303_TrainAOrB : UserControl, IView
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

        private List<DefButton> _viewBtns = new List<DefButton>();//跳转页面的按钮
        private List<DefButton> _intputBtns = new List<DefButton>();//数字键与右侧功能键

        public SH_V303_TrainAOrB()
        {
            InitializeComponent();
        }

        public SH_V303_TrainAOrB(Int32 id, ViewManager viewManager, ICommunicationDataService dataService)
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
            Margin = new Padding(0);
            _dataService = dataService;
            m_ListenViewChanged.BoolChanged += onViewChanged;
            ID = id;
            ViewManger = viewManager;
            ViewManger.Register(this);

            _viewBtns = new List<DefButton>()
            {
                _DefBtn_Return_0
            };
            _intputBtns = new List<DefButton>()
            {
                _btn_Data_1,_btn_Data_2,_btn_Data_3,_btn_Data_4,_btn_Data_5,_btn_Data_6,_btn_Data_7,_btn_Data_8,_btn_Data_9,_btn_Data_10
            };
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
                                default://数字
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public void InvalidateNew()
        {
        }
    }
}
