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

namespace Motor.HMI.CRH3C380B.Base.制动状态
{
    /// <summary>
    /// 控件-总风管
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class ControlMainReservoirPipe : CRH3C380BBase
    {
        private List<CommonInnerControlBase> m_ConstInfos;

        private List<RectangleF> m_RectsList;
        private bool m_IsCrh380BL;
        private float[] m_FValue;

        private readonly string[] m_ReservoirPipeNameIndexs =
        {
            InFloatKeys.Inf总风管_总,
            InFloatKeys.Inf总风管_1,
            InFloatKeys.Inf总风管_8,
            InFloatKeys.Inf总风管_9,
            InFloatKeys.Inf总风管_16,
        };

        private List<int[]> m_FValueAllArrange;

        private bool m_DrawLargeMainReservoirPipe;

        private readonly NeedChangeLength[] m_ChangingRect = new NeedChangeLength[5];

        private List<int[]> m_TrainIdList;

        private readonly string[] m_BtnStr = {"", "", "", "", "", "", "", "", "", "制动\n状态"};

        public override string GetInfo()
        {
            return "控件-总风管";
        }

        //6
        public override bool Initalize()
        {
            InitData();

            InitalizeInfos();

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA != 2)
            {
                return;
            }

            m_DrawLargeMainReservoirPipe = false;
            if (nParaB != 280 && nParaB != 254)
            {
                return;
            }

            m_DrawLargeMainReservoirPipe = true;
            if (nParaB == 280)
            {
                for (int i = 0; i < 10; i++)
                {
                    DMITitle.BtnContentList[i].BtnStr = m_BtnStr[i];
                }
            }
        }

        public override void Paint(Graphics g)
        {
            GetValue();

            Draw(g);
        }

        public void GetValue()
        {
            if (DMIButton.BtnUpList.Count != 0 && (DMIButton.BtnUpList[0] == 15 || DMIButton.BtnUpList[0] == 0))
            {
                append_postCmd(CmdType.ChangePage, 21, 0, 0);
            }

            for (int i = 0; i < m_FValue.Length; i++)
            {
                if (DMITitle.TrainInSide)
                {
                    m_FValue[i] =
                        GetInFloatValue(
                            m_ReservoirPipeNameIndexs[
                                m_FValueAllArrange[(DMITitle.MarshallMode || m_IsCrh380BL) ? 1 : 0][i]]);
                }
                else
                {
                    m_FValue[i] = GetInFloatValue(m_ReservoirPipeNameIndexs[i]);
                }
            }
        }

        private void Draw(Graphics g)
        {
            PaintGroundImage(g);

            PaintRectangles(g);
        }

