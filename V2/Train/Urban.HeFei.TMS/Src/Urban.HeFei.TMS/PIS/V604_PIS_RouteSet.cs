#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-25
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图604-帮助-通讯帮助-No.0
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

namespace Urban.HeFei.TMS.PIS
{
    /// <summary>
    /// 功能描述：视图604-帮助-通讯帮助-No.0
    /// 创建人：lih
    /// 创建时间：2015-8-25
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V604PISRouteSet: baseClass
    {
        #region 私有变量
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private Font m_ChineseFont = new Font("宋体", 18);
        private Button[] m_Btns;//按钮列表
        private int m_I = 0;
        private Rectangle[] m_FrameRects;

        private string[] m_FrameStrs;
        private Rectangle[] m_FrameStrRects;
        private string[] m_BtnNames;
        private ViewState m_CurrentViewState;
        private Rectangle[] m_WhiteStaInfoRects;

       // private TextBox[] _textBoxs;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "PIS-旅程设置";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V604_PIS_RouteSet";
        //}

        /// <summary>
        /// 初始化函数：初始化操作
        /// </summary>
        /// <param name="nErrorObjectIndex">错误索性</param>
        /// <returns>初始化是否成功</returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_FrameStrs = new string[4] { "旅程设置", "下一站", "终点站", "跳站" };
            m_FrameRects = new Rectangle[3];
            m_FrameRects[0] = new Rectangle(37, 179, 554, 56);
            m_FrameRects[1] = new Rectangle(37, 242, 554, 56);
            m_FrameRects[2] = new Rectangle(37, 305, 554, 56);

            //_textBoxs = new TextBox[3];

            //_textBoxs[0] = new TextBox(
            //        new RectangleF(288, 180, 303, 54),
            //        new TextBoxStyle()
            //        {
            //            FontStyle = new FontStyle_ES() 
            //            { Font = new Font("宋体", 15, FontStyle.Regular), 
            //                StringFormat = FontInfo.SF_CC, 
            //                TextBrush = (SolidBrush)Brushes.Black 
            //            },
            //            Background = null,
            //            BackgroundBrushes = Brushs.WhiteBrush
            //        },
            //        60400);

            //_textBoxs[1] = new TextBox(
            //        new RectangleF(288, 243, 303, 54),
            //        new TextBoxStyle()
            //        {
            //            FontStyle = new FontStyle_ES()
            //            {
            //                Font = new Font("宋体", 15, FontStyle.Regular), 
            //                StringFormat = FontInfo.SF_CC, 
            //                TextBrush = (SolidBrush)Brushes.Black 
            //            },
            //            Background = null,
            //            BackgroundBrushes=Brushs.WhiteBrush
            //        },
            //        60401);

            //_textBoxs[2] = new TextBox(
            //       new RectangleF(288, 306, 303, 54),
            //       new TextBoxStyle()
            //       {
            //           FontStyle = new FontStyle_ES()
            //           {
            //               Font = new Font("宋体", 15, FontStyle.Regular), 
            //               StringFormat = FontInfo.SF_CC, 
            //               TextBrush = (SolidBrush)Brushes.Black 
            //           },
            //           Background = null,
            //           BackgroundBrushes = Brushs.WhiteBrush
            //       },
            //       60402);


            m_WhiteStaInfoRects = new Rectangle[3];
            m_WhiteStaInfoRects[0] = new Rectangle(288, 180, 303, 54);
            m_WhiteStaInfoRects[1] = new Rectangle(288, 243, 303, 54);
            m_WhiteStaInfoRects[2] = new Rectangle(288, 306, 303, 54);

            m_FrameStrRects = new Rectangle[4];
            m_FrameStrRects[0] = new Rectangle(49, 146, 120, 28);
            m_FrameStrRects[1] = new Rectangle(37, 179, 251, 56);
            m_FrameStrRects[2] = new Rectangle(37, 242, 251, 56);
            m_FrameStrRects[3] = new Rectangle(37, 305, 251, 56);

            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });

            m_BtnNames = new string[4] { "选择", "修改", "确认", "取消" };
            var bs1 = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[0], DownImage = m_ResourceImage[1] };
            m_Btns = new Button[5];
            //下一站 选择
            m_Btns[0] = new Button(m_BtnNames[0], new Rectangle(604, 179, 84, 50), (int)ViewState.PisrSelectNextStation, bs1, true);
            //终点站 选择
            m_Btns[1] = new Button(m_BtnNames[0], new Rectangle(604, 242, 84, 50), (int)ViewState.PisrSelectEndStation, bs1, true);
            //修改
            m_Btns[2] = new Button(m_BtnNames[1], new Rectangle(604, 305, 84, 50), (int)ViewState.PisrModifyBtn, bs1, true);

            //确认
            m_Btns[3] = new Button(m_BtnNames[2], new Rectangle(701, 179, 84, 50), (int)ViewState.PisrConfrimBtn, bs1, true);
            //取消
            m_Btns[4] = new Button(m_BtnNames[3], new Rectangle(701, 242, 84, 50), (int)ViewState.PisrCancelBtn, bs1, true);

            for (m_I = 0; m_I < m_Btns.Length; m_I++)
            {
                m_Btns[m_I].ClickEvent+=V604_PIS_RouteSet_ClickEvent;
            }

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

            //for (i = 0; i < _textBoxs.Length; i++)
            //{
            //    if ((nPoint.X >= this._textBoxs[i].Rect.X)
            //           && (nPoint.X <= this._textBoxs[i].Rect.X + this._textBoxs[i].Rect.Width)
            //           && (nPoint.Y >= this._textBoxs[i].Rect.Y)
            //           && (nPoint.Y <= this._textBoxs[i].Rect.Y + this._textBoxs[i].Rect.Height))
            //    {
            //        _textBoxs[i].MouseDown(nPoint);
            //        break;
            //    }
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
        void V604_PIS_RouteSet_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if(!Enum.TryParse(e.Message.ToString(),out m_CurrentViewState))
            {
                return;
            }
            switch (m_CurrentViewState)
            {
                case ViewState.PISAutoModel:
                    append_postCmd(CmdType.ChangePage, e.Message, 0, 0);
                    break;
                case ViewState.PISSemiAutoModel:
                    append_postCmd(CmdType.ChangePage, e.Message, 0, 0);
                    break;
                default: break;
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
            for (m_I = 0; m_I < m_FrameRects.Length; m_I++)
            {
                dcGs.DrawRectangle(m_BlackLinePen, m_FrameRects[m_I]);
            }

            for (m_I = 0; m_I < m_FrameStrRects.Length; m_I++)
            {
                dcGs.DrawString(m_FrameStrs[m_I], m_ChineseFont, Brushs.BlackBrush, m_FrameStrRects[m_I], FontInfo.SfLc);
            }

            for (m_I = 0; m_I < m_WhiteStaInfoRects.Length; m_I++)//_whiteStaInfoRects.Length; i++)i < _textBoxs.Length;i++
            {
                dcGs.FillRectangle(Brushs.WhiteBrush, m_WhiteStaInfoRects[m_I]);
            }

            for (m_I = 0; m_I < m_Btns.Length; m_I++)
            {
                m_Btns[m_I].Paint(dcGs);
            }
            base.paint(dcGs);
        }
        #endregion
    }
}
