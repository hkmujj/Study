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
    public class DMIEmergency : CRH3C380BBase
    {
        private GraphicsPath m_TrianglePath;
        private GraphicsPath m_TrianglePath1;
        //释放牵引限制
        private List<CommonInnerControlBase> m_ConstTextCollection;
        // 空调紧急关
        private List<CommonInnerControlBase> m_CommonInnerControl;

        private readonly bool[] m_BtnIsDown = {false, false, false, false, false, false};

        private bool[] m_BValue;

        private List<RectangleF> m_RectsList;

        private readonly string[] m_BtnStr =
        {
            "紧急模式",
            "",
            "释放\n速度限制",
            "复位横向加速度报警",
            "",
            "",
            "",
            "关闭空调\n紧急关",
            "",
            "主页面"
        };

        private readonly string[] m_ContentStrs =
        {
            "1 紧急模式指南",
            "",
            "3 释放速度-限制(ATP 关闭)",
            "4 复位横向加速度报警",
            "",
            "6 取消牵引限制(门开)",
            "",
            "8 关闭空调(紧急关闭)",
            "",
            "0 主页面"
        };

        //2
        public override string GetInfo()
        {
            return "DMI紧急";
        }

        //6
        public override bool Initalize()
        {
            InitData();
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            GetBoolValue();

            for (int i = 0; i < 10; i++)
            {
                if (i == 5 && GetInBoolValue(InBoolKeys.Inb消息_故障240) && !GetInBoolValue(InBoolKeys.Inb取消牵引限制门开指令))
                {
                    DMITitle.BtnContentList[i].BtnStr = "取消牵引限制";
                    continue;
                }

                if (i == 2 && !GetInBoolValue(InBoolKeys.Inb紧急释放速度限制ATP关闭是否可用))
                {
                    DMITitle.BtnContentList[i].BtnStr = "";
                    continue;
                }

                DMITitle.BtnContentList[i].BtnStr = m_BtnStr[i];
            }

            if (GetInBoolValue(InBoolKeys.Inb取消牵引限制门开指令))
            {
                append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub维护0为关闭1为开启), 0, 0);
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
            if (m_BtnIsDown[5])
            {
                m_CommonInnerControl.ForEach(e => e.OnDraw(g));
                g.FillPath(Brushes.Black, m_TrianglePath);
                g.FillPath(Brushes.White, m_TrianglePath1);
            }
        }

        private void GetValue()
        {

            if (DMIButton.BtnUpList.Count == 0)
            {
                return;
            }

            switch (DMIButton.BtnUpList[0])
            {
                case 0: //取消
                    if (m_BtnIsDown[3] || m_BtnIsDown[5])
                    {
                        m_BtnIsDown[3] = false;
                        m_BtnIsDown[5] = false;
                    }
                    else
                    {
                        append_postCmd(CmdType.ChangePage, 11, 0, 0);
                    }

                    break;
                case 5: //确认
                    if (m_BtnIsDown[3])
                    {
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub维护0为关闭1为开启), 1, 0);
                        m_BtnIsDown[3] = false;
                    }
                    if (m_BtnIsDown[5])
                    {
                        append_postCmd(CmdType.SetBoolValue, 5013, 0, 0);
                        m_BtnIsDown[5] = false;
                    }
                    break;
                case 6:
                    // 紧急模式指南
                    append_postCmd(CmdType.ChangePage, 151, 0, 0);
                    break;
                case 8: //释放 速度限制
                    if (GetInBoolValue(InBoolKeys.Inb紧急释放速度限制ATP关闭是否可用))
                    {
                        append_postCmd(CmdType.ChangePage, 152, 0, 0);
                    }
                    break;
                case 9: //复位横向加速度报警
                    append_postCmd(CmdType.ChangePage, 154, 0, 0);
                    break;
                case 11: // 制动限制
                    if (GetInBoolValue(InBoolKeys.Inb消息_故障240))
                    {
                        m_BtnIsDown[3] = true;
                    }
                    else
                    {
                        m_BtnIsDown[3] = false;
                    }
                    break;
                case 13: //关闭空调 紧急关
                    m_BtnIsDown[5] = true;
                    break;
                case 15: //主页面
                    append_postCmd(CmdType.ChangePage, 11, 0, 0);
                    break;
            }
        }

        /// <summary>
        /// 获取Bool量
        /// </summary>
        private void GetBoolValue()
        {
            m_BValue = new bool[UIObj.InBoolList.Count];
            for (int i = 0; i < UIObj.InBoolList.Count; i++)
            {
                m_BValue[i] = BoolList[UIObj.InBoolList[i]];
            }
        }

        private void PaintRectangles(Graphics g)
        {
            g.DrawString("紧急", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));

            for (int i = 0; i < 10; i++)
            {
                var brush = DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush;
                var region = m_RectsList[12 + i];
                if (i == 5 && (!GetInBoolValue(InBoolKeys.Inb消息_故障240) || GetInBoolValue(InBoolKeys.Inb取消牵引限制门开指令)))
                {
                    brush = DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.Grey4;
                }
                if (i == 2 && !GetInBoolValue(InBoolKeys.Inb紧急释放速度限制ATP关闭是否可用))
                {
                    brush = DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.Grey4;
                }
                g.DrawString(m_ContentStrs[i], FontsItems.FontC24B, brush, region,
                    FontsItems.TheAlignment(FontRelated.靠左));
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
                    Text = "确认：\n释放牵引限制",
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
            m_CommonInnerControl = new List<CommonInnerControlBase>
            {
                new GDIRectText
                {
                    OutLineRectangle = new Rectangle(680, 440, 110, 60),
                    Text = "确认：\n所有单车空调紧急关闭",
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
            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIEmergency, ref m_RectsList))
            {

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