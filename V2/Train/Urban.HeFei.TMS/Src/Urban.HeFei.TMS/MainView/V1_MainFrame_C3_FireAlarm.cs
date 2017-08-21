#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-07
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图1-主界面-No.3-烟火报警
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

namespace Urban.HeFei.TMS.MainView
{
    /// <summary>
    /// 功能描述：视图1-主界面-No.3-烟火报警
    /// 创建人：lih
    /// 创建时间：2015-08-07
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1MainFrameC3FireAlarm : baseClass
    {
        #region 私有变量
        private List<Image> m_ResourceImages = new List<Image>();//图片资源
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private Font m_ChineseFont = new Font("宋体", 14);
        private Brush m_BlackBrush = new SolidBrush(Color.Black);
        private Rectangle m_FrameRect = new Rectangle(12, 312, 674, 45);
        //private Rectangle m_FrameStrRect = new Rectangle(12, 312, 111, 45);
        private Rectangle[] m_ChildrenRects;
        private string m_FrameStr = "烟火报警";
        private int m_I = 0;
        private bool m_Rect1Flag = false;
        private bool m_Rect2Flag = false;
        private bool m_Rect3Flag = false;
        private bool m_Rect4Flag = false;
        private bool m_Rect5Flag = false;
        private bool m_Rect6Flag = false;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "主界面烟火报警";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V1_MainFrame_C3_FireAlarm";
        //}

        /// <summary>
        /// 初始化函数：导入图片
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {

            m_ChildrenRects = new Rectangle[6];
            m_ChildrenRects[0] = new Rectangle(177,316, 61, 39);
            m_ChildrenRects[1] = new Rectangle(263,316, 61, 39);
            m_ChildrenRects[2] = new Rectangle(349,316, 61, 39);
            m_ChildrenRects[3] = new Rectangle(435,316, 61, 39);
            m_ChildrenRects[4] = new Rectangle(521,316, 61, 39);
            m_ChildrenRects[5] = new Rectangle(607,316, 61, 39);

            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    m_ResourceImages.Add(Image.FromStream(fs));
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

            dcGs.DrawRectangle(m_BlackLinePen, m_FrameRect);
            dcGs.DrawString(m_FrameStr, m_ChineseFont, m_BlackBrush, m_FrameRect, FontInfo.SfLc);

            m_Rect1Flag = false;
            m_Rect2Flag = false;
            m_Rect3Flag = false;
            m_Rect4Flag = false;
            m_Rect5Flag = false;
            m_Rect6Flag = false;


            for (m_I = 0; m_I < 4; m_I++)
            {
                if (BoolList[UIObj.InBoolList[0] + m_I])
                {
                    m_Rect1Flag = true;
                    dcGs.DrawImage(m_ResourceImages[m_I], m_ChildrenRects[0]);
                }
                if (BoolList[UIObj.InBoolList[4] + m_I])
                {
                    m_Rect2Flag = true;
                    dcGs.DrawImage(m_ResourceImages[m_I], m_ChildrenRects[1]);
                }
                if (BoolList[UIObj.InBoolList[8] + m_I])
                {
                    m_Rect3Flag = true;
                    dcGs.DrawImage(m_ResourceImages[m_I], m_ChildrenRects[2]);
                }
                if (BoolList[UIObj.InBoolList[12] + m_I])
                {
                    m_Rect4Flag = true;
                    dcGs.DrawImage(m_ResourceImages[m_I], m_ChildrenRects[3]);
                }
                if (BoolList[UIObj.InBoolList[16] + m_I])
                {
                    m_Rect5Flag = true;
                    dcGs.DrawImage(m_ResourceImages[m_I], m_ChildrenRects[4]);
                }
                if (BoolList[UIObj.InBoolList[20] + m_I])
                {
                    m_Rect6Flag = true;
                    dcGs.DrawImage(m_ResourceImages[m_I], m_ChildrenRects[5]);
                }
            }

            if (!m_Rect1Flag) { dcGs.DrawImage(m_ResourceImages[4], m_ChildrenRects[0]); }
            if (!m_Rect2Flag) { dcGs.DrawImage(m_ResourceImages[4], m_ChildrenRects[1]);}
            if (!m_Rect3Flag) { dcGs.DrawImage(m_ResourceImages[4], m_ChildrenRects[2]);}

            if (!m_Rect4Flag) { dcGs.DrawImage(m_ResourceImages[4], m_ChildrenRects[3]);}
            if (!m_Rect5Flag) { dcGs.DrawImage(m_ResourceImages[4], m_ChildrenRects[4]);}

            if (!m_Rect6Flag) { dcGs.DrawImage(m_ResourceImages[4], m_ChildrenRects[5]); }

            base.paint(dcGs);
        }
        #endregion
    }
}
