using System.Drawing;
using CommonUtil.Controls;

namespace Urban.Iran.HMI
{
    public class SelectableControl : CommonInnerControlBase
    {
        public CommonInnerControlBase InnerControl { get; private set; }

        public bool Selected { set; get; }

        public Pen SelectedPen { set; get; }

        public override Rectangle OutLineRectangle
        {
            get { return InnerControl.OutLineRectangle; }
            set { InnerControl.OutLineRectangle = value; }
        }


        public SelectableControl(CommonInnerControlBase innerControl)
        {
            InnerControl = innerControl;
        }

        public override bool OnMouseDown(Point point)
        {
            return InnerControl.OnMouseDown(point);
        }

        public override bool OnMouseUp(Point point)
        {
            return InnerControl.OnMouseUp(point);
        }

        public override void OnPaint(Graphics g)
        {
            Refresh();

            InnerControl.Refresh();

            OnDraw(g);
        }

        public override void OnDraw(Graphics g)
        {
            InnerControl.OnDraw(g);
            if (Selected && SelectedPen != null)
            {
                g.DrawRectangle(SelectedPen, OutLineRectangle);
            }
        }

        public override bool Contains(Point point)
        {
            return InnerControl.Contains(point);
        }

        public override void Init()
        {
            InnerControl.Init();
        }
    }
}