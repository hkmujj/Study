using System;
using System.Windows.Forms;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using System.Collections.Generic;
using CommonControls;
using System.Drawing;
using System.Linq;
using YunDa.JC.MMI.Common;
using MyControls;
using YunDa.JC.MMI.Common.Extensions;

namespace SH_Reconnect.Views
{
    public partial class SH_RC_V4_1_Net : UserControl, IView,IDataChangedListener
    {
        public SH_RC_V4_1_Net()
        {
            InitializeComponent();
        }
        public int ID { get; set; }

        public bool IsShow
        {
            get { return _isShow; }
            set
            {
                if (_isShow == value) return;
                _isShow = value;

                if (_isShow) //显示
                {
                    if (!ViewManger.Contains(this))
                    {
                        ViewManger.Add(this);
                        
                        this.InvokeIfNeed(new Action(Show));
                    }
                }
                else//隐藏
                {
                    if (ViewManger.Contains(this))
                    {
                        ViewManger.Remove(this);
                        this.InvokeIfNeed(new Action(Hide));
                    }
                }
            }
        }
        private bool _isShow = false;

        public ViewManager ViewManger { get; set; }

        private ICommunicationDataService _dataService;
        private List<ShowBtn> _showBtn;
        private List<DefValue> _defValue;
        private List<DefChoice> _defChoice;

        public SH_RC_V4_1_Net(Int32 id, ViewManager viewManager, ICommunicationDataService dataService)
        {
            InitializeComponent();

            SetStyle(
               ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
               ControlStyles.OptimizedDoubleBuffer, true);

            ID = id;

            ViewManger = viewManager;
            ViewManger.Register(this);

            _dataService = dataService;
            _showBtn = new List<ShowBtn>()
            {
                showBtn1,showBtn2,showBtn3,showBtn4,
                showBtn8,showBtn5,showBtn6,showBtn7,
                showBtn9,showBtn10,showBtn11,showBtn12,
                showBtn13,showBtn14,showBtn15,showBtn16,
                showBtn17,showBtn18,showBtn19,showBtn20,
                showBtn21,showBtn22,showBtn23,showBtn24,
                showBtn25,showBtn26,showBtn27,showBtn28,
                showBtn29,showBtn30,showBtn31,showBtn32,
                showBtn33,showBtn34,showBtn35,showBtn36,
            };
            _defValue = new List<DefValue>()
            {
                defValue1,
                defValue2
            };
            _defChoice = new List<DefChoice>()
            {
                defChoice1,
                defChoice2
            };
            DataChangedProxy.Instance.Regist(this);
            //_dataService.ReadService.BoolChanged += onBoolChanged;
            //_dataService.ReadService.FloatChanged += onFloatChanged;
        }
        
        //void onBoolChanged(object sender, CommunicationDataChangedArgs<bool> e)
        //{
        //    foreach (var cmd in e.NewValue)
        //    {
        //        foreach (var item in _showBtn)
        //        {
        //            BoolConfigInfo item1 = item.BoolLogicSet.Find(a => a.BoolLogic == cmd.Key);
        //            if (item1 != null)
        //            {
        //                item1.InputData = cmd.Value;
        //                item.SetColor();
        //                break;
        //            }
        //        }
        //        foreach (var item in _defChoice)
        //        {
        //            BoolConfigInfo item1 = item.BoolData.Find(a => a.BoolLogic == cmd.Key);
        //            if (item1 != null)
        //            {
        //                item1.InputData = cmd.Value;
        //                item.SetValue();
        //                break;
        //            }
        //        }
        //    }
        //}

        //void onFloatChanged(object sender, CommunicationDataChangedArgs<float> e)
        //{
        //    foreach (var cmd in e.NewValue)
        //    {
        //        DefValue val = _defValue.Find(a => a.FloatData.FloatLogic == cmd.Key);
        //        if (val != null)
        //        {
        //            val.FloatData.InputData = cmd.Value;
        //        }
        //    }
        //}
        public void OnBoolItemChanged(ref KeyValuePair<int, bool> cmd)
        {
            int key = cmd.Key;
            foreach (var item in _showBtn)
            {
                BoolConfigInfo item1 = item.BoolLogicSet.Find(a => a.BoolLogic == key);
                if (item1 != null)
                {
                    item1.InputData = cmd.Value;
                    item.SetColor();
                    break;
                }
            }
            foreach (var item in _defChoice)
            {
                BoolConfigInfo item1 = item.BoolData.Find(a => a.BoolLogic == key);
                if (item1 != null)
                {
                    item1.InputData = cmd.Value;
                    item.SetValue();
                    break;
                }
            }
        }

        public void OnFloatItemChanged(ref KeyValuePair<int, float> cmd)
        {
            int key = cmd.Key;
            DefValue val = _defValue.Find(a => a.FloatData.FloatLogic == key);
            if (val != null)
            {
                val.FloatData.InputData = cmd.Value;
            }
        }

        private void defButtonDefClick(object sender, ButtonClickArgs e)
        {
            ViewManger.CurrentViewID = e.ViewID;
        }

        private void defButton_MouseDown(object sender, MouseEventArgs e)
        {
            ((DefButton)sender).IsFocused = true;
        }

        private void defButton_MouseUp(object sender, MouseEventArgs e)
        {
            ((DefButton)sender).IsFocused = false;
        }
        public void InvalidateNew() { }
    }
}


