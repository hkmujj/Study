/*--------------------------------------------------------------------------------------------------
 *
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-06
 *
 * -------------------------------------------------------------------------------------------------
 *
 * 功能描述：维护-No.9-亮度调节
 *
 *-------------------------------------------------------------------------------------------------*/

using ES.JCTMS.Common.Control.Common;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using SS4B_TMS.Common;
using SS4B_TMS.Resource;
using System.Drawing;

namespace SS4B_TMS.JournalTemperature
{
    /// <summary>
    ///     功能描述：维护-No.9-亮度调节
    ///     创建人：lih
    ///     创建时间：2015-08-06
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V9JTInfo : baseClass
    {
        private readonly string[] LogicName = new[]
        {
            InFloatKeys.InF轴箱1温度,
InFloatKeys.InF电机1温度,
InFloatKeys.InF抱轴1温度,
InFloatKeys.InF轴箱2温度,
InFloatKeys.InF电机2温度,
InFloatKeys.InF抱轴2温度,
InFloatKeys.InF轴箱3温度,
InFloatKeys.InF电机3温度,
InFloatKeys.InF抱轴3温度,
InFloatKeys.InF轴箱4温度,
InFloatKeys.InF电机4温度,
InFloatKeys.InF抱轴4温度,
InFloatKeys.InF轴箱5温度,
InFloatKeys.InF电机5温度,
InFloatKeys.InF抱轴5温度,
InFloatKeys.InF轴箱6温度,
InFloatKeys.InF电机6温度,
InFloatKeys.InF抱轴6温度,
InFloatKeys.InF轴箱7温度,
InFloatKeys.InF电机7温度,
InFloatKeys.InF抱轴7温度,
InFloatKeys.InF轴箱8温度,
InFloatKeys.InF电机8温度,
InFloatKeys.InF抱轴8温度,
        };

        /// <summary>
        ///     界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawString(m_FrameStrs, m_ChineseFont, Brushs.WhiteBrush, m_FrameStrRects, FontInfo.SfCc);

            for (m_I = 0; m_I < 2; m_I++)
            {
                dcGs.DrawLine(m_WhiteLinePen, m_HPStarts[m_I], m_HPEnds[m_I]);
            }

            for (m_I = 0; m_I < 2; m_I++)
            {
                dcGs.DrawLine(m_WhiteLinePen, m_VPStarts[m_I], m_VPEnds[m_I]);
            }
            dcGs.DrawLine(m_WhiteLinePen, m_VPStarts[8], m_VPEnds[8]);

            dcGs.FillRectangle(Brushs.YellowBrush, m_FillFrameRect);
            dcGs.FillRectangle(m_FillTitleBrush, m_FillTitleRect);

            for (m_I = 2; m_I < 12; m_I++)
            {
                dcGs.DrawLine(m_BlackLinePen, m_HPStarts[m_I], m_HPEnds[m_I]);
            }
            for (m_I = 1; m_I < 8; m_I++)
            {
                dcGs.DrawLine(m_BlackLinePen, m_VPStarts[m_I], m_VPEnds[m_I]);
            }

            dcGs.DrawString(m_FixedStrs[0], m_ChineseFont, Brushs.WhiteBrush, m_FixedRects[0], FontInfo.SfCc);
            dcGs.DrawString(m_FixedStrs[1], m_ChineseFont, Brushs.BlueBrush, m_FixedRects[1], FontInfo.SfLc);
            dcGs.DrawString(m_FixedStrs[2], m_ChineseFont, Brushs.BlueBrush, m_FixedRects[2], FontInfo.SfLc);
            dcGs.DrawString(m_FixedStrs[3], m_ChineseFont, Brushs.WhiteBrush, m_FixedRects[3], FontInfo.SfLc);
            dcGs.DrawString(m_FixedStrs[4], m_ChineseFont, Brushs.WhiteBrush, m_FixedRects[4], FontInfo.SfCc);

            dcGs.DrawString(m_FixedStrs[5], m_ChineseFont, Brushs.BlackBrush, m_FixedRects[5], FontInfo.SfLc);
            dcGs.DrawString(m_FixedStrs[6], m_ChineseFont, Brushs.BlackBrush, m_FixedRects[6], FontInfo.SfRc);

            for (m_I = 7; m_I < 21; m_I++)
            {
                dcGs.DrawString(m_FixedStrs[m_I], m_ChineseFont, Brushs.BlueBrush, m_FixedRects[m_I], FontInfo.SfCc);
            }

            if (GetInFloatValue(InFloatKeys.InFA轴位温度) < float.Epsilon)
            {
                m_ChangeableStrs[0] = "--";
            }
            else
            {
                m_ChangeableStrs[0] = GetInFloatValue(InFloatKeys.InFA轴位温度).ToString("0.0");
            }

            if (GetInFloatValue(InFloatKeys.InFB轴位温度) < float.Epsilon)
            {
                m_ChangeableStrs[1] = "--";
            }
            else
            {
                m_ChangeableStrs[1] = GetInFloatValue(InFloatKeys.InFB轴位温度).ToString("0.0");
            }
            dcGs.DrawString(m_ChangeableStrs[0], m_ChineseFont, Brushs.YellowBrush, m_ChangeableRects[0], FontInfo.SfCc);
            dcGs.DrawString(m_ChangeableStrs[1], m_ChineseFont, Brushs.YellowBrush, m_ChangeableRects[1], FontInfo.SfCc);
            for (m_J = 0; m_J < 8; m_J++)
            {
                var tmp = GetInFloatValue(LogicName[m_J * 3 + 0]);
                if (tmp < float.Epsilon)
                {
                    m_ChangeableStrs[m_J * 6 + 2] = "--";
                    m_ChangeableStrs[m_J * 6 + 7] = "--";
                }
                else
                {
                    m_ChangeableStrs[m_J * 6 + 2] = tmp.ToString("0.0");
                    m_ChangeableStrs[m_J * 6 + 7] = tmp.ToString("0.0");
                }
                tmp = GetInFloatValue(LogicName[m_J * 3 + 1]);

                if (tmp < float.Epsilon)
                {
                    m_ChangeableStrs[m_J * 6 + 3] = "--";
                    m_ChangeableStrs[m_J * 6 + 6] = "--";
                }
                else
                {
                    m_ChangeableStrs[m_J * 6 + 3] = tmp.ToString("0.0");
                    m_ChangeableStrs[m_J * 6 + 6] = tmp.ToString("0.0");
                }
                tmp = GetInFloatValue(LogicName[m_J * 3 + 2]);
                if (tmp < float.Epsilon)
                {
                    m_ChangeableStrs[m_J * 6 + 4] = "--";
                    m_ChangeableStrs[m_J * 6 + 5] = "--";
                }
                else
                {
                    m_ChangeableStrs[m_J * 6 + 4] = tmp.ToString("0.0");
                    m_ChangeableStrs[m_J * 6 + 5] = tmp.ToString("0.0");
                }
            }

            for (m_I = 2; m_I < 50; m_I++)
            {
                dcGs.DrawString(m_ChangeableStrs[m_I], m_ChineseFont, Brushs.BlackBrush, m_ChangeableRects[m_I], FontInfo.SfCc);
            }

            base.paint(dcGs);
        }

