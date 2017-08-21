#region 文件说明
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
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using Urban.HeFei.TMS.Common;
using Urban.HeFei.TMS.Help;

namespace Urban.HeFei.TMS.History
{
    /// <summary>
    /// 功能描述：视图7--No.0-主界面
    /// 创建人：lih
    /// 创建时间：2015-8-26
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V7HistoryC0MainView : baseClass
    {
        #region 私有变量
        private ListBox<HistoryFaultInfo> m_ListBox;//列表控件
        private List<Image> m_ResourceImage = new List<Image>();//图片资源



        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private Pen m_WhiteLinePen = new Pen(new SolidBrush(Color.White), 1.6f);

        private Font m_ChineseFont = new Font("宋体", 18);
        private Font m_DigitFont = new Font("Arial", 32);


        private string[] m_FrameStrs;
        private Rectangle[] m_FrameStrRects;

        private Rectangle m_PageRect = new Rectangle(720, 287, 54, 20);
        private string m_PageStr;
        private Button[] m_PageBtns;

        private Rectangle m_FrameRect = new Rectangle(22, 150, 666, 300);
        private Rectangle m_FrameFillRect = new Rectangle(23, 151, 665, 299);
        private int m_I = 0;

        private int m_CurrentPageIndex = 1;
        private int m_AllPageCount = 1;
        private string m_TimeShowFormat = "yy-MM-dd HH:mm:ss";

        private int m_ItemsCount = 0;
        private Rectangle m_ShowAllCountRect = new Rectangle(247, 457, 553, 48);
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "故障历史信息-主界面";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V7_History_C0_MainView";
        //}

        /// <summary>
        /// 初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_FrameStrs = new string[2] { "历史故障清单", "检查到故障条数" };
            m_FrameStrRects = new Rectangle[2];
            m_FrameStrRects[0] = new Rectangle(21, 118, 162, 36);
            m_FrameStrRects[1] = new Rectangle(75, 459, 190, 48);

            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });

            var bsUp = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[0], DownImage = m_ResourceImage[0] };
            var bsDown = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[1], DownImage = m_ResourceImage[1] };

            m_PageBtns = new Button[2];
            m_PageBtns[0] = new Button("", new Rectangle(715, 321, 64, 45), (int)ViewState.HsUpBtn, bsUp, false);
            m_PageBtns[1] = new Button("", new Rectangle(715, 372, 64, 45), (int)ViewState.HsDownBtn, bsDown, false);
            m_PageBtns[0].ClickEvent += V7_History_C0_MainView_ClickEvent;
            m_PageBtns[1].ClickEvent += V7_History_C0_MainView_ClickEvent;

            m_ListBox = new ListBox<HistoryFaultInfo>(new RectangleF(23, 151, 666, 300), new List<HistoryFaultInfo>(),
               new ListBoxHeader() { Text = "车号", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("宋体", 13), DataFont = new Font("宋体", 12, FontStyle.Bold), BackgroundBrush = Brushs.BlackBrush, Height = 25, Width = 86, Property = "CoachNo", SfData = FontInfo.SfCc, SfHeader = FontInfo.SfCc },
               new ListBoxHeader() { Text = "故障名称", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("宋体", 13), DataFont = new Font("宋体", 12f), BackgroundBrush = Brushs.BlackBrush, Height = 25, Width = 392, Property = "FaultName", SfData = FontInfo.SfLc, SfHeader = FontInfo.SfLc },
               new ListBoxHeader() { Text = "发生时间", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("宋体", 13), DataFont = new Font("宋体", 12f), BackgroundBrush = Brushs.BlackBrush, Height = 25, Width = 90, Property = "StartTime", SfData = FontInfo.SfLc, SfHeader = FontInfo.SfLc },
               new ListBoxHeader() { Text = "消失时间", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("宋体", 13), DataFont = new Font("宋体", 12f), BackgroundBrush = Brushs.BlackBrush, Height = 25, Width = 90, Property = "OverTime", SfData = FontInfo.SfLc, SfHeader = FontInfo.SfLc }
               );
            m_ListBox.RowCount = 5;


            FaultListManager.HistoryChanged += FaultListManager_HistoryChanged;


            return true;
        }

        private void FaultListManager_HistoryChanged(Help.FaultInfo obj)
        {
            m_ListBox.DataList.Add(new HistoryFaultInfo()
            {
                CoachNo = obj.CarNum,
                FaultName = obj.FaultName,
                StartTime = obj.Time.ToString(m_TimeShowFormat),
                OverTime = obj.RemoveTime.ToString(m_TimeShowFormat)

            });

            m_ItemsCount = m_ListBox.DataList.Count;
            m_AllPageCount = (m_ItemsCount % m_ListBox.RowCount) > 0 ? (m_ItemsCount / m_ListBox.RowCount + 1) : (m_ItemsCount / m_ListBox.RowCount);

            if (m_AllPageCount == 0)
            {
                m_AllPageCount = 1;
            }
            m_PageStr = string.Format("{0}-{1}", m_CurrentPageIndex, m_AllPageCount);
        }
        #endregion

        #region 鼠标事件
        /// <summary>
        /// mouseDown
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
            for (m_I = 0; m_I < m_PageBtns.Length; m_I++)
            {
                if ((nPoint.X >= m_PageBtns[m_I].Rect.X)
                       && (nPoint.X <= m_PageBtns[m_I].Rect.X + m_PageBtns[m_I].Rect.Width)
                       && (nPoint.Y >= m_PageBtns[m_I].Rect.Y)
                       && (nPoint.Y <= m_PageBtns[m_I].Rect.Y + m_PageBtns[m_I].Rect.Height))
                {
                    m_PageBtns[m_I].MouseDown(nPoint);
                    break;
                }
            }


            return base.mouseDown(nPoint);
        }

        /// <summary>
        /// mouseUp
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            for (m_I = 0; m_I < m_PageBtns.Length; m_I++)
            {
                if ((nPoint.X >= m_PageBtns[m_I].Rect.X)
                       && (nPoint.X <= m_PageBtns[m_I].Rect.X + m_PageBtns[m_I].Rect.Width)
                       && (nPoint.Y >= m_PageBtns[m_I].Rect.Y)
                       && (nPoint.Y <= m_PageBtns[m_I].Rect.Y + m_PageBtns[m_I].Rect.Height))
                {
                    m_PageBtns[m_I].MouseUp(nPoint);
                    break;
                }
            }

            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 按钮点击事件响应函数：切换到相应视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void V7_History_C0_MainView_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (e.Message == (int)ViewState.HsUpBtn && m_CurrentPageIndex > 1)
            {
                m_CurrentPageIndex--;
                m_ListBox.LastPage();
            }
            else if (e.Message == (int)ViewState.HsDownBtn && m_CurrentPageIndex < m_AllPageCount)
            {
                m_CurrentPageIndex++;
                m_ListBox.NextPage();
            }
            //append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, e.Message + 702, 0, 0);
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawRectangle(m_WhiteLinePen, m_FrameRect);
            dcGs.FillRectangle(Brushs.BlackBrush, m_FrameFillRect);
            dcGs.DrawString(m_PageStr, m_ChineseFont, Brushs.BlackBrush, m_PageRect, FontInfo.SfCc);
            dcGs.DrawString(m_ItemsCount.ToString(), m_DigitFont, Brushs.WhiteBrush, m_ShowAllCountRect, FontInfo.SfLc);

            m_ListBox.Paint(dcGs);
            for (m_I = 0; m_I < m_FrameStrRects.Length; m_I++)
            {
                dcGs.DrawString(m_FrameStrs[m_I], m_ChineseFont, Brushs.BlackBrush, m_FrameStrRects[m_I], FontInfo.SfLc);
            }
            for (m_I = 0; m_I < m_PageBtns.Length; m_I++)
            {
                m_PageBtns[m_I].Paint(dcGs);
            }

            base.paint(dcGs);
        }

        #endregion
    }
}
