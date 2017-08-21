using System;
using System.Windows.Forms;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using System.Collections.Generic;
using CommonControls;
using System.Linq;
using YunDa.JC.MMI.Common;
using System.Drawing;
using System.Drawing.Drawing2D;
using MyControls;
using YunDa.JC.MMI.Common.Extensions;

namespace SH_Reconnect.Views
{
    public partial class SH_RC_V2_Net : UserControl, IView,IDataChangedListener
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

        private List<DefShow> _defShow;
        private List<DefLine> _defLine;

        public SH_RC_V2_Net()
        {
            InitializeComponent();
        }

        public SH_RC_V2_Net(Int32 id, ViewManager viewManager, ICommunicationDataService dataService)
        {
            InitializeComponent();

            SetStyle(
               ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
               ControlStyles.OptimizedDoubleBuffer, true);

            ID = id;

            ViewManger = viewManager;
            ViewManger.Register(this);
            _dataService = dataService;
            _defShow = new List<DefShow>()
            {
                defShow1,defShow2,defShow3,defShow4,defShow5,
                defShow6,defShow7,defShow8,defShow9,defShow10,
                defShow11,defShow12,defShow13,defShow14,defShow15,
                defShow16,defShow17,defShow18,defShow19,defShow20,
                defShow21,defShow22,defShow23,defShow24,defShow25,
                defShow26
            };
            _defLine = new List<DefLine>()
            {
                defLine1,defLine2,defLine3,defLine4,
                defLine5,defLine6,defLine7,defLine8,
                defLine9,defLine10,defLine11,defLine12,
                defLine13,defLine14,defLine15,defLine16,
                defLine17,defLine18,defLine19,defLine20,
                defLine21,defLine22,defLine23,defLine24,
                defLine25
            };
            DataChangedProxy.Instance.Regist(this);
            //_dataService.ReadService.BoolChanged += onBoolChanged;
        }

        //void onBoolChanged(object sender, CommunicationDataChangedArgs<bool> e)
        //{
        //    BoolConfigInfo item1 = null;
        //    foreach (var cmd in e.NewValue)
        //    {
        //        foreach (var item in _defShow)
        //        {
        //            item1 = item.BoolData.Find(a => a.BoolLogic == cmd.Key);
        //            if (item1 != null)
        //            {
        //                item1.InputData = cmd.Value;
        //                item.SetColor();
        //                break;
        //            }
        //        }
        //        if (defShow15.BackColor == Color.Lime && defShow5.BackColor == Color.Lime)
        //        {
        //            defLine27.LineColor = Color.Lime;
        //            defLine28.LineColor = Color.Lime;
        //            defLine26.LineColor = Color.Lime;
        //            defLine27.SetColor();
        //            defLine26.SetColor();
        //            defLine25.SetColor();
        //        }
        //        else 
        //        {
        //            if (defShow15.BackColor == Color.Red || defShow5.BackColor == Color.Red)
        //            {
        //                defLine27.LineColor = Color.Red;
        //                defLine28.LineColor = Color.Red;
        //                defLine26.LineColor = Color.Red;
        //                defLine27.SetColor();
        //                defLine26.SetColor();
        //                defLine25.SetColor();
        //            }
        //            else 
        //            {
        //                defLine27.LineColor = Color.Gray;
        //                defLine28.LineColor = Color.Gray;
        //                defLine26.LineColor = Color.Gray;
        //                defLine27.SetColor();
        //                defLine26.SetColor();
        //                defLine25.SetColor();
        //            }
        //        }
        //        foreach (var item in _defLine)
        //        {
        //            item1 = item.BoolData.Find(a => a.BoolLogic == cmd.Key);
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
            BoolConfigInfo item1 = null;
            int key = item.Key;
            foreach (var it in _defShow)
            {
                item1 = it.BoolData.Find(a => a.BoolLogic == key);
                if (item1 != null)
                {
                    item1.InputData = item.Value;
                    it.SetColor();
                    break;
                }
            }
            if (defShow15.BackColor == Color.Lime && defShow5.BackColor == Color.Lime)
            {
                defLine27.LineColor = Color.Lime;
                defLine28.LineColor = Color.Lime;
                defLine26.LineColor = Color.Lime;
                defLine27.SetColor();
                defLine26.SetColor();
                defLine25.SetColor();
            }
            else
            {
                if (defShow15.BackColor == Color.Red || defShow5.BackColor == Color.Red)
                {
                    defLine27.LineColor = Color.Red;
                    defLine28.LineColor = Color.Red;
                    defLine26.LineColor = Color.Red;
                    defLine27.SetColor();
                    defLine26.SetColor();
                    defLine25.SetColor();
                }
                else
                {
                    defLine27.LineColor = Color.Gray;
                    defLine28.LineColor = Color.Gray;
                    defLine26.LineColor = Color.Gray;
                    defLine27.SetColor();
                    defLine26.SetColor();
                    defLine25.SetColor();
                }
            }
            foreach (var it in _defLine)
            {
                item1 = it.BoolData.Find(a => a.BoolLogic == key);
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

        }
        
        private void defButton_DefClick(object sender, ButtonClickArgs e)
        {
            ViewManger.CurrentViewID = e.ViewID;
        }

        private void defLabel_MouseDown(object sender, MouseEventArgs e)
        {
            defButton5.IsFocused = false;
        }


        private void defButton_MouseDown(object sender, MouseEventArgs e)
        {
            ((DefButton)sender).IsFocused = true;
            defButton5.IsFocused = false;
        }

        private void defButton_MouseUp(object sender, MouseEventArgs e)
        {
            ((DefButton)sender).IsFocused = false;
        }

        private void defButton_MouseClick(object sender, MouseEventArgs e)
        {
            //defButton10.IsFocused = false;
            //defButton9.IsFocused = false;
            ((DefButton)sender).IsFocused = true;
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    e.Graphics.DrawString("LocoBus", this.defLabel3.Font, Brushes.White, new RectangleF(230, 450, 100, 50));
        //}
        public void InvalidateNew() { }
    }
}
