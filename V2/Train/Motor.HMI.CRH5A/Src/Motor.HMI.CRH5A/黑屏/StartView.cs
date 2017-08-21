using System;
using System.Drawing;
using CommonUtil.Controls;
using Motor.HMI.CRH5A.底层共用;

namespace Motor.HMI.CRH5A.黑屏
{
    /// <summary>
    /// 启动界面
    /// </summary>
    public class StartView : CommonInnerControlBase
    {
        private int m_Step;

        private const int MaxStep = 20;

        private bool m_Started;

        private StringFormat m_Format;

        public event Action StartCompleted;

        public bool Stopped { get { return !m_Started; } }

        public bool Started { get { return m_Started; } }

        public StartView()
        {
            m_Format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center};
            Stop();
        }

        public void Start()
        {
            m_Step = 0;
            m_Started = true;
        }

        public void Stop()
        {
            m_Started = false;
        }

        /// <summary>
        /// 跳过启动
        /// </summary>
        public void Skip()
        {
            OnStartCompleted();

            Stop();
        }

        protected void OnStartCompleted()
        {
            Action handler = StartCompleted;
            if (handler != null)
            {
                handler();
            }
        }

        public override void OnDraw(Graphics g)
        {
            if (Stopped)
            {
                return;
            }

            if (++m_Step > MaxStep)
            {
                OnStartCompleted();
                Stop();
            }

            g.DrawString("应用程序正在等待正确配置...", FontsItems.Font14, Brushes.Red, OutLineRectangle, m_Format);
        }
    }
}