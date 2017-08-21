#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-23
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图3-制动-No.0-页面1
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
    /// 功能描述：视图3-制动-No.0-页面1
    /// 创建人：lih
    /// 创建时间：2015-08-06
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V3BrakeC0Page1 : baseClass
    {
        #region 私有变量
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private int m_Temprature = 18;//温度
        private string m_ModeString = "未知";
        private Font m_ChineseFont = new Font("宋体", 14);
        private Font m_DigitFont = new Font("Arial", 20, FontStyle.Regular);
        private Brush m_RectBrush = new SolidBrush(Color.FromArgb(1, 116, 154));
        private Brush m_BlackBrush = new SolidBrush(Color.Black);
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private int[] m_ServiceBrakingPointXs;
        private int[] m_ServiceBrakingPointYs;
        private int m_I, m_J = 0;
        private Rectangle[] m_FrameRects;
        private string[] m_FrameStrs;
        private Rectangle[] m_DigitRects;

        private Rectangle[] m_CyzdRects;
        private Rectangle[] m_TfzdRects;
        private Rectangle[] m_KyjRects;

        private bool[] m_CyzdFlags;
        private bool[] m_TfzdFlags;
        private bool[] m_KyjFlags;

        private string m_MApStr1;
        private string m_MApStr2;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "制动-页面1";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V3_Brake_C0_Page1";
        //}

        /// <summary>
        /// 组件初始化函数：初始化处理
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_ServiceBrakingPointXs = new int[6];
            for (m_I = 0; m_I < 6; m_I++)
            {
                m_ServiceBrakingPointXs[m_I] = 175 + m_I * 86;
            }
            m_ServiceBrakingPointYs = new int[5];
            m_ServiceBrakingPointYs[0] = 268;
            m_ServiceBrakingPointYs[1] = 312;
            m_ServiceBrakingPointYs[2] = 366;
            m_ServiceBrakingPointYs[3] = 416;
            m_ServiceBrakingPointYs[4] = 465;

            m_FrameRects = new Rectangle[4];
            m_FrameRects[0] = new Rectangle(12, 262, 674, 96);
            m_FrameRects[1] = new Rectangle(12, 362, 674, 46);
            m_FrameRects[2] = new Rectangle(12, 412, 674, 46);
            m_FrameRects[3] = new Rectangle(12, 462, 674, 46);
            m_FrameStrs = new string[4];
            m_FrameStrs[0] = "常用制动";
            m_FrameStrs[1] = "停放制动";
            m_FrameStrs[2] = "空压机";
            //m_FrameStrs[3] = "主风管压力";

            m_DigitRects = new Rectangle[2];
            m_DigitRects[0] = new Rectangle(m_ServiceBrakingPointXs[1], m_ServiceBrakingPointYs[4], 60, 40);
            m_DigitRects[1] = new Rectangle(m_ServiceBrakingPointXs[4], m_ServiceBrakingPointYs[4], 60, 40);

            m_CyzdRects = new Rectangle[12];
            m_TfzdRects = new Rectangle[6];
            m_CyzdFlags = new bool[12];
            m_TfzdFlags = new bool[6];
            m_KyjFlags = new bool[2] { false, false };

            for (m_I = 0; m_I < 6; m_I++)
            {
                m_CyzdRects[2 * m_I] = new Rectangle(m_ServiceBrakingPointXs[m_I], m_ServiceBrakingPointYs[0], 60, 40);
                m_CyzdRects[2 * m_I + 1] = new Rectangle(m_ServiceBrakingPointXs[m_I], m_ServiceBrakingPointYs[1], 60, 40);
                m_CyzdFlags[2 * m_I] = false;
                m_CyzdFlags[2 * m_I + 1] = false;

                m_TfzdRects[m_I] = new Rectangle(m_ServiceBrakingPointXs[m_I], m_ServiceBrakingPointYs[2], 60, 40);
                m_TfzdFlags[m_I] = false;
            }
            m_KyjRects = new Rectangle[2];
            m_KyjRects[0] = new Rectangle(m_ServiceBrakingPointXs[0], m_ServiceBrakingPointYs[3], 60, 40);
            m_KyjRects[1] = new Rectangle(m_ServiceBrakingPointXs[5], m_ServiceBrakingPointYs[3], 60, 40);

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
        /// 界面绘制函数
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            for (m_I = 0; m_I < m_FrameRects.Length - 1; m_I++)
            {
                dcGs.DrawRectangle(m_BlackLinePen, m_FrameRects[m_I]);
                dcGs.DrawString(m_FrameStrs[m_I], m_ChineseFont, m_BlackBrush, m_FrameRects[m_I], FontInfo.SfLc);
            }

            for (m_J = 0; m_J < 6; m_J++)
            {
                m_CyzdFlags[2 * m_J] = false;
                m_CyzdFlags[2 * m_J + 1] = false;
                m_TfzdFlags[m_J] = false;
                for (m_I = 0; m_I < 4; m_I++)//常用制动缓解
                {
                    if (BoolList[UIObj.InBoolList[m_J] + m_I])
                    {
                        m_CyzdFlags[2 * m_J] = true;
                        dcGs.DrawImage(m_ResourceImage[9 + m_I], m_CyzdRects[2 * m_J]);
                    }
                    if (BoolList[UIObj.InBoolList[m_J] + 4 + m_I])
                    {
                        m_CyzdFlags[2 * m_J + 1] = true;
                        dcGs.DrawImage(m_ResourceImage[9 + m_I], m_CyzdRects[2 * m_J + 1]);
                    }

                    if (BoolList[UIObj.InBoolList[m_J + 6] + m_I])//停放制动
                    {
                        m_TfzdFlags[m_J] = true;
                        dcGs.DrawImage(m_ResourceImage[4 + m_I], m_TfzdRects[m_J]);
                    }
                }
            }

            for (m_J = 0; m_J < 6; m_J++)
            {
                if (m_CyzdFlags[2 * m_J] == false)
                {
                    dcGs.DrawImage(m_ResourceImage[13], m_CyzdRects[2 * m_J]);
                }
                if (m_CyzdFlags[2 * m_J + 1] == false)
                {
                    dcGs.DrawImage(m_ResourceImage[13], m_CyzdRects[2 * m_J + 1]);
                }
                if (m_TfzdFlags[m_J] == false)
                {
                    dcGs.DrawImage(m_ResourceImage[8], m_TfzdRects[m_J]);
                }
            }

            m_KyjFlags[0] = false;
            m_KyjFlags[1] = false;
            for (m_I = 0; m_I < 3; m_I++)//空压机状态
            {
                if (BoolList[UIObj.InBoolList[12] + m_I])
                {
                    m_KyjFlags[0] = true;
                    dcGs.DrawImage(m_ResourceImage[m_I], m_KyjRects[0]);
                }
                if (BoolList[UIObj.InBoolList[13] + m_I])
                {
                    m_KyjFlags[1] = true;
                    dcGs.DrawImage(m_ResourceImage[m_I], m_KyjRects[1]);
                }
            }

            if (m_KyjFlags[0] == false)
            {
                dcGs.DrawImage(m_ResourceImage[3], m_KyjRects[0]);
            }
            if (m_KyjFlags[1] == false)
            {
                dcGs.DrawImage(m_ResourceImage[3], m_KyjRects[1]);
            }

            //dcGs.DrawRectangle(m_BlackLinePen, m_ServiceBrakingPointXs[1], m_ServiceBrakingPointYs[4], 60, 40);
            //dcGs.DrawRectangle(m_BlackLinePen, m_ServiceBrakingPointXs[4], m_ServiceBrakingPointYs[4], 60, 40);

            //dcGs.FillRectangle(m_RectBrush, m_ServiceBrakingPointXs[1] + 1, m_ServiceBrakingPointYs[4] + 1, 59, 39);
            //dcGs.FillRectangle(m_RectBrush, m_ServiceBrakingPointXs[4] + 1, m_ServiceBrakingPointYs[4] + 1, 59, 39);



            //m_MApStr1 = (((int)(FloatList[UIObj.InFloatList[0]] * 10)) / 10.0f).ToString("0.0");
            //m_MApStr2 = (((int)(FloatList[UIObj.InFloatList[1]] * 10)) / 10.0f).ToString("0.0");

            //dcGs.DrawString(m_MApStr1, m_DigitFont, m_BlackBrush,m_DigitRects[0], FontInfo.m_SfCc);

            //dcGs.DrawString(m_MApStr2, m_DigitFont, m_BlackBrush, m_DigitRects[1], FontInfo.m_SfCc);

            base.paint(dcGs);
        }
        #endregion
    }
}
