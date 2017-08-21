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
    public class V70102WIVersionInfo : baseClass
    {
        /// <summary>
        ///     界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawImage(ImageResource.btn_b_up, m_FlagRect);

            m_BtnsDownTabView.ForEach(a => a.Paint(dcGs));

            m_VersionInfos1.Paint(dcGs);
            m_VersionInfo2.Paint(dcGs);
            m_VersionInfo3.Paint(dcGs);
            m_VersionInfo4.Paint(dcGs);

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

            base.paint(dcGs);
        }

        private readonly List<Button> m_BtnsDownTabView = new List<Button>(); //按钮列表
        private int m_I;
        private Pen m_WhiteLinePen = new Pen(new SolidBrush(Color.White), 1.6f);
        private Pen m_WhiteBigLinePen = new Pen(new SolidBrush(Color.White), 2.0f);
        private ButtonStyle m_NormalBs;
        private Rectangle m_FlagRect;

        private ListBox<SoftwareVersionInfoNew> m_VersionInfos1;
        private ListBox<SoftwareVersionInfoNew> m_VersionInfo2;
        private ListBox<SoftwareVersionInfoNew> m_VersionInfo3;
        private ListBox<SoftwareVersionInfoNew> m_VersionInfo4;
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

            m_VersionInfos1 = new ListBox<SoftwareVersionInfoNew>(new RectangleF(182, 45, 200, 240),
                new List<SoftwareVersionInfoNew>(),
                new ListBoxHeader
                {
                    Text = "设备",
                    TextBrush = Brushs.WhiteBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14, FontStyle.Regular),
                    BackgroundBrush = Brushs.BlackBrush,
                    Height = 30,
                    Width = 80,
                    TProperty = "SoftwareName",
                    SF_Data = FontInfo.SfLc,
                    SF_Header = FontInfo.SfLc
                },
                new ListBoxHeader
                {
                    Text = "版本信息",
                    TextBrush = Brushs.WhiteBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14f),
                    BackgroundBrush = Brushs.BlackBrush,
                    Height = 30,
                    Width = 120,
                    TProperty = "VersionInfo",
                    SF_Data = FontInfo.SfLc,
                    SF_Header = FontInfo.SfCc
                }
                );
            m_VersionInfos1.RowCount = 7;

            m_VersionInfos1.GridBrush = Brushs.WhiteBrush;

            var hfi1 = new SoftwareVersionInfoNew { SoftwareName = "OCE", VersionInfo = "2015.02.12" };
            var hfi2 = new SoftwareVersionInfoNew { SoftwareName = "DTE", VersionInfo = "2014.08.00" };
            var hfi3 = new SoftwareVersionInfoNew { SoftwareName = "COM", VersionInfo = "2014.07.01" };
            var hfi4 = new SoftwareVersionInfoNew { SoftwareName = "SIE", VersionInfo = "2014.04.15" };
            var hfi5 = new SoftwareVersionInfoNew { SoftwareName = "LTE", VersionInfo = "2015.02.08" };
            var hfi6 = new SoftwareVersionInfoNew { SoftwareName = "GACR", VersionInfo = "2014.06.12" };
            var hfi7 = new SoftwareVersionInfoNew { SoftwareName = "IDU", VersionInfo = "2015.01.23" };

            m_VersionInfos1.DataList.Add(hfi1);
            m_VersionInfos1.DataList.Add(hfi2);
            m_VersionInfos1.DataList.Add(hfi3);
            m_VersionInfos1.DataList.Add(hfi4);
            m_VersionInfos1.DataList.Add(hfi5);
            m_VersionInfos1.DataList.Add(hfi6);
            m_VersionInfos1.DataList.Add(hfi7);

            m_VersionInfo2 = new ListBox<SoftwareVersionInfoNew>(new RectangleF(384, 45, 200, 210),
                new List<SoftwareVersionInfoNew>(),
                new ListBoxHeader
                {
                    Text = "设备",
                    TextBrush = Brushs.WhiteBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14, FontStyle.Regular),
                    BackgroundBrush = Brushs.BlackBrush,
                    Height = 30,
                    Width = 80,
                    TProperty = "SoftwareName",
                    SF_Data = FontInfo.SfLc,
                    SF_Header = FontInfo.SfLc
                },
                new ListBoxHeader
                {
                    Text = "版本信息",
                    TextBrush = Brushs.WhiteBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14f),
                    BackgroundBrush = Brushs.BlackBrush,
                    Height = 30,
                    Width = 120,
                    TProperty = "VersionInfo",
                    SF_Data = FontInfo.SfLc,
                    SF_Header = FontInfo.SfCc
                }
                );
            m_VersionInfo2.RowCount = 6;

            m_VersionInfo2.GridBrush = Brushs.WhiteBrush;

            var hfi11 = new SoftwareVersionInfoNew { SoftwareName = "CTR", VersionInfo = "2015.02.12" };
            var hfi12 = new SoftwareVersionInfoNew { SoftwareName = "MVB", VersionInfo = "2014.08.00" };
            var hfi13 = new SoftwareVersionInfoNew { SoftwareName = "DIAG", VersionInfo = "2014.07.01" };
            var hfi14 = new SoftwareVersionInfoNew { SoftwareName = "RUN", VersionInfo = "2014.04.15" };
            var hfi15 = new SoftwareVersionInfoNew { SoftwareName = "GDTE", VersionInfo = "2015.02.08" };
            var hfi16 = new SoftwareVersionInfoNew { SoftwareName = "GDIF", VersionInfo = "2014.06.12" };

            m_VersionInfo2.DataList.Add(hfi11);
            m_VersionInfo2.DataList.Add(hfi12);
            m_VersionInfo2.DataList.Add(hfi13);
            m_VersionInfo2.DataList.Add(hfi14);
            m_VersionInfo2.DataList.Add(hfi15);
            m_VersionInfo2.DataList.Add(hfi16);

            m_VersionInfo3 = new ListBox<SoftwareVersionInfoNew>(new RectangleF(182, 287, 200, 210),
                new List<SoftwareVersionInfoNew>(),
                new ListBoxHeader
                {
                    Text = "设备",
                    TextBrush = Brushs.WhiteBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14, FontStyle.Regular),
                    BackgroundBrush = Brushs.BlackBrush,
                    Height = 30,
                    Width = 80,
                    TProperty = "SoftwareName",
                    SF_Data = FontInfo.SfLc,
                    SF_Header = FontInfo.SfLc
                },
                new ListBoxHeader
                {
                    Text = "版本信息",
                    TextBrush = Brushs.WhiteBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14f),
                    BackgroundBrush = Brushs.BlackBrush,
                    Height = 30,
                    Width = 120,
                    TProperty = "VersionInfo",
                    SF_Data = FontInfo.SfLc,
                    SF_Header = FontInfo.SfCc
                }
                );
            m_VersionInfo3.RowCount = 6;

            m_VersionInfo3.GridBrush = Brushs.WhiteBrush;

            var hfi21 = new SoftwareVersionInfoNew { SoftwareName = "OCE", VersionInfo = "2000.00.00" };
            var hfi22 = new SoftwareVersionInfoNew { SoftwareName = "DTE", VersionInfo = "2000.00.00" };
            var hfi23 = new SoftwareVersionInfoNew { SoftwareName = "COM", VersionInfo = "2000.00.00" };
            var hfi24 = new SoftwareVersionInfoNew { SoftwareName = "SIE", VersionInfo = "2000.00.00" };
            var hfi25 = new SoftwareVersionInfoNew { SoftwareName = "LTE", VersionInfo = "2000.00.00" };
            var hfi26 = new SoftwareVersionInfoNew { SoftwareName = "GACR", VersionInfo = "2000.00.00" };

            m_VersionInfo3.DataList.Add(hfi21);
            m_VersionInfo3.DataList.Add(hfi22);
            m_VersionInfo3.DataList.Add(hfi23);
            m_VersionInfo3.DataList.Add(hfi24);
            m_VersionInfo3.DataList.Add(hfi25);
            m_VersionInfo3.DataList.Add(hfi26);

            m_VersionInfo4 = new ListBox<SoftwareVersionInfoNew>(new RectangleF(384, 287, 200, 210),
                new List<SoftwareVersionInfoNew>(),
                new ListBoxHeader
                {
                    Text = "设备",
                    TextBrush = Brushs.WhiteBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14, FontStyle.Regular),
                    BackgroundBrush = Brushs.BlackBrush,
                    Height = 30,
                    Width = 80,
                    TProperty = "SoftwareName",
                    SF_Data = FontInfo.SfLc,
                    SF_Header = FontInfo.SfLc
                },
                new ListBoxHeader
                {
                    Text = "版本信息",
                    TextBrush = Brushs.WhiteBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14f),
                    BackgroundBrush = Brushs.BlackBrush,
                    Height = 30,
                    Width = 120,
                    TProperty = "VersionInfo",
                    SF_Data = FontInfo.SfLc,
                    SF_Header = FontInfo.SfCc
                }
                );
            m_VersionInfo4.RowCount = 6;

            m_VersionInfo4.GridBrush = Brushs.WhiteBrush;

            var hfi31 = new SoftwareVersionInfoNew { SoftwareName = "CTR", VersionInfo = "2000.00.00" };
            var hfi32 = new SoftwareVersionInfoNew { SoftwareName = "MVB", VersionInfo = "2000.00.00" };
            var hfi33 = new SoftwareVersionInfoNew { SoftwareName = "DIAG", VersionInfo = "2000.00.00" };
            var hfi34 = new SoftwareVersionInfoNew { SoftwareName = "RUN", VersionInfo = "2000.00.00" };
            var hfi35 = new SoftwareVersionInfoNew { SoftwareName = "GDTE", VersionInfo = "2000.00.00" };
            var hfi36 = new SoftwareVersionInfoNew { SoftwareName = "GDIF", VersionInfo = "2000.00.00" };

            m_VersionInfo4.DataList.Add(hfi31);
            m_VersionInfo4.DataList.Add(hfi32);
            m_VersionInfo4.DataList.Add(hfi33);
            m_VersionInfo4.DataList.Add(hfi34);
            m_VersionInfo4.DataList.Add(hfi35);
            m_VersionInfo4.DataList.Add(hfi36);

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
    }
}