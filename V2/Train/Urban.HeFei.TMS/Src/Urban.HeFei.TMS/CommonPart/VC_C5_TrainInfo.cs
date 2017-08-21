#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-6
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：公共组件-No.5-显示列车车辆信息
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using Urban.HeFei.TMS.Common;
using Urban.HeFei.TMS.Resource;

namespace Urban.HeFei.TMS.CommonPart
{
    /// <summary>
    /// 功能描述：公共组件-No.5-显示列车车辆信息
    /// 创建人：lih
    /// 创建时间：2015-8-6
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class VcC5TrainInfo : baseClass
    {
        #region 私有变量
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private Pen m_BlackBigLinePen = new Pen(new SolidBrush(Color.Black), 2.0f);
        private SolidBrush m_BgSolidBrush = new SolidBrush(Color.FromArgb(0, 153, 204));
        private Font m_DigitFont = new Font("Arial", 12, FontStyle.Regular);
        private SolidBrush m_CabSolidBrush = new SolidBrush(Color.FromArgb(147, 147, 147));

        private string m_TractionRation;
        private string m_BrakeRation;

        private int[] m_Tc2;
        private int[] m_Mp2;
        private int[] m_M2;
        private int[] m_M1;
        private int[] m_Mp1;
        private int[] m_Tc1;
        private int m_I = 0, m_J = 0;

        private Rectangle[] m_Tc2DoorRects;
        private Rectangle[] m_Mp2DoorRects;
        private Rectangle[] m_M2DoorRects;

        private Rectangle[] m_M1DoorRects;
        private Rectangle[] m_Mp1DoorRects;
        private Rectangle[] m_Tc1DoorRects;

        private bool[] m_Tc2Flags;
        private bool[] m_Mp2Flags;
        private bool[] m_M2Flags;

        private bool[] m_M1Flags;
        private bool[] m_Mp1Flags;
        private bool[] m_Tc1Flags;

        private Rectangle[] m_Tc2Pecu;
        private Rectangle[] m_Mp2Pecu;
        private Rectangle[] m_M2Pecu;

        private Rectangle[] m_M1Pecu;
        private Rectangle[] m_Mp1Pecu;
        private Rectangle[] m_Tc1Pecu;

        private int[] m_Tc2PecuIndexs;
        private int[] m_Mp2PecuIndexs;
        private int[] m_M2PecuIndexs;
        private int[] m_M1PecuIndexs;
        private int[] m_Mp1PecuIndexs;
        private int[] m_Tc1PecuIndexs;


        private int[] m_Tc2Count1 = new int[4] { 0, 0, 0, 0 };

        private int[] m_Mp2Count1 = new int[4] { 0, 0, 0, 0 };

        private int[] m_M2Count1 = new int[4] { 0, 0, 0, 0 };

        private int[] m_M1Count1 = new int[4] { 0, 0, 0, 0 };

        private int[] m_Mp1Count1 = new int[4] { 0, 0, 0, 0 };

        private int[] m_Tc1Count1 = new int[4] { 0, 0, 0, 0 };

        private Rectangle[] m_CabRects;
        private int[] m_CabIndexs;

        private Rectangle[] m_FrameRects;
        private Rectangle[] m_DiretionRects;

        private Point[] m_DiretionLeftPoints;
        private Point[] m_DiretionRightPoints;

        private Rectangle[] m_TAndbRects;

        private Rectangle[] m_PantographRects;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "公共视图-列车车辆信息";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "VC_C5_TrainInfo";
        //}

        public override bool init(ref int nErrorObjectIndex)
        {

            m_FrameRects = new Rectangle[6];
            for (m_I = 0; m_I < 6; m_I++)
            {
                m_FrameRects[m_I] = new Rectangle(168 + m_I * 86, 170, 78, 38);
            }

            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });
            var temp = UIObj.InBoolList;

            if (temp.Count < 6) { return false; }
            //门的顺序:2 4 6 8 1 2 5 7
            m_Tc2 = new int[8];
            m_Tc2Flags = new bool[8];
            for (m_I = 0; m_I < 8; m_I++)
            {
                m_Tc2Flags[m_I] = false;
                m_Tc2[m_I] = temp[0] + 6 * m_I;
            }

