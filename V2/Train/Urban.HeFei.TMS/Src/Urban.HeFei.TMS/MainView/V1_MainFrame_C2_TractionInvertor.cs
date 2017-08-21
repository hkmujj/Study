#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-07
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图1-主界面-No.2-牵引逆变器
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

namespace Urban.HeFei.TMS.MainView
{
    /// <summary>
    /// 功能描述：视图1-主界面-No.2-牵引逆变器
    /// 创建人：lih
    /// 创建时间：2015-08-07
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1MainFrameC2TractionInvertor : baseClass
    {
        #region 私有变量
        private List<Image> m_ResourceImages = new List<Image>();//图片资源
        private SolidBrush[] m_AirBrakingStates;//空气制动状态
        private List<Label> m_LabelsPower = new List<Label>();//电源状态文本框列表
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private Font m_ChineseFont= new Font("宋体", 16, FontStyle.Regular);
        private Brush m_BlackBrush= new SolidBrush(Color.Black);
        private Rectangle m_FrameRect= new Rectangle(12, 272, 674, 53);
        private Rectangle m_FrameStrRect = new Rectangle(12, 272, 131, 53);
        private Rectangle [] m_ChildrenRects;
        private string m_FrameStr="牵引逆变器";
        private int m_I = 0;
        private bool m_Rect1Flag = false;
        private bool m_Rect2Flag = false;
        private bool m_Rect3Flag = false;
        private bool m_Rect4Flag = false;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "主界面牵引逆变器";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V1_MainFrame_C2_TractionInvertor";
        //}

        /// <summary>
        /// 初始化函数：导入图片、初始化操作
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_ChildrenRects = new Rectangle[4];
            m_ChildrenRects[0] = new Rectangle(263, 279, 61, 39);
            m_ChildrenRects[1] = new Rectangle(349, 279, 61, 39);
            m_ChildrenRects[2] = new Rectangle(435, 279, 61, 39);
            m_ChildrenRects[3] = new Rectangle(521, 279, 61, 39);

            //导入图片资源
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
            dcGs.DrawString(m_FrameStr, m_ChineseFont, m_BlackBrush,m_FrameStrRect, FontInfo.SfCc);

            m_Rect1Flag = false;
            m_Rect2Flag = false;
            m_Rect3Flag = false;
            m_Rect4Flag = false;

            for (m_I = 0; m_I < 5; m_I++)
            {
                if (BoolList[UIObj.InBoolList[0] + m_I])
                {
                    m_Rect1Flag = true;
                    dcGs.DrawImage(m_ResourceImages[m_I], m_ChildrenRects[0]);
                }
                if (BoolList[UIObj.InBoolList[5] + m_I])
                {
                    m_Rect2Flag = true;
                    dcGs.DrawImage(m_ResourceImages[m_I], m_ChildrenRects[1]);
                }
                if (BoolList[UIObj.InBoolList[10] + m_I])
                {
                    m_Rect3Flag = true;
                    dcGs.DrawImage(m_ResourceImages[m_I], m_ChildrenRects[2]);
                }
                if (BoolList[UIObj.InBoolList[15] + m_I])
                {
                    m_Rect4Flag = true;
                    dcGs.DrawImage(m_ResourceImages[m_I], m_ChildrenRects[3]);
                }
            }

            if (!m_Rect1Flag) { dcGs.DrawImage(m_ResourceImages[5], m_ChildrenRects[0]); }
            if (!m_Rect2Flag) { dcGs.DrawImage(m_ResourceImages[5], m_ChildrenRects[1]); }
            if (!m_Rect3Flag) { dcGs.DrawImage(m_ResourceImages[5], m_ChildrenRects[2]); }
            if (!m_Rect4Flag) { dcGs.DrawImage(m_ResourceImages[5], m_ChildrenRects[3]); }

            base.paint(dcGs);
        }

        #endregion
    }
}
