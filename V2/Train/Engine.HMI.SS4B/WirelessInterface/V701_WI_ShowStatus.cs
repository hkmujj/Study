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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using CommonUtil.Util;

namespace SS4B_TMS.WirelessInterface
{
    /// <summary>
    ///     功能描述：视图7--No.0-主界面
    ///     创建人：lih
    ///     创建时间：2015-8-26
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V701WIShowStatus : baseClass
    {
        private readonly string[] m_LogicNames2 = new[]
        {
                InBoolKeys.InB主车分相准备,
                InBoolKeys.InB主车分相通过,
                InBoolKeys.InB从1分相准备,
                InBoolKeys.InB从1分相通过,
                InBoolKeys.InB从2分相准备,
                InBoolKeys.InB从2分相通过,
                InBoolKeys.InB从3分相准备,
                InBoolKeys.InB从3分相通过,
            };

        private readonly string[] m_LogicName3 = new[]
           {
                InBoolKeys.InB主车机车工况MMI蓄电池合,
                InBoolKeys.InB主车机车工况MMI主断合,
                InBoolKeys.InB主车机车工况MMI牵引,
                InBoolKeys.InB主车机车工况MMI制动,
                InBoolKeys.InB从1机车工况MMI蓄电池合,
                InBoolKeys.InB从1机车工况MMI主断合,
                InBoolKeys.InB从1机车工况MMI牵引,
                InBoolKeys.InB从1机车工况MMI制动,
                InBoolKeys.InB从2机车工况MMI蓄电池合,
                InBoolKeys.InB从2机车工况MMI主断合,
                InBoolKeys.InB从2机车工况MMI牵引,
                InBoolKeys.InB从2机车工况MMI制动,
                InBoolKeys.InB从3机车工况MMI蓄电池合,
                InBoolKeys.InB从3机车工况MMI主断合,
                InBoolKeys.InB从3机车工况MMI牵引,
                InBoolKeys.InB从3机车工况MMI制动,
            };

        private readonly string[] m_LogicName4 = new[]
          {
                InBoolKeys.InB主车通讯状态正常,
                InBoolKeys.InB从1通讯状态正常,
                InBoolKeys.InB从2通讯状态正常,
                InBoolKeys.InB从3通讯状态正常,
            };

        private readonly string[] m_LogicNames5 = new[]
        {
                InBoolKeys.InB主车操作端,
                InBoolKeys.InB从1操作端,
                InBoolKeys.InB从2操作端,
                InBoolKeys.InB从3操作端,
            };

        private readonly string[] m_LogicName6 = new[]
           {
                InBoolKeys.InB主车预备,
                InBoolKeys.InB从1预备,
                InBoolKeys.InB从2预备,
                InBoolKeys.InB从3预备,
            };

        private readonly string[] m_LogicName7 = new[]
          {
                InBoolKeys.InB主车零位,
                InBoolKeys.InB从1零位,
                InBoolKeys.InB从2零位,
                InBoolKeys.InB从3零位,
            };

        private readonly string[] m_LogicName8 = new string[]
          {
                InBoolKeys.InB主车A节受电弓,
                InBoolKeys.InB从1A节受电弓,
                InBoolKeys.InB从2A节受电弓,
                InBoolKeys.InB从3A节受电弓,
          };

        private readonly string[] m_LogicName9 = new string[]
           {
                InBoolKeys.InB主车B节受电弓,
                InBoolKeys.InB从1B节受电弓,
                InBoolKeys.InB从2B节受电弓,
                InBoolKeys.InB从3B节受电弓,
           };

        private readonly string[] m_LogicName11 = new[]
        {
                InBoolKeys.InB主车B车主段,
                InBoolKeys.InB从1B车主段,
                InBoolKeys.InB从2B车主段,
                InBoolKeys.InB从3B车主段,
            };

        private readonly string[] m_LogicName10 = new[]
           {
                InBoolKeys.InB主车A车主断,
                InBoolKeys.InB从1车主断,
                InBoolKeys.InB从2车主断,
                InBoolKeys.InB从3车主断,
            };

        private readonly string[] m_LogicName12 = new[]
           {
                InBoolKeys.InB主车故障标示,
                InBoolKeys.InB从1故障标示,
                InBoolKeys.InB从2故障标示,
                InBoolKeys.InB从3故障标示,
            };

        private readonly string[] m_LogicName1 = new[]
        {
                InFloatKeys.InF主车网压,
                InFloatKeys.InF主车设定级位,
                InFloatKeys.InF主车运行速度,
                InFloatKeys.InF主车电机电流,
                InFloatKeys.InF主车电机电压,
                InFloatKeys.InF主车励磁电流,
                InFloatKeys.InF主车控制回路电压,
                InFloatKeys.InF主车总风缸,
                InFloatKeys.InF主车大闸目标,
                InFloatKeys.InF主车均衡缸,
                InFloatKeys.InF主车列车管,
                InFloatKeys.InF主车流量计,
                InFloatKeys.InF主车闸缸,
                InFloatKeys.InF主车列尾压力,
                InFloatKeys.InF主车磁削等级,
                InFloatKeys.InF主车故障等级,
                InFloatKeys.InF从1网压,
                InFloatKeys.InF从1设定级位,
                InFloatKeys.InF从1运行速度,
                InFloatKeys.InF从1电机电流,
                InFloatKeys.InF从1电机电压,
                InFloatKeys.InF从1励磁电流,
                InFloatKeys.InF从1控制回路电压,
                InFloatKeys.InF从1总风缸,
                InFloatKeys.InF从1大闸目标,
                InFloatKeys.InF从1均衡缸,
                InFloatKeys.InF从1列车管,
                InFloatKeys.InF从1流量计,
                InFloatKeys.InF从1闸缸,
                InFloatKeys.InF从1列尾压力,
                InFloatKeys.InF从1磁削等级,
                InFloatKeys.InF从1故障等级,
                InFloatKeys.InF从2网压,
                InFloatKeys.InF从2设定级位,
                InFloatKeys.InF从2运行速度,
                InFloatKeys.InF从2电机电流,
                InFloatKeys.InF从2电机电压,
                InFloatKeys.InF从2励磁电流,
                InFloatKeys.InF从2控制回路电压,
                InFloatKeys.InF从2总风缸,
                InFloatKeys.InF从2大闸目标,
                InFloatKeys.InF从2均衡缸,
                InFloatKeys.InF从2列车管,
                InFloatKeys.InF从2流量计,
                InFloatKeys.InF从2闸缸,
                InFloatKeys.InF从2列尾压力,
                InFloatKeys.InF从2磁削等级,
                InFloatKeys.InF从2故障等级,
                InFloatKeys.InF从3网压,
                InFloatKeys.InF从3设定级位,
                InFloatKeys.InF从3运行速度,
                InFloatKeys.InF从3电机电流,
                InFloatKeys.InF从3电机电压,
                InFloatKeys.InF从3励磁电流,
                InFloatKeys.InF从3控制回路电压,
                InFloatKeys.InF从3总风缸,
                InFloatKeys.InF从3大闸目标,
                InFloatKeys.InF从3均衡缸,
                InFloatKeys.InF从3列车管,
                InFloatKeys.InF从3流量计,
                InFloatKeys.InF从3闸缸,
                InFloatKeys.InF从3列尾压力,
                InFloatKeys.InF从3磁削等级,
                InFloatKeys.InF从3故障等级,
            };

        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);

