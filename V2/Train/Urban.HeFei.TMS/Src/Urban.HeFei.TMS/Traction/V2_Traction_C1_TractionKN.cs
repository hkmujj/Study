#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-06
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图2-牵引-No.1-牵引力
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common.Control;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using Urban.HeFei.TMS.Common;

namespace Urban.HeFei.TMS
{
    /// <summary>
    /// 功能描述：视图2-牵引-No.1-牵引力
    /// 创建人：lih
    /// 创建时间：2015-08-07
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V2TractionC1TractionKn:baseClass
    {
        #region 私有变量
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private Font m_Font;//字体
        private Button m_BtnCheck;//自检按钮
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private Pen m_DegreeLinePen = new Pen(new SolidBrush(Color.FromArgb(154,215,235)), 1.6f);
        private List<int> m_PointY;
        private Font m_ChineseFont = new Font("宋体", 16, FontStyle.Regular);
        private Font m_DigitFont = new Font("Arial", 16, FontStyle.Regular);
        private Brush m_BlackBrush = new SolidBrush(Color.Black);
        private Brush m_SpeAFont = new SolidBrush(Color.FromArgb(0, 255, 2));
        private Brush m_SpeBFont = new SolidBrush(Color.FromArgb(84, 171, 0));
        private Rectangle m_FrameRect = new Rectangle(12, 311, 674, 144);
        private string[]m_FrameStr= {"牵引力(KN)","电制动力(KN)"};
        private int m_I, m_J = 0;
        private Rectangle[] m_ChildrenRects;

        private Rectangle[] m_TractionRects;
        private int m_TempAHeight, m_TempBHeight = 0;

        private int[] m_OldHeight = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };

