/*--------------------------------------------------------------------------------------------------
 *
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-07
 *
 * -------------------------------------------------------------------------------------------------
 *
 * 功能描述：视图1-主界面-No.1-辅助逆变器
 *
 *-------------------------------------------------------------------------------------------------*/

using ES.JCTMS.Common.Control;
using ES.JCTMS.Common.Control.Common;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using SS4B_TMS.Common;
using SS4B_TMS.Resource;
using System.Collections.Generic;
using System.Drawing;

namespace SS4B_TMS.PatternTwo
{
    /// <summary>
    ///     功能描述：维护-No.9-亮度调节
    ///     创建人：lih
    ///     创建时间：2015-08-06
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V10PatternTwoWorkCondition : baseClass
    {
        /// <summary>
        ///     界面绘制：按钮绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawLine(m_FramePen, m_FirstRow[0], m_FirstRow[6]);
            dcGs.DrawLine(m_FramePen, m_SecondRow[0], m_SecondRow[6]);
            dcGs.DrawLine(m_FramePen, m_ThirdRow[0], m_ThirdRow[6]);
            for (m_I = 0; m_I < 7; m_I++)
            {
                dcGs.DrawLine(m_FramePen, m_FirstRow[m_I], m_SecondRow[m_I]);
                dcGs.DrawLine(m_FramePen, m_SecondRow[m_I], m_ThirdRow[m_I]);
            }
            for (m_I = 0; m_I < 6; m_I++)
            {
                dcGs.FillRectangle(m_FillFixedBrush, m_FillFixedRects[m_I]);
            }

            for (m_I = 0; m_I < 6; m_I++)
            {
                dcGs.DrawString(m_FixedRectStrs[2 * m_I], m_ChineseFont, Brushs.BlackBrush, m_FixedRects[2 * m_I], FontInfo.SfCb);
                dcGs.DrawString(m_FixedRectStrs[2 * m_I + 1], m_ChineseFont, Brushs.BlackBrush, m_FixedRects[2 * m_I + 1],
                    FontInfo.SfCt);
            }

            //for (i = 0; i <6; i++)
            //{
            //    dcGs.DrawString(changeableRectStrs[i], _chineseFont, Brushs.GreenBrush, changeableRects[i], FontInfo.SF_CB);
            //}
            string[] LogicName1 = new[]
            {
                InBoolKeys.InB通讯A1正常,
                InBoolKeys.InB通讯A2正常,
                InBoolKeys.InB通讯B1正常,
                InBoolKeys.InB通讯B2正常,
                InBoolKeys.InBLUC通讯正常A,
                InBoolKeys.InBLUC通讯正常B,
            };
            string[] logicName2 = new string[]
            {
                InBoolKeys.InB主车机车工况MMI蓄电池合,
                InBoolKeys.InB主车机车工况MMI主断合,
                InBoolKeys.InB主车机车工况MMI牵引,
                InBoolKeys.InB主车机车工况MMI制动,
            };
            for (m_I = 0; m_I < 6; m_I++)
            {
                if (GetInBoolValue(LogicName1[m_I]))
                {
                    dcGs.DrawString(m_ChangeableRectStrs[m_I], m_ChineseFont, Brushs.GreenBrush, m_ChangeableRects[m_I],
                        FontInfo.SfCb);
                }
                else
                {
                    dcGs.DrawString(m_ChangeableDefaultStrs[m_I], m_ChineseFont, Brushs.GreenBrush, m_ChangeableRects[m_I],
                        FontInfo.SfCb);
                }
            }

            m_WorkConditionFlag = false;
            for (m_I = 0; m_I < 4; m_I++)
            {
                if (GetInBoolValue(logicName2[m_I]))
                {
                    m_WorkConditionFlag = true;
                    dcGs.DrawString(m_WorkConditionStrs[m_I], m_BigFont, Brushs.GreenBrush, m_ChangeableRects[6],
                        FontInfo.SfCc);
                }
            }
            if (m_WorkConditionFlag == false)
            {
                dcGs.DrawString(m_WorkConditionStrs[4], m_BigFont, Brushs.GreenBrush, m_ChangeableRects[6], FontInfo.SfCc);
            }

            for (m_I = 0; m_I < 2; m_I++)
            {
                dcGs.DrawRectangle(m_FramePen, m_UnitRects[m_I]);
                dcGs.DrawString(m_UnitStrs[m_I], m_ChineseFont, Brushs.WhiteBrush, m_UnitRects[m_I], FontInfo.SfCc);
            }

            m_ChangeableRectStrs[7] = GetInFloatValue(InFloatKeys.InF运行级位).ToString("0.0");
            dcGs.DrawString(m_ChangeableRectStrs[7], m_BigDigitFont, Brushs.GreenBrush, m_ChangeableRects[7], FontInfo.SfRc);

            m_ChangeableRectStrs[8] = GetInFloatValue(InFloatKeys.InF机车速度).ToString("0");
            dcGs.DrawString(m_ChangeableRectStrs[8], m_BigDigitFont, Brushs.GreenBrush, m_ChangeableRects[8], FontInfo.SfRc);

            base.paint(dcGs);
        }

