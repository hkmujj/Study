
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
    public partial class SH_RC_V3_Net : UserControl, IView, IDataChangedListener
    {
        public SH_RC_V3_Net()
        {
            InitializeComponent();
        }
        public int ID { get; set; }
        private bool _isShow = false;
        public ViewManager ViewManger { get; set; }
        private ICommunicationDataService _dataService;

        private List<DefValue> _defValue;
        private List<ShowBtn> _showBtn;
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
        public SH_RC_V3_Net(Int32 id, ViewManager viewManager, ICommunicationDataService dataService)
        {
            InitializeComponent();

            SetStyle(
               ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
               ControlStyles.OptimizedDoubleBuffer, true);

            ID = id;
            ViewManger = viewManager;

            ViewManger.Register(this);
            _dataService = dataService;
            this._defValue = new List<DefValue>()
            {
                defValue1,defValue2,defValue3,defValue4,
                defValue5,defValue6,defValue7,defValue8,
                defValue9,defValue10,defValue11,defValue12,
                defValue13,defValue14,defValue15,defValue16,
                defValue17,defValue18,defValue19,defValue20,
                defValue21,defValue22,defValue23,defValue24,
                defValue25,defValue26,defValue27,defValue28,
                defValue29,defValue30,defValue31,defValue32,
                defValue33,defValue34,defValue35,defValue36,
                defValue37,defValue38,defValue39,defValue40,
                defValue41,defValue42,defValue43,defValue44,
                defValue45,defValue46,defValue47,defValue48,
                defValue49,defValue50,defValue51,defValue52
            };
            this._showBtn = new List<ShowBtn>()
            {
                showBtn1,showBtn2,showBtn3,showBtn4,
                showBtn5,showBtn6,showBtn7,showBtn8,
                showBtn9,showBtn10,showBtn11,showBtn12,
                showBtn13,showBtn14,showBtn15,showBtn16,
                showBtn17,showBtn18,showBtn19,showBtn20,
                showBtn21,showBtn22,showBtn23,showBtn24,
                showBtn25,showBtn26,showBtn27,showBtn28,
                showBtn29,showBtn30,showBtn31,showBtn32,
                showBtn33,showBtn34,showBtn35,showBtn36,
                showBtn37,showBtn38,showBtn39,showBtn40,
                showBtn41,showBtn42,showBtn43,showBtn44,
                showBtn45,showBtn46,showBtn47,showBtn48,
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
        }

        public void OnFloatItemChanged(ref KeyValuePair<int, float> cmd)
        {
            int key = cmd.Key;
            DefValue val = _defValue.Find(a => a.FloatData.FloatLogic == key);
            if (val != null)
            {
                val.FloatData.InputData = cmd.Value;
                val.SetValue();
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
