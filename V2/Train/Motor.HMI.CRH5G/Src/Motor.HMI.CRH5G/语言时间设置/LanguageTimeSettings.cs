using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH5G.底层共用;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Motor.HMI.CRH5G.Resource.Images;

namespace Motor.HMI.CRH5G.语言时间设置
{
    [GksDataType(DataType.isMMIObjectClass)]
    class LanguageTimeSettings : CRH5GBase
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        //2
        public override string GetInfo()
        {
            return "语言时间设置视图";
        }


        //6
        public override bool Initalize()
        {
            return Init();
        }

        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::

        //public override void setRunValue(int nParaA, int nParaB, float nParaC)
        //{
        //    base.setRunValue(nParaA, nParaB, nParaC);
        //    if (nParaA != 2) return;
        //    m_TheWords = string.Empty;
        //}
        public override void Paint(Graphics g)
        {

            DrawOn(g);

            if (ButtonsScreen.BtnState != null)
            {
                switch (ButtonsScreen.BtnState.BtnId)
                {
                    case 10:
                        append_postCmd(CmdType.ChangePage, 33, 0, 0);
                        break;
                    case 15:
                        PrintNumber("1");
                        break;
                    case 16:
                        PrintNumber("2");
                        break;
                    case 17:
                        PrintNumber("3");
                        break;
                    case 18:
                        PrintNumber("4");
                        break;
                    case 19:
                        PrintNumber("5");
                        break;
                    case 20:
                        PrintNumber("6");
                        break;
                    case 21:
                        PrintNumber("7");
                        break;
                    case 22:
                        PrintNumber("8");
                        break;  
                    case 23:
                        PrintNumber("9");
                        break;
                    case 24:
                        PrintNumber("0");
                        break;
                }
            }

            if (ButtonsScreen.BtnState == null || ButtonsScreen.BtnState.IsPress) return;
            switch (ButtonsScreen.BtnState.BtnId)
            {
                case 11://上翻
                    if (index == 2)
                    {
                        index = 1;
                    }
                    ButtonsScreen.BtnState.Press();
                    break;
                case 12://下翻
                    if (index == 1)
                    {
                        index = 2;
                    }
                    ButtonsScreen.BtnState.Press();
                    break;
            }

           
        }

        private void PrintNumber(string numb)
        {
            
            ButtonsScreen.BtnState.Press();
        }

        private void DrawOn(Graphics e)
        {
            
                e.DrawImage(ImagesResouce.时间设置, m_RectsList[0]);

                e.DrawString("汉语", FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush,
                    m_RectsList[1], FontsItems.TheAlignment(FontRelated.居中));
                e.DrawString("English", FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush,
                    m_RectsList[2], FontsItems.TheAlignment(FontRelated.居中));


                e.DrawRectangle(new Pen(Color.Red, 2), Rectangle.Round(m_RectsList[index]));



        }
        #endregion

        private bool Init()
        {
            m_RectsList = new List<RectangleF>();
            m_RectsList = Coordinate.RectangleFLists(ViewIDName.LanguageTimeSettings);
            return true;
        }

        private List<RectangleF> m_RectsList;

        private int index = 1;
    }
}