        private readonly Pen m_WhiteLinePen = new Pen(new SolidBrush(Color.White), 1.6f);
        private readonly Pen m_WhiteBigLinePen = new Pen(new SolidBrush(Color.White), 2.0f);

        private readonly Font m_ChineseFont = new Font("宋体", 14);

        private readonly Font m_ChineseSmallFont = new Font("宋体", 12);
        private readonly Font m_ChineseFont10 = new Font("宋体", 10);

        private int m_I;

        private readonly List<Button> m_BtnsDownTabView = new List<Button>(); //按钮列表

        private ButtonStyle m_NormalBs;
        private Rectangle m_FlagRect;

        private Rectangle[] m_FrameRects;

        private int[] m_HpXs;
        private int[] m_HpYs;

        private int[] m_VpXs;
        private int[] m_VpYs;

        private Point[] m_HpStarts;
        private Point[] m_HpEnds;

        private Point[] m_VpStarts;
        private Point[] m_VpEnds;

        private Rectangle[] m_FixedRects;
        private string[] m_FixedStrs;

        private int m_EllipseWidth = 18;
        private int m_EllipseHeight = 18;

        private readonly SolidBrush m_GoldBrush = new SolidBrush(Color.FromArgb(255, 201, 14));
        private readonly SolidBrush m_BlueBrush = new SolidBrush(Color.FromArgb(0, 255, 255));

        private string[] m_MainLocoWc;
        private string[] m_SubLoco1Wc;
        private string[] m_SubLoco2Wc;
        private string[] m_SubLoco3Wc;

        private Rectangle[] m_LocoWorkConRects; //机车工况

        private Rectangle[] m_CommunicationStatusRects; //通讯状态
        private Rectangle[] m_OperateTerminalRects; //操作端
        private Rectangle[] m_PrepareRects; //预备
        private Rectangle[] m_ZeroRects; //零位
        private Rectangle[] m_ApantographRects; //A受电弓
        private Rectangle[] m_BpantographRects; //B受电弓
        private Rectangle[] m_ALineCircuitBreakerRects; //A主断
        private Rectangle[] m_BLineCircuitBreakerRects; //B主断

        private Rectangle[] m_DegaussingLevelRects; //削磁等级
        private Rectangle[] m_ParvafaciesStatusRects; //分相状态
        private Rectangle[] m_BrakeStatusRects; //制动状态
        private Rectangle[] m_FaultMarkingRects; //故障标示
        private Rectangle m_FaultDisplay; //故障描述

        private Rectangle m_FillFaultDisplay; //故障描述

        private Rectangle[] m_PressureRects; //网压
        private Rectangle[] m_SettingLevelRects; //设定级位
        private Rectangle[] m_RunSpeedRects; //运行速度
        private Rectangle[] m_CurrentRects; //电机电流
        private Rectangle[] m_VoltageRects; //电机电压
        private Rectangle[] m_ExcitationRects; //励磁电流
        private Rectangle[] m_ControlLoopRects; //控制回路
        private Rectangle[] m_MainAirReservoirRects; //总风缸
        private Rectangle[] m_DazhaDestRects; //大闸目标
        private Rectangle[] m_BalancedCylinderRects; //均衡缸
        private Rectangle[] m_TrainPipeRects; //列车管
        private Rectangle[] m_FlowInstrumentRects; //流量计
        private Rectangle[] m_BrakeCylinderRects; //闸缸

