using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH5G.底层共用;

namespace Motor.HMI.CRH5G.密码
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class PassWord : CRH5GBase 
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        //2
        public override string GetInfo()
        {
            return "密码视图";
        }

        //6
        public override bool Initalize()
        {
            return Init();
        }

        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if(nParaA != 2)return;
            m_TheWords = string.Empty;
        }

        public override void Paint(Graphics g)
        {
            if (ButtonsScreen.BtnState != null)
            {
                switch (ButtonsScreen.BtnState.BtnId)
                {
                    case 10:
                        append_postCmd(CmdType.ChangePage, 21, 0, 0);
                        break;
                    case 15:
                        PrintPassword("1");
                        break;
                    case 16:
                        PrintPassword("2");
                        break;
                    case 17:
                        PrintPassword("3");
                        break;
                    case 18:
                        PrintPassword("4");
                        break;
                    case 19:
                        PrintPassword("5");
                        break;
                    case 20:
                        if (m_PassWord.Equals(m_TheWords))
                        {
                            append_postCmd(CmdType.ChangePage, 22, 0, 0);//仪表
                        }
                        else
                        {
                            m_TheWords = string.Empty;
                        }
                        ButtonsScreen.BtnState.Press();
                        break;
                    case 21:
                        append_postCmd(CmdType.ChangePage, 21, 0, 0);
                        break;
                }
            }
          
            DrawOn(g);
        }

        private void PrintPassword(string numb)
        {
            if (m_TheWords.Length < 5)
            {
                m_TheWords += numb;
            }
            ButtonsScreen.BtnState.Press();
        }

        private void DrawOn(Graphics e)
        {

            e.DrawRectangle(new Pen(Color.Green, 3), Rectangle.Round(m_RectsList[0]));

            e.DrawString("请输入密码", FontsItems.Fonts_Regular(FontName.宋体, 26, false), SolidBrushsItems.YellowBrush1,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.居中));

            if (m_TheWords.Length == 0)
            {
                if (DateTime.Now.Second % 2 == 0)
                {
                    e.DrawString("|", FontsItems.Fonts_Regular(FontName.宋体, 18, true), SolidBrushsItems.WhiteBrush,
                    m_RectsList[4], FontsItems.TheAlignment(FontRelated.居中));
                } 
            } 
            
            //for (int i = 0; i < 5; i++)
            //{
            //    e.DrawRectangle(PenItems.WhitePen, Rectangle.Round(m_RectsList[2 + i]));
            //}

            if(m_TheWords.Length == 0)return;
            for (int i = 0; i < m_TheWords.Length; i++)
            {
                e.DrawString("*", FontsItems.DefaultFont, SolidBrushsItems.GreenBrush1,
                    m_RectsList[2 + i], FontsItems.TheAlignment(FontRelated.居中));
            }
        }

        #endregion

        private bool Init()
        {
            m_RectsList = Coordinate.RectangleFLists(ViewIDName.PassWord);
            return true;
        }

        private List<RectangleF> m_RectsList;

        private string m_TheWords = string.Empty;
        private const string m_PassWord = "12345";
    }
}
