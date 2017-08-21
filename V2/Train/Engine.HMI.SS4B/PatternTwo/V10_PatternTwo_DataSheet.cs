/*--------------------------------------------------------------------------------------------------
 *
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-07
 *
 * -------------------------------------------------------------------------------------------------
 *
 * 功能描述：视图1-主界面-No.2-牵引逆变器
 *
 *-------------------------------------------------------------------------------------------------*/

using ES.JCTMS.Common.Control;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using SS4B_TMS.Common;
using SS4B_TMS.Resource;
using System.Collections.Generic;
using System.Drawing;

namespace SS4B_TMS.PatternTwo
{
    /// <summary>
    ///     功能描述：视图1-主界面-No.2-牵引逆变器
    ///     创建人：lih
    ///     创建时间：2015-08-07
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V10PatternTwoDataSheet : baseClass
    {
        private readonly string[] m_LogicName1 = new[]
           {
                InFloatKeys.InFA车电机电压V1,
                InFloatKeys.InFA车电机电压V2,
                InFloatKeys.InFA车电机电压V3,
                InFloatKeys.InFA车电机电压V4,
            };

        private readonly string[] m_LogicName2 = new[]
        {
                InFloatKeys.InFB车电机电压V1,
                InFloatKeys.InFB车电机电压V2,
                InFloatKeys.InFB车电机电压V3,
                InFloatKeys.InFB车电机电压V4,
            };

        private readonly string[] m_LogicName3 = new[]
        {
                InFloatKeys.InFA车电机电流I1,
                InFloatKeys.InFA车电机电流I2,
                InFloatKeys.InFA车电机电流I3,
                InFloatKeys.InFA车电机电流I4,
            };

        private readonly string[] m_LogicName4 = new[]
        {
                InFloatKeys.InFB车电机电流I1,
                InFloatKeys.InFB车电机电流I2,
                InFloatKeys.InFB车电机电流I3,
                InFloatKeys.InFB车电机电流I4,
            };

        private readonly string[] m_LogicName5 = new[]
        {
                InFloatKeys.InF励磁电流A1,
                InFloatKeys.InF励磁电流A2,
                InFloatKeys.InF励磁电流B1,
                InFloatKeys.InF励磁电流B2,
            };

        /// <summary>
        ///     界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawLine(m_FramePen, m_FirstRow[0], m_FirstRow[15]);
            dcGs.DrawLine(m_FramePen, m_SecondRow[0], m_SecondRow[15]);

            for (m_I = 0; m_I < 26; m_I++)
            {
                dcGs.DrawLine(m_FramePen, m_FirstRow[m_I], m_SecondRow[m_I]);
            }

            dcGs.DrawRectangle(m_FramePen, m_LabelRect);
            dcGs.FillRectangle(m_LabelFillBrush, m_LabelFillRect);

            for (m_I = 0; m_I < 20; m_I++)
            {
                dcGs.FillRectangle(Brushs.WhiteBrush, m_FillStripRects[m_I]);
            }

            for (m_I = 0; m_I < 51; m_I++)
            {
                dcGs.DrawLine(m_LinePen, m_VoltagePoints[2 * m_I], m_VoltageNextPoints[2 * m_I]);
                dcGs.DrawLine(m_LinePen, m_VoltagePoints[2 * m_I + 1], m_VoltageNextPoints[2 * m_I + 1]);

                dcGs.DrawLine(m_LinePen, m_ElectPoints[2 * m_I], m_ElectNextPoints[2 * m_I]);
                dcGs.DrawLine(m_LinePen, m_ElectPoints[2 * m_I + 1], m_ElectNextPoints[2 * m_I + 1]);

                dcGs.DrawLine(m_LinePen, m_ExcitationPoints[2 * m_I], m_ExcitationNextPoints[2 * m_I]);
                dcGs.DrawLine(m_LinePen, m_ExcitationPoints[2 * m_I + 1], m_ExcitationNextPoints[2 * m_I + 1]);

                dcGs.DrawLine(m_LinePen, m_NewElectrPoints[2 * m_I], m_NewElectrNextPoints[2 * m_I]);
                dcGs.DrawLine(m_LinePen, m_NewElectrPoints[2 * m_I + 1], m_NewElectrNextPoints[2 * m_I + 1]);

                dcGs.DrawLine(m_LinePen, m_NewExcitPoints[2 * m_I], m_NewExcitNextPoints[2 * m_I]);
                dcGs.DrawLine(m_LinePen, m_NewExcitPoints[2 * m_I + 1], m_NewExcitNextPoints[2 * m_I + 1]);
            }

            for (m_I = 0; m_I < 5; m_I++)
            {
                dcGs.DrawString(m_SheetDataStrs[m_I], m_DigitAFont, Brushs.WhiteBrush, m_SheetDataRects[5 * m_I], FontInfo.SfCc);
                dcGs.DrawString(m_SheetDataStrs[m_I], m_DigitAFont, Brushs.WhiteBrush, m_SheetDataRects[5 * m_I + 1],
                    FontInfo.SfCc);
                dcGs.DrawString(m_SheetDataStrs[m_I], m_DigitAFont, Brushs.WhiteBrush, m_SheetDataRects[5 * m_I + 2],
                    FontInfo.SfCc);
                dcGs.DrawString(m_SheetDataStrs[m_I], m_DigitAFont, Brushs.WhiteBrush, m_SheetDataRects[5 * m_I + 3],
                    FontInfo.SfCc);
                dcGs.DrawString(m_SheetDataStrs[m_I], m_DigitAFont, Brushs.WhiteBrush, m_SheetDataRects[5 * m_I + 4],
                    FontInfo.SfCc);
            }

            for (m_I = 0; m_I < 5; m_I++)
            {
                dcGs.DrawString(m_UnitStrs[m_I], m_DigitAFont, Brushs.WhiteBrush, m_UnitStrRects[m_I], FontInfo.SfCc);
            }

            for (m_I = 0; m_I < 20; m_I++)
            {
                dcGs.DrawString(m_UnitDigitStrs[m_I], m_DigitAFont, Brushs.BlackBrush, m_DigitFlagRects[m_I], FontInfo.SfCt);
            }
            for (m_I = 0; m_I < 5; m_I++)
            {
                dcGs.DrawString(m_UnitNameStrs[m_I], m_ChineseFont, Brushs.BlackBrush, m_TextFlagRects[m_I], FontInfo.SfCb);
            }

            for (m_I = 0; m_I < 4; m_I++)
            {
                m_StriptHeights[m_I] = (int)(GetInFloatValue(m_LogicName1[m_I]) / 5.63); //电压
                //striptHeights[i] = 50;
                m_StriptHeights[m_I + 4] = 50;
                m_StriptHeights[m_I + 8] = 50;
                m_StriptHeights[m_I + 12] = 50;
                m_StriptHeights[m_I + 16] = 50;

                m_ColumnarFillRects[m_I].Y = 494 - m_StriptHeights[m_I];
                m_ColumnarFillRects[m_I].Height = m_StriptHeights[m_I];
                dcGs.FillRectangle(m_ColumnarBrush, m_ColumnarFillRects[m_I]);

                m_StriptHeights[m_I + 4] = (int)(GetInFloatValue(m_LogicName2[m_I]) / 5.63); //电压
                m_ColumnarFillRects[m_I + 4].Y = 494 - m_StriptHeights[m_I + 4];
                m_ColumnarFillRects[m_I + 4].Height = m_StriptHeights[m_I + 4];
                dcGs.FillRectangle(m_ColumnarBrush, m_ColumnarFillRects[m_I + 4]);

                m_StriptHeights[m_I + 8] = (int)(GetInFloatValue(m_LogicName3[m_I]) / 5.63); //电流
                m_ColumnarFillRects[m_I + 8].Y = 494 - m_StriptHeights[m_I + 8];
                m_ColumnarFillRects[m_I + 8].Height = m_StriptHeights[m_I + 8];
                dcGs.FillRectangle(m_ColumnarBrush, m_ColumnarFillRects[m_I + 8]);

                m_StriptHeights[m_I + 12] = (int)(GetInFloatValue(m_LogicName4[m_I]) / 5.63); //电流
                m_ColumnarFillRects[m_I + 12].Y = 494 - m_StriptHeights[m_I + 12];
                m_ColumnarFillRects[m_I + 12].Height = m_StriptHeights[m_I + 12];
                dcGs.FillRectangle(m_ColumnarBrush, m_ColumnarFillRects[m_I + 12]);

                m_StriptHeights[m_I + 16] = (int)(GetInFloatValue(m_LogicName5[m_I]) / 5.63); //励磁
                m_ColumnarFillRects[m_I + 16].Y = 494 - m_StriptHeights[m_I + 16];
                m_ColumnarFillRects[m_I + 16].Height = m_StriptHeights[m_I + 16];
                dcGs.FillRectangle(m_ColumnarBrush, m_ColumnarFillRects[m_I + 16]);
            }

            dcGs.FillPolygon(Brushs.RedBrush, m_VoltageAdirectors);
            dcGs.FillPolygon(Brushs.RedBrush, m_VoltageBdirectors);

            dcGs.FillPolygon(Brushs.RedBrush, m_ElectricAdirectors);
            dcGs.FillPolygon(Brushs.RedBrush, m_ElectricBdirectors);

            if (GetInBoolValue(InBoolKeys.InB主车机车工况MMI制动))
            {
                dcGs.FillPolygon(Brushs.RedBrush, m_NewExcitAdirectors);
                dcGs.FillPolygon(Brushs.RedBrush, m_NewExcitBdirectors);

                dcGs.FillPolygon(Brushs.RedBrush, m_ElectricCdirectors);
                dcGs.FillPolygon(Brushs.RedBrush, m_ElectricDdirectors);

                dcGs.FillPolygon(Brushs.RedBrush, m_NewElectrCDirectors);
                dcGs.FillPolygon(Brushs.RedBrush, m_NewElectrDDirectors);
            }
            else
            {
                dcGs.FillPolygon(Brushs.RedBrush, m_ExcitationAdirectors);
                dcGs.FillPolygon(Brushs.RedBrush, m_ExcitationBdirectors);

                dcGs.FillPolygon(Brushs.RedBrush, m_NewElectrADirectors);
                dcGs.FillPolygon(Brushs.RedBrush, m_NewElectrBDirectors);
            }

            for (m_I = 0; m_I < 20; m_I++)
            {
                dcGs.DrawRectangle(m_LineAPen, m_DigitShowRects[m_I]);
                dcGs.DrawString(m_DigitShowStrs[m_I], m_DigitAFont, Brushs.BlackBrush, m_DigitShowRects[m_I], FontInfo.SfCc);
            }

            base.paint(dcGs);
        }

