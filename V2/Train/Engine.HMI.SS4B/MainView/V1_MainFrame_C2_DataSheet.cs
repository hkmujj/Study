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

using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using SS4B_TMS.Common;
using SS4B_TMS.Resource;
using System.Drawing;

namespace SS4B_TMS.MainView
{
    /// <summary>
    ///     功能描述：视图1-主界面-No.2-牵引逆变器
    ///     创建人：lih
    ///     创建时间：2015-08-07
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1MainFrameC2DataSheet : baseClass
    {
        private readonly string[] m_LogicName = new[]
        {
            InFloatKeys.InFA车电机电压V1,
            InFloatKeys.InFA车电机电压V2,
            InFloatKeys.InFA车电机电压V3,
            InFloatKeys.InFA车电机电压V4,
            InFloatKeys.InFB车电机电压V1,
            InFloatKeys.InFB车电机电压V2,
            InFloatKeys.InFB车电机电压V3,
            InFloatKeys.InFB车电机电压V4,
            InFloatKeys.InFA车电机电流I1,
            InFloatKeys.InFA车电机电流I2,
            InFloatKeys.InFA车电机电流I3,
            InFloatKeys.InFA车电机电流I4,
            InFloatKeys.InFB车电机电流I1,
            InFloatKeys.InFB车电机电流I2,
            InFloatKeys.InFB车电机电流I3,
            InFloatKeys.InFB车电机电流I4,
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

            for (m_I = 0; m_I < 16; m_I++)
            {
                dcGs.DrawLine(m_FramePen, m_FirstRow[m_I], m_SecondRow[m_I]);
            }

            dcGs.DrawRectangle(m_FramePen, m_LabelRect);
            dcGs.FillRectangle(m_LabelFillBrush, m_LabelFillRect);

            for (m_I = 0; m_I < 12; m_I++)
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
            }

            for (m_I = 0; m_I < 5; m_I++)
            {
                dcGs.DrawString(m_SheetDataStrs[m_I], m_DigitAFont, Brushs.WhiteBrush, m_SheetDataRects[3 * m_I], FontInfo.SfCc);
                dcGs.DrawString(m_SheetDataStrs[m_I], m_DigitAFont, Brushs.WhiteBrush, m_SheetDataRects[3 * m_I + 1],
                    FontInfo.SfCc);
                dcGs.DrawString(m_SheetDataStrs[m_I], m_DigitAFont, Brushs.WhiteBrush, m_SheetDataRects[3 * m_I + 2],
                    FontInfo.SfCc);
            }

            for (m_I = 0; m_I < 3; m_I++)
            {
                dcGs.DrawString(m_UnitStrs[m_I], m_DigitAFont, Brushs.WhiteBrush, m_UnitStrRects[m_I], FontInfo.SfCc);
            }

            for (m_I = 0; m_I < 12; m_I++)
            {
                dcGs.DrawString(m_UnitDigitStrs[m_I], m_DigitAFont, Brushs.BlackBrush, m_DigitFlagRects[m_I], FontInfo.SfCt);
            }
            for (m_I = 0; m_I < 3; m_I++)
            {
                dcGs.DrawString(m_UnitNameStrs[m_I], m_ChineseFont, Brushs.BlackBrush, m_TextFlagRects[m_I], FontInfo.SfCb);
            }

            for (m_I = 0; m_I < 4; m_I++)
            {
                m_StriptHeights[m_I] = (int)(GetInFloatValue(m_LogicName[0 + m_I]) / 5.63); //电压
                m_ColumnarFillRects[m_I].Y = 494 - m_StriptHeights[m_I];
                m_ColumnarFillRects[m_I].Height = m_StriptHeights[m_I];
                dcGs.FillRectangle(m_ColumnarBrush, m_ColumnarFillRects[m_I]);

                m_StriptHeights[m_I + 4] = (int)(GetInFloatValue(m_LogicName[8 + m_I]) / 5.63); //电流
                m_ColumnarFillRects[m_I + 4].Y = 494 - m_StriptHeights[m_I + 4];
                m_ColumnarFillRects[m_I + 4].Height = m_StriptHeights[m_I + 4];
                dcGs.FillRectangle(m_ColumnarBrush, m_ColumnarFillRects[m_I + 4]);

                m_StriptHeights[m_I + 8] = (int)(GetInFloatValue(m_LogicName[16 + m_I]) / 5.63); //励磁
                m_ColumnarFillRects[m_I + 8].Y = 494 - m_StriptHeights[m_I + 8];
                m_ColumnarFillRects[m_I + 8].Height = m_StriptHeights[m_I + 8];
                dcGs.FillRectangle(m_ColumnarBrush, m_ColumnarFillRects[m_I + 8]);
            }
            dcGs.FillPolygon(Brushs.RedBrush, m_VoltageAdirectors);
            dcGs.FillPolygon(Brushs.RedBrush, m_VoltageBdirectors);
            if (GetInBoolValue(InBoolKeys.InB主车机车工况MMI制动))
            {
                dcGs.FillPolygon(Brushs.RedBrush, m_ElectricCdirectors);
                dcGs.FillPolygon(Brushs.RedBrush, m_ElectricDdirectors);

                dcGs.FillPolygon(Brushs.RedBrush, m_ExcitationAdirectors);
                dcGs.FillPolygon(Brushs.RedBrush, m_ExcitationBdirectors);
            }
            else
            {
                dcGs.FillPolygon(Brushs.RedBrush, m_ElectricAdirectors);
                dcGs.FillPolygon(Brushs.RedBrush, m_ElectricBdirectors);
            }

            base.paint(dcGs);
        }

