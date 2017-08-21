using System;
using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Coordinate = Motor.HMI.CRH3C380B.Base.底层共用.Coordinate;

namespace Motor.HMI.CRH3C380B.Base.制动有效率
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class DMIVmaxTable : CRH3C380BBase, IDisposable
    {
        private List<RectangleF> m_RectsList;

        private readonly string[] m_BtnStr = {"", "", "", "", "", "", "", "", "", "返回"};

        private readonly string[] m_Str1 =
        {
            "制动有效率%",
            "",
            "低于50",
            "50-56",
            "57-62",
            "63-68",
            "69-74",
            "75-80",
            "81-87",
            "88-93",
            "94-100"
        };

        private readonly string[] m_Str2 =
        {
            "最高限速设定值",
            "km/h",
            "0",
            "210",
            "220",
            "230",
            "250",
            "270",
            "290",
            "310",
            "350"
        };

        private List<CommonInnerControlBase> m_ShowingItemControl;


        public override string GetInfo()
        {
            return "DMI最高限速表";
        }


        //6
        public override bool Initalize()
        {
            InitData();
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA != 2 || DMITitle.BtnContentList.Count == 0)
            {
                return;
            }

            for (int i = 0; i < 10; i++)
            {
                DMITitle.BtnContentList[i].BtnStr = m_BtnStr[i];
            }
        }

        public override void Paint(Graphics g)
        {
            ResponseBtnEvent();

            Draw(g);
        }

        private void Draw(Graphics g)
        {
            PaintRectangles(g);
        }

        private void ResponseBtnEvent()
        {
            if (DMIButton.BtnUpList.Count == 0)
            {
                return;
            }

            switch (DMIButton.BtnUpList[0])
            {
                case 0:
                    append_postCmd(CmdType.ChangePage, 230, 0, 0);
                    break;
                case 15:
                    append_postCmd(CmdType.ChangePage, 230, 0, 0); //返回
                    break;
            }
        }

        private void PaintRectangles(Graphics g)
        {
            g.DrawString("制动有效率; 最高限速表", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));

            m_ShowingItemControl.ForEach(e => e.OnPaint(g));
        }


        private void InitData()
        {
            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIVmaxTable, ref m_RectsList))
            {

            }

            InitalizeShowingControls();
        }

        private void InitalizeShowingControls()
        {
            var location = new Point(170, 83);
            var size = new Size(130, 22);
            m_Str2[10] = GlobalParam.Instance.ProjectType == ProjectType.CRH3C ? "330" : "350";

            m_ShowingItemControl = new List<CommonInnerControlBase>
            {
                // 背景
                new GDIRectText
                {
                    BackColorVisible = true,
                    BkColor = Color.White,
                    OutLineRectangle = Rectangle.Ceiling(m_RectsList[2]),
                }
            };

            for (int i = 0; i < 1; i++)
            {
                m_ShowingItemControl.Add(new GDIRectText
                {
                    Text = m_Str1[i],
                    DrawFont = FontsItems.FontC14B,
                    TextBrush = SolidBrushsItems.BlackBrush,
                    TextFormat = FontsItems.TheAlignment(FontRelated.居中),
                    BackColorVisible = false,
                    OutLineRectangle =
                        new Rectangle(location.X + 45, location.Y + i*size.Height, size.Width, size.Height),
                });

                m_ShowingItemControl.Add(new GDIRectText
                {
                    Text = m_Str2[i],
                    DrawFont = FontsItems.FontC14B,
                    TextBrush = SolidBrushsItems.BlackBrush,
                    TextFormat = FontsItems.TheAlignment(FontRelated.靠右),
                    BackColorVisible = false,
                    OutLineRectangle =
                        new Rectangle(location.X + size.Width + 45, location.Y + size.Height*i, size.Width, size.Height),
                });
            }


            for (int i = 1; i < m_Str1.Length; i++)
            {
                m_ShowingItemControl.Add(new GDIRectText
                {
                    Text = m_Str1[i],
                    DrawFont = FontsItems.FontC14B,
                    TextBrush = SolidBrushsItems.BlackBrush,
                    TextFormat = FontsItems.TheAlignment(FontRelated.靠右),
                    BackColorVisible = false,
                    OutLineRectangle = new Rectangle(location.X, location.Y + i*size.Height, size.Width, size.Height),
                });

                m_ShowingItemControl.Add(new GDIRectText
                {
                    Text = m_Str2[i],
                    DrawFont = FontsItems.FontC14B,
                    TextBrush = SolidBrushsItems.BlackBrush,
                    TextFormat = FontsItems.TheAlignment(FontRelated.靠右),
                    BackColorVisible = false,
                    OutLineRectangle =
                        new Rectangle(location.X + size.Width, location.Y + size.Height*i, size.Width, size.Height),
                });
            }
        }

        public void Dispose()
        {
        }
    }
}
