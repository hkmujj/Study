using System;
using System.Drawing;
using CommonUtil.Controls;
using Motor.HMI.CRH5A.底层共用;
using Coordinate = Motor.HMI.CRH5A.底层共用.Coordinate;

namespace Motor.HMI.CRH5A.主屏
{
    internal class HomeScreenItem : GDIRectText
    {
        public Action<HomeScreenItem> ConfirmSelected;

        private static readonly Pen RedPen = new Pen(SolidBrushsItems.RedBrush1, 3.0f * Coordinate.Scaling);

        public void OnConfirmSelected()
        {
            Action<HomeScreenItem> handler = ConfirmSelected;
            if (handler != null)
            {
                handler(this);
            }
        }

        public bool Selected
        {
            set
            {
                NeedDarwOutline = value;
            }
            get { return NeedDarwOutline; }
        }

        public HomeScreenItem()
        {
            OutLinePen = RedPen;
        }


    }
}