        private readonly Font m_ChineseFont = new Font("宋体", 16, FontStyle.Regular);
        private Rectangle[] m_ChildrenRects;
        private int m_I;

        private readonly Pen m_FramePen = new Pen(new SolidBrush(Color.FromArgb(128, 128, 128)), 4);

        private readonly Pen m_LinePen = new Pen(new SolidBrush(Color.White), 2);

        private readonly SolidBrush m_LabelFillBrush = new SolidBrush(Color.FromArgb(148, 148, 148));

        private readonly Font m_DigitAFont = new Font("Arial", 12, FontStyle.Regular);

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

        private PointF[] m_VoltageNextPoints;
        private PointF[] m_ElectNextPoints;
        private PointF[] m_ExcitationNextPoints;

        private string[] m_SheetDataStrs;
        private string[] m_UnitStrs;
        private string[] m_UnitDigitStrs;
        private string[] m_UnitNameStrs;

        private Rectangle[] m_SheetDataRects;

        private readonly SolidBrush m_ColumnarBrush = new SolidBrush(Color.FromArgb(106, 236, 236));
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

        private float m_VoltHeight = 0.0f;
        private float m_ElectHeight = 0.0f;
        private float m_ExcitHeight = 0.0f;

        private int[] m_StriptHeights;

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

            m_PointX = new int[16] { 1, 27, 56, 116, 142, 171, 198, 224, 285, 312, 340, 368, 394, 453, 481, 509 };
            m_PointY = new int[2] { 104, 495 };

            m_SheetPointX = new float[6] { 56, 116, 224, 285, 394, 453 };
            m_SheetPointY = new float[51];

            m_VoltagePoints = new PointF[102];
            m_ElectPoints = new PointF[102];
            m_ExcitationPoints = new PointF[102];

            m_VoltageNextPoints = new PointF[102];
            m_ElectNextPoints = new PointF[102];
            m_ExcitationNextPoints = new PointF[102];

            m_SheetDataStrs = new string[5] { "2000", "1600", "1200", "800", "400" };
            m_UnitStrs = new string[3] { "V", "A", "A" };
            m_UnitDigitStrs = new string[12] { "1", "2", "3", "4", "1", "2", "3", "4", "1", "2", "3", "4" };
            m_UnitNameStrs = new string[3] { "电压", "电流", "励磁" };

            m_SheetDataRects = new Rectangle[15];
            for (m_I = 0; m_I < 5; m_I++)
            {
                m_SheetDataRects[3 * m_I] = new Rectangle(56, 129 + m_I * 72, 60, 15);
                m_SheetDataRects[3 * m_I + 1] = new Rectangle(224, 129 + m_I * 72, 60, 15);
                m_SheetDataRects[3 * m_I + 2] = new Rectangle(394, 129 + m_I * 72, 60, 15);
            }

