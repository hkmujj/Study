#region 文件说明
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

namespace Urban.HeFei.TMS.Assist
{
    /// <summary>
    /// 功能描述：视图4-辅助-No.0-页面1
    /// 创建人：lih
    /// 创建时间：2015-8-24
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V4AssistC0Page1 : baseClass
    {
        #region 私有变量
        private List<Image> m_ResourceImages = new List<Image>();//图片资源
        private List<Button> m_BtnsDownTabView = new List<Button>();//按钮列表
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
        private string m_PageStr = "页1-2";

        private int[] m_RectYs;
        private int[] m_RectXs;

        private Rectangle[] m_Rect1;
        private Rectangle[] m_Rect2;
        private Rectangle[] m_Rect3;
        private Rectangle[] m_Rect4;
        private Rectangle[] m_FillRect4;
        private Rectangle[] m_Rect5;
        private Rectangle[] m_FillRect5;

        private bool[] m_Rect3Flags;
        private string[] m_Rect4Strs;
        private string[] m_Rect5Strs;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "辅助-页面1";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V4_Assist_C0_Page1";
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
            m_FrameStr = new string[5] { "", "", "列车供电接触器", "直流110V", "中压" };

            m_RectYs = new int[5];
            for (m_I = 0; m_I < m_RectYs.Length; m_I++)
            {
                m_RectYs[m_I] = 266 + m_I * 50;
            }
            m_RectXs = new int[6];
            for (m_I = 0; m_I < m_RectXs.Length; m_I++)
            {
                m_RectXs[m_I] = 175 + m_I * 86;
            }

            m_Rect1 = new Rectangle[2];
            m_Rect1[0] = new Rectangle(m_RectXs[0], m_RectYs[2], 60, 36);
            m_Rect1[1] = new Rectangle(m_RectXs[5], m_RectYs[2], 60, 36);

            m_Rect2 = new Rectangle[6];
            for (m_I = 0; m_I < m_RectXs.Length; m_I++)
            {
                m_Rect2[m_I] = new Rectangle(m_RectXs[m_I], m_RectYs[1], 60, 36);
            }
            m_Rect3 = new Rectangle[4];
            m_Rect3[0] = new Rectangle(m_RectXs[0], m_RectYs[2], 60, 36);
            m_Rect3[1] = new Rectangle(m_RectXs[2], m_RectYs[2], 60, 36);
            m_Rect3[2] = new Rectangle(m_RectXs[3], m_RectYs[2], 60, 36);
            m_Rect3[3] = new Rectangle(m_RectXs[5], m_RectYs[2], 60, 36);
            m_Rect3Flags = new bool[4] { false, false, false, false };

            m_Rect4 = new Rectangle[2];
            m_FillRect4 = new Rectangle[2];
            m_Rect4[0] = new Rectangle(m_RectXs[0], m_RectYs[3], 60, 36);
            m_Rect4[1] = new Rectangle(m_RectXs[5], m_RectYs[3], 60, 36);
            m_FillRect4[0] = new Rectangle(m_RectXs[0] + 1, m_RectYs[3] + 1, 59, 35);
            m_FillRect4[1] = new Rectangle(m_RectXs[5] + 1, m_RectYs[3] + 1, 59, 35);

            m_Rect4Strs = new string[2] { "", "" };

            m_Rect5 = new Rectangle[4];
            m_FillRect5 = new Rectangle[4];
            m_Rect5[0] = new Rectangle(m_RectXs[0], m_RectYs[4], 60, 36);
            m_Rect5[1] = new Rectangle(m_RectXs[2], m_RectYs[4], 60, 36);
            m_Rect5[2] = new Rectangle(m_RectXs[3], m_RectYs[4], 60, 36);
            m_Rect5[3] = new Rectangle(m_RectXs[5], m_RectYs[4], 60, 36);

            m_FillRect5[0] = new Rectangle(m_RectXs[0] + 1, m_RectYs[4] + 1, 59, 35);
            m_FillRect5[1] = new Rectangle(m_RectXs[2] + 1, m_RectYs[4] + 1, 59, 35);
            m_FillRect5[2] = new Rectangle(m_RectXs[3] + 1, m_RectYs[4] + 1, 59, 35);
            m_FillRect5[3] = new Rectangle(m_RectXs[5] + 1, m_RectYs[4] + 1, 59, 35);
            m_Rect5Strs = new string[5] { "", "", "", "", "" };


            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    m_ResourceImages.Add(Image.FromStream(fs));
                }
            });

            var bs1 = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImages[0], DownImage = m_ResourceImages[0] };

            var btn1 = new Button(
                  "",
                  new RectangleF(713, 359, 57, 34),
                 3,
                  bs1,
                  false
                  );
            btn1.ClickEvent += btn1_ClickEvent;//up
            m_BtnsDownTabView.Add(btn1);

            return true;
        }
        #endregion

        #region 鼠标事件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            var flag = false;
            for (var i = 0; i < m_BtnsDownTabView.Count; i++)
            {
                if ((nPoint.X >= m_BtnsDownTabView[i].Rect.X)
                    && (nPoint.X <= m_BtnsDownTabView[i].Rect.X + m_BtnsDownTabView[i].Rect.Width)
                    && (nPoint.Y >= m_BtnsDownTabView[i].Rect.Y)
                    && (nPoint.Y <= m_BtnsDownTabView[i].Rect.Y + m_BtnsDownTabView[i].Rect.Height))
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                m_BtnsDownTabView.ToList().ForEach(a => a.MouseUp(nPoint));
            }
            return base.mouseUp(nPoint);
        }


        /// <summary>
        /// up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn1_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            append_postCmd(CmdType.ChangePage, (int)ViewState.AssistPage2, 0, 0);
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            m_BtnsDownTabView.ToList().ForEach(a => a.Paint(dcGs));

            dcGs.DrawString(m_PageStr, m_ChineseFont, m_BlackBrush, m_PageRect, FontInfo.SfCc);

            for (m_I = 0; m_I < m_FrameRects.Length; m_I++)
            {
                dcGs.DrawRectangle(m_BlackLinePen, m_FrameRects[m_I]);
                dcGs.DrawString(m_FrameStr[m_I], m_ChineseFont, m_BlackBrush, m_FrameRects[m_I], FontInfo.SfLc);
            }

            for (m_I = 0; m_I < m_Rect1.Length; m_I++)//第一个列表框
            {
                if (BoolList[UIObj.InBoolList[m_I] + 1])
                {
                    dcGs.DrawImage(m_ResourceImages[2], m_Rect1[m_I]);
                }
                else if (BoolList[UIObj.InBoolList[m_I]])
                {
                    dcGs.DrawImage(m_ResourceImages[1], m_Rect1[m_I]);
                }
                else
                {
                    dcGs.DrawImage(m_ResourceImages[6], m_Rect1[m_I]);
                }
            }


            //for (m_I = 0; m_I < m_Rect2.Length; m_I++)//第二个列表框
            //{
            //    if (BoolList[UIObj.InBoolList[m_I + 2] + 1])
            //    {
            //        dcGs.DrawImage(m_ResourceImages[2], m_Rect2[m_I]);
            //    }
            //    else if (BoolList[UIObj.InBoolList[m_I + 2]])
            //    {
            //        dcGs.DrawImage(m_ResourceImages[1], m_Rect2[m_I]);
            //    }
            //    else
            //    {
            //        dcGs.DrawImage(m_ResourceImages[6], m_Rect2[m_I]);
            //    }

            //}


            //for (m_I = 0; m_I < m_Rect3.Length; m_I++)//第三个列表框
            //{
            //    m_Rect3Flags[m_I] = false;
            //    for (m_J = 0; m_J < 3; m_J++)
            //    {
            //        if (BoolList[UIObj.InBoolList[8 + m_I] + m_J])
            //        {
            //            m_Rect3Flags[m_I] = true;
            //            dcGs.DrawImage(m_ResourceImages[3 + m_J], m_Rect3[m_I]);
            //        }
            //    }
            //    if (m_Rect3Flags[m_I] == false)
            //    {
            //        dcGs.DrawImage(m_ResourceImages[6], m_Rect3[m_I]);
            //    }
            //}


            for (m_I = 0; m_I < m_Rect4.Length; m_I++)//第四个列表框
            {
                dcGs.DrawRectangle(m_BlackLinePen, m_Rect4[m_I]);
                dcGs.FillRectangle(m_RectBrush, m_FillRect4[m_I]);

                m_Rect4Strs[m_I] = FloatList[UIObj.InFloatList[m_I]].ToString("f1");

                dcGs.DrawString(m_Rect4Strs[m_I], m_DigitFont, m_BlackBrush, m_Rect4[m_I], FontInfo.SfCc);
            }
            for (m_I = 0; m_I < m_Rect5.Length; m_I++)//第五个列表框
            {
                dcGs.DrawRectangle(m_BlackLinePen, m_Rect5[m_I]);
                dcGs.FillRectangle(m_RectBrush, m_FillRect5[m_I]);

                m_Rect5Strs[m_I] = FloatList[UIObj.InFloatList[2 + m_I]].ToString("f1");
                dcGs.DrawString(m_Rect5Strs[m_I], m_DigitFont, m_BlackBrush, m_FillRect5[m_I], FontInfo.SfCc);
            }

            base.paint(dcGs);
        }
        #endregion
    }
}
