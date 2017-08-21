using System;
using System.Diagnostics;
using System.Drawing;
using CRH2MMI.Common;
using CRH2MMI.Common.Config;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace CRH2MMI.BreakLocked
{
    interface IScreensavers
    {
        void FeedWatcher();

        /// <summary>
        /// 检查是否进入屏保
        /// </summary>
        void CheckToScreensavers();
    }

    /// <summary>
    /// 屏保
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    internal class Screensavers : CRH2BaseClass, IDisposable, IScreensavers
    {
        private ViewConfig m_LastViewConfig;

        private static readonly TimeSpan SwitchTimeSpan = new TimeSpan(0,0,15,0);

        private Stopwatch m_Stopwatch;

        private bool m_IsActive;

        private bool m_UsedThis;

        public void FeedWatcher()
        {
            if (m_UsedThis)
            {
                m_Stopwatch.Restart();
                if (m_IsActive)
                {
                    append_postCmd(CmdType.ChangePage, (int) m_LastViewConfig, 0, 0);
                }
                m_IsActive = false;
            }
        }

        public void CheckToScreensavers()
        {
            if (m_UsedThis)
            {
                if (m_Stopwatch.Elapsed > SwitchTimeSpan)
                {
                    m_IsActive = true;
                }
                if (m_IsActive)
                {
                    append_postCmd(CmdType.ChangePage, (int) ViewConfig.Screensavers, 0, 0);
                }
            }
        }

        public override bool Init()
        {
            m_UsedThis = false;
            if (m_UsedThis)
            {
                m_IsActive = false;
                m_Stopwatch = new Stopwatch();
                m_Stopwatch.Start();

            }

            return true;
        }

        protected override bool OnMouseDown(Point point)
        {
            FeedWatcher();
            return true;
        }

        public override void NavigateFrom(ViewConfig fromOhter)
        {
            m_LastViewConfig = fromOhter;
        }

        public override void paint(Graphics g)
        {
        }

        public void Dispose()
        {
            if (m_UsedThis)
            {
                m_Stopwatch.Stop();
                m_IsActive = false;
            }
        }
    }
}