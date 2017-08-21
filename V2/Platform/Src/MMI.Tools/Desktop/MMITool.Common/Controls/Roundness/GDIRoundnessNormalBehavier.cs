using System;
using System.Drawing;

namespace MMITool.Common.Controls.Roundness
{
    /// <summary>
    /// 普通行为
    /// </summary>
    public class GDIRoundnessNormalBehavier : IBehavierStratege<GDIRoundness>
    {
        private Action<Graphics> m_DrawAction;

        private bool m_LastNeedDrawArcState;

        private bool m_LastNeedDrawContentState;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public GDIRoundnessNormalBehavier(GDIRoundness control)
        {
            Control = control;
            m_LastNeedDrawArcState = Control.NeedDrawArc;
            m_LastNeedDrawContentState = Control.NeedDrawContent;
            if (control.NeedDrawArc)
            {
                m_DrawAction += Control.DrawArc;
            }
            if (control.NeedDrawContent)
            {
                m_DrawAction += Control.DrawContent;
            }

            Control.NeedDrawArcChanged = (sender, args) =>
            {
                if (m_LastNeedDrawArcState != Control.NeedDrawArc)
                {
                    m_LastNeedDrawArcState = Control.NeedDrawArc;
                    if (m_LastNeedDrawContentState)
                    {
                        m_DrawAction += Control.DrawArc;
                    }
                    else
                    {
                        // ReSharper disable once DelegateSubtraction
                        m_DrawAction -= Control.DrawArc;
                    }
                }
            };
            Control.NeedDrawContentChanged = (sender, args) =>
            {
                if (m_LastNeedDrawContentState != Control.NeedDrawContent)
                {
                    m_LastNeedDrawContentState = Control.NeedDrawContent;
                    if (m_LastNeedDrawContentState)
                    {
                        m_DrawAction += Control.DrawContent;
                    }
                    else
                    {
                        // ReSharper disable once DelegateSubtraction
                        m_DrawAction -= Control.DrawContent;
                    }
                }
            };
        }

        /// <summary>
        /// Control
        /// </summary>
        public GDIRoundness Control { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool OnMouseDown(Point point)
        {
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool OnMouseUp(Point point)
        {
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        public void OnDraw(Graphics g)
        {
            //Control.Refresh();

            if (m_DrawAction != null)
            {
                m_DrawAction(g);
            }
        }
    }
}
