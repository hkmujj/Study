using System;
using System.Windows.Forms;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using System.Collections.Generic;
using CommonControls;
using System.Linq;
using YunDa.JC.MMI.Common;
using MyControls;
using YunDa.JC.MMI.Common.Extensions;

namespace SH_Reconnect.Views
{
    public partial class SH_RC_V1_EquipmentData : UserControl, IView,IDataChangedListener
    {
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

        private List<DefValue> _defValue;
        private List<DefDisplay> _defDisplay;

        public SH_RC_V1_EquipmentData()
        {
            InitializeComponent();
        }

        public SH_RC_V1_EquipmentData(Int32 id, ViewManager viewManager, ICommunicationDataService dataService)
        {
            InitializeComponent();

            SetStyle(
               ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
               ControlStyles.OptimizedDoubleBuffer, true);

            ID = id;

            ViewManger = viewManager;
            ViewManger.Register(this);

            _dataService = dataService;
            _defValue = new List<DefValue>()
            {
                defValue1,
                defValue2,
                defValue3,
                defValue4,
                defValue5,
                defValue6,
                defValue7
            };
            _defDisplay = new List<DefDisplay>() 
            {
                defDisplay1 ,defDisplay2,
                defDisplay3,defDisplay4,
                defDisplay5,defDisplay6,
                defDisplay7,defDisplay8,
                defDisplay9,defDisplay10,
                defDisplay11,defDisplay12,
                defDisplay13,defDisplay14,
                defDisplay15,defDisplay16,
            };
            DataChangedProxy.Instance.Regist(this);
            //_dataService.ReadService.FloatChanged += onFloatChanged;
            //_dataService.ReadService.BoolChanged += onBoolChanged;
        }

        //void onFloatChanged(object sender, CommunicationDataChangedArgs<float> e)
        //{
        //    foreach (var cmd in e.NewValue)
        //    {
        //        DefValue val = _defValue.Find(a => a.FloatData.FloatLogic == cmd.Key);
        //        if (val != null)
        //        {
        //            val.FloatData.InputData = cmd.Value;
        //            val.SetValue();
        //        }
        //    }
        //}

        //void onBoolChanged(object sender, CommunicationDataChangedArgs<bool> e)
        //{
        //    foreach (var cmd in e.NewValue)
        //    {
        //        foreach (var item in _defDisplay)
        //        {
        //            BoolConfigInfo item1 = item.BoolLogicSet.Find(a => a.BoolLogic == cmd.Key);
        //            if (item1 != null)
        //            {
        //                item1.InputData = cmd.Value;
        //                item.SetColor();
        //                break;
        //            }
        //        }
        //    }
        //}
        public void OnBoolItemChanged(ref KeyValuePair<int, bool> item)
        {
            int key = item.Key;
            foreach (var it in _defDisplay)
            {
                BoolConfigInfo item1 = it.BoolLogicSet.Find(a => a.BoolLogic == key);
                if (item1 != null)
                {
                    item1.InputData = item.Value;
                    it.SetColor();
                    break;
                }
            }
        }

        public void OnFloatItemChanged(ref KeyValuePair<int, float> item)
        {
            int key = item.Key;
            DefValue val = _defValue.Find(a => a.FloatData.FloatLogic == key);
            if (val != null)
            {
                val.FloatData.InputData = item.Value;
                val.SetValue();
            }
        }
        private void defLabel6_MouseDown(object sender, MouseEventArgs e)
        {
            defButton10.IsFocused = false;
            defButton9.IsFocused = false;
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

        private void defButton_MouseClick(object sender, MouseEventArgs e)
        {
            defButton10.IsFocused = false;
            defButton9.IsFocused = false;
            ((DefButton)sender).IsFocused = true;
        }

        private void defTouchBtn1_Click(object sender, EventArgs e)
        {
            _dataService.WriteService.ChangeBool(320, true);
        }
        public void InvalidateNew() { }
    }
}
