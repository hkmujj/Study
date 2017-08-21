using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using Urban.QingDao3Line.MMI.Resource;
using Urban.QingDao3Line.MMI.底层共用;

namespace Urban.QingDao3Line.MMI.主驾驶界面
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class MainDrivingScreen : QingDaoBaseClass
    {
        //2
        public override string GetInfo()
        {
            return "主驾驶界面";
        }

        //6
        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }

        public override bool mouseDown(Point point)
        {
            int index = 0;
            for (; index < 7; index++)
            {
                if (m_BtnArea[index].IsVisible(point))
                {
                    m_BtnIsDown[index] = !m_BtnIsDown[index];
                    break;
                }
            }
            return true;
        }


        public override void paint(Graphics g)
        {
            GetValue();
            PaintState(g);
        }

        public void GetValue()
        {
            for (int i = 0; i < m_BValue.Length; i++)
            {
                m_BValue[i] = BoolList[UIObj.InBoolList[i]];
            }
            for (int i = 0; i < m_FValue.Length; i++)
            {
                m_FValue[i] = FloatList[UIObj.InFloatList[i]];
            }
        }

        private void PaintState(Graphics e)
        {
            #region::::::::::::::列车:::::::::::::::::::::::::


            #endregion
            //Load of the car
            e.DrawLines(Pens.Black, LoadOfTheCar);
            for (int i = 0; i < 6; i++)
            {
                e.FillRectangle(Common.m_GreyWhite, m_RectsList[63 + i].X, m_RectsList[63 + i].Y - 1, m_RectsList[63 + i].Width, m_RectsList[63 + i].Height);
                e.DrawRectangle(Pens.Black, m_RectsList[63 + i].X, m_RectsList[63 + i].Y - 1, m_RectsList[63 + i].Width, m_RectsList[63 + i].Height);
            }

            #region:::辅助变流器1，2:::::::::::::::::


            //Traction/Brake Error
            e.DrawLines(Pens.Black, TractionAndBrack);
            for (int i = 0; i < 4; i++)
            {
                e.FillRectangle(Common.m_GreyWhite, m_RectsList[100 + i].X, m_RectsList[100 + i].Y, m_RectsList[100 + i].Width, m_RectsList[100 + i].Height);
                e.DrawRectangle(Pens.Black, m_RectsList[100 + i].X, m_RectsList[100 + i].Y, m_RectsList[100 + i].Width, m_RectsList[100 + i].Height);
            }
            //SB Pressure
            e.DrawLines(Pens.Black, SBPressure);
            #endregion

            #region ::::::::::旁路框::::::::::

            for (int i = 0; i < 4; i++)
            {
                e.FillRectangle(Common.m_GreyWhite, m_Rects[i]);
            }
            for (int i = 0; i < 4; i++)
            {
                e.DrawRectangle(Pens.Black, m_Rects[i].X, m_Rects[i].Y, m_Rects[i].Width, m_Rects[i].Height);
            }
            #endregion

            #region::::::车轮打滑::::::::::::::::
            if (m_BValue[6])
            {
                e.DrawImage(m_ImgsList[0], m_Rects[4]);
            }
            if (m_BValue[7])
            {
                e.DrawImage(m_ImgsList[1], m_Rects[5]);
            }
            #endregion

            #region::::::BCU信息丢失:::::::::::::
            if (m_BValue[8])
            {
                e.FillRectangle(new SolidBrush(Color.Red), m_Rects[6]);
                e.DrawRectangle(new Pen(Color.Black, 2), m_Rects[6].X, m_Rects[6].Y, m_Rects[6].Width, m_Rects[6].Height);
                e.DrawString(m_Txt[11], Common.m_Font10B, SolidBrushsItems.BlackBrush, m_Rects[7], FontItems.TheAlignment(FontRelated.居中));
                e.DrawString(m_Txt[12], Common.m_Font10B, SolidBrushsItems.BlackBrush, m_Rects[8], FontItems.TheAlignment(FontRelated.居中));
            }
            #endregion

            #region:::::RIOM未运行::::::::::
            if (m_BValue[9])
            {
                e.FillRectangle(new SolidBrush(Color.Red), m_Rects[9]);
                e.DrawRectangle(Common.m_BlackPen1, m_Rects[9].X, m_Rects[9].Y, m_Rects[9].Width, m_Rects[9].Height);
                e.DrawString(m_Txt[13], Common.m_Font10B, SolidBrushsItems.BlackBrush, m_Rects[9], FontItems.TheAlignment(FontRelated.居中));
            }
            #endregion

            //字
            for (int i = 0; i < 11; i++)
            {
                //车辆载荷、牵引/制动力、制动缸压力
                if (i < 3)
                    e.DrawString(m_Txt[i], Common.m_Font9B, SolidBrushsItems.BlackBrush, m_RectsList[139 + i], FontItems.TheAlignment(FontRelated.居中));
                //MPR1、MPR2
                if (i >= 3 && i < 5)
                    e.DrawString(m_Txt[i], FontItems.Fonts_Regular(FontName.Arial, 11, true), SolidBrushsItems.BlackBrush, m_RectsList[139 + i], FontItems.TheAlignment(FontRelated.居中));
                //牵引力需求、施加的牵引力等
                if (i >= 5 && i < 9)
                {
                    e.DrawString(m_Txt[i], Common.m_Font9B, Common.m_BlackBrush, m_RectsList[139 + i], Common.m_LeftCenterFormat);
                }
                //旁路
                if (i >= 9)
                    e.DrawString(m_Txt[i], Common.m_Font9B, SolidBrushsItems.BlackBrush, m_RectsList[139 + i], FontItems.TheAlignment(FontRelated.居中));
            }
            //旁路信息
            for (int i = 0; i < 12; i++)
            {
                e.DrawString(m_Str[i], FontItems.Fonts_Regular(FontName.Arial, 8, true), SolidBrushsItems.BlackBrush,
                    m_RectsList[150 + i], Common.m_MFormat);
                e.DrawString(m_Str[i], FontItems.Fonts_Regular(FontName.Arial, 8, true), SolidBrushsItems.BlackBrush,
                    m_RectsList[162 + i], FontItems.TheAlignment(FontRelated.居中));
            }
        }

        //public void DrawDoors(Graphics e)
        //{
        //    //车头
        //    if (m_BValue[0])
        //    {
        //        e.DrawImage(m_ImgsList[0], m_RectsList[0]);
        //        e.DrawImage(m_ImgsList[6], m_RectsList[1]);
        //    }
        //    else if (m_BValue[1])
        //    {
        //        e.DrawImage(m_ImgsList[1], m_RectsList[0]);
        //        e.DrawImage(m_ImgsList[7], m_RectsList[1]);
        //    }
        //    else if (m_BValue[2])
        //    {
        //        e.DrawImage(m_ImgsList[3], m_RectsList[0]);
        //        e.DrawImage(m_ImgsList[5], m_RectsList[1]);
        //    }
        //    else
        //    {
        //        e.DrawImage(m_ImgsList[2], m_RectsList[0]);
        //        e.DrawImage(m_ImgsList[4], m_RectsList[1]);
        //    }
        //    //驾驶室
        //    for (int i = 0; i < 2; i++)
        //    {
        //        e.FillRectangle(new SolidBrush(Color.Yellow), m_RectsList[2 + i]);
        //        e.DrawRectangle(new Pen(Color.Black), m_RectsList[2 + i].X, m_RectsList[2 + i].Y, m_RectsList[2 + i].Width,
        //            m_RectsList[2 + i].Height);
        //    }
        //    for (int i = 0; i < 4; i++)
        //    {
        //        if (m_BValue[6])
        //        {
        //            e.DrawImage(m_ImgsList[8], m_RectsList[4 + i]);
        //        }
        //        else if (m_BValue[7])
        //        {
        //            e.DrawImage(m_ImgsList[9], m_RectsList[4 + i]);
        //        }
        //        else
        //            e.DrawImage(m_ImgsList[10], m_RectsList[4 + i]);
        //    }
        //    //车厢及门
        //    for (int i = 0; i < 6; i++)
        //    {
        //        e.FillRectangle(new SolidBrush(Color.Yellow), m_RectsList[8 + i]);
        //        e.DrawRectangle(new Pen(Color.Black), m_RectsList[8 + i].X, m_RectsList[8 + i].Y, m_RectsList[8 + i].Width,
        //            m_RectsList[8 + i].Height);
        //    }

        //    for (int i = 0; i < 48; i++)
        //    {
        //        if (m_BValue[8])
        //            e.DrawImage(m_ImgsList[11], m_RectsList[14 + i]);
        //        else if (m_BValue[9])
        //            e.DrawImage(m_ImgsList[12], m_RectsList[14 + i]);
        //        else if (m_BValue[10])
        //            e.DrawImage(m_ImgsList[13], m_RectsList[14 + i]);
        //        else if (m_BValue[11])
        //            e.DrawImage(m_ImgsList[14], m_RectsList[14 + i]);
        //        else if (m_BValue[12])
        //            e.DrawImage(m_ImgsList[15], m_RectsList[14 + i]);
        //        else if (m_BValue[13])
        //            e.DrawImage(m_ImgsList[16], m_RectsList[14 + i]);
        //        else  
        //            //默认车门关闭
        //            e.DrawImage(m_ImgsList[17], m_RectsList[14 + i]);
        //    }
        //}

        public void InitData()
        {
            m_BValue = new bool[UIObj.InBoolList.Count];
            for (int i = 0; i < m_BValue.Length; i++)
            {
                m_BValue[i] = BoolList[UIObj.InBoolList[i]];
            }
            m_FValue = new float[UIObj.InFloatList.Count];

            m_ImgsList = new List<Image>();
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_ImgsList.Add(Image.FromFile(Path.Combine(RecPath, UIObj.ParaList[i])));
            }
            if (!Coordinate.ClassIsInited) Coordinate.InitData();
            if (Coordinate.RectangleFLists(ViewIDName.MainDrivingScreen, ref m_RectsList))
            {
                for (int i = 0; i < 7; i++)
                {
                    m_BtnArea.Add(new Region(m_RectsList[175 + i]));
                }
            }
            //旁路框 /0-3
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(0 + 725 * i, 283, 74, 260));
            }
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(0 + 725 * i, 260, 74, 23));
            }
            //车轮打滑检测框  /4-5
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(240 + 280 * i, 485, 50, 50));
            }
            //BCU信息丢失框  /6-8
            m_Rects.Add(new Rectangle(300, 475, 255, 60));
            m_Rects.Add(new Rectangle(300, 485, 255, 25));
            m_Rects.Add(new Rectangle(300, 510, 255, 25));
            //RIOM未运行框   /9-10
            m_Rects.Add(new Rectangle(110, 477, 84, 30));
            m_Rects.Add(new Rectangle(110, 470, 84, 30));

            TractionAndBrack = new PointF[] 
            {
                new PointF(m_RectsList[140].X,m_RectsList[99].Y),
                new PointF(m_RectsList[99].X,m_RectsList[99].Y),
                new PointF(m_RectsList[99].X,m_RectsList[99].Y+m_RectsList [99].Height),
                new PointF(m_RectsList[99].X+m_RectsList[99].Width,m_RectsList[99].Y+m_RectsList [99].Height),
                new PointF(m_RectsList[99].X+m_RectsList[99].Width,m_RectsList[99].Y),
                new PointF(m_RectsList[140].X+m_RectsList[140].Width,m_RectsList[99].Y)
            };
            SBPressure = new PointF[]
            {
                new PointF(m_RectsList[141].X,m_RectsList[104].Y),
                new PointF(m_RectsList[104].X,m_RectsList[104].Y),
                new PointF(m_RectsList[104].X,m_RectsList[104].Y+m_RectsList [104].Height),
                new PointF(m_RectsList[104].X+m_RectsList[104].Width,m_RectsList[104].Y+m_RectsList [104].Height),
                new PointF(m_RectsList[104].X+m_RectsList[104].Width,m_RectsList[104].Y),
                new PointF(m_RectsList[141].X+m_RectsList[141].Width ,m_RectsList[104].Y)
            };
            LoadOfTheCar = new PointF[]
            {
                new PointF(m_RectsList[139].X,m_RectsList[189].Y),
                new PointF(m_RectsList[189].X,m_RectsList[189].Y),
                new PointF(m_RectsList[189].X,m_RectsList[189].Y+m_RectsList [189].Height),
                new PointF(m_RectsList[189].X+m_RectsList[189].Width,m_RectsList[189].Y+m_RectsList [189].Height),
                new PointF(m_RectsList[189].X+m_RectsList[189].Width,m_RectsList[189].Y),
                new PointF(m_RectsList[139].X+m_RectsList[139].Width ,m_RectsList[189].Y)
            };

        }

        private bool[] m_BValue;
        private float[] m_FValue;
        private List<Image> m_ImgsList;
        private List<RectangleF> m_RectsList;
        private List<RectangleF> m_Rects = new List<RectangleF>();
        private readonly List<Region> m_BtnArea = new List<Region>();
        private readonly bool[] m_BtnIsDown = new bool[7];

        private readonly string[] m_Txt =
        {
            ResourceFacade.DrivingViewResourceLoadOfTheCar, ResourceFacade.DrivingViewResourceTractionBrakeEffort,
            ResourceFacade.DrivingViewResourceSBPressure,ResourceFacade.DrivingViewResourceMPR1, 
            ResourceFacade.DrivingViewResourceMPR2,ResourceFacade.DrivingViewResourceTractionEffortAchieved,
            ResourceFacade.DrivingViewResourceTractionEffortDemand,ResourceFacade.DrivingViewResourceBrakeEffortDemand, 
            ResourceFacade.DrivingViewResourceBrakeEffortAchieved,ResourceFacade.DrivingViewResourceByPass, 
            ResourceFacade.DrivingViewResourceByPass,ResourceFacade.DrivingViewResourceBCULosing,
            ResourceFacade.DrivingViewResourcePleaseBack,ResourceFacade.DrivingViewResourceRIOM
        };

        private readonly string[] m_Str =
        {
            ResourceFacade.DrivingViewResourceSKTDB, ResourceFacade.DrivingViewResourceSKCDB, ResourceFacade.DrivingViewResourceSKTIB, ResourceFacade.DrivingViewResourceSKTPB,
            ResourceFacade.DrivingViewResourceSKNRB, ResourceFacade.DrivingViewResourceSKZVB, ResourceFacade.DrivingViewResourceSKESS, ResourceFacade.DrivingViewResourceSKEBB,
            ResourceFacade.DrivingViewResourceSKMPB, ResourceFacade.DrivingViewResourceSKPRB, ResourceFacade.DrivingViewResourceSKDMB, ResourceFacade.DrivingViewResourceSKATP,
            
        };

        private PointF[] TractionAndBrack;
        private PointF[] SBPressure;
        private PointF[] LoadOfTheCar;
    }
}



