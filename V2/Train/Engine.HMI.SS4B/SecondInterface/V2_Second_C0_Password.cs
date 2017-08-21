/*--------------------------------------------------------------------------------------------------
 *
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-06
 *
 * -------------------------------------------------------------------------------------------------
 *
 * 功能描述：视图2-牵引-No.0-牵引电流
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

namespace SS4B_TMS
{
    /// <summary>
    ///     功能描述：视图2-牵引-No.0-牵引电流
    ///     创建人：lih
    ///     创建时间：2015-08-06
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V2SecondC0Password : baseClass
    {
        private readonly string[] LogicName = new[]
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
        };

        /// <summary>
        ///     绘制界面
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawImage(ImageResource.flag1, m_FlagRect);
            for (m_I = 0; m_I < 10; m_I++)
            {
                m_BtnsDownTabView[m_I].Paint(dcGs);
            }

            if (GetInBoolValue(InBoolKeys.InB确认按下状态MMIE确认键按下状态))
            {
                m_OffFlag = true;
            }
            else if (m_OffFlag)
            {
                m_OffFlag = false;
                append_postCmd(CmdType.ChangePage, (int)(ViewState.MainInterface), 0, 0);
            }

            for (m_I = 0; m_I < 10; m_I++)
            {
                if (GetInBoolValue(LogicName[m_I]))
                {
                    m_BtnFlags[m_I] = true;
                    //userInputPsd += _strs_Btn_TabView[i];
                }
                else if (m_BtnFlags[m_I])
                {
                    m_BtnFlags[m_I] = false;
                    m_UserInputPsd += m_StrsBtnTabView[m_I];
                }
            }

            dcGs.DrawString(m_PdinputStr, m_ChineseFont, Brushs.WhiteBrush, m_PdInputStrRect, FontInfo.SfCc);
            dcGs.DrawString(m_PdStr, m_ChineseFont, Brushs.WhiteBrush, m_PdRect, FontInfo.SfCc);

            dcGs.FillRectangle(Brushs.WhiteBrush, m_InputFrameRect);

            dcGs.DrawString(m_UserInputPsd, m_ChineseFont, Brushs.BlackBrush, m_InputFrameRect, FontInfo.SfCc);

            m_BackBtn.Paint(dcGs);
            m_ConfirmBtn.Paint(dcGs);

            base.paint(dcGs);
        }

        private Font m_Font; //字体
        private Button m_BtnCheck; //自检按钮
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private readonly Font m_ChineseFont = new Font("宋体", 16, FontStyle.Regular);
        private Brush m_BlackBrush = new SolidBrush(Color.Black);
        private Brush m_RectBrush = new SolidBrush(Color.FromArgb(1, 116, 154));
        private Font m_DigitFont = new Font("Arial", 14, FontStyle.Regular);
        private Rectangle m_FrameRect = new Rectangle(12, 262, 674, 44);
        private string m_FrameStr = "牵引电流(A)";
        private Rectangle[] m_ChildrenRects;
        private Rectangle[] m_ChildrenStrRects;
        private int m_I;
        private string[] m_CurrentStrs;
        private readonly List<Button> m_BtnsDownTabView = new List<Button>(); //按钮列表
        private ButtonStyle m_NormalBs;
        private string m_UserInputPsd = "";
        private Rectangle m_FlagRect;

        private Rectangle m_PdInputStrRect;
        private Rectangle m_PdRect;
        private Rectangle m_InputFrameRect;

        private string m_PdinputStr;
        private string m_PdStr;

        private Button m_BackBtn;
        private Button m_ConfirmBtn;
        private string[] m_StrsBtnTabView;

        private bool[] m_BtnFlags;
        private bool m_OffFlag;

        /// <summary>
        ///     获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "牵引-牵引电流";
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
            m_StrsBtnTabView = new string[10] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            m_NormalBs = new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_14_CC_WAndB,
                Background = ImageResource.btn_b_up,
                DownImage = ImageResource.btn_y_down
            };

            m_BtnFlags = new bool[10]
            {
                false, false, false, false, false,
                false, false, false, false, false
            };
            for (var i = 0; i < m_StrsBtnTabView.Length; i++)
            {
                var btn = new Button(
                    m_StrsBtnTabView[i],
                    new Rectangle(125 + 68 * i, 539, 60, 60),
                    ((int)(ViewState.SiDigitOne + i)),
                    m_NormalBs,
                    false
                    );
                btn.ClickEvent += btn_ClickEvent;
                m_BtnsDownTabView.Add(btn);
            }
            m_FlagRect = new Rectangle(1, 539, 122, 58); //标识

            m_PdinputStr = "密码输入界面";
            m_PdStr = "密码:";

            m_PdInputStrRect = new Rectangle(323, 68, 168, 41);
            m_PdRect = new Rectangle(239, 198, 88, 41);
            m_InputFrameRect = new Rectangle(360, 197, 159, 48);

            var backStyle = new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_14_CC_WAndB,
                Background = ImageResource.back,
                DownImage = ImageResource.btn_y_down
            };
            var confirmStyle = new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_14_CC_WAndB,
                Background = ImageResource.confirm,
                DownImage = ImageResource.btn_y_down
            };

            m_BackBtn = new Button(
                "返回",
                new RectangleF(738, 104, 58, 60),
                (int)ViewState.SiBtnBack,
                backStyle,
                true);

            m_BackBtn.ClickEvent += btn_ClickEvent;

            m_ConfirmBtn = new Button(
                "确认",
                new RectangleF(738, 433, 58, 95),
                (int)ViewState.SiBtnConfirm,
                confirmStyle,
                true
                );
            m_ConfirmBtn.ClickEvent += btn_ClickEvent;

            return true;
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
            if ((nPoint.X >= m_BackBtn.Rect.X)
                && (nPoint.X <= m_BackBtn.Rect.X + m_BackBtn.Rect.Width)
                && (nPoint.Y >= m_BackBtn.Rect.Y)
                && (nPoint.Y <= m_BackBtn.Rect.Y + m_BackBtn.Rect.Height))
            {
                m_BackBtn.MouseDown(nPoint);
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
            if ((nPoint.X >= m_BackBtn.Rect.X)
                && (nPoint.X <= m_BackBtn.Rect.X + m_BackBtn.Rect.Width)
                && (nPoint.Y >= m_BackBtn.Rect.Y)
                && (nPoint.Y <= m_BackBtn.Rect.Y + m_BackBtn.Rect.Height))
            {
                m_BackBtn.MouseUp(nPoint);
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
        ///     菜单切换按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if ((int)ViewState.SiBtnBack == e.Message || (int)ViewState.SiBtnConfirm == e.Message)
            {
                var temp = (int)CommonStatus.CurrentViewState;
                append_postCmd(CmdType.ChangePage, (int)CommonStatus.CurrentViewState, 0, 0);
                return;
            }

            if (m_BtnsDownTabView.Find(a => a.ID == e.Message) != null)
            {
                m_BtnsDownTabView.Find(a => a.ID == e.Message).IsReplication = false;
                if (e.Message < 210)
                {
                    m_UserInputPsd += (e.Message - 200).ToString();
                }
                else
                {
                    m_UserInputPsd += "0";
                }
            }
            m_BtnsDownTabView.FindAll(a => a.ID != e.Message).ForEach(b => b.IsReplication = true);
        }
    }
}