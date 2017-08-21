using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace KumM_TMS.空调
{
    /// <summary>
    /// 空调设置
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class Air_condition : baseClass
    {
        public static Air_condition Instance { get; private set; }
        private IList<int> m_TagertTemperature;
        private IList<int> m_DisplayTemperature;
        private IList<string> m_TemperatureModel;
        private int SetValues;
        private Timer m_Timer;
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "空调设置";
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }


        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void paint(Graphics dcGs)
        {
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        void SetVatle(int value)
        {
            SetValues = value;
            m_Timer.Change(2000, int.MaxValue);
        }
        public override bool mouseDown(Point nPoint)
        {
            int index = 0;
            for (; index < Rect.Count; ++index)
            {
                if (Rect[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 0:
                    buttonIsDown[0] = true;
                    SetVatle(-1);
                    break;
                case 1:
                    buttonIsDown[1] = true;
                    SetVatle(-2);
                    break;
                case 2:
                    buttonIsDown[2] = true;
                    SetVatle(1);
                    break;
                case 3:
                    buttonIsDown[3] = true;
                    SetVatle(2);
                    break;
                case 4:
                    buttonIsDown[4] = true;
                    SetVatle(0);
                    break;
                case 5:
                    buttonIsDown[5] = true;
                    SetVatle(0);
                    break;
                case 6:
                    buttonIsDown[6] = !buttonIsDown[6];
                    break;
            }
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            for (; index < Rect.Count; ++index)
            {
                if (Rect[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 0:
                    buttonIsDown[0] = false;
                    break;
                case 1:
                    buttonIsDown[1] = false;
                    break;
                case 2:
                    buttonIsDown[2] = false;
                    break;
                case 3:
                    buttonIsDown[3] = false;
                    break;
                case 4:
                    buttonIsDown[4] = false;
                    break;
                case 5:
                    buttonIsDown[5] = false;
                    break;
                //case 6:
                //    buttonIsDown[6] = false;
                //    break;
            }
            return base.mouseUp(nPoint);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        /// <summary>
        /// 画框架
        /// </summary>
        /// <param name="e"></param>
        private void DrawFrame(Graphics e)
        {
            for (int i = 0; i < 42; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen, rects[i].X, rects[i].Y, rects[i].Width, rects[i].Height);
            }

            for (int i = 0; i < 6; i++)
            {
                e.DrawString(FormatStyle.Str8[i], FormatStyle.Font12B,
                    FormatStyle.WhiteBrush, rects[i], drawFormat);
            }

            //for (int i = 0; i < 6; i++)
            //{
            //    e.DrawLine(FormatStyle.WhitePen, pDrawPoint[i], pDrawPoint[i + 6]);
            //}
        }

        /// <summary>
        /// 画值
        /// </summary>
        /// <param name="e"></param>
        private void DrawValue(Graphics e)
        {
            //室内温度/室外温度/空调目标温度/
            for (int i = 0; i < 6; i++)
            {
                e.DrawString(FloatList[52 + i].ToString("0.0"), FormatStyle.Font12B,
                    FormatStyle.WhiteBrush, rects[6 + i], drawFormat);
                e.DrawString(FloatList[85 + i].ToString("0.0"), FormatStyle.Font12B,
                    FormatStyle.WhiteBrush, rects[12 + i], drawFormat);
                //e.DrawString(Convert.ToInt32(FloatList[116 + i]).ToString(), FormatStyle.Font12B,
                //    FormatStyle.WhiteBrush, rects[18 + i], drawFormat);
                e.DrawString(m_DisplayTemperature[i].ToString(), FormatStyle.Font12B,
                   FormatStyle.WhiteBrush, rects[18 + i], drawFormat);
                e.DrawString("自动-通风", FormatStyle.Font12B,
                   FormatStyle.WhiteBrush, rects[24 + i], drawFormat);
                e.DrawString(m_TemperatureModel[i], FormatStyle.Font12B,
                  FormatStyle.WhiteBrush, rects[30 + i], drawFormat);
            }


            //压缩机状态
            for (int i = 0; i < 24; i++)
            {
                if (BoolList[776 + i * 3])
                    e.FillRectangle(FormatStyle.LightGreenBrush, rects[42 + i]);
                else if (BoolList[777 + i * 3])
                    e.FillRectangle(FormatStyle.RedBrush, rects[42 + i]);
                else if (BoolList[778 + i * 3])
                    e.FillRectangle(FormatStyle.MediumGreySolidBrush, rects[42 + i]);
                e.DrawString(FormatStyle.Str10[i], FormatStyle.Font10,
                    FormatStyle.BlackBrush, rects[42 + i], drawFormat);
            }

            ////空调温度
            //for (int i = 0; i < 12; i++)
            //{
            //    e.DrawString(Convert.ToInt32(FloatList[116 + i]).ToString(),
            //        FormatStyle.Font14, FormatStyle.WhiteBrush, rects[45 + i], drawFormat);
            //}

            ////空调状态
            //for (int i = 0; i < 12; i++)
            //{
            //    if (BoolList[851 + i * 7])
            //        e.DrawImage(Img[0], rects[57 + i]);
            //    else if (BoolList[852 + i * 7])
            //        e.DrawImage(Img[1], rects[57 + i]);
            //    else if (BoolList[853 + i * 7])
            //        e.DrawImage(Img[2], rects[57 + i]);
            //    else if (BoolList[854 + i * 7])
            //        e.DrawImage(Img[3], rects[57 + i]);
            //    else if (BoolList[855 + i * 7])
            //        e.DrawImage(Img[4], rects[57 + i]);
            //    else if (BoolList[856 + i * 7])
            //        e.DrawImage(Img[5], rects[57 + i]);
            //    else if (BoolList[857 + i * 7])
            //        e.DrawImage(Img[6], rects[57 + i]);
            //}

            //空调设置
            e.DrawString(FormatStyle.Str8[6], FormatStyle.Font12,
                FormatStyle.WhiteBrush, rects[66], drawFormat);


            //按键状态
            for (int i = 0; i < 7; i++)
            {
                if (buttonIsDown[i])
                    e.DrawImage(Img[8], rects[70 + i]);
                else
                    e.DrawImage(Img[7], rects[70 + i]);
                e.DrawString(FormatStyle.Str9[i], FormatStyle.Font18B,
                    FormatStyle.BlackBrush, rects[70 + i], drawFormat);
            }
        }

        private void DrawOn(Graphics e)
        {
            DrawFrame(e);
            DrawValue(e);
        }
        #endregion

        public void Clear()
        {
            m_DisplayTemperature = new List<int>(m_TagertTemperature);
            m_TemperatureModel = new List<string>()
            {
                "","","","","",""
            };
        }
        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        /// <summary>
        /// 初始化坐标、数组、热区
        /// </summary>
        private void InitData()
        {
            Instance = this;
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Alignment = StringAlignment.Center;

            RightFormat.LineAlignment = StringAlignment.Far;
            RightFormat.Alignment = StringAlignment.Center;

            LeftFormat.LineAlignment = StringAlignment.Near;
            LeftFormat.Alignment = StringAlignment.Center;
            m_TagertTemperature = new List<int>()
            {
                24,24,24,24,24,24
            };
            m_DisplayTemperature = new List<int>(m_TagertTemperature);
            m_Timer = new Timer((state) =>
            {
                if (state != null)
                {
                    var condition = (Air_condition)state;
                    for (int i = 0; i < m_DisplayTemperature.Count; i++)
                    {
                        m_DisplayTemperature[i] = m_TagertTemperature[i] + Convert.ToInt32(condition.SetValues);
                        m_TemperatureModel[i] = condition.SetValues != 0 ? (condition.SetValues > 0 ? "+" + condition.SetValues.ToString() + "k" : condition.SetValues.ToString() + "k") : string.Empty;
                    }
                }
            }, this, int.MaxValue, int.MaxValue);
            m_TemperatureModel = new List<string>()
            {
                "","","","","",""
            };
            pDrawPoint = new PointF[20];

            rects = new RectangleF[120];

            Img = new Image[15];

            buttonIsDown = new bool[10];

            Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::: rects ::::::::::::::::::::::
            for (int i = 0; i < 6; i++)
            {
                rects[i] = new Rectangle(10, 170 + i * 29, 115, 29);
                for (int j = 0; j < 6; j++)
                {
                    rects[6 + i * 6 + j] = new Rectangle(125 + j * 95, 170 + i * 29, 95, 29);
                }
            }

            for (int i = 0; i < 6; i++)
            {
                //压缩机状态
                rects[42 + i * 4] = new Rectangle(128 + i * 95, 317, 21, 25);
                rects[43 + i * 4] = new Rectangle(151 + i * 95, 317, 21, 25);
                rects[44 + i * 4] = new Rectangle(174 + i * 95, 317, 21, 25);
                rects[45 + i * 4] = new Rectangle(197 + i * 95, 317, 21, 25);
            }

            rects[66] = new Rectangle(10, 380, 140, 120);

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    rects[70 + i * 5 + j] = new Rectangle(150 + j * 110, 380 + i * 60, 110, 60);
                }
            }

            //MoveScreen
            for (int i = 0; i < rects.Length; i++)
            {
                rects[i].X = (rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                rects[i].Y = (rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                rects[i].Width *= FormatStyle.Scale;
                rects[i].Height *= FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::: Rect :::::::::::::::::::::::
            for (int i = 0; i < 10; i++)
            {
                Rect.Add(new Region(rects[70 + i]));
            }
            #endregion
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat drawFormat = new StringFormat();

        public static StringFormat RightFormat = new StringFormat();

        public static StringFormat LeftFormat = new StringFormat();

        #region:::::::::::::::::::::::::::传值部分::::::::::::::::::::::::::::::::
        /// <summary>
        /// 坐标集
        /// </summary>
        internal PointF[] pDrawPoint;

        /// <summary>
        /// 矩形框集
        /// </summary>
        internal RectangleF[] rects;

        /// <summary>
        /// 图片集
        /// </summary>
        internal Image[] Img;

        /// <summary>
        /// 键是否按下
        /// </summary>
        internal bool[] buttonIsDown;

        /// <summary>
        /// 按键列表
        /// </summary>
        internal List<Region> Rect;
        #endregion#
        #endregion
    }

    /// <summary>
    /// 空调设置帮助
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class Air_conditionHelp : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "空调设置帮助";
        }



        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }


        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void paint(Graphics dcGs)
        {
            //GetValue();
            DrawOn(dcGs);
            base.paint(dcGs);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        private void DrawOn(Graphics e)
        {
            e.DrawImage(Img[0], rects[0]);
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        /// <summary>
        /// 初始化坐标、数组、热区
        /// </summary>
        private void InitData()
        {
            pDrawPoint = new PointF[20];

            rects = new RectangleF[20];

            Img = new Image[10];

            buttonIsDown = new bool[10];

            Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            rects[0] = new RectangleF(0, 100, 798, 450);

            //MoveScreen
            for (int i = 0; i < rects.Length; i++)
            {
                rects[i].X = (rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                rects[i].Y = (rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                rects[i].Width *= FormatStyle.Scale;
                rects[i].Height *= FormatStyle.Scale;
            }
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::

        #region:::::::::::::::::::::::::::传值部分::::::::::::::::::::::::::::::::
        /// <summary>
        /// 坐标集
        /// </summary>
        internal PointF[] pDrawPoint;

        /// <summary>
        /// 矩形框集
        /// </summary>
        internal RectangleF[] rects;

        /// <summary>
        /// 图片集
        /// </summary>
        internal Image[] Img;

        /// <summary>
        /// 键是否按下
        /// </summary>
        internal bool[] buttonIsDown;

        /// <summary>
        /// 按键列表
        /// </summary>
        internal List<Region> Rect;
        #endregion#
        #endregion
    }

}
