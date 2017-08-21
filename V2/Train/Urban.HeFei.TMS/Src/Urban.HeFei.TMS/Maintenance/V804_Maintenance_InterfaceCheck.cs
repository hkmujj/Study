#region 文件说明
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

namespace Urban.HeFei.TMS.Maintenance
{
    /// <summary>
    /// 功能描述：维护-No.4-接口检查
    /// 创建人：lih
    /// 创建时间：2015-08-06
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V804MaintenanceInterfaceCheck:baseClass
    {
        #region 私有属性
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private Rectangle m_FrameRect = new Rectangle(20, 112, 640, 320);
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private Font m_ChineseFont = new Font("宋体", 16);
        private Font m_DigitFont = new Font("Arial", 16);
        private ListBox<InterfaceCheck> m_Listbox;
        private Button [] m_Btns;
        private Rectangle m_PageRect;
        private string[] m_BtnNames;
        private int m_I = 0;

        private Button m_PageUpBtn;
        private Button m_PageDownBtn;
        private string m_PageStr;
        private int m_CurrentPageIndex = 1;
        private int m_PageCount = 1;
        private string m_ListStr;
        private Rectangle m_ListRect;
        #endregion 


        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "维护-接口检查";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V804_Maintenance_InterfaceCheck";
        //}

        /// <summary>
        /// 初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {

            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });

            m_BtnNames = new string[3] { "输入", "输出", "选择" };

             var bs   = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[0], DownImage = m_ResourceImage[1] };
           
            m_Btns = new Button[3];
            m_Btns[0] = new Button(m_BtnNames[0], new RectangleF(31, 441, 83, 48), (int)ViewState.MtIInputBtn, bs, true);
            m_Btns[1] = new Button(m_BtnNames[1], new RectangleF(125, 441, 83, 48), (int)ViewState.MtIOutputBtn, bs, true);
            m_Btns[2] = new Button(m_BtnNames[2], new RectangleF(300, 441, 83, 48), (int)ViewState.MtISelectBtn, bs, true);

            for (m_I = 0; m_I < m_Btns.Length; m_I++)
            {
                m_Btns[m_I].ClickEvent += V804_Maintenance_InterfaceCheck_ClickEvent;
            }

                m_PageRect = new Rectangle(700, 292, 70, 25);


            var bsUp = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[3], DownImage = m_ResourceImage[3] };
            var bsdown = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[4], DownImage = m_ResourceImage[4] };

            m_PageUpBtn = new Button("", new RectangleF(700, 320, 70, 45), (int)ViewState.MtIPageBtnUp, bsUp, false);
            m_PageDownBtn = new Button("", new RectangleF(700, 370, 70, 45), (int)ViewState.MtIPageBtnDown, bsdown, false);

            m_PageUpBtn.ClickEvent += V804_Maintenance_InterfaceCheck_ClickEvent;
            m_PageDownBtn.ClickEvent += V804_Maintenance_InterfaceCheck_ClickEvent;

            m_Listbox = new ListBox<InterfaceCheck>(new RectangleF(24, 157, 639, 274), new List<InterfaceCheck>(),
               new ListBoxHeader() { Text = "地址", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("宋体", 14), DataFont = new Font("Arial", 14, FontStyle.Regular), BackgroundBrush = Brushs.BlackBrush, Height = 25, Width = 210, Property = "ICAddress", SfData = FontInfo.SfCc, SfHeader = FontInfo.SfCc },
               new ListBoxHeader() { Text = "信号标识", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("宋体", 14), DataFont = new Font("Arial", 14f), BackgroundBrush = Brushs.BlackBrush, Height = 25, Width = 210, Property = "ICSignalFlag", SfData = FontInfo.SfCc, SfHeader = FontInfo.SfCc },
               new ListBoxHeader() { Text = "值", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("宋体", 14), DataFont = new Font("Arial", 14f), BackgroundBrush = Brushs.BlackBrush, Height = 25, Width = 210, Property = "ICValue", SfData = FontInfo.SfCc, SfHeader = FontInfo.SfCc }
               );

            m_Listbox.RowCount = 4;

            var ic1 = new InterfaceCheck() { IcAddress = "C1_DI1_0", IcSignalFlag = "ZVR1", IcValue = "0" };
            var ic2 = new InterfaceCheck() { IcAddress = "C1_DI1_1", IcSignalFlag = "ZVR2", IcValue = "1" };
            var ic3 = new InterfaceCheck() { IcAddress = "C1_DI1_2", IcSignalFlag = "ZVR3", IcValue = "0" };
            var ic4 = new InterfaceCheck() { IcAddress = "C1_DI1_3", IcSignalFlag = "ZVR4", IcValue = "2" };

            m_Listbox.DataList.Add(ic1);
            m_Listbox.DataList.Add(ic2);
            m_Listbox.DataList.Add(ic3);
            m_Listbox.DataList.Add(ic4);
            

            m_PageCount = (m_Listbox.DataList.Count % m_Listbox.RowCount) > 0 ? (m_Listbox.DataList.Count / m_Listbox.RowCount + 1) : (m_Listbox.DataList.Count / m_Listbox.RowCount);

            m_ListStr = "RIOM10输入";
            m_ListRect = new Rectangle(84, 118, 120, 45);
            m_PageStr = string.Format("{0}-{1}", m_CurrentPageIndex, m_PageCount);


            return true;
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
            //判断点击的位置是否属于该button集合
            for (m_I = 0; m_I < m_Btns.Length; m_I++)
            {
                if ((nPoint.X >= m_Btns[m_I].Rect.X)
                       && (nPoint.X <= m_Btns[m_I].Rect.X + m_Btns[m_I].Rect.Width)
                       && (nPoint.Y >= m_Btns[m_I].Rect.Y)
                       && (nPoint.Y <= m_Btns[m_I].Rect.Y + m_Btns[m_I].Rect.Height))
                {
                    m_Btns[m_I].MouseDown(nPoint);
                    break;
                }
            }
                if ((nPoint.X >= m_PageUpBtn.Rect.X)
                       && (nPoint.X <= m_PageUpBtn.Rect.X + m_PageUpBtn.Rect.Width)
                       && (nPoint.Y >= m_PageUpBtn.Rect.Y)
                       && (nPoint.Y <= m_PageUpBtn.Rect.Y + m_PageUpBtn.Rect.Height))
                {
                    m_PageUpBtn.MouseDown(nPoint);
                }
            if ((nPoint.X >= m_PageDownBtn.Rect.X)
                 && (nPoint.X <= m_PageDownBtn.Rect.X + m_PageDownBtn.Rect.Width)
                 && (nPoint.Y >= m_PageDownBtn.Rect.Y)
                 && (nPoint.Y <= m_PageDownBtn.Rect.Y + m_PageDownBtn.Rect.Height))
            {
                m_PageDownBtn.MouseDown(nPoint);
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

            for (m_I = 0; m_I < m_Btns.Length; m_I++)
            {
                if ((nPoint.X >= m_Btns[m_I].Rect.X)
                       && (nPoint.X <= m_Btns[m_I].Rect.X + m_Btns[m_I].Rect.Width)
                       && (nPoint.Y >= m_Btns[m_I].Rect.Y)
                       && (nPoint.Y <= m_Btns[m_I].Rect.Y + m_Btns[m_I].Rect.Height))
                {
                    m_Btns[m_I].MouseUp(nPoint);
                    break;
                }
            }

            if ((nPoint.X >= m_PageUpBtn.Rect.X)
                    && (nPoint.X <= m_PageUpBtn.Rect.X + m_PageUpBtn.Rect.Width)
                    && (nPoint.Y >= m_PageUpBtn.Rect.Y)
                    && (nPoint.Y <= m_PageUpBtn.Rect.Y + m_PageUpBtn.Rect.Height))
            {
                m_PageUpBtn.MouseUp(nPoint);
            }
            if ((nPoint.X >= m_PageDownBtn.Rect.X)
                   && (nPoint.X <= m_PageDownBtn.Rect.X + m_PageDownBtn.Rect.Width)
                   && (nPoint.Y >= m_PageDownBtn.Rect.Y)
                   && (nPoint.Y <= m_PageDownBtn.Rect.Y + m_PageDownBtn.Rect.Height))
            {
                m_PageDownBtn.MouseUp(nPoint);
            }

            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void V804_Maintenance_InterfaceCheck_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            ViewState tempViewState;
            if (!Enum.TryParse(e.Message.ToString(), out tempViewState))
            {
                return;
            }
            switch (tempViewState)
            {
                case ViewState.MtIInputBtn:
                    break;
                case ViewState.MtIOutputBtn:
                    break;
                case ViewState.MtISelectBtn:
                    break;
                case ViewState.MtIPageBtnUp:
                    if (m_CurrentPageIndex > 1)
                    {
                        m_CurrentPageIndex--;
                        m_Listbox.LastPage();
                        m_PageStr = string.Format("{0}-{1}", m_CurrentPageIndex, m_PageCount);
                    }
                    break;
                case ViewState.MtIPageBtnDown:
                    if (m_CurrentPageIndex < m_PageCount)
                    {
                        m_CurrentPageIndex++;
                        m_Listbox.NextPage();
                        m_PageStr = string.Format("{0}-{1}", m_CurrentPageIndex, m_PageCount);
                    }
                    break;
                default: break;
            }
        }


        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {

            dcGs.DrawImage(m_ResourceImage[2], m_FrameRect);

            dcGs.DrawString(m_ListStr, m_ChineseFont, Brushs.WhiteBrush, m_ListRect, FontInfo.SfLc);

            for (m_I = 0; m_I < m_Btns.Length; m_I++)
            {
                m_Btns[m_I].Paint(dcGs);
            }

            m_Listbox.Paint(dcGs);

            m_PageUpBtn.Paint(dcGs);
            m_PageDownBtn.Paint(dcGs);

            dcGs.DrawString(m_PageStr, m_DigitFont, Brushs.BlackBrush, m_PageRect, FontInfo.SfCc);

            base.paint(dcGs);
        }
        #endregion
    }
}