        private SolidBrush[] m_AirBrakingStates; //空气制动状态
        private List<Label> m_LabelsPower = new List<Label>(); //电源状态文本框列表
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private readonly Font m_ChineseFont = new Font("宋体", 16, FontStyle.Regular);

        private Font m_DigitFont = new Font("宋体", 10, FontStyle.Regular);

        private Brush m_BlackBrush = new SolidBrush(Color.Black);
        private Rectangle m_FrameRect = new Rectangle(12, 334, 674, 53);
        private Rectangle m_FrameStrRect = new Rectangle(12, 334, 131, 53);
        private Rectangle[] m_ChildrenRects;
        private string m_FrameStr = "牵引逆变器";
        private int m_I;
        private bool m_Rect1Flag = false;
        private bool m_Rect2Flag = false;
        private bool m_Rect3Flag = false;
        private bool m_Rect4Flag = false;

        private readonly Pen m_FramePen = new Pen(new SolidBrush(Color.FromArgb(128, 128, 128)), 4);

        private readonly Pen m_LinePen = new Pen(new SolidBrush(Color.White), 2);

        private readonly Pen m_LineAPen = new Pen(new SolidBrush(Color.White), 1.2f);

        private readonly SolidBrush m_LabelFillBrush = new SolidBrush(Color.FromArgb(148, 148, 148));

