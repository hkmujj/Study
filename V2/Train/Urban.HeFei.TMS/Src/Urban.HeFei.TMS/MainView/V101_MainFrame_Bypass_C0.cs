#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-21
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图101-运行-盘路信息-No.0
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using CommonUtil.Controls;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.HeFei.TMS.Common;
using Urban.HeFei.TMS.Resource;

namespace Urban.HeFei.TMS.MainView
{
    /// <summary>
    /// 功能描述：视图101- 旁路信息-No.0
    /// 创建人：lih
    /// 创建时间：2015-8-21
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V101MainFrameBypassC0 : baseClass
    {
        #region 私有变量
        private List<Image> m_ResourceImages = new List<Image>();//图片资源
        private string[] m_StrDishRoad;//盘路信息文本列表

        private Button m_PageUpBtn;  //向上选择按钮
        private Button m_PageDownBtn; //向下选择按钮


        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);

        private int m_I, m_J = 0;
        private SolidBrush m_RectBrush = new SolidBrush(Color.FromArgb(1, 116, 154));
        private Font m_DigitFont = new Font("Arial", 14);
        private Font m_ChineseFont = new Font("宋体", 14);
        private Font m_ChineseFontA = new Font("宋体", 14, FontStyle.Bold);
        private Rectangle m_PageRect = new Rectangle(710, 335, 56, 27);
        private SolidBrush m_BlackBrush = new SolidBrush(Color.Black);
        private Rectangle[] m_FrameRects;
        private Rectangle[] m_FrameNameRects;
        private string[] m_FrameStr;
        private int[] m_RectYs;
        private int[] m_RectXs;
        private Rectangle[] m_RectAll;

        private bool[] m_RectAllFlags;
        private string m_PageStr = "页1-2";
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "旁路信息-界面1";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V101_MainFrame_Bypass_C0";
        //}

        /// <summary>
        /// 初始化函数：初始化操作
        /// </summary>
        /// <param name="nErrorObjectIndex">错误索性</param>
        /// <returns>初始化是否成功</returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_FrameRects = new Rectangle[5];
            for (m_I = 0; m_I < m_FrameRects.Length; m_I++)
            {
                m_FrameRects[m_I] = new Rectangle(12, 262 + m_I * 50, 674, 45);
            }

            m_FrameStr = new string[5] { "DMPBS", "LMRGBS", "PBRBS", "ABRBS", "DOBS" };

            m_RectXs = new int[2] { 176, 606 };
            m_RectYs = new int[5];
            for (m_I = 0; m_I < m_RectYs.Length; m_I++)
            {
                m_RectYs[m_I] = 266 + m_I * 50;
            }

