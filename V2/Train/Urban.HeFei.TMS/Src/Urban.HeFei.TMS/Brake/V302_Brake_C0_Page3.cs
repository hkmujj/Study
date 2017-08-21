#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-23
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图3-制动-No.0-页面3
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using Urban.HeFei.TMS.Common;

namespace Urban.HeFei.TMS.Brake
{
     /// <summary>
    /// 功能描述：视图3-制动-No.2-页面3
    /// 创建人：lih
    /// 创建时间：2015-08-06
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V302BrakeC0Page3:baseClass
    {
        #region 私有变量
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private Rectangle m_FrameRect = new Rectangle(12, 264, 674, 48);
        private SolidBrush m_RectBrush = new SolidBrush(Color.FromArgb(1, 116, 154));
        private Font m_DigitFont = new Font("Arial", 18);
        private Font m_ChineseFont = new Font("宋体", 14);
        private SolidBrush m_BlackBrush = new SolidBrush(Color.Black);
        private Rectangle[] m_LoadRects;
        private Rectangle[] m_LoadStrRects;
        private int m_I = 0;
        private string[] m_LoadStrs;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "制动-页面3";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V302_Brake_C0_Page3";
        //}

        /// <summary>
        /// 组件初始化函数：初始化处理
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_LoadRects = new Rectangle[6];
            m_LoadStrRects = new Rectangle[6];
            m_LoadStrs = new string[6];
            for (var i = 0; i < 6; i++)
            {
                m_LoadStrs[i] = "";
                m_LoadRects[i] = new Rectangle(175 + i * 86, 268, 60, 40);
                m_LoadStrRects[i] = new Rectangle(176 + i * 86, 269, 59, 39);
            }

            ////导入图片资源
            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });

            //模式按钮
            return true;
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawRectangle(m_BlackLinePen, m_FrameRect);
            dcGs.DrawString("载重(t)", m_ChineseFont, m_BlackBrush, m_FrameRect, FontInfo.SfLc);

            for (m_I = 0; m_I < m_LoadRects.Length; m_I++)
            {
                dcGs.DrawRectangle(m_BlackLinePen, m_LoadRects[m_I]);
                dcGs.FillRectangle(m_RectBrush, m_LoadStrRects[m_I]);

                m_LoadStrs[m_I] = (((int)(FloatList[UIObj.InFloatList[m_I]] * 10)) / 10.0f).ToString("0.0");
                dcGs.DrawString(m_LoadStrs[m_I], m_DigitFont, m_BlackBrush, m_LoadRects[m_I], FontInfo.SfCc);
            }
            base.paint(dcGs);
        }
        #endregion

    }
}
