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
    public class LiberateSpeedLimit : CRH3C380BBase
    {
        private GraphicsPath m_TrianglePath;
        private GraphicsPath m_TrianglePath1;
        private List<CommonInnerControlBase> m_ConstTextCollection;

        private readonly bool[] m_BtnIsDown = {false, false, false, false, false, false};

        private List<RectangleF> m_RectsList;

        private List<RectangleF> m_PrivateRectsList;

        private readonly string[] m_BtnStr =
        {
            "限速\n100km/h",
            "",
            "取消\n限速",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
        };

        private readonly string[] m_ContentStrs =
        {
            "",
            "速度限制:",
            "",
            "100km/h",
            "限速已取消",
            "",
            "",
            "",
            "",
            ""
        };

        private bool m_BlContentStrs;

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


            for (int i = 0; i < 10; i++)
            {
                DMITitle.BtnContentList[i].BtnStr = m_BtnStr[i];
            }

            m_BlContentStrs = GetInBoolValue(InBoolKeys.Inb速度限制100kmh);
            if (m_BlContentStrs)
            {
                DMITitle.BtnContentList[0].BtnStr = string.Empty;
                DMITitle.BtnContentList[2].BtnStr = m_BtnStr[2];
            }
            else
            {
                DMITitle.BtnContentList[0].BtnStr = m_BtnStr[0];
                DMITitle.BtnContentList[2].BtnStr = string.Empty;
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
            if (m_BtnIsDown[3])
            {
                m_ConstTextCollection.ForEach(e => e.OnDraw(g));
                g.FillPath(Brushes.Black, m_TrianglePath);
                g.FillPath(Brushes.White, m_TrianglePath1);
            }
        }

        private void GetValue()
        {
            DMITitle.BtnContentList[9].BtnStr = "主页面";
            ResponseBtnEvent();
        }

        private void ResponseBtnEvent()
        {
            if (DMIButton.BtnDownList.Count != 0)
            {
                switch (DMIButton.BtnDownList[0])
                {
                    case 5: //确认 释放速度限制
                        if (m_BtnIsDown[3])
                        {
                            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub取消速度限制100kmh), 1, 0);
                        }
                        break;
                }
            }

            if (DMIButton.BtnUpList.Count != 0)
            {
                switch (DMIButton.BtnUpList[0])
                {
                    case 0: //取消
                        if (m_BtnIsDown[3])
                        {
                            m_BtnIsDown[3] = false;
                        }
                        else
                        {
                            append_postCmd(CmdType.ChangePage, 150, 0, 0);
                        }

                        break;
                    case 5: //确认 释放速度限制
                        if (m_BtnIsDown[3])
                        {
                            append_postCmd(CmdType.SetInBoolValue, GetInBoolIndex(InBoolKeys.Inb速度限制100kmh), 0, 0);
                            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub取消速度限制100kmh), 0, 0);
                            m_BtnIsDown[3] = false;
                        }
                        break;
                    case 6: // 限速\n100km/h
                        if (!m_BlContentStrs)
                        {
                            append_postCmd(CmdType.SetInBoolValue, GetInBoolIndex(InBoolKeys.Inb速度限制100kmh), 1, 0);
                        }
                        break;
                    case 8: //释放 速度限制
                        if (m_BlContentStrs)
                        {
                            m_BtnIsDown[3] = true;
                        }
                        break;
                    case 15: //主页面
                        append_postCmd(CmdType.ChangePage, 11, 0, 0);
                        break;
                }
            }
        }

        private void PaintRectangles(Graphics g)
        {
            g.DrawString("紧急;释放速度限制", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));

            g.DrawString(m_ContentStrs[1], FontsItems.FontC24B,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_PrivateRectsList[13], FontsItems.TheAlignment(FontRelated.靠左));
            if (m_BlContentStrs)
            {
                g.FillRectangle(Brushes.White, m_PrivateRectsList[15]);
                g.DrawString(m_ContentStrs[3], FontsItems.FontC24B,
                    SolidBrushsItems.BlackBrush,
                    m_PrivateRectsList[15], FontsItems.TheAlignment(FontRelated.靠左));
                g.DrawString(m_ContentStrs[4], FontsItems.FontC24B,
                    SolidBrushsItems.WhiteBrush,
                    m_PrivateRectsList[16], FontsItems.TheAlignment(FontRelated.靠左));
            }
            else
            {
                g.DrawString(m_ContentStrs[3], FontsItems.FontC24B,
                    SolidBrushsItems.WhiteBrush,
                    m_PrivateRectsList[15], FontsItems.TheAlignment(FontRelated.靠左));
                g.FillRectangle(Brushes.White, m_PrivateRectsList[16]);
                g.DrawString(m_ContentStrs[4], FontsItems.FontC24B,
                    SolidBrushsItems.BlackBrush,
                    m_PrivateRectsList[16], FontsItems.TheAlignment(FontRelated.靠左));
            }
        }

        private void InitData()
        {
            //确认 取消 按钮指示
            m_ConstTextCollection = new List<CommonInnerControlBase>
            {
                new GDIRectText
                {
                    OutLineRectangle = new Rectangle(680, 440, 110, 60),
                    Text = "确认：\n司机取消限速",
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
            var x1 = m_ConstTextCollection[0].OutLineRectangle.Right - 15;
            var x2 = m_ConstTextCollection[0].OutLineRectangle.Right;
            var y2 = m_ConstTextCollection[0].OutLineRectangle.GetMidPoint(Direction.Right).Y;
            var y1 = y2 - 10;
            var y3 = y2 + 10;
            TrianglePath(x1, y1, x2, y2, y3, true);
            x1 = m_ConstTextCollection[1].OutLineRectangle.Right - 15;
            x2 = m_ConstTextCollection[1].OutLineRectangle.Right;
            y2 = m_ConstTextCollection[1].OutLineRectangle.GetMidPoint(Direction.Right).Y;
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
            RectangleF rectangleF = new RectangleF();
            m_PrivateRectsList = new List<RectangleF>();
            foreach (RectangleF t in m_RectsList)
            {
                rectangleF.X = t.X + 40;
                rectangleF.Y = t.Y;
                rectangleF.Width = t.Width;
                rectangleF.Height = t.Height;
                m_PrivateRectsList.Add(rectangleF);
            }
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