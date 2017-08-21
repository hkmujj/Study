/*--------------------------------------------------------------------------------------------------
 *
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-21
 *
 * -------------------------------------------------------------------------------------------------
 *
 * 功能描述：公共组件-No.1-切换视图按钮
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
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SS4B_TMS.CommonPart
{
    /// <summary>
    ///     功能描述：公共组件-No.1-切换视图按钮
    ///     创建人：lih
    ///     创建时间：2015-08-05
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class VCC1ViewChangeButton : baseClass
    {
        private readonly string[] m_StateNames = new[]
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
        };

        /// <summary>
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawRectangle(m_FramePen, m_FrameRect);
            dcGs.DrawImage(ImageResource.flag1, m_FlagRect);
            for (m_BtnIndex = 0; m_BtnIndex < 10; m_BtnIndex++)
            {
                if (GetInBoolValue(m_StateNames[m_BtnIndex]))
                {
                    if (m_BtnIndex == 4 || m_BtnIndex == 7)
                    {
                        break;
                    }
                    if (m_BtnIndex == 5)
                    {
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.OutB运行里程时间清零标志), 1, 0);
                        break;
                    }
                }
                if (GetInBoolValue(m_StateNames[m_BtnIndex]) == false && m_BtnIndex == 5)
                {
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.OutB运行里程时间清零标志), 0, 0);
                }
                if (GetInBoolValue(m_StateNames[m_BtnIndex]))
                {
                    m_BtnFlags[m_BtnIndex] = true;
                    //append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (10 - btnIndex), 0, 0);
                    break;
                }
                if (m_BtnFlags[m_BtnIndex])
                {
                    if (m_BtnIndex == 8)
                    {
                        break;
                    }
                    append_postCmd(CmdType.ChangePage, (10 - m_BtnIndex), 0, 0);
                    m_BtnFlags[m_BtnIndex] = false;
                    break;
                }
            }

            for (m_BtnIndex = 0; m_BtnIndex < 10; m_BtnIndex++)
            {
                m_BtnsDownTabView[m_BtnIndex].Paint(dcGs);
            }

            base.paint(dcGs);
        }

        private ViewState m_CurrentViewState; //当前视图
        private readonly List<Button> m_BtnsDownTabView = new List<Button>(); //按钮列表

        private ButtonStyle m_NormalBs;

        private ButtonStyle m_SmallNormalBs;

        private int m_BtnIndex;

        private Rectangle m_FlagRect;

        private readonly Pen m_FramePen = new Pen(new SolidBrush(Color.FromArgb(128, 128, 128)), 2);
        private readonly Rectangle m_FrameRect = new Rectangle(1, 538, 799, 62);

        private bool[] m_BtnFlags;

        /// <summary>
        ///     获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "公共试图-视图切换按钮";
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
            var strsBtnTabView = new string[10]
            {"格式二", "轴温信息", " LCU信息", "无线界面", "", "公里时清零", "时间设置", "亮度设置", "二级界面", "主界面"};
            m_NormalBs = new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_14_CC_WAndB,
                Background = ImageResource.btn_b_up,
                DownImage = ImageResource.btn_y_down
            };

            new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_14_CC_WAndB,
                Background = ImageResource.btn_y_up,
                DownImage = ImageResource.btn_y_down
            };

            new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_14_CC_WAndB,
                Background = ImageResource.btn_r_up,
                DownImage = ImageResource.btn_r_down
            };

            m_SmallNormalBs = new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_13_CC_WAndB,
                Background = ImageResource.btn_b_up,
                DownImage = ImageResource.btn_y_down
            };

            new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_13_CC_WAndB,
                Background = ImageResource.btn_y_up,
                DownImage = ImageResource.btn_y_down
            };

            new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_13_CC_WAndB,
                Background = ImageResource.btn_r_up,
                DownImage = ImageResource.btn_r_down
            };

            m_FlagRect = new Rectangle(1, 539, 122, 58); //标识

            m_BtnFlags = new bool[10] { false, false, false, false, false, false, false, false, false, false };

            for (var i = strsBtnTabView.Length; i >= 1; i--)
            {
                var btn = new Button(
                    strsBtnTabView[i - 1],
                    new Rectangle(125 + 68 * (i - 1), 539, 60, 60),
                    (10 - i),
                    m_NormalBs,
                    false
                    );
                btn.ClickEvent += btn_ClickEvent;
                m_BtnsDownTabView.Add(btn);
            }

            for (m_BtnIndex = 0; m_BtnIndex < 10; m_BtnIndex++)
            {
                if (m_BtnsDownTabView[m_BtnIndex].Text.Length < 5)
                {
                    m_BtnsDownTabView[m_BtnIndex].Style = m_NormalBs;
                }
                else
                {
                    m_BtnsDownTabView[m_BtnIndex].Style = m_SmallNormalBs;
                }
            }

            return true;
        }

        /// <summary>
        ///     获取到当前运行视图：根据当前视图设置按钮状态
        /// </summary>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                if (CommonStatus.IsBlackScreen)
                {
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.OutB运行里程时间清零标志), 1, 0);
                    CommonStatus.IsBlackScreen = false;
                }
                m_CurrentViewState = (ViewState)nParaB;
                CommonStatus.CurrentViewState = m_CurrentViewState;

                if (CommonStatus.InterfaceNameDicts.ContainsKey(CommonStatus.CurrentViewState))
                {
                    CommonStatus.CurrentInterfaceName = CommonStatus.InterfaceNameDicts[CommonStatus.CurrentViewState];
                }

                if (ViewState.BypassInfo2 == m_CurrentViewState //旁路
                    || ViewState.BypassInfo == m_CurrentViewState //旁路
                    || ViewState.BrakePage2 == m_CurrentViewState //制动
                    || ViewState.BrakePage3 == m_CurrentViewState //制动
                    || ViewState.AssistPage2 == m_CurrentViewState //辅助
                    || ViewState.AirConditionerPage2 == m_CurrentViewState //空调
                    || (ViewState.SiDigitOne <= m_CurrentViewState && m_CurrentViewState <= ViewState.SiDigitZero)
                    //数字键
                    ||
                    (ViewState.MtTrainInfo <= m_CurrentViewState && m_CurrentViewState <= ViewState.MtAdjustBrightness)
                    //维护
                    ) //这些视图切换放在各自的buttonClick事件中处理
                {
                    return;
                }

                switch (m_CurrentViewState) //目前只处理导航项的视图切换
                {
                    default:
                        if (m_BtnsDownTabView.Find(a => a.ID == nParaB - 1) != null)
                        {
                            m_BtnsDownTabView.Find(a => a.ID == nParaB - 1).IsReplication = false;
                        }
                        m_BtnsDownTabView.FindAll(a => a.ID != nParaB - 1).ForEach(b => b.IsReplication = true);
                        break;
                }
            }
        }

        /// <summary>
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

            return base.mouseUp(nPoint);
        }

        /// <summary>
        ///     菜单切换按钮点击事件响应函数：实现视图的切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (Convert.ToInt32(((int)m_CurrentViewState).ToString()) == e.Message + 1 &&
                m_CurrentViewState != ViewState.FaultInfo &&
                m_CurrentViewState != ViewState.BypassInfo)
            {
                (m_BtnsDownTabView.Find(a => a.ID == e.Message)).IsReplication = false;
                return;
            } //点击的为同一个视图，函数返回

            switch ((ViewState)(e.Message + 1))
            {
                default: //切换到相应视图
                    if (5 == e.Message + 1)
                    {
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.OutB运行里程时间清零标志), 1, 0);
                        break;
                    }
                    if (e.Message == 2)
                    {
                        break;
                    }
                    append_postCmd(CmdType.ChangePage, e.Message + 1, 0, 0);
                    break;
            }
        }
    }
}