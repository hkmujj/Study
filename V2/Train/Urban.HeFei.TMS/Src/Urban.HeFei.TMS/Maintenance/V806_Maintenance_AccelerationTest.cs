#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-06
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：维护-No.6-加速度测试
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using Urban.HeFei.TMS.Common;

namespace Urban.HeFei.TMS.Maintenance
{
    /// <summary>
    /// 功能描述：维护-No.6-加速度测试
    /// 创建人：lih
    /// 创建时间：2015-08-06
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V806MaintenanceAccelerationTest:baseClass
    {
        #region 私有变量
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private Button[] m_Btns;
        private string[] m_BtnStrs;
        private ListBox<AccelerationTestInfo> m_Listbox;
        private Rectangle[] m_BtnRects;
        private int m_I = 0;
        private string[] m_ListStrs;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "维护-加速度测试";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V806_Maintenance_AccelerationTest";
        //}

        /// <summary>
        /// 初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_BtnStrs = new string[5] { "初始化", "20", "40", "60", "80" };
            m_ListStrs = new string[8] { "类别", "初速度", "末速度", "距离", "时间", "加速度", "牵引", "制动" };
            m_Btns = new Button[5];

             UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });

            var btnStyles = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[0], DownImage = m_ResourceImage[1] };

            m_BtnRects=new Rectangle[5];
            m_BtnRects[0]=new Rectangle(172,369,84,50);
            m_BtnRects[1]=new Rectangle(280,369,84,50);
            m_BtnRects[2]=new Rectangle(369,369,84,50);
            m_BtnRects[3]=new Rectangle(458,369,84,50);
            m_BtnRects[4]=new Rectangle(547,369,84,50);

            for (m_I = 0; m_I < m_Btns.Length; m_I++)
            {
                m_Btns[m_I] = new Button(m_BtnStrs[m_I], m_BtnRects[m_I], (int)(ViewState.MtAtInitalBtn + m_I), btnStyles, true);
                m_Btns[m_I].ClickEvent += V806_Maintenance_AccelerationTest_ClickEvent;
            }

            m_Listbox = new ListBox<AccelerationTestInfo>(new RectangleF(25, 152, 728, 186), new List<AccelerationTestInfo>(),
              new ListBoxHeader() { Text = m_ListStrs[0], TextBrush = Brushs.BlackBrush, HeaderFont = new Font("宋体", 14), DataFont = new Font("Arial", 14, FontStyle.Regular), Height = 40, Width = 120, BackgroundBrush = Brushes.Transparent, Property = "Style", SfData = FontInfo.SfCc, SfHeader = FontInfo.SfCc },
              new ListBoxHeader() { Text = m_ListStrs[1], TextBrush = Brushs.BlackBrush, HeaderFont = new Font("宋体", 14), DataFont = new Font("Arial", 14f), Height = 40, Width = 120, BackgroundBrush = Brushes.Transparent, Property = "InitialSpeed", SfData = FontInfo.SfCc, SfHeader = FontInfo.SfCc },
              new ListBoxHeader() { Text = m_ListStrs[2], TextBrush = Brushs.BlackBrush, HeaderFont = new Font("宋体", 14), DataFont = new Font("Arial", 14f), Height = 40, Width = 120, BackgroundBrush = Brushes.Transparent, Property = "EndSpeed", SfData = FontInfo.SfCc, SfHeader = FontInfo.SfCc },
              new ListBoxHeader() { Text = m_ListStrs[3], TextBrush = Brushs.BlackBrush, HeaderFont = new Font("宋体", 14), DataFont = new Font("Arial", 14f), Height = 40, Width = 120, BackgroundBrush = Brushes.Transparent, Property = "Distance", SfData = FontInfo.SfCc, SfHeader = FontInfo.SfCc },
              new ListBoxHeader() { Text = m_ListStrs[4], TextBrush = Brushs.BlackBrush, HeaderFont = new Font("宋体", 14), DataFont = new Font("Arial", 14f), Height = 40, Width = 120, BackgroundBrush = Brushes.Transparent, Property = "Time", SfData = FontInfo.SfCc, SfHeader = FontInfo.SfCc },
              new ListBoxHeader() { Text = m_ListStrs[5], TextBrush = Brushs.BlackBrush, HeaderFont = new Font("宋体", 14), DataFont = new Font("Arial", 14f), Height = 40, Width = 116, BackgroundBrush = Brushes.Transparent, Property = "Acceleration", SfData = FontInfo.SfCc, SfHeader = FontInfo.SfCc }
              );
            m_Listbox.BackgroundBrush = Brushes.Transparent;
            m_Listbox.GridBrush = Brushes.Black;
            m_Listbox.RowCount = 2;

            var traction = new AccelerationTestInfo() { Style = "牵引" };
            var brake = new AccelerationTestInfo() { Style = "制动" };

            m_Listbox.DataList.Add(traction);
            m_Listbox.DataList.Add(brake);
          

            return true;
        }
        #endregion

        #region 鼠标事件
        /// <summary>
        /// mouseDown
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
            //判断点击的位置是否属于该button集合
            for (m_I = 0; m_I < m_Btns.Length; m_I++)
            {
                if ((nPoint.X >= m_Btns[m_I].Rect.X)
                       && (nPoint.X <= m_Btns[m_I].Rect.X + m_Btns[m_I].Rect.Width)
                       && (nPoint.Y >= m_Btns[m_I].Rect.Y)
                       && (nPoint.Y <= m_Btns[m_I].Rect.Y + m_Btns[m_I].Rect.Height))
                {
                    m_Btns[m_I].MouseDown(nPoint);
                    break;
                }
            }

            return base.mouseDown(nPoint);
        }


        /// <summary>
        /// mouseUp
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            for (m_I = 0; m_I < m_Btns.Length; m_I++)
            {
                if ((nPoint.X >= m_Btns[m_I].Rect.X)
                       && (nPoint.X <= m_Btns[m_I].Rect.X + m_Btns[m_I].Rect.Width)
                       && (nPoint.Y >= m_Btns[m_I].Rect.Y)
                       && (nPoint.Y <= m_Btns[m_I].Rect.Y + m_Btns[m_I].Rect.Height))
                {
                    m_Btns[m_I].MouseUp(nPoint);
                    break;
                }
            }

            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void V806_Maintenance_AccelerationTest_ClickEvent(object sender, ClickEventArgs<int> e)
        {
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            m_Listbox.Paint(dcGs);

            for (m_I = 0; m_I < m_Btns.Length; m_I++)
            {
                m_Btns[m_I].Paint(dcGs);
            }
            
            base.paint(dcGs);
        }
        #endregion
    }
}
