#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-25
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图604-帮助-通讯帮助-No.0
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.HeFei.TMS.Common;
using Urban.HeFei.TMS.Resource;

namespace Urban.HeFei.TMS.PIS
{
    /// <summary>
    /// 功能描述：视图602-帮助-车辆帮助-No.0
    /// 创建人：lih
    /// 创建时间：2015-8-25
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V605PISSpecialInfo : baseClass
    {
        #region 私有变量
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private Font m_ChineseFont = new Font("宋体", 18);
        private Button[] m_Btns;//按钮列表
        private int m_I = 0;
        private Rectangle m_PageRect = new Rectangle(716, 195, 70, 27);
        private string m_PageStr;

        private string m_FrameStr = "应急广播清单";
        private Rectangle m_FrameStrRect = new Rectangle(32, 116, 160, 30);
        private string[] m_BtnNames;
        private ViewState m_CurrentViewState;

        private Button[] m_PageBtns;

        private List<string> m_Items = new List<string>();
        private ListBox<string> m_SpecialInfos;

        private Button m_ReturnArrowBtn;
        private SolidBrush m_BackgrounpSolidBrush;
        private Rectangle m_BackgrounpRectangle;
        private int m_CurrentPageIndex = 1;
        private int m_AllPageCount = 1;

        private int m_BfModel = 0;
        private int m_BfCount = 0;
        private int m_LastClickIndex = -1;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "PIS-特殊信息";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V605_PIS_SpecialInfo";
        //}

        /// <summary>
        /// 初始化函数：初始化操作
        /// </summary>
        /// <param name="nErrorObjectIndex">错误索性</param>
        /// <returns>初始化是否成功</returns>
        public override bool init(ref int nErrorObjectIndex)
        {

            m_BackgrounpSolidBrush = new SolidBrush(Color.FromArgb(215, 215, 215));
            m_BackgrounpRectangle = new Rectangle(0, 516, 800, 84);

            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });
            m_Items =
                File.ReadLines(Path.Combine(AppPaths.ConfigDirectory, "紧急广播信息.txt"), Encoding.Default)
                    .Where(w => !w.StartsWith(";;;;"))
                    .ToList();
            //for (m_I = 1; m_I <= 21; m_I++)
            //{
            //    m_Items.Add("特殊信息" + (m_I.ToString()));
            //}

            var listHeader = new ListBoxHeader[1];
            listHeader[0] = new ListBoxHeader();
            listHeader[0].DataFont = m_ChineseFont;
            listHeader[0].SfData = FontInfo.SfLc;
            listHeader[0].Width = 674;
            listHeader[0].Height = 25;
            listHeader[0].TextBrush = Brushs.WhiteBrush;

            m_SpecialInfos = new ListBox<string>(new Rectangle(12, 147, 674, 251), m_Items, listHeader);
            m_SpecialInfos.BackgroundBrush = Brushs.BlackBrush;
            m_SpecialInfos.GridBrush = Brushs.WhiteBrush;
            m_SpecialInfos.HideHeader = true;
            m_SpecialInfos.RowCount = 7;
            m_SpecialInfos.SelectedItemBrush = new SolidBrush(Color.FromArgb(215, 215, 215));

            m_AllPageCount = (m_Items.Count % m_SpecialInfos.RowCount) > 0 ? (m_Items.Count / m_SpecialInfos.RowCount + 1) : (m_Items.Count / m_SpecialInfos.RowCount);

