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
    public partial class SH_RC_V5_Net : UserControl, IView,IDataChangedListener
    {
        public SH_RC_V5_Net()
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

        private List<DefValue> _defValue;

        public SH_RC_V5_Net(Int32 id, ViewManager viewManager, ICommunicationDataService dataService)
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
                defValue2
            };
            //_dataService.ReadService.FloatChanged += onFloatChanged;
            DataChangedProxy.Instance.Regist(this);
        }
        public void OnBoolItemChanged(ref KeyValuePair<int, bool> item)
        {
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


