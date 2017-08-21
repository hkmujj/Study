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
    public class V704WIRemoveBZ : baseClass
    {
        /// <summary>
        ///     界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawImage(ImageResource.flag2, m_FlagRect);
            for (m_I = 0; m_I < 10; m_I++)
            {
                m_BtnsDownTabView[m_I].Paint(dcGs);
            }
            m_BackBtn.Paint(dcGs);
            m_ConfirmBtn.Paint(dcGs);

            if (GetInBoolValue(InBoolKeys.InB设定按下状态))
            {
                m_Btnflag = true;
                //append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.WirelessInterface), 0, 0);
            }
            else if (m_Btnflag)
            {
                m_Btnflag = false;
                append_postCmd(CmdType.ChangePage, (int)(ViewState.WirelessInterface), 0, 0);
            }
            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.OutB解除编组标志),
                GetInBoolValue(InBoolKeys.InB确认按下状态MMIE确认键按下状态) ? 1 : 0, 0);

            m_ATermainalBtn.Paint(dcGs);
            m_BTermainalBtn.Paint(dcGs);

            for (m_I = 0; m_I < 2; m_I++)
            {
                dcGs.DrawString(m_TsInfoStrs[m_I], m_ChineseFont, Brushs.RedBrush, m_TsInfoRects[m_I], FontInfo.SfCc);
            }

            base.paint(dcGs);
        }

        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);

        private Pen m_WhiteLinePen = new Pen(new SolidBrush(Color.White), 1.6f);
        private Pen m_WhiteBigLinePen = new Pen(new SolidBrush(Color.White), 2.6f);

        private readonly Font m_ChineseFont = new Font("宋体", 14);

        private readonly List<Button> m_BtnsDownTabView = new List<Button>(); //按钮列表
        private ButtonStyle m_NormalBs;
        private Rectangle m_FlagRect;

        private Button m_BackBtn;
        private Button m_ConfirmBtn;
        private int m_I;

        private Rectangle[] m_TsInfoRects;
        private string[] m_TsInfoStrs;

        private Button m_ATermainalBtn;
        private Button m_BTermainalBtn;
        private bool m_Btnflag;
        private bool m_Sendflag = false;

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
            var strsBtnTabView = new string[10] { "1", "2", " 3", "4", "5", "6", "7", "8", "9", "0" };
            m_NormalBs = new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_14_CC_WAndB,
                Background = ImageResource.btn_b_up,
                DownImage = ImageResource.btn_y_down
            };

            for (var i = 0; i < strsBtnTabView.Length; i++)
            {
                var btn = new Button(
                    strsBtnTabView[i],
                    new Rectangle(125 + 68 * i, 539, 60, 60),
                    ((int)(ViewState.SiDigitOne + i)),
                    m_NormalBs,
                    false
                    );
                btn.ClickEvent += btn_ClickEvent;
                m_BtnsDownTabView.Add(btn);
            }
            m_FlagRect = new Rectangle(1, 539, 122, 58); //标识

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

            var atStr = string.Format("{0}\n{1}", "A端", "\"1\"");
            var btStr = string.Format("{0}\n{1}", "B端", "\"2\"");

            var atStyle = new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_20_CC_WAndB,
                Background = ImageResource.back,
                DownImage = ImageResource.btn_y_down
            };

            var btStyle = new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_20_CC_WAndB,
                Background = ImageResource.btn_g_up,
                DownImage = ImageResource.btn_g_down
            };

            m_ATermainalBtn = new Button(
                atStr,
                new RectangleF(185, 300, 160, 100),
                70401,
                atStyle, true
                );

            m_BTermainalBtn = new Button(
                btStr,
                new RectangleF(465, 300, 160, 100),
                70402,
                btStyle,
                true
                );

            m_TsInfoRects = new Rectangle[2];
            m_TsInfoRects[0] = new Rectangle(345, 104, 120, 40);
            m_TsInfoRects[1] = new Rectangle(185, 144, 500, 40);
            m_TsInfoStrs = new string[2]
            {
                "请谨慎操作:",
                "请确认您所在的司机室后再设置司机室端!"
            };
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
                append_postCmd(CmdType.ChangePage, (int)ViewState.WirelessInterface, 0, 0);
                return;
            }

            if (m_BtnsDownTabView.Find(a => a.ID == e.Message) != null)
            {
                m_BtnsDownTabView.Find(a => a.ID == e.Message).IsReplication = false;
            }
            m_BtnsDownTabView.FindAll(a => a.ID != e.Message).ForEach(b => b.IsReplication = true);
        }
    }
}