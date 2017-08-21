using System;
using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Util;
using CRH2MMI.Common.Util;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace CRH2MMI.Common.Screen
{
    class StartingView : CommonInnerControlBase
    {
        public EventHandler StateChanged;

        private ICourseService m_CourseService;

        public StartingState CurretnState
        {
            set
            {
                m_CurretnState = value;
                HandleUtil.OnHandle(StateChanged, this, null);
            }
            get { return m_CurretnState; }
        }

        public Image Icon { set; get; }

        public StartViewConfig StartViewConfig
        {
            get { return m_StartViewConfig; }
            set
            {
                m_StartViewConfig = value;
                m_FirstLine = string.Format("时速{0}公里铁路动车组", value.Speed);
                if (!string.IsNullOrWhiteSpace(value.MoveTail))
                {
                    m_FirstLine += string.Format("({0})",value.MoveTail);
                }

                Period = value.StartPeriod;
            }
        }

        /// <summary>
        /// 启动时间
        /// </summary>
        public uint Period
        {
            set
            {
                m_Period = value;
                m_CurrentTime = m_Period;
            }
            get { return m_Period; }
        }

        private uint m_CurrentTime;

        private uint m_Period;
        private StartingState m_CurretnState;

        private StartViewConfig m_StartViewConfig;
        private string m_FirstLine;

        // ReSharper disable once InconsistentNaming
        private static readonly List<Point> m_TextLocations = new List<Point>()
                                                              {
                                                                  new Point(160, 140),
                                                                  new Point(240, 180),
                                                                  new Point(300, 290),
                                                                  new Point(320, 370)
                                                              };


        public StartingView()
        {
            Period = 25;
        }

        public void OnRestart()
        {
            m_CurrentTime = 0;
            CurretnState = StartingState.Starting;
        }

        public void Init(IDataPackage dataPackage)
        {
            m_CourseService = dataPackage.ServiceManager.GetService<ICourseService>();
            m_CourseService.CourseStateChanged += CourseServiceOnCourseStateChanged;
        }

        private void CourseServiceOnCourseStateChanged(object sender, CourseStateChangedArgs courseStateChangedArgs)
        {
            m_CurretnState = StartingState.Shutdown;
        }

        public void OnShutdown()
        {
            CurretnState = StartingState.Shutdown;
        }

        public override void OnDraw(Graphics g)
        {

            if (CurretnState == StartingState.Shutdown)
            {
                return;
            }

            ++m_CurrentTime;
            if (m_CurrentTime < Period)
            {
                DrawInfo(g);
            }
            else
            {
                -- m_CurrentTime;
                CurretnState = StartingState.Complete;
            }
        }

        private void DrawInfo(Graphics g)
        {
            g.DrawImage(Icon, new Rectangle(100, 120, 60, 60));

            g.DrawString(m_FirstLine, CRH2Resource.Font24, CRH2Resource.WhiteBrush, m_TextLocations[0]);

            g.DrawString("车辆信息控制装置", CRH2Resource.Font24, CRH2Resource.WhiteBrush, m_TextLocations[1]);

            g.DrawString("正在准备。", CRH2Resource.Font24, CRH2Resource.WWBrush, m_TextLocations[2]);

            g.DrawString("请稍等。", CRH2Resource.Font24, CRH2Resource.WWBrush, m_TextLocations[3]);
        }
    }

    enum StartingState
    {
        Starting,
        Complete,
        Shutdown
    }
}
