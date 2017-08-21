/*--------------------------------------------------------------------------------------------------
 *
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-06
 *
 * -------------------------------------------------------------------------------------------------
 *
 * 功能描述：维护-No.4-接口检查
 *
 *-------------------------------------------------------------------------------------------------*/

using ES.JCTMS.Common;
using ES.JCTMS.Common.Control;
using ES.JCTMS.Common.Control.Common;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using SS4B_TMS.Common;
using SS4B_TMS.Resource;
using System.Collections.Generic;
using System.Drawing;

namespace SS4B_TMS.LCUInfo
{
    /// <summary>
    ///     功能描述：维护-No.4-接口检查
    ///     创建人：lih
    ///     创建时间：2015-08-06
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V804MaintenanceInterfaceCheck : baseClass
    {
        /// <summary>
        ///     界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
        }

        private ListBox<InterfaceCheck> m_Listbox;
        private Button[] m_Btns;

        private string[] m_BtnNames;
        private int m_I;

        private Button m_PageUpBtn;
        private Button m_PageDownBtn;

        private readonly int m_CurrentPageIndex = 1;
        private int m_PageCount = 1;
        private string m_ListStr;

        /// <summary>
        ///     获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "维护-接口检查";
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
            m_BtnNames = new string[3] { "输入", "输出", "选择" };

            var bs = new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_16_CC_B,
                Background = ImageResource.Button_NoSelected,
                DownImage = ImageResource.Button_Selected,
            };

            m_Btns = new Button[3];
            m_Btns[0] = new Button(m_BtnNames[0], new RectangleF(31, 441, 83, 48), (int)ViewState.MtIInputBtn, bs, true);
            m_Btns[1] = new Button(m_BtnNames[1], new RectangleF(125, 441, 83, 48), (int)ViewState.MtIOutputBtn, bs,
                true);
            m_Btns[2] = new Button(m_BtnNames[2], new RectangleF(300, 441, 83, 48), (int)ViewState.MtISelectBtn, bs,
                true);

            for (m_I = 0; m_I < m_Btns.Length; m_I++)
            {
                m_Btns[m_I].ClickEvent += V804_Maintenance_InterfaceCheck_ClickEvent;
            }

            var bsUp = new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_16_CC_B,
                Background = ImageResource.PABtnUp,
                DownImage = ImageResource.PABtnUp,
            };
            var bsdown = new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_16_CC_B,
                Background = ImageResource.PABtnDown,
                DownImage = ImageResource.PABtnDown,
            };

            m_PageUpBtn = new Button("", new RectangleF(700, 320, 70, 45), (int)ViewState.MtIPageBtnUp, bsUp, false);
            m_PageDownBtn = new Button("", new RectangleF(700, 370, 70, 45), (int)ViewState.MtIPageBtnDown, bsdown,
                false);

            m_PageUpBtn.ClickEvent += V804_Maintenance_InterfaceCheck_ClickEvent;
            m_PageDownBtn.ClickEvent += V804_Maintenance_InterfaceCheck_ClickEvent;

            m_Listbox = new ListBox<InterfaceCheck>(new RectangleF(24, 157, 639, 274), new List<InterfaceCheck>(),
                new ListBoxHeader
                {
                    Text = "地址",
                    TextBrush = Brushs.WhiteBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14, FontStyle.Regular),
                    BackgroundBrush = Brushs.BlackBrush,
                    Height = 25,
                    Width = 210,
                    TProperty = "ICAddress",
                    SF_Data = FontInfo.SfCc,
                    SF_Header = FontInfo.SfCc
                },
                new ListBoxHeader
                {
                    Text = "信号标识",
                    TextBrush = Brushs.WhiteBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14f),
                    BackgroundBrush = Brushs.BlackBrush,
                    Height = 25,
                    Width = 210,
                    TProperty = "ICSignalFlag",
                    SF_Data = FontInfo.SfCc,
                    SF_Header = FontInfo.SfCc
                },
                new ListBoxHeader
                {
                    Text = "值",
                    TextBrush = Brushs.WhiteBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14f),
                    BackgroundBrush = Brushs.BlackBrush,
                    Height = 25,
                    Width = 210,
                    TProperty = "ICValue",
                    SF_Data = FontInfo.SfCc,
                    SF_Header = FontInfo.SfCc
                }
                );

            m_Listbox.RowCount = 4;

            var ic1 = new InterfaceCheck { ICAddress = "C1_DI1_0", ICSignalFlag = "ZVR1", ICValue = "0" };
            var ic2 = new InterfaceCheck { ICAddress = "C1_DI1_1", ICSignalFlag = "ZVR2", ICValue = "1" };
            var ic3 = new InterfaceCheck { ICAddress = "C1_DI1_2", ICSignalFlag = "ZVR3", ICValue = "0" };
            var ic4 = new InterfaceCheck { ICAddress = "C1_DI1_3", ICSignalFlag = "ZVR4", ICValue = "2" };

            m_Listbox.DataList.Add(ic1);
            m_Listbox.DataList.Add(ic2);
            m_Listbox.DataList.Add(ic3);
            m_Listbox.DataList.Add(ic4);

            m_PageCount = (m_Listbox.DataList.Count % m_Listbox.RowCount) > 0
                ? (m_Listbox.DataList.Count / m_Listbox.RowCount + 1)
                : (m_Listbox.DataList.Count / m_Listbox.RowCount);

            m_ListStr = "RIOM10输入";

            return true;
        }

        /// <summary>
        ///     mouseDown
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
            return base.mouseDown(nPoint);
        }

        /// <summary>
        ///     mouseUp
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            return base.mouseUp(nPoint);
        }

        /// <summary>
        ///     点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void V804_Maintenance_InterfaceCheck_ClickEvent(object sender, ClickEventArgs<int> e)
        {
        }
    }
}