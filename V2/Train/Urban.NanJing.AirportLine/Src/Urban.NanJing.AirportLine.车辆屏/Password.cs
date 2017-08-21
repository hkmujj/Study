using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
namespace Urban.NanJing.AirportLine.DDU
{
    [GksDataType(DataType.isMMIObjectClass)]
    class Password : baseClass
    {
        private Image[] m_ImageArray;

        private Rectangle m_ImageRect;

        private RectText[] m_ButtonArray;

        private RectText m_Title;

        private string[] m_StringArray;

        public override string GetInfo()
        {
            return "密码";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_ImageArray = new Image[UIObj.ParaList.Count];


            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                using (FileStream fs = new FileStream(RecPath + "\\" + UIObj.ParaList[i], FileMode.Open))
                {
                    m_ImageArray[i] = Image.FromStream(fs);
                }
            }

            m_ImageRect = new Rectangle(Common.m_InitialPosX + 20, Common.m_InitialPosY + 200, 658, 340);

            m_StringArray = new string[] 
            {
                "1", "2", "3", "4", "5", "6", "7", "8", "9", "0",
                "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P",
                "A", "S", "D", "F", "G", "H", "J", "K", "L",
                "Z", "X", "C", "V", "B", "N","M", 
                "Cancel", "Enter"
            };


            m_Title = new RectText(new Rectangle(Common.m_InitialPosX + 60, Common.m_InitialPosY + 180, 540, 45), "", 14, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 2, false, m_ImageArray[3], true);
            m_ButtonArray = new RectText[38];



            for (int i = 0; i < 36; i++)
            {
                if (i < 29)
                {
                    m_ButtonArray[i] = new RectText(new Rectangle(Common.m_InitialPosX + 60 + 54 * (i % 10), Common.m_InitialPosY + 270 + i / 10 * 54, 48, 48), m_StringArray[i], 14, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 2, false, m_ImageArray[1], true);
                    m_ButtonArray[i].MouseDownEvent += OnMouseUpHandler1;
                }
                else
                {
                    m_ButtonArray[i] = new RectText(new Rectangle(Common.m_InitialPosX + 60 + 54 * ((i+1) % 10 ), Common.m_InitialPosY + 270 + (i+1) / 10 * 54, 48, 48), m_StringArray[i], 14, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 2, false, m_ImageArray[1], true);
                    m_ButtonArray[i].MouseDownEvent += OnMouseUpHandler1;
                }
            }
            m_ButtonArray[36] = new RectText(new Rectangle(Common.m_InitialPosX + 60 , Common.m_InitialPosY + 270 + 4* 54, 102, 48), m_StringArray[36], 14, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 2, false, m_ImageArray[2], true);
            m_ButtonArray[36].MouseDownEvent += OnMouseUpHandler2;

            m_ButtonArray[37] = new RectText(new Rectangle(Common.m_InitialPosX + 60 +54*2, Common.m_InitialPosY + 270 + 4 * 54, 102, 48), m_StringArray[37], 14, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 2, false, m_ImageArray[2], true);
            return true;
        }

        public override void paint(Graphics g)
        {
            g.DrawString("请输入密码以便进入维护界面", Common.m_14Font, Common.m_BlackBrush, new Point(Common.m_InitialPosX + 20, Common.m_InitialPosY + 140));
            //g.DrawImage(_imageArray[0], _imageRect);
            for (int i = 0; i < 38; i++)
            {
                m_ButtonArray[i].OnDraw(g);
            }
            m_Title.OnDraw(g);
        }


        private void OnMouseUpHandler1(string value)
        {
            m_Title.Text += value;
        }

        private void OnMouseUpHandler2(string value)
        {
            m_Title.Text = "";
        }

        public override bool mouseUp(Point point)
        {
            foreach (var v in m_ButtonArray)
            {
                v.OnMouseUp(point);
            }

            return true;
        }
    }
}
