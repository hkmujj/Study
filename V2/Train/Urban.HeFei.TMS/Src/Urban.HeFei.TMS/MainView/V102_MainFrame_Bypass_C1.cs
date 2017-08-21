#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-21
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图102-运行-故障-No.0-主界面
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Util.Extension;
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
    /// 功能描述：视图102-旁路信息-No.1-主界面2
    /// 创建人：lih
    /// 创建时间：2015-8-21
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V102MainFrameBypassC1 : baseClass
    {
        #region 私有变量
        private ListBox<FaultInfoTms> m_ListBox;//列表控件
        private List<Image> m_ResourceImage = new List<Image>();//图片资源

        private bool m_IsOperate = false;//是否操作

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
        private string m_PageStr = "页2-2";
        private Rectangle[] m_Rect1;
        private Rectangle[] m_Rect2;
        private Rectangle[] m_Rect3;
        private Rectangle[] m_RectStr3;
        private Rectangle[] m_Rect4;
        private string[] m_CstStrs;

        private bool[] m_Rect1Flags;
        private bool[] m_Rect2Flags;
        private bool[] m_Rect3Flags;
        private bool[] m_Rect4Flags;


        private string[] m_CtsLogicName = new[]
        {
            InBoolKeys.TC1列车连挂开关N, InBoolKeys.TC1列车连挂开关CTE, InBoolKeys.TC1列车连挂开关PBA, InBoolKeys.TC2列车连挂开关N,
            InBoolKeys.TC2列车连挂开关CTE, InBoolKeys.TC2列车连挂开关PBA
        };

        private List<GDIRectText> m_RecText;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "旁路信息-界面2";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V102_MainFrame_Bypass_C1";
        //}

        /// <summary>
        /// 初始化函数：导入图片、创建控件、列表框
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {

            m_RectXs = new int[2] { 175, 607 };
            m_RectYs = new int[4] { 266, 318, 370, 422 };

            m_FrameRects = new Rectangle[4];
            for (m_I = 0; m_I < m_FrameRects.Length; m_I++)
            {
                m_FrameRects[m_I] = new Rectangle(12, 264 + m_I * 52, 674, 48);
            }


            m_FrameStr = new string[4] { "CTS", "ADDBS", "ATCIS", "HTS" };
            m_CstStrs = new string[3] { "N", "CTE", "PBA" };


            m_Rect1 = new Rectangle[2];
            m_Rect1[0] = new Rectangle(m_RectXs[0], m_RectYs[0], 60, 44);
            m_Rect1[1] = new Rectangle(m_RectXs[1], m_RectYs[0], 60, 44);

            m_Rect2 = new Rectangle[2];
            m_Rect2[0] = new Rectangle(m_RectXs[0], m_RectYs[1], 60, 44);
            m_Rect2[1] = new Rectangle(m_RectXs[1], m_RectYs[1], 60, 44);

            m_Rect3 = new Rectangle[2];
            m_RectStr3 = new Rectangle[2];
            m_Rect3[0] = new Rectangle(m_RectXs[0] + 2, m_RectYs[2], 54, 38);
            m_Rect3[1] = new Rectangle(m_RectXs[1] + 2, m_RectYs[2], 54, 38);
            m_RectStr3[0] = new Rectangle(m_RectXs[0] + 3, m_RectYs[2] + 1, 53, 37);
            m_RectStr3[1] = new Rectangle(m_RectXs[1] + 3, m_RectYs[2] + 1, 53, 37);


            m_Rect4 = new Rectangle[2];
            m_Rect4[0] = new Rectangle(m_RectXs[0], m_RectYs[3], 60, 44);
            m_Rect4[1] = new Rectangle(m_RectXs[1], m_RectYs[3], 60, 44);

            m_Rect1Flags = new bool[2] { false, false };
            m_Rect2Flags = new bool[2] { false, false };
            m_Rect3Flags = new bool[2] { false, false };
            m_Rect4Flags = new bool[2] { false, false };

            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });


            var bs1 = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[2], DownImage = m_ResourceImage[2] };
            var bs2 = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[3], DownImage = m_ResourceImage[3] };


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
            foreach (var t1 in m_RectYs)
            {
                tmpRec.AddRange(m_RectXs.Select(t => new Point(t, t1)));
            }
            var tmpSize = new Size(60, 44);
            List<string[]> tmpLogic = new List<string[]>();
            tmpLogic.Add(new[] { InBoolKeys.TC1列车连挂开关N, InBoolKeys.TC1列车连挂开关CTE, InBoolKeys.TC1列车连挂开关PBA });
            tmpLogic.Add(new[] { InBoolKeys.TC2列车连挂开关N, InBoolKeys.TC2列车连挂开关CTE, InBoolKeys.TC2列车连挂开关PBA });
            tmpLogic.Add(new[] { InBoolKeys.TC1自动降弓旁路开关闭合, InBoolKeys.TC1自动降弓旁路开关断开 });
            tmpLogic.Add(new[] { InBoolKeys.TC2自动降弓旁路开关闭合, InBoolKeys.TC2自动降弓旁路开关断开 });
            tmpLogic.Add(new[] { InBoolKeys.TC1ATC切除开关闭合, InBoolKeys.TC1ATC切除开关断开 });
            tmpLogic.Add(new[] { InBoolKeys.TC2ATC切除开关闭合, InBoolKeys.TC2ATC切除开关断开 });
            tmpLogic.Add(new[] { InBoolKeys.TC1半车开关闭合, InBoolKeys.TC1半车开关断开 });
            tmpLogic.Add(new[] { InBoolKeys.TC2半车开关闭合, InBoolKeys.TC2半车开关断开 });

            var tmp1 = tmpRec[tmpRec.Count - 2];
            var tmp2 = tmpRec[tmpRec.Count - 1];

            var leng1 = ((tmp2.X - tmp1.X) - (tmpSize.Width - 10) * 2) / 2;

            tmp1.Offset(leng1, 0);
            tmp2.Offset(-leng1, 0);
            tmpRec[tmpRec.Count - 2] = tmp1;
            tmpRec[tmpRec.Count - 1] = tmp2;



            var tempImage = new[] { m_ResourceImage[0], m_ResourceImage[1], m_ResourceImage[4] };
            m_RecText = new List<GDIRectText>();

            for (var j = 0; j < tmpLogic.Count; j++)
            {
                if (j > 1)
                {
                    m_RecText.Add(new GDIRectText()
                    {
                        OutLineRectangle = new Rectangle(tmpRec[j], tmpSize),
                        NeedDarwOutline = false,
                        BkColor = Color.Empty,
                        BKImage = m_ResourceImage[1],
                        BackColorVisible = true,
                        Tag = new object[] { tmpLogic[j], tempImage },
                        RefreshAction = o => RefreshImages((GDIRectText)o)
                    });
                }
                else
                {
                    m_RecText.Add(new GDIRectText()
                    {
                        OutLineRectangle = new Rectangle(tmpRec[j], tmpSize),
                        OutLinePen = m_BlackLinePen,
                        NeedDarwOutline = false,
                        BKBrush = m_RectBrush,
                        TextFormat = FontInfo.SfCc,
                        DrawFont = m_DigitFont,
                        TextBrush = Brushs.BlackBrush,
                        BKImage = m_ResourceImage[4],
                        BackColorVisible = true,
                        Tag = new object[] { tmpLogic[j], m_CstStrs },
                        RefreshAction = o => RefreshText((GDIRectText)o)
                    });
                }

            }
            return true;
        }
        private readonly Brush m_EmptyBrush = new SolidBrush(Color.Empty);
        private void RefreshText(GDIRectText text)
        {
            var tags = text.Tag as object[];
            var logic = tags[0] as string[];
            var str = tags[1] as string[];

            for (int i = 0; i < logic.Length; i++)
            {
                if (GetInBoolValue(logic[i]))
                {
                    text.Text = str[i];
                    text.BKImage = null;
                    text.BKBrush = m_RectBrush;
                    break;
                }
                else
                {
                    text.Text = string.Empty;
                    text.BKImage = m_ResourceImage[4];
                    text.BKBrush = m_EmptyBrush;
                    text.NeedDarwOutline = false;
                }
            }
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

        private Action<object> RefreshImage()
        {
            return (o =>
            {


            });
        }

        #endregion

        #region 鼠标事件
        /// <summary>
        /// mouseUp
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
        /// mouseDown
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
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
        /// down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _pageDownBtn_ClickEvent(object sender, ClickEventArgs<int> e)
        {

        }

        /// <summary>
        /// up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PageUpBtnClickEvent(object sender, ClickEventArgs<int> e)
        {
            append_postCmd(CmdType.ChangePage, (int)ViewState.BypassInfo, 0, 0);
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 
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
                dcGs.DrawString(m_FrameStr[m_I], m_DigitFont, Brushs.BlackBrush, m_FrameRects[m_I], FontInfo.SfLc);
            }
            m_RecText.ForEach(f => f.OnPaint(dcGs));
            //for (m_I = 0; m_I < m_Rect1.Length; m_I++)
            //{
            //    m_Rect1Flags[m_I] = false;
            //    for (m_J = 0; m_J < 2; m_J++)
            //    {
            //        if (BoolList[UIObj.InBoolList[m_I] + m_J])
            //        {
            //            m_Rect1Flags[m_I] = true;
            //            dcGs.DrawImage(m_ResourceImage[m_J], m_Rect1[m_I]);
            //        }
            //    }
            //    if (m_Rect1Flags[m_I] == false) { dcGs.DrawImage(m_ResourceImage[4], m_Rect1[m_I]); }
            //}

            //for (m_I = 0; m_I < m_Rect2.Length; m_I++)
            //{
            //    m_Rect2Flags[m_I] = false;
            //    for (m_J = 0; m_J < 2; m_J++)
            //    {
            //        if (BoolList[UIObj.InBoolList[m_I + 2] + m_J])
            //        {
            //            m_Rect2Flags[m_I] = true;
            //            dcGs.DrawImage(m_ResourceImage[m_J], m_Rect2[m_I]);
            //        }
            //    }
            //    if (m_Rect2Flags[m_I] == false) { dcGs.DrawImage(m_ResourceImage[4], m_Rect2[m_I]); }
            //}

            //for (m_I = 0; m_I < m_Rect3.Length; m_I++)
            //{
            //    m_Rect3Flags[m_I] = false;

            //    for (m_J = 0; m_J < 3; m_J++)
            //    {
            //        if (BoolList[UIObj.InBoolList[m_I + 4] + m_J])
            //        {
            //            m_Rect3Flags[m_I] = true;
            //            dcGs.DrawRectangle(m_BlackLinePen, m_Rect3[m_I]);
            //            dcGs.FillRectangle(m_RectBrush, m_RectStr3[m_I]);
            //            dcGs.DrawString(m_CstStrs[m_J], m_DigitFont, Brushs.m_BlackBrush, m_Rect3[m_I], FontInfo.m_SfCc);
            //        }
            //    }


            //}
            //for (m_I = 0; m_I < m_Rect3.Length; m_I++)
            //{
            //    if (m_Rect3Flags[m_I] == false)
            //    {
            //        dcGs.DrawImage(m_ResourceImage[4], m_Rect3[m_I]);
            //    }

            //}

            //for (m_I = 0; m_I < m_Rect4.Length; m_I++)
            //{
            //    m_Rect4Flags[m_I] = false;
            //    for (m_J = 0; m_J < 2; m_J++)
            //    {
            //        if (BoolList[UIObj.InBoolList[m_I + 6] + m_J])
            //        {
            //            m_Rect4Flags[m_I] = true;
            //            dcGs.DrawImage(m_ResourceImage[m_J], m_Rect4[m_I]);
            //        }
            //    }
            //    if (m_Rect4Flags[m_I] == false) { dcGs.DrawImage(m_ResourceImage[4], m_Rect4[m_I]); }
            //}
            base.paint(dcGs);
        }
        #endregion
    }
}
