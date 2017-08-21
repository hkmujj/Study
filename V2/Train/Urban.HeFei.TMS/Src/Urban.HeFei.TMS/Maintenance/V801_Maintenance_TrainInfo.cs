#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-06
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：维护-No.1-列车信息
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using Urban.HeFei.TMS.Common;

namespace Urban.HeFei.TMS.Maintenance
{
    /// <summary>
    /// 功能描述：维护-No.1-列车信息
    /// 创建人：lih
    /// 创建时间：2015-08-06
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V801MaintenanceTrainInfo:baseClass
    {
        #region 私有变量
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private Font m_ChineseFont = new Font("宋体", 16);
        private int m_I = 0;
        private Rectangle[] m_FrameRects;
        private string[] m_FrameStrs;
        private Rectangle[] m_FrameStrRects;


        private Rectangle[] m_FillRects;
        private int m_PointX = 512;
        private int[] m_PointY;

        private Rectangle[] m_InfoRects;
        private string[] m_InfoStrs;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "维护-列车信息";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V801_Maintenance_TrainInfo";
        //}

        /// <summary>
        /// 初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_FrameRects = new Rectangle[4];
            for (m_I = 0; m_I < m_FrameRects.Length; m_I++)
            {
                m_FrameRects[m_I] = new Rectangle(164, 145 + m_I * 80, 470, 55);
            }
            m_FrameStrRects = new Rectangle[4];
            for (m_I = 0; m_I < m_FrameStrRects.Length; m_I++)
            {
                m_FrameStrRects[m_I] = new Rectangle(164, 145 + m_I * 80, 226, 55);
            }
            m_FrameStrs = new string[4] { "总运行里程(KM)", "总运行时间(h)", "空压机工作时间(min)", "VCU主从" };

            m_PointY = new int[4] { 306, 360, 386, 440 };

            m_FillRects=new Rectangle[4];
            for(m_I=0;m_I<m_FillRects.Length;m_I++)
            {
                m_FillRects[m_I]=new Rectangle(390,146+m_I*80,244,54);
            }

            m_InfoRects = new Rectangle[6];
            m_InfoRects[0] = new Rectangle(390, 145, 244, 55);
            m_InfoRects[1] = new Rectangle(390, 225, 244, 55);
            m_InfoRects[2] = new Rectangle(390, 305, 122, 55);
            m_InfoRects[3] = new Rectangle(512, 305, 122, 55);
            m_InfoRects[4] = new Rectangle(390, 385, 122, 55);
            m_InfoRects[5] = new Rectangle(512, 385, 122, 55);

            m_InfoStrs = new string[6] { "1234", "1234", "1234", "1234", "主", "从" };

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
            for (m_I = 0; m_I < m_FrameRects.Length; m_I++)
            {
                dcGs.DrawRectangle(m_BlackLinePen, m_FrameRects[m_I]);
                dcGs.DrawString(m_FrameStrs[m_I], m_ChineseFont, Brushs.BlackBrush, m_FrameStrRects[m_I], FontInfo.SfLc);
                dcGs.FillRectangle(Brushs.WhiteBrush, m_FillRects[m_I]);
            }

            dcGs.DrawLine(m_BlackLinePen, m_PointX, m_PointY[0], m_PointX, m_PointY[1]);
            dcGs.DrawLine(m_BlackLinePen, m_PointX, m_PointY[2], m_PointX, m_PointY[3]);

            for (m_I = 0; m_I < m_InfoRects.Length; m_I++)
            {
                dcGs.DrawString(m_InfoStrs[m_I], m_ChineseFont, Brushs.BlackBrush, m_InfoRects[m_I], FontInfo.SfCc);
            }

            base.paint(dcGs);
        }
        #endregion
    }
    
}
