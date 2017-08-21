#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-06
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：维护-No.8-密码设置
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
    /// 功能描述：维护-No.8-密码设置
    /// 创建人：lih
    /// 创建时间：2015-08-06
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V808MaintenancePasswordSetting:baseClass
    {
        #region 私有属性
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private Font m_ChineseFont = new Font("宋体", 16);
        private string[] m_FrameStrs;
        private Rectangle[] m_StrRects;
        private Rectangle[] m_EditRects;
        private Rectangle[] m_EditFileRects;

        private Button[] m_Btns;
        private string[] m_BtnStrs;

        private Rectangle m_DigitFrame = new Rectangle(418, 114, 307, 307);

        private int m_I, m_J = 0;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "维护-密码设置";
        }


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

            m_FrameStrs = new string[2] { "原密码:", "新密码:" };
            m_StrRects = new Rectangle[2];
            m_StrRects[0] = new Rectangle(83, 210, 92, 24);
            m_StrRects[1] = new Rectangle(83, 265, 92, 24);

            m_EditRects = new Rectangle[2];
            m_EditRects[0] = new Rectangle(182, 202, 232, 44);
            m_EditRects[1] = new Rectangle(182, 256, 232, 44);

            m_EditFileRects = new Rectangle[2];
            m_EditFileRects[0] = new Rectangle(183, 203, 231, 43);
            m_EditFileRects[1] = new Rectangle(183, 257, 231, 43);

            var digitBtnStyle = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[0], DownImage = m_ResourceImage[1] };
            var inputCancelStyle=new ButtonStyle(){FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[2], DownImage = m_ResourceImage[3] };
            var inputConfirmStyle=new ButtonStyle(){FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[4], DownImage = m_ResourceImage[5] };
            var commonBtnStyle=new ButtonStyle(){FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[6], DownImage = m_ResourceImage[7] };

             var digitBackBtnStyle = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[9], DownImage = m_ResourceImage[10] };


            m_BtnStrs=new string[13]{"确认","取消","7","8","9","4","5","6","1","2","3","0","返回"};
            m_Btns = new Button[15];
            m_Btns[0] = new Button(m_BtnStrs[0], new RectangleF(196, 318, 80, 45),(int)ViewState.MtPdsCConfirmBtn,commonBtnStyle,true);
            m_Btns[1] = new Button(m_BtnStrs[1], new RectangleF(312, 318, 80, 45),(int)ViewState.MtPdsCCancelBtn,commonBtnStyle,true);
            

            for (m_I = 0; m_I < 3; m_I++)
            {
                for (m_J = 0; m_J < 3; m_J++)
                {
                    m_Btns[m_I * 3 + m_J + 2] = new Button(m_BtnStrs[m_I * 3 + m_J + 2], new RectangleF(467 + m_J * 70, 155 + m_I * 44, 69, 43), (int)(ViewState.MtPdsDigitSeven + m_I * 3 + m_J), digitBtnStyle, true);
                }
            }

            m_Btns[11] = new Button(m_BtnStrs[11], new RectangleF(467, 287, 69, 43), (int)ViewState.MtPdsDigitZero, digitBtnStyle, true);
            m_Btns[12] = new Button(m_BtnStrs[12], new RectangleF(537, 287, 138, 43), (int)ViewState.MtPdsDigitBack, digitBackBtnStyle, true);

            m_Btns[13] = new Button(m_BtnStrs[0], new RectangleF(467, 363, 85, 45), (int)ViewState.MtPdsDigitConfirm, inputConfirmStyle, true);
            m_Btns[14] = new Button(m_BtnStrs[1], new RectangleF(591, 363, 85, 45), (int)ViewState.MtPdsDigitCancel, inputCancelStyle, true);

            for (m_I = 0; m_I < m_Btns.Length; m_I++)
            {
                m_Btns[m_I].ClickEvent += V808_Maintenance_PasswordSetting_ClickEvent;
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
        void V808_Maintenance_PasswordSetting_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            ViewState temp;
            if (!Enum.TryParse(e.Message.ToString(), out temp))
            {
                return;
            }

            switch (temp)
            {
                case ViewState.MtPdsCConfirmBtn:
                    break;
                case ViewState.MtPdsCCancelBtn:
                    break;
                case ViewState.MtPdsDigitBack:
                    break;
                case ViewState.MtPdsDigitCancel:
                    break;
                case ViewState.MtPdsDigitConfirm:
                    break;
                case ViewState.MtPdsDigitEight:
                    break;
                case ViewState.MtPdsDigitFive:
                    break;
                case ViewState.MtPdsDigitFour:
                    break;
                case ViewState.MtPdsDigitNine:
                    break;
                case ViewState.MtPdsDigitOne:
                    break;
                case ViewState.MtPdsDigitSeven:
                    break;
                case ViewState.MtPdsDigitSix:
                    break;
                case ViewState.MtPdsDigitThree:
                    break;
                case ViewState.MtPdsDigitTwo:
                    break;
                case ViewState.MtPdsDigitZero:
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
            dcGs.DrawImage(m_ResourceImage[8], m_DigitFrame);

            for (m_I = 0; m_I < m_StrRects.Length; m_I++)
            {
                dcGs.DrawString(m_FrameStrs[m_I], m_ChineseFont, Brushs.BlackBrush, m_StrRects[m_I], FontInfo.SfLc);
            }

            for (m_I = 0; m_I < m_Btns.Length; m_I++)
            {
                m_Btns[m_I].Paint(dcGs);
            }

            for (m_I = 0; m_I < m_EditFileRects.Length; m_I++)
            {
                dcGs.DrawRectangle(m_BlackLinePen, m_EditFileRects[m_I]);
            }

            for (m_I = 0; m_I < m_EditFileRects.Length; m_I++)
            {
                dcGs.FillRectangle(Brushs.WhiteBrush, m_EditFileRects[m_I]);
            }

                base.paint(dcGs);
        }
        #endregion
    }
}