        private readonly Font m_DigitAFont = new Font("Arial", 10, FontStyle.Regular);

        private int[] m_PointX;
        private int[] m_PointY;
        private Point[] m_FirstRow;
        private Point[] m_SecondRow;

        private Rectangle m_LabelRect;
        private Rectangle m_LabelFillRect;

        private Rectangle[] m_FillStripRects;

        private float[] m_SheetPointX;
        private float[] m_SheetPointY;

        private PointF[] m_VoltagePoints;
        private PointF[] m_ElectPoints;
        private PointF[] m_ExcitationPoints;
        private PointF[] m_NewElectrPoints;
        private PointF[] m_NewExcitPoints;

        private PointF[] m_VoltageNextPoints;
        private PointF[] m_ElectNextPoints;
        private PointF[] m_ExcitationNextPoints;
        private PointF[] m_NewElectrNextPoints;
        private PointF[] m_NewExcitNextPoints;

        private string[] m_SheetDataStrs;
        private string[] m_UnitStrs;
        private string[] m_UnitDigitStrs;
        private string[] m_UnitNameStrs;

        private Rectangle[] m_SheetDataRects;

        private readonly SolidBrush m_ColumnarBrush = new SolidBrush(Color.FromArgb(148, 207, 209));
        private Rectangle[] m_ColumnarFillRects;

