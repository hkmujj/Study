using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using CommonUtil.Controls;
using CommonUtil.Model;
using CommonUtil.Util.Extension;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;
using Coordinate = Motor.HMI.CRH3C380B.Base.底层共用.Coordinate;

namespace Motor.HMI.CRH3C380B.Base.紧急
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class ResetCrosswiseAcceleration : CRH3C380BBase
    {
        private List<CommonInnerControlBase> m_Collection;
        private bool m_IsDown;
        private bool[] m_BvBools;
        private bool[] m_Bool;
        private List<RectangleF> m_RectsList;

        private readonly string[] m_ContentStrs =
        {
            "",
            "横向加速度监控",
            "",
            "没有限速",
            "限速",
            "复位所有横向加速度报警",
            "",
            "",
            "",
            ""
        };

        private GraphicsPath m_TrianglePath;
        private GraphicsPath m_TrianglePath1;

        private void ResetBool()
        {
            for (int i = 0; i < m_Bool.Length; i++)
            {
                m_Bool[i] = false;
            }
        }

        //2
        public override string GetInfo()
        {
            return "左屏--释放速度限制页面";
        }

        //6
        public override bool Initalize()
        {
            InitData();
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                m_IsDown = false;
                DMITitle.BtnContentList[9].BtnStr = "主页面";
            }
        }

        public override void Paint(Graphics g)
        {
            GetValue();
            Draw(g);
        }

        private void Draw(Graphics g)
        {
            PaintRectangles(g);
        }

        private void GetValue()
        {
            m_BvBools[3] = !GetInBoolValue(InBoolKeys.Inb限速);
            m_BvBools[4] = GetInBoolValue(InBoolKeys.Inb限速);
            m_BvBools[5] = GetInBoolValue(InBoolKeys.Inb复位所有横向加速度报警);
            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub复位所有横向加速度报警), 0, 0);
            if (m_IsDown || !m_BvBools[4])
            {
                DMITitle.BtnContentList[5].BtnStr = string.Empty;
            }
            if (m_BvBools[4] && !m_IsDown)
            {
                DMITitle.BtnContentList[5].BtnStr = "复位横向\n加速度报警";
            }
            if (m_IsDown)
            {
                ResetBool();
                m_Bool[5] = true;
            }
            else
            {
                ResetBool();
                m_Bool[3] = m_BvBools[3];
                m_Bool[4] = m_BvBools[4];
            }
            if (DMIButton.BtnUpList.Count == 0)
            {
                return;
            }

            switch (DMIButton.BtnUpList[0])
            {
                case 0: //C键
                    if (m_IsDown)
                    {
                        m_IsDown = false;
                        break;
                    }

                    append_postCmd(CmdType.ChangePage, 150, 0, 0);
                    break;
                case 5:
                    m_IsDown = false;
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub复位所有横向加速度报警), 1, 0);
                    break;
                case 11:
                    if (DMITitle.BtnContentList[5].BtnStr != string.Empty)
                    {
                        m_IsDown = true;
                    }
                    break;
                case 15: //主页面
                    m_IsDown = false;
                    append_postCmd(CmdType.ChangePage, 11, 0, 0);
                    break;
            }
        }

        private void PaintRectangles(Graphics g)
        {
            g.DrawString("紧急;复位横向加速度报警", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));
            for (int i = 0; i < 10; i++)
            {

                if (m_Bool[i])
                {
                    g.FillRectangle(Brushes.White, m_RectsList[12 + i]);
                }

                g.DrawString(m_ContentStrs[i], FontsItems.FontC24B,
                    m_Bool[i] ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                    m_RectsList[12 + i], FontsItems.TheAlignment(FontRelated.靠左));
            }

            if (m_IsDown)
            {
                m_Collection.ForEach(e => e.OnDraw(g));
                g.FillPath(Brushes.Black, m_TrianglePath);
                g.FillPath(Brushes.White, m_TrianglePath1);
            }
        }

        private void InitData()
        {
            m_Bool = new bool[10];
            m_Collection = new List<CommonInnerControlBase>
            {
                new GDIRectText
                {
                    OutLineRectangle = new Rectangle(680, 440, 110, 60),
                    Text = "确认：\n复位横向加速度报警",
                    TextColor = Color.Black,
                    BkColor = Color.White,
                    OutLinePen = PenItems.BlackPen,
                    NeedDarwOutline = true
                },
                new GDIRectText
                {
                    OutLineRectangle = new Rectangle(680, 40, 110, 60),
                    Text = "取消",
                    TextColor = Color.White,
                    BkColor = Color.Black,
                    NeedDarwOutline = true
                }
            };
            var x1 = m_Collection[0].OutLineRectangle.Right - 15;
            var x2 = m_Collection[0].OutLineRectangle.Right;
            var y2 = m_Collection[0].OutLineRectangle.GetMidPoint(Direction.Right).Y;
            var y1 = y2 - 10;
            var y3 = y2 + 10;
            TrianglePath(x1, y1, x2, y2, y3, true);
            x1 = m_Collection[1].OutLineRectangle.Right - 15;
            x2 = m_Collection[1].OutLineRectangle.Right;
            y2 = m_Collection[1].OutLineRectangle.GetMidPoint(Direction.Right).Y;
            y1 = y2 - 10;
            y3 = y2 + 10;
            TrianglePath(x1, y1, x2, y2, y3, false);
            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIEmergency, ref m_RectsList))
            {

            }
            m_BvBools = new bool[m_RectsList.Count];
        }

        /// <summary>
        /// 三角
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="y3"></param>
        /// <param name="bl"></param>
        private void TrianglePath(int x1, int y1, int x2, int y2, int y3, bool bl)
        {
            if (bl)
            {
                m_TrianglePath = new GraphicsPath();
                m_TrianglePath.AddLine(new Point(x1, y1), new Point(x2, y2));
                m_TrianglePath.AddLine(new Point(x2, y2), new Point(x1, y3));
                m_TrianglePath.AddLine(new Point(x1, y3), new Point(x1, y1));
            }
            else
            {
                m_TrianglePath1 = new GraphicsPath();
                m_TrianglePath1.AddLine(new Point(x1, y1), new Point(x2, y2));
                m_TrianglePath1.AddLine(new Point(x2, y2), new Point(x1, y3));
                m_TrianglePath1.AddLine(new Point(x1, y3), new Point(x1, y1));
            }
        }
    }
}