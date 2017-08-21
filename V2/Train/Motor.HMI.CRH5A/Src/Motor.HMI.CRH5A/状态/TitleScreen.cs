using System;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface;
using Motor.HMI.CRH5A.Resource;
using Motor.HMI.CRH5A.底层共用;

namespace Motor.HMI.CRH5A.Staus
{
    [GksDataType(DataType.isMMIObjectClass)]
    public partial class TitleScreen
    {
        //2
        public override string GetInfo()
        {
            return "标题视图";
        }
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            ViewId = nParaB;
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                AcceptBottomBtnVisitor(null);
            }
        }

        public void AcceptBottomBtnVisitor(BottomButtonVisitor visitor)
        {
            m_BottomButtonVisitor = visitor;
        }

        public override void Paint(Graphics g)
        {
            var value = ScreenIdentification == ScreenIdentification.ScreenTd
                ? GetInBoolValue(InBoolKeys.InBTD黑屏479)
                : GetInBoolValue(InBoolKeys.InBTS黑屏478);
            if (!value)
            {
                append_postCmd(CmdType.ChangePage, 0, 0, 0);
            }
            if (GetInBoolValue(InBoolKeys.InB制动试验开始) && ScreenIdentification == ScreenIdentification.ScreenTd)
            {
                append_postCmd(CmdType.ChangePage, 34, 0, 0);
            }

            GetValue();
            DrawOn(g);
            DrawBtnState(g);
        }

        private void DrawOn(Graphics g)
        {
            for (int i = 0; i < 4; i++)
            {
                g.DrawRectangle(PenItems.GetThePen(SolidBrushsItems.WhiteBrush, 2),
                    m_RectsList[i].X,
                    m_RectsList[i].Y,
                    m_RectsList[i].Width,
                    m_RectsList[i].Height);
            }

            for (int i = 0; i < 10; i++)
            {
                g.DrawRectangle(PenItems.GetThePen(SolidBrushsItems.GreenBrush1, 2),
                    m_RectsList[6 + i].X,
                    m_RectsList[6 + i].Y,
                    m_RectsList[6 + i].Width,
                    m_RectsList[6 + i].Height);
            }

            g.DrawString("设定速度",
                FontsItems.Fonts_Regular(FontName.宋体, 10f, false),
                SolidBrushsItems.YellowBrush1,
                m_RectsList[4],
                FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(Convert.ToInt32(GetInFloatValue(InFloatKeys.InF车辆设定速度)) + "Km/h",
                FontsItems.Fonts_Regular(FontName.宋体, 14f, true),
                SolidBrushsItems.WhiteBrush,
                m_RectsList[5],
                FontsItems.TheAlignment(FontRelated.居中));


            g.DrawString(CurrentTime.ToString("d/MM/yy"),
                FontsItems.Fonts_Regular(FontName.宋体, 14f, true),
                SolidBrushsItems.WhiteBrush,
                m_RectsList[2],
                FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(CurrentTime.ToString("hh:mm:ss tt").Replace("上午", "AM").Replace("下午", "PM"),
                FontsItems.Fonts_Regular(FontName.宋体, 14f, true),
                SolidBrushsItems.WhiteBrush,
                m_RectsList[3],
                FontsItems.TheAlignment(FontRelated.居中));

        }

        //下方按钮
        private void DrawBtnState(Graphics g)
        {
            if (m_BottomButtonVisitor == null)
            {
                DrawBtnSelectedState(g);

                DrawBottomBtnContent(g);
            }
            else
            {
                m_BottomButtonVisitor.DrawBtnContent(g);
            }
        }

        private void DrawBtnSelectedState(Graphics g)
        {
            switch (ViewId)
            {
                case 41:
                case 40:
                case 34:
                case 30:
                case 21:
                case 10:
                case 7:
                case 4:
                case 1:
                    g.DrawRectangle(PenItems.GetThePen(SolidBrushsItems.RedBrush1, 2f), Rectangle.Round(m_RectsList[6]));
                    break;
                case 42:
                case 38:
                case 35:
                case 31:
                case 22:
                case 11:
                case 8:
                case 5:
                case 2:
                    g.DrawRectangle(PenItems.GetThePen(SolidBrushsItems.RedBrush1, 2f), Rectangle.Round(m_RectsList[7]));
                    break;
                case 43:
                case 36:
                case 32:
                case 9:
                case 6:
                case 3:
                    g.DrawRectangle(PenItems.GetThePen(SolidBrushsItems.RedBrush1, 2f), Rectangle.Round(m_RectsList[8]));
                    break;
            }
        }

        private void DrawBottomBtnContent(Graphics g)
        {

            for (int i = 0; i < 10; i++)
            {
                switch (ViewId)
                {
                    case 3:
                    case 2:
                    case 1:
                        g.DrawString(m_BtnStr1[i],
                            FontsItems.DefaultFont,
                            SolidBrushsItems.GreenBrush1,
                            m_RectsList[6 + i],
                            FontsItems.TheAlignment(FontRelated.居中));
                        break;
                    case 6:
                    case 5:
                    case 4:
                        g.DrawString(m_BtnStr2[i],
                            FontsItems.DefaultFont,
                            SolidBrushsItems.GreenBrush1,
                            m_RectsList[6 + i],
                            FontsItems.TheAlignment(FontRelated.居中));
                        break;
                    case 9:
                    case 8:
                    case 7:
                        g.DrawString(m_BtnStr3[i],
                            FontsItems.DefaultFont,
                            SolidBrushsItems.GreenBrush1,
                            m_RectsList[6 + i],
                            FontsItems.TheAlignment(FontRelated.居中));
                        break;
                    case 11:
                    case 10:
                        g.DrawString(m_BtnStr4[i],
                            FontsItems.DefaultFont,
                            SolidBrushsItems.GreenBrush1,
                            m_RectsList[6 + i],
                            FontsItems.TheAlignment(FontRelated.居中));
                        break;
                    case 22:
                    case 21: //仪表试图
                        g.DrawString(m_BtnStr5[i],
                            FontsItems.DefaultFont,
                            SolidBrushsItems.GreenBrush1,
                            m_RectsList[6 + i],
                            FontsItems.TheAlignment(FontRelated.居中));
                        break;
                    case 32:
                    case 31:
                    case 30: //电子仪器
                        g.DrawString(m_BtnStr6[i],
                            FontsItems.DefaultFont,
                            SolidBrushsItems.GreenBrush1,
                            m_RectsList[6 + i],
                            FontsItems.TheAlignment(FontRelated.居中));
                        break;
                    case 36:
                    case 35:
                    case 34: //制动试验
                        g.DrawString(m_BtnStr7[i],
                            FontsItems.DefaultFont,
                            SolidBrushsItems.GreenBrush1,
                            m_RectsList[6 + i],
                            FontsItems.TheAlignment(FontRelated.居中));
                        break;
                    case 38:
                    case 37: //系统设置
                        g.DrawString(m_BtnStr8[i],
                            FontsItems.DefaultFont,
                            SolidBrushsItems.GreenBrush1,
                            m_RectsList[6 + i],
                            FontsItems.TheAlignment(FontRelated.居中));
                        break;
                    case 39: //下载数据故障
                        g.DrawString(m_BtnStr9[i],
                            FontsItems.DefaultFont,
                            SolidBrushsItems.GreenBrush1,
                            m_RectsList[6 + i],
                            FontsItems.TheAlignment(FontRelated.居中));
                        break;
                    case 40: //指令
                        g.DrawString(m_BtnStr10[i],
                            FontsItems.DefaultFont,
                            SolidBrushsItems.GreenBrush1,
                            m_RectsList[6 + i],
                            FontsItems.TheAlignment(FontRelated.居中));
                        break;
                    case 43:
                    case 42:
                    case 41: //维护
                        g.DrawString(m_BtnStr11[i],
                            FontsItems.DefaultFont,
                            SolidBrushsItems.GreenBrush1,
                            m_RectsList[6 + i],
                            FontsItems.TheAlignment(FontRelated.居中));
                        break;
                    case 44: //密码
                        g.DrawString(m_BtnStr13[i],
                            FontsItems.DefaultFont,
                            SolidBrushsItems.GreenBrush1,
                            m_RectsList[6 + i],
                            FontsItems.TheAlignment(FontRelated.居中));
                        break;
                    case 101: //故障确认
                        g.DrawString(BtnStr12[i],
                            FontsItems.Font36,
                            SolidBrushsItems.GreenBrush1,
                            m_RectsList[6 + i],
                            FontsItems.TheAlignment(FontRelated.居中));
                        break;
                    case 102: //故障历史
                        g.DrawString(BtnStr14[i],
                            FontsItems.DefaultFont,
                            SolidBrushsItems.GreenBrush1,
                            m_RectsList[6 + i],
                            FontsItems.TheAlignment(FontRelated.居中));
                        break;
                }
            }
        }
    }
}
