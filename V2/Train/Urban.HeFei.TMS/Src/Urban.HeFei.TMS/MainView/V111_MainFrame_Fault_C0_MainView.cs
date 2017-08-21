#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-21
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图111-故障-No.0-主界面
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.HeFei.TMS.Common;
using Urban.HeFei.TMS.Help;

namespace Urban.HeFei.TMS.MainView
{
    /// <summary>
    ///  功能描述：视图111-故障-No.0-主界面
    /// 创建人：lih
    /// 创建时间：2015-8-21
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V111MainFrameFaultC0MainView : baseClass
    {
        #region 私有变量
        private ListBox<FaultInfoTms> m_ListBox;//列表控件
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private Font m_ChineseFont = new Font("宋体", 18);
        private Font m_DigitFont = new Font("Arial", 32);
        private string[] m_FrameStrs;
        private Rectangle[] m_FrameStrRects;

        private Rectangle m_PageRect = new Rectangle(710, 290, 70, 27);
        private string m_PageStr;
        private Button[] m_PageBtns;

        private Rectangle m_FrameRect = new Rectangle(29, 140, 680, 300);

        private Button m_ReturnArrowBtn;
        private SolidBrush m_BackgrounpSolidBrush;
        private Rectangle m_BackgrounpRectangle;
        private int m_I = 0;

        private int m_CurrentPageIndex = 1;
        private int m_AllPageCount = 1;
        private string m_TimeShowFormat = "yy-MM-dd HH:mm:ss";

        private int m_ItemsCount = 0;
        private Rectangle m_ShowAllCountRect = new Rectangle(588, 96, 212, 48);

        private LocomotiveFailures m_LfInfos;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "故障-显示界面";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V111_MainFrame_Fault_C0_MainView";
        //}

        /// <summary>
        /// 初始化函数：导入图片、创建控件、列表框
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });

            //OnGetFailureInfos();


            var bsUp = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[0], DownImage = m_ResourceImage[0] };
            var bsDown = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[1], DownImage = m_ResourceImage[1] };

            m_PageBtns = new Button[2];
            m_PageBtns[0] = new Button("", new Rectangle(713, 321, 70, 45), (int)ViewState.FiUpBtn, bsUp, false);
            m_PageBtns[1] = new Button("", new Rectangle(713, 372, 70, 45), (int)ViewState.FiDownBtn, bsDown, false);
            m_PageBtns[0].ClickEvent += V111_MainFrame_Fault_C0_MainView_ClickEvent;
            m_PageBtns[1].ClickEvent += V111_MainFrame_Fault_C0_MainView_ClickEvent;

            m_FrameStrs = new string[3] { "故障清单事件", "故障条数:", "提示:" };
            m_FrameStrRects = new Rectangle[3];
            m_FrameStrRects[0] = new Rectangle(35, 104, 170, 30);
            m_FrameStrRects[1] = new Rectangle(474, 98, 160, 42);
            m_FrameStrRects[2] = new Rectangle(40, 452, 88, 30);

            m_BackgrounpSolidBrush = new SolidBrush(Color.FromArgb(215, 215, 215));
            m_BackgrounpRectangle = new Rectangle(0, 516, 800, 84);

            var bsResult = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[3], DownImage = m_ResourceImage[3] };
            m_ReturnArrowBtn = new Button("", new Rectangle(668, 520, 109, 75), (int)ViewState.FiReturnArrow, bsResult, false);

            m_ReturnArrowBtn.ClickEvent += _ReturnArrowBtn_ClickEvent;


            m_ListBox = new ListBox<FaultInfoTms>(new RectangleF(48, 159, 638, 275), new List<FaultInfoTms>(),
             new ListBoxHeader() { Text = "车号", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("宋体", 13), DataFont = new Font("宋体", 12, FontStyle.Bold), BackgroundBrush = Brushs.BlackBrush, Height = 25, Width = 84, Property = "CoachNo", SfData = FontInfo.SfCc, SfHeader = FontInfo.SfCc },
             new ListBoxHeader() { Text = "故障名称", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("宋体", 13), DataFont = new Font("宋体", 12f), BackgroundBrush = Brushs.BlackBrush, Height = 25, Width = 390, Property = "FaultName", SfData = FontInfo.SfLc, SfHeader = FontInfo.SfLc },
             new ListBoxHeader() { Text = "代码", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("宋体", 13), DataFont = new Font("宋体", 12f), BackgroundBrush = Brushs.BlackBrush, Height = 25, Width = 86, Property = "FaultCode", SfData = FontInfo.SfLc, SfHeader = FontInfo.SfLc },
             new ListBoxHeader() { Text = "等级", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("宋体", 13), DataFont = new Font("Arial", 12f), BackgroundBrush = Brushs.BlackBrush, Height = 25, Width = 75, Property = "FaultRank", SfData = FontInfo.SfLc, SfHeader = FontInfo.SfLc }
             );
            m_ListBox.RowCount = 5;
            FaultListManager.FaultInfoChanged += FaultListManager_FaultInfoChanged;
            m_ItemsCount = m_ListBox.DataList.Count;

            m_AllPageCount = (m_ItemsCount % m_ListBox.RowCount) > 0 ? (m_ItemsCount / m_ListBox.RowCount + 1) : (m_ItemsCount / m_ListBox.RowCount);

            if (m_AllPageCount == 0)
            {
                m_AllPageCount = 1;
            }

            m_PageStr = string.Format("{0}-{1}", m_CurrentPageIndex, m_AllPageCount);

            return true;
        }

        private void FaultListManager_FaultInfoChanged(Help.FaultInfo arg1, int arg2)
        {


            if (arg2 == 1)
            {
                m_ListBox.DataList.Add(new FaultInfoTms()
                {
                    CoachNo = arg1.CarNum,
                    FaultName = arg1.FaultName,
                    FaultCode = arg1.FaultCode,
                    FaultRank = arg1.Level.ToString()
                });
            }
            else
            {
                var tmp =
                    m_ListBox.DataList.FirstOrDefault(
                        f =>
                            f.FaultName == arg1.FaultName && f.CoachNo == arg1.CarNum && f.FaultCode == arg1.FaultCode &&
                            f.FaultRank == arg1.Level.ToString());
                if (tmp != null)
                {
                    m_ListBox.DataList.Remove(tmp);
                }
            }

            m_ItemsCount = m_ListBox.DataList.Count;

            m_AllPageCount = (m_ItemsCount % m_ListBox.RowCount) > 0 ? (m_ItemsCount / m_ListBox.RowCount + 1) : (m_ItemsCount / m_ListBox.RowCount);

            if (m_AllPageCount == 0)
            {
                m_AllPageCount = 1;
            }

            m_PageStr = string.Format("{0}-{1}", m_CurrentPageIndex, m_AllPageCount);
        }

        /// <summary>
        /// onGetFailureInfos
        /// </summary>
        //private void OnGetFailureInfos()
        //{
        //    var path = AppConfig.AppPaths.ConfigDirectory;
        //    var lfFileName = Path.Combine(path, "故障信息.xml");
        //    m_LfInfos = new LocomotiveFailures();
        //    m_LfInfos = LocomotiveFailureHelper.DeserializeLocomotiveFailures(lfFileName);
        //}
        #endregion

        #region 鼠标事件
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

            if ((nPoint.X >= m_ReturnArrowBtn.Rect.X)
                    && (nPoint.X <= m_ReturnArrowBtn.Rect.X + m_ReturnArrowBtn.Rect.Width)
                    && (nPoint.Y >= m_ReturnArrowBtn.Rect.Y)
                    && (nPoint.Y <= m_ReturnArrowBtn.Rect.Y + m_ReturnArrowBtn.Rect.Height))
            {
                m_ReturnArrowBtn.MouseUp(nPoint);
            }

            return base.mouseUp(nPoint);
        }

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

            if ((nPoint.X >= m_ReturnArrowBtn.Rect.X)
                   && (nPoint.X <= m_ReturnArrowBtn.Rect.X + m_ReturnArrowBtn.Rect.Width)
                   && (nPoint.Y >= m_ReturnArrowBtn.Rect.Y)
                   && (nPoint.Y <= m_ReturnArrowBtn.Rect.Y + m_ReturnArrowBtn.Rect.Height))
            {
                m_ReturnArrowBtn.MouseDown(nPoint);
            }
            return base.mouseDown(nPoint);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _ReturnArrowBtn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (e.Message == (int)ViewState.FiReturnArrow)
            {
                append_postCmd(CmdType.ChangePage, (int)CommonStatus.FaultReturnViewState, 0, 0);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void V111_MainFrame_Fault_C0_MainView_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (e.Message == (int)ViewState.FiUpBtn && m_CurrentPageIndex > 1)
            {
                m_CurrentPageIndex--;
                m_ListBox.LastPage();
            }
            else if (e.Message == (int)ViewState.FiDownBtn && m_CurrentPageIndex < m_AllPageCount)
            {
                m_CurrentPageIndex++;
                m_ListBox.NextPage();
            }
            m_PageStr = string.Format("{0}-{1}", m_CurrentPageIndex, m_AllPageCount);
        }

        private int m_CurrentFaultState = 0;
        /// <summary>
        /// down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _pageDownBtn_ClickEvent(object sender, ClickEventArgs<int> e)
        {

        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            dcGs.FillRectangle(m_BackgrounpSolidBrush, m_BackgrounpRectangle);
            m_ReturnArrowBtn.Paint(dcGs);
            dcGs.DrawString(m_PageStr, m_ChineseFont, Brushs.BlackBrush, m_PageRect, FontInfo.SfCc);
            dcGs.DrawImage(m_ResourceImage[2], m_FrameRect);
            dcGs.DrawString(m_ItemsCount.ToString(), m_DigitFont, Brushs.WhiteBrush, m_ShowAllCountRect, FontInfo.SfLc);

            for (m_I = 0; m_I < m_FrameStrRects.Length; m_I++)
            {
                dcGs.DrawString(m_FrameStrs[m_I], m_ChineseFont, Brushs.BlackBrush, m_FrameStrRects[m_I], FontInfo.SfLc);
            }

            for (m_I = 0; m_I < m_PageBtns.Length; m_I++)
            {
                m_PageBtns[m_I].Paint(dcGs);
            }
            m_ListBox.Paint(dcGs);


            base.paint(dcGs);
        }
        #endregion
    }
}
