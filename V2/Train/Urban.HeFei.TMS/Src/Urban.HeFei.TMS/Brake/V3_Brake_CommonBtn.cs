#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-07
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：制动-No.4-公共按钮
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.HeFei.TMS.Common;

namespace Urban.HeFei.TMS.Brake
{
    /// <summary>
    /// 功能描述：制动-No.4-公共按钮
    /// 创建人：lih
    /// 创建时间：2015-08-06
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V3BrakeCommonBtn:baseClass
    {
        #region 私有变量
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private List<Button> m_BtnsDownTabView = new List<Button>();//按钮列表
        private ViewState m_CurrentPage = ViewState.BrakePage1;
        private Font m_ChineseFont = new Font("宋体", 14, FontStyle.Bold);
        private SolidBrush m_BlackBrush = new SolidBrush(Color.Black);
        private Rectangle m_Rect = new Rectangle(710, 335, 56, 27);
        string m_Str = "";
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "制动-公共按钮";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V3_Brake_CommonBtn";
        //}

        /// <summary>
        /// 初始化数据
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

            var bs1 = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[0], DownImage = m_ResourceImage[0] };
            var bs2 = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[1], DownImage = m_ResourceImage[1] };


            var btn1 = new Button(
                    "",
                    new RectangleF(713, 359, 57, 34),
                   3,
                    bs1,
                    false
                    );
            btn1.ClickEvent += btn1_ClickEvent;//up

            var btn2 = new Button(
                   "",
                   new RectangleF(713, 398, 57, 34),
                  301,
                   bs2,
                   false
                   );
            btn2.ClickEvent += btn2_ClickEvent;//down

            m_BtnsDownTabView.Add(btn1);

            m_BtnsDownTabView.Add(btn2);

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
            //bool flag = false;
            //for (int i = 0; i < this._btns_Down_TabView.Count; i++)
            //{
            //    if((nPoint.X>=this._btns_Down_TabView[i].Rect.X) 
            //        && (nPoint.X<=this._btns_Down_TabView[i].Rect.X+this._btns_Down_TabView[i].Rect.Width)
            //        && (nPoint.Y >= this._btns_Down_TabView[i].Rect.Y)
            //        && (nPoint.Y <= this._btns_Down_TabView[i].Rect.Y + this._btns_Down_TabView[i].Rect.Height))
            //    {
            //        flag = true;
            //        break;
            //    }
            //}

            //if (flag)
            //{
            //    this._btns_Down_TabView.ToList().ForEach(a => a.MouseDown(nPoint));
            //}
            return base.mouseDown(nPoint);
        }


        /// <summary>
        /// mouseUp
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
             var flag = false;
            for (var i = 0; i < m_BtnsDownTabView.Count; i++)
            {
                if((nPoint.X>=m_BtnsDownTabView[i].Rect.X) 
                    && (nPoint.X<=m_BtnsDownTabView[i].Rect.X+m_BtnsDownTabView[i].Rect.Width)
                    && (nPoint.Y >= m_BtnsDownTabView[i].Rect.Y)
                    && (nPoint.Y <= m_BtnsDownTabView[i].Rect.Y + m_BtnsDownTabView[i].Rect.Height))
                {
                    flag = true;
                    break;
                }
            }

            if (flag)
            {
                m_BtnsDownTabView.ToList().ForEach(a => a.MouseUp(nPoint));
            }
            return base.mouseUp(nPoint);
        }


        /// <summary>
        /// btn1_ClickEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn1_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (m_CurrentPage == ViewState.BrakePage1)
            {
                return;
            }
            else if (m_CurrentPage == ViewState.BrakePage2)
            {
                m_CurrentPage = ViewState.BrakePage1;
                append_postCmd(CmdType.ChangePage,(int)m_CurrentPage, 0, 0);
            }
            else if (m_CurrentPage == ViewState.BrakePage3)
            {
                m_CurrentPage = ViewState.BrakePage2;
                append_postCmd(CmdType.ChangePage, (int)m_CurrentPage, 0, 0);
            }
           
            
        }

        /// <summary>
        /// btn2_ClickEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn2_ClickEvent(object sender, ClickEventArgs<int> e)
        {
           
            if (m_CurrentPage == ViewState.BrakePage1)
            {
                m_CurrentPage = ViewState.BrakePage2;
                append_postCmd(CmdType.ChangePage,(int) m_CurrentPage, 0, 0);
            }
            else if (m_CurrentPage == ViewState.BrakePage2)
            {
                m_CurrentPage = ViewState.BrakePage3;
                append_postCmd(CmdType.ChangePage, (int)m_CurrentPage, 0, 0);
            }
            else if (m_CurrentPage == ViewState.BrakePage3)
            {
                return;
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
            m_BtnsDownTabView.ToList().ForEach(a => a.Paint(dcGs));
            if (m_CurrentPage == ViewState.BrakePage1)
            {
                m_Str = "页1-3";
            }
            else if (m_CurrentPage == ViewState.BrakePage2)
            {
                m_Str = "页2-3";
            }
            else if (m_CurrentPage == ViewState.BrakePage3)
            {
                m_Str = "页3-3";
            }

            dcGs.DrawString(m_Str, m_ChineseFont,m_BlackBrush, m_Rect, FontInfo.SfCc);

            base.paint(dcGs);
        }

        #endregion
    }
}
