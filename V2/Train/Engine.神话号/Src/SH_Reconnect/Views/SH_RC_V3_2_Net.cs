
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
    public partial class SH_RC_V3_2_Net : UserControl, IView,IDataChangedListener
    {
        public SH_RC_V3_2_Net()
        {
            InitializeComponent();
        }
        public int ID { get; set; }
        private bool _isShow = false;
        public ViewManager ViewManger { get; set; }
        private ICommunicationDataService _dataService;
        private List<ShowBtn> _showBtn;
        public bool IsShow
        {
            get { return _isShow; }
            set
            {
                if (IsShow == value) return;
                _isShow = value;
                if (_isShow)   //显示
                {
                    if (!ViewManger.Contains(this))
                    {
                        ViewManger.Add(this);
                        this.InvokeIfNeed(new Action(Show));
                    }
                }
                else     //隐藏
                {
                    if (ViewManger.Contains(this))
                    {
                        ViewManger.Remove(this);
                        this.InvokeIfNeed(new Action(Hide));
                    }
                }
            }
        }
        public SH_RC_V3_2_Net(Int32 id, ViewManager viewManager, ICommunicationDataService dataService)
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
                showBtn1,showBtn2,showBtn3,showBtn4,showBtn5,
                showBtn6,showBtn7,showBtn8,showBtn9,showBtn10,
                showBtn11,showBtn12,showBtn13,showBtn14,showBtn15,
                showBtn16,showBtn17,showBtn18,showBtn19,showBtn20,
                showBtn21,showBtn22,showBtn23,showBtn24,showBtn25,
                showBtn26,showBtn27,showBtn28,showBtn29,showBtn30,
                showBtn31,showBtn32,showBtn33,showBtn34,showBtn35,
                showBtn36,showBtn37,showBtn38,showBtn39,showBtn40,
                showBtn41,showBtn42,showBtn43,showBtn44,showBtn45,
                showBtn46,showBtn47,showBtn48,showBtn49,showBtn50,
                showBtn51,showBtn52,showBtn53,showBtn54,showBtn55,
                showBtn56,showBtn57,showBtn58,showBtn59,showBtn60,
                showBtn61,showBtn62,showBtn63,showBtn64,showBtn65,
                showBtn66,showBtn67,showBtn68,showBtn69,showBtn70,
                showBtn71,showBtn72,showBtn73,showBtn74,showBtn75,
                showBtn76,showBtn77,showBtn78,showBtn79,showBtn80,
                showBtn81,showBtn82,showBtn83,showBtn84,showBtn85,
                showBtn86,showBtn87,showBtn88,showBtn89,showBtn90,
                showBtn91,showBtn92,showBtn93,showBtn94,showBtn95,
                showBtn96,showBtn97,showBtn98,showBtn99,showBtn100,
                showBtn101,showBtn102,showBtn103,showBtn104,showBtn105,
                showBtn106,showBtn107,showBtn108,showBtn109,showBtn110,
                showBtn111,showBtn112,showBtn113,showBtn114,showBtn115,
                showBtn116,showBtn117,showBtn118,showBtn119,showBtn120,
                showBtn121,showBtn122,showBtn123,showBtn124,showBtn125,
                showBtn126
            };
            DataChangedProxy.Instance.Regist(this);
            //_dataService.ReadService.BoolChanged += onBoolChanged;
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

        public void OnFloatItemChanged(ref KeyValuePair<int, float> item)
        {

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
            ((DefButton)sender).IsFocused = true;
        }
        public void InvalidateNew() { }
    }
}