        private int[] m_TimeCount = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };

        int m_ChangeAValue = 0, m_ChangeBValue=0, m_CurrentValue = 0;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "牵引-牵引力";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V2_Traction_C1_TractionKN";
        //}

        /// <summary>
        /// 初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_Font = new Font("宋体", 10.5f, FontStyle.Bold);

            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });

            m_PointY = new List<int>();

            m_PointY.Add(321);
            m_PointY.Add(334);
            m_PointY.Add(346);
            m_PointY.Add(359);
            m_PointY.Add(371);
            m_PointY.Add(384);
            m_PointY.Add(396);
            m_PointY.Add(409);
            m_PointY.Add(421);
            m_PointY.Add(433);
            m_PointY.Add(447);

            m_ChildrenRects = new Rectangle[3];
            m_ChildrenRects[0] = new Rectangle(235, 436, 15, 18);
            m_ChildrenRects[1] = new Rectangle(206, 376, 44, 18);
            m_ChildrenRects[2] = new Rectangle(189, 314, 61, 18);

            m_TractionRects = new Rectangle[8];
            m_TractionRects[0] = new Rectangle(270, 446, 23, 0);
            m_TractionRects[1] = new Rectangle(292, 446, 25, 0);

            m_TractionRects[2] = new Rectangle(356, 446, 23, 0);
            m_TractionRects[3] = new Rectangle(378, 446, 25, 0);

            m_TractionRects[4] = new Rectangle(442, 446, 23, 0);
            m_TractionRects[5] = new Rectangle(464, 446, 25, 0);

            m_TractionRects[6] = new Rectangle(528, 446, 23, 0);
            m_TractionRects[7] = new Rectangle(550, 446, 25, 0);


            return true;
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 绘制界面
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawRectangle(m_BlackLinePen, m_FrameRect);

            if(BoolList[UIObj.InBoolList[1]])
            {
                dcGs.DrawString(m_FrameStr[1], m_ChineseFont, m_BlackBrush, m_FrameRect, FontInfo.SfLc);
            }
            else
            {
                dcGs.DrawString(m_FrameStr[0], m_ChineseFont, m_BlackBrush, m_FrameRect, FontInfo.SfLc);
            }
            

            dcGs.DrawRectangle(m_BlackLinePen, 269, 321, 48, 126);
            dcGs.DrawRectangle(m_BlackLinePen, 355, 321, 48, 126);
            dcGs.DrawRectangle(m_BlackLinePen, 441, 321, 48, 126);
            dcGs.DrawRectangle(m_BlackLinePen, 527, 321, 48, 126);
         

            for (m_J = 0; m_J < m_PointY.Count; m_J++)
            {
                dcGs.DrawLine(m_DegreeLinePen, 249, m_PointY[m_J], 269, m_PointY[m_J]);
                dcGs.DrawLine(m_DegreeLinePen, 576, m_PointY[m_J], 595, m_PointY[m_J]);
            }

            for (m_I = 0; m_I < 3; m_I++)
            {
                for ( m_J = 0; m_J < m_PointY.Count; m_J++)
                {
                    dcGs.DrawLine(m_DegreeLinePen, 318 + m_I * 86, m_PointY[m_J], 355 + m_I * 86, m_PointY[m_J]);
                }
            }

            dcGs.DrawString("0", m_DigitFont, m_BlackBrush, m_ChildrenRects[0], FontInfo.SfRc);

            dcGs.DrawString("50", m_DigitFont, m_BlackBrush, m_ChildrenRects[1], FontInfo.SfRc);

            dcGs.DrawString("100", m_DigitFont, m_BlackBrush, m_ChildrenRects[2], FontInfo.SfRc);
           
            OnPaintDynamic(dcGs);

            //for (i = 0; i < 4; i++)
            //{
            //    tempAHeight = (int)(1.44 * FloatList[UIObj.InFloatList[i]-1]);//需求值
            //    _tractionRects[2 * i].Y = 446 - tempAHeight;
            //    _tractionRects[2 * i].Height = tempAHeight;

            //    tempBHeight = (int)(1.44 * FloatList[UIObj.InFloatList[i]]);//实际值
            //    _tractionRects[2 * i + 1].Y = 446 - tempBHeight;
            //    _tractionRects[2 * i + 1].Height = tempBHeight;

            //    dcGs.FillRectangle(_SpeAFont, _tractionRects[2 * i]);
            //    dcGs.FillRectangle(_SpeBFont, _tractionRects[2 * i + 1]);
            //}

            base.paint(dcGs);
        }

        /// <summary>
        /// 绘制动态图形
        /// </summary>
        /// <param name="dcGs"></param>
        private void OnPaintDynamic(Graphics dcGs)
        {
            for (m_I = 0; m_I < 4; m_I++)
            {
                m_TempAHeight = (int)(1.44 * FloatList[UIObj.InFloatList[m_I] - 1]);//需求值
                m_TractionRects[2 * m_I].Y = 446 - m_TempAHeight;
                m_TractionRects[2 * m_I].Height = m_TempAHeight;
                dcGs.FillRectangle(m_SpeAFont, m_TractionRects[2 * m_I]);
                //changeAValue = tempAHeight - _oldHeight[2 * i];
                //onPaintOnce(dcGs, changeAValue, tempAHeight, 2 * i, _SpeAFont);


                m_TempBHeight = (int)(1.44 * FloatList[UIObj.InFloatList[m_I]]);//实际值
                m_TractionRects[2 * m_I + 1].Y = 446 - m_TempBHeight;
                m_TractionRects[2 * m_I + 1].Height = m_TempBHeight;
                dcGs.FillRectangle(m_SpeBFont, m_TractionRects[2 * m_I + 1]);
                //changeBValue = tempBHeight - _oldHeight[2 * i + 1];
               // onPaintOnce(dcGs, changeBValue, tempBHeight, 2 * i+1, _SpeBFont);
            }
        }

        /// <summary>
        /// 绘制单个动态图形
        /// </summary>
        /// <param name="tempchangeValue"></param>
        /// <param name="tempHeight"></param>
        /// <param name="index"></param>
        /// <param name="font"></param>
        private void OnPaintOnce(Graphics dcGs,int tempchangeValue, int tempHeight, int index, Brush font)
        {
            if (tempchangeValue > 4 || tempchangeValue < -4)
            {
                if (m_TimeCount[index] * 4 < Math.Abs(tempchangeValue))
                {
                    m_TimeCount[index]++;
                    m_CurrentValue = (tempchangeValue > 4) ? m_TimeCount[index] * 4 : (0 - tempchangeValue - m_TimeCount[index] * 4);
                    m_TractionRects[index].Y = 446 - m_CurrentValue;
                    m_TractionRects[index].Height = m_CurrentValue;
                    dcGs.FillRectangle(font, m_TractionRects[index]);
                }
                else
                {
                    m_OldHeight[index] = tempHeight;
                    m_TimeCount[index] = 0;
                    m_TractionRects[index].Y = 446 - tempHeight;
                    m_TractionRects[index].Height = tempHeight;
                    dcGs.FillRectangle(font, m_TractionRects[index]);
                }
            }
        }

        #endregion
    }
}
