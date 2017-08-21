using System;
using System.Collections.Generic;
using System.Drawing;
using Engine.TCMS.HXD3D.Fault;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.Title;
using Engine.TCMS.HXD3D.底层共用;
using MMI.Facility.Interface.Attribute;

namespace Engine.TCMS.HXD3D.主菜单
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class MainScreen : HXD3BaseClass
    {
        // Fields
        private bool[] m_BValue;

        public static TheCountdown Countdown = new TheCountdown(60);
        private float[] m_FValue;
        private bool m_HasPlan;
        private List<Image> m_ImgsList;
        private readonly int[] m_PowerAndBrake = {100, 100};
        private float m_QianYZhiD;
        private List<RectangleF> m_RectsList;
        private readonly int[] m_Shuzi = {0x88b8, 500, 100, 100, 150, 800, 800, 800, 800};
        private readonly NeedChangeLength[] m_TheChangeRectsArr = new NeedChangeLength[9];

        // Methods
        private void Draw(Graphics g)
        {
            PaintGroundImage(g);
            PaintState(g);
            PaintRectangles(g);
        }

        public override string GetInfo()
        {
            return "主屏视图";
        }

        private SolidBrush GetRectangleBrush(int numb, float valueNow)
        {
            switch (numb)
            {
                case 0:
                    if (((valueNow <= 0f) || (valueNow >= 17500f)) && ((valueNow < 31000f) || (valueNow > 35000f)))
                    {
                        if (((valueNow >= 17500f) && (valueNow < 19000f)) ||
                            ((valueNow >= 30000f) && (valueNow < 31000f)))
                        {
                            return SolidBrushsItems.BlueBrush1;
                        }

                        if ((valueNow < 19000f) || (valueNow >= 30000f))
                        {
                            break;
                        }

                        return SolidBrushsItems.GreenBrush1;
                    }

                    return SolidBrushsItems.RedBrush1;

                case 1:
                    return SolidBrushsItems.YellowBrush1;

                case 3:
                    return SolidBrushsItems.YellowBrush1;

                case 4:
                    return SolidBrushsItems.BlueBrush1;

                case 5:
                    if (((valueNow <= 0f) || (valueNow >= 520f)) && ((valueNow <= 620f) || (valueNow >= 800f)))
                    {
                        if ((valueNow < 520f) || (valueNow > 620f))
                        {
                            break;
                        }

                        return SolidBrushsItems.YellowBrush1;
                    }

                    return SolidBrushsItems.RedBrush1;

                case 6:
                    if (((valueNow <= 0f) || (valueNow >= 520f)) && ((valueNow <= 620f) || (valueNow >= 800f)))
                    {
                        if ((valueNow < 520f) || (valueNow > 620f))
                        {
                            break;
                        }

                        return SolidBrushsItems.YellowBrush1;
                    }

                    return SolidBrushsItems.RedBrush1;

                case 7:
                    if (((valueNow <= 0f) || (valueNow >= 520f)) && ((valueNow <= 620f) || (valueNow >= 800f)))
                    {
                        if ((valueNow < 520f) || (valueNow > 620f))
                        {
                            break;
                        }

                        return SolidBrushsItems.YellowBrush1;
                    }

                    return SolidBrushsItems.RedBrush1;

                case 8:
                    if (((valueNow <= 0f) || (valueNow >= 520f)) && ((valueNow <= 620f) || (valueNow >= 800f)))
                    {
                        if ((valueNow >= 520f) && (valueNow <= 620f))
                        {
                            return SolidBrushsItems.YellowBrush1;
                        }

                        break;
                    }

                    return SolidBrushsItems.RedBrush1;
            }

            return SolidBrushsItems.BlackBrush;
        }

        private void GetValue()
        {
            int num;
            for (num = 0; num < m_BValue.Length; num++)
            {
                m_BValue[num] = BoolList[UIObj.InBoolList[num]];
            }
            for (num = 0; num < m_FValue.Length; num++)
            {
                m_FValue[num] = FloatList[UIObj.InFloatList[num]];
            }
        }

        protected override  void Initalize()
        {
            InitData();
        }

        private void InitData()
        {
            int num;
            m_BValue = new bool[UIObj.InBoolList.Count];
            m_FValue = new float[UIObj.InFloatList.Count];
            m_ImgsList = new List<Image>();
            for (num = 0; num < UIObj.ParaList.Count; num++)
            {
                m_ImgsList.Add(Image.FromFile(RecPath + @"\" + UIObj.ParaList[num]));
            }

            
            {
                
            }
            if (Coordinate.RectangleFLists(ViewIDName.MainScreen, ref m_RectsList))
            {
                for (num = 0; num < 2; num++)
                {
                    m_TheChangeRectsArr[num] = new NeedChangeLength(m_RectsList[8 + num], m_Shuzi[num],
                        RectRiseDirection.上, false);
                }
                for (num = 4; num < 9; num++)
                {
                    m_TheChangeRectsArr[num] = new NeedChangeLength(m_RectsList[7 + num], m_Shuzi[num],
                        RectRiseDirection.上, false);
                }
                for (num = 0; num < 2; num++)
                {
                    m_TheChangeRectsArr[2 + num] = new NeedChangeLength(m_RectsList[10], m_PowerAndBrake[num],
                        RectRiseDirection.上, false);
                }

                m_HasPlan = true;
            }
        }

        public override void paint(Graphics g)
        {
            GetValue();
            Draw(g);
        }

        private void PaintGroundImage(Graphics g)
        {
            int num;
            g.DrawImage(
                ObjectService.GetObject<TopTitleScreen>(ProjectName)[0].MainModel ? m_ImgsList[0x13] : m_ImgsList[0],
                m_RectsList[0x17]);
            for (num = 0; num < 0x17; num++)
            {
                g.DrawRectangle(new Pen(Color.White), Rectangle.Round(m_RectsList[num]));
            }
            for (num = 0x18; num < 0x19; num++)
            {
                g.DrawRectangle(new Pen(Color.White), Rectangle.Round(m_RectsList[num]));
            }
        }

        private void PaintRectangles(Graphics g)
        {
            int num;
            for (num = 0; num < 2; num++)
            {
                m_TheChangeRectsArr[num].DrawRectangle(g, ref m_FValue[3 + num],
                    GetRectangleBrush(num, m_FValue[3 + num]));
            }

            if (m_BValue[12])
            {
                m_QianYZhiD = m_FValue[5] > m_PowerAndBrake[0] ? m_PowerAndBrake[0] : m_FValue[5];
                m_TheChangeRectsArr[2].DrawRectangle(g, ref m_QianYZhiD, SolidBrushsItems.BlueBrush1);
            }
            else if (m_BValue[13])
            {
                m_QianYZhiD = m_FValue[5] > m_PowerAndBrake[1] ? m_PowerAndBrake[1] : m_FValue[5];
                m_TheChangeRectsArr[3].DrawRectangle(g, ref m_QianYZhiD, SolidBrushsItems.RedBrush1);
            }
            for (num = 4; num < 9; num++)
            {
                m_TheChangeRectsArr[num].DrawRectangle(g, ref m_FValue[2 + num],
                    GetRectangleBrush(num, m_FValue[2 + num]));
            }
        }

        private void PaintState(Graphics g)
        {
            g.DrawString(Convert.ToInt32(m_FValue[3]/1000).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 8f, true),
                SolidBrushsItems.YellowBrush1, m_RectsList[0], FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(Convert.ToInt32(m_FValue[4]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 8f, true),
                SolidBrushsItems.YellowBrush1, m_RectsList[1], FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(Convert.ToInt32(m_FValue[5]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 8f, true),
                SolidBrushsItems.YellowBrush1, m_RectsList[2], FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(Convert.ToInt32(m_FValue[6]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 8f, true),
                SolidBrushsItems.YellowBrush1, m_RectsList[3], FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(Convert.ToInt32(m_FValue[7]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 8f, true),
                SolidBrushsItems.YellowBrush1, m_RectsList[4], FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(Convert.ToInt32(m_FValue[8]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 8f, true),
                SolidBrushsItems.YellowBrush1, m_RectsList[5], FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(Convert.ToInt32(m_FValue[9]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 8f, true),
                SolidBrushsItems.YellowBrush1, m_RectsList[6], FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(Convert.ToInt32(m_FValue[10]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 8f, true),
                SolidBrushsItems.YellowBrush1, m_RectsList[7], FontsItems.TheAlignment(FontRelated.居中));
            var ef = m_RectsList[0x10];
            g.FillRectangle(new SolidBrush(Color.Blue), ef.X, ef.Y, ef.Width, ef.Height);
            g.DrawString(TopTitleScreen.TrainID, FontsItems.Fonts_Regular(FontName.宋体, 10f, true),
                SolidBrushsItems.WhiteBrush, m_RectsList[0x10], FontsItems.TheAlignment(FontRelated.居中));
            if (FaultReceive.MsgInf.TestMsgList.Count > 0)
            {
                var gHxdd = FaultReceive.MsgInf.TestMsgList[0];
                if (gHxdd.TrainID == 1)
                {
                    g.FillRectangle(new SolidBrush(Color.Red), m_RectsList[0x12]);
                }
                if (gHxdd.TrainID == 2)
                {
                    g.FillRectangle(new SolidBrush(Color.Red), m_RectsList[0x13]);
                }
                ef = m_RectsList[0x11];
                g.FillRectangle(new SolidBrush(Color.Red), ef.X, ef.Y, ef.Width, ef.Height);
            }
            g.DrawString("故障", FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.WhiteBrush,
                m_RectsList[0x11], FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString("1", FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.WhiteBrush,
                m_RectsList[0x12], FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString("2", FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.WhiteBrush,
                m_RectsList[0x13], FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(Convert.ToInt32(m_FValue[0]) + "km/h", FontsItems.Fonts_Regular(FontName.宋体, 14f, true),
                SolidBrushsItems.YellowBrush1, m_RectsList[20], FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(Convert.ToInt32(m_FValue[1]) == 0 ? "-" : Convert.ToInt32(m_FValue[1]) + "km/h",
                FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.YellowBrush1, m_RectsList[0x15],
                FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(m_FValue[2].ToString("0.0"), FontsItems.Fonts_Regular(FontName.宋体, 12f, true),
                SolidBrushsItems.YellowBrush1, m_RectsList[0x16], FontsItems.TheAlignment(FontRelated.居中));
            for (var i = 0; i < 2; i++)
            {
                if (m_BValue[12 + i])
                {
                    g.DrawImage(m_ImgsList[0x11 + i], m_RectsList[0x1a]);
                }
            }
        }
    }
}