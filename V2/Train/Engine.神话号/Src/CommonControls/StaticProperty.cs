using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CommonControls
{
    public class StaticProperty
    {
        public static StringFormat SfCenter = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        public static StringFormat SfLeftCenter = new StringFormat()
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Center
        };

        public static StringFormat SfRightCenter = new StringFormat()
        {
            Alignment = StringAlignment.Far,
            LineAlignment = StringAlignment.Center
        };

        public static StringFormat SfLeftTop = new StringFormat()
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Near
        };

        public static Font Font12 = new Font("Verdana", 12);

        public static Font Font11 = new Font("Verdana", 11);

        public static Font FontSont11 = new Font("宋体", 11);

        public static Font Font10 = new Font("Verdana", 10);
    }
}
