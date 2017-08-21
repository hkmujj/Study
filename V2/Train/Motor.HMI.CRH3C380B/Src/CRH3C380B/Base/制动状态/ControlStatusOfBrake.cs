using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;
using Motor.HMI.CRH3C380B.Resource.Images;
using Coordinate = Motor.HMI.CRH3C380B.Base.底层共用.Coordinate;

namespace Motor.HMI.CRH3C380B.Base.制动状态
{
    /// <summary>
    /// 控件-列车制动状态
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class ControlStatusOfBrake : CRH3C380BBase
    {
        private List<RectangleF> m_RectsList;

        /// <summary>
        /// 完整状态
        /// </summary>
        private bool m_DrawEd;

        private List<string> m_BValueList;

        private List<int[]> m_BValueAllArrange;

        private bool[] m_BValue;

        private readonly int[] m_RectBrakeE = {0, 1, 4, 5, 10, 11, 14, 15, 16, 17, 20, 21, 26, 27, 30, 31};

        /// <summary>
        /// 制动 E 的状态未知逻辑索引
        /// key : 车厢号
        /// </summary>
        private Dictionary<int, int> m_BrakeEUnkownStateIndexDictionary;

        /// <summary>
        /// 制动 D 的状态未知逻辑索引
        /// key : 车厢号
        /// </summary>
        private Dictionary<int, int> m_BrakeDUnkownStateIndexDictionary;

        //2
        public override string GetInfo()
        {
            return "控件-列车制动状态";
        }

        //6
        public override bool Initalize()
        {
            InitData();

            m_BrakeEUnkownStateIndexDictionary = (new[] {1, 3, 6, 8, 9, 11, 14, 16}).ToDictionary(kvp => kvp,
                kvp => GetInBoolIndex(string.Format("制动施加{0}E状态未知", kvp)));

            var tmp = new int[16];
            for (int i = 0; i < 16; i++)
            {
                tmp[i] = i + 1;
            }

            m_BrakeDUnkownStateIndexDictionary = tmp.ToDictionary(kvp => kvp,
                kvp => GetInBoolIndex(string.Format("制动施加{0}D状态未知", kvp)));

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA != 2)
            {
                return;
            }

            m_DrawEd = nParaB == 21;
        }

        public override void Paint(Graphics g)
        {
            GetValue();

            Draw(g);
        }

        private void Draw(Graphics g)
        {
            PaintGroundImage(g);

            DrawStrings(g);
        }

        private void DrawStrings(Graphics graphics)
        {

            m_ControlCollection.ForEach(e => e.OnDraw(graphics));
            if (m_IsBl)
            {
                graphics.DrawString(m_Str[0], FontsItems.FontC11,
                    DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush, m_Rectangle[0],
                    FontsItems.TheAlignment(FontRelated.靠左));
            }
            else
            {
                graphics.DrawString(DMITitle.MarshallMode && DMITitle.TrainInSide ? m_Str[2] : m_Str[1],
                    FontsItems.FontC11, DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                    m_Rectangle[1], FontsItems.TheAlignment(FontRelated.靠左));
                graphics.DrawString(DMITitle.MarshallMode ? (DMITitle.TrainInSide ? m_Str[1] : m_Str[2]) : "",
                    FontsItems.FontC11, DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                    m_Rectangle[2], FontsItems.TheAlignment(FontRelated.靠左));

            }
        }

        private void GetValue()
        {
            for (var i = 0; i < m_BValue.Length; i++)
            {
                if (DMITitle.TrainInSide)
                {
                    m_BValue[i] =
                        BoolList[
                            GetInBoolIndex(
                                m_BValueList[m_BValueAllArrange[(DMITitle.MarshallMode || m_IsBl) ? 1 : 0][i]])];
                }
                else
                {
                    m_BValue[i] = BoolList[GetInBoolIndex(m_BValueList[i])];
                }
            }
        }

        private void PaintGroundImage(Graphics g)
        {
            if (!m_DrawEd)
            {
                // ReSharper disable once ConditionalTernaryEqualBranch
                g.DrawImage(DMITitle.NightMode ? BrakeImages.竖线_短 : BrakeImages.竖线_短,
                    m_RectsList[18]);
                // ReSharper disable once ConditionalTernaryEqualBranch
                g.DrawImage(DMITitle.NightMode ? BrakeImages.D : BrakeImages.D,
                    m_RectsList[118]);

            }
            else
            {
                // ReSharper disable once ConditionalTernaryEqualBranch
                g.DrawImage(DMITitle.NightMode ? BrakeImages.ED : BrakeImages.ED,
                    m_RectsList[18]);

                DrawBreakeE(g);
            }

            DrawRailwayCarriageNumber(g);

            DrawBreakeD(g);
        }

