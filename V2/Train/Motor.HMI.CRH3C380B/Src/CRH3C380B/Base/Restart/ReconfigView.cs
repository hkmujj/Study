using System;
using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

using Motor.HMI.CRH3C380B.Base.系统;
using Motor.HMI.CRH3C380B.Common;
using ProjectType = Motor.HMI.CRH3C380B.Common.ProjectType;

namespace Motor.HMI.CRH3C380B.Base.Restart
{
    /// <summary>
    /// 重启后配置
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class ReconfigView : CRH3C380BBase
    {
        private ReconfigViewUnstartPage m_ReconfigViewUnstartPage;
        private ReconfigViewConfigingPage m_ReconfigViewConfigingPage;

        //private List<GraphicsPath> m_CarPathCollection;
        private List<CommonInnerControlBase> m_Cellection;
        public ReconfigState ReconfigState { set; get; }

        private ReconfigViewPage m_CurrentPage;

        private static readonly List<ReconfigView> AllReconfigViewCollection;

        static ReconfigView()
        {
            AllReconfigViewCollection = new List<ReconfigView>();
        }

        public override bool Initalize()
        {
            AllReconfigViewCollection.Add(this);
            m_ReconfigViewUnstartPage = new ReconfigViewUnstartPage(this);
            m_ReconfigViewUnstartPage.Init();
            m_ReconfigViewUnstartPage.ReconfigStateChanged += page =>
            {
                if (page.ReconfigState == ReconfigState.Configing)
                {
                    AllReconfigViewCollection.ForEach(
                        e => e.m_ReconfigViewUnstartPage.ReconfigState = ReconfigState.Configing);
                }
            };

            m_CurrentPage = m_ReconfigViewUnstartPage;

            m_ReconfigViewConfigingPage = new ReconfigViewConfigingPage();
            m_ReconfigViewConfigingPage.Init();

            InitalizeCar();
            if (GlobalParam.Instance.ProjectType == ProjectType.CRH380BL)
            {
                m_Cellection.ForEach(f => f.Visible = false);
                m_Cellection[3].Visible = true;
                m_Cellection[2].Visible = true;
            }
            else
            {
                DMITitle.MarshallModeChanged += m =>
                {
                    m_Cellection[1].Visible = DMITitle.MarshallMode;
                    m_Cellection[3].Visible = DMITitle.MarshallMode;
                };
            }

            return true;
        }

        private void InitalizeCar()
        {
            var location = new Point(45, 150);
            m_Cellection = new List<CommonInnerControlBase>();

            var tgv = new TrainGroupView(location)
            {
                Visible = true,
                Tag = "动车组 1",
            };
            const int txtHeight = 40;
            m_Cellection.Add(new GDIRectText
            {
                Text = "动车组 1",
                OutLineRectangle =
                    new Rectangle(tgv.OutLineRectangle.X, tgv.OutLineRectangle.Y - txtHeight,
                        TrainGroupView.DefaultWidth, txtHeight),
                TextFormat =
                    new StringFormat {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center},
                Visible = true
            });

            location.Offset(TrainGroupView.DefaultWidth + 2, 0);
            var tgv1 = new TrainGroupView(location)
            {
                Visible = false,
                Tag = "动车组 2",
            };

            m_Cellection.Add(new GDIRectText
            {
                Text = "动车组 2",
                OutLineRectangle =
                    new Rectangle(tgv1.OutLineRectangle.X, tgv1.OutLineRectangle.Y - txtHeight,
                        TrainGroupView.DefaultWidth, txtHeight),
                TextFormat =
                    new StringFormat {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center},
                Visible = false
            });
            m_Cellection.Add(tgv);
            m_Cellection.Add(tgv1);
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                DMITitle.TitleName = "确认列车配置";
                m_ReconfigViewUnstartPage.Reset();
                m_ReconfigViewConfigingPage.Reset();
                m_CurrentPage = m_ReconfigViewUnstartPage;
                ReconfigState = ReconfigState.Unstart;
            }
        }

        public override void Paint(Graphics g)
        {
            DrawContentByState(g);
            m_Cellection.ForEach(e => e.OnDraw(g));
        }



        private void DrawContentByState(Graphics g)
        {
            switch (m_CurrentPage.ReconfigState)
            {
                case ReconfigState.Unstart:
                    m_CurrentPage = m_ReconfigViewUnstartPage;
                    break;
                case ReconfigState.Configing:
                    m_CurrentPage = m_ReconfigViewConfigingPage;
                    break;
                case ReconfigState.ConfigCompleted:
                    append_postCmd(CmdType.ChangePage, 3, 0, 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (m_CurrentPage != null)
            {
                m_CurrentPage.OnDraw(g);
            }
        }
    }
}