using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using CommonUtil.Util;
using Engine.TCMS.HXD3D.Config;
using Engine.TCMS.HXD3D.Fault;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.底层共用;
using MMI.Facility.Interface.Attribute;

namespace Engine.TCMS.HXD3D.主菜单
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class Main : HXD3BaseClass
    {
        // Fields
        private readonly Image[] m_BaseImage = new Image[2];
        private bool[] m_BValue;
        private float[] m_FValue;
        private List<Image> m_ImgsList;
        private readonly int[] m_PowerAndBrake = {100, 100};
        private float m_QianYZhiD;
        private List<RectangleF> m_RectsList;
        private readonly int[] m_Shuzi = {0x9c40, 500, 150, 100};
        private readonly NeedChangeLength[] m_TheChangeRectsArr = new NeedChangeLength[5];
        private SolidBrush m_TitleBrush;
        // Methods
        private void ButtomTitleImg(Graphics g)
        {
            int num;
            if (m_BValue[13])
            {
                g.DrawImage(m_ImgsList[3], m_RectsList[8]);
            }
            for (num = 0; num < 3; num++)
            {
                if (m_BValue[14 + num])
                {
                    g.DrawImage(m_ImgsList[14 + num], m_RectsList[15]);
                }
            }
            for (num = 0; num < 3; num++)
            {
                if (m_BValue[0x11 + num])
                {
                    g.DrawImage(m_ImgsList[0x11 + num], m_RectsList[0x11]);
                }
            }
            for (num = 0; num < 2; num++)
            {
                if (m_BValue[20 + num])
                {
                    g.DrawImage(m_ImgsList[20 + num], m_RectsList[0x15]);
                }
            }
        }

        private void Draw(Graphics g)
        {
            PaintRectangles(g);
            PaintGroundImage(g);
            ButtomTitleImg(g);
            PaintState(g);
        }

        public override string GetInfo()
        {
            return "主菜单界面";
        }

        private SolidBrush GetRectangleBrush(int numb, float valueNow)
        {
            switch (numb)
            {
                case 0:
                    if (((valueNow <= 0f) || (valueNow >= 17500f)) && ((valueNow < 31000f) || (valueNow > 40000f)))
                    {
                        if (((valueNow >= 17500f) && (valueNow < 19000f)) ||
                            ((valueNow >= 40000f) && (valueNow < 31000f)))
                        {
                            return SolidBrushsItems.BlueBrush1;
                        }
                        if ((valueNow >= 19000f) && (valueNow < 40000f))
                        {
                            return SolidBrushsItems.GreenBrush1;
                        }

                        break;
                    }

                    return SolidBrushsItems.RedBrush1;

                case 1:
                    return SolidBrushsItems.YellowBrush1;

                case 2:
                    return SolidBrushsItems.YellowBrush1;

                case 3:
                    return SolidBrushsItems.BlueBrush1;
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

        protected override void Initalize()
        {
            GlobalParam.Instance.ProjectConfig =
                DataSerialization.DeserializeFromXmlFile<ProjectConfig>(Path.Combine(
                    AppConfig.AppPaths.ConfigDirectory, ProjectConfig.FileName));
            m_TitleBrush =
                new SolidBrush(GlobalParam.Instance.ProjectConfig.ProjectType == HXD3DProject.呼和浩特
                    ? Color.Red
                    : Color.Orange);
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

            m_BaseImage[0] = m_ImgsList[1];
            m_BaseImage[1] = m_ImgsList[2];


            if (Coordinate.RectangleFLists(ViewIDName.Main, ref m_RectsList))
            {
                for (num = 0; num < 3; num++)
                {
                    m_TheChangeRectsArr[num] = new NeedChangeLength(m_RectsList[4 + num], m_Shuzi[num],
                        RectRiseDirection.上, false);
                }
                for (num = 0; num < 2; num++)
                {
                    m_TheChangeRectsArr[3 + num] = new NeedChangeLength(m_RectsList[7], m_PowerAndBrake[num],
                        RectRiseDirection.上, false);
                }
            }
        }

        public override void paint(Graphics g)
        {
            GetValue();
            Draw(g);
            PaintRectangles(g);
        }

        private void PaintGroundImage(Graphics e)
        {
            e.DrawImage(m_ImgsList[0], m_RectsList[0x24]);
            for (int i = 0; i < 0x12; i++)
            {
                e.DrawRectangle(new Pen(Color.White), Rectangle.Round(m_RectsList[8 + i]));
            }
        }

        private void PaintRectangles(Graphics g)
        {
            for (int i = 0; i < 3; i++)
            {
                m_TheChangeRectsArr[i].DrawRectangle(g, ref m_FValue[1 + i], GetRectangleBrush(i, m_FValue[1 + i]));
            }

            if (m_BValue[0])
            {
                m_QianYZhiD = m_FValue[4] > m_PowerAndBrake[0] ? m_PowerAndBrake[0] : m_FValue[4];
                m_TheChangeRectsArr[3].DrawRectangle(g, ref m_QianYZhiD, SolidBrushsItems.BlueBrush1);
            }
            else if (m_BValue[1])
            {
                m_QianYZhiD = m_FValue[4] > m_PowerAndBrake[1] ? m_PowerAndBrake[1] : m_FValue[4];
                m_TheChangeRectsArr[4].DrawRectangle(g, ref m_QianYZhiD, SolidBrushsItems.RedBrush1);
            }
            g.DrawString("牵引", FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.BlueBrush1,
                new RectangleF(330, 392, 60, 20), FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString("/", FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.WhiteBrush,
                new RectangleF(350, 392, 60, 20), FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString("制动", FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.RedBrush1,
                new RectangleF(375, 392, 60, 20), FontsItems.TheAlignment(FontRelated.居中));
        }


        private void PaintState(Graphics g)
        {
            int num;
            g.DrawString(Convert.ToInt32(m_FValue[1]/1000).ToString(CultureInfo.InvariantCulture),
                FontsItems.Fonts_Regular(FontName.宋体, 8f, true), SolidBrushsItems.YellowBrush1, m_RectsList[0],
                FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(Convert.ToInt32(m_FValue[2]).ToString(CultureInfo.InvariantCulture),
                FontsItems.Fonts_Regular(FontName.宋体, 8f, true), SolidBrushsItems.YellowBrush1, m_RectsList[1],
                FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(Convert.ToInt32(m_FValue[3]).ToString(CultureInfo.InvariantCulture),
                FontsItems.Fonts_Regular(FontName.宋体, 8f, true), SolidBrushsItems.YellowBrush1, m_RectsList[2],
                FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(Convert.ToInt32(m_FValue[4]).ToString(CultureInfo.InvariantCulture),
                FontsItems.Fonts_Regular(FontName.宋体, 8f, true), SolidBrushsItems.YellowBrush1, m_RectsList[3],
                FontsItems.TheAlignment(FontRelated.居中));
            for (num = 0; num < 3; num++)
            {
                if (m_BValue[2 + num])
                {
                    g.DrawImage(m_ImgsList[3 + num], m_RectsList[0x25]);
                }
            }

            if (m_BValue[5])
            {
                if (m_BValue[0x1a] && m_BValue[0x1b])
                {
                    g.DrawImage(m_ImgsList[0x18], m_RectsList[0x27]);
                }
                else if (m_BValue[0x1a])
                {
                    g.DrawImage(m_ImgsList[0x19], m_RectsList[0x27]);
                }
                else if (m_BValue[0x1b])
                {
                    g.DrawImage(m_ImgsList[0x1a], m_RectsList[0x27]);
                }
                else
                {
                    g.DrawImage(m_ImgsList[6], m_RectsList[0x27]);
                }
            }
            else if (m_BValue[6])
            {
                g.DrawImage(m_ImgsList[0x17], m_RectsList[0x27]);
            }
            if (m_BValue[9])
            {
                g.DrawImage(m_ImgsList[0x13], m_RectsList[0x2c]);
            }
            else if (m_BValue[0x19])
            {
                g.DrawImage(m_ImgsList[0x12], m_RectsList[0x2c]);
            }
            else if (m_BValue[0x18])
            {
                g.DrawImage(m_ImgsList[0x11], m_RectsList[0x2c]);
            }
            if (m_BValue[7])
            {
                g.DrawImage(m_ImgsList[8], m_RectsList[0x29]);
            }
            else if (m_BValue[8])
            {
                g.DrawImage(m_ImgsList[9], m_RectsList[0x29]);
            }
            else if (m_BValue[9])
            {
                g.DrawImage(m_ImgsList[10], m_RectsList[0x29]);
            }
            if (m_BValue[10])
            {
                g.DrawImage(m_ImgsList[12], m_RectsList[0x30]);
            }
            if (m_BValue[0x17])
            {
                g.DrawImage(m_ImgsList[15], m_RectsList[0x2e]);
            }
            if (m_BValue[0x16])
            {
                g.DrawImage(m_ImgsList[0x16], m_RectsList[0x2f]);
            }
            g.DrawString(m_FValue[5].ToString("0.0"), FontsItems.Fonts_Regular(FontName.宋体, 20f, true),
                SolidBrushsItems.YellowBrush1, m_RectsList[0x1a], FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(Convert.ToInt32(m_FValue[0]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 14f, true),
                SolidBrushsItems.YellowBrush1, m_RectsList[0x1b], FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(Convert.ToInt32(m_FValue[6]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 14f, true),
                SolidBrushsItems.YellowBrush1, m_RectsList[0x1c], FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(Convert.ToInt32(m_FValue[7]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 14f, true),
                SolidBrushsItems.YellowBrush1, m_RectsList[30], FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(Convert.ToInt32(m_FValue[8]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 14f, true),
                SolidBrushsItems.YellowBrush1, m_RectsList[0x1d], FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(Convert.ToInt32(m_FValue[9]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 14f, true),
                SolidBrushsItems.YellowBrush1, m_RectsList[0x1f], FontsItems.TheAlignment(FontRelated.居中));
            RectangleF ef = m_RectsList[0x20];
            g.FillRectangle(m_TitleBrush, ef.X, ef.Y, ef.Width, ef.Height);
            g.DrawString("故障信息", FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.WhiteBrush,
                m_RectsList[0x20], FontsItems.TheAlignment(FontRelated.居中));
            ef = m_RectsList[0x22];
            g.FillRectangle(m_TitleBrush, ef.X, ef.Y, ef.Width, ef.Height);
            g.DrawString("提示信息", FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.WhiteBrush,
                m_RectsList[0x22], FontsItems.TheAlignment(FontRelated.居中));
            if (FaultReceive.MsgInf.UnFlagCurrentMsgList.Count > 0)
            {
                for (num = 0; num < FaultReceive.MsgInf.UnFlagCurrentMsgList.Count; num++)
                {
                    if (num < 6)
                    {
                        g.DrawString(FaultReceive.MsgInf.UnFlagCurrentMsgList[num].MsgContent, FontsItems.DefaultFont,
                            SolidBrushsItems.RedBrush1, m_RectsList[0x37 + num], FontsItems.TheAlignment(FontRelated.靠左));
                    }
                }
            }

            for (num = 0; num < FaultReceive.MsgInf.CurLowLevelMsgList.Count; num++)
            {
                if (num < 6)
                {
                    g.DrawString(FaultReceive.MsgInf.CurLowLevelMsgList[num].MsgContent, FontsItems.DefaultFont,
                        SolidBrushsItems.YellowBrush1, m_RectsList[0x3d + num], FontsItems.TheAlignment(FontRelated.靠左));
                }
            }
        }
    }
}