            m_Mp2 = new int[8];
            m_Mp2Flags = new bool[8];
            for (m_I = 0; m_I < 8; m_I++)
            {
                m_Mp2Flags[m_I] = false;
                m_Mp2[m_I] = temp[1] + 6 * m_I;
            }

            m_M2 = new int[8];
            m_M2Flags = new bool[8];
            for (m_I = 0; m_I < 8; m_I++)
            {
                m_M2Flags[m_I] = false;
                m_M2[m_I] = temp[2] + 6 * m_I;
            }

            //门的顺序: 7 5 3 1 8 6 4 2
            m_M1 = new int[8];
            m_M1Flags = new bool[8];
            for (m_I = 0; m_I < 8; m_I++)
            {
                m_M1Flags[m_I] = false;
                m_M1[m_I] = temp[3] - 6 * m_I;
            }

            m_Mp1 = new int[8];
            m_Mp1Flags = new bool[8];
            for (m_I = 0; m_I < 8; m_I++)
            {
                m_Mp1Flags[m_I] = false;
                m_Mp1[m_I] = temp[4] - 6 * m_I;
            }

            m_Tc1 = new int[8];
            m_Tc1Flags = new bool[8];
            for (m_I = 0; m_I < 8; m_I++)
            {
                m_Tc1Flags[m_I] = false;
                m_Tc1[m_I] = temp[5] - 6 * m_I;
            }

            m_Tc2DoorRects = new Rectangle[8];
            for (m_I = 0; m_I < 4; m_I++)
            {
                m_Tc2DoorRects[m_I] = new Rectangle(168 + m_I * 21, 154, 15, 16);
                m_Tc2DoorRects[m_I + 4] = new Rectangle(168 + m_I * 21, 208, 15, 16);
            }

            m_Mp2DoorRects = new Rectangle[8];
            for (m_I = 0; m_I < 4; m_I++)
            {
                m_Mp2DoorRects[m_I] = new Rectangle(254 + m_I * 21, 154, 15, 16);
                m_Mp2DoorRects[m_I + 4] = new Rectangle(254 + m_I * 21, 208, 15, 16);
            }


            m_M2DoorRects = new Rectangle[8];
            for (m_I = 0; m_I < 4; m_I++)
            {
                m_M2DoorRects[m_I] = new Rectangle(340 + m_I * 21, 154, 15, 16);
                m_M2DoorRects[m_I + 4] = new Rectangle(340 + m_I * 21, 208, 15, 16);
            }


            m_M1DoorRects = new Rectangle[8];
            for (m_I = 0; m_I < 4; m_I++)
            {
                m_M1DoorRects[m_I] = new Rectangle(426 + m_I * 21, 154, 15, 16);
                m_M1DoorRects[m_I + 4] = new Rectangle(426 + m_I * 21, 208, 15, 16);
            }

            m_Mp1DoorRects = new Rectangle[8];
            for (m_I = 0; m_I < 4; m_I++)
            {
                m_Mp1DoorRects[m_I] = new Rectangle(512 + m_I * 21, 154, 15, 16);
                m_Mp1DoorRects[m_I + 4] = new Rectangle(512 + m_I * 21, 208, 15, 16);
            }

            m_Tc1DoorRects = new Rectangle[8];
            for (m_I = 0; m_I < 4; m_I++)
            {
                m_Tc1DoorRects[m_I] = new Rectangle(598 + m_I * 21, 154, 15, 16);
                m_Tc1DoorRects[m_I + 4] = new Rectangle(598 + m_I * 21, 208, 15, 16);
            }

            m_Tc2Pecu = new Rectangle[4];
            m_Tc2PecuIndexs = new int[4];
            m_Tc2PecuIndexs[0] = temp[6];
            m_Tc2PecuIndexs[1] = temp[6] + 2;
            m_Tc2PecuIndexs[2] = temp[18];
            m_Tc2PecuIndexs[3] = temp[18] + 2;
            m_Tc2Pecu[0] = new Rectangle(231, 170, 15, 14);
            m_Tc2Pecu[1] = new Rectangle(210, 196, 15, 14);
            m_Tc2Pecu[2] = new Rectangle(189, 170, 15, 14);
            m_Tc2Pecu[3] = new Rectangle(168, 196, 15, 14);

