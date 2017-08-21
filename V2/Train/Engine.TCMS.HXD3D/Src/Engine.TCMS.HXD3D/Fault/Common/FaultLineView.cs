using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Util;

namespace Engine.TCMS.HXD3D.Fault.Common
{
    public class FaultLineView : CommonInnerControlBase
    {
        public EventHandler<EventArgs<FaultMsgHXD3D>> LineClicked;

        private bool m_IsSelected;

        public ReadOnlyCollection<Label> Contents { private set; get; }

        private readonly Action<ReadOnlyCollection<Label>, FaultMsgHXD3D> m_UpdateContentsAction;

        public FaultLineView(List<Label> contents, Action<ReadOnlyCollection<Label>,FaultMsgHXD3D> updateContentsAction)
        {
            Contents = contents.AsReadOnly();
            m_UpdateContentsAction = updateContentsAction;
        }

        public void UpdateContents(FaultMsgHXD3D msg)
        {
            Tag = msg;
            m_UpdateContentsAction(Contents, msg);
        }

        public Brush SelectedBrush { set; get; }


        /// <summary>绘图</summary>
        /// <param name="g"></param>
        public override void OnDraw(Graphics g)
        {
            if (m_IsSelected && SelectedBrush != null)
            {
                 g.FillRectangle(SelectedBrush, OutLineRectangle);
            }

            foreach (var lb in Contents)
            {
                lb.OnDraw(g);
            }
        }

        /// <summary>鼠标按下</summary>
        /// <param name="point"></param>
        public override bool OnMouseDown(Point point)
        {
            if (OutLineRectangle.Contains(point))
            {
                m_IsSelected = true;

                return true;
            }

            return false;
        }

        /// <summary>鼠标按下后弹起</summary>
        /// <param name="point"></param>
        public override bool OnMouseUp(Point point)
        {
            if (OutLineRectangle.Contains(point) && m_IsSelected)
            {
                OnLineClicked(new EventArgs<FaultMsgHXD3D>(Tag as FaultMsgHXD3D));
                m_IsSelected = false;
                return true;
            }

            m_IsSelected = false;

            return false;
        }

        protected virtual void OnLineClicked(EventArgs<FaultMsgHXD3D> e)
        {
            if (LineClicked != null)
            {
                LineClicked.Invoke(this, e);
            }
        }
    }
}