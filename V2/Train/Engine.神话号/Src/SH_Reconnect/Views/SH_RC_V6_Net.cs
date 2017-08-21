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
    public partial class SH_RC_V6_Net : UserControl, IView,IDataChangedListener
    {
        public SH_RC_V6_Net()
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
        private List<DefChoice> _defChoice;
        private List<DefValue> _defValue;

        public SH_RC_V6_Net(Int32 id, ViewManager viewManager, ICommunicationDataService dataService)
        {
            InitializeComponent();

            SetStyle(
               ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
               ControlStyles.OptimizedDoubleBuffer, true);

            ID = id;

            ViewManger = viewManager;
            ViewManger.Register(this);

            _dataService = dataService;

            _defChoice = new List<DefChoice>() 
            {
                defChoice1, 
                defChoice2,
                defChoice3, 
                defChoice4,
                defChoice5
            };
            _defValue = new List<DefValue>()
            {
                defValue1,defValue2,defValue3,defValue4,
                defValue5,defValue6,defValue7,defValue8,
                defValue9,defValue10,defValue11
            };
            DataChangedProxy.Instance.Regist(this);
            //_dataService.ReadService.BoolChanged += onBoolChanged;
            //_dataService.ReadService.FloatChanged += onFloatChanged;
        }
        public void OnBoolItemChanged(ref KeyValuePair<int, bool> cmd)
        {
            int key = cmd.Key;
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

        public void OnFloatItemChanged(ref KeyValuePair<int, float> item)
        {

        }
        //void onBoolChanged(object sender, CommunicationDataChangedArgs<bool> e)
        //{
        //    foreach (var cmd in e.NewValue)
        //    {
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

        void onFloatChanged(object sender, CommunicationDataChangedArgs<float> e)
        {
            foreach (var cmd in e.NewValue)
            {
                DefValue val = _defValue.Find(a => a.FloatData.FloatLogic == cmd.Key);
                if (val != null)
                {
                    val.FloatData.InputData = cmd.Value;
                    val.SetValue();
                }
            }
        }

        private void defButton_DefClick(object sender, ButtonClickArgs e)
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