            m_Mp2Pecu = new Rectangle[4];
            m_Mp2PecuIndexs = new int[4];
            m_Mp2PecuIndexs[0] = temp[7];
            m_Mp2PecuIndexs[1] = temp[7] + 2;
            m_Mp2PecuIndexs[2] = temp[19];
            m_Mp2PecuIndexs[3] = temp[19] + 2;
            m_Mp2Pecu[0] = new Rectangle(317, 170, 15, 14);
            m_Mp2Pecu[1] = new Rectangle(296, 196, 15, 14);
            m_Mp2Pecu[2] = new Rectangle(275, 170, 15, 14);
            m_Mp2Pecu[3] = new Rectangle(254, 196, 15, 14);


            m_M2Pecu = new Rectangle[4];
            m_M2PecuIndexs = new int[4];
            m_M2PecuIndexs[0] = temp[8];
            m_M2PecuIndexs[1] = temp[8] + 2;
            m_M2PecuIndexs[2] = temp[20];
            m_M2PecuIndexs[3] = temp[20] + 2;
            m_M2Pecu[0] = new Rectangle(403, 170, 15, 14);
            m_M2Pecu[1] = new Rectangle(382, 196, 15, 14);
            m_M2Pecu[2] = new Rectangle(361, 170, 15, 14);
            m_M2Pecu[3] = new Rectangle(340, 196, 15, 14);

            m_M1Pecu = new Rectangle[4];
            m_M1PecuIndexs = new int[4];
            m_M1PecuIndexs[0] = temp[9];
            m_M1PecuIndexs[1] = temp[9] + 2;
            m_M1PecuIndexs[2] = temp[21];
            m_M1PecuIndexs[3] = temp[21] + 2;
            m_M1Pecu[0] = new Rectangle(489, 170, 15, 14);
            m_M1Pecu[1] = new Rectangle(468, 196, 15, 14);
            m_M1Pecu[2] = new Rectangle(447, 170, 15, 14);
            m_M1Pecu[3] = new Rectangle(426, 196, 15, 14);

            m_Mp1Pecu = new Rectangle[4];
            m_Mp1PecuIndexs = new int[4];
            m_Mp1PecuIndexs[0] = temp[10];
            m_Mp1PecuIndexs[1] = temp[10] + 2;
            m_Mp1PecuIndexs[2] = temp[22];
            m_Mp1PecuIndexs[3] = temp[22] + 2;
            m_Mp1Pecu[0] = new Rectangle(575, 170, 15, 14);
            m_Mp1Pecu[1] = new Rectangle(554, 196, 15, 14);
            m_Mp1Pecu[2] = new Rectangle(533, 170, 15, 14);
            m_Mp1Pecu[3] = new Rectangle(512, 196, 15, 14);

            m_Tc1Pecu = new Rectangle[4];
            m_Tc1PecuIndexs = new int[4];
            m_Tc1PecuIndexs[0] = temp[11];
            m_Tc1PecuIndexs[1] = temp[11] + 2;
            m_Tc1PecuIndexs[2] = temp[23];
            m_Tc1PecuIndexs[3] = temp[23] + 2;
            m_Tc1Pecu[0] = new Rectangle(661, 170, 15, 14);
            m_Tc1Pecu[1] = new Rectangle(640, 196, 15, 14);
            m_Tc1Pecu[2] = new Rectangle(619, 170, 15, 14);
            m_Tc1Pecu[3] = new Rectangle(598, 196, 15, 14);

            m_CabRects = new Rectangle[2];
            m_CabIndexs = new int[2] { temp[12], temp[13] };
            m_CabRects[0] = new Rectangle(145, 154, 45, 70);
            m_CabRects[1] = new Rectangle(654, 154, 45, 70);

            m_DiretionRects = new Rectangle[2];
            m_DiretionLeftPoints = new Point[3];
            m_DiretionRightPoints = new Point[3];
            m_DiretionLeftPoints[0] = new Point(122, 189);
            m_DiretionLeftPoints[1] = new Point(135, 154);
            m_DiretionLeftPoints[2] = new Point(135, 224);

            m_DiretionRightPoints[0] = new Point(708, 154);
            m_DiretionRightPoints[1] = new Point(708, 224);
            m_DiretionRightPoints[2] = new Point(721, 189);

            m_DiretionRects[0] = new Rectangle(122, 154, 13, 70);
            m_DiretionRects[1] = new Rectangle(708, 154, 13, 70);



