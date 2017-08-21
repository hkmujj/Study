#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-25
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图501-空调-No.1-界面2
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

namespace Urban.HeFei.TMS.AC
{
    /// <summary>
    /// 功能描述：视图501-空调-No.1-界面2
    /// 创建人：lih
    /// 创建时间：2015-8-25
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V501AirConditionerC0Page2 : baseClass
    {

        #region 稀有变量
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private Button m_BtnsDownTabView;
        private Button m_BtnClose;
        private string m_PageStr = "页2-2";
        private Rectangle m_PageRect = new Rectangle(710, 335, 56, 27);
        private SolidBrush m_BlackBrush = new SolidBrush(Color.Black);

        private Font m_ChineseFont = new Font("宋体", 10);
        private Font m_ChineseFontB = new Font("宋体", 14);
        private Font m_ChineseFontA = new Font("宋体", 14, FontStyle.Bold);
        private Rectangle[] m_FrameRects;
        private Rectangle m_FrameSpeA = new Rectangle(158, 470, 116, 24);
        private string[] m_FrameStr;
        private int[] m_RectYs;
        private int[] m_RectXs;

        private Rectangle[] m_Rect1;
        private Rectangle[] m_Rect2;
        private Rectangle[] m_Rect3;
        private Rectangle[] m_Rect4;
        private int m_I, m_J = 0;

        private string[] m_NewAirValveStatus;
        private string[] m_ReturnAirValveStatus;
        private Rectangle[] m_RectStr1;
        private Rectangle[] m_RectStr2;
        private Rectangle[] m_RectStr3;
        private Rectangle[] m_RectStr4;

        private SolidBrush m_RectBrush1 = new SolidBrush(Color.FromArgb(1, 116, 154));
        private SolidBrush m_RectBrush2 = new SolidBrush(Color.FromArgb(255, 0, 24));
        private SolidBrush m_RectBrush3 = new SolidBrush(Color.FromArgb(147, 147, 147));

        private int[] m_Rect1Index;
        private int[] m_Rect2Index;
        private int[] m_Rect3Index;
        private int[] m_Rect4Index;

        private bool[] m_Rect1Flags;
        private bool[] m_Rect2Flags;
        private bool[] m_Rect3Flags;
        private bool[] m_Rect4Flags;

        private int m_NewAirValveFlag = 0;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "空调-No.1-界面2";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V501_AirConditioner_C0_Page2";
        //}

        /// <summary>
        /// 初始化函数：导入图片、创建控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {

            m_FrameRects = new Rectangle[4];
            m_FrameRects[0] = new Rectangle(12, 262, 674, 43);
            m_FrameRects[1] = new Rectangle(12, 311, 674, 43);
            m_FrameRects[2] = new Rectangle(12, 360, 674, 43);
            m_FrameRects[3] = new Rectangle(12, 409, 674, 43);

            m_FrameStr = new string[5] { "机组1新风阀状态", "机组2新风阀状态", "机组1回风阀状态", "机组2回风阀状态", "新风阀关闭" };
            m_NewAirValveStatus = new string[6] { "3/3", "2/3", "1/3", "关闭", "故障", "?" };
            m_ReturnAirValveStatus = new string[4] { "打开", "关闭", "故障", "?" };

            m_RectXs = new int[12];
            var j = 0;
            for (m_I = 0; m_I < m_RectXs.Length; m_I = m_I + 2)
            {
                m_RectXs[m_I] = 176 + j * 86;
                m_RectXs[m_I + 1] = m_RectXs[m_I] + 31;
                j++;
            }

            m_RectYs = new int[4] { 266, 315, 364, 412 };

            m_Rect1 = new Rectangle[12];
            m_Rect2 = new Rectangle[12];
            m_Rect3 = new Rectangle[12];
            m_Rect4 = new Rectangle[12];
            m_RectStr1 = new Rectangle[12];
            m_RectStr2 = new Rectangle[12];
            m_RectStr3 = new Rectangle[12];
            m_RectStr4 = new Rectangle[12];
            for (m_I = 0; m_I < m_RectXs.Length; m_I++)
            {
                m_Rect1[m_I] = new Rectangle(m_RectXs[m_I], m_RectYs[0], 29, 33);
                m_Rect2[m_I] = new Rectangle(m_RectXs[m_I], m_RectYs[1], 29, 33);
                m_Rect3[m_I] = new Rectangle(m_RectXs[m_I], m_RectYs[2], 29, 33);
                m_Rect4[m_I] = new Rectangle(m_RectXs[m_I], m_RectYs[3], 29, 33);

                m_RectStr1[m_I] = new Rectangle(m_RectXs[m_I] + 1, m_RectYs[0] + 1, 28, 32);
                m_RectStr2[m_I] = new Rectangle(m_RectXs[m_I] + 1, m_RectYs[1] + 1, 28, 32);
                m_RectStr3[m_I] = new Rectangle(m_RectXs[m_I] + 1, m_RectYs[2] + 1, 28, 32);
                m_RectStr4[m_I] = new Rectangle(m_RectXs[m_I] + 1, m_RectYs[3] + 1, 28, 32);
            }


            m_Rect1Index = new int[12];
            m_Rect1Flags = new bool[12];
            for (m_I = 0; m_I < 6; m_I++)
            {
                m_Rect1Flags[2 * m_I] = false;
                m_Rect1Flags[2 * m_I + 1] = false;
                m_Rect1Index[2 * m_I] = UIObj.InBoolList[0] - m_I * 10;
                m_Rect1Index[2 * m_I + 1] = m_Rect1Index[2 * m_I] + 5;
            }

            m_Rect2Index = new int[12];
            m_Rect2Flags = new bool[12];
            for (m_I = 0; m_I < 6; m_I++)
            {
                m_Rect2Flags[2 * m_I] = false;
                m_Rect2Flags[2 * m_I + 1] = false;
                m_Rect2Index[2 * m_I] = UIObj.InBoolList[2] - m_I * 10;
                m_Rect2Index[2 * m_I + 1] = m_Rect2Index[2 * m_I] + 5;
            }

            m_Rect3Index = new int[12];
            m_Rect3Flags = new bool[12];
            for (m_I = 0; m_I < 6; m_I++)
            {
                m_Rect3Flags[2 * m_I] = false;
                m_Rect3Flags[2 * m_I + 1] = false;
                m_Rect3Index[2 * m_I] = UIObj.InBoolList[4] - m_I * 6;
                m_Rect3Index[2 * m_I + 1] = m_Rect3Index[2 * m_I] + 3;
            }

            m_Rect4Index = new int[12];
            m_Rect4Flags = new bool[12];
            for (m_I = 0; m_I < 6; m_I++)
            {
                m_Rect4Flags[2 * m_I] = false;
                m_Rect4Flags[2 * m_I + 1] = false;
                m_Rect4Index[2 * m_I] = UIObj.InBoolList[6] - m_I * 6;
                m_Rect4Index[2 * m_I + 1] = m_Rect4Index[2 * m_I] + 3;
            }

            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });


            var bs1 = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[0], DownImage = m_ResourceImage[0] };

            m_BtnsDownTabView = new Button(
                  "",
                  new RectangleF(713, 359, 57, 34),
                 3,
                  bs1,
                  false
                  );
            m_BtnsDownTabView.ClickEvent += btn1_ClickEvent;//up


            m_BtnClose = new Button(
                   "点击关闭",
                   new RectangleF(279, 457, 89, 48),
                   (int)ViewState.AirConditionP2NewAirClose,
                   new ButtonStyle()
                   {
                       FontStyle = new FontStyleEs() { Font = new Font("宋体", 14), TextBrush = Brushs.BlackBrush, StringFormat = FontInfo.SfCc },
                       Background = m_ResourceImage[1],
                       DownImage = m_ResourceImage[2]
                   },
                   false
                   );
            m_BtnClose.ClickEvent += _btn_Close_ClickEvent;
            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + CommonStatus.SdBoolBaseNumber, 0, 0);

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

            if ((nPoint.X >= m_BtnsDownTabView.Rect.X)
                && (nPoint.X <= m_BtnsDownTabView.Rect.X + m_BtnsDownTabView.Rect.Width)
                && (nPoint.Y >= m_BtnsDownTabView.Rect.Y)
                && (nPoint.Y <= m_BtnsDownTabView.Rect.Y + m_BtnsDownTabView.Rect.Height))
            {
                m_BtnsDownTabView.MouseDown(nPoint);
            }

            if ((nPoint.X >= m_BtnClose.Rect.X)
               && (nPoint.X <= m_BtnClose.Rect.X + m_BtnClose.Rect.Width)
               && (nPoint.Y >= m_BtnClose.Rect.Y)
               && (nPoint.Y <= m_BtnClose.Rect.Y + m_BtnClose.Rect.Height))
            {
                m_BtnClose.MouseDown(nPoint);
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

            if ((nPoint.X >= m_BtnsDownTabView.Rect.X)
                && (nPoint.X <= m_BtnsDownTabView.Rect.X + m_BtnsDownTabView.Rect.Width)
                && (nPoint.Y >= m_BtnsDownTabView.Rect.Y)
                && (nPoint.Y <= m_BtnsDownTabView.Rect.Y + m_BtnsDownTabView.Rect.Height))
            {
                m_BtnsDownTabView.MouseUp(nPoint);
            }

            if ((nPoint.X >= m_BtnClose.Rect.X)
               && (nPoint.X <= m_BtnClose.Rect.X + m_BtnClose.Rect.Width)
               && (nPoint.Y >= m_BtnClose.Rect.Y)
               && (nPoint.Y <= m_BtnClose.Rect.Y + m_BtnClose.Rect.Height))
            {
                m_BtnClose.MouseUp(nPoint);
            }

            return base.mouseUp(nPoint);
        }


        /// <summary>
        ///btn1_ClickEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn1_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            append_postCmd(CmdType.ChangePage, (int)ViewState.AirConditioner, 0, 0);
        }

        /// <summary>
        /// _btn_Close_ClickEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _btn_Close_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (e.Message == (int)ViewState.AirConditionP2NewAirClose)
            {
                m_NewAirValveFlag = (m_NewAirValveFlag == 0) ? 1 : 0;
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
            m_BtnsDownTabView.Paint(dcGs);
            m_BtnClose.Paint(dcGs);
            dcGs.DrawString(m_PageStr, m_ChineseFontA, m_BlackBrush, m_PageRect, FontInfo.SfCc);

            dcGs.DrawString(m_FrameStr[4], m_ChineseFontB, m_BlackBrush, m_FrameSpeA, FontInfo.SfLc);

            for (m_I = 0; m_I < m_FrameRects.Length; m_I++)
            {
                dcGs.DrawRectangle(m_BlackLinePen, m_FrameRects[m_I]);
                dcGs.DrawString(m_FrameStr[m_I], m_ChineseFontB, m_BlackBrush, m_FrameRects[m_I], FontInfo.SfLc);
            }
            if (1 == m_NewAirValveFlag)
            {
                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + CommonStatus.SdBoolBaseNumber, 1, 0);
            }
            else
            {
                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + CommonStatus.SdBoolBaseNumber, 0, 0);
            }
            paint_ValveState(dcGs, m_Rect1, 6, 5, m_Rect1Flags, 3, m_Rect1Index);
            paint_ValveState(dcGs, m_Rect2, 6, 5, m_Rect2Flags, 3, m_Rect2Index);
            PainValueState(dcGs, m_Rect3, 6, 3, m_Rect3Flags, 9, m_Rect3Index);
            PainValueState(dcGs, m_Rect4, 6, 3, m_Rect4Flags, 9, m_Rect4Index);

            base.paint(dcGs);
        }
        Rectangle m_Rect = new Rectangle();
        private void PainValueState(Graphics g, Rectangle[] rec, int recLen, int stateLen, bool[] rectFlags, int stateStart,
            int[] rectIndex)
        {
            for (int i = 0; i < recLen; i++)
            {
                rectFlags[2 * i] = false;
                rectFlags[2 * i + 1] = false;
                m_Rect.X = rec[2 * i].X;
                m_Rect.Y = rec[2 * i].Y;
                m_Rect.Height = rec[2 * i].Height;
                m_Rect.Width = rec[2 * i].Width + rec[2 * i + 1].Width;
                for (m_J = 0; m_J < stateLen; m_J++)
                {
                   
                    if (BoolList[rectIndex[2 * i] + m_J])
                    {
                        rectFlags[2 * i] = true;
                        g.DrawImage(m_ResourceImage[stateStart + m_J], m_Rect);
                    }
                    //if (BoolList[rectIndex[2 * m_I + 1] + m_J])
                    //{
                    //    rectFlags[2 * m_I + 1] = true;
                    //    g.DrawImage(m_ResourceImage[stateStart + m_J], rec[2 * m_I + 1]);
                    //}
                }
                if (rectFlags[2 * i] == false) { g.DrawImage(m_ResourceImage[8], m_Rect); }
               // if (rectFlags[2 * m_I + 1] == false) { g.DrawImage(m_ResourceImage[8], rec[2 * m_I + 1]); }
            }
        }
        /// <summary>
        /// paint_ValveState
        /// </summary>
        /// <param name="dcGs"></param>
        /// <param name="_rectTemp"></param>
        /// <param name="rectLen"></param>
        /// <param name="stateLen"></param>
        /// <param name="_rectFlags"></param>
        /// <param name="stateStart"></param>
        /// <param name="rectIndexs"></param>
        private void paint_ValveState(Graphics dcGs, Rectangle[] rectTemp, int rectLen, int stateLen, bool[] rectFlags, int stateStart, int[] rectIndexs)
        {
            for (m_I = 0; m_I < rectLen; m_I++)
            {
                rectFlags[2 * m_I] = false;
                rectFlags[2 * m_I + 1] = false;
                for (m_J = 0; m_J < stateLen; m_J++)
                {
                    if (BoolList[rectIndexs[2 * m_I] + m_J])
                    {
                        rectFlags[2 * m_I] = true;
                        dcGs.DrawImage(m_ResourceImage[stateStart + m_J], rectTemp[2 * m_I]);
                    }
                    if (BoolList[rectIndexs[2 * m_I + 1] + m_J])
                    {
                        rectFlags[2 * m_I + 1] = true;
                        dcGs.DrawImage(m_ResourceImage[stateStart + m_J], rectTemp[2 * m_I + 1]);
                    }
                }
                if (rectFlags[2 * m_I] == false) { dcGs.DrawImage(m_ResourceImage[8], rectTemp[2 * m_I]); }
                if (rectFlags[2 * m_I + 1] == false) { dcGs.DrawImage(m_ResourceImage[8], rectTemp[2 * m_I + 1]); }

                //dcGs.DrawRectangle(_blackLinePen, _rect3[i]);
                //dcGs.FillRectangle(_rectBrush1, _rectStr3[i]);
                //dcGs.DrawString(_returnAirValveStatus[0], _chineseFont, _blackBrush, _rect3[i], FontInfo.SF_CC);
            }

        }
        #endregion
    }
}