        private readonly List<Button> m_Btns = new List<Button>(); //按钮列表

        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private readonly Font m_ChineseFont = new Font("宋体", 12, FontStyle.Regular);
        private readonly Font m_BigFont = new Font("宋体", 16, FontStyle.Regular);
        private readonly Font m_BigDigitFont = new Font("Agency FB", 28, FontStyle.Regular);
        private Brush m_BlackBrush = new SolidBrush(Color.Black);
        private Rectangle m_FrameRect = new Rectangle(12, 272, 674, 53);
        private Rectangle m_FrameStrRect = new Rectangle(12, 272, 131, 53);

        private string m_FrameStr = "辅助逆变器";

        private Rectangle[] m_Rects;

        private bool m_Rect1Flag = false;
        private bool m_Rect2Flag = false;
        private bool m_Rect3Flag = false;
        private bool m_Rect4Flag = false;

        private Point[] m_FirstRow;
        private Point[] m_SecondRow;
        private Point[] m_ThirdRow;

        private int[] m_PointX;
        private int[] m_PointY;
        private int m_I, m_J = 0;

        private string[] m_FixedRectStrs;
        private Rectangle[] m_FixedRects;

        private string[] m_ChangeableRectStrs;
        private Rectangle[] m_ChangeableRects;

        private readonly Pen m_FramePen = new Pen(new SolidBrush(Color.FromArgb(128, 128, 128)), 2);
        private Pen m_ChangedTextPen = new Pen(new SolidBrush(Color.Green), 2);

        private readonly SolidBrush m_FillFixedBrush = new SolidBrush(Color.FromArgb(78, 76, 113));
        private Rectangle[] m_FillFixedRects;
        //private Rectangle[] _changedRects;

        private Rectangle[] m_UnitRects;
        private string[] m_UnitStrs;

        private string[] m_ChangeableDefaultStrs;

        private string[] m_WorkConditionStrs;
        private bool m_WorkConditionFlag;

        private string[] m_WorkArrays = new string[3] { "A组工作", "B组工作", "--" };

