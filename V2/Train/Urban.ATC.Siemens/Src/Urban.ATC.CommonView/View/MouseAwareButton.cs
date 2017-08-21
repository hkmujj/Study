using System;
using System.Windows.Forms;
using Motor.ATP.CommonView.Utils.Extensions;

namespace Motor.ATP.CommonView.View
{
    public partial class MouseAwareButton : Button
    {
        private MouseEventArgs m_MouseEventArgs;
        private readonly Action m_ClickAction;
        private readonly Action m_MouseDownAction;
        private readonly Action m_MouseUpAction;

        public MouseAwareButton()
        {
            InitializeComponent();
            m_ClickAction = PerformClick;
            m_MouseDownAction = () => OnMouseDown(m_MouseEventArgs);
            m_MouseUpAction = () => OnMouseUp(m_MouseEventArgs);
            m_MouseEventArgs = new MouseEventArgs(MouseButtons.Left, 0, Left + Size.Width/2, Top + Size.Height/2, 0);
            LocationChanged += OnRegionChanged;
            SizeChanged += OnRegionChanged;
        }

        private void OnRegionChanged(object sender, EventArgs eventArgs)
        {
            m_MouseEventArgs = new MouseEventArgs(MouseButtons.Left, 0, Left + Size.Width / 2, Top + Size.Height / 2, 0);
        }

        /// <summary>
        /// 手动触发 MouseClick
        /// </summary>
        public void RaiseMouseClick()
        {
            
            this.InvokeIfNeed(m_ClickAction);
        }

        /// <summary>
        /// 手动触发 MouseDown
        /// </summary>
        public void RaiseMouseDown()
        {
            
            this.InvokeIfNeed(m_MouseDownAction);
        }

        /// <summary>
        /// 手动触发 MouseUp
        /// </summary>
        public void RaiseMouseUp()
        {
            this.InvokeIfNeed(m_MouseUpAction);
        }
    }
}
