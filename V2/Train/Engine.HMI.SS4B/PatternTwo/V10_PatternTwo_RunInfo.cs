/*--------------------------------------------------------------------------------------------------
 *
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-07
 *
 * -------------------------------------------------------------------------------------------------
 *
 * 功能描述：视图1-主界面-No.3-烟火报警
 *
 *-------------------------------------------------------------------------------------------------*/

using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using SS4B_TMS.Common;
using SS4B_TMS.Resource;
using System;
using System.Drawing;

namespace SS4B_TMS.PatternTwo
{
    /// <summary>
    ///     功能描述：视图1-主界面-No.2-牵引逆变器
    ///     创建人：lih
    ///     创建时间：2015-08-07
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V10PatternTwoRunInfo : baseClass
    {
        /// <summary>
        ///     界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            //dcGs.DrawRectangle(_framePen, _frameRect);
            //dcGs.FillRectangle(_labelFillBrush, _fillFrameRect);

            for (m_I = 0; m_I < 4; m_I++)
            {
                dcGs.DrawRectangle(m_FramePen, m_FixedRects[m_I]);
                dcGs.DrawRectangle(m_FramePen, m_ChangedRects[m_I]);
                dcGs.FillRectangle(m_FillFixeBrush, m_FillfixedRects[m_I]);
            }

            for (m_I = 0; m_I < 4; m_I++)
            {
                dcGs.DrawString(m_FixedStrs[m_I], m_ChineseFont, Brushs.BlackBrush, m_FillfixedRects[m_I], FontInfo.SfCc);
            }

            m_ChangedStrs[0] = DateTime.Now.ToString("yyyy.MM.dd  HH:mm:ss");
            dcGs.DrawString(m_ChangedStrs[0], m_ChineseSmallFont, Brushs.WhiteBrush, m_ChangedRects[0], FontInfo.SfCc);

            m_ChangedStrs[1] = string.Format("{0}公里", (int)GetInFloatValue(InFloatKeys.InF本次运行里程));
            dcGs.DrawString(m_ChangedStrs[1], m_ChineseSmallFont, Brushs.WhiteBrush, m_ChangedRects[1], FontInfo.SfCc);

            var temp = GetInFloatValue(InFloatKeys.InF本次运行时间) / (60);
            m_ChangedStrs[2] = string.Format("{0}时{1}分", (int)(temp / 60), (int)temp);
            dcGs.DrawString(m_ChangedStrs[2], m_ChineseSmallFont, Brushs.WhiteBrush, m_ChangedRects[2], FontInfo.SfCc);

            if (GetInBoolValue(InBoolKeys.InB加馈标志))
            {
                dcGs.DrawString(m_ChangedStrs[3], m_ChineseSmallFont, Brushs.WhiteBrush, m_ChangedRects[3], FontInfo.SfCc);
            }
            else
            {
                dcGs.DrawString(m_ChangedDefaultStrs[3], m_ChineseSmallFont, Brushs.WhiteBrush, m_ChangedRects[3],
                    FontInfo.SfCc);
            }
            base.paint(dcGs);
        }

        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private readonly Font m_ChineseFont = new Font("宋体", 14, FontStyle.Regular);
        private readonly Font m_ChineseSmallFont = new Font("宋体", 10, FontStyle.Regular);

        private Brush m_BlackBrush = new SolidBrush(Color.Black);

        private Rectangle m_FrameRect = new Rectangle(509, 1, 291, 538);

        private readonly Pen m_FramePen = new Pen(new SolidBrush(Color.FromArgb(128, 128, 128)), 4);

        private SolidBrush m_LabelFillBrush = new SolidBrush(Color.FromArgb(9, 38, 190));

        private Rectangle m_FillFrameRect = new Rectangle(510, 154, 289, 384);

        private Rectangle[] m_ChildrenRects;

        private readonly SolidBrush m_FillFixeBrush = new SolidBrush(Color.FromArgb(78, 76, 113));

        private string m_FrameStr = "烟火报警";

        private int m_I;
        private bool m_Rect1Flag = false;
        private bool m_Rect2Flag = false;
        private bool m_Rect3Flag = false;
        private bool m_Rect4Flag = false;
        private bool m_Rect5Flag = false;
        private bool m_Rect6Flag = false;

        private Rectangle[] m_FixedRects;
        private Rectangle[] m_FillfixedRects;
        private Rectangle[] m_ChangedRects;

        private string[] m_FixedStrs;
        private string[] m_ChangedStrs;
        private string[] m_ChangedDefaultStrs;

        /// <summary>
        ///     获取组件描述信息
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "主界面运行信息";
        }

        /// <summary>
        ///     获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        /// <summary>
        ///     初始化函数：导入图片
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            //_childrenRects = new Rectangle[6];
            //_childrenRects[0] = new Rectangle(177, 404, 61, 39);
            //_childrenRects[1] = new Rectangle(263, 404, 61, 39);
            //_childrenRects[2] = new Rectangle(349, 404, 61, 39);
            //_childrenRects[3] = new Rectangle(435, 404, 61, 39);
            //_childrenRects[4] = new Rectangle(521, 404, 61, 39);
            //_childrenRects[5] = new Rectangle(607, 404, 61, 39);
            m_FixedRects = new Rectangle[4];
            m_ChangedRects = new Rectangle[4];
            m_FillfixedRects = new Rectangle[4];
            for (m_I = 0; m_I < 4; m_I++)
            {
                m_FixedRects[m_I] = new Rectangle(509, 1 + m_I * 25, 124, 25);
                m_FillfixedRects[m_I] = new Rectangle(510, 2 + m_I * 25, 123, 24);
                m_ChangedRects[m_I] = new Rectangle(633, 1 + m_I * 25, 166, 25);
            }

            m_FixedStrs = new string[4] { "日期时间", "本次运行里程", "本次运行时间", "加馈标志" };

            m_ChangedStrs = new string[6] { "", "", "", "", "", "" };
            m_ChangedStrs[0] = DateTime.Now.ToString("yyyy.MM.dd  HH:mm:ss");
            m_ChangedStrs[1] = "0公里";
            m_ChangedStrs[2] = "0时0分";
            m_ChangedStrs[3] = "加馈";
            m_ChangedStrs[4] = "有信息";
            m_ChangedStrs[5] = "0";

            m_ChangedDefaultStrs = new string[4] { "", "", "", "未加馈" };

            return true;
        }
    }
}