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
    public class V70103WIFaultLamp : baseClass
    {
        private readonly List<Button> m_BtnsDownTabView = new List<Button>(); //按钮列表
        private int m_I, m_J;
        private readonly Pen m_WhiteLinePen = new Pen(new SolidBrush(Color.White), 1.6f);
        private Pen m_WhiteBigLinePen = new Pen(new SolidBrush(Color.White), 2.0f);
        private ButtonStyle m_NormalBs;
        private Rectangle m_FlagRect;
        private readonly Font m_ChineseFont = new Font("宋体", 14);

        private Font m_ChineseSmallFont = new Font("宋体", 12);
        private Point[] m_HPStarts;
        private Point[] m_HPEnds;

        private Point[] m_VPStarts;
        private Point[] m_VPEnds;

        private Rectangle[] m_FixedRects;
        private string[] m_FixedStrs;
        private Rectangle[] m_ChangeableRects;
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

            m_HPStarts = new Point[12];
            m_HPEnds = new Point[12];

            m_VPStarts = new Point[6];
            m_VPEnds = new Point[6];

            for (m_I = 0; m_I < 12; m_I++)
            {
                m_HPStarts[m_I] = new Point(125, 100 + 33 * m_I);
                m_HPEnds[m_I] = new Point(725, 100 + 33 * m_I);
            }
            for (m_I = 0; m_I < 6; m_I++)
            {
                m_VPStarts[m_I] = new Point(125 + 120 * m_I, 100);
                m_VPEnds[m_I] = new Point(125 + 120 * m_I, 463);
            }

            m_FixedStrs = new string[15]
            {
                "故障名", "主接地", "欠压", "功补过流", "辅助回路", "控制接地", "空转/滑行",
                "牵引过载", "励磁过载", "辅机故障", "原边过流", "主  机", "从  1", "从  2", "从  3"
            };

            m_FixedRects = new Rectangle[15];
            for (m_I = 0; m_I < 11; m_I++)
            {
                m_FixedRects[m_I] = new Rectangle(125, 100 + 33 * m_I, 120, 33);
            }
            for (m_I = 11; m_I < 15; m_I++)
            {
                m_FixedRects[m_I] = new Rectangle(245 + 120 * (m_I - 11), 100, 120, 33);
            }

            m_ChangeableRects = new Rectangle[40];
            for (m_I = 0; m_I < 10; m_I++)
            {
                m_ChangeableRects[4 * m_I] = new Rectangle(296, 140 + 33 * m_I, 18, 18);
                m_ChangeableRects[4 * m_I + 1] = new Rectangle(416, 140 + 33 * m_I, 18, 18);
                m_ChangeableRects[4 * m_I + 2] = new Rectangle(536, 140 + 33 * m_I, 18, 18);
                m_ChangeableRects[4 * m_I + 3] = new Rectangle(656, 140 + 33 * m_I, 18, 18);
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
            dcGs.DrawImage(ImageResource.flag2, m_FlagRect);

            m_BtnsDownTabView.ForEach(a => a.Paint(dcGs));

            if (GetInBoolValue(InBoolKeys.InB巡检0按下状态MMI0键按下状态))
            {
                m_Btnflag = true;
                //append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.WI_BTN_ShowStatus), 0, 0);
            }
            else if (m_Btnflag)
            {
                m_Btnflag = false;
                append_postCmd(CmdType.ChangePage, (int)(ViewState.WiBtnShowStatus), 0, 0);
            }

            for (m_I = 0; m_I < 12; m_I++)
            {
                dcGs.DrawLine(m_WhiteLinePen, m_HPStarts[m_I], m_HPEnds[m_I]);
            }

            for (m_I = 0; m_I < 6; m_I++)
            {
                dcGs.DrawLine(m_WhiteLinePen, m_VPStarts[m_I], m_VPEnds[m_I]);
            }

            for (m_I = 0; m_I < 15; m_I++)
            {
                dcGs.DrawString(m_FixedStrs[m_I], m_ChineseFont, Brushs.WhiteBrush, m_FixedRects[m_I], FontInfo.SfCc);
            }

            OnPaintChangeable(dcGs);

            base.paint(dcGs);
        }

        private string[] LogicName = new[]
        {
            InBoolKeys.InB主车主接地,
            InBoolKeys.InB主车欠压,
            InBoolKeys.InB主车功补过流,
            InBoolKeys.InB主车辅助回路,
            InBoolKeys.InB主车控制接地,
            InBoolKeys.InB主车空转滑行,
            InBoolKeys.InB主车牵引过载,
            InBoolKeys.InB主车励磁过载,
            InBoolKeys.InB主车辅机故障,
            InBoolKeys.InB主车原边过流,
            InBoolKeys.InB从1主接地,
            InBoolKeys.InB从1欠压,
            InBoolKeys.InB从1功补过流,
            InBoolKeys.InB从1辅助回路,
            InBoolKeys.InB从1控制接地,
            InBoolKeys.InB从1空转滑行,
            InBoolKeys.InB从1牵引过载,
            InBoolKeys.InB从1励磁过载,
            InBoolKeys.InB从1辅机故障,
            InBoolKeys.InB从1原边过流,
            InBoolKeys.InB从2主接地,
            InBoolKeys.InB从2欠压,
            InBoolKeys.InB从2功补过流,
            InBoolKeys.InB从2辅助回路,
            InBoolKeys.InB从2控制接地,
            InBoolKeys.InB从2空转滑行,
            InBoolKeys.InB从2牵引过载,
            InBoolKeys.InB从2励磁过载,
            InBoolKeys.InB从2辅机故障,
            InBoolKeys.InB从2原边过流,
            InBoolKeys.InB从3主接地,
            InBoolKeys.InB从3欠压,
            InBoolKeys.InB从3功补过流,
            InBoolKeys.InB从3辅助回路,
            InBoolKeys.InB从3控制接地,
            InBoolKeys.InB从3空转滑行,
            InBoolKeys.InB从3牵引过载,
            InBoolKeys.InB从3励磁过载,
            InBoolKeys.InB从3辅机故障,
            InBoolKeys.InB从3原边过流,
        };

        /// <summary>
        ///     onPaintChangeable
        /// </summary>
        private void OnPaintChangeable(Graphics dcGs)
        {
            for (m_I = 0; m_I < 4; m_I++)
            {
                for (m_J = 0; m_J < 10; m_J++)
                {
                    dcGs.FillEllipse(GetInBoolValue(LogicName[m_I * 10 + m_J]) ? Brushs.RedBrush : Brushs.WhiteBrush,
                        m_ChangeableRects[m_J * 4 + m_I]);
                }
            }
        }
    }
}