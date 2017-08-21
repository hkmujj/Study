#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-06
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：维护-No.7-制动自检
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
    /// 功能描述：维护-No.7-制动自检
    /// 创建人：lih
    /// 创建时间：2015-08-06
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V807MaintenanceBrakeSelfChecking:baseClass
    {
        #region 私有变量
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private Button m_Btn;
        private string m_BtnStr;
        private ListBox<BrakeCheckInfo> m_Listbox;
        private string[] m_ListStr;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "维护-制动自检";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V807_Maintenance_BrakeSelfChecking";
        //}

        /// <summary>
        /// 初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {

            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });

            m_ListStr = new string[2] { "单元一", "单元二" };
            m_BtnStr = "自检";
            m_Listbox = new ListBox<BrakeCheckInfo>(new RectangleF(227, 213, 345, 103), new List<BrakeCheckInfo>(),
             new ListBoxHeader() { Text = m_ListStr[0], TextBrush = Brushs.BlackBrush, HeaderFont = new Font("宋体", 14), DataFont = new Font("Arial", 14, FontStyle.Regular), Height = 40, Width = 170, BackgroundBrush = Brushes.Transparent, Property = "UnitOne", SfData = FontInfo.SfCc, SfHeader = FontInfo.SfCc },
             new ListBoxHeader() { Text = m_ListStr[1], TextBrush = Brushs.BlackBrush, HeaderFont = new Font("宋体", 14), DataFont = new Font("Arial", 14f), Height = 40, Width = 172, BackgroundBrush = Brushes.Transparent, Property = "UnitTwo", SfData = FontInfo.SfCc, SfHeader = FontInfo.SfCc }
             );
            m_Listbox.BackgroundBrush = Brushes.Transparent;
            m_Listbox.GridBrush = Brushes.Black;
            m_Listbox.RowCount = 1;

            var brakeCheckInfo = new BrakeCheckInfo();
            m_Listbox.DataList.Add(brakeCheckInfo);

            var btnStyles = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[0], DownImage = m_ResourceImage[1] };
            m_Btn = new Button(m_BtnStr, new Rectangle(357, 339, 84, 45), (int)(ViewState.MtBscCheckBtn), btnStyles, true);
            m_Btn.ClickEvent += _btn_ClickEvent;
     

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
            if ((nPoint.X >= m_Btn.Rect.X)
                   && (nPoint.X <= m_Btn.Rect.X + m_Btn.Rect.Width)
                   && (nPoint.Y >= m_Btn.Rect.Y)
                   && (nPoint.Y <= m_Btn.Rect.Y + m_Btn.Rect.Height))
            {
                m_Btn.MouseDown(nPoint);
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

            if ((nPoint.X >= m_Btn.Rect.X)
                   && (nPoint.X <= m_Btn.Rect.X + m_Btn.Rect.Width)
                   && (nPoint.Y >= m_Btn.Rect.Y)
                   && (nPoint.Y <= m_Btn.Rect.Y + m_Btn.Rect.Height))
            {
                m_Btn.MouseUp(nPoint);
            }


            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _btn_ClickEvent(object sender, ClickEventArgs<int> e)
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
            m_Btn.Paint(dcGs);
            base.paint(dcGs);
        }
        #endregion
    }
}
