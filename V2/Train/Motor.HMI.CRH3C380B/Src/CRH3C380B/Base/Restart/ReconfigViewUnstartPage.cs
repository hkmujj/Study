using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using CommonUtil.Controls;
using CommonUtil.Model;
using CommonUtil.Util.Extension;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.Restart
{
    public class ReconfigViewUnstartPage : ReconfigViewPage
    {

        private List<CommonInnerControlBase> m_ConstTextCollection;

        private GDIRectText m_ContentTitle;

        private GraphicsPath m_TrianglePath;

        private readonly CRH3C380BBase m_SourceObj;

        public event Action<ReconfigViewUnstartPage> ReconfigStateChanged;

        public ReconfigViewUnstartPage(CRH3C380BBase sourceObj)
        {
            m_SourceObj = sourceObj;
        }

        public string ContentTitle
        {
            set { m_ContentTitle.Text = value; }
            get { return m_ContentTitle.Text; }
        }

        public override void Init()
        {
            InitalizeConstInfo();
        }

        private void InitalizeConstInfo()
        {
            var txt = new GDIRectText { OutLineRectangle = new Rectangle(650, 440, 150, 60), Text = "请确认新的列车配置", TextColor = Color.Black, BkColor = Color.White };
            m_ContentTitle = new GDIRectText { OutLineRectangle = new Rectangle(30, 80, 400, 20), Text = "更新列车配置" };
            m_ConstTextCollection = new List<CommonInnerControlBase>
            {
                                        txt,
                                        new GDIRectText
                                        {
                                            OutLineRectangle = new Rectangle(30, 440, 600, 60),
                                            Text = "如果列车配置不正确,必须关闭列车,重新启动(断开和接通蓄电池)",
                                            TextColor = Color.Black,
                                            BkColor = Color.White
                                        },
                                        m_ContentTitle,
                                    };
            var x1 = txt.OutLineRectangle.Right - 15;
            var x2 = txt.OutLineRectangle.Right;
            var y2 = txt.OutLineRectangle.GetMidPoint(Direction.Right).Y;
            var y1 = y2 - 10;
            var y3 = y2 + 10;
            m_TrianglePath = new GraphicsPath();
            m_TrianglePath.AddLine(new Point(x1, y1), new Point(x2, y2));
            m_TrianglePath.AddLine(new Point(x2, y2), new Point(x1, y3));
            m_TrianglePath.AddLine(new Point(x1, y3), new Point(x1, y1));
        }

        public override void OnDraw(Graphics g)
        {
            ResonseUserEvent();
            m_ConstTextCollection.ForEach(e => e.OnDraw(g));
            g.FillPath(Brushes.Black, m_TrianglePath);
        }

        public override void Reset()
        {
            ReconfigState = ReconfigState.Unstart;
            OnReconfigStateChanged(this);
        }

        private void ResonseUserEvent()
        {
            if (m_SourceObj.DMIButton.BtnUpList.Count == 0)
            {
                return;
            }
            switch (m_SourceObj.DMIButton.BtnUpList[0])
            {
                    // E 键
                case 5 :
                    if (ReconfigState == ReconfigState.Unstart)
                    {
                        ReconfigState = ReconfigState.Configing;
                        OnReconfigStateChanged(this);
                    }
                    break;
            }
        }


        private void OnReconfigStateChanged(ReconfigViewUnstartPage obj)
        {
            var handler = ReconfigStateChanged;
            if (handler != null)
            {
                handler(obj);
            }
        }

    }
}