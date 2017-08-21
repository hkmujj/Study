using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using Motor.HMI.CRH3C380B.Base.�ײ㹲��;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;
using Motor.HMI.CRH3C380B.Resource.Images;
using Coordinate = Motor.HMI.CRH3C380B.Base.�ײ㹲��.Coordinate;

namespace Motor.HMI.CRH3C380B.Base.�ƶ�״̬
{
    /// <summary>
    /// �ؼ�-�г���
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class ControlTrainPipe : CRH3C380BBase
    {
        private float[] m_FValue;
        private List<int[]> m_FValueAllArrange;

        private bool m_DrawTrainPipe;

        private List<int[]> m_TrainIdList;

        private List<RectangleF> m_RectsList;

        private List<CommonInnerControlBase> m_ConstInfos;

        private readonly NeedChangeLength[] m_ChangingRect = new NeedChangeLength[5];

        private readonly string[] m_PipeValueNameIndexs =
        {
            InFloatKeys.Inf�г���_��,
            InFloatKeys.Inf�г���_16,
            InFloatKeys.Inf�г���_9,
            InFloatKeys.Inf�г���_8,
            InFloatKeys.Inf�г���_1,
        };

        private TrainLength m_TrainLength;

        //2
        public override string GetInfo()
        {
            return "�ؼ�-�г���";
        }

        //6
        public override bool Initalize()
        {
            InitData();
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA != 2)
            {
                return;
            }

            m_DrawTrainPipe = nParaB == 257;
        }

        public override void Paint(Graphics g)
        {
            GetValue();
            Draw(g);
        }

        public void GetValue()
        {
            for (int i = 0; i < m_FValue.Length; i++)
            {
                if (DMITitle.TrainInSide)
                {
                    m_FValue[i] =
                        GetInFloatValue(m_PipeValueNameIndexs[m_FValueAllArrange[DMITitle.MarshallMode ? 1 : 0][i]]);
                }
                else
                {
                    m_FValue[i] = GetInFloatValue(m_PipeValueNameIndexs[i]);
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
            if (!m_DrawTrainPipe)
            {
                m_ChangingRect[0].DrawRectangle(g, m_FValue[0], SolidBrushsItems.BlueBrush1,
                    DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen);
            }
            else
            {


                if (GlobalParam.Instance.ProjectType == ProjectType.CRH380BL)
                {
                    m_TrainLength = TrainLength.L16;
                    for (int index = 0; index < 4; index++)
                    {
                        m_ChangingRect[index + 1].DrawRectangle(g, m_FValue[index + 1], SolidBrushsItems.BlueBrush1,
                            DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen);
                    }
                }
                else
                {
                    m_TrainLength = DMITitle.MarshallMode ? TrainLength.L16 : TrainLength.L8;

                    for (int index = 0; index < (DMITitle.MarshallMode ? 4 : 2); index++)
                    {
                        m_ChangingRect[index + 1].DrawRectangle(g, m_FValue[index + 1], SolidBrushsItems.BlueBrush1,
                            DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen);
                    }
                }

                m_ConstInfos.ForEach(e => e.OnPaint(g));
            }
        }

        private void PaintGroundImage(Graphics g)
        {
            if (m_DrawTrainPipe)
            {
                // ReSharper disable once ConditionalTernaryEqualBranch
                g.DrawImage(DMITitle.NightMode ? BrakeImages.�г��ܳ� : BrakeImages.�г��ܳ�,
                    m_RectsList[109]);
            }
            else
            {
                // ReSharper disable once ConditionalTernaryEqualBranch
                g.DrawImage(DMITitle.NightMode ? BrakeImages.�г��� : BrakeImages.�г���,
                    m_RectsList[12]);
            }

        }

        private void InitData()
        {

            m_FValueAllArrange = new List<int[]>
            {
                new[] {0, 2, 1, 4, 3},
                new[] {1, 4, 3, 2, 1}
            };

            m_FValue = new float[m_PipeValueNameIndexs.Length];

            m_TrainIdList = new List<int[]> {new[] {1, 8, 9, 16}, new[] {8, 1, 16, 9}, new[] {16, 9, 8, 1}};

            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIBrakeStatus, ref m_RectsList))
            {

            }

            m_ChangingRect[0] = new NeedChangeLength(m_RectsList[13], 650f, RectRiseDirection.��, false);
            for (var i = 0; i < 4; i++)
            {
                m_ChangingRect[i + 1] = new NeedChangeLength(m_RectsList[110 + i*2], 650f, RectRiseDirection.��, false);
            }

            m_ConstInfos = new List<CommonInnerControlBase>();

            InitalizeLineOf16();

            InitalizeTrainNumber();

        }

        private void InitalizeLineOf16()
        {
            int x = (int) ((m_ChangingRect[2].TheRectangleF.Right + m_ChangingRect[3].TheRectangleF.X)/2);
            int y = 75;
            m_ConstInfos.Add(
                new Line(new Point(x, y), new Point(x, (int) m_ChangingRect[1].TheRectangleF.Bottom), 2)
                {
                    Color = Color.White,
                    RefreshAction = o =>
                    {
                        var txt = (Line) o;
                        txt.Visible = m_TrainLength == TrainLength.L16;
                    }
                });
        }

        private void InitalizeTrainNumber()
        {
            for (int i = 0; i < 4; i++)
            {
                var i1 = i;
                m_ConstInfos.Add(new GDIRectText
                {
                    OutLineRectangle =
                        new Rectangle((int) m_RectsList[111 + i*2].X, (int) (m_RectsList[111 + i*2].Y + 40),
                            (int) m_RectsList[111 + i*2].Width, (int) (m_RectsList[111 + i*2].Height)),
                    NeedDarwOutline = false,
                    BackColorVisible = false,
                    TextColor = Color.White,
                    TextFormat = FontsItems.TheAlignment(FontRelated.����),
                    DrawFont = FontsItems.FontE12,
                    Text = m_TrainIdList[0][i].ToString(),
                    RefreshAction = o =>
                    {
                        var txt = (GDIRectText) o;
                        if (i1 <= 1)
                        {
                            txt.Visible = true;
                        }
                        else
                        {
                            txt.Visible = m_TrainLength == TrainLength.L16;
                        }
                    }
                });
            }

            var y = 75;
            var width = 90;
            var height = 30;


            m_ConstInfos.Add(new GDIRectText
            {
                OutLineRectangle = new Rectangle((int) m_ChangingRect[1].TheRectangleF.X, y, width, height),
                NeedDarwOutline = false,
                BackColorVisible = false,
                TextColor = Color.White,
                TextFormat = FontsItems.TheAlignment(FontRelated.������),
                DrawFont = FontsItems.FontE12,
                Text = "������ 1",
                RefreshAction = o =>
                {
                    var txt = (GDIRectText) o;
                    txt.Visible = m_TrainLength == TrainLength.L16;
                }
            });


            m_ConstInfos.Add(new GDIRectText
            {
                OutLineRectangle =
                    new Rectangle(
                        (int) ((m_ChangingRect[2].TheRectangleF.Right + m_ChangingRect[3].TheRectangleF.X)/2), y, width,
                        height),
                NeedDarwOutline = false,
                BackColorVisible = false,
                TextColor = Color.White,
                TextFormat = FontsItems.TheAlignment(FontRelated.������),
                DrawFont = FontsItems.FontE12,
                Text = "������ 2",
                RefreshAction = o =>
                {
                    var txt = (GDIRectText) o;
                    txt.Visible = m_TrainLength == TrainLength.L16;
                }
            });
        }
    }
}