            for (m_I = 0; m_I < 51; m_I++)
            {
                m_SheetPointY[m_I] = 137 + 7.1f * m_I;
            }

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
            }

            m_FirstRow = new Point[16];
            m_SecondRow = new Point[16];
            for (m_I = 0; m_I < 16; m_I++)
            {
                m_FirstRow[m_I] = new Point(m_PointX[m_I], m_PointY[0]);
                m_SecondRow[m_I] = new Point(m_PointX[m_I], m_PointY[1]);
            }

            m_LabelRect = new Rectangle(1, 495, 508, 40);
            m_LabelFillRect = new Rectangle(2, 496, 507, 39);

            m_FillStripRects = new Rectangle[12];
            m_FillStripRects[0] = new Rectangle(2, 105, 24, 389);
            m_FillStripRects[1] = new Rectangle(28, 105, 28, 389);

            m_FillStripRects[2] = new Rectangle(117, 105, 24, 389);
            m_FillStripRects[3] = new Rectangle(143, 105, 26, 389);
            m_FillStripRects[4] = new Rectangle(172, 105, 25, 389);
            m_FillStripRects[5] = new Rectangle(199, 105, 26, 389);

            m_FillStripRects[6] = new Rectangle(286, 105, 25, 389);
            m_FillStripRects[7] = new Rectangle(313, 105, 26, 389);
            m_FillStripRects[8] = new Rectangle(341, 105, 26, 389);
            m_FillStripRects[9] = new Rectangle(369, 105, 26, 389);

            m_FillStripRects[10] = new Rectangle(454, 105, 25, 389);
            m_FillStripRects[11] = new Rectangle(482, 105, 26, 389);

            m_ColumnarFillRects = new Rectangle[12];
            m_ColumnarFillRects[0] = new Rectangle(2, 105, 24, 389);
            m_ColumnarFillRects[1] = new Rectangle(28, 105, 28, 389);

            m_ColumnarFillRects[2] = new Rectangle(117, 105, 24, 389);
            m_ColumnarFillRects[3] = new Rectangle(143, 105, 26, 389);
            m_ColumnarFillRects[4] = new Rectangle(172, 105, 25, 389);
            m_ColumnarFillRects[5] = new Rectangle(199, 105, 26, 389);

            m_ColumnarFillRects[6] = new Rectangle(286, 105, 25, 389);
            m_ColumnarFillRects[7] = new Rectangle(313, 105, 26, 389);
            m_ColumnarFillRects[8] = new Rectangle(341, 105, 26, 389);
            m_ColumnarFillRects[9] = new Rectangle(369, 105, 26, 389);

            m_ColumnarFillRects[10] = new Rectangle(454, 105, 25, 389);
            m_ColumnarFillRects[11] = new Rectangle(482, 105, 26, 389);

            m_DigitFlagRects = new Rectangle[12];
            m_DigitFlagRects[0] = new Rectangle(2, 495, 24, 30);
            m_DigitFlagRects[1] = new Rectangle(28, 495, 28, 30);

            m_DigitFlagRects[2] = new Rectangle(117, 495, 24, 30);
            m_DigitFlagRects[3] = new Rectangle(143, 495, 26, 30);
            m_DigitFlagRects[4] = new Rectangle(172, 495, 25, 30);
            m_DigitFlagRects[5] = new Rectangle(199, 495, 26, 30);

            m_DigitFlagRects[6] = new Rectangle(286, 495, 25, 30);
            m_DigitFlagRects[7] = new Rectangle(313, 495, 26, 30);
            m_DigitFlagRects[8] = new Rectangle(341, 495, 26, 30);
            m_DigitFlagRects[9] = new Rectangle(369, 495, 26, 30);

            m_DigitFlagRects[10] = new Rectangle(454, 495, 25, 30);
            m_DigitFlagRects[11] = new Rectangle(482, 495, 26, 30);

            m_TextFlagRects = new Rectangle[3];
            m_TextFlagRects[0] = new Rectangle(56, 495, 60, 42);
            m_TextFlagRects[1] = new Rectangle(224, 495, 61, 42);
            m_TextFlagRects[2] = new Rectangle(394, 495, 60, 42);

            m_UnitStrRects = new Rectangle[3];
            m_UnitStrRects[0] = new Rectangle(56, 104, 60, 20);
            m_UnitStrRects[1] = new Rectangle(224, 104, 60, 20);
            m_UnitStrRects[2] = new Rectangle(394, 104, 60, 20);

            m_VoltageAdirectors = new PointF[3];
            m_VoltageAdirectors[0] = new PointF(57, 295);
            m_VoltageAdirectors[1] = new PointF(79, 288);
            m_VoltageAdirectors[2] = new PointF(79, 302);

            m_VoltageBdirectors = new PointF[3];
            m_VoltageBdirectors[0] = new PointF(117, 295);
            m_VoltageBdirectors[1] = new PointF(95, 288);
            m_VoltageBdirectors[2] = new PointF(95, 302);

            m_ElectricAdirectors = new PointF[3];
            m_ElectricAdirectors[0] = new PointF(224, 270);
            m_ElectricAdirectors[1] = new PointF(246, 263);
            m_ElectricAdirectors[2] = new PointF(246, 277);

            m_ElectricCdirectors = new PointF[3];
            m_ElectricCdirectors[0] = new PointF(224, 341);
            m_ElectricCdirectors[1] = new PointF(246, 334);
            m_ElectricCdirectors[2] = new PointF(246, 348);

            m_ElectricBdirectors = new PointF[3];
            m_ElectricBdirectors[0] = new PointF(285, 270);
            m_ElectricBdirectors[1] = new PointF(263, 263);
            m_ElectricBdirectors[2] = new PointF(263, 277);

            m_ElectricDdirectors = new PointF[3];
            m_ElectricDdirectors[0] = new PointF(285, 341);
            m_ElectricDdirectors[1] = new PointF(263, 334);
            m_ElectricDdirectors[2] = new PointF(263, 348);

            m_ExcitationAdirectors = new PointF[3];
            m_ExcitationAdirectors[0] = new PointF(394, 285);
            m_ExcitationAdirectors[1] = new PointF(416, 278);
            m_ExcitationAdirectors[2] = new PointF(416, 292);

            m_ExcitationBdirectors = new PointF[3];
            m_ExcitationBdirectors[0] = new PointF(453, 285);
            m_ExcitationBdirectors[1] = new PointF(431, 278);
            m_ExcitationBdirectors[2] = new PointF(431, 292);

            m_StriptHeights = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            return true;
        }
    }
}