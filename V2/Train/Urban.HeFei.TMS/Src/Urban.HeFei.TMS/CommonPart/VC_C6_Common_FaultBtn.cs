#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-07
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：公共组件-No.6-公共按钮
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

namespace Urban.HeFei.TMS.CommonPart
{
    /// <summary>
    /// 功能描述：公共组件-No6-公共按钮_故障
    /// 创建人：lih
    /// 创建时间：2015-8-7
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class VcC6CommonFaultBtn : baseClass
    {
        private List<Image> m_ResourceImage = new List<Image>();//图片资源

        private Button m_FaultBtn;//故障按钮

        private ViewState m_OldViewState=ViewState.MainInterface;

        private ButtonStyle[] m_FaultBtnStyles;
        private string m_BtnStr = "故障";
         
        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "公共视图-公共故障按钮";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "VC_C6_Common_FaultBtn";
        //}

        /// <summary>
        /// init
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

            m_FaultBtnStyles = new ButtonStyle[2];

            m_FaultBtnStyles[0] = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[0], DownImage = m_ResourceImage[0] };
            m_FaultBtnStyles[1] = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[1], DownImage = m_ResourceImage[1] };

            m_FaultBtn = new Button(
                   m_BtnStr,
                   new RectangleF(702, 449, 85, 50),
                  (int)ViewState.FaultInfo,
                   m_FaultBtnStyles[0],
                   false
                   );
            m_FaultBtn.ClickEvent += btn_ClickEvent;

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
          

            if ((nPoint.X >= m_FaultBtn.Rect.X)
                    && (nPoint.X <= m_FaultBtn.Rect.X + m_FaultBtn.Rect.Width)
                    && (nPoint.Y >= m_FaultBtn.Rect.Y)
                    && (nPoint.Y <= m_FaultBtn.Rect.Y + m_FaultBtn.Rect.Height))
            {
                m_FaultBtn.MouseDown(nPoint);
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
            if ((nPoint.X >= m_FaultBtn.Rect.X)
                    && (nPoint.X <= m_FaultBtn.Rect.X + m_FaultBtn.Rect.Width)
                    && (nPoint.Y >= m_FaultBtn.Rect.Y)
                    && (nPoint.Y <= m_FaultBtn.Rect.Y + m_FaultBtn.Rect.Height))
            {
                    m_FaultBtn.MouseUp(nPoint);
            }
   
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (e.Message == (int)ViewState.FaultInfo)//故障按钮
            {
                CommonStatus.FaultReturnViewState = CommonStatus.CurrentViewState;
                append_postCmd(CmdType.ChangePage, e.Message, 0, 0);
                CommonStatus.CurrentViewState = ViewState.FaultInfo;
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
            m_FaultBtn.Style = (BoolList[UIObj.InBoolList[0]]) ? m_FaultBtnStyles[1] : m_FaultBtnStyles[0];
           m_FaultBtn.Paint(dcGs);

            base.paint(dcGs);
        }

        #endregion
    }
}
