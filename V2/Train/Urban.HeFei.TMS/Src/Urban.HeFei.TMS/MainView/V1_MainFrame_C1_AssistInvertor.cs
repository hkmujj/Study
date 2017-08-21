#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-07
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图1-主界面-No.1-辅助逆变器
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.HeFei.TMS.Common;

namespace Urban.HeFei.TMS.MainView
{
    /// <summary>
    /// 功能描述：视图1-主界面-No.1-辅助逆变器
    /// 创建人：lih
    /// 创建时间：2015-08-07
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1MainFrameC1AssistInvertor : baseClass
    {
        #region 私有变量
        private List<Button> m_Btns = new List<Button>();//按钮列表
        private List<Image> m_ResourceImages = new List<Image>();//图片资源
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private Font m_ChineseFont= new Font("宋体", 14);
        private Brush m_BlackBrush=new SolidBrush(Color.Black);
        private Rectangle m_FrameRect=new Rectangle(12, 262, 674, 45);
     
        private string m_FrameStr = "辅助逆变器";
        private Rectangle[] m_Rects;
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
            return "主界面辅助逆变器";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V1_MainFrame_C1_AssistInvertor";
        //}

        /// <summary>
        /// 初始化函数：导入图片、创建组件控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_Rects = new Rectangle[4];
            m_Rects[0] = new Rectangle(177, 266, 60, 36);
            m_Rects[1] = new Rectangle(349, 266, 60, 36);
            m_Rects[2] = new Rectangle(435, 266, 60, 36);
            m_Rects[3] = new Rectangle(607, 266, 60, 36);

            //导入图片（二进制）
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

        #region 鼠标事件
        /// <summary>
        /// 组件鼠标按下事件监测函数
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
            //判断点击的位置是否属于该button集合
            for (var i = 0; i < m_Btns.Count; i++)
            {
                if((nPoint.X>=m_Btns[i].Rect.X) 
                    && (nPoint.X<=m_Btns[i].Rect.X+m_Btns[i].Rect.Width)
                    && (nPoint.Y >= m_Btns[i].Rect.Y)
                    && (nPoint.Y <= m_Btns[i].Rect.Y + m_Btns[i].Rect.Height))
                {
                    m_Btns[i].MouseDown(nPoint);
                    break;
                }
            }

            return true;
        }

        /// <summary>
        /// 组件鼠标弹起事件监测函数
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            for (var i = 0; i < m_Btns.Count; i++)
            {
                if ((nPoint.X >= m_Btns[i].Rect.X)
                    && (nPoint.X <= m_Btns[i].Rect.X + m_Btns[i].Rect.Width)
                    && (nPoint.Y >= m_Btns[i].Rect.Y)
                    && (nPoint.Y <= m_Btns[i].Rect.Y + m_Btns[i].Rect.Height))
                {
                    m_Btns[i].MouseUp(nPoint);
                    break;
                }
            }
            return true;
        }

        /// <summary>
        /// 按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            //按钮弹起事件
            switch (e.Message)
            {
                case 0://盘路
                    append_postCmd(CmdType.ChangePage, 101, 0, 0);
                    break;
                case 1://故障
                    append_postCmd(CmdType.ChangePage, 102, 0, 0);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制：按钮绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawRectangle(m_BlackLinePen, m_FrameRect);
            dcGs.DrawString(m_FrameStr, m_ChineseFont, m_BlackBrush, m_FrameRect, FontInfo.SfLc);

            //BoolList[UIObj.InBoolList[0]+i]
            m_Rect1Flag = false;
            m_Rect2Flag = false;
            m_Rect3Flag = false;
            m_Rect4Flag = false;
            for (m_I = 0; m_I < 3; m_I++)
            {
                if (BoolList[UIObj.InBoolList[0] + m_I])
                {
                    m_Rect1Flag = true;
                    dcGs.DrawImage(m_ResourceImages[m_I], m_Rects[0]);
                }
                if(BoolList[UIObj.InBoolList[3] + m_I])
                {
                    m_Rect2Flag = true;
                    dcGs.DrawImage(m_ResourceImages[m_I], m_Rects[1]);
                }
                if (BoolList[UIObj.InBoolList[6] + m_I])
                {
                    m_Rect3Flag = true;
                    dcGs.DrawImage(m_ResourceImages[m_I], m_Rects[2]);
                }
                if(BoolList[UIObj.InBoolList[9] + m_I])
                {
                    m_Rect4Flag = true;
                    dcGs.DrawImage(m_ResourceImages[m_I], m_Rects[3]);
                }
            }

            if (!m_Rect1Flag) { dcGs.DrawImage(m_ResourceImages[3], m_Rects[0]); }
            if (!m_Rect2Flag) { dcGs.DrawImage(m_ResourceImages[3], m_Rects[1]); }
            if (!m_Rect3Flag) { dcGs.DrawImage(m_ResourceImages[3], m_Rects[2]); }
            if (!m_Rect4Flag) { dcGs.DrawImage(m_ResourceImages[3], m_Rects[3]); }

            base.paint(dcGs);
        }
        #endregion
    }
}