            m_TAndbRects = new Rectangle[6];
            m_TAndbRects[0] = new Rectangle(18, 132, 10, 14);
            m_TAndbRects[1] = new Rectangle(18, 211, 10, 14);
            m_TAndbRects[2] = new Rectangle(60, 132, 68, 14);
            m_TAndbRects[3] = new Rectangle(60, 211, 68, 14);

            m_TAndbRects[4] = new Rectangle(35, 98, 24, 79);
            m_TAndbRects[5] = new Rectangle(35, 177, 24, 79);

            m_PantographRects = new Rectangle[2];
            m_PantographRects[0] = new Rectangle(280, 120, 42, 30);
            m_PantographRects[1] = new Rectangle(540, 120, 42, 30);

            return true;
        }

        /// <summary>
        /// 设置读取文本标志
        /// </summary>
        /// <param name="nPara"></param>
        /// <returns></returns>
        //public override bool canSetStringList(int nPara)
        //{
        //    if (nPara == 1)
        //    {
        //        return true;
        //    }
        //    else if (nPara == 2)
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
        //}
        #endregion

        #region 界面绘制
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            paint_Pantograph(dcGs);
            paint_TAndB(dcGs);
            paint_TrainInfo(dcGs);

            base.paint(dcGs);
        }

        /// <summary>
        /// 绘制受电弓
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Pantograph(Graphics dcGs)
        {
            if (BoolList[UIObj.InBoolList[16] + 1])
            {
                dcGs.DrawImage(m_ResourceImage[23], m_PantographRects[1]);
            }
            else if (BoolList[UIObj.InBoolList[16] + 2])
            {
                dcGs.DrawImage(m_ResourceImage[24], m_PantographRects[1]);
            }
            else if (BoolList[UIObj.InBoolList[16] + 3])
            {
                dcGs.DrawImage(m_ResourceImage[26], m_PantographRects[1]);
            }
            else if (BoolList[UIObj.InBoolList[16]])
            {
                dcGs.DrawImage(m_ResourceImage[25], m_PantographRects[1]);
            }
            else
            {
                dcGs.DrawImage(m_ResourceImage[27], m_PantographRects[1]);
            }

            if (BoolList[UIObj.InBoolList[17] + 1])
            {
                dcGs.DrawImage(m_ResourceImage[0], m_PantographRects[0]);
            }
            else if (BoolList[UIObj.InBoolList[17] + 2])
            {
                dcGs.DrawImage(m_ResourceImage[1], m_PantographRects[0]);
            }
            else if (BoolList[UIObj.InBoolList[17] + 3])
            {
                dcGs.DrawImage(m_ResourceImage[3], m_PantographRects[0]);
            }
            else if (BoolList[UIObj.InBoolList[17]])
            {
                dcGs.DrawImage(m_ResourceImage[2], m_PantographRects[0]);
            }
            else
            {
                dcGs.DrawImage(m_ResourceImage[4], m_PantographRects[0]);
            }


        }

        /// <summary>
        /// paint_TAndB
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_TAndB(Graphics dcGs)
        {


            dcGs.FillRectangle(Brushs.GreenBrush, GetCuurent(m_TAndbRects[4], true, FloatList[UIObj.InFloatList[0]]));
            dcGs.FillRectangle(Brushs.WhiteBrush, GetCuurent(m_TAndbRects[5], false, FloatList[UIObj.InFloatList[1]]));


            dcGs.DrawRectangle(m_BlackLinePen, m_TAndbRects[4]);
            dcGs.DrawRectangle(m_BlackLinePen, m_TAndbRects[5]);

            dcGs.DrawString("T", m_DigitFont, Brushs.BlackBrush, m_TAndbRects[0], FontInfo.SfCc);

            dcGs.DrawString("B", m_DigitFont, Brushs.BlackBrush, m_TAndbRects[1], FontInfo.SfCc);

            m_TractionRation = (((int)(FloatList[UIObj.InFloatList[0]] * 10)) / 10.0f).ToString("F0") + "%";

            dcGs.DrawString(m_TractionRation, m_DigitFont, Brushs.BlackBrush, m_TAndbRects[2], FontInfo.SfCc);

            m_BrakeRation = (((int)(FloatList[UIObj.InFloatList[1]] * 10)) / 10.0f).ToString("F0") + "%";

            dcGs.DrawString(m_BrakeRation, m_DigitFont, Brushs.BlackBrush, m_TAndbRects[3], FontInfo.SfCc);


        }

        private Rectangle GetCuurent(Rectangle rec, bool flag, float value)
        {
            var result = new Rectangle();
            if (flag)
            {
                var s = (int)(rec.Height * (value / 100));
                result.X = rec.X;
                result.Y = rec.Y + rec.Height - s;
                result.Width = rec.Width;
                result.Height = s;
            }
            else
            {
                var s = (int)(rec.Height * (value / 100));
                result.X = rec.X;
                result.Y = rec.Y;
                result.Width = rec.Width;
                result.Height = s;

            }
            return result;
        }
        /// <summary>
        /// paint_TrainInfo
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_TrainInfo(Graphics dcGs)
        {

            if (BoolList[m_CabIndexs[0]])
            {
                //dcGs.DrawImage(this._resource_Image[5], _cabRects[0]);
                //_cabRects[0].Width = _cabRects[0].Width + 20;
                dcGs.DrawArc(m_BlackLinePen, m_CabRects[0], 90, 180);
                dcGs.FillPie(Brushs.GreenBrush, m_CabRects[0], 90, 180);
            }
            else if (BoolList[m_CabIndexs[0] + 1])
            {
                dcGs.DrawArc(m_BlackLinePen, m_CabRects[0], 90, 180);
                //dcGs.DrawImage(this._resource_Image[7], _cabRects[0]);
            }
            else
            {
                dcGs.DrawArc(m_BlackLinePen, m_CabRects[0], 90, 180);
                dcGs.FillPie(m_CabSolidBrush, m_CabRects[0], 90, 180);
            }

            if (BoolList[m_CabIndexs[1]])
            {
                dcGs.DrawArc(m_BlackLinePen, m_CabRects[1], 90, -180);
                dcGs.FillPie(Brushs.GreenBrush, m_CabRects[1], 90, -180);
                // dcGs.DrawImage(this._resource_Image[6], _cabRects[1]);
            }
            else if (BoolList[m_CabIndexs[1] + 1])
            {
                dcGs.DrawArc(m_BlackLinePen, m_CabRects[1], 90, -180);
                //dcGs.DrawImage(this._resource_Image[8], _cabRects[1]);
            }
            else
            {
                dcGs.DrawArc(m_BlackLinePen, m_CabRects[1], 90, -180);
                dcGs.FillPie(m_CabSolidBrush, m_CabRects[1], 90, -180);
            }


            if (BoolList[UIObj.InBoolList[15]])
            {
                dcGs.DrawPolygon(m_BlackLinePen, m_DiretionLeftPoints);
                dcGs.FillPolygon(Brushs.GreenBrush, m_DiretionLeftPoints);
                //dcGs.DrawImage(this._resource_Image[11], _diretionRects[0]);
            }

            if (BoolList[UIObj.InBoolList[14]])
            {
                dcGs.DrawPolygon(m_BlackLinePen, m_DiretionRightPoints);
                dcGs.FillPolygon(Brushs.GreenBrush, m_DiretionRightPoints);
                //dcGs.DrawImage(this._resource_Image[12],_diretionRects[1]);
            }


            //dcGs.DrawArc(_blackLinePen, 145, 154, 44, 70, 90, 180);
            //dcGs.DrawArc(_blackLinePen, 655, 154, 44, 70, 90, -180);

            for (m_I = 0; m_I < 6; m_I++)
            {
                dcGs.DrawRectangle(m_BlackBigLinePen, (168 + m_I * 86), 154, 78, 70);
                dcGs.DrawString("123456", m_DigitFont, Brushs.BlackBrush, m_FrameRects[m_I], FontInfo.SfCc);
                if (m_I < 5)
                {
                    dcGs.DrawLine(m_BlackBigLinePen, 246 + m_I * 86, 165, 254 + m_I * 86, 165);
                    dcGs.DrawLine(m_BlackBigLinePen, 246 + m_I * 86, 212, 254 + m_I * 86, 212);
                }
            }

            //绘制门的状态
            for (m_I = 0; m_I < 8; m_I++)
            {
                m_Tc2Flags[m_I] = false;
                m_Mp2Flags[m_I] = false;
                m_M2Flags[m_I] = false;
                m_M1Flags[m_I] = false;
                m_Mp1Flags[m_I] = false;
                m_Tc1Flags[m_I] = false;

                for (m_J = 0; m_J < 6; m_J++)
                {
                    if (BoolList[m_Tc2[m_I] + m_J])
                    {
                        m_Tc2Flags[m_I] = true;
                        dcGs.DrawImage(m_ResourceImage[15 + m_J], m_Tc2DoorRects[m_I]);
                    }
                    if (BoolList[m_Mp2[m_I] + m_J])
                    {
                        m_Mp2Flags[m_I] = true;
                        dcGs.DrawImage(m_ResourceImage[15 + m_J], m_Mp2DoorRects[m_I]);
                    }
                    if (BoolList[m_M2[m_I] + m_J])
                    {
                        m_M2Flags[m_I] = true;
                        dcGs.DrawImage(m_ResourceImage[15 + m_J], m_M2DoorRects[m_I]);
                    }
                    if (BoolList[m_M1[m_I] + m_J])
                    {
                        m_M1Flags[m_I] = true;
                        dcGs.DrawImage(m_ResourceImage[15 + m_J], m_M1DoorRects[m_I]);
                    }
                    if (BoolList[m_Mp1[m_I] + m_J])
                    {
                        m_Mp1Flags[m_I] = true;
                        dcGs.DrawImage(m_ResourceImage[15 + m_J], m_Mp1DoorRects[m_I]);
                    }
                    if (BoolList[m_Tc1[m_I] + m_J])
                    {
                        m_Tc1Flags[m_I] = true;
                        dcGs.DrawImage(m_ResourceImage[15 + m_J], m_Tc1DoorRects[m_I]);
                    }
                }

            }

            for (m_I = 0; m_I < 8; m_I++)
            {
                if (!m_Tc2Flags[m_I]) { dcGs.DrawImage(m_ResourceImage[21], m_Tc2DoorRects[m_I]); }
                if (!m_Mp2Flags[m_I]) { dcGs.DrawImage(m_ResourceImage[21], m_Mp2DoorRects[m_I]); }
                if (!m_M2Flags[m_I]) { dcGs.DrawImage(m_ResourceImage[21], m_M2DoorRects[m_I]); }

                if (!m_M1Flags[m_I]) { dcGs.DrawImage(m_ResourceImage[21], m_M1DoorRects[m_I]); }
                if (!m_Mp1Flags[m_I]) { dcGs.DrawImage(m_ResourceImage[21], m_Mp1DoorRects[m_I]); }
                if (!m_Tc1Flags[m_I]) { dcGs.DrawImage(m_ResourceImage[21], m_Tc1DoorRects[m_I]); }

            }


            for (m_I = 0; m_I < 4; m_I++)
            {
                PaintPecu(dcGs, m_Tc2PecuIndexs[m_I], m_Tc2Pecu[m_I], ref m_Tc2Count1[m_I]);
                PaintPecu(dcGs, m_Mp2PecuIndexs[m_I], m_Mp2Pecu[m_I], ref m_Mp2Count1[m_I]);
                PaintPecu(dcGs, m_M2PecuIndexs[m_I], m_M2Pecu[m_I], ref m_M2Count1[m_I]);

                PaintPecu(dcGs, m_Tc1PecuIndexs[m_I], m_Tc1Pecu[m_I], ref m_Tc1Count1[m_I]);
                PaintPecu(dcGs, m_Mp1PecuIndexs[m_I], m_Mp1Pecu[m_I], ref m_Mp1Count1[m_I]);
                PaintPecu(dcGs, m_M1PecuIndexs[m_I], m_M1Pecu[m_I], ref m_M1Count1[m_I]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dcGs"></param>
        /// <param name="index"></param>
        /// <param name="rect"></param>
        private void PaintPecu(Graphics dcGs, int index, Rectangle rect, ref int count1)
        {
            if (BoolList[index])
            {
                if (count1 < 2)
                {
                    count1++;
                }
                else if (count1 >= 2 && count1 <= 4)
                {
                    dcGs.DrawImage(m_ResourceImage[22], rect);
                    count1++;
                }
                else if (count1 > 4)
                {
                    count1 = 0;
                }
            }
            else if (BoolList[index + 1])
            {
                dcGs.DrawImage(m_ResourceImage[22], rect);
            }
        }
        #endregion

    }
}
