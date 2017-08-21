#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-06
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图2-牵引-No.3-高速断路器
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

namespace Urban.HeFei.TMS
{
    /// <summary>
    /// 功能描述：视图2-牵引-No.3-高速断路器
    /// 创建人：lih
    /// 创建时间：2015-08-10
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V2TractionC2HSpeedCirBreak:baseClass
    {
        #region 私有变量
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private Font m_ChineseFont = new Font("宋体", 16, FontStyle.Regular);
        private Rectangle m_FrameRect = new Rectangle(12, 262, 674, 44);
        private Brush m_BlackBrush = new SolidBrush(Color.Black);
        private Rectangle[] m_ChildrenRects;
        private string m_FrameStr = "高速断路器";
        private int m_I = 0;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "牵引-高速断路器";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V2_Traction_C2_HSpeedCirBreak";
        //}

        /// <summary>
        /// 初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_ChildrenRects = new Rectangle[4];
            m_ChildrenRects[0] = new Rectangle(263, 265, 28, 38);
            m_ChildrenRects[1] = new Rectangle(295, 265, 28, 38);
            m_ChildrenRects[2] = new Rectangle(521, 265, 28, 38);
            m_ChildrenRects[3] = new Rectangle(553, 265, 28, 38);

            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
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
            dcGs.DrawRectangle(m_BlackLinePen, m_FrameRect);
            dcGs.DrawString(m_FrameStr, m_ChineseFont, m_BlackBrush, m_FrameRect, FontInfo.SfLc);

            for (m_I = 0; m_I < 4; m_I++)
            {
                if (BoolList[UIObj.InBoolList[m_I]])
                {
                    dcGs.DrawImage(m_ResourceImage[0], m_ChildrenRects[m_I]);
                }
                else if (BoolList[UIObj.InBoolList[m_I] + 1])
                {
                    dcGs.DrawImage(m_ResourceImage[1], m_ChildrenRects[m_I]);
                }
                else
                {
                    dcGs.DrawImage(m_ResourceImage[2], m_ChildrenRects[m_I]);
                }
            }

            base.paint(dcGs);
        }
        #endregion

    }
}
