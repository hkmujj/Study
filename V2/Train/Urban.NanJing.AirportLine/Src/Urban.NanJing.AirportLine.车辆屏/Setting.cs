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
    public class Setting : baseClass
    {
        private Image[] m_ImageArray;
        private Button[] m_ButtonArray;
        private Button m_QuitButton;
        private int[] m_MessageArray;

        public override string GetInfo()
        {
            return "设置";
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

            m_MessageArray = new int[15] { 6, 7, 10, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 1 };

            m_ButtonArray = new Button[14];

            for (int i = 0; i < m_ButtonArray.Length; i++)
            {
                m_ButtonArray[i] = new Button(new Rectangle(130 + (i % 5) * 90, 210 + (i / 5) * 90, 80, 80), null, m_ImageArray[i], m_MessageArray[i]);
                m_ButtonArray[i].MouseUpEvent += OnButtonMouseUp;
            }

            m_QuitButton = new Button(new Rectangle(650, 500, 130, 30), null, m_ImageArray[14], m_MessageArray[14]);
            m_QuitButton.MouseUpEvent += OnButtonMouseUp;
            return true;
        }

        public override bool mouseDown(Point point)
        {
            for (int i = 0; i < m_ButtonArray.Length; i++)
            {
                m_ButtonArray[i].MouseDown(point);
            }
            m_QuitButton.MouseDown(point);
            return true;
        }


        public override bool mouseUp(Point point)
        {
            for (int i = 0; i < m_ButtonArray.Length; i++)
            {
                m_ButtonArray[i].MouseUp(point);
            }
            m_QuitButton.MouseUp(point);
            return true;
        }

        public override void paint(Graphics g)
        {
            for (int i = 0; i < m_ButtonArray.Length; i++)
            {
                m_ButtonArray[i].OnDraw(g);
            }
            m_QuitButton.OnDraw(g);
        }

        private void OnButtonMouseUp(int message)
        {
            append_postCmd(CmdType.ChangePage, message, 0, 0);
        }
    }
}