        private void PaintRectangles(Graphics g)
        {
            if (m_DrawLargeMainReservoirPipe)
            {
                g.DrawString("总风管", FontsItems.FontC11,
                    DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                    m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));

                m_ConstInfos.ForEach(e => e.OnPaint(g));

                for (int i = 0; i < ( ( DMITitle.MarshallMode || m_IsCrh380BL ) ? 4 : 2 ); i++)
                {
                    m_ChangingRect[i + 1].DrawRectangle(g, m_FValue[i + 1], SolidBrushsItems.BlueBrush1,
                        DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen);
                    g.DrawString(( DMITitle.TrainInSide ? ( ( DMITitle.MarshallMode || m_IsCrh380BL ) ? m_TrainIdList[2][i] : m_TrainIdList[1][i] ) : m_TrainIdList[0][i] ).ToString(m_IsCrh380BL ? "00" : ""),
                        FontsItems.FontE12, DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                        m_RectsList[102 + i * 2], FontsItems.TheAlignment(FontRelated.居中));
                }
            }
            else
            {
                m_ChangingRect[0].DrawRectangle(g, m_FValue[0], SolidBrushsItems.BlueBrush1,
                    DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen);
            }
        }

        private void PaintGroundImage(Graphics g)
        {
            if (m_DrawLargeMainReservoirPipe)
            {
                // ReSharper disable once ConditionalTernaryEqualBranch
                g.DrawImage(DMITitle.NightMode ? BrakeImages.总风管长 : BrakeImages.总风管长, m_RectsList[100]);
            }
            else
            {
                // ReSharper disable once ConditionalTernaryEqualBranch
                g.DrawImage(DMITitle.NightMode ? BrakeImages.总风管短 : BrakeImages.总风管短,
                    m_RectsList[14]);
            }
        }

        private void InitData()
        {
            m_IsCrh380BL = GlobalParam.Instance.ProjectType == ProjectType.CRH380BL;

            m_FValueAllArrange = new List<int[]>
            {
                new[] {0, 2, 1, 4, 3},
                new[] {1, 4, 3, 2, 1}
            };

            m_FValue = new float[m_ReservoirPipeNameIndexs.Length];

            m_TrainIdList = new List<int[]> {new[] {1, 8, 9, 16}, new[] {8, 1, 16, 9}, new[] {16, 9, 8, 1}};

            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIBrakeStatus, ref m_RectsList))
            {

            }

            m_ChangingRect[0] = new NeedChangeLength(m_RectsList[15], 1100f, RectRiseDirection.上, false);
            for (var index = 0; index < 4; index++)
            {
                m_ChangingRect[index + 1] = new NeedChangeLength(m_RectsList[101 + index*2], 1000f, RectRiseDirection.上,
                    false);
            }
        }

        private void InitalizeInfos()
        {
            m_ConstInfos = new List<CommonInnerControlBase>();

            if (m_IsCrh380BL)
            {
                InitalizeInfos380BL();
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    var i1 = i;
                    m_ConstInfos.Add(new GDIRectText
                    {
                        NeedDarwOutline = false,
                        BackColorVisible = false,
                        TextColor = Color.White,
                        TextFormat = FontsItems.TheAlignment(FontRelated.居中),
                        OutLineRectangle = Rectangle.Ceiling(m_RectsList[102 + i*2]),
                        DrawFont = FontsItems.FontE12,
                        RefreshAction = o =>
                        {
                            var txt = (GDIRectText) o;
                            txt.TextBrush = DMITitle.NightMode
                                ? SolidBrushsItems.BlackBrush
                                : SolidBrushsItems.WhiteBrush;
                            txt.Text =
                                (DMITitle.TrainInSide
                                    ? ((DMITitle.MarshallMode)
                                        ? m_TrainIdList[2][i1]
                                        : m_TrainIdList[1][i1])
                                    : m_TrainIdList[0][i1]).ToString("");

                            txt.Visible = i1 <= 1 || DMITitle.MarshallMode;
                        }
                    });
                }

                var size = new Size(80, 25);
                var y = Rectangle.Ceiling(m_RectsList[102 + 0*2]).Y - size.Height;

                for (int i = 0; i < 2; i++)
                {
                    m_ConstInfos.Add(new GDIRectText
                    {
                        NeedDarwOutline = false,
                        BackColorVisible = false,
                        TextColor = Color.White,
                        TextFormat = FontsItems.TheAlignment(FontRelated.靠左上),
                        OutLineRectangle = new Rectangle((int) m_RectsList[102 + i*2*2].X, y, size.Width, size.Height),
                        Text = string.Format("动车组 {0}", i + 1),
                        RefreshAction = o =>
                        {
                            var txt = (GDIRectText) o;
                            txt.Visible = DMITitle.MarshallMode;
                        }
                    });
                }

                var p3 = Rectangle.Ceiling(m_RectsList[102 + 1*2*2]);
                var x = p3.X;
                y = p3.Y - size.Height;
                p3 = Rectangle.Ceiling(m_ChangingRect[3].TheRectangleF);
                m_ConstInfos.Add(new Line(new Point(x, y), new Point(x, p3.Bottom))
                {
                    Color = Color.White,
                    RefreshAction = o =>
                    {
                        var txt = (Line) o;
                        txt.Visible = DMITitle.MarshallMode;
                    }
                });
            }
        }

        private void InitalizeInfos380BL()
        {
            for (int i = 0; i < 4; i++)
            {
                var i1 = i;
                m_ConstInfos.Add(new GDIRectText
                {
                    NeedDarwOutline = false,
                    BackColorVisible = false,
                    TextColor = Color.White,
                    TextFormat = FontsItems.TheAlignment(FontRelated.居中),
                    OutLineRectangle = Rectangle.Ceiling(m_RectsList[102 + i*2]),
                    DrawFont = FontsItems.FontE12,
                    RefreshAction = o =>
                    {
                        var txt = (GDIRectText) o;
                        txt.TextBrush = DMITitle.NightMode
                            ? SolidBrushsItems.BlackBrush
                            : SolidBrushsItems.WhiteBrush;
                        txt.Text =
                            (DMITitle.TrainInSide ? m_TrainIdList[2][i1] : m_TrainIdList[0][i1]).ToString("00");
                    }
                });
            }
        }
    }
}