            m_BtnNames = new string[2] { "单次", "循环" };
            var bs1 = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[0], DownImage = m_ResourceImage[1] };
            m_Btns = new Button[2];
            m_Btns[0] = new Button(m_BtnNames[0], new Rectangle(266, 438, 88, 50), (int)ViewState.PissSingleBtn, bs1, true);
            m_Btns[1] = new Button(m_BtnNames[1], new Rectangle(374, 438, 88, 50), (int)ViewState.PissCycleBtn, bs1, true);

            m_Btns[0].ClickEvent += V605_PIS_SpecialInfo_ClickEvent;
            m_Btns[1].ClickEvent += V605_PIS_SpecialInfo_ClickEvent;

            var bsUp = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[2], DownImage = m_ResourceImage[2] };
            var bsDown = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[3], DownImage = m_ResourceImage[3] };

            m_PageBtns = new Button[2];
            m_PageBtns[0] = new Button("", new Rectangle(718, 220, 70, 34), (int)ViewState.PissUpBtn, bsUp, false);
            m_PageBtns[1] = new Button("", new Rectangle(718, 258, 70, 34), (int)ViewState.PissDownBtn, bsDown, false);
            m_PageBtns[0].ClickEvent += V605_PIS_SpecialInfo_ClickEvent;
            m_PageBtns[1].ClickEvent += V605_PIS_SpecialInfo_ClickEvent;

            var bsReturnArrow = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[4], DownImage = m_ResourceImage[4] };
            m_ReturnArrowBtn = new Button("", new Rectangle(668, 520, 109, 75), (int)ViewState.PissReturnArrow, bsReturnArrow, false);
            m_ReturnArrowBtn.ClickEvent += V605_PIS_SpecialInfo_ClickEvent;

            m_PageStr = string.Format("{0}-{1}", m_CurrentPageIndex, m_AllPageCount);

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

            if ((nPoint.X >= m_SpecialInfos.Rect.X)
                      && (nPoint.X <= m_SpecialInfos.Rect.X + m_SpecialInfos.Rect.Width)
                      && (nPoint.Y >= m_SpecialInfos.Rect.Y)
                      && (nPoint.Y <= m_SpecialInfos.Rect.Y + m_SpecialInfos.Rect.Height))
            {
                m_SpecialInfos.MouseDown(nPoint);
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

            if ((nPoint.X >= m_SpecialInfos.Rect.X)
                      && (nPoint.X <= m_SpecialInfos.Rect.X + m_SpecialInfos.Rect.Width)
                      && (nPoint.Y >= m_SpecialInfos.Rect.Y)
                      && (nPoint.Y <= m_SpecialInfos.Rect.Y + m_SpecialInfos.Rect.Height))
            {
                m_SpecialInfos.MouseUp(nPoint);
            }
            if (GetOutBoolValue(OutBoolKeys.广播模式单次))
            {
                append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.广播模式单次), 0, 0);
            }
            if (GetOutBoolValue(OutBoolKeys.广播模式循环))
            {
                append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.广播模式循环), 0, 0);
            }
            return base.mouseUp(nPoint);
        }


        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void V605_PIS_SpecialInfo_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if ((int)ViewState.PissSingleBtn == e.Message)
            {
                m_BfModel = 1;
                append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.广播模式单次), 1, 0);
            }
            if ((int)ViewState.PissCycleBtn == e.Message)
            {
                m_BfModel = 2;
                append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.广播模式循环), 1, 0);
            }
            else if ((int)ViewState.PissUpBtn == e.Message
                || (int)ViewState.PissDownBtn == e.Message)
            {
                if ((int)ViewState.PissUpBtn == e.Message
                    && m_CurrentPageIndex > 1)
                {
                    m_CurrentPageIndex--;
                    m_SpecialInfos.LastPage();
                }
                else if ((int)ViewState.PissDownBtn == e.Message
                    && m_CurrentPageIndex < m_AllPageCount)
                {
                    m_CurrentPageIndex++;
                    m_SpecialInfos.NextPage();
                }
            }
            else if ((int)ViewState.PissReturnArrow == e.Message)
            {
                append_postCmd(CmdType.ChangePage, (int)CommonStatus.BroadcastReturnViewState, 0, 0);
            }

            m_PageStr = string.Format("{0}-{1}", m_CurrentPageIndex, m_AllPageCount);
        }

        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {

            dcGs.DrawString(m_FrameStr, m_ChineseFont, Brushs.BlackBrush, m_FrameStrRect, FontInfo.SfLc);

            dcGs.DrawString(m_PageStr, m_ChineseFont, Brushs.BlackBrush, m_PageRect, FontInfo.SfCc);

            for (m_I = 0; m_I < m_Btns.Length; m_I++)
            {
                m_Btns[m_I].Paint(dcGs);
            }

            for (m_I = 0; m_I < m_PageBtns.Length; m_I++)
            {
                m_PageBtns[m_I].Paint(dcGs);
            }
            m_SpecialInfos.Paint(dcGs);

            dcGs.FillRectangle(m_BackgrounpSolidBrush, m_BackgrounpRectangle);
            m_ReturnArrowBtn.Paint(dcGs);

            OnSendData();

            base.paint(dcGs);
        }

        /// <summary>
        /// onSendData
        /// </summary>
        private void OnSendData()
        {
            if (m_LastClickIndex != m_SpecialInfos.SelectedIndex)
            {
                m_BfCount = 0;
            }
            if (m_BfModel == 1)
            {
                if (m_BfCount == 0 && m_SpecialInfos.SelectedIndex != -1)
                {
                    append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[0] + CommonStatus.SdFloatBaseNumber, 0, (float)m_SpecialInfos.SelectedIndex);
                    m_BfCount++;
                }
            }
            else if (m_BfModel == 2)
            {
                if (m_SpecialInfos.SelectedIndex != -1)
                {
                    append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[0] + CommonStatus.SdFloatBaseNumber, 0, (float)m_SpecialInfos.SelectedIndex);
                }
            }
        }
        #endregion
    }
}
