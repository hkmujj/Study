using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using Motor.HMI.CRH5G.Resource;
using Motor.HMI.CRH5G.Resource.Images;
using Motor.HMI.CRH5G.底层共用;

namespace Motor.HMI.CRH5G.仪表
{
    [GksDataType(DataType.isMMIObjectClass)]
    class InstrumentScreen_1 : CRH5GBase
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        //2
        public override string GetInfo()
        {
            return "仪表视图1";
        }


        //6
        public override bool Initalize()
        {
            return Init();
        }

        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void Paint(Graphics g)
        {
            GetValue();
            DrawOn(g);
        }

        int m_FValueIndex;
        private void GetValue()
        {

            m_DesPress = GetInFloatValue(InFloatKeys.InF线电压网压单位KV);
        }

        private void DrawOn(Graphics e)
        {
            e.DrawImage(TitleScreen.ChangeLength ? list[0] : list[6], m_RectsList[1]);

            int i;
            for (i = 0; i < 2; i++)
            {
                e.DrawImage(list[i + 1], m_RectsList[i + 2]);
            }

            for (i = 0; i < 2; i++)
            {
                m_ThePointer[i + (TitleScreen.ChangeLength ? 0 : 4)].PaintPointerNormal(e,i==0?GetInFloatValue(InFloatKeys.InF车辆作用力合力单位KN红三角):GetInFloatValue(InFloatKeys.InF车辆作用力合力单位KN));
            }
            e.DrawRectangle(new Pen(Color.White, 1), Rectangle.Round(m_RectsList[5]));
            e.DrawString("速度表",
                FontsItems.Fonts_Regular(FontName.宋体, 16f, false),
                SolidBrushsItems.YellowBrush1,
                m_RectsList[4],
                FontsItems.TheAlignment(FontRelated.居中));
            e.DrawString(Convert.ToInt32(GetInFloatValue(InFloatKeys.InF车辆自身速度实际运行速度单位kmh)) + " Km/h",
                FontsItems.Fonts_Regular(FontName.宋体, 18f, true),
                SolidBrushsItems.WhiteBrush,
                m_RectsList[6],
                FontsItems.TheAlignment(FontRelated.居中));

            //线电压、线电流
            m_ThePointer[2].PaintPointerNormal(e, MCurrentShowPress);
            m_ThePointer[3].PaintPointerNormal(e, GetInFloatValue(InFloatKeys.InF线电流单位A));
        }

        #endregion

        private Image[] list;
        private bool Init()
        {
            m_MCurrentShowPress = 0;

            m_RectsList = new List<RectangleF>();
            m_RectsList = Coordinate.RectangleFLists(ViewIDName.InstrumentScreen1);
            list = new Image[]
            {
                ImagesResouce.作用力2,
                ImagesResouce.线电压,
                ImagesResouce.线电流,
                ImagesResouce.红三角,
                ImagesResouce.指针_2,
                ImagesResouce.指针_3,
                ImagesResouce.作用力

            };
            m_ThePointer = new SpeedPointer[6];
            var tmp = ScreenIdentification == ScreenIdentification.ScreenTs ? 320f : 640f;
            for (int i = 0; i < 2; i++)
            {
                m_ThePointer[i] = new SpeedPointer(135f, -135f,
                                                    90f,
                                                    tmp, -tmp,
                                                    m_RectsList[i],
                                                    new PointF(m_RectsList[i].X + (i == 0 ? 215 : 200), m_RectsList[i].Y + (i == 0 ? 215 : 200)),
                                                    list[3 + i]);
                m_ThePointer[2 + i] = new SpeedPointer(270f, -45f,
                                                    -45,
                                                    i == 0 ? 32000f : 1000f, 0f,
                                                    m_RectsList[2 + i],
                                                    new PointF(m_RectsList[2 + i].X + 122, m_RectsList[2 + i].Y + 122),
                                                    list[5]);
                m_ThePointer[4 + i] = new SpeedPointer(135f, -135f,
                                                    90f,
                                                    320f, -320f,
                                                    m_RectsList[i],
                                                    new PointF(m_RectsList[i].X + (i == 0 ? 215 : 200), m_RectsList[i].Y + (i == 0 ? 215 : 200)),
                                                    list[3 + i]);
            }
            return true;
        }

        private SpeedPointer[] m_ThePointer;

        private List<RectangleF> m_RectsList;
        private float m_DesPress;

        private float m_MCurrentShowPress;

        public float MCurrentShowPress
        {
            get
            {
                m_MCurrentShowPress = m_MCurrentShowPress <= m_DesPress ?
                Math.Min(m_MCurrentShowPress + PressStep, m_DesPress) :
                Math.Max(m_MCurrentShowPress - PressStep, -m_DesPress);
                return m_MCurrentShowPress;
            }
        }

        private int PressStep
        {
            get
            {
                // return this.ScreenIdentification == ScreenIdentification.ScreenTS ? 1000 : 3;
                return 1000;
            }
        }
    }

    [GksDataType(DataType.isMMIObjectClass)]
    class InstrumentScreen_2 : CRH5GBase
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        //2
        public override string GetInfo()
        {
            return "仪表视图2";
        }

        public InstrumentScreen_2()
        {

        }
        public override bool Initalize()
        {
            return Init();
        }

        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void Paint(Graphics g)
        {
            GetValue();
            DrawOn(g);
        }

   
        private void GetValue()
        {
           
        }

        private void DrawOn(Graphics e)
        {
            e.DrawImage(ImagesResouce.速度表盘, m_RectsList[1]);
            e.DrawImage(ImagesResouce.制动主管压力, m_RectsList[2]);
            e.DrawImage(ImagesResouce.主风管压力,m_RectsList[3]);
            m_ThePointer[0].PaintPointerNormal(e, GetInFloatValue(InFloatKeys.InF车辆自身速度实际运行速度单位kmh));
            m_ThePointer[1].PaintPointerNormal(e, GetInFloatValue(InFloatKeys.InF制动管压力单位BAR));
            m_ThePointer[2].PaintPointerNormal(e, GetInFloatValue(InFloatKeys.InF总风管压力单位BAR));
        }
        #endregion

        private bool Init()
        {
            m_RectsList = new List<RectangleF>();
            m_RectsList = Coordinate.RectangleFLists(ViewIDName.InstrumentScreen2);

            m_ThePointer = new SpeedPointer[3];
            m_ThePointer[0] = new SpeedPointer(320f, -70f,
                                                -70f,
                                                300f, 0f,
                                                m_RectsList[1],
                                                new PointF(m_RectsList[1].X + 210, m_RectsList[1].Y + 210),
                                                ImagesResouce.指针_1);
            m_ThePointer[1] = new SpeedPointer(270f, -45f,
                                                    -45f,
                                                  1000f, 0f,
                                                    m_RectsList[2],
                                                    new PointF(m_RectsList[2].X + 122, m_RectsList[2].Y + 122),
                                                    ImagesResouce.指针_3);
            m_ThePointer[2] = new SpeedPointer(270f, -45f,
                                                    -45f,
                                                  1000f, 0f,
                                                    m_RectsList[3],
                                                    new PointF(m_RectsList[3].X + 122, m_RectsList[3].Y + 122),
                                                    ImagesResouce.指针_3);
            return true;
        }

     
        private SpeedPointer[] m_ThePointer;

        private List<RectangleF> m_RectsList;

        public InstrumentScreen_2(int i)
        {
        }
    }
}
