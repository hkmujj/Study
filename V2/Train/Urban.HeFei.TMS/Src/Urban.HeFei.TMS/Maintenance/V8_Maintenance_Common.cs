#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-07
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：维护界面-No.10-公共部分
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.HeFei.TMS.Common;

namespace Urban.HeFei.TMS.Maintenance
{
    /// <summary>
    /// 功能描述：维护界面-公共按钮
    /// 创建人：lih
    /// 创建时间：2015-08-06
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V8MaintenanceCommon:baseClass
    {
        #region 私有变量
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private Button m_BackBtn;//返回按钮
        private ViewState m_OldViewState = ViewState.MainInterface;
        private ButtonStyle m_BackBtnStyles;
        private SolidBrush m_BackgrounpSolidBrush;
        private Rectangle m_BackgrounpRectangle;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "Maintenance-公共按钮";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V8_Maintenance_Common";
        //}

        /// <summary>
        /// 初始化
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

            m_BackgrounpSolidBrush = new SolidBrush(Color.FromArgb(215, 215, 215));
            m_BackgrounpRectangle = new Rectangle(0, 516, 800, 84);

            m_BackBtnStyles = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[0], DownImage = m_ResourceImage[0] };

            m_BackBtn = new Button(
                    "",
                    new RectangleF(668, 520, 109, 75),
                   (int)ViewState.MtCommonBackBtn,
                    m_BackBtnStyles,
                    false
                    );
            m_BackBtn.ClickEvent += btn_ClickEvent;

            return true;
        }

        /// <summary>
        /// 设置读取文本标志
        /// </summary>
        /// <param name="nPara"></param>
        /// <returns></returns>
        //public override bool canSetStringList(int nPara)
        //{
        //    if (nPara == 2)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        /// <summary>
        /// 获取文本信息
        /// </summary>
        /// <param name="nIndex"></param>
        /// <param name="cStr"></param>
        //public override void addStringByLine(int nIndex, string cStr)
        //{
        //    String[] split = cStr.Split(new char[] { '\t' });
        //}
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
            if ((nPoint.X >= m_BackBtn.Rect.X)
                   && (nPoint.X <= m_BackBtn.Rect.X + m_BackBtn.Rect.Width)
                   && (nPoint.Y >= m_BackBtn.Rect.Y)
                   && (nPoint.Y <= m_BackBtn.Rect.Y + m_BackBtn.Rect.Height))
            {
                m_BackBtn.MouseDown(nPoint);
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
            if ((nPoint.X >= m_BackBtn.Rect.X)
                    && (nPoint.X <= m_BackBtn.Rect.X + m_BackBtn.Rect.Width)
                    && (nPoint.Y >= m_BackBtn.Rect.Y)
                    && (nPoint.Y <= m_BackBtn.Rect.Y + m_BackBtn.Rect.Height))
            {
                m_BackBtn.MouseUp(nPoint);
            }
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            CommonStatus.BroadcastReturnViewState = CommonStatus.CurrentViewState;
            if (e.Message == (int)ViewState.MtCommonBackBtn)
            {
                append_postCmd(CmdType.ChangePage, (int)ViewState.Maintenance, 0, 0);
                CommonStatus.CurrentViewState = ViewState.Maintenance;
                CommonStatus.CurrentInterfaceName = CommonStatus.InterfaceNameDicts[CommonStatus.CurrentViewState];
            }
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            dcGs.FillRectangle(m_BackgrounpSolidBrush, m_BackgrounpRectangle);
            m_BackBtn.Paint(dcGs);
            base.paint(dcGs);
        }

        #endregion
    }
}
