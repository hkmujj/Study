/*--------------------------------------------------------------------------------------------------
 *
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-26
 *
 * -------------------------------------------------------------------------------------------------
 *
 * 功能描述：视图7-检修-No.0-主界面
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

namespace SS4B_TMS.WirelessInterface
{
    /// <summary>
    ///     功能描述：视图7--No.0-主界面
    ///     创建人：lih
    ///     创建时间：2015-8-26
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V70101WIBrakeStatus : baseClass
    {
        private readonly string[] m_LogicName = new[]
            {
                InBoolKeys.InB主车同向端制动机状态,
                InBoolKeys.InB主车反向端制动机状态,
                InBoolKeys.InB主车同向端BCU生命信号,
                InBoolKeys.InB主车反向端BCU生命信号,
                InBoolKeys.InB主车同向端重联阀位置错误,
                InBoolKeys.InB主车反向端重联阀位置错误,
                InBoolKeys.InB主车同向端异常紧急,
                InBoolKeys.InB主车反向端异常紧急,
                InBoolKeys.InB从1同向端制动机状态,
                InBoolKeys.InB从1反向端制动机状态,
                InBoolKeys.InB从1同向端BCU生命信号,
                InBoolKeys.InB从1反向端BCU生命信号,
                InBoolKeys.InB从1同向端重联阀位置错误,
                InBoolKeys.InB从1反向端重联阀位置错误,
                InBoolKeys.InB从1同向端异常紧急,
                InBoolKeys.InB从1反向端异常紧急,
                InBoolKeys.InB从2同向端制动机状态,
                InBoolKeys.InB从2反向端制动机状态,
                InBoolKeys.InB从2同向端BCU生命信号,
                InBoolKeys.InB从2反向端BCU生命信号,
                InBoolKeys.InB从2同向端重联阀位置错误,
                InBoolKeys.InB从2反向端重联阀位置错误,
                InBoolKeys.InB从2同向端异常紧急,
                InBoolKeys.InB从2反向端异常紧急,
                InBoolKeys.InB从3同向端制动机状态,
                InBoolKeys.InB从3反向端制动机状态,
                InBoolKeys.InB从3同向端BCU生命信号,
                InBoolKeys.InB从3反向端BCU生命信号,
                InBoolKeys.InB从3同向端重联阀位置错误,
                InBoolKeys.InB从3反向端重联阀位置错误,
                InBoolKeys.InB从3同向端异常紧急,
                InBoolKeys.InB从3反向端异常紧急,
            };

        private readonly List<Button> m_BtnsDownTabView = new List<Button>(); //按钮列表
        private int m_I;
        private readonly Pen m_WhiteLinePen = new Pen(new SolidBrush(Color.White), 1.6f);
        private readonly Pen m_WhiteBigLinePen = new Pen(new SolidBrush(Color.White), 2.0f);
        private ButtonStyle m_NormalBs;
        private Rectangle m_FlagRect;

        private readonly Rectangle m_FrameRect = new Rectangle(80, 46, 600, 440);
        private Rectangle[] m_FixedRects;
        private string[] m_FixedStrs;

        private Rectangle[] m_ChangeableRects;
        private readonly Font m_ChineseFont = new Font("宋体", 14);

        private Point[] m_HPStarts;
        private Point[] m_HPEnds;

        private Point[] m_VPStarts;
        private Point[] m_VPEnds;

        private bool m_Btnflag;

        /// <summary>
        ///     获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "故障历史信息-主界面";
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
            var strsBtnTabView = new string[10] { "", "", "", "", "", "", "", "", "", "返回" };

            m_NormalBs = new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_13_CC_WAndB,
                Background = ImageResource.btn_b_up,
                DownImage = ImageResource.btn_y_down
            };
            m_FlagRect = new Rectangle(1, 539, 122, 58); //标识

            for (m_I = 0; m_I < strsBtnTabView.Length; m_I++)
            {
                var btn = new Button(
                    strsBtnTabView[m_I],
                    new Rectangle(125 + 68 * m_I, 539, 60, 60),
                    ((int)(ViewState.WiBtnShowStatus + m_I)),
                    m_NormalBs,
                    false
                    );
                btn.ClickEvent += btn_ClickEvent;
                m_BtnsDownTabView.Add(btn);
            }

            m_FixedRects = new Rectangle[15];
            m_FixedStrs = new string[15]
            {
                "状       态", "", "",
                "同向端制动机状态     ",
                "反向端制动机状态     ",
                "同向端BCU生命信号    ",
                "反向端BCU生命信号    ",
                "同向端重联阀位置错误 ",
                "反向端重联阀位置错误 ",
                "同向端异常紧急       ",
                "反向端异常紧急       ",
                "主  机", "从  1", "从  2", "从  3"
            };
            for (m_I = 0; m_I < 11; m_I++)
            {
                m_FixedRects[m_I] = new Rectangle(80, 46 + 40 * m_I, 212, 40);
            }
            for (m_I = 11; m_I < 15; m_I++)
            {
                m_FixedRects[m_I] = new Rectangle(292 + 97 * (m_I - 11), 46, 97, 40);
            }

            m_ChangeableRects = new Rectangle[40];
            for (m_I = 0; m_I < 10; m_I++)
            {
                m_ChangeableRects[4 * m_I] = new Rectangle(332, 97 + 40 * m_I, 18, 18);
                m_ChangeableRects[4 * m_I + 1] = new Rectangle(429, 97 + 40 * m_I, 18, 18);
                m_ChangeableRects[4 * m_I + 2] = new Rectangle(526, 97 + 40 * m_I, 18, 18);
                m_ChangeableRects[4 * m_I + 3] = new Rectangle(623, 97 + 40 * m_I, 18, 18);
            }

            m_HPStarts = new Point[10];
            m_HPEnds = new Point[10];

            for (m_I = 0; m_I < 10; m_I++)
            {
                m_HPStarts[m_I] = new Point(80, 86 + 40 * m_I);
                m_HPEnds[m_I] = new Point(680, 86 + 40 * m_I);
            }

            m_VPStarts = new Point[4];
            m_VPEnds = new Point[4];
            for (m_I = 0; m_I < 4; m_I++)
            {
                m_VPStarts[m_I] = new Point(292 + 97 * m_I, 46);
                m_VPEnds[m_I] = new Point(292 + 97 * m_I, 486);
            }

            return true;
        }

        /// <summary>
        ///     mouseDown
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
            for (m_I = 0; m_I < m_BtnsDownTabView.Count; m_I++)
            {
                if ((nPoint.X >= m_BtnsDownTabView[m_I].Rect.X)
                    && (nPoint.X <= m_BtnsDownTabView[m_I].Rect.X + m_BtnsDownTabView[m_I].Rect.Width)
                    && (nPoint.Y >= m_BtnsDownTabView[m_I].Rect.Y)
                    && (nPoint.Y <= m_BtnsDownTabView[m_I].Rect.Y + m_BtnsDownTabView[m_I].Rect.Height))
                {
                    m_BtnsDownTabView[m_I].MouseDown(nPoint);
                    break;
                }
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
            for (m_I = 0; m_I < m_BtnsDownTabView.Count; m_I++)
            {
                if ((nPoint.X >= m_BtnsDownTabView[m_I].Rect.X)
                    && (nPoint.X <= m_BtnsDownTabView[m_I].Rect.X + m_BtnsDownTabView[m_I].Rect.Width)
                    && (nPoint.Y >= m_BtnsDownTabView[m_I].Rect.Y)
                    && (nPoint.Y <= m_BtnsDownTabView[m_I].Rect.Y + m_BtnsDownTabView[m_I].Rect.Height))
                {
                    m_BtnsDownTabView[m_I].MouseUp(nPoint);
                    break;
                }
            }

            return base.mouseUp(nPoint);
        }

        /// <summary>
        ///     按钮点击事件响应函数：切换到相应视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case (int)ViewState.WiBtnBack:
                    append_postCmd(CmdType.ChangePage, (int)ViewState.WiBtnShowStatus, 0, 0);
                    break;

                default:
                    break;
            }
            if (m_BtnsDownTabView.Find(a => a.ID == e.Message) != null)
            {
                m_BtnsDownTabView.Find(a => a.ID == e.Message).IsReplication = false;
            }
            m_BtnsDownTabView.FindAll(a => a.ID != e.Message).ForEach(b => b.IsReplication = true);
        }

        /// <summary>
        ///     界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawRectangle(m_WhiteBigLinePen, m_FrameRect);

            for (m_I = 0; m_I < 10; m_I++)
            {
                dcGs.DrawLine(m_WhiteLinePen, m_HPStarts[m_I], m_HPEnds[m_I]);
            }
            for (m_I = 0; m_I < 4; m_I++)
            {
                dcGs.DrawLine(m_WhiteLinePen, m_VPStarts[m_I], m_VPEnds[m_I]);
            }

            dcGs.DrawImage(ImageResource.flag2, m_FlagRect);

            m_BtnsDownTabView.ForEach(a => a.Paint(dcGs));

            if (GetInBoolValue(InBoolKeys.InB巡检0按下状态MMI0键按下状态))
            {
                m_Btnflag = true;
                // append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.WI_BTN_ShowStatus), 0, 0);
            }
            else if (m_Btnflag)
            {
                m_Btnflag = false;
                append_postCmd(CmdType.ChangePage, (int)(ViewState.WiBtnShowStatus), 0, 0);
            }

            OnPaintFixed(dcGs);
            OnPaintChanged(dcGs);

            base.paint(dcGs);
        }

        /// <summary>
        ///     onPaintFixed
        /// </summary>
        /// <param name="dcGs"></param>
        private void OnPaintFixed(Graphics dcGs)
        {
            for (m_I = 0; m_I < 15; m_I++)
            {
                dcGs.DrawString(m_FixedStrs[m_I], m_ChineseFont, Brushs.WhiteBrush, m_FixedRects[m_I], FontInfo.SfCc);
            }
        }

        /// <summary>
        ///     onPaintChanged
        /// </summary>
        /// <param name="dcGs"></param>
        private void OnPaintChanged(Graphics dcGs)
        {
            for (m_I = 0; m_I < 4; m_I++)
            {
                for (int i = 0; i < 2; i++)
                {
                    dcGs.FillEllipse(Brushs.WhiteBrush,
                   m_ChangeableRects[i + m_I * 2]);
                }
                for (int i = 0; i < 8; i++)
                {
                    dcGs.FillEllipse(GetInBoolValue(m_LogicName[m_I * 8 + i]) ? Brushs.RedBrush : Brushs.WhiteBrush,
                    m_ChangeableRects[8 + 4 * i + m_I]);
                }
            }
        }
    }
}