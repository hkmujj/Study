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

using CommonUtil.Controls;
using ES.JCTMS.Common;
using ES.JCTMS.Common.Control;
using ES.JCTMS.Common.Control.Common;
using Excel.Interface;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using SS4B_TMS.Common;
using SS4B_TMS.Resource;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SS4B_TMS.WirelessInterface
{
    /// <summary>
    ///     功能描述：视图4-辅助-No.0-页面1
    ///     创建人：lih
    ///     创建时间：2015-8-24
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V708WIBZInfoTS : baseClass
    {
        private readonly List<Button> m_BtnsDownTabView = new List<Button>(); //按钮列表
        private int m_I;
        private Pen m_WhiteLinePen = new Pen(new SolidBrush(Color.White), 1.6f);
        private Pen m_WhiteBigLinePen = new Pen(new SolidBrush(Color.White), 2.0f);
        private ButtonStyle m_NormalBs;
        private Rectangle m_FlagRect;
        private readonly Rectangle m_FrameRect = new Rectangle(118, 46, 400, 40);
        private string m_FrameStr;
        private readonly Font m_ChineseFont = new Font("宋体", 14);
        public static List<GDIRectText> Text;
        private bool m_BtnFlag;

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
            Text = new List<GDIRectText>();
            var center = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            var left = new StringFormat()
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            };
            Text.Add(new GDIRectText()
            {
                Text = "编组诊断提示信息",
                TextBrush = Brushs.WhiteBrush,
                NeedDarwOutline = false,
                BackColorVisible = false,
                TextFormat = center,
                OutLineRectangle = new Rectangle(0, 0, 800, 70)
            });
            foreach (var info in ExcelParser.Parser<FaultInfo>(AppPaths.ConfigDirectory))
            {
                Text.Add(new GDIRectText()
                {
                    Text = info.Title,
                    Tag = info,
                    TextFormat = info.Index == 0 ? center : left,
                    NeedDarwOutline = info.Index != 0,
                    OutLineRectangle = new Rectangle(12, 70 + info.Index * 33, 230, 30),
                    RefreshAction = o => RefreshSatus((GDIRectText)o)
                });
                Text.Add(new GDIRectText()
                {
                    Text = info.Info,
                    Tag = info,
                    TextFormat = info.Index == 0 ? center : left,
                    NeedDarwOutline = info.Index != 0,
                    OutLineRectangle = new Rectangle(244, 70 + info.Index * 33, 540, 30),
                    RefreshAction = o => RefreshSatus((GDIRectText)o)
                });
            }
            return true;
        }

        public static List<FaultInfo> Fault = new List<FaultInfo>();

        private void RefreshSatus(GDIRectText gdi)
        {
            var info = gdi.Tag as FaultInfo;
            if (info != null)
            {
                if (string.IsNullOrEmpty(info.LogicName))
                {
                    gdi.OutLinePen.Color = info.DefaultFram;
                    gdi.BkColor = info.DefaultBack;
                    gdi.TextColor = info.DefaultFon;
                    return;
                }
                var value = GetInBoolValue(info.LogicName);
                if (value)
                {
                    if (Fault.Any(a => a.Index == info.Index))
                    {
                    }
                    else
                    {
                        Fault.Add(info);
                    }
                }
                else
                {
                    Fault.Remove(info);
                }
                gdi.OutLinePen.Color = value ? info.FaultFram : info.DefaultFram;
                gdi.BkColor = value ? info.FaultBack : info.DefaultBack;
                gdi.TextColor = value ? info.FaultFon : info.DefaultFon;
            }
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
                    append_postCmd(CmdType.ChangePage, (int)ViewState.WirelessInterface, 0, 0);
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
            //最左边按钮
            dcGs.DrawImage(ImageResource.flag1, m_FlagRect);
            //按钮
            m_BtnsDownTabView.ForEach(a => a.Paint(dcGs));

            if (GetInBoolValue(InBoolKeys.InB巡检0按下状态MMI0键按下状态))
            {
                m_BtnFlag = true;
            }
            else if (m_BtnFlag)
            {
                m_BtnFlag = false;
                append_postCmd(CmdType.ChangePage, (int)(ViewState.WirelessInterface), 0, 0);
            }

            Text.ForEach(f => f.OnPaint(dcGs));

            base.paint(dcGs);
        }
    }
}