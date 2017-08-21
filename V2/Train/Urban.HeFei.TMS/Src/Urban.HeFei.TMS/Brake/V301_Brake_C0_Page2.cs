#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-23
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图3-制动-No.0-页面2
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using Urban.HeFei.TMS.Common;

namespace Urban.HeFei.TMS.Brake
{
    /// <summary>
    /// 功能描述：视图3-制动-No.1-页面2
    /// 创建人：lih
    /// 创建时间：2015-08-06
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V301BrakeC0Page2 : baseClass
    {
        #region 私有变量
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);

        private int[] m_ServiceBrakingPointXs;
        private int[] m_ServiceBrakingPointYs;
        private int m_I, m_J = 0;
        private SolidBrush m_RectBrush = new SolidBrush(Color.FromArgb(1, 116, 154));
        private Font m_DigitFont = new Font("Arial", 18);
        private Font m_ChineseFont = new Font("宋体", 14);
        private SolidBrush m_BlackBrush = new SolidBrush(Color.Black);
        private Rectangle[] m_FrameRects;
        private Rectangle[] m_FrameNameRects;
        private Rectangle[] m_KhRects;
        private Rectangle[] m_KhStrRects;
        private Rectangle[] m_BrakePreRects;
        private Rectangle[] m_BrakePreStrRects;
        private Rectangle[] m_EmergeRects;
        private bool[] m_EmergeFlags;
      
        private string[] m_KhStrs;
        private string[] m_BrakeStrs;

        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "制动-页面2";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V301_Brake_C0_Page2";
        //}

        /// <summary>
        /// 组件初始化函数：初始化处理
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
          
            m_FrameRects = new Rectangle[3];
            m_FrameRects[0] = new Rectangle(12, 262, 674, 98);
            m_FrameRects[1] = new Rectangle(12, 364, 674, 98);
            m_FrameRects[2] = new Rectangle(12, 466, 674, 45);

            m_FrameNameRects = new Rectangle[5];
            m_FrameNameRects[0] = new Rectangle(12, 262, 116, 98);
            m_FrameNameRects[1] = new Rectangle(12, 364, 154, 98);
            m_FrameNameRects[2] = new Rectangle(12, 466, 674, 45);

            m_ServiceBrakingPointXs = new int[6];
            for (var i = 0; i < 6; i++)
            {
                m_ServiceBrakingPointXs[i] = 175 + i * 86;
            }
            m_ServiceBrakingPointYs = new int[5];
            m_ServiceBrakingPointYs[0] = 268;
            m_ServiceBrakingPointYs[1] = 312;
            m_ServiceBrakingPointYs[2] = 370;
            m_ServiceBrakingPointYs[3] = 415;
            m_ServiceBrakingPointYs[4] = 470;

            m_KhRects = new Rectangle[12];
            m_KhStrRects = new Rectangle[12];
            m_KhStrs = new string[12];
            var len = 0;
            len = m_ServiceBrakingPointXs.Length;

            for (m_J = 0; m_J < len; m_J++)
            {
                m_KhStrs[2 * m_J] = "";
                m_KhStrs[2 * m_J + 1] = "";
                m_KhRects[2 * m_J] = new Rectangle(m_ServiceBrakingPointXs[m_J], m_ServiceBrakingPointYs[0], 60, 40);
                m_KhRects[2 * m_J + 1] = new Rectangle(m_ServiceBrakingPointXs[m_J], m_ServiceBrakingPointYs[1], 60, 40);

                m_KhStrRects[2 * m_J] = new Rectangle(m_ServiceBrakingPointXs[m_J] + 1, m_ServiceBrakingPointYs[0] + 1, 59, 39);
                m_KhStrRects[2 * m_J + 1] = new Rectangle(m_ServiceBrakingPointXs[m_J] + 1, m_ServiceBrakingPointYs[1] + 1, 59, 39);
            }


            m_BrakePreRects = new Rectangle[12];
            m_BrakePreStrRects = new Rectangle[12];
            m_BrakeStrs = new string[12];
            for (m_J = 0; m_J < len; m_J++)
            {
                m_BrakeStrs[2 * m_J] = "";
                m_BrakeStrs[2 * m_J + 1] = "";
                m_BrakePreRects[2 * m_J] = new Rectangle(m_ServiceBrakingPointXs[m_J], m_ServiceBrakingPointYs[2], 60, 40);
                m_BrakePreRects[2 * m_J + 1] = new Rectangle(m_ServiceBrakingPointXs[m_J], m_ServiceBrakingPointYs[3], 60, 40);

                m_BrakePreStrRects[2 * m_J] = new Rectangle(m_ServiceBrakingPointXs[m_J] + 1, m_ServiceBrakingPointYs[2] + 1, 59, 39);
                m_BrakePreStrRects[2 * m_J + 1] = new Rectangle(m_ServiceBrakingPointXs[m_J] + 1, m_ServiceBrakingPointYs[3] + 1, 59, 39);
            }