        private Rectangle m_TailPressure; //列尾压力

        private string[] m_DegaussingLevelStrs; //削磁等级
        private string[] m_ParvafaciesStatusStrs; //分相状态
        private string[] m_BrakeStatusStrs; //制动状态
        private string m_FaultDisplayStr; //故障描述
        private string[] m_PressureStrs; //网压
        private string[] m_SettingLevelStrs; //设定级位
        private string[] m_RunSpeedStrs; //运行速度
        private string[] m_CurrentStrs; //电机电流
        private string[] m_VoltageStrs; //电机电压
        private string[] m_ExcitationStrs; //励磁电流
        private string[] m_ControlLoopStrs; //控制回路
        private string[] m_MainAirReservoirStrs; //总风缸
        private string[] m_DazhaDestStrs; //大闸目标
        private string[] m_BalancedCylinderStrs; //均衡缸
        private string[] m_TrainPipeStrs; //列车管
        private string[] m_FlowInstrumentStrs; //流量计
        private string[] m_BrakeCylinderStrs; //闸缸

        private string m_TailPressureStrs; //列尾压力

        private Rectangle[] m_TitleStrRects;
        private string[] m_TitleStrs;

        private Rectangle[] m_FillRects;

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
            var strsBtnTabView = new string[10] { "制动 显示", "", "版本 信息", "故障 指示灯", "IO", "", "", "", "", "返回" };
            m_BtnFlags = new bool[10]
            {
                false, false, false, false, false,
                false, false, false, false, false
            };

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

            m_FrameRects = new Rectangle[4];
            m_FrameRects[0] = new Rectangle(6, 76, 79, 462);
            m_FrameRects[1] = new Rectangle(85, 43, 316, 495);
            m_FrameRects[2] = new Rectangle(401, 76, 79, 462);
            m_FrameRects[3] = new Rectangle(480, 43, 316, 495);

            m_HpXs = new int[2] { 6, 796 };
            m_HpYs = new int[14];
            for (m_I = 0; m_I < 14; m_I++)
            {
                m_HpYs[m_I] = 76 + 33 * m_I;
            }

            m_VpYs = new int[2] { 43, 505 };
            m_VpXs = new int[6] { 164, 243, 322, 559, 639, 717 };

            m_HpStarts = new Point[14];
            m_HpEnds = new Point[14];

            m_VpStarts = new Point[6];
            m_VpEnds = new Point[6];

            for (m_I = 0; m_I < 14; m_I++)
            {
                m_HpStarts[m_I] = new Point(m_HpXs[0], m_HpYs[m_I]);
                m_HpEnds[m_I] = new Point(m_HpXs[1], m_HpYs[m_I]);
            }

            for (m_I = 0; m_I < 6; m_I++)
            {
                m_VpStarts[m_I] = new Point(m_VpXs[m_I], m_VpYs[0]);
                m_VpEnds[m_I] = new Point(m_VpXs[m_I], m_VpYs[1]);
            }

            OnIniteFixed();
            OnIniteChanged();

            m_FillRects = new Rectangle[14];
            m_FillRects[0] = new Rectangle(76, 26, 7, 5);
            m_FillRects[1] = new Rectangle(84, 21, 7, 10);
            m_FillRects[2] = new Rectangle(92, 16, 7, 15);
            m_FillRects[3] = new Rectangle(100, 11, 7, 20);

            m_FillRects[4] = new Rectangle(112, 12, 18, 18);

            m_FillRects[5] = new Rectangle(210, 26, 7, 5);
            m_FillRects[6] = new Rectangle(218, 21, 7, 10);
            m_FillRects[7] = new Rectangle(226, 16, 7, 15);
            m_FillRects[8] = new Rectangle(234, 11, 7, 20);

            m_FillRects[9] = new Rectangle(246, 12, 18, 18);

            m_FillRects[10] = new Rectangle(376, 12, 37, 20);
            m_FillRects[11] = new Rectangle(414, 12, 37, 20);

            m_FillRects[12] = new Rectangle(536, 12, 37, 20);
            m_FillRects[13] = new Rectangle(574, 12, 37, 20);