        private string GetCarNumOfBl(int i)
        {
            return DMITitle.TrainInSide ? (16 - i).ToString("00") : (i + 1).ToString("00");
        }

        private void DrawRailwayCarriageNumber(Graphics g)
        {
            if (m_IsBl)
            {
                for (var index = 0; index < 16; index++)
                {
                    g.DrawString(
                        GetCarNumOfBl(index),
                        FontsItems.FontC11,
                        DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                        m_RectsList[(m_DrawEd ? 20 : 119) + index],
                        FontsItems.TheAlignment(FontRelated.居中));
                }
            }
            else
            {
                for (var index = 0; index < (DMITitle.MarshallMode ? 16 : 8); index++)
                {
                    g.DrawString(
                        GetCarNum.GetNum(index, DMITitle.MarshallMode, DMITitle.TrainInSide),
                        FontsItems.FontC11,
                        DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                        m_RectsList[(m_DrawEd ? 20 : 119) + index],
                        FontsItems.TheAlignment(FontRelated.居中));
                }
            }

        }

        private void DrawBreakeE(Graphics g)
        {
            for (var i = 0; i < ((DMITitle.MarshallMode || m_IsBl) ? 8 : 4); i++)
            {
                //制动施加E
                var carriageNo = m_RectBrakeE[i*2]/2 + 1;
                if (BoolList[m_BrakeEUnkownStateIndexDictionary[carriageNo]])
                {
                    g.DrawImage(CommonImages.StateUnkown,
                        m_RectsList[36 + m_RectBrakeE[i*2]]);
                }
                else if (m_BValue[i])
                {
                    g.FillRectangle(SolidBrushsItems.YellowBrush1, m_RectsList[36 + m_RectBrakeE[i*2]]);
                    g.DrawRectangle(DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen,
                        Rectangle.Round(m_RectsList[36 + m_RectBrakeE[i*2]]));

                }
                else
                {
                    g.DrawRectangle(DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen,
                        Rectangle.Round(m_RectsList[36 + m_RectBrakeE[i*2]]));
                }

                //制动有效E
                if (m_BValue[i + 8])
                {
                    g.FillRectangle(SolidBrushsItems.YellowBrush1, m_RectsList[36 + m_RectBrakeE[i*2 + 1]]);
                }
                g.DrawRectangle(DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen,
                    Rectangle.Round(m_RectsList[36 + m_RectBrakeE[i*2 + 1]]));
            }
        }

        private void DrawBreakeD(Graphics g)
        {
            for (var i = 0; i < ((DMITitle.MarshallMode || m_IsBl) ? 16 : 8); i++)
            {
                //制动施加D
                var carriageNo = i + 1;
                if (BoolList[m_BrakeDUnkownStateIndexDictionary[carriageNo]])
                {
                    g.DrawImage(CommonImages.StateUnkown, m_RectsList[68 + i*2]);
                    //g.DrawRectangle(DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen,
                    //    Rectangle.Round(_rectsList[68 + index * 2]));
                }
                else if (m_BValue[i + 16])
                {
                    g.FillRectangle(SolidBrushsItems.BlueBrush1, m_RectsList[68 + i*2]);
                    g.DrawRectangle(DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen,
                        Rectangle.Round(m_RectsList[68 + i*2]));
                }
                else
                {
                    g.DrawRectangle(DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen,
                        Rectangle.Round(m_RectsList[68 + i*2]));
                }
                //制动有效D
                if (m_BValue[i + 32])
                {
                    g.FillRectangle(SolidBrushsItems.BlueBrush1, m_RectsList[68 + i*2 + 1]);

                }
                g.DrawRectangle(DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen,
                    Rectangle.Round(m_RectsList[68 + i*2 + 1]));
            }
        }

        private bool m_IsBl;
        private List<RectangleF> m_Rectangle;
        private readonly string[] m_Str = {"", "动车组1", "动车组2"};
        private List<CommonInnerControlBase> m_ControlCollection;

