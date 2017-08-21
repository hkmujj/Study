using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.QingDao3Line.MMI.Resource;

namespace Urban.QingDao3Line.MMI.底层共用.Title
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class TitleScreen : QingDaoBaseClass
    {
        private bool[] m_BValue;
        private float[] m_FValue;
        private List<Image> m_ImgsList;
        private List<RectangleF> m_RectsList;
        public static bool[] m_BtnIsDown = new bool[11];
        private List<Region> m_region=new List<Region>();
        private bool[] m_IsFault = new bool[2];
        private readonly string[] m_Str = 
        {
            ResourceFacade.DrivingViewResourceNextStation, ResourceFacade.DrivingViewResourceFinalStation,
            ResourceFacade.DrivingViewResourceBreaking,ResourceFacade .DrivingViewResourceWashing,
            ResourceFacade .DrivingViewResourceLimittingForward,ResourceFacade.DrivingViewResourceManual,
            ResourceFacade.DrivingViewResourceATO,ResourceFacade .DrivingViewResourceLimittingBack
         };

        private readonly string m_Error = ResourceFacade.DrivingViewResourceErrorInfo;
            
        private FlashTimer m_FlashTimer;

        public static int m_CurrentView;

        /// <summary>
        /// 车站信息
        /// </summary>
        private readonly Dictionary<int, string> m_StationDic = new Dictionary<int, string>();

        //2
        public override string GetInfo()
        {
            return "标题视图";
        }

        //6
        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            ReadConfigcs read = new ReadConfigcs("车站信息", m_StationDic);
            read.ReaFile(Path.Combine(RecPath, "..\\Config"));
            return true;
        }

        public override void paint(Graphics g)
        {
            GetValue();
            Draw(g);
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            m_CurrentView = nParaB;
            base.setRunValue(nParaA, nParaB, nParaC);
        }

        private void Draw(Graphics e)
        {
            PaintGroundImage(e);
            PaintState(e);
            PaintButtonState(e);

        }

        private void GetValue()
        {
            for (int i = 0; i < m_BValue.Length; i++)
            {
                m_BValue[i] = BoolList[this.UIObj.InBoolList[i]];
            }
            for (int i = 0; i < m_FValue.Length; i++)
            {
                m_FValue[i] = FloatList[this.UIObj.InFloatList[i]];
            }
        }

        private void PaintState(Graphics e)
        {
            e.DrawRectangle(new Pen(Color.Black), m_RectsList[0].X, m_RectsList[0].Y, m_RectsList[0].Width,
                m_RectsList[0].Height);
            for (int i = 0; i < 2; i++)
            {
                e.DrawRectangle(new Pen(Color.Black), m_RectsList[2 + i].X, m_RectsList[2 + i].Y,
                    m_RectsList[2 + i].Width, m_RectsList[2 + i].Height);
            }
            //IOS
            e.DrawString("0 IOS" + Convert.ToInt32(m_FValue[0]).ToString("#"),
                FontItems.Fonts_Regular(FontName.Arial, 12, true), SolidBrushsItems.BlackBrush,
                m_RectsList[2], FontItems.TheAlignment(FontRelated.居中));
            //列车模式
            for (int i = 0; i < 6;i++ )
            {
                if (m_BValue[9 + i])
                {
                    e.DrawString(m_Str[2+i], FontItems.Fonts_Regular(FontName.宋体, 12, true),
                        SolidBrushsItems.BlackBrush,
                        m_RectsList[3], FontItems.TheAlignment(FontRelated.居中));
                }
            }
            e.DrawRectangle(new Pen(Color.Black), m_RectsList[8].X, m_RectsList[8].Y, m_RectsList[8].Width,
                m_RectsList[8].Height);
            //最大速度
            e.DrawString(Convert.ToInt32(m_FValue[1]).ToString("0"),
                FontItems.Fonts_Regular(FontName.Arial, 8, true), SolidBrushsItems.RedBrush,
                m_RectsList[9], FontItems.TheAlignment(FontRelated.居中));
            e.DrawString(ResourceFacade.DrivingViewResourcekmPerh,
                FontItems.Fonts_Regular(FontName.Arial, 8, true), SolidBrushsItems.RedBrush,
                m_RectsList[9], Common.m_RightCenterFormat);
            //实际速度
            e.DrawString(Convert.ToInt32(m_FValue[2]).ToString("0.0") ,
                FontItems.Fonts_Regular(FontName.Arial, 22, true), SolidBrushsItems.BlackBrush,
                m_RectsList[10], Common.m_LeftFormat);
            e.DrawString(ResourceFacade.DrivingViewResourcekmPerh,
                FontItems.Fonts_Regular(FontName.Arial, 8, true), SolidBrushsItems.BlackBrush,
                m_RectsList[10], Common.m_RightFormat);
            //日期 时间
            for (int i = 0; i < 2; i++)
            {
                e.DrawRectangle(new Pen(Color.Black), m_RectsList[11 + i].X, m_RectsList[11 + i].Y,
                  m_RectsList[11 + i].Width, m_RectsList[11 + i].Height);
            }
            e.DrawString(DateTime.Now.ToString(ResourceFacade.DrivingViewResourceDateFormat), FontItems.Fonts_Regular(FontName.Arial, 12, false),
                SolidBrushsItems.BlackBrush,
                m_RectsList[11], FontItems.TheAlignment(FontRelated.居中));
            e.DrawString(DateTime.Now.ToString(ResourceFacade.DrivingViewResourceTimeFormat), FontItems.Fonts_Regular(FontName.Arial, 12, true),
                SolidBrushsItems.BlackBrush,
                m_RectsList[12], FontItems.TheAlignment(FontRelated.居中));
            //下一站  终点站
            for (int i = 0; i < 2; i++)
            {
                e.DrawRectangle(new Pen(Color.Black), m_RectsList[13 + i].X, m_RectsList[13 + i].Y,
                    m_RectsList[13 + i].Width, m_RectsList[13 + i].Height);
                e.DrawString(m_Str[i], FontItems.Fonts_Regular(FontName.宋体, 10, true), SolidBrushsItems.BlackBrush,
                    m_RectsList[15 + i], FontItems.TheAlignment(FontRelated.靠左));
                if (m_StationDic.ContainsKey(Convert.ToInt32(m_FValue[3 + i])))
                {
                    e.DrawString(m_StationDic[Convert.ToInt32(m_FValue[3 + i])],
                        FontItems.Fonts_Regular(FontName.宋体, 10, true), SolidBrushsItems.BlackBrush,
                        m_RectsList[17 + i], FontItems.TheAlignment(FontRelated.居中));
                }
            }
            //电压
            e.DrawRectangle(new Pen(Color.Black), m_RectsList[19].X, m_RectsList[19].Y, m_RectsList[19].Width,
                m_RectsList[19].Height);
            e.DrawString(Convert.ToInt32(m_FValue[5]).ToString("#") + "V",
                FontItems.Fonts_Regular(FontName.Arial, 14, true), SolidBrushsItems.BlackBrush,
                m_RectsList[19], FontItems.TheAlignment(FontRelated.居中));
            if (m_BValue[0])
            {
                e.FillRectangle(new SolidBrush(Color.Red), m_RectsList[20]);
                e.DrawString(m_Error, FontItems.Fonts_Regular(FontName.宋体, 14, true), SolidBrushsItems.BlackBrush,
                    m_RectsList[20], FontItems.TheAlignment(FontRelated.居中));
            }
        }

        private void PaintGroundImage(Graphics e)
        {
            e.FillRectangle(SolidBrushsItems.BackGrounBrush, m_RectsList[21]);
        }

        private void PaintButtonState(Graphics e)
        {
            if (m_FlashTimer.IsNeedFlash())
            {
                for (int i = 0; i < 4; i++)
                {
                    //4个闪烁图标
                    if (m_BValue[1 + i])
                    {
                        e.DrawImage(m_ImgsList[4 + i], m_RectsList[4 + i]); 
                    }
                    if (m_BValue[5 + i])
                    {
                        e.DrawImage(m_ImgsList[i], m_RectsList[1]);
                    }
                }
            }

        }

        public override bool mouseDown(Point point)
        {
            for (int index = 0; index < 5; index++)
            {
                if (m_region[index].IsVisible(point))
                {
                    switch (index)
                    {
                        case 0:
                            if (m_BValue[5]||m_BValue[6]||m_BValue[7]||m_BValue[8])
                            {
                                append_postCmd(CmdType.ChangePage, 55, 0, 0);
                            }
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            if (m_BValue[3])
                            {
                                append_postCmd(CmdType.ChangePage, 56, 0, 0);
                                break;
                            }
                            break;
                        case 4:
                            break;
                        default:
                            break;
                    }
                    break;
                }
            }
            return base.mouseDown(point);
        }

        public override bool mouseUp(Point point)
        {
            return base.mouseUp(point);
        }

        /// <summary>
        /// 把所有btnisDown的值制为false
        /// </summary>

        private void InitData()
        {
            m_BValue = new bool[UIObj.InBoolList.Count];
            m_FValue = new float[UIObj.InFloatList.Count];

            m_ImgsList = new List<Image>();
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_ImgsList.Add(Image.FromFile(Path.Combine(RecPath, UIObj.ParaList[i])));
            }
            if (!Coordinate.ClassIsInited) Coordinate.InitData();
            if (Coordinate.RectangleFLists(ViewIDName.TitleScreen, ref m_RectsList))
            {
                m_FlashTimer = new FlashTimer(1);
            }
            m_region.Add(new Region(m_RectsList[1]));
            for (int i = 0;i<4; i++)
            {
                m_region.Add(new Region(m_RectsList[i + 4]));
            }
        }
    }
}