            return true;
        }
        //分相距离区域
        private Rectangle SplitRectangle = new Rectangle(6, 43, 79, 33);
        /// <summary>
        ///     onIniteFixed
        /// </summary>
        private void OnIniteFixed()
        {
            m_FixedStrs = new string[36]
            {
                "机车工况", "通讯状态", "操作端", "预备", "零位", "A受电弓", "B受电弓", "A节主断", "B节主断", "削磁等级", "分相状态", "制动状态", "故障标示",
                "故障描述",
                "主车", "从1", "从2", "从3",
                "网压KV", "设定级位", "运行速度", "电机电流", "电机电压", "励磁电流", "控制回路", "总风缸", "大闸目标", "均衡缸", "列车管", "流量计", "闸缸", "列尾压力",
                "主车", "从1", "从2", "从3"
            };
            m_FixedRects = new Rectangle[36];

            for (m_I = 0; m_I < 14; m_I++)
            {
                m_FixedRects[m_I] = new Rectangle(6, 76 + 33 * m_I, 79, 33);
                m_FixedRects[18 + m_I] = new Rectangle(401, 76 + 33 * m_I, 79, 33);
            }

            for (m_I = 0; m_I < 4; m_I++)
            {
                m_FixedRects[14 + m_I] = new Rectangle(85 + 79 * m_I, 43, 79, 33);
                m_FixedRects[32 + m_I] = new Rectangle(480 + 79 * m_I, 43, 79, 33);
            }
        }

        /// <summary>
        ///     onIniteChanged
        /// </summary>
        private void OnIniteChanged()
        {
            m_LocoWorkConRects = new Rectangle[4];
            m_MainLocoWc = new string[5] { "蓄电池合", "主断合", "牵引", "制动", "未知" };
            m_SubLoco1Wc = new string[5] { "蓄电池合", "主断合", "牵引", "制动", "未知" };
            m_SubLoco2Wc = new string[5] { "蓄电池合", "主断合", "牵引", "制动", "未知" };
            m_SubLoco3Wc = new string[5] { "蓄电池合", "主断合", "牵引", "制动", "未知" };

            m_CommunicationStatusRects = new Rectangle[4];
            m_OperateTerminalRects = new Rectangle[4];
            m_PrepareRects = new Rectangle[4];
            m_ZeroRects = new Rectangle[4];
            m_ApantographRects = new Rectangle[4];
            m_BpantographRects = new Rectangle[4];
            m_ALineCircuitBreakerRects = new Rectangle[4];
            m_BLineCircuitBreakerRects = new Rectangle[4];

            m_DegaussingLevelRects = new Rectangle[4];
            m_ParvafaciesStatusRects = new Rectangle[4];
            m_BrakeStatusRects = new Rectangle[4];
            m_FaultMarkingRects = new Rectangle[4];

            m_FaultDisplay = new Rectangle(85, 505, 316, 33); //left
            m_FillFaultDisplay = new Rectangle(86, 506, 315, 32);

            m_PressureRects = new Rectangle[4];
            m_SettingLevelRects = new Rectangle[4];
            m_RunSpeedRects = new Rectangle[4];
            m_CurrentRects = new Rectangle[4];
            m_VoltageRects = new Rectangle[4];
            m_ExcitationRects = new Rectangle[4];
            m_ControlLoopRects = new Rectangle[4];
            m_MainAirReservoirRects = new Rectangle[4];
            m_DazhaDestRects = new Rectangle[4];
            m_BalancedCylinderRects = new Rectangle[4];
            m_TrainPipeRects = new Rectangle[4];
            m_FlowInstrumentRects = new Rectangle[4];
            m_BrakeCylinderRects = new Rectangle[4];

            m_TailPressure = new Rectangle(480, 505, 316, 33);

            m_DegaussingLevelStrs = new string[4] { "", "", "", "" };
            m_ParvafaciesStatusStrs = new string[4] { "", "", "", "" };
            m_BrakeStatusStrs = new string[4] { "", "", "", "" };
            m_PressureStrs = new string[4] { "", "", "", "" };
            m_SettingLevelStrs = new string[4] { "", "", "", "" };
            m_RunSpeedStrs = new string[4] { "", "", "", "" };
            m_CurrentStrs = new string[4] { "", "", "", "" };
            m_VoltageStrs = new string[4] { "", "", "", "" };
            m_ExcitationStrs = new string[4] { "", "", "", "" };
            m_ControlLoopStrs = new string[4] { "", "", "", "" };
            m_MainAirReservoirStrs = new string[4] { "", "", "", "" };
            m_DazhaDestStrs = new string[4] { "", "", "", "" };
            m_BalancedCylinderStrs = new string[4] { "", "", "", "" };
            m_TrainPipeStrs = new string[4] { "", "", "", "" };
            m_FlowInstrumentStrs = new string[4] { "", "", "", "" };
            m_BrakeCylinderStrs = new string[4] { "", "", "", "" };

            for (m_I = 0; m_I < 4; m_I++)
            {
                m_LocoWorkConRects[m_I] = new Rectangle(85 + 79 * m_I, 76, 79, 33);
                m_CommunicationStatusRects[m_I] = new Rectangle(115 + 79 * m_I, 116, 18, 18);

                m_OperateTerminalRects[m_I] = new Rectangle(115 + 79 * m_I, 149, 18, 18);

                m_PrepareRects[m_I] = new Rectangle(115 + 79 * m_I, 182, 18, 18);

                m_ZeroRects[m_I] = new Rectangle(115 + 79 * m_I, 215, 18, 18);
                m_ApantographRects[m_I] = new Rectangle(115 + 79 * m_I, 248, 18, 18);
                m_BpantographRects[m_I] = new Rectangle(115 + 79 * m_I, 281, 18, 18);
                m_ALineCircuitBreakerRects[m_I] = new Rectangle(115 + 79 * m_I, 314, 18, 18);

                m_BLineCircuitBreakerRects[m_I] = new Rectangle(115 + 79 * m_I, 347, 18, 18);

                m_DegaussingLevelRects[m_I] = new Rectangle(85 + 79 * m_I, 373, 79, 33);
                m_ParvafaciesStatusRects[m_I] = new Rectangle(85 + 79 * m_I, 406, 79, 33);
                m_BrakeStatusRects[m_I] = new Rectangle(85 + 79 * m_I, 439, 79, 33);

                m_FaultMarkingRects[m_I] = new Rectangle(115 + 79 * m_I, 479, 18, 18);

                m_PressureRects[m_I] = new Rectangle(480 + 79 * m_I, 76, 79, 33);
                m_SettingLevelRects[m_I] = new Rectangle(480 + 79 * m_I, 109, 79, 33);
                m_RunSpeedRects[m_I] = new Rectangle(480 + 79 * m_I, 142, 79, 33);
                m_CurrentRects[m_I] = new Rectangle(480 + 79 * m_I, 175, 79, 33);
                m_VoltageRects[m_I] = new Rectangle(480 + 79 * m_I, 208, 79, 33);
                m_ExcitationRects[m_I] = new Rectangle(480 + 79 * m_I, 241, 79, 33);
                m_ControlLoopRects[m_I] = new Rectangle(480 + 79 * m_I, 274, 79, 33);
                m_MainAirReservoirRects[m_I] = new Rectangle(480 + 79 * m_I, 307, 79, 33);
                m_DazhaDestRects[m_I] = new Rectangle(480 + 79 * m_I, 340, 79, 33);
                m_BalancedCylinderRects[m_I] = new Rectangle(480 + 79 * m_I, 373, 79, 33);
                m_TrainPipeRects[m_I] = new Rectangle(480 + 79 * m_I, 406, 79, 33);
                m_FlowInstrumentRects[m_I] = new Rectangle(480 + 79 * m_I, 439, 79, 33);
                m_BrakeCylinderRects[m_I] = new Rectangle(480 + 79 * m_I, 472, 79, 33);
            }

            m_TitleStrRects = new Rectangle[10];

            m_TitleStrRects[0] = new Rectangle(15, 12, 60, 20);
            m_TitleStrRects[1] = new Rectangle(149, 12, 60, 20);

            m_TitleStrRects[2] = new Rectangle(295, 12, 80, 20);
            m_TitleStrRects[3] = new Rectangle(376, 12, 37, 20);
            m_TitleStrRects[4] = new Rectangle(414, 12, 37, 20);

            m_TitleStrRects[5] = new Rectangle(462, 12, 80, 20);
            m_TitleStrRects[6] = new Rectangle(536, 12, 37, 20);
            m_TitleStrRects[7] = new Rectangle(574, 12, 37, 20);

            m_TitleStrRects[8] = new Rectangle(670, 12, 60, 20);
            m_TitleStrRects[9] = new Rectangle(722, 12, 70, 20);

            m_TitleStrs = new string[10]
            {
                "LTE-A", "LTE-B",
                "800M电台", "A", "B",
                "400K电台", "A", "B",
                "公里标", ""
            };
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
                    append_postCmd(CmdType.ChangePage, (int)ViewState.WirelessInterface, 0, 0);
                    break;

                case (int)ViewState.WiBtnShowStatus:
                    append_postCmd(CmdType.ChangePage, (int)ViewState.WiBtnBrakeStatus, 0, 0);
                    break;

                case (int)ViewState.WiBtnBlankThree:
                    append_postCmd(CmdType.ChangePage, (int)ViewState.WiBtnVersionInfo, 0, 0);
                    break;

                case (int)ViewState.WiBtnRemoveMarshalling:
                    append_postCmd(CmdType.ChangePage, (int)ViewState.WiBtnFaultLamp, 0, 0);
                    break;

                case (int)ViewState.WiBtnBlankFive:

                    //append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)ViewState.WI_BTN_IO, 0, 0);
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
            dcGs.DrawRectangles(m_WhiteBigLinePen, m_FrameRects);
            m_TitleStrs[9] = GetInFloatValue(InFloatKeys.InF公里标).ToString("F3");
            for (m_I = 0; m_I < 14; m_I++)
            {
                dcGs.DrawLine(m_WhiteLinePen, m_HpStarts[m_I], m_HpEnds[m_I]);
            }
            for (m_I = 0; m_I < 6; m_I++)
            {
                dcGs.DrawLine(m_WhiteBigLinePen, m_VpStarts[m_I], m_VpEnds[m_I]);
            }

            if (GetInBoolValue(InBoolKeys.InB巡检0按下状态MMI0键按下状态))
            {
                m_BtnFlags[9] = true;
            }
            else if (m_BtnFlags[9])
            {
                m_BtnFlags[9] = false;
                append_postCmd(CmdType.ChangePage, (int)(ViewState.WirelessInterface), 0, 0);
            }

            if (GetInBoolValue(InBoolKeys.InB向前1按下状态MMI1键按下状态))
            {
                m_BtnFlags[4] = true;

                //append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.WI_BTN_BrakeStatus), 0, 0);
            }
            else if (m_BtnFlags[4])
            {
                m_BtnFlags[4] = false;
                append_postCmd(CmdType.ChangePage, (int)(ViewState.WiBtnBrakeStatus), 0, 0);
            }

            if (GetInBoolValue(InBoolKeys.InB车位3按下状态MMI3键按下状态))
            {
                m_BtnFlags[6] = true;

                //append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.WI_BTN_VersionInfo), 0, 0);
            }
            else if (m_BtnFlags[6])
            {
                m_BtnFlags[6] = false;
                append_postCmd(CmdType.ChangePage, (int)(ViewState.WiBtnVersionInfo), 0, 0);
            }

            if (GetInBoolValue(InBoolKeys.InB进路号4按下状态MMI4键按下状态))
            {
                m_BtnFlags[7] = true;

                //append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.WI_BTN_FaultLamp), 0, 0);
            }
            else if (m_BtnFlags[7])
            {
                m_BtnFlags[7] = false;
                append_postCmd(CmdType.ChangePage, (int)(ViewState.WiBtnFaultLamp), 0, 0);
            }

            if (GetInBoolValue(InBoolKeys.InB定标5按下状态MMI5键按下状态))
            {
                m_BtnFlags[8] = true;

                // append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.WI_BTN_IO), 0, 0);
            }
            else if (m_BtnFlags[8])
            {
                m_BtnFlags[8] = false;

                //append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.WI_BTN_IO), 0, 0);
            }

            //LTE=A 颜色
            var tmp = GetBrush(InBoolKeys.InBLTEA绿, InBoolKeys.InBLTEA红);

            //LTE-A
            dcGs.FillRectangle(tmp, m_FillRects[0]);
            dcGs.FillRectangle(tmp, m_FillRects[1]);
            dcGs.FillRectangle(tmp, m_FillRects[2]);
            dcGs.FillRectangle(tmp, m_FillRects[3]);
            dcGs.FillEllipse(tmp, m_FillRects[4]);

            tmp = GetBrush(InBoolKeys.InBLTEB绿, InBoolKeys.InBLTEB红);

            //LTE-B
            dcGs.FillRectangle(tmp, m_FillRects[5]);
            dcGs.FillRectangle(tmp, m_FillRects[6]);
            dcGs.FillRectangle(tmp, m_FillRects[7]);
            dcGs.FillRectangle(tmp, m_FillRects[8]);
            dcGs.FillEllipse(tmp, m_FillRects[9]);

            dcGs.FillRectangle(GetBrush(InBoolKeys.InB800M电台A绿, InBoolKeys.InB800M电台A红), m_FillRects[10]);
            dcGs.FillRectangle(GetBrush(InBoolKeys.InB800M电台B绿, InBoolKeys.InB800M电台B红), m_FillRects[11]);
            dcGs.FillRectangle(GetBrush(InBoolKeys.InB400k电台A绿, InBoolKeys.InB400k电台A红), m_FillRects[12]);
            dcGs.FillRectangle(GetBrush(InBoolKeys.InB400k电台B绿, InBoolKeys.InB400k电台B红), m_FillRects[13]);

            for (m_I = 0; m_I < 10; m_I++)
            {
                if (m_I == 3 || m_I == 4 || m_I == 6 || m_I == 7)
                {
                    dcGs.DrawString(m_TitleStrs[m_I], m_ChineseSmallFont, Brushs.BlackBrush, m_TitleStrRects[m_I], FontInfo.SfCc);
                }
                else
                {
                    dcGs.DrawString(m_TitleStrs[m_I], m_ChineseSmallFont, Brushs.WhiteBrush, m_TitleStrRects[m_I], FontInfo.SfLc);
                }
            }

            dcGs.DrawRectangle(m_WhiteLinePen, m_TitleStrRects[9]);

            OnPaintFixed(dcGs);

            OnPaintChangeable(dcGs);

            base.paint(dcGs);
        }

        private SolidBrush GetBrush(string name1, string name2)
        {
            var value1 = GetInBoolValue(name1);
            var value2 = GetInBoolValue(name2);
            if (value2)
            {
                return Brushs.RedBrush;
            }
            if (value1)
            {
                return Brushs.GreenBrush;
            }
            return Brushs.WhiteBrush;
        }

        /// <summary>
        ///     onPaintFixed
        /// </summary>
        /// <param name="dcGs"></param>
        private void OnPaintFixed(Graphics dcGs)
        {
            for (m_I = 0; m_I < 13; m_I++)
            {
                dcGs.DrawString(m_FixedStrs[m_I], m_ChineseSmallFont, m_GoldBrush, m_FixedRects[m_I], FontInfo.SfCc);
            }
            for (m_I = 13; m_I < 18; m_I++)
            {
                dcGs.DrawString(m_FixedStrs[m_I], m_ChineseSmallFont, Brushs.WhiteBrush, m_FixedRects[m_I], FontInfo.SfCc);
            }

            for (m_I = 18; m_I < 25; m_I++)
            {
                dcGs.DrawString(m_FixedStrs[m_I], m_ChineseSmallFont, m_GoldBrush, m_FixedRects[m_I], FontInfo.SfCc);
            }

            for (m_I = 25; m_I < 32; m_I++)
            {
                dcGs.DrawString(m_FixedStrs[m_I], m_ChineseSmallFont, m_BlueBrush, m_FixedRects[m_I], FontInfo.SfCc);
            }
            for (m_I = 32; m_I < 36; m_I++)
            {
                dcGs.DrawString(m_FixedStrs[m_I], m_ChineseSmallFont, Brushs.WhiteBrush, m_FixedRects[m_I], FontInfo.SfCc);
            }
        }
        /// <summary>
        /// 获取圆圈状态颜色
        /// </summary>
        /// <param name="value">对应的Bool量</param>
        /// <returns>绘制的颜色</returns>
        private SolidBrush GetElipseBrush(bool value)
        {
            return value ? Brushs.GreenBrush : Brushs.WhiteBrush;
        }
        /// <summary>
        ///     onPaintChangeable
        /// </summary>
        /// <param name="dcGs"></param>
        private void OnPaintChangeable(Graphics dcGs)
        {
            dcGs.FillRectangle(Brushs.RedBrush, m_FillFaultDisplay);

            for (m_I = 0; m_I < 4; m_I++)
            {
                if (GetInBoolValue(m_LogicName3[0 + m_I * 4]))
                {
                    dcGs.DrawString(m_MainLocoWc[0], m_ChineseSmallFont, Brushs.WhiteBrush, m_LocoWorkConRects[m_I],
                        FontInfo.SfCc);
                }
                else if (GetInBoolValue(m_LogicName3[1 + m_I * 4]))
                {
                    dcGs.DrawString(m_MainLocoWc[1], m_ChineseSmallFont, Brushs.WhiteBrush, m_LocoWorkConRects[m_I],
                        FontInfo.SfCc);
                }
                else if (GetInBoolValue(m_LogicName3[2 + m_I * 4]))
                {
                    dcGs.DrawString(m_MainLocoWc[2], m_ChineseSmallFont, Brushs.WhiteBrush, m_LocoWorkConRects[m_I],
                        FontInfo.SfCc);
                }
                else if (GetInBoolValue(m_LogicName3[3 + m_I * 4]))
                {
                    dcGs.DrawString(m_MainLocoWc[3], m_ChineseSmallFont, Brushs.WhiteBrush, m_LocoWorkConRects[m_I],
                        FontInfo.SfCc);
                }
                else
                {
                    dcGs.DrawString(m_MainLocoWc[4], m_ChineseSmallFont, Brushs.WhiteBrush, m_LocoWorkConRects[m_I],
                        FontInfo.SfCc);
                }

                #region 所有圆圈状态
                dcGs.FillEllipse(GetElipseBrush(GetInBoolValue(m_LogicName4[m_I])),
                 m_CommunicationStatusRects[m_I]);
                dcGs.FillEllipse(GetElipseBrush(GetInBoolValue(m_LogicNames5[m_I])),
                    m_OperateTerminalRects[m_I]);
                dcGs.FillEllipse(GetElipseBrush(GetInBoolValue(m_LogicName6[m_I])),
                   m_PrepareRects[m_I]);
                dcGs.FillEllipse(GetElipseBrush(GetInBoolValue(m_LogicName7[m_I])),
                m_ZeroRects[m_I]);
                dcGs.FillEllipse(GetElipseBrush(GetInBoolValue(m_LogicName8[m_I])),
                  m_ApantographRects[m_I]);
                dcGs.FillEllipse(GetElipseBrush(GetInBoolValue(m_LogicName9[m_I])),
                   m_BpantographRects[m_I]);
                dcGs.FillEllipse(GetElipseBrush(GetInBoolValue(m_LogicName10[m_I])),
                  m_ALineCircuitBreakerRects[m_I]);
                dcGs.FillEllipse(GetElipseBrush(GetInBoolValue(m_LogicName11[m_I])),
                   m_BLineCircuitBreakerRects[m_I]);
                dcGs.FillEllipse(GetElipseBrush(GetInBoolValue(m_LogicName12[m_I])),
                  m_FaultMarkingRects[m_I]);
                #endregion

                m_DegaussingLevelStrs[m_I] = GetInFloatValue(m_LogicName1[m_I * 16 + 14]).ConvertToString(); //磁削等级
                dcGs.DrawString(m_DegaussingLevelStrs[m_I], m_ChineseFont, Brushs.WhiteBrush, m_DegaussingLevelRects[m_I],
                    FontInfo.SfCc);
                m_PressureStrs[m_I] = GetInFloatValue(m_LogicName1[m_I * 16]).ConvertToString("0.0"); //网压
                dcGs.DrawString(m_PressureStrs[m_I], m_ChineseFont, Brushs.WhiteBrush, m_PressureRects[m_I], FontInfo.SfCc);
                m_SettingLevelStrs[m_I] = GetInFloatValue(m_LogicName1[m_I * 16 + 1]).ConvertToString("0.0");
                dcGs.DrawString(m_SettingLevelStrs[m_I], m_ChineseFont, Brushs.WhiteBrush, m_SettingLevelRects[m_I],
                    FontInfo.SfCc);
                m_RunSpeedStrs[m_I] = GetInFloatValue(m_LogicName1[m_I * 16 + 2]).ConvertToString("0");
                dcGs.DrawString(m_RunSpeedStrs[m_I], m_ChineseFont, Brushs.WhiteBrush, m_RunSpeedRects[m_I], FontInfo.SfCc);
                m_CurrentStrs[m_I] = GetInFloatValue(m_LogicName1[m_I * 16 + 3]).ConvertToString("0");
                dcGs.DrawString(m_CurrentStrs[m_I], m_ChineseFont, Brushs.WhiteBrush, m_CurrentRects[m_I], FontInfo.SfCc);
                m_VoltageStrs[m_I] = GetInFloatValue(m_LogicName1[m_I * 16 + 4]).ConvertToString("0");
                dcGs.DrawString(m_VoltageStrs[m_I], m_ChineseFont, Brushs.WhiteBrush, m_VoltageRects[m_I], FontInfo.SfCc);
                m_ExcitationStrs[m_I] = GetInFloatValue(m_LogicName1[m_I * 16 + 5]).ConvertToString("0");
                dcGs.DrawString(m_ExcitationStrs[m_I], m_ChineseFont, Brushs.WhiteBrush, m_ExcitationRects[m_I], FontInfo.SfCc);
                m_ControlLoopStrs[m_I] = GetInFloatValue(m_LogicName1[m_I * 16 + 6]).ConvertToString("0");
                dcGs.DrawString(m_ControlLoopStrs[m_I], m_ChineseFont, Brushs.WhiteBrush, m_ControlLoopRects[m_I], FontInfo.SfCc);
                m_MainAirReservoirStrs[m_I] = GetInFloatValue(m_LogicName1[m_I * 16 + 7]).ConvertToString("0");
                dcGs.DrawString(m_MainAirReservoirStrs[m_I], m_ChineseFont, Brushs.WhiteBrush, m_MainAirReservoirRects[m_I],
                    FontInfo.SfCc);
                m_DazhaDestStrs[m_I] = GetInFloatValue(m_LogicName1[m_I * 16 + 8]).ConvertToString("0");
                dcGs.DrawString(m_DazhaDestStrs[m_I], m_ChineseFont, Brushs.WhiteBrush, m_DazhaDestRects[m_I], FontInfo.SfCc);
                m_BalancedCylinderStrs[m_I] = GetInFloatValue(m_LogicName1[m_I * 16 + 9]).ConvertToString("0");
                dcGs.DrawString(m_BalancedCylinderStrs[m_I], m_ChineseFont, Brushs.WhiteBrush, m_BalancedCylinderRects[m_I],
                    FontInfo.SfCc);
                m_TrainPipeStrs[m_I] = GetInFloatValue(m_LogicName1[m_I * 16 + 10]).ConvertToString("0");
                dcGs.DrawString(m_TrainPipeStrs[m_I], m_ChineseFont, Brushs.WhiteBrush, m_TrainPipeRects[m_I], FontInfo.SfCc);
                m_FlowInstrumentStrs[m_I] = GetInFloatValue(m_LogicName1[m_I * 16 + 11]).ConvertToString("0");
                dcGs.DrawString(m_FlowInstrumentStrs[m_I], m_ChineseFont, Brushs.WhiteBrush, m_FlowInstrumentRects[m_I],
                    FontInfo.SfCc);
                m_BrakeCylinderStrs[m_I] = GetInFloatValue(m_LogicName1[m_I * 16 + 12]).ConvertToString("0");
                dcGs.DrawString(m_BrakeCylinderStrs[m_I], m_ChineseFont, Brushs.WhiteBrush, m_BrakeCylinderRects[m_I],
                    FontInfo.SfCc);
                m_TailPressureStrs = GetInFloatValue(m_LogicName1[13]).ConvertToString("0kPa");
                dcGs.DrawString(m_TailPressureStrs, m_ChineseFont, Brushs.WhiteBrush, m_TailPressure, FontInfo.SfCc);
            }

            for (m_I = 0; m_I < 4; m_I++)
            {
                if (GetInBoolValue(SplitLogicName[m_I * 3 + 0]))
                {
                    SplitPhaseStatu[m_I] = SplitPhaseStatus.Plan;
                }
                else if (GetInBoolValue(SplitLogicName[m_I * 3 + 1]))
                {
                    SplitPhaseStatu[m_I] = SplitPhaseStatus.Join;
                }
                else if (GetInBoolValue(SplitLogicName[m_I * 3 + 2]))
                {
                    SplitPhaseStatu[m_I] = SplitPhaseStatus.After;
                }
                else
                {
                    SplitPhaseStatu[m_I] = SplitPhaseStatus.UnKnow;
                }
                dcGs.DrawString(EnumUtil.GetDescription(SplitPhaseStatu[m_I]).FirstOrDefault(), m_ChineseSmallFont, Brushs.WhiteBrush, m_ParvafaciesStatusRects[m_I],
                    FontInfo.SfCc);
            }
            dcGs.DrawRectangle(m_WhiteLinePen, SplitRectangle);
            var str = "分相距离\r\n" + GetInFloatValue(InFloatKeys.InF分相区距离).ConvertToString("0m", float.Epsilon);
            dcGs.DrawString(str, m_ChineseFont10, Brushs.WhiteBrush, SplitRectangle,
                    FontInfo.SfCc);
        }

        private string[] SplitLogicName =
        {
            InBoolKeys.InB主车分相状态分相预备,
            InBoolKeys.InB主车分相状态分相区,
            InBoolKeys.InB主车分相状态分相已过,
            InBoolKeys.InB从1分相状态分相预备,
            InBoolKeys.InB从1分相状态分相区,
            InBoolKeys.InB从1分相状态分相已过,
            InBoolKeys.InB从2分相状态分相预备,
            InBoolKeys.InB从2分相状态分相区,
            InBoolKeys.InB从2分相状态分相已过,
            InBoolKeys.InB从3分相状态分相预备,
            InBoolKeys.InB从3分相状态分相区,
            InBoolKeys.InB从3分相状态分相已过,
        };

        private SplitPhaseStatus[] SplitPhaseStatu = new SplitPhaseStatus[4]
        {
            SplitPhaseStatus.UnKnow,
            SplitPhaseStatus.UnKnow,
            SplitPhaseStatus.UnKnow,
            SplitPhaseStatus.UnKnow
        };

        public enum SplitPhaseStatus
        {
            [Description("")]
            UnKnow,
            [Description("分相预备")]
            Plan,
            [Description("分相区")]
            Join,
            [Description("分相已过")]
            After,
        }
    }
}