            m_RectAll = new Rectangle[10];
            m_RectAllFlags = new bool[10];
            for (m_I = 0; m_I < m_RectYs.Length; m_I++)
            {
                m_RectAllFlags[2 * m_I] = false;
                m_RectAllFlags[2 * m_I + 1] = false;
                m_RectAll[2 * m_I] = new Rectangle(m_RectXs[0], m_RectYs[m_I], 60, 37);
                m_RectAll[2 * m_I + 1] = new Rectangle(m_RectXs[1], m_RectYs[m_I], 60, 37);
            }


            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    m_ResourceImages.Add(Image.FromStream(fs));
                }
            });


            var bs1 = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImages[2], DownImage = m_ResourceImages[2] };
            var bs2 = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImages[3], DownImage = m_ResourceImages[3] };


            m_PageUpBtn = new Button(
                   "",
                   new RectangleF(713, 359, 57, 34),
                  (int)ViewState.BypassInfo,
                   bs1,
                   false
                   );
            m_PageUpBtn.ClickEvent += PageUpBtnClickEvent;//up

            m_PageDownBtn = new Button(
                  "",
                  new RectangleF(713, 398, 57, 34),
                 (int)ViewState.BypassInfo2,
                  bs2,
                  false
                  );
            m_PageDownBtn.ClickEvent += _pageDownBtn_ClickEvent;
            var tmpRec = new List<Point>();
            var tmpSize = new Size(60, 37);
            foreach (var t1 in m_RectYs)
            {
                tmpRec.AddRange(m_RectXs.Select(t => new Point(t, t1)));
            }
            List<string[]> tmpLogic = new List<string[]>();
            tmpLogic.Add(new[] { InBoolKeys.TC1警惕按钮旁路开关闭合, InBoolKeys.TC1警惕按钮旁路开关断开 });
            tmpLogic.Add(new[] { InBoolKeys.TC2警惕按钮旁路开关闭合, InBoolKeys.TC2警惕按钮旁路开关断开 });
            tmpLogic.Add(new[] { InBoolKeys.TC1主风欠压旁路开关闭合, InBoolKeys.TC1主风欠压旁路开关断开 });
            tmpLogic.Add(new[] { InBoolKeys.TC2主风欠压旁路开关闭合, InBoolKeys.TC2主风欠压旁路开关断开 });
            tmpLogic.Add(new[] { InBoolKeys.TC1停放制动缓解旁路开关闭合, InBoolKeys.TC1停放制动缓解旁路开关断开 });
            tmpLogic.Add(new[] { InBoolKeys.TC2停放制动缓解旁路开关闭合, InBoolKeys.TC2停放制动缓解旁路开关断开 });
            tmpLogic.Add(new[] { InBoolKeys.TC1所有常用制动缓解旁路开关闭合, InBoolKeys.TC1所有常用制动缓解旁路开关断开 });
            tmpLogic.Add(new[] { InBoolKeys.TC2所有常用制动缓解旁路开关闭合, InBoolKeys.TC2所有常用制动缓解旁路开关断开 });
            tmpLogic.Add(new[] { InBoolKeys.TC1门旁路开关闭合, InBoolKeys.TC1门旁路开关断开 });
            tmpLogic.Add(new[] { InBoolKeys.TC2门旁路开关闭合, InBoolKeys.TC2门旁路开关断开 });
            var tempImage = new[] { m_ResourceImages[0], m_ResourceImages[1], m_ResourceImages[4] };
            m_RecText = new List<GDIRectText>();
            for (var j = 0; j < tmpLogic.Count; j++)
            {
                //if (j > 1)
                {
                    m_RecText.Add(new GDIRectText()
                    {
                        OutLineRectangle = new Rectangle(tmpRec[j], tmpSize),
                        NeedDarwOutline = false,
                        BkColor = Color.Empty,
                        BKImage = m_ResourceImages[1],
                        BackColorVisible = true,
                        Tag = new object[] { tmpLogic[j], tempImage },
                        RefreshAction = o => RefreshImages((GDIRectText)o)
                    });
                }


            }
            return true;
        }
        private void RefreshImages(GDIRectText rectText)
        {
            var tmps = rectText;
            var tags = tmps.Tag as object[];
            var logic = tags[0] as string[];
            var image = tags[1] as Image[];
            var tmp = false;
            for (int i = 0; i < logic.Length; i++)
            {
                if (GetInBoolValue(logic[i]))
                {
                    tmp = true;
                    tmps.BKImage = image[i];
                    break;
                }
            }
            if (!tmp)
            {
                tmps.BKImage = image[image.Length - 1];
            }
        }
        #endregion

        private List<GDIRectText> m_RecText;
        #region 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {

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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PageUpBtnClickEvent(object sender, ClickEventArgs<int> e)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _pageDownBtn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            append_postCmd(CmdType.ChangePage, (int)ViewState.BypassInfo2, 0, 0);
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {

            m_PageUpBtn.Paint(dcGs);
            m_PageDownBtn.Paint(dcGs);

            dcGs.DrawString(m_PageStr, m_ChineseFontA, m_BlackBrush, m_PageRect, FontInfo.SfCc);

            for (m_I = 0; m_I < m_FrameRects.Length; m_I++)
            {
                dcGs.DrawRectangle(m_BlackLinePen, m_FrameRects[m_I]);
                dcGs.DrawString(m_FrameStr[m_I], m_ChineseFont, Brushs.BlackBrush, m_FrameRects[m_I], FontInfo.SfLc);
            }

            m_RecText.ForEach(f => f.OnPaint(dcGs));
            //for (m_I = 0; m_I < 5; m_I++)
            //{
            //    m_RectAllFlags[2 * m_I] = false;
            //    m_RectAllFlags[2 * m_I + 1] = false;
            //    for (m_J = 0; m_J < 2; m_J++)
            //    {
            //        if (BoolList[UIObj.InBoolList[2 * m_I] + m_J])
            //        {
            //            m_RectAllFlags[2 * m_I] = true;
            //            dcGs.DrawImage(m_ResourceImages[m_J], m_RectAll[2 * m_I]);
            //        }
            //        if (BoolList[UIObj.InBoolList[2 * m_I + 1] + m_J])
            //        {
            //            m_RectAllFlags[2 * m_I + 1] = true;
            //            dcGs.DrawImage(m_ResourceImages[m_J], m_RectAll[2 * m_I + 1]);
            //        }
            //    }

            //    if (m_RectAllFlags[2 * m_I] == false) { dcGs.DrawImage(m_ResourceImages[4], m_RectAll[2 * m_I]); }
            //    if (m_RectAllFlags[2 * m_I + 1] == false) { dcGs.DrawImage(m_ResourceImages[4], m_RectAll[2 * m_I + 1]); }
            //}

            base.paint(dcGs);
        }
        #endregion
    }
}