        private Rectangle[] m_DigitFlagRects;
        private Rectangle[] m_TextFlagRects;
        private Rectangle[] m_UnitStrRects;

        private PointF[] m_VoltageAdirectors;
        private PointF[] m_VoltageBdirectors;

        private PointF[] m_ElectricAdirectors;
        private PointF[] m_ElectricBdirectors;

        private PointF[] m_ElectricCdirectors;
        private PointF[] m_ElectricDdirectors;

        private PointF[] m_ExcitationAdirectors;
        private PointF[] m_ExcitationBdirectors;

        private PointF[] m_NewElectrADirectors;
        private PointF[] m_NewElectrBDirectors;

        private PointF[] m_NewElectrCDirectors;
        private PointF[] m_NewElectrDDirectors;

        private PointF[] m_NewExcitAdirectors;
        private PointF[] m_NewExcitBdirectors;

        private float m_VoltHeight = 0.0f;
        private float m_ElectHeight = 0.0f;
        private float m_ExcitHeight = 0.0f;

        private int[] m_StriptHeights;

        private Rectangle[] m_DigitShowRects;
        private string[] m_DigitShowStrs;

        /// <summary>
        ///     获取组件描述信息
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "主界面电压电流数据表";
        }

        /// <summary>
        ///     获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        /// <summary>
        ///     初始化函数：导入图片、初始化操作
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_ChildrenRects = new Rectangle[4];
            m_ChildrenRects[0] = new Rectangle(263, 341, 61, 39);
            m_ChildrenRects[1] = new Rectangle(349, 341, 61, 39);
            m_ChildrenRects[2] = new Rectangle(435, 341, 61, 39);
            m_ChildrenRects[3] = new Rectangle(521, 341, 61, 39);

            //pointX = new int[26] {
            //    1, 27, 56, 116, 142, 171, 198, 224,
            //    285, 312, 340, 368, 394, 453, 481, 509,
            //    535,561,620,646,672,698,724,783,809,835
            //};
            //1, 27,
            //  56, 111,//
            //  137, 166, 193,
            //  219, 275,//
            //  302, 330, 358,
            //  384, 438, //
            //  466, 494, 520,
            //  546,600,//
            //  626,642,668,
            //  694,749,//
            //  774,798
            m_PointX = new int[26]
            {
                1, 27,
                56, 111, //
                137, 164, 191,
                217, 273, //
                301, 327, 353,
                381, 435, //
                462, 488, 514,
                540, 594, //
                620, 644, 669,
                695, 750, //
                775, 799
            };
            m_PointY = new int[2] { 104, 495 };

            m_SheetPointX = new float[10] { 56, 111, 217, 273, 381, 435, 540, 594, 695, 750 };
            m_SheetPointY = new float[51];

            m_VoltagePoints = new PointF[102];
            m_ElectPoints = new PointF[102];
            m_ExcitationPoints = new PointF[102];
            m_NewElectrPoints = new PointF[102];
            m_NewExcitPoints = new PointF[102];

            m_VoltageNextPoints = new PointF[102];
            m_ElectNextPoints = new PointF[102];
            m_ExcitationNextPoints = new PointF[102];
            m_NewElectrNextPoints = new PointF[102];
            m_NewExcitNextPoints = new PointF[102];

            m_SheetDataStrs = new string[5] { "2000", "1600", "1200", "800", "400" };
            m_UnitStrs = new string[5] { "V", "V", "A", "A", "A" };
            m_UnitDigitStrs = new string[20]
            {
                "1", "2", "3", "4",
                "1", "2", "3", "4",
                "1", "2", "3", "4",
                "1", "2", "3", "4",
                "1", "2", "3", "4"
            };
            m_UnitNameStrs = new string[5] { "电压", "电压", "电流", "电流", "励磁" };

            m_SheetDataRects = new Rectangle[25];
            for (m_I = 0; m_I < 5; m_I++)
            {
                m_SheetDataRects[5 * m_I] = new Rectangle(56, 129 + m_I * 72, 52, 15);
                m_SheetDataRects[5 * m_I + 1] = new Rectangle(217, 129 + m_I * 72, 52, 15);
                m_SheetDataRects[5 * m_I + 2] = new Rectangle(381, 129 + m_I * 72, 52, 15);
                m_SheetDataRects[5 * m_I + 3] = new Rectangle(540, 129 + m_I * 72, 52, 15);
                m_SheetDataRects[5 * m_I + 4] = new Rectangle(695, 129 + m_I * 72, 52, 15);
            }

            for (m_I = 0; m_I < 51; m_I++)
            {
                m_SheetPointY[m_I] = 137 + 7.1f * m_I;
            }

            InitePointFs();

            m_FirstRow = new Point[26];
            m_SecondRow = new Point[26];
            for (m_I = 0; m_I < 26; m_I++)
            {
                m_FirstRow[m_I] = new Point(m_PointX[m_I], m_PointY[0]);
                m_SecondRow[m_I] = new Point(m_PointX[m_I], m_PointY[1]);
            }

            IniteFixedRects();

            IniteFixedDirectors();

            m_StriptHeights = new int[20]
            {
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0
            };
            return true;
        }

        /// <summary>
        ///     initePointFs
        /// </summary>
        private void InitePointFs()
        {
            for (m_I = 0; m_I < 51; m_I++)
            {
                m_VoltagePoints[2 * m_I] = new PointF(m_SheetPointX[0], m_SheetPointY[m_I]);
                if (m_I % 5 == 0)
                {
                    m_VoltageNextPoints[2 * m_I] = new PointF(m_SheetPointX[0] + 10, m_SheetPointY[m_I]);
                }
                else
                {
                    m_VoltageNextPoints[2 * m_I] = new PointF(m_SheetPointX[0] + 5, m_SheetPointY[m_I]);
                }

                m_VoltagePoints[2 * m_I + 1] = new PointF(m_SheetPointX[1], m_SheetPointY[m_I]);
                if (m_I % 5 == 0)
                {
                    m_VoltageNextPoints[2 * m_I + 1] = new PointF(m_SheetPointX[1] - 10, m_SheetPointY[m_I]);
                }
                else
                {
                    m_VoltageNextPoints[2 * m_I + 1] = new PointF(m_SheetPointX[1] - 5, m_SheetPointY[m_I]);
                }

                m_ElectPoints[2 * m_I] = new PointF(m_SheetPointX[2], m_SheetPointY[m_I]);
                if (m_I % 5 == 0)
                {
                    m_ElectNextPoints[2 * m_I] = new PointF(m_SheetPointX[2] + 10, m_SheetPointY[m_I]);
                }
                else
                {
                    m_ElectNextPoints[2 * m_I] = new PointF(m_SheetPointX[2] + 5, m_SheetPointY[m_I]);
                }

                m_ElectPoints[2 * m_I + 1] = new PointF(m_SheetPointX[3], m_SheetPointY[m_I]);
                if (m_I % 5 == 0)
                {
                    m_ElectNextPoints[2 * m_I + 1] = new PointF(m_SheetPointX[3] - 10, m_SheetPointY[m_I]);
                }
                else
                {
                    m_ElectNextPoints[2 * m_I + 1] = new PointF(m_SheetPointX[3] - 5, m_SheetPointY[m_I]);
                }

                m_ExcitationPoints[2 * m_I] = new PointF(m_SheetPointX[4], m_SheetPointY[m_I]);
                if (m_I % 5 == 0)
                {
                    m_ExcitationNextPoints[2 * m_I] = new PointF(m_SheetPointX[4] + 10, m_SheetPointY[m_I]);
                }
                else
                {
                    m_ExcitationNextPoints[2 * m_I] = new PointF(m_SheetPointX[4] + 5, m_SheetPointY[m_I]);
                }

                m_ExcitationPoints[2 * m_I + 1] = new PointF(m_SheetPointX[5], m_SheetPointY[m_I]);
                if (m_I % 5 == 0)
                {
                    m_ExcitationNextPoints[2 * m_I + 1] = new PointF(m_SheetPointX[5] - 10, m_SheetPointY[m_I]);
                }
                else
                {
                    m_ExcitationNextPoints[2 * m_I + 1] = new PointF(m_SheetPointX[5] - 5, m_SheetPointY[m_I]);
                }

                m_NewElectrPoints[2 * m_I] = new PointF(m_SheetPointX[6], m_SheetPointY[m_I]);
                if (m_I % 5 == 0)
                {
                    m_NewElectrNextPoints[2 * m_I] = new PointF(m_SheetPointX[6] + 10, m_SheetPointY[m_I]);
                }
                else
                {
                    m_NewElectrNextPoints[2 * m_I] = new PointF(m_SheetPointX[6] + 5, m_SheetPointY[m_I]);
                }
                m_NewElectrPoints[2 * m_I + 1] = new PointF(m_SheetPointX[7], m_SheetPointY[m_I]);
                if (m_I % 5 == 0)
                {
                    m_NewElectrNextPoints[2 * m_I + 1] = new PointF(m_SheetPointX[7] - 10, m_SheetPointY[m_I]);
                }
                else
                {
                    m_NewElectrNextPoints[2 * m_I + 1] = new PointF(m_SheetPointX[7] - 5, m_SheetPointY[m_I]);
                }

                m_NewExcitPoints[2 * m_I] = new PointF(m_SheetPointX[8], m_SheetPointY[m_I]);
                if (m_I % 5 == 0)
                {
                    m_NewExcitNextPoints[2 * m_I] = new PointF(m_SheetPointX[8] + 10, m_SheetPointY[m_I]);
                }
                else
                {
                    m_NewExcitNextPoints[2 * m_I] = new PointF(m_SheetPointX[8] + 5, m_SheetPointY[m_I]);
                }
                m_NewExcitPoints[2 * m_I + 1] = new PointF(m_SheetPointX[9], m_SheetPointY[m_I]);
                if (m_I % 5 == 0)
                {
                    m_NewExcitNextPoints[2 * m_I + 1] = new PointF(m_SheetPointX[9] - 10, m_SheetPointY[m_I]);
                }
                else
                {
                    m_NewExcitNextPoints[2 * m_I + 1] = new PointF(m_SheetPointX[9] - 5, m_SheetPointY[m_I]);
                }
            }
        }

        /// <summary>
        ///     initeFixedDirectors
        /// </summary>
        private void IniteFixedDirectors()
        {
            //1, 27,
            //  56, 111,//
            //  137, 164, 191,
            //  217, 273,//
            //  301, 327, 353,
            //  381, 435, //
            //  462, 488, 514,
            //  540,594,//
            //  620,644,669,
            //  695,750,//
            //  775,799

            m_VoltageAdirectors = new PointF[3];
            m_VoltageAdirectors[0] = new PointF(57, 295);
            m_VoltageAdirectors[1] = new PointF(72, 288);
            m_VoltageAdirectors[2] = new PointF(72, 302);

            m_VoltageBdirectors = new PointF[3];
            m_VoltageBdirectors[0] = new PointF(112, 295);
            m_VoltageBdirectors[1] = new PointF(98, 288);
            m_VoltageBdirectors[2] = new PointF(98, 302);

            m_ElectricAdirectors = new PointF[3];
            m_ElectricAdirectors[0] = new PointF(273, 295);
            m_ElectricAdirectors[1] = new PointF(261, 288);
            m_ElectricAdirectors[2] = new PointF(261, 302);

            m_ElectricBdirectors = new PointF[3];
            m_ElectricBdirectors[0] = new PointF(222, 295);
            m_ElectricBdirectors[1] = new PointF(234, 288);
            m_ElectricBdirectors[2] = new PointF(234, 302);

            m_ExcitationAdirectors = new PointF[3];
            m_ExcitationAdirectors[0] = new PointF(381, 270);
            m_ExcitationAdirectors[1] = new PointF(395, 263);
            m_ExcitationAdirectors[2] = new PointF(395, 277);

            m_ExcitationBdirectors = new PointF[3];
            m_ExcitationBdirectors[0] = new PointF(435, 270);
            m_ExcitationBdirectors[1] = new PointF(421, 263);
            m_ExcitationBdirectors[2] = new PointF(421, 277);

            m_ElectricCdirectors = new PointF[3];
            m_ElectricCdirectors[0] = new PointF(381, 341);
            m_ElectricCdirectors[1] = new PointF(395, 334);
            m_ElectricCdirectors[2] = new PointF(395, 348);

            m_ElectricDdirectors = new PointF[3];
            m_ElectricDdirectors[0] = new PointF(435, 341);
            m_ElectricDdirectors[1] = new PointF(421, 334);
            m_ElectricDdirectors[2] = new PointF(421, 348);

            m_NewElectrCDirectors = new PointF[3];
            m_NewElectrCDirectors[0] = new PointF(540, 341);
            m_NewElectrCDirectors[1] = new PointF(554, 334);
            m_NewElectrCDirectors[2] = new PointF(554, 348);

            m_NewElectrDDirectors = new PointF[3];
            m_NewElectrDDirectors[0] = new PointF(594, 341);
            m_NewElectrDDirectors[1] = new PointF(580, 334);
            m_NewElectrDDirectors[2] = new PointF(580, 348);

            m_NewElectrADirectors = new PointF[3];
            m_NewElectrADirectors[0] = new PointF(540, 270);
            m_NewElectrADirectors[1] = new PointF(554, 263);
            m_NewElectrADirectors[2] = new PointF(554, 277);

            m_NewElectrBDirectors = new PointF[3];
            m_NewElectrBDirectors[0] = new PointF(594, 270);
            m_NewElectrBDirectors[1] = new PointF(580, 263);
            m_NewElectrBDirectors[2] = new PointF(580, 277);

            m_NewExcitAdirectors = new PointF[3];
            m_NewExcitAdirectors[0] = new PointF(695, 285);
            m_NewExcitAdirectors[1] = new PointF(709, 278);
            m_NewExcitAdirectors[2] = new PointF(709, 293);

            m_NewExcitBdirectors = new PointF[3];
            m_NewExcitBdirectors[0] = new PointF(750, 285);
            m_NewExcitBdirectors[1] = new PointF(736, 278);
            m_NewExcitBdirectors[2] = new PointF(736, 293);
        }

        /// <summary>
        ///     initeFixedRects
        /// </summary>
        private void IniteFixedRects()
        {
            m_LabelRect = new Rectangle(1, 495, 797, 40);
            m_LabelFillRect = new Rectangle(2, 496, 796, 39);

            m_FillStripRects = new Rectangle[20];
            m_FillStripRects[0] = new Rectangle(2, 105, 24, 389);
            m_FillStripRects[1] = new Rectangle(28, 105, 27, 389);

            m_FillStripRects[2] = new Rectangle(112, 105, 24, 389);
            m_FillStripRects[3] = new Rectangle(138, 105, 26, 389);
            m_FillStripRects[4] = new Rectangle(165, 105, 25, 389);
            m_FillStripRects[5] = new Rectangle(192, 105, 25, 389);

            m_FillStripRects[6] = new Rectangle(274, 105, 26, 389);
            m_FillStripRects[7] = new Rectangle(302, 105, 25, 389);
            m_FillStripRects[8] = new Rectangle(328, 105, 25, 389);
            m_FillStripRects[9] = new Rectangle(354, 105, 25, 389);

            m_FillStripRects[10] = new Rectangle(436, 105, 25, 389);
            m_FillStripRects[11] = new Rectangle(463, 105, 25, 389);
            m_FillStripRects[12] = new Rectangle(489, 105, 25, 389);
            m_FillStripRects[13] = new Rectangle(515, 105, 24, 389);

            m_FillStripRects[14] = new Rectangle(595, 105, 24, 389);
            m_FillStripRects[15] = new Rectangle(621, 105, 23, 389);
            m_FillStripRects[16] = new Rectangle(645, 105, 24, 389);
            m_FillStripRects[17] = new Rectangle(670, 105, 24, 389);

            m_FillStripRects[18] = new Rectangle(751, 105, 24, 389);
            m_FillStripRects[19] = new Rectangle(776, 105, 24, 389);

            m_ColumnarFillRects = new Rectangle[20];
            m_ColumnarFillRects[0] = new Rectangle(2, 105, 24, 389);
            m_ColumnarFillRects[1] = new Rectangle(28, 105, 27, 389);

            m_ColumnarFillRects[2] = new Rectangle(112, 105, 24, 389);
            m_ColumnarFillRects[3] = new Rectangle(138, 105, 26, 389);
            m_ColumnarFillRects[4] = new Rectangle(165, 105, 25, 389);
            m_ColumnarFillRects[5] = new Rectangle(192, 105, 26, 389);

            m_ColumnarFillRects[6] = new Rectangle(274, 105, 26, 389);
            m_ColumnarFillRects[7] = new Rectangle(302, 105, 25, 389);
            m_ColumnarFillRects[8] = new Rectangle(328, 105, 25, 389);
            m_ColumnarFillRects[9] = new Rectangle(354, 105, 25, 389);

            m_ColumnarFillRects[10] = new Rectangle(436, 105, 25, 389);
            m_ColumnarFillRects[11] = new Rectangle(463, 105, 25, 389);
            m_ColumnarFillRects[12] = new Rectangle(489, 105, 25, 389);
            m_ColumnarFillRects[13] = new Rectangle(515, 105, 26, 389);

            m_ColumnarFillRects[14] = new Rectangle(595, 105, 25, 389);
            m_ColumnarFillRects[15] = new Rectangle(621, 105, 23, 389);
            m_ColumnarFillRects[16] = new Rectangle(645, 105, 24, 389);
            m_ColumnarFillRects[17] = new Rectangle(670, 105, 25, 389);

            m_ColumnarFillRects[18] = new Rectangle(751, 105, 24, 389);
            m_ColumnarFillRects[19] = new Rectangle(776, 105, 25, 389);

            m_DigitFlagRects = new Rectangle[20];
            m_DigitFlagRects[0] = new Rectangle(2, 495, 24, 30);
            m_DigitFlagRects[1] = new Rectangle(28, 495, 28, 30);

            m_DigitFlagRects[2] = new Rectangle(112, 495, 24, 30);
            m_DigitFlagRects[3] = new Rectangle(138, 495, 26, 30);
            m_DigitFlagRects[4] = new Rectangle(165, 495, 25, 30);
            m_DigitFlagRects[5] = new Rectangle(192, 495, 26, 30);

            m_DigitFlagRects[6] = new Rectangle(274, 495, 26, 30);
            m_DigitFlagRects[7] = new Rectangle(302, 495, 25, 30);
            m_DigitFlagRects[8] = new Rectangle(328, 495, 25, 30);
            m_DigitFlagRects[9] = new Rectangle(354, 495, 26, 30);

            m_DigitFlagRects[10] = new Rectangle(436, 495, 25, 30);
            m_DigitFlagRects[11] = new Rectangle(463, 495, 25, 30);
            m_DigitFlagRects[12] = new Rectangle(489, 495, 25, 30);
            m_DigitFlagRects[13] = new Rectangle(515, 495, 26, 30);

            m_DigitFlagRects[14] = new Rectangle(595, 495, 25, 30);
            m_DigitFlagRects[15] = new Rectangle(621, 495, 26, 30);
            m_DigitFlagRects[16] = new Rectangle(645, 495, 24, 30);
            m_DigitFlagRects[17] = new Rectangle(670, 495, 25, 30);

            m_DigitFlagRects[18] = new Rectangle(751, 495, 24, 30);
            m_DigitFlagRects[19] = new Rectangle(776, 495, 25, 30);

            m_TextFlagRects = new Rectangle[5];
            m_TextFlagRects[0] = new Rectangle(56, 495, 56, 42);
            m_TextFlagRects[1] = new Rectangle(217, 495, 56, 42);
            m_TextFlagRects[2] = new Rectangle(381, 495, 56, 42);
            m_TextFlagRects[3] = new Rectangle(540, 495, 56, 42);
            m_TextFlagRects[4] = new Rectangle(695, 495, 56, 42);

            m_UnitStrRects = new Rectangle[5];
            m_UnitStrRects[0] = new Rectangle(56, 104, 56, 20);
            m_UnitStrRects[1] = new Rectangle(217, 104, 56, 20);
            m_UnitStrRects[2] = new Rectangle(381, 104, 56, 20);
            m_UnitStrRects[3] = new Rectangle(540, 104, 56, 20);
            m_UnitStrRects[4] = new Rectangle(695, 104, 56, 20);

            //1, 27,
            //  56, 111,//
            //  137, 164, 191,
            //  217, 273,//
            //  301, 327, 353,
            //  381, 435, //
            //  462, 488, 514,
            //  540,594,//
            //  620,644,669,
            //  695,750,//
            //  775,799

            m_DigitShowRects = new Rectangle[20];
            m_DigitShowStrs = new string[20]
            {
                "9", "1", "10", "1",
                "0", "0", "6", "4",
                "0", "4", "3", "3",
                "2", "0", "2", "2",
                "5", "17", "0", "16"
            };
            for (m_I = 0; m_I < 10; m_I++)
            {
                m_DigitShowRects[2 * m_I] = new Rectangle(m_FillStripRects[2 * m_I].Left, 511, 46, 12);
                m_DigitShowRects[2 * m_I + 1] = new Rectangle(m_FillStripRects[2 * m_I].Left, 525, 46, 12);
            }
        }
    }
}