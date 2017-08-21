using System;
using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;
using Motor.HMI.CRH3C380B.Resource.Images;
using Coordinate = Motor.HMI.CRH3C380B.Base.底层共用.Coordinate;

namespace Motor.HMI.CRH3C380B.Base.系统
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIBatteryVoltage110V : CRH3C380BBase
    {
        private List<CommonInnerControlBase> m_ControlCollection;

        private List<RectangleF> m_Rectangle;

        private List<RectangleF> m_RectsList;

        private List<int[]> m_FValueAllArrange;

        private float[] m_FValue;

        private readonly string[] m_Str = {"动车组1", "动车组2"};
        private readonly string[] m_Str1 = {"电池1/2", "电池3/4"};
        private readonly string[] m_Str2 = {"04", "05", "12", "13"};

        private readonly string[] m_VoltageNameIndexs =
        {
            InFloatKeys.Inf蓄电池电压_04,
            InFloatKeys.Inf蓄电池电压_05,
            InFloatKeys.Inf蓄电池电压_12,
            InFloatKeys.Inf蓄电池电压_13,
        };


        private readonly string[] m_BtnStr =
        {
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "主页面"
        };

        private readonly NeedChangeLength[] m_ChangingRect = new NeedChangeLength[4];

        //2
        public override string GetInfo()
        {
            return "DMI蓄电池电压110V";
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
            GetValue();

            Draw(g);
        }

        private void Draw(Graphics g)
        {
            PaintGroundImage(g);

            PaintRectangles(g);
        }

        private void GetValue()
        {
            ResponseBtnEvent();

            for (var i = 0; i < m_FValue.Length; i++)
            {
                if (DMITitle.TrainInSide)
                {
                    m_FValue[i] =
                        GetInFloatValue(m_VoltageNameIndexs[m_FValueAllArrange[DMITitle.MarshallMode ? 1 : 0][i]]);
                }
                else
                {
                    m_FValue[i] = GetInFloatValue(m_VoltageNameIndexs[i]);
                }
            }
        }

        private void ResponseBtnEvent()
        {
            if (DMIButton.BtnUpList.Count != 0)
            {
                switch (DMIButton.BtnUpList[0])
                {
                    case 0:
                        append_postCmd(CmdType.ChangePage, 120, 0, 0);
                        break;
                    case 15:
                        append_postCmd(CmdType.ChangePage, 11, 0, 0);
                        break;
                }
            }
        }

        private void PaintRectangles(Graphics g)
        {
            g.DrawString("系统; 蓄电池电压", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));
            if (GlobalParam.Instance.ProjectType == ProjectType.CRH380BL)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (m_FValue[i] > 60)
                    {
                        m_ChangingRect[i].DrawRectangle(g, m_FValue[i] - 60, SolidBrushsItems.BlueBrush1,
                            PenItems.WhiltPen);
                    }
                    g.DrawString(m_Str2[i], FontsItems.FontC12B,
                        DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                        m_Rectangle[2 + i], FontsItems.TheAlignment(FontRelated.居中));
                }

            }
            else
            {
                g.DrawString(DMITitle.TrainInSide && DMITitle.MarshallMode ? m_Str[1] : m_Str[0], FontsItems.FontC11,
                    DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush, m_Rectangle[0],
                    FontsItems.TheAlignment(FontRelated.靠左));
                g.DrawString(DMITitle.MarshallMode ? DMITitle.TrainInSide ? m_Str[0] : m_Str[1] : "", FontsItems.FontC11,
                    DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush, m_Rectangle[1],
                    FontsItems.TheAlignment(FontRelated.靠左));
                g.DrawString(DMITitle.TrainInSide ? m_Str1[1] : m_Str1[0], FontsItems.FontC11,
                    DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush, m_Rectangle[2],
                    FontsItems.TheAlignment(FontRelated.靠左));
                g.DrawString(DMITitle.TrainInSide ? m_Str1[0] : m_Str1[1], FontsItems.FontC11,
                    DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush, m_Rectangle[3],
                    FontsItems.TheAlignment(FontRelated.靠左));
                g.DrawString(DMITitle.MarshallMode ? DMITitle.TrainInSide ? m_Str1[1] : m_Str1[0] : "",
                    FontsItems.FontC11, DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                    m_Rectangle[4], FontsItems.TheAlignment(FontRelated.靠左));
                g.DrawString(DMITitle.MarshallMode ? DMITitle.TrainInSide ? m_Str1[0] : m_Str1[1] : "",
                    FontsItems.FontC11, DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                    m_Rectangle[5], FontsItems.TheAlignment(FontRelated.靠左));
                //////////////////////////////////////////////////////////////////////////////////////////////////
                m_ControlCollection.ForEach(f => f.OnDraw(g));
                for (int i = 0; i < (DMITitle.MarshallMode ? 4 : 2); i++)
                {
                    if (m_FValue[i] > 60)
                    {
                        m_ChangingRect[i].DrawRectangle(g, m_FValue[i] - 60, SolidBrushsItems.BlueBrush1,
                            PenItems.WhiltPen);
                    }
                }
            }


        }

        private void PaintGroundImage(Graphics g)
        {
            switch (GlobalParam.Instance.ProjectType)
            {
                case ProjectType.CRH3C:
                    g.DrawImage(SystemImages.Battery_0_3C, m_RectsList[12]);
                    break;
                case ProjectType.CRH380B:
                    g.DrawImage(SystemImages.Battery_0_380, m_RectsList[12]);
                    break;
                case ProjectType.CRH380BL:
                    g.DrawImage(SystemImages.Battery_0_380, m_RectsList[12]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void InitData()
        {

            m_FValueAllArrange = new List<int[]>
            {
                new[] {1, 0, 3, 2},
                new[] {3, 2, 1, 0}
            };

            m_FValue = new float[m_VoltageNameIndexs.Length];

            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIBatteryVoltage110V, ref m_RectsList))
            {

            }
            m_ControlCollection = new List<CommonInnerControlBase>
            {
                new Line(new Point(250, 75), new Point(250, 475))
                {
                    Color = DMITitle.NightMode ? Color.Black : Color.White,
                    Visible = false,
                }
            };
            DMITitle.MarshallModeChanged += m =>
            {
                m_ControlCollection[0].Visible = DMITitle.MarshallMode;
            };
            m_Rectangle = new List<RectangleF>
            {
                new RectangleF(25, 80, 125, 30),
                new RectangleF(250, 80, 125, 30),
                new RectangleF(25, 130, 75, 30),
                new RectangleF(100, 130, 75, 30),
                new RectangleF(250, 130, 75, 30),
                new RectangleF(325, 130, 75, 30),
            };
            for (int i = 0; i < 4; i++)
            {
                m_ChangingRect[i] = new NeedChangeLength(m_RectsList[i + 13], 80f, RectRiseDirection.上, false);
            }
        }
    }
}