        //private Button _btnReduce;
        private Brush[] m_Brushes;

        private Rectangle[] m_BrsRects;

        //private Button _btnAdd;
        private readonly int m_CurrentValue = 0;

        private int m_I, m_J;
        private readonly int m_MaxValue = 5;
        private readonly Font m_ChineseFont = new Font("宋体", 12);

        private readonly Pen m_WhiteLinePen = new Pen(new SolidBrush(Color.White), 1.6f);
        private Pen m_WhiteBigLinePen = new Pen(new SolidBrush(Color.White), 2.0f);

        private readonly Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);

        private Rectangle[] m_FrameRects;
        private string m_FrameStrs;
        private Rectangle m_FrameStrRects;

        private Point[] m_HPStarts;
        private Point[] m_HPEnds;

        private Point[] m_VPStarts;
        private Point[] m_VPEnds;

        private Rectangle[] m_FixedRects;
        private string[] m_FixedStrs;

        private Rectangle[] m_ChangeableRects;
        private string[] m_ChangeableStrs;

        private readonly SolidBrush m_FillTitleBrush = new SolidBrush(Color.FromArgb(176, 183, 212));
        private Rectangle m_FillFrameRect;
        private Rectangle m_FillTitleRect;

        /// <summary>
        ///     获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "维护-亮度调节";
        }

        /// <summary>
        ///     获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        /// <summary>
        ///     初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_HPStarts = new Point[12];
            m_HPEnds = new Point[12];
            for (m_I = 0; m_I < 12; m_I++)
            {
                m_HPStarts[m_I] = new Point(12, 60 + 33 * m_I);
                m_HPEnds[m_I] = new Point(712, 60 + 33 * m_I);
            }

            m_VPStarts = new Point[9];
            m_VPEnds = new Point[9];

            m_VPStarts[0] = new Point(12, 60);
            m_VPEnds[0] = new Point(12, 423);

            m_VPStarts[1] = new Point(12, 126);
            m_VPEnds[1] = new Point(112, 159);

            for (m_I = 2; m_I < 8; m_I++)
            {
                m_VPStarts[m_I] = new Point(112 + 100 * (m_I - 2), 123);
                m_VPEnds[m_I] = new Point(112 + 100 * (m_I - 2), 423);
            }

            m_VPStarts[8] = new Point(712, 60);
            m_VPEnds[8] = new Point(712, 423);

            m_FillTitleRect = new Rectangle(13, 127, 99, 32);
            m_FillFrameRect = new Rectangle(13, 127, 700, 297);

            m_FixedRects = new Rectangle[21];
            m_FixedRects[0] = new Rectangle(12, 60, 250, 33);

            m_FixedRects[1] = new Rectangle(262, 60, 40, 33);
            m_FixedRects[2] = new Rectangle(362, 60, 40, 33);

            m_FixedRects[3] = new Rectangle(492, 60, 90, 33);

            m_FixedRects[4] = new Rectangle(12, 93, 700, 33);

            m_FixedRects[5] = new Rectangle(14, 130, 50, 33);
            m_FixedRects[6] = new Rectangle(62, 126, 50, 33);

            for (m_I = 0; m_I < 8; m_I++)
            {
                m_FixedRects[7 + m_I] = new Rectangle(12, 159 + 33 * m_I, 100, 33);
            }

            for (m_I = 0; m_I < 6; m_I++)
            {
                m_FixedRects[15 + m_I] = new Rectangle(112 + 100 * m_I, 126, 100, 33);
            }

            m_FixedStrs = new string[21]
            {
                "环境温度:", "A:", "B:", "单位℃",
                "I 端左 ---> 右",
                "轴", "位",
                "A1", "A2", "A3", "A4", "B1", "B2", "B3", "B4",
                "轴箱", "电机", "抱轴", "抱轴", "电机", "轴箱"
            };

            m_FrameStrs = "轴温信息窗口";
            m_FrameStrRects = new Rectangle(300, 30, 150, 30);

            m_ChangeableRects = new Rectangle[50];
            m_ChangeableStrs = new string[50]
            {
                "---", "---",
                "--", "--", "--", "--", "--", "--", "--", "--",
                "--", "--", "--", "--", "--", "--", "--", "--",
                "--", "--", "--", "--", "--", "--", "--", "--",
                "--", "--", "--", "--", "--", "--", "--", "--",
                "--", "--", "--", "--", "--", "--", "--", "--",
                "--", "--", "--", "--", "--", "--", "--", "--"
            };

            m_ChangeableRects[0] = new Rectangle(302, 60, 60, 33);
            m_ChangeableRects[1] = new Rectangle(402, 60, 60, 33);
            for (m_I = 0; m_I < 8; m_I++)
            {
                m_ChangeableRects[6 * m_I + 2] = new Rectangle(112, 159 + 33 * m_I, 100, 33);
                m_ChangeableRects[6 * m_I + 3] = new Rectangle(212, 159 + 33 * m_I, 100, 33);
                m_ChangeableRects[6 * m_I + 4] = new Rectangle(312, 159 + 33 * m_I, 100, 33);
                m_ChangeableRects[6 * m_I + 5] = new Rectangle(412, 159 + 33 * m_I, 100, 33);
                m_ChangeableRects[6 * m_I + 6] = new Rectangle(512, 159 + 33 * m_I, 100, 33);
                m_ChangeableRects[6 * m_I + 7] = new Rectangle(612, 159 + 33 * m_I, 100, 33);
            }

            return true;
        }

        /// <summary>
        ///     更新亮度显示
        /// </summary>
        private void OnChangedBright()
        {
            for (m_I = 0; m_I < m_CurrentValue; m_I++)
            {
                m_Brushes[m_I] = new SolidBrush(Color.FromArgb(0, 255, 0));
            }
            for (m_I = m_CurrentValue; m_I < m_MaxValue; m_I++)
            {
                m_Brushes[m_I] = new SolidBrush(Color.FromArgb(147, 147, 147));
            }
        }

        /// <summary>
        ///     mouseDown
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
            //判断点击的位置是否属于该button集合
            //if ((nPoint.X >= this._btnReduce.Rect.X)
            //       && (nPoint.X <= this._btnReduce.Rect.X + this._btnReduce.Rect.Width)
            //       && (nPoint.Y >= this._btnReduce.Rect.Y)
            //       && (nPoint.Y <= this._btnReduce.Rect.Y + this._btnReduce.Rect.Height))
            //{
            //    _btnReduce.MouseDown(nPoint);
            //}
            //if ((nPoint.X >= this._btnAdd.Rect.X)
            //       && (nPoint.X <= this._btnAdd.Rect.X + this._btnAdd.Rect.Width)
            //       && (nPoint.Y >= this._btnAdd.Rect.Y)
            //       && (nPoint.Y <= this._btnAdd.Rect.Y + this._btnAdd.Rect.Height))
            //{
            //    _btnAdd.MouseDown(nPoint);
            //}
            return base.mouseDown(nPoint);
        }

        /// <summary>
        ///     mouseUp
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            //if ((nPoint.X >= this._btnReduce.Rect.X)
            //       && (nPoint.X <= this._btnReduce.Rect.X + this._btnReduce.Rect.Width)
            //       && (nPoint.Y >= this._btnReduce.Rect.Y)
            //       && (nPoint.Y <= this._btnReduce.Rect.Y + this._btnReduce.Rect.Height))
            //{
            //    _btnReduce.MouseUp(nPoint);
            //}
            //if ((nPoint.X >= this._btnAdd.Rect.X)
            //    && (nPoint.X <= this._btnAdd.Rect.X + this._btnAdd.Rect.Width)
            //    && (nPoint.Y >= this._btnAdd.Rect.Y)
            //    && (nPoint.Y <= this._btnAdd.Rect.Y + this._btnAdd.Rect.Height))
            //{
            //    _btnAdd.MouseUp(nPoint);
            //}

            return base.mouseUp(nPoint);
        }

        /// <summary>
        ///     点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            //if ((int)ViewState.MT_AB_ReduceBtn == e.Message)
            //{
            //    if (_currentValue > 0)
            //    {
            //        _currentValue--;
            //    }
            //}
            //else if ((int)ViewState.MT_AB_AddBtn == e.Message)
            //{
            //    if (_currentValue < _MaxValue)
            //    {
            //        _currentValue++;
            //    }
            //}
            //onChangedBright();
        }
    }
}