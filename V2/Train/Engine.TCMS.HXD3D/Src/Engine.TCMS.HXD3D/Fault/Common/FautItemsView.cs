using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Util;

namespace Engine.TCMS.HXD3D.Fault.Common
{
    public class FautItemsView : CommonInnerControlBase
    {
        public event EventHandler<EventArgs<FaultMsgHXD3D>> ItemClicked;

        // ReSharper disable once InconsistentNaming
        protected List<Line> m_GridLines;

        // ReSharper disable once InconsistentNaming
        protected IEnumerable<FaultMsgHXD3D> m_ShowingMsgs;

        // ReSharper disable once InconsistentNaming
        protected List<FaultLineView> m_FaultLineViews;

        private IEnumerable<FaultLineView> FaultItemLines { get { return m_FaultLineViews.Skip(1); } }

        /// <summary>绘图</summary>
        /// <param name="g"></param>
        public override void OnDraw(Graphics g)
        {
            m_GridLines.ForEach(e => e.OnDraw(g));

            m_FaultLineViews.ForEach(e => e.OnDraw(g));
        }

        public virtual void UpdateFaults(IEnumerable<FaultMsgHXD3D> msgs)
        {
            var mit = msgs.GetEnumerator();
            foreach (var view in FaultItemLines)
            {
                view.UpdateContents(mit.MoveNext() ? mit.Current : null);
            }
        }

        /// <summary>鼠标按下</summary>
        /// <param name="point"></param>
        public override bool OnMouseDown(Point point)
        {
            foreach (var e in FaultItemLines)
            {
                e.OnMouseDown(point);
            }

            return true;
        }

        /// <summary>鼠标按下后弹起</summary>
        /// <param name="point"></param>
        public override bool OnMouseUp(Point point)
        {
            foreach (var e in FaultItemLines)
            {
                e.OnMouseUp(point);
            }

            return true;
        }

        protected virtual void OnItemClicked(EventArgs<FaultMsgHXD3D> e)
        {
            if (ItemClicked != null)
            {
                ItemClicked.Invoke(this, e);
            }
        }
    }
}