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

using System;
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
    /// 功能描述：公共组件-No6-公共按钮-旁路
    /// 创建人：lih
    /// 创建时间：2015-8-7
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class VcC7CommonBypassBtn:baseClass
    {
        #region 私有变量
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private Button m_BypassBtn;//旁路按钮
        private ViewState m_OldViewState = ViewState.MainInterface;
        private ButtonStyle[] m_BypassBatnStyles;
        private string[] m_BtnStr;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "公共视图-公共旁路按钮";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
       

        /// <summary>
        /// init
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {

            m_BtnStr = new string[3] { "旁路", "返回", "" };

            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });

            m_BypassBatnStyles = new ButtonStyle[2];

            m_BypassBatnStyles[0] = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[0], DownImage = m_ResourceImage[0] };
            m_BypassBatnStyles[1] = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[2], DownImage = m_ResourceImage[2] };
          

            m_BypassBtn = new Button(
                  m_BtnStr[0],
                   new RectangleF(702, 272, 85, 50),
                  (int)ViewState.BypassInfo,
                   m_BypassBatnStyles[0],
                   false
                   );
            m_BypassBtn.ClickEvent += btn_ClickEvent;
            
            
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

        #region
        /// <summary>
        /// mouseDown
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
            //判断点击的位置是否属于该button集合
            if ((nPoint.X >= m_BypassBtn.Rect.X)
                   && (nPoint.X <= m_BypassBtn.Rect.X + m_BypassBtn.Rect.Width)
                   && (nPoint.Y >= m_BypassBtn.Rect.Y)
                   && (nPoint.Y <= m_BypassBtn.Rect.Y + m_BypassBtn.Rect.Height))
            {
                m_BypassBtn.MouseDown(nPoint);
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
            if ((nPoint.X >= m_BypassBtn.Rect.X)
                    && (nPoint.X <= m_BypassBtn.Rect.X + m_BypassBtn.Rect.Width)
                    && (nPoint.Y >= m_BypassBtn.Rect.Y)
                    && (nPoint.Y <= m_BypassBtn.Rect.Y + m_BypassBtn.Rect.Height))
            {

                m_BypassBtn.MouseUp(nPoint);
            }

            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// btn_ClickEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (e.Message == (int)ViewState.BypassInfo)//旁路按钮
            {
                if (CommonStatus.CurrentViewState == ViewState.BypassInfo || CommonStatus.CurrentViewState == ViewState.BypassInfo2)//返回到前一界面（按旁路按钮时的界面）
                {
                    append_postCmd(CmdType.ChangePage, (int)m_OldViewState, 0, 0);
                }
                else//跳转到旁路界面
                {
                    m_OldViewState = CommonStatus.CurrentViewState;
                    append_postCmd(CmdType.ChangePage, (int)ViewState.BypassInfo, 0, 0);
                }
            }
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 绘制界面
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            if (CommonStatus.CurrentViewState == ViewState.BypassInfo || CommonStatus.CurrentViewState == ViewState.BypassInfo2)
            {
                m_BypassBtn.Text = m_BtnStr[1];
            }
            else
            {
                m_BypassBtn.Text = m_BtnStr[0];
            }

            m_BypassBtn.Style = (BoolList[UIObj.InBoolList[0]]) ? m_BypassBatnStyles[1] : m_BypassBatnStyles[0];
            m_BypassBtn.Paint(dcGs);

            base.paint(dcGs);
        }

        #endregion
    }
}
