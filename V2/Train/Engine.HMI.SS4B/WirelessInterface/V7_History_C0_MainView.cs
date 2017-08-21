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

using CommonUtil.Util;
using ES.JCTMS.Common;
using ES.JCTMS.Common.Control;
using ES.JCTMS.Common.Control.Common;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using SS4B_TMS.Common;
using SS4B_TMS.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace SS4B_TMS.WirelessInterface
{
    /// <summary>
    ///     功能描述：视图7--No.0-主界面
    ///     创建人：lih
    ///     创建时间：2015-8-26
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V7HistoryC0MainView : baseClass
    {
        private string[] LogicNames = new[]
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
            InBoolKeys.InB主从连接成功标志,
        };

        private string[] OtherCarLteLogic =
        {
            InBoolKeys.InB从1LTEA绿,
            InBoolKeys.InB从1LTEB绿,
            InBoolKeys.InB从2LTEA绿,
            InBoolKeys.InB从2LTEB绿,
            InBoolKeys.InB从3LTEA绿,
            InBoolKeys.InB从3LTEB绿,
        };

        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);

        private readonly Pen m_WhiteLinePen = new Pen(new SolidBrush(Color.White), 1.6f);
        private readonly Pen m_WhiteBigLinePen = new Pen(new SolidBrush(Color.White), 2.6f);
        private Rectangle m_FaultInfoRec = new Rectangle(12, 500, 300, 30);
        private readonly Font m_ChineseFont = new Font("宋体", 14);
        private Font m_ChineseBigFont = new Font("宋体", 18);

        private readonly Font m_DigitFont = new Font("Arial", 12);

        private ButtonStyle m_NormalBs;
        private Rectangle m_FlagRect;

        private int m_I;

        private int m_CurrentPageIndex = 1;
        private int m_AllPageCount = 1;
        private readonly string m_TimeShowFormat = "yyyy-MM-dd HH:mm:ss";

        private int m_ItemsCount = 0;

        private readonly List<Button> m_BtnsDownTabView = new List<Button>(); //按钮列表

        private Rectangle[] m_UpRects;
        private Rectangle[] m_CentreRects;

        //当前端为A端所在区域
        private readonly Rectangle m_CurrentTerminalRect = new Rectangle(1, 1, 141, 36);

        private readonly string m_CurTStr = "当前端为";
        private readonly string[] m_TerminalStrs = new string[2] { "A端", "B端" };

        private readonly Rectangle m_CurTimeRect = new Rectangle(623, 1, 177, 36);
        private string m_CurrentTimeStr;

        private Rectangle[] m_UpFixedRects;

        private Rectangle[] m_UpFixedStrsRects;
        private string[] m_UpFixedStrs;

        private Rectangle[] m_UpFillRects;
        private string[] m_UpChangedStrs;

        private Point[] m_UpFixedLineStarts;
        private Point[] m_UpFixedLineEnds;

        private Point[] m_CentreLineStarts;
        private Point[] m_CentreLineEnds;

        private int[] m_CentrePXs;
        private int[] m_CentrePYs;

        private Rectangle[] m_CentreFixedRects;
        private string[] m_CentreFixedStrs;

        private Rectangle[] m_CentreFillRects;
        private string[] m_CentreFillStrs;

        private Rectangle m_FaultRect = new Rectangle(6, 492, 250, 30);
        private string m_FaultStr = "";

        private bool m_BztsFlag;
        private readonly Rectangle m_BztsRect = new Rectangle(300, 465, 200, 40);
        private readonly string m_ConnectionOk = "编组成功";
        private readonly string m_BztsStr = "正在通讯....,请等待!";
        private Timer m_Timer;
        private bool[] m_BtnFlags;

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
            m_Timer = new Timer((state) =>
              {
                  if (GetOutBoolValue(OutBoolKeys.OutB编组设置))
                  {
                      append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.OutB编组设置), 0, 0);
                  }
              }, null, 0, int.MaxValue);

            var strsBtnTabView = new string[10] { "状态显示", "编组", "", "解除编组", "", "编组设置", "改端", "编组诊断", "", "返回" };
            m_BtnFlags = new bool[10] { false, false, false, false, false, false, false, false, false, false };
            m_NormalBs = new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_14_CC_W,
                Background = ImageResource.btn_b_up,
                DownImage = ImageResource.btn_b_down
            };

            var greenStyle = new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_14_CC_WAndB,
                Background = ImageResource.btn_g_up,
                DownImage = ImageResource.btn_g_down
            };

            var redStyle = new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_14_CC_WAndB,
                Background = ImageResource.btn_r_up,
                DownImage = ImageResource.btn_r_down
            };

            var yellowStyle = new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_14_CC_WAndB,
                Background = ImageResource.btn_y_up,
                DownImage = ImageResource.btn_y_down
            };

            for (m_I = 0; m_I < strsBtnTabView.Length; m_I++)
            {
                var btn = new Button(
                    strsBtnTabView[m_I],
                    new Rectangle(125 + 68 * m_I, 539, 60, 60),
                    ((int)(ViewState.WiBtnShowStatus + m_I)),
                    m_NormalBs,
                    true
                    );
                btn.ClickEvent += btn_ClickEvent;
                m_BtnsDownTabView.Add(btn);
            }
            m_BtnsDownTabView[1].Style = greenStyle;
            m_BtnsDownTabView[3].Style = redStyle;
            m_BtnsDownTabView[5].Style = yellowStyle;

            m_UpRects = new Rectangle[4];
            m_UpRects[0] = new Rectangle(6, 38, 165, 97);
            m_UpRects[1] = new Rectangle(216, 38, 165, 97);
            m_UpRects[2] = new Rectangle(423, 38, 165, 97);
            m_UpRects[3] = new Rectangle(623, 38, 171, 97);

            m_CentreRects = new Rectangle[2];
            m_CentreRects[0] = new Rectangle(6, 149, 787, 151);
            m_CentreRects[1] = new Rectangle(6, 311, 787, 151);

            m_FlagRect = new Rectangle(1, 539, 122, 58); //标识

            OnIniteUpRects();
            OnIniteCentre();

            //faultStr = "重联开关故障";
            return true;
        }

        /// <summary>
        ///     onIniteUpRects
        /// </summary>
        private void OnIniteUpRects()
        {
            m_UpFixedRects = new Rectangle[9];
            m_UpFixedRects[0] = new Rectangle(14, 52, 147, 25);
            m_UpFixedRects[1] = new Rectangle(14, 91, 147, 25);

            m_UpFixedRects[2] = new Rectangle(228, 44, 71, 42);
            m_UpFixedRects[3] = new Rectangle(228, 89, 71, 42);

            m_UpFixedRects[4] = new Rectangle(626, 47, 82, 26);
            m_UpFixedRects[5] = new Rectangle(626, 77, 82, 26);
            m_UpFixedRects[6] = new Rectangle(626, 107, 82, 26);
            m_UpFixedRects[7] = new Rectangle(710, 47, 82, 26);
            m_UpFixedRects[8] = new Rectangle(710, 77, 82, 26);

            m_UpFixedLineStarts = new Point[4];
            m_UpFixedLineEnds = new Point[4];

            m_UpFixedLineStarts[0] = new Point(86, 52);
            m_UpFixedLineEnds[0] = new Point(86, 77);

            m_UpFixedLineStarts[1] = new Point(86, 91);
            m_UpFixedLineEnds[1] = new Point(86, 116);

            m_UpFixedLineStarts[2] = new Point(228, 65);
            m_UpFixedLineEnds[2] = new Point(299, 65);

            m_UpFixedLineStarts[3] = new Point(228, 110);
            m_UpFixedLineEnds[3] = new Point(299, 110);

            m_UpFillRects = new Rectangle[21];
            //OCE-A区域
            m_UpFillRects[0] = new Rectangle(87, 53, 74, 24);
            //OCE-B区域
            m_UpFillRects[1] = new Rectangle(87, 92, 74, 24);
            //LTE-A区域
            m_UpFillRects[2] = new Rectangle(229, 66, 70, 20);
            //LTE-B区域
            m_UpFillRects[3] = new Rectangle(229, 111, 70, 20);
            //LTE-A 信号区域
            m_UpFillRects[4] = new Rectangle(317, 72, 12, 13);
            m_UpFillRects[5] = new Rectangle(331, 62, 12, 23);
            m_UpFillRects[6] = new Rectangle(345, 52, 12, 33);
            m_UpFillRects[7] = new Rectangle(359, 42, 12, 43);
            //LTE-B 信号区域
            m_UpFillRects[8] = new Rectangle(317, 118, 12, 13);
            m_UpFillRects[9] = new Rectangle(331, 108, 12, 23);
            m_UpFillRects[10] = new Rectangle(345, 98, 12, 33);
            m_UpFillRects[11] = new Rectangle(359, 88, 12, 43);
            //800M电台A
            m_UpFillRects[12] = new Rectangle(425, 63, 79, 20);
            //800M电台B
            m_UpFillRects[13] = new Rectangle(508, 63, 79, 20);
            //400K电台A
            m_UpFillRects[14] = new Rectangle(425, 106, 79, 20);
            //400K电台B
            m_UpFillRects[15] = new Rectangle(508, 106, 79, 20);
            //右上角五个区域
            //GDTE-A
            m_UpFillRects[16] = new Rectangle(627, 48, 81, 25);
            //GDTE-B
            m_UpFillRects[17] = new Rectangle(627, 78, 81, 25);
            //TAX通信箱
            m_UpFillRects[18] = new Rectangle(627, 108, 81, 25);
            //固态盘状态
            m_UpFillRects[19] = new Rectangle(711, 48, 81, 25);
            //BCU通信
            m_UpFillRects[20] = new Rectangle(711, 78, 81, 25);

            m_UpChangedStrs = new string[21]
            {
                "", "", "", "",
                "", "", "", "", "", "", "", "",
                "A节", "B节", "A节", "B节",
                "GDTE-A", "GDTE-B", "TAX箱通信",
                "固态盘状态", "BCU通信"
            };

            m_UpFixedStrsRects = new Rectangle[6];

            m_UpFixedStrs = new string[6] { "OCE-A", "OCE-B", "LTE-A", "LTE-B", "800M电台", "400K电台" };

            m_UpFixedStrsRects[0] = new Rectangle(14, 52, 72, 25);
            m_UpFixedStrsRects[1] = new Rectangle(14, 91, 72, 25);

            m_UpFixedStrsRects[2] = new Rectangle(228, 44, 70, 20);
            m_UpFixedStrsRects[3] = new Rectangle(228, 89, 70, 20);

            m_UpFixedStrsRects[4] = new Rectangle(423, 38, 165, 27);
            m_UpFixedStrsRects[5] = new Rectangle(423, 81, 165, 27);
        }

        /// <summary>
        ///     onIniteCentre
        /// </summary>
        private void OnIniteCentre()
        {
            m_CentreLineStarts = new Point[28];
            m_CentreLineEnds = new Point[28];

            m_CentrePXs = new int[9] { 69, 154, 268, 331, 415, 529, 591, 675, 793 };
            m_CentrePYs = new int[8] { 149, 201, 253, 300, 311, 362, 411, 462 };

            for (m_I = 0; m_I < 8; m_I++)
            {
                m_CentreLineStarts[2 * m_I] = new Point(m_CentrePXs[m_I], m_CentrePYs[0]);
                m_CentreLineEnds[2 * m_I] = new Point(m_CentrePXs[m_I], m_CentrePYs[3]);

                m_CentreLineStarts[2 * m_I + 1] = new Point(m_CentrePXs[m_I], m_CentrePYs[4]);
                m_CentreLineEnds[2 * m_I + 1] = new Point(m_CentrePXs[m_I], m_CentrePYs[7]);
            }

            for (m_I = 8; m_I < 11; m_I++)
            {
                m_CentreLineStarts[2 * m_I] = new Point(m_CentrePXs[3 * (m_I - 8)], m_CentrePYs[1]);
                m_CentreLineEnds[2 * m_I] = new Point(m_CentrePXs[3 * (m_I - 8) + 2], m_CentrePYs[1]);

                m_CentreLineStarts[2 * m_I + 1] = new Point(m_CentrePXs[3 * (m_I - 8)], m_CentrePYs[2]);
                m_CentreLineEnds[2 * m_I + 1] = new Point(m_CentrePXs[3 * (m_I - 8) + 2], m_CentrePYs[2]);
            }

            for (m_I = 11; m_I < 14; m_I++)
            {
                m_CentreLineStarts[2 * m_I] = new Point(m_CentrePXs[3 * (m_I - 11)], m_CentrePYs[5]);
                m_CentreLineEnds[2 * m_I] = new Point(m_CentrePXs[3 * (m_I - 11) + 2], m_CentrePYs[5]);

                m_CentreLineStarts[2 * m_I + 1] = new Point(m_CentrePXs[3 * (m_I - 11)], m_CentrePYs[6]);
                m_CentreLineEnds[2 * m_I + 1] = new Point(m_CentrePXs[3 * (m_I - 11) + 2], m_CentrePYs[6]);
            }

            m_CentreFixedRects = new Rectangle[24];

            m_CentreFixedStrs = new string[24]
            {
                "本车信息", "车型", "车号", "序号",
                "编组信息", "编组状态", "编组数量", "主从设置",
                "主车信息", "车型", "车号", "运行方向",
                "从1 信息", "车型", "车号", "车间距",
                "从2 信息", "车型", "车号", "车间距",
                "从3 信息", "车型", "车号", "车间距"
            };

            m_CentreFillStrs = new string[24]
            {
                "SS4B", "188", "",
                "未设", "", "未设",
                "", "", "未设",
                "", "", "",
                "", "", "",
                "", "", "",
                "LTE-A", "LTE-B",
                "LTE-A", "LTE-B",
                "LTE-A", "LTE-B"
            };

            m_CentreFixedRects = new Rectangle[24];

            m_CentreFixedRects[0] = new Rectangle(6, m_CentrePYs[0], m_CentrePXs[0] - 6, m_CentrePYs[3] - m_CentrePYs[0]);
            m_CentreFixedRects[1] = new Rectangle(m_CentrePXs[0], m_CentrePYs[0], m_CentrePXs[1] - m_CentrePXs[0],
                m_CentrePYs[1] - m_CentrePYs[0]);
            m_CentreFixedRects[2] = new Rectangle(m_CentrePXs[0], m_CentrePYs[1], m_CentrePXs[1] - m_CentrePXs[0],
                m_CentrePYs[2] - m_CentrePYs[1]);
            m_CentreFixedRects[3] = new Rectangle(m_CentrePXs[0], m_CentrePYs[2], m_CentrePXs[1] - m_CentrePXs[0],
                m_CentrePYs[3] - m_CentrePYs[2]);

            m_CentreFixedRects[4] = new Rectangle(m_CentrePXs[2], m_CentrePYs[0], m_CentrePXs[3] - m_CentrePXs[2],
                m_CentrePYs[3] - m_CentrePYs[0]);
            m_CentreFixedRects[5] = new Rectangle(m_CentrePXs[3], m_CentrePYs[0], m_CentrePXs[4] - m_CentrePXs[3],
                m_CentrePYs[1] - m_CentrePYs[0]);
            m_CentreFixedRects[6] = new Rectangle(m_CentrePXs[3], m_CentrePYs[1], m_CentrePXs[4] - m_CentrePXs[3],
                m_CentrePYs[2] - m_CentrePYs[1]);
            m_CentreFixedRects[7] = new Rectangle(m_CentrePXs[3], m_CentrePYs[2], m_CentrePXs[4] - m_CentrePXs[3],
                m_CentrePYs[3] - m_CentrePYs[2]);

            m_CentreFixedRects[8] = new Rectangle(m_CentrePXs[5], m_CentrePYs[0], m_CentrePXs[6] - m_CentrePXs[5],
                m_CentrePYs[3] - m_CentrePYs[0]);
            m_CentreFixedRects[9] = new Rectangle(m_CentrePXs[6], m_CentrePYs[0], m_CentrePXs[7] - m_CentrePXs[6],
                m_CentrePYs[1] - m_CentrePYs[0]);
            m_CentreFixedRects[10] = new Rectangle(m_CentrePXs[6], m_CentrePYs[1], m_CentrePXs[7] - m_CentrePXs[6],
                m_CentrePYs[2] - m_CentrePYs[1]);
            m_CentreFixedRects[11] = new Rectangle(m_CentrePXs[6], m_CentrePYs[2], m_CentrePXs[7] - m_CentrePXs[6],
                m_CentrePYs[3] - m_CentrePYs[2]);

            m_CentreFixedRects[12] = new Rectangle(6, m_CentrePYs[4], m_CentrePXs[0] - 6, m_CentrePYs[7] - m_CentrePYs[4]);
            m_CentreFixedRects[13] = new Rectangle(m_CentrePXs[0], m_CentrePYs[4], m_CentrePXs[1] - m_CentrePXs[0],
                m_CentrePYs[5] - m_CentrePYs[4]);
            m_CentreFixedRects[14] = new Rectangle(m_CentrePXs[0], m_CentrePYs[5], m_CentrePXs[1] - m_CentrePXs[0],
                m_CentrePYs[6] - m_CentrePYs[5]);
            m_CentreFixedRects[15] = new Rectangle(m_CentrePXs[0], m_CentrePYs[6], m_CentrePXs[1] - m_CentrePXs[0],
                m_CentrePYs[7] - m_CentrePYs[6]);

            m_CentreFixedRects[16] = new Rectangle(m_CentrePXs[2], m_CentrePYs[4], m_CentrePXs[3] - m_CentrePXs[2],
                m_CentrePYs[7] - m_CentrePYs[4]);
            m_CentreFixedRects[17] = new Rectangle(m_CentrePXs[3], m_CentrePYs[4], m_CentrePXs[4] - m_CentrePXs[3],
                m_CentrePYs[5] - m_CentrePYs[4]);
            m_CentreFixedRects[18] = new Rectangle(m_CentrePXs[3], m_CentrePYs[5], m_CentrePXs[4] - m_CentrePXs[3],
                m_CentrePYs[6] - m_CentrePYs[5]);
            m_CentreFixedRects[19] = new Rectangle(m_CentrePXs[3], m_CentrePYs[6], m_CentrePXs[4] - m_CentrePXs[3],
                m_CentrePYs[7] - m_CentrePYs[6]);

            m_CentreFixedRects[20] = new Rectangle(m_CentrePXs[5], m_CentrePYs[4], m_CentrePXs[6] - m_CentrePXs[5],
                m_CentrePYs[7] - m_CentrePYs[4]);
            m_CentreFixedRects[21] = new Rectangle(m_CentrePXs[6], m_CentrePYs[4], m_CentrePXs[7] - m_CentrePXs[6],
                m_CentrePYs[5] - m_CentrePYs[4]);
            m_CentreFixedRects[22] = new Rectangle(m_CentrePXs[6], m_CentrePYs[5], m_CentrePXs[7] - m_CentrePXs[6],
                m_CentrePYs[6] - m_CentrePYs[5]);
            m_CentreFixedRects[23] = new Rectangle(m_CentrePXs[6], m_CentrePYs[6], m_CentrePXs[7] - m_CentrePXs[6],
                m_CentrePYs[7] - m_CentrePYs[6]);

            m_CentreFillRects = new Rectangle[24];
            m_CentreFillRects[0] = new Rectangle(m_CentrePXs[1] + 5, m_CentrePYs[0] + 5, m_CentrePXs[2] - m_CentrePXs[1] - 8,
                m_CentrePYs[1] - m_CentrePYs[0] - 8);
            m_CentreFillRects[1] = new Rectangle(m_CentrePXs[1] + 5, m_CentrePYs[1] + 5, m_CentrePXs[2] - m_CentrePXs[1] - 8,
                m_CentrePYs[2] - m_CentrePYs[1] - 8);
            m_CentreFillRects[2] = new Rectangle(m_CentrePXs[1] + 5, m_CentrePYs[2] + 5, m_CentrePXs[2] - m_CentrePXs[1] - 8,
                m_CentrePYs[3] - m_CentrePYs[2] - 8);

            m_CentreFillRects[3] = new Rectangle(m_CentrePXs[4] + 5, m_CentrePYs[0] + 5, m_CentrePXs[5] - m_CentrePXs[4] - 8,
                m_CentrePYs[1] - m_CentrePYs[0] - 8);
            m_CentreFillRects[4] = new Rectangle(m_CentrePXs[4] + 5, m_CentrePYs[1] + 5, m_CentrePXs[5] - m_CentrePXs[4] - 8,
                m_CentrePYs[2] - m_CentrePYs[1] - 8);
            m_CentreFillRects[5] = new Rectangle(m_CentrePXs[4] + 5, m_CentrePYs[2] + 5, m_CentrePXs[5] - m_CentrePXs[4] - 8,
                m_CentrePYs[3] - m_CentrePYs[2] - 8);

            m_CentreFillRects[6] = new Rectangle(m_CentrePXs[7] + 5, m_CentrePYs[0] + 5, m_CentrePXs[8] - m_CentrePXs[7] - 8,
                m_CentrePYs[1] - m_CentrePYs[0] - 8);
            m_CentreFillRects[7] = new Rectangle(m_CentrePXs[7] + 5, m_CentrePYs[1] + 5, m_CentrePXs[8] - m_CentrePXs[7] - 8,
                m_CentrePYs[2] - m_CentrePYs[1] - 8);
            m_CentreFillRects[8] = new Rectangle(m_CentrePXs[7] + 5, m_CentrePYs[2] + 5, m_CentrePXs[8] - m_CentrePXs[7] - 8,
                m_CentrePYs[3] - m_CentrePYs[2] - 8);

            m_CentreFillRects[9] = new Rectangle(m_CentrePXs[1] + 5, m_CentrePYs[4] + 5, m_CentrePXs[2] - m_CentrePXs[1] - 8,
                m_CentrePYs[5] - m_CentrePYs[4] - 8);
            m_CentreFillRects[10] = new Rectangle(m_CentrePXs[1] + 5, m_CentrePYs[5] + 5, m_CentrePXs[2] - m_CentrePXs[1] - 8,
                m_CentrePYs[6] - m_CentrePYs[5] - 8);
            m_CentreFillRects[11] = new Rectangle(m_CentrePXs[1] + 5, m_CentrePYs[6] + 5, m_CentrePXs[2] - m_CentrePXs[1] - 8,
                m_CentrePYs[7] - m_CentrePYs[6] - 8);

            m_CentreFillRects[12] = new Rectangle(m_CentrePXs[4] + 5, m_CentrePYs[4] + 5, m_CentrePXs[5] - m_CentrePXs[4] - 8,
                m_CentrePYs[5] - m_CentrePYs[4] - 8);
            m_CentreFillRects[13] = new Rectangle(m_CentrePXs[4] + 5, m_CentrePYs[5] + 5, m_CentrePXs[5] - m_CentrePXs[4] - 8,
                m_CentrePYs[6] - m_CentrePYs[5] - 8);
            m_CentreFillRects[14] = new Rectangle(m_CentrePXs[4] + 5, m_CentrePYs[6] + 5, m_CentrePXs[5] - m_CentrePXs[4] - 8,
                m_CentrePYs[7] - m_CentrePYs[6] - 8);

            m_CentreFillRects[15] = new Rectangle(m_CentrePXs[7] + 5, m_CentrePYs[4] + 5, m_CentrePXs[8] - m_CentrePXs[7] - 8,
                m_CentrePYs[5] - m_CentrePYs[4] - 8);
            m_CentreFillRects[16] = new Rectangle(m_CentrePXs[7] + 5, m_CentrePYs[5] + 5, m_CentrePXs[8] - m_CentrePXs[7] - 8,
                m_CentrePYs[6] - m_CentrePYs[5] - 8);
            m_CentreFillRects[17] = new Rectangle(m_CentrePXs[7] + 5, m_CentrePYs[6] + 5, m_CentrePXs[8] - m_CentrePXs[7] - 8,
                m_CentrePYs[7] - m_CentrePYs[6] - 8);

            m_CentreFillRects[18] = new Rectangle(8, 425, m_CentrePXs[0] - 8, 16);
            m_CentreFillRects[19] = new Rectangle(8, 442, m_CentrePXs[0] - 8, 18);

            m_CentreFillRects[20] = new Rectangle(m_CentrePXs[2] + 2, 425, m_CentrePXs[0] - 8, 16);
            m_CentreFillRects[21] = new Rectangle(m_CentrePXs[2] + 2, 442, m_CentrePXs[0] - 8, 18);

            m_CentreFillRects[22] = new Rectangle(m_CentrePXs[5] + 2, 425, m_CentrePXs[0] - 8, 16);
            m_CentreFillRects[23] = new Rectangle(m_CentrePXs[5] + 2, 442, m_CentrePXs[0] - 8, 18);
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
                    append_postCmd(CmdType.ChangePage, (int)ViewState.MainInterface, 0, 0);
                    break;

                case (int)ViewState.WiBtnShowStatus:
                    append_postCmd(CmdType.ChangePage, (int)ViewState.WiBtnShowStatus, 0, 0);
                    break;

                case (int)ViewState.WiBtnShowStatus + 3:
                    append_postCmd(CmdType.ChangePage, 710, 0, 0);
                    break;

                case (int)ViewState.WiBtnShowStatus + 5:
                    append_postCmd(CmdType.ChangePage, (int)(ViewState.WiBtnBzSetting), 0, 0);
                    break;

                case (int)ViewState.WiBtnShowStatus + 6:
                    append_postCmd(CmdType.ChangePage, (int)(ViewState.WiBtnRemoveBz), 0, 0);
                    break;

                case (int)ViewState.WiBtnShowStatus + 7:
                    append_postCmd(CmdType.ChangePage, (int)(ViewState.WiBtnBzDiagnoseInfo), 0, 0);
                    break;

                case 71002:
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.OutB编组设置), 1, 0);
                    m_BztsFlag = true;
                    m_Timer.Change(500, int.MaxValue);
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
            //按钮响应
            ButtonResponse(dcGs);
            //页面框架
            PaintFram(dcGs);
            //区域颜色
            PaintUpFillRects(dcGs);
            //固定字符
            PaintCentreRects(dcGs);
            //接口类容变更
            PaintCentenString(dcGs);
            //故障
            PaintFault(dcGs);

            base.paint(dcGs);
        }

        private void PaintFram(Graphics dcGs)
        {
            dcGs.DrawImage(ImageResource.flag2, m_FlagRect);
            m_BtnsDownTabView.ForEach(a => a.Paint(dcGs));

            dcGs.DrawRectangles(m_WhiteLinePen, m_UpRects);

            dcGs.DrawRectangles(m_WhiteBigLinePen, m_CentreRects);

            dcGs.DrawString((m_CurTStr + m_TerminalStrs[0]), m_ChineseFont, Brushs.WhiteBrush, m_CurrentTerminalRect,
                FontInfo.SfCc);

            m_CurrentTimeStr = DateTime.Now.ToString(m_TimeShowFormat);
            dcGs.DrawString(m_CurrentTimeStr, m_DigitFont, Brushs.WhiteBrush, m_CurTimeRect, FontInfo.SfCc);

            dcGs.DrawRectangles(m_WhiteLinePen, m_UpFixedRects);

            for (m_I = 0; m_I < 4; m_I++)
            {
                dcGs.DrawLine(m_WhiteLinePen, m_UpFixedLineStarts[m_I], m_UpFixedLineEnds[m_I]);
            }

            for (m_I = 0; m_I < 28; m_I++)
            {
                dcGs.DrawLine(m_WhiteBigLinePen, m_CentreLineStarts[m_I], m_CentreLineEnds[m_I]);
            }
        }

        private void PaintFault(Graphics dcGs)
        {
            if (!string.IsNullOrEmpty(m_FaultStr))
            {
                dcGs.FillRectangle(Brushs.RedBrush, m_FaultRect);
            }
            dcGs.DrawString(m_FaultStr, m_ChineseFont, Brushs.WhiteBrush, m_FaultRect, FontInfo.SfCc);
            //刷新故障
            V708WIBZInfoTS.Text.ForEach(f => f.Refresh());
            if (V708WIBZInfoTS.Fault.Count != 0)
            {
                if (Index % 10 != 0)
                {
                    Index++;
                    return;
                }
                if (Index / 10 == V708WIBZInfoTS.Fault.Count)
                {
                    Index = 0;
                }

                FaultInfo info;
                try
                {
                    info = V708WIBZInfoTS.Fault[Index / 10];
                }
                catch
                {
                    info = V708WIBZInfoTS.Fault[0];
                    Index = 0;
                }
                Index++;
                m_FaultStr = info.Title;
            }
            else
            {
                m_FaultStr = string.Empty;
                Index = -1;
            }
        }

        private int Index = -1;

        private void ButtonResponse(Graphics dcGs)
        {
            if (GetInBoolValue(LogicNames[9]))
            {
                m_BtnFlags[9] = true;
                // append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage,(int)(ViewState.MainInterface), 0, 0);
            }
            else if ((m_BtnFlags[9]))
            {
                append_postCmd(CmdType.ChangePage, (int)(ViewState.MainInterface), 0, 0);
                m_BtnFlags[9] = false;
            }

            if (GetInBoolValue(LogicNames[0]))
            {
                m_BtnFlags[0] = true;
                //append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.WI_BTN_ShowStatus), 0, 0);
            }
            else if (m_BtnFlags[0])
            {
                m_BtnFlags[0] = false;
                append_postCmd(CmdType.ChangePage, (int)(ViewState.WiBtnShowStatus), 0, 0);
            }

            if (GetInBoolValue(LogicNames[1]))
            {
                m_BztsFlag = true;
                append_postCmd(CmdType.SetBoolValue, GetInBoolIndex(OutBoolKeys.OutB编组设置), 1, 0);
                m_Timer.Change(500, int.MaxValue);
            }
            if (GetInBoolValue(LogicNames[10]))
            {
                dcGs.DrawString(m_ConnectionOk, m_ChineseFont, Brushs.WhiteBrush, m_BztsRect, FontInfo.SfCc);
                m_BztsFlag = false;
            }
            if (m_BztsFlag && !GetInBoolValue(LogicNames[10]))
            {
                dcGs.DrawString(m_BztsStr, m_ChineseFont, Brushs.WhiteBrush, m_BztsRect, FontInfo.SfCc);
            }

            if (GetInBoolValue(LogicNames[3]))
            {
                m_BtnFlags[3] = true;
            }
            else if (m_BtnFlags[3])
            {
                m_BtnFlags[3] = false;
                append_postCmd(CmdType.ChangePage, 710, 0, 0);
            }

            if (GetInBoolValue(LogicNames[6]))
            {
                m_BtnFlags[6] = true;
            }
            else if (m_BtnFlags[6])
            {
                m_BtnFlags[6] = false;
                append_postCmd(CmdType.ChangePage, (int)(ViewState.WiBtnRemoveBz), 0, 0);
            }

            if (GetInBoolValue(LogicNames[5]))
            {
                m_BtnFlags[5] = true;
            }
            else if (m_BtnFlags[5])
            {
                m_BtnFlags[5] = false;
                append_postCmd(CmdType.ChangePage, (int)(ViewState.WiBtnBzSetting), 0, 0);
            }

            if (GetInBoolValue(LogicNames[7]))
            {
                m_BtnFlags[7] = true;
                //append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.WI_BTN_BZDiagnoseInfo), 0, 0);
            }
            else if (m_BtnFlags[7])
            {
                m_BtnFlags[7] = false;
                append_postCmd(CmdType.ChangePage, (int)(ViewState.WiBtnBzDiagnoseInfo), 0, 0);
            }
        }

        /// <summary>
        ///     paintUpFillRects
        /// </summary>
        /// <param name="dcGs"></param>
        private void PaintUpFillRects(Graphics dcGs)
        {
            //OCE-A 状态
            if (GetInBoolValue(InBoolKeys.InBOCEA红))
            {
                dcGs.FillRectangle(Brushs.RedBrush, m_UpFillRects[0]);
            }
            else if (GetInBoolValue(InBoolKeys.InBOCEA绿))
            {
                dcGs.FillRectangle(Brushs.GreenBrush, m_UpFillRects[0]);
            }
            else
            {
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[0]);
            }
            dcGs.DrawString(GetInFloatValue(InFloatKeys.InFOCEA).ToString("F0"), m_ChineseFont, Brushs.BlackBrush, m_UpFillRects[0], FontInfo.SfCc);
            //OCE-B 状态
            if (GetInBoolValue(InBoolKeys.InBOCEB红))
            {
                dcGs.FillRectangle(Brushs.RedBrush, m_UpFillRects[1]);
            }
            else if (GetInBoolValue(InBoolKeys.InBOCEB绿))
            {
                dcGs.FillRectangle(Brushs.GreenBrush, m_UpFillRects[1]);
            }
            else
            {
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[1]);
            }
            dcGs.DrawString(GetInFloatValue(InFloatKeys.InFOCEB).ToString("F0"), m_ChineseFont, Brushs.BlackBrush, m_UpFillRects[1], FontInfo.SfCc);
            //LTE-A 状态
            if (GetInBoolValue(InBoolKeys.InBLTEA红))
            {
                dcGs.FillRectangle(Brushs.RedBrush, m_UpFillRects[2]);
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[4]);
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[5]);
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[6]);
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[7]);
                dcGs.DrawString("已脱网", m_ChineseFont, Brushs.BlackBrush, m_UpFillRects[2], FontInfo.SfCc);
            }
            else if (GetInBoolValue(InBoolKeys.InBLTEA绿))
            {
                dcGs.FillRectangle(Brushs.GreenBrush, m_UpFillRects[2]);
                dcGs.FillRectangle(Brushs.GreenBrush, m_UpFillRects[4]);
                dcGs.FillRectangle(Brushs.GreenBrush, m_UpFillRects[5]);
                dcGs.FillRectangle(Brushs.GreenBrush, m_UpFillRects[6]);
                dcGs.FillRectangle(Brushs.GreenBrush, m_UpFillRects[7]);
                dcGs.DrawString("已注册", m_ChineseFont, Brushs.BlackBrush, m_UpFillRects[2], FontInfo.SfCc);
            }
            else
            {
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[2]);
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[4]);
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[5]);
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[6]);
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[7]);
            }
            //LTE-B
            if (GetInBoolValue(InBoolKeys.InBLTEB红))
            {
                dcGs.FillRectangle(Brushs.RedBrush, m_UpFillRects[3]);
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[8]);
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[9]);
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[10]);
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[11]);
                dcGs.DrawString("已脱网", m_ChineseFont, Brushs.BlackBrush, m_UpFillRects[3], FontInfo.SfCc);
            }
            else if (GetInBoolValue(InBoolKeys.InBLTEB绿))
            {
                dcGs.FillRectangle(Brushs.GreenBrush, m_UpFillRects[3]);
                dcGs.FillRectangle(Brushs.GreenBrush, m_UpFillRects[8]);
                dcGs.FillRectangle(Brushs.GreenBrush, m_UpFillRects[9]);
                dcGs.FillRectangle(Brushs.GreenBrush, m_UpFillRects[10]);
                dcGs.FillRectangle(Brushs.GreenBrush, m_UpFillRects[11]);
                dcGs.DrawString("已注册", m_ChineseFont, Brushs.BlackBrush, m_UpFillRects[3], FontInfo.SfCc);
            }
            else
            {
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[3]);
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[8]);
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[9]);
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[10]);
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[11]);
            }
            //800M-A
            if (GetInBoolValue(InBoolKeys.InB800M电台A红))
            {
                dcGs.FillRectangle(Brushs.RedBrush, m_UpFillRects[12]);
            }
            else if (GetInBoolValue(InBoolKeys.InB800M电台A绿))
            {
                dcGs.FillRectangle(Brushs.GreenBrush, m_UpFillRects[12]);
            }
            else
            {
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[12]);
            }
            //800M-B
            if (GetInBoolValue(InBoolKeys.InB800M电台B红))
            {
                dcGs.FillRectangle(Brushs.RedBrush, m_UpFillRects[13]);
            }
            else if (GetInBoolValue(InBoolKeys.InB800M电台B绿))
            {
                dcGs.FillRectangle(Brushs.GreenBrush, m_UpFillRects[13]);
            }
            else
            {
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[13]);
            }
            //400K-A
            if (GetInBoolValue(InBoolKeys.InB400k电台A红))
            {
                dcGs.FillRectangle(Brushs.RedBrush, m_UpFillRects[14]);
            }
            else if (GetInBoolValue(InBoolKeys.InB400k电台A绿))
            {
                dcGs.FillRectangle(Brushs.GreenBrush, m_UpFillRects[14]);
            }
            else
            {
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[14]);
            }
            //400K-B
            if (GetInBoolValue(InBoolKeys.InB400k电台B红))
            {
                dcGs.FillRectangle(Brushs.RedBrush, m_UpFillRects[15]);
            }
            else if (GetInBoolValue(InBoolKeys.InB400k电台B绿))
            {
                dcGs.FillRectangle(Brushs.GreenBrush, m_UpFillRects[15]);
            }
            else
            {
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[15]);
            }
            //GDTE-A
            if (GetInBoolValue(InBoolKeys.InBGDTEA红))
            {
                dcGs.FillRectangle(Brushs.RedBrush, m_UpFillRects[16]);
            }
            else if (GetInBoolValue(InBoolKeys.InBGDTEA绿))
            {
                dcGs.FillRectangle(Brushs.GreenBrush, m_UpFillRects[16]);
            }
            else
            {
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[16]);
            }
            //GDTE-B
            if (GetInBoolValue(InBoolKeys.InBGDTEB红))
            {
                dcGs.FillRectangle(Brushs.RedBrush, m_UpFillRects[17]);
            }
            else if (GetInBoolValue(InBoolKeys.InBGDTEB绿))
            {
                dcGs.FillRectangle(Brushs.GreenBrush, m_UpFillRects[17]);
            }
            else
            {
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[17]);
            }
            //TAX箱
            if (GetInBoolValue(InBoolKeys.InBTAX箱通红))
            {
                dcGs.FillRectangle(Brushs.RedBrush, m_UpFillRects[18]);
            }
            else if (GetInBoolValue(InBoolKeys.InBTAX箱通绿))
            {
                dcGs.FillRectangle(Brushs.GreenBrush, m_UpFillRects[18]);
            }
            else
            {
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[18]);
            }
            //固态盘
            if (GetInBoolValue(InBoolKeys.InB固态盘红))
            {
                dcGs.FillRectangle(Brushs.RedBrush, m_UpFillRects[19]);
            }
            else if (GetInBoolValue(InBoolKeys.InB固态盘绿))
            {
                dcGs.FillRectangle(Brushs.GreenBrush, m_UpFillRects[19]);
            }
            else
            {
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[19]);
            }
            //BCU通信
            if (GetInBoolValue(InBoolKeys.InBBCU通信红))
            {
                dcGs.FillRectangle(Brushs.RedBrush, m_UpFillRects[20]);
            }
            else if (GetInBoolValue(InBoolKeys.InBBCU通信绿))
            {
                dcGs.FillRectangle(Brushs.GreenBrush, m_UpFillRects[20]);
            }
            else
            {
                dcGs.FillRectangle(Brushs.WhiteBrush, m_UpFillRects[20]);
            }

            for (m_I = 0; m_I < 21; m_I++)
            {
                dcGs.DrawString(m_UpChangedStrs[m_I], m_ChineseFont, Brushs.BlackBrush, m_UpFillRects[m_I], FontInfo.SfCc);
            }

            for (m_I = 0; m_I < 6; m_I++)
            {
                dcGs.DrawString(m_UpFixedStrs[m_I], m_ChineseFont, Brushs.WhiteBrush, m_UpFixedStrsRects[m_I], FontInfo.SfCc);
            }
        }

        /// <summary>
        ///     paintCentreRects
        /// </summary>
        /// <param name="dcGs"></param>
        private void PaintCentreRects(Graphics dcGs)
        {
            foreach (var rect in m_CentreFillRects.Take(18))
            {
                dcGs.FillRectangle(Brushs.WhiteBrush, rect);
            }
            for (int i = 18; i < m_CentreFillRects.Length; i++)
            {
                dcGs.FillRectangle(GetInBoolValue(OtherCarLteLogic[i - 18]) ? Brushs.GreenBrush : Brushs.WhiteBrush, m_CentreFillRects[i]);
            }
            //dcGs.FillRectangles(Brushs.WhiteBrush, m_CentreFillRects);

            for (m_I = 18; m_I < 24; m_I++)
            {
                dcGs.DrawString(m_CentreFillStrs[m_I], m_ChineseFont, Brushs.BlackBrush, m_CentreFillRects[m_I], FontInfo.SfCc);
            }

            for (m_I = 0; m_I < 24; m_I++)
            {
                dcGs.DrawString(m_CentreFixedStrs[m_I], m_ChineseFont, Brushs.WhiteBrush, m_CentreFixedRects[m_I], FontInfo.SfCc);
            }
        }

        private void PaintCentenString(Graphics g)
        {
            var value = GetInFloatValue(InFloatKeys.InF本车信息车型).ConvertToInt();
            DrawString(g, EnumUtil.GetDescription((CarType)value).FirstOrDefault(), m_CentreFillRects[0]);
            value = GetInFloatValue(InFloatKeys.InF本车信息车号).ConvertToInt();
            DrawString(g, value.ToString(), m_CentreFillRects[1]);
            value = GetInFloatValue(InFloatKeys.InF本车信息序号).ConvertToInt();
            DrawString(g, EnumUtil.GetDescription((CarNum)value).FirstOrDefault(), m_CentreFillRects[2]);
            value = GetInFloatValue(InFloatKeys.InF编组信息编组状态).ConvertToInt();
            DrawString(g, EnumUtil.GetDescription((Marshalling)value).FirstOrDefault(), m_CentreFillRects[3]);
            value = GetInFloatValue(InFloatKeys.InF编组信息编组数量).ConvertToInt();
            DrawString(g, value.ToString(), m_CentreFillRects[4]);
            value = GetInFloatValue(InFloatKeys.InF编组信息主从设置).ConvertToInt();
            DrawString(g, EnumUtil.GetDescription((CarNum)value).FirstOrDefault(), m_CentreFillRects[5]);
            value = GetInFloatValue(InFloatKeys.InF主车信息车型).ConvertToInt();
            DrawString(g, EnumUtil.GetDescription((CarType)value).FirstOrDefault(), m_CentreFillRects[6]);
            value = GetInFloatValue(InFloatKeys.InF主车信息车号).ConvertToInt();
            DrawString(g, value.ConvertToString(), m_CentreFillRects[7]);
            value = GetInFloatValue(InFloatKeys.InF主车信息运行方向).ConvertToInt();
            DrawString(g, EnumUtil.GetDescription((Direction)value).FirstOrDefault(), m_CentreFillRects[8]);
            value = GetInFloatValue(InFloatKeys.InF从1信息车型).ConvertToInt();
            DrawString(g, EnumUtil.GetDescription((CarType)value).FirstOrDefault(), m_CentreFillRects[9]);
            value = GetInFloatValue(InFloatKeys.InF从1信息车号).ConvertToInt();
            DrawString(g, value.ConvertToString(), m_CentreFillRects[10]);
            value = GetInFloatValue(InFloatKeys.InF从1信息车间距).ConvertToInt();
            DrawString(g, value.ConvertToString(), m_CentreFillRects[11]);
            value = GetInFloatValue(InFloatKeys.InF从2信息车型).ConvertToInt();
            DrawString(g, EnumUtil.GetDescription((CarType)value).FirstOrDefault(), m_CentreFillRects[12]);
            value = GetInFloatValue(InFloatKeys.InF从2信息车号).ConvertToInt();
            DrawString(g, value.ConvertToString(), m_CentreFillRects[13]);
            value = GetInFloatValue(InFloatKeys.InF从2信息车间距).ConvertToInt();
            DrawString(g, value.ConvertToString(), m_CentreFillRects[14]);
            value = GetInFloatValue(InFloatKeys.InF从3信息车型).ConvertToInt();
            DrawString(g, EnumUtil.GetDescription((CarType)value).FirstOrDefault(), m_CentreFillRects[15]);
            value = GetInFloatValue(InFloatKeys.InF从3信息车号).ConvertToInt();
            DrawString(g, value.ConvertToString(), m_CentreFillRects[16]);
            value = GetInFloatValue(InFloatKeys.InF从3信息车间距).ConvertToInt();
            DrawString(g, value.ConvertToString(), m_CentreFillRects[17]);
        }

        private void DrawString(Graphics g, string str, Rectangle rec)
        {
            g.DrawString(str, m_ChineseFont, Brushs.BlackBrush, rec, FontInfo.SfCc);
        }
    }

    public static class FloatExtend
    {
        public static int ConvertToInt(this float value)
        {
            return (int)value;
        }

        public static string ConvertToString(this int value)
        {
            if (value == 0)
            {
                return "--";
            }
            return value.ToString();
        }

        public static string ConvertToString(this float value, string format = null, float fValue = 0)
        {
            if (value.ConvertToInt() < fValue)
            {
                return "--";
            }
            return format == null ? value.ToString("F0") : value.ToString(format);
        }

    }

    public enum Direction
    {
        [Description("未知")]
        Unknow = 0,

        [Description("上行")]
        Up = 1,

        [Description("下行")]
        Down = 2,
    }

    public enum Marshalling
    {
        [Description("未知")]
        未知,

        [Description("编组")]
        编组,

        [Description("未编组")]
        未编组
    }

    public enum CarNum
    {
        [Description("主车")]
        主车,

        [Description("从1")]
        从1,

        [Description("从2")]
        从2,

        [Description("从3")]
        从3,

        [Description("从车")]
        从车,
    }

    public enum CarType
    {
        [Description("--")]
        Unknow = 0,

        [Description("SS4B")]
        SS4B = 1,

        [Description("SS4G")]
        SS4G = 2,

        [Description("SH8")]
        SH8 = 3,
    }
}