            m_EmergeRects = new Rectangle[4];
            m_EmergeRects[0] = new Rectangle(175, 469, 28, 38);
            m_EmergeRects[1] = new Rectangle(207, 469, 28, 38);
            m_EmergeRects[2] = new Rectangle(605, 469, 28, 38);
            m_EmergeRects[3] = new Rectangle(637, 469, 28, 38);


            //导入图片资源
            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });
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
            dcGs.DrawRectangle(m_BlackLinePen, m_FrameRects[0]);
            dcGs.DrawRectangle(m_BlackLinePen, m_FrameRects[1]);
            //    dcGs.DrawRectangle(m_BlackLinePen, m_FrameRects[2]);

            dcGs.DrawString("空簧压力(Bar)", m_ChineseFont, m_BlackBrush, m_FrameNameRects[0], FontInfo.SfLc);

            dcGs.DrawString("制动风缸压力(Bar)", m_ChineseFont, m_BlackBrush, m_FrameNameRects[1], FontInfo.SfLc);

            //  dcGs.DrawString("紧急制动继电器", m_ChineseFont, m_BlackBrush, m_FrameNameRects[2], FontInfo.m_SfLc);

            for (m_I = 0; m_I < m_KhRects.Length; m_I++)//空簧压力
            {
                dcGs.DrawRectangle(m_BlackLinePen, m_KhRects[m_I]);
                dcGs.FillRectangle(m_RectBrush, m_KhStrRects[m_I]);


            }
            for (m_I = 0; m_I < 6; m_I++)
            {
                m_KhStrs[2 * m_I] = GetStringValue(FloatList[UIObj.InFloatList[m_I]]);
                m_KhStrs[2 * m_I + 1] = GetStringValue(FloatList[UIObj.InFloatList[m_I] + 1]);
                dcGs.DrawString(m_KhStrs[2 * m_I], m_DigitFont, m_BlackBrush, m_KhRects[2 * m_I], FontInfo.SfCc);
                dcGs.DrawString(m_KhStrs[2 * m_I + 1], m_DigitFont, m_BlackBrush, m_KhRects[2 * m_I + 1], FontInfo.SfCc);
            }

            for (m_I = 0; m_I < m_BrakePreRects.Length; m_I++)//制动风缸压力
            {
                dcGs.DrawRectangle(m_BlackLinePen, m_BrakePreRects[m_I]);
                dcGs.FillRectangle(m_RectBrush, m_BrakePreStrRects[m_I]);

            }

            for (m_I = 0; m_I < 6; m_I++)
            {
                m_BrakeStrs[2 * m_I] = GetStringValue(FloatList[UIObj.InFloatList[m_I + 6]]);
                m_BrakeStrs[2 * m_I + 1] = GetStringValue(FloatList[UIObj.InFloatList[m_I + 6] + 1]);

                dcGs.DrawString(m_BrakeStrs[2 * m_I], m_DigitFont, m_BlackBrush, m_BrakePreRects[2 * m_I], FontInfo.SfCc);
                dcGs.DrawString(m_BrakeStrs[2 * m_I + 1], m_DigitFont, m_BlackBrush, m_BrakePreRects[2 * m_I + 1], FontInfo.SfCc);

            }

            //for (m_I = 0; m_I < 2; m_I++)//紧急制动继电器
            //{
            //    if(BoolList[UIObj.InBoolList[m_I]])
            //    {
            //        dcGs.DrawImage(m_ResourceImage[0], m_EmergeRects[2 * m_I]);
            //        dcGs.DrawImage(m_ResourceImage[0], m_EmergeRects[2 * m_I + 1]);
            //    }
            //    else if(BoolList[UIObj.InBoolList[m_I] + 1])
            //    {
            //        dcGs.DrawImage(m_ResourceImage[1], m_EmergeRects[2 * m_I]);
            //        dcGs.DrawImage(m_ResourceImage[1], m_EmergeRects[2 * m_I + 1]);
            //    }
            //    else
            //    {
            //        dcGs.DrawImage(m_ResourceImage[2], m_EmergeRects[2 * m_I]);
            //        dcGs.DrawImage(m_ResourceImage[2], m_EmergeRects[2 * m_I + 1]);
            //    }
            //}
            base.paint(dcGs);
        }

        private string GetStringValue(float value)
        {
            string result;

            if (value == (int)value)
            {
                result = value.ToString("F0");
            }
            else
            {
                result = value.ToString("F1");
            }
            return result;

        }
        #endregion

    }
}