        /// <summary>
        ///     获取组件描述信息
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "主界面机车工作情况";
        }

        /// <summary>
        ///     获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        /// <summary>
        ///     初始化函数：导入图片、创建组件控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_Rects = new Rectangle[4];
            m_Rects[0] = new Rectangle(177, 278, 61, 39);
            m_Rects[1] = new Rectangle(349, 278, 61, 39);
            m_Rects[2] = new Rectangle(435, 278, 61, 39);
            m_Rects[3] = new Rectangle(607, 278, 61, 39);

            m_PointX = new int[7] { 1, 45, 173, 217, 340, 384, 509 };
            m_PointY = new int[3] { 1, 52, 104 };

            m_FirstRow = new Point[7];
            m_SecondRow = new Point[7];
            m_ThirdRow = new Point[7];

            for (m_I = 0; m_I < m_FirstRow.Length; m_I++)
            {
                m_FirstRow[m_I] = new Point(m_PointX[m_I], m_PointY[0]);
                m_SecondRow[m_I] = new Point(m_PointX[m_I], m_PointY[1]);
                m_ThirdRow[m_I] = new Point(m_PointX[m_I], m_PointY[2]);
            }

            m_FixedRectStrs = new string[12] { "A1", "A2", "B1", "B2", "LCU", "通讯", "机车", "工况", "运行", "级位", "机车", "速度" };
            m_FixedRects = new Rectangle[12];

            m_FixedRects[0] = new Rectangle(1, 1, 44, 25);
            m_FixedRects[1] = new Rectangle(1, 26, 44, 26);

            m_FixedRects[2] = new Rectangle(173, 1, 44, 25);
            m_FixedRects[3] = new Rectangle(173, 26, 44, 26);

            m_FixedRects[4] = new Rectangle(340, 1, 44, 25);
            m_FixedRects[5] = new Rectangle(340, 26, 44, 26);

            m_FixedRects[6] = new Rectangle(1, 52, 44, 25);
            m_FixedRects[7] = new Rectangle(1, 77, 44, 26);

            m_FixedRects[8] = new Rectangle(173, 52, 44, 25);
            m_FixedRects[9] = new Rectangle(173, 77, 44, 26);

            m_FixedRects[10] = new Rectangle(340, 52, 44, 25);
            m_FixedRects[11] = new Rectangle(340, 77, 44, 26);

            m_FillFixedRects = new Rectangle[6];
            m_FillFixedRects[0] = new Rectangle(2, 2, 43, 50);
            m_FillFixedRects[1] = new Rectangle(174, 2, 43, 50);
            m_FillFixedRects[2] = new Rectangle(341, 2, 43, 50);

            m_FillFixedRects[3] = new Rectangle(2, 53, 43, 50);
            m_FillFixedRects[4] = new Rectangle(174, 53, 43, 50);
            m_FillFixedRects[5] = new Rectangle(341, 53, 43, 50);

            m_ChangeableRectStrs = new string[9] { "A组工作", "A组工作", "A组工作", "A组工作", "通讯正常", "通讯正常", "蓄电池合", "0.0", "0" };
            m_ChangeableDefaultStrs = new string[9] { "--", "--", "--", "--", "--", "--", "--", "", "" };
            m_WorkConditionStrs = new string[5] { "蓄电池合", "主断合", "牵引", "制动", "" };

            m_ChangeableRects = new Rectangle[9];
            m_ChangeableRects[0] = new Rectangle(45, 1, 128, 25);
            m_ChangeableRects[1] = new Rectangle(45, 26, 128, 26);

            m_ChangeableRects[2] = new Rectangle(217, 1, 123, 25);
            m_ChangeableRects[3] = new Rectangle(217, 26, 123, 26);

            m_ChangeableRects[4] = new Rectangle(384, 1, 125, 25);
            m_ChangeableRects[5] = new Rectangle(384, 26, 125, 26);

            m_ChangeableRects[6] = new Rectangle(45, 52, 125, 52);
            m_ChangeableRects[7] = new Rectangle(217, 52, 96, 52);
            m_ChangeableRects[8] = new Rectangle(384, 52, 96, 52);

            m_UnitRects = new Rectangle[2];
            m_UnitRects[0] = new Rectangle(313, 60, 22, 39);
            m_UnitRects[1] = new Rectangle(479, 60, 25, 39);

            m_UnitStrs = new string[2] { "级", "km/h" };

            return true;
        }

        /// <summary>
        ///     组件鼠标按下事件监测函数
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
            //判断点击的位置是否属于该button集合
            for (var i = 0; i < m_Btns.Count; i++)
            {
                if ((nPoint.X >= m_Btns[i].Rect.X)
                    && (nPoint.X <= m_Btns[i].Rect.X + m_Btns[i].Rect.Width)
                    && (nPoint.Y >= m_Btns[i].Rect.Y)
                    && (nPoint.Y <= m_Btns[i].Rect.Y + m_Btns[i].Rect.Height))
                {
                    m_Btns[i].MouseDown(nPoint);
                    break;
                }
            }

            return true;
        }

        /// <summary>
        ///     组件鼠标弹起事件监测函数
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            for (var i = 0; i < m_Btns.Count; i++)
            {
                if ((nPoint.X >= m_Btns[i].Rect.X)
                    && (nPoint.X <= m_Btns[i].Rect.X + m_Btns[i].Rect.Width)
                    && (nPoint.Y >= m_Btns[i].Rect.Y)
                    && (nPoint.Y <= m_Btns[i].Rect.Y + m_Btns[i].Rect.Height))
                {
                    m_Btns[i].MouseUp(nPoint);
                    break;
                }
            }
            return true;
        }

        /// <summary>
        ///     按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            //按钮弹起事件
            switch (e.Message)
            {
                case 0: //盘路
                    append_postCmd(CmdType.ChangePage, 101, 0, 0);
                    break;

                case 1: //故障
                    append_postCmd(CmdType.ChangePage, 102, 0, 0);
                    break;

                default:
                    break;
            }
        }
    }
}