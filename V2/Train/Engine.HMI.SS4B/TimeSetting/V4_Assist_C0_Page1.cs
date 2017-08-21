/*--------------------------------------------------------------------------------------------------
 *
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-24
 *
 * -------------------------------------------------------------------------------------------------
 *
 * 功能描述：视图4-辅助-No.0-页面1
 *
 *-------------------------------------------------------------------------------------------------*/

using ES.JCTMS.Common;
using ES.JCTMS.Common.Control;
using ES.JCTMS.Common.Control.Common;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using SS4B_TMS.Common;
using SS4B_TMS.Resource;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SS4B_TMS.TimeSetting
{
    /// <summary>
    ///     功能描述：视图4-辅助-No.0-页面1
    ///     创建人：lih
    ///     创建时间：2015-8-24
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V4AssistC0Page1 : baseClass
    {
        private string[] LogicNames = new string[]
        {
            InBoolKeys.InB向前1按下状态MMI1键按下状态,
            InBoolKeys.InB调车2按下状态MMI2键按下状态,
            InBoolKeys.InB车位3按下状态MMI3键按下状态,
            InBoolKeys.InB进路号4按下状态MMI4键按下状态,
            InBoolKeys.InB定标5按下状态MMI5键按下状态,
            InBoolKeys.InB向后6按下状态MMI6键按下状态,
            InBoolKeys.InB开车7按下状态MMI7键按下状态,
            InBoolKeys.InB自动校正8按下状态MMI8键按下状态,
            InBoolKeys.InB出入库9按下状态MMI9键按下状态,
            InBoolKeys.InB巡检0按下状态MMI0键按下状态,
            InBoolKeys.InB设定按下状态,
            InBoolKeys.InB确认按下状态MMIE确认键按下状态,
            InBoolKeys.InB方向向前按下状态MMI向上按键按下状态,
            InBoolKeys.InB方向向后按下状态MMI向下按键按下状态,
            InBoolKeys.InB方向向左按下状态MMI向左按键按下状态,
            InBoolKeys.InB方向向右按下状态MMI向右按键按下状态,
        };

        /// <summary>
        ///     界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawImage(ImageResource.flag1, m_FlagRect);
            m_BtnsDownTabView.ToList().ForEach(a => a.Paint(dcGs));
            m_OffsideBtns.ToList().ForEach(a => a.Paint(dcGs));
            m_ConfirmBtn.Paint(dcGs);

            for (m_I = 0; m_I < 3; m_I++)
            {
                dcGs.DrawString(m_RectStrs[m_I], m_ChineseFont, Brushs.WhiteBrush, m_Rects[m_I], FontInfo.SfCc);
            }

            if (GetInBoolValue(LogicNames[10]))
            {
                m_BtnFlags[10] = true;
            }
            else if (m_BtnFlags[10])
            {
                m_BtnFlags[10] = false;
                append_postCmd(CmdType.ChangePage, (int)(ViewState.MainInterface), 0, 0);
            }

            if (GetInBoolValue(LogicNames[12]))
            {
                m_BtnFlags[12] = true;
            }
            else if (m_BtnFlags[12])
            {
                m_BtnFlags[12] = false;
                if (m_CursorIndex >= 8)
                {
                    m_CursorIndex = 0;
                }
            }

            if (GetInBoolValue(LogicNames[13]))
            {
                m_BtnFlags[13] = true;
            }
            else if (m_BtnFlags[13])
            {
                m_BtnFlags[13] = false;
                if (m_CursorIndex < 8)
                {
                    m_CursorIndex = 8;
                }
            }

            if (GetInBoolValue(LogicNames[14]))
            {
                m_BtnFlags[14] = true;
            }
            else if (m_BtnFlags[14])
            {
                m_BtnFlags[14] = false;
                if (m_CursorIndex < 8 && m_CursorIndex > 0)
                {
                    m_CursorIndex--;
                }
                if (m_CursorIndex > 8 && m_CursorIndex < 14)
                {
                    m_CursorIndex--;
                }
            }

            if (GetInBoolValue(LogicNames[15]))
            {
                m_BtnFlags[15] = true;
            }
            else if (m_BtnFlags[15])
            {
                m_BtnFlags[15] = false;
                if (m_CursorIndex >= 0 && m_CursorIndex <= 7)
                {
                    m_CursorIndex++;
                }
                if (m_CursorIndex >= 8 && m_CursorIndex <= 12)
                {
                    m_CursorIndex++;
                }
            }

            for (m_I = 0; m_I < 10; m_I++)
            {
                if (GetInBoolValue(LogicNames[m_I]))
                {
                    m_BtnFlags[m_I] = true;
                }
                else if (m_BtnFlags[m_I])
                {
                    m_BtnFlags[m_I] = false;
                    m_InputStrs[m_CursorIndex] = m_StrsBtnTabView[m_I];
                    if (m_CursorIndex < 13)
                    {
                        m_CursorIndex++;
                    }
                }
            }

            m_DateString = string.Format("{0}{1}{2}{3}-{4}{5}-{6}{7}",
                m_InputStrs[0], m_InputStrs[1], m_InputStrs[2], m_InputStrs[3],
                m_InputStrs[4], m_InputStrs[5],
                m_InputStrs[6], m_InputStrs[7]);

            m_TimeString = string.Format("{0}{1}:{2}{3}:{4}{5}",
                m_InputStrs[8], m_InputStrs[9],
                m_InputStrs[10], m_InputStrs[11],
                m_InputStrs[12], m_InputStrs[13]);

            dcGs.FillRectangle(Brushs.WhiteBrush, m_DateRect);
            dcGs.FillRectangle(Brushs.WhiteBrush, m_TimeRect);

            if (m_CursorIndex < 8)
            {
                dcGs.FillRectangle(Brushs.BlueBrush, m_DateFillRect);
                dcGs.DrawString(m_DateString, m_ChineseFont, Brushs.WhiteBrush, m_DateRect, FontInfo.SfCc);

                dcGs.DrawLine(m_WhiteLinePen, m_PointX[m_CursorIndex], m_PointY[0], m_PointX[m_CursorIndex] + 6, m_PointY[0]);

                dcGs.DrawString(m_TimeString, m_ChineseFont, Brushs.BlackBrush, m_TimeRect, FontInfo.SfCc);
            }

            if (m_CursorIndex >= 8)
            {
                dcGs.FillRectangle(Brushs.BlueBrush, m_TimeFillRect);

                dcGs.DrawString(m_TimeString, m_ChineseFont, Brushs.WhiteBrush, m_TimeRect, FontInfo.SfCc);

                dcGs.DrawLine(m_WhiteLinePen, m_PointX[m_CursorIndex], m_PointY[1], m_PointX[m_CursorIndex] + 6, m_PointY[1]);

                dcGs.DrawString(m_DateString, m_ChineseFont, Brushs.BlackBrush, m_DateRect, FontInfo.SfCc);
            }
            base.paint(dcGs);
        }

        private readonly List<Button> m_BtnsDownTabView = new List<Button>(); //按钮列表
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);

        private readonly Pen m_WhiteLinePen = new Pen(new SolidBrush(Color.White), 1.6f);

        private int m_I, m_J = 0;
        private SolidBrush m_RectBrush = new SolidBrush(Color.FromArgb(1, 116, 154));
        private Font m_DigitFont = new Font("Arial", 14);
        private readonly Font m_ChineseFont = new Font("宋体", 14);
        private Font m_ChineseFontA = new Font("宋体", 14, FontStyle.Bold);
        private Rectangle m_PageRect = new Rectangle(710, 335, 56, 27);
        private SolidBrush m_BlackBrush = new SolidBrush(Color.Black);
        private Rectangle[] m_FrameRects;
        private Rectangle[] m_FrameNameRects;
        private string[] m_FrameStr;
        private string m_PageStr = "页1-2";

        private readonly List<Button> m_OffsideBtns = new List<Button>();
        private ButtonStyle m_NormalBs;
        private Rectangle m_FlagRect;

        private string m_DateString = "0000-00-00";
        private string m_TimeString = "00:00:00";
        private Rectangle m_DateRect;
        private Rectangle m_TimeRect;

        private Rectangle m_DateFillRect;
        private Rectangle m_TimeFillRect;

        private Button m_ConfirmBtn;

        private string[] m_RectStrs;
        private Rectangle[] m_Rects;

        private int[] m_PointX;
        private int[] m_PointY;

        private int m_CursorIndex;
        private string[] m_StrsBtnTabView;

        private string[] m_InputStrs;

        private bool[] m_BtnFlags;

        /// <summary>
        ///     获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "辅助-页面1";
        }

        /// <summary>
        ///     获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        /// <summary>
        ///     初始化函数：初始化操作
        /// </summary>
        /// <param name="nErrorObjectIndex">错误索性</param>
        /// <returns>初始化是否成功</returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_StrsBtnTabView = new string[10] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            m_NormalBs = new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_14_CC_WAndB,
                Background = ImageResource.btn_b_up,
                DownImage = ImageResource.btn_y_down
            };

            for (m_I = 0; m_I < m_StrsBtnTabView.Length; m_I++)
            {
                var btn = new Button(
                    m_StrsBtnTabView[m_I],
                    new Rectangle(125 + 68 * m_I, 539, 60, 60),
                    ((int)(ViewState.SiDigitOne + m_I)),
                    m_NormalBs,
                    false
                    );
                btn.ClickEvent += btn_ClickEvent;
                m_BtnsDownTabView.Add(btn);
            }
            m_FlagRect = new Rectangle(1, 539, 122, 58); //标识

            var comonStyle = new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_14_CC_WAndB,
                Background = ImageResource.back,
                DownImage = ImageResource.btn_y_down
            };
            var btnStrs = new string[5] { "返回", "上移", "下移", "左移", "右移" };
            for (m_I = 0; m_I < btnStrs.Length; m_I++)
            {
                var btn = new Button(
                    btnStrs[m_I],
                    new Rectangle(738, 104 + 66 * m_I, 58, 60),
                    ((int)(ViewState.SiBtnBack + m_I)),
                    comonStyle,
                    true
                    );
                btn.ClickEvent += offsideBtn_ClickEvent;
                m_OffsideBtns.Add(btn);
            }

            var confirmStyle = new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_14_CC_WAndB,
                Background = ImageResource.confirm,
                DownImage = ImageResource.btn_y_down
            };
            m_ConfirmBtn = new Button(
                "确认",
                new RectangleF(738, 433, 58, 95),
                (int)ViewState.SiBtnConfirm,
                confirmStyle,
                true
                );
            m_ConfirmBtn.ClickEvent += offsideBtn_ClickEvent;

            m_RectStrs = new string[3] { "时间设置", "日期:", "时间:" };
            m_Rects = new Rectangle[3];
            m_Rects[0] = new Rectangle(362, 72, 125, 30);
            m_Rects[1] = new Rectangle(129, 197, 60, 30);
            m_Rects[2] = new Rectangle(129, 260, 60, 30);

            m_DateRect = new Rectangle(312, 197, 255, 40);
            m_TimeRect = new Rectangle(312, 260, 255, 40);

            m_DateFillRect = new Rectangle(313, 198, 254, 39);
            m_TimeFillRect = new Rectangle(313, 261, 254, 39);

            m_PointX = new int[14]
            {
                392, 402, 412, 421, 441, 451, 471, 481,
                402, 412, 430, 441, 461, 471
            };
            m_InputStrs = new string[14]
            {
                "0", "0", "0", "0",
                "0", "0", "0", "0",
                "0", "0", "0", "0",
                "0", "0"
            };
            m_PointY = new int[2] { 224, 287 };

            m_BtnFlags = new bool[16]
            {
                false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false
            };

            return true;
        }

        /// <summary>
        ///     mouseDown
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
            //判断点击的位置是否属于该button集合
            for (var i = 0; i < m_BtnsDownTabView.Count; i++)
            {
                if ((nPoint.X >= m_BtnsDownTabView[i].Rect.X)
                    && (nPoint.X <= m_BtnsDownTabView[i].Rect.X + m_BtnsDownTabView[i].Rect.Width)
                    && (nPoint.Y >= m_BtnsDownTabView[i].Rect.Y)
                    && (nPoint.Y <= m_BtnsDownTabView[i].Rect.Y + m_BtnsDownTabView[i].Rect.Height))
                {
                    m_BtnsDownTabView[i].MouseDown(nPoint);
                    break;
                }
            }

            for (var i = 0; i < m_OffsideBtns.Count; i++)
            {
                if ((nPoint.X >= m_OffsideBtns[i].Rect.X)
                    && (nPoint.X <= m_OffsideBtns[i].Rect.X + m_OffsideBtns[i].Rect.Width)
                    && (nPoint.Y >= m_OffsideBtns[i].Rect.Y)
                    && (nPoint.Y <= m_OffsideBtns[i].Rect.Y + m_OffsideBtns[i].Rect.Height))
                {
                    m_OffsideBtns[i].MouseDown(nPoint);
                    break;
                }
            }

            if ((nPoint.X >= m_ConfirmBtn.Rect.X)
                && (nPoint.X <= m_ConfirmBtn.Rect.X + m_ConfirmBtn.Rect.Width)
                && (nPoint.Y >= m_ConfirmBtn.Rect.Y)
                && (nPoint.Y <= m_ConfirmBtn.Rect.Y + m_ConfirmBtn.Rect.Height))
            {
                m_ConfirmBtn.MouseDown(nPoint);
            }

            return base.mouseDown(nPoint);
        }

        /// <summary>
        ///     mouseUp
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            for (var i = 0; i < m_BtnsDownTabView.Count; i++)
            {
                if ((nPoint.X >= m_BtnsDownTabView[i].Rect.X)
                    && (nPoint.X <= m_BtnsDownTabView[i].Rect.X + m_BtnsDownTabView[i].Rect.Width)
                    && (nPoint.Y >= m_BtnsDownTabView[i].Rect.Y)
                    && (nPoint.Y <= m_BtnsDownTabView[i].Rect.Y + m_BtnsDownTabView[i].Rect.Height))
                {
                    m_BtnsDownTabView[i].MouseUp(nPoint);
                    break;
                }
            }

            for (var i = 0; i < m_OffsideBtns.Count; i++)
            {
                if ((nPoint.X >= m_OffsideBtns[i].Rect.X)
                    && (nPoint.X <= m_OffsideBtns[i].Rect.X + m_OffsideBtns[i].Rect.Width)
                    && (nPoint.Y >= m_OffsideBtns[i].Rect.Y)
                    && (nPoint.Y <= m_OffsideBtns[i].Rect.Y + m_OffsideBtns[i].Rect.Height))
                {
                    m_OffsideBtns[i].MouseUp(nPoint);
                    break;
                }
            }

            if ((nPoint.X >= m_ConfirmBtn.Rect.X)
                && (nPoint.X <= m_ConfirmBtn.Rect.X + m_ConfirmBtn.Rect.Width)
                && (nPoint.Y >= m_ConfirmBtn.Rect.Y)
                && (nPoint.Y <= m_ConfirmBtn.Rect.Y + m_ConfirmBtn.Rect.Height))
            {
                m_ConfirmBtn.MouseUp(nPoint);
            }

            return base.mouseUp(nPoint);
        }

        /// <summary>
        ///     offsideBtn_ClickEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void offsideBtn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case (int)ViewState.SiBtnBack:
                    append_postCmd(CmdType.ChangePage, (int)CommonStatus.CurrentViewState, 0, 0);
                    break;

                case (int)ViewState.SiBtnConfirm:
                    //append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)Common.CommonStatus.CurrentViewState, 0, 0);
                    break;

                case (int)ViewState.SiBtnShitUp:
                    if (m_CursorIndex >= 8)
                    {
                        m_CursorIndex = 0;
                    }
                    break;

                case (int)ViewState.SiBtnShitDown:
                    if (m_CursorIndex < 8)
                    {
                        m_CursorIndex = 8;
                    }
                    break;

                case (int)ViewState.SiBtnShitLeft:
                    if (m_CursorIndex < 8 && m_CursorIndex > 0)
                    {
                        m_CursorIndex--;
                    }
                    if (m_CursorIndex > 8 && m_CursorIndex < 14)
                    {
                        m_CursorIndex--;
                    }
                    break;

                case (int)ViewState.SiBtnShitRight:
                    if (m_CursorIndex >= 0 && m_CursorIndex < 7)
                    {
                        m_CursorIndex++;
                    }
                    if (m_CursorIndex >= 8 && m_CursorIndex <= 12)
                    {
                        m_CursorIndex++;
                    }
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        ///     菜单切换按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (m_BtnsDownTabView.Find(a => a.ID == e.Message) != null)
            {
                m_BtnsDownTabView.Find(a => a.ID == e.Message).IsReplication = false;
            }

            m_InputStrs[m_CursorIndex] = m_StrsBtnTabView[e.Message - (int)ViewState.SiDigitOne];
            if (m_CursorIndex < 13)
            {
                m_CursorIndex++;
            }
            m_BtnsDownTabView.FindAll(a => a.ID != e.Message).ForEach(b => b.IsReplication = true);
        }
    }
}