        private void InitData()
        {
            m_IsBl = GlobalParam.Instance.ProjectType == ProjectType.CRH380BL;

            m_BValueList = new List<string>
            {
                InBoolKeys.Inb制动施加1E,
                InBoolKeys.Inb制动施加3E,
                InBoolKeys.Inb制动施加6E,
                InBoolKeys.Inb制动施加8E,
                InBoolKeys.Inb制动施加9E,
                InBoolKeys.Inb制动施加11E,
                InBoolKeys.Inb制动施加14E,
                InBoolKeys.Inb制动施加16E,
                InBoolKeys.Inb制动有效1E,
                InBoolKeys.Inb制动有效3E,
                InBoolKeys.Inb制动有效6E,
                InBoolKeys.Inb制动有效8E,
                InBoolKeys.Inb制动有效9E,
                InBoolKeys.Inb制动有效11E,
                InBoolKeys.Inb制动有效14E,
                InBoolKeys.Inb制动有效16E,
                InBoolKeys.Inb制动施加1D,
                InBoolKeys.Inb制动施加2D,
                InBoolKeys.Inb制动施加3D,
                InBoolKeys.Inb制动施加4D,
                InBoolKeys.Inb制动施加5D,
                InBoolKeys.Inb制动施加6D,
                InBoolKeys.Inb制动施加7D,
                InBoolKeys.Inb制动施加8D,
                InBoolKeys.Inb制动施加9D,
                InBoolKeys.Inb制动施加10D,
                InBoolKeys.Inb制动施加11D,
                InBoolKeys.Inb制动施加12D,
                InBoolKeys.Inb制动施加13D,
                InBoolKeys.Inb制动施加14D,
                InBoolKeys.Inb制动施加15D,
                InBoolKeys.Inb制动施加16D,
                InBoolKeys.Inb制动有效1D,
                InBoolKeys.Inb制动有效2D,
                InBoolKeys.Inb制动有效3D,
                InBoolKeys.Inb制动有效4D,
                InBoolKeys.Inb制动有效5D,
                InBoolKeys.Inb制动有效6D,
                InBoolKeys.Inb制动有效7D,
                InBoolKeys.Inb制动有效8D,
                InBoolKeys.Inb制动有效9D,
                InBoolKeys.Inb制动有效10D,
                InBoolKeys.Inb制动有效11D,
                InBoolKeys.Inb制动有效12D,
                InBoolKeys.Inb制动有效13D,
                InBoolKeys.Inb制动有效14D,
                InBoolKeys.Inb制动有效15D,
                InBoolKeys.Inb制动有效16D,


            };

            m_BValueAllArrange = new List<int[]>
            {
                new[]
                {
                    3,
                    2,
                    1,
                    0,
                    7,
                    6,
                    5,
                    4,
                    11,
                    10,
                    9,
                    8,
                    15,
                    14,
                    13,
                    12,
                    23,
                    22,
                    21,
                    20,
                    19,
                    18,
                    17,
                    16,
                    31,
                    30,
                    29,
                    28,
                    27,
                    26,
                    25,
                    24,
                    39,
                    38,
                    37,
                    36,
                    35,
                    34,
                    33,
                    32,
                    47,
                    46,
                    45,
                    44,
                    43,
                    42,
                    41,
                    40
                },
                new[]
                {
                    7,
                    6,
                    5,
                    4,
                    3,
                    2,
                    1,
                    0,
                    15,
                    14,
                    13,
                    12,
                    11,
                    10,
                    9,
                    8,
                    31,
                    30,
                    29,
                    28,
                    27,
                    26,
                    25,
                    24,
                    23,
                    22,
                    21,
                    20,
                    19,
                    18,
                    17,
                    16,
                    47,
                    46,
                    45,
                    44,
                    43,
                    42,
                    41,
                    40,
                    39,
                    38,
                    37,
                    36,
                    35,
                    34,
                    33,
                    32
                }
            };
            // 换端情况下从boollist中取值，8车
            // 换端情况下从boollist中取值，16车

            m_BValue = new bool[m_BValueList.Count];


            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIBrakeStatus, ref m_RectsList))
            {

            }

            m_Rectangle = new List<RectangleF>
            {
                new RectangleF(150, 190, 130, 30),
                new RectangleF(150, 260, 130, 30),
                new RectangleF(428, 260, 130, 30)
            };
            m_ControlCollection = new List<CommonInnerControlBase>
            {
                new Line(new Point(428, 260), new Point(428, 482))
                {
                    Color = DMITitle.NightMode ? Color.Black : Color.White,
                    Tag = 1,
                    Visible = false
                }
            };
            if (!m_IsBl)
            {
                DMITitle.MarshallModeChanged += m =>
                {
                    m_ControlCollection[m_ControlCollection.FindIndex(f => f is Line && (int) f.Tag == 1)].Visible =
                        DMITitle.MarshallMode;
                };
            }

        }
    }
}