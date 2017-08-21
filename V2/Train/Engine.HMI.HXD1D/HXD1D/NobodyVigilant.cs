using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using HXD1D.Common;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using System.Windows.Forms;

namespace HXD1D
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class NobodyVigilant : baseClass
    {
        private Timer _timer;
        private Int32 _countDown = 10;

        public Boolean IsCountDownStart
        {
            set
            {
                if (_isCountDownStart == value) return;
                _isCountDownStart = value;
                if (_isCountDownStart)
                {
                    _countDown = 10;
                    this._timer.Start();
                }
                else this._timer.Stop();
            }
        }
        private Boolean _isCountDownStart = false;

        public override string GetInfo()
        {
            return "无人警惕";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            this._timer = new Timer();
            this._timer.Interval = 1000;
            this._timer.Tick += _timer_Tick;

            return true;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            _countDown--;
        }

        public override void paint(Graphics dcGs)
        {

            IsCountDownStart = BoolList[UIObj.InBoolList[0]];
            if (_countDown == 0)
            {
                _timer.Stop();
                return;
            }
            if (_isCountDownStart)//绘制无人警惕界面
            {
                dcGs.FillRectangle(Brushes.Yellow, new Rectangle(205, 183, 420, 254));
                dcGs.DrawString(
                    "警惕",
                    new Font("Arial", 30),
                    Brushes.Red,
                    new Rectangle(205, 183, 420, 154),
                    new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                    );
                dcGs.DrawString(
                    _countDown.ToString(),
                    new Font("Arial", 50),
                    Brushes.Red,
                    new Rectangle(205, 183, 420, 274),
                    new StringFormat() {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center}
                    );
            }
        }
    }
}
