using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using HXD1D.Common;
using HXD1D.Titlte;
using HXD1D.控制设置;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using System.Timers;

namespace HXD1D
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V54_BreakingOut : baseClass
    {
        private Timer _timer;
        private Int32 count = 0;
        private Boolean _isOver = false;

        public override string GetInfo()
        {
            return "施加惩罚制动";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            _timer = new Timer(1000);
            _timer.Elapsed += _timer_Elapsed;

            return true;
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            count++;
            if (count == 6)
            {
                count = 0;
                _timer.Enabled = false;
                _timer.Stop();
                _isOver = true;
            }
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                if (nParaB == 54)
                {
                    _timer.Enabled = true;
                    _timer.Start();
                    _isOver = false;
                }
            }
            
            base.setRunValue(nParaA, nParaB, nParaC);
        }

        public override void paint(Graphics dcGs)
        {
            if (_isOver)
            {
                append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, 55, 0, 0);
            }

            dcGs.FillRectangle(
                Brushes.Red,
                new Rectangle(0, 200, 800, 200)
                );
            dcGs.DrawString(
               "施加惩罚全制动(操纵后备阀)。",
               FormatStyle.Font32B,
               Brushes.Black,
               new RectangleF(0, 200, 800, 200),
               new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
               );
            
            base.paint(dcGs);
        }
    }
}
