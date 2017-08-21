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
    public class V55_BreakingOver : baseClass
    {
        public override string GetInfo()
        {
            return "确认施加惩罚制动";
        }
        public override bool init(ref int nErrorObjectIndex)
        {

            return true;
        }

        public override void paint(Graphics dcGs)
        {
            dcGs.FillRectangle(
                Brushes.Red,
                new Rectangle(200, 100, 400, 400)
                );
            dcGs.DrawString(
               "请确认停放制动\n是否已经缓解\n  \n请按“E”键确认",
               FormatStyle.Font32B,
               Brushes.Black,
               new RectangleF(200, 100, 400, 400),
               new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
               );

            base.paint(dcGs);
        }
    }
}
