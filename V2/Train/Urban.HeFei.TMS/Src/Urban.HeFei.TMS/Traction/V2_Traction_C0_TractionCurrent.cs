#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-06
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图2-牵引-No.0-牵引电流
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common.Control;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using Urban.HeFei.TMS.Common;

namespace Urban.HeFei.TMS
{
    /// <summary>
    /// 功能描述：视图2-牵引-No.0-牵引电流
    /// 创建人：lih
    /// 创建时间：2015-08-06
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V2TractionC0TractionCurrent : baseClass
    {
        #region 私有变量
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private Font m_Font;//字体
        private Button m_BtnCheck;//自检按钮
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private Font m_ChineseFont = new Font("宋体", 16, FontStyle.Regular);
        private Brush m_BlackBrush = new SolidBrush(Color.Black);
        private Brush m_RectBrush = new SolidBrush(Color.FromArgb(1, 116, 154));
        private Font m_DigitFont = new Font("Arial", 14, FontStyle.Regular);
        private Rectangle m_FrameRect = new Rectangle(12, 461, 674, 44);
        private string m_FrameStr = "牵引电流(A)";
        private Rectangle[] m_ChildrenRects;
        private Rectangle[] m_ChildrenStrRects;
        private int m_I = 0;
        private string[] m_CurrentStrs;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "牵引-牵引电流";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V2_Traction_C0_TractionCurrent";
        //}

        /// <summary>
        /// 初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_Font = new Font("宋体", 10.5f, FontStyle.Bold);

            m_ChildrenRects = new Rectangle[4];
            m_ChildrenStrRects = new Rectangle[4];
            m_CurrentStrs = new string[4] { "", "", "", "" };

            m_ChildrenRects[0] = new Rectangle(262, 465, 60, 37);
            m_ChildrenRects[1] = new Rectangle(348, 465, 60, 37);
            m_ChildrenRects[2] = new Rectangle(434, 465, 60, 37);
            m_ChildrenRects[3] = new Rectangle(520, 465, 60, 37);


            m_ChildrenStrRects[0] = new Rectangle(263, 466, 59, 36);
            m_ChildrenStrRects[1] = new Rectangle(349, 466, 59, 36);
            m_ChildrenStrRects[2] = new Rectangle(435, 466, 59, 36);
            m_ChildrenStrRects[3] = new Rectangle(521, 466, 59, 36);

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
        /// 绘制界面
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawRectangle(m_BlackLinePen, m_FrameRect);
            dcGs.DrawString(m_FrameStr, m_ChineseFont, m_BlackBrush, m_FrameRect, FontInfo.SfLc);

            for (m_I = 0; m_I < 4; m_I++)
            {
                m_CurrentStrs[m_I] = FloatList[UIObj.InFloatList[m_I]].ToString("0");
                dcGs.DrawRectangle(m_BlackLinePen, m_ChildrenRects[m_I]);
                dcGs.FillRectangle(m_RectBrush, m_ChildrenStrRects[m_I]);
                dcGs.DrawString(m_CurrentStrs[m_I], m_DigitFont, m_BlackBrush, m_ChildrenRects[m_I], FontInfo.SfCc);
            }

            base.paint(dcGs);
        }

        #endregion
    }
}
