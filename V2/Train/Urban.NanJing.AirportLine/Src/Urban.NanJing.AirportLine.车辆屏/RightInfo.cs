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
    class RightInfo : baseClass
    {
        private List<Button> m_ButtonList = new List<Button>();
        private int m_CurrentView;
        private Image[] m_ImageArray;

        private Point m_LineStartPoint;
        private Point m_LineEndPoint;

        private Button m_Examine;
        private Button m_EventSave;
        private Button m_EventInfo;
        private Button m_Road;
        private Button m_Road1;
        private Button m_Fault;
        private Button m_SpecialInfo;
        private Button m_Return;
        private Button m_Wheel;
        private Button m_Report;

        private int[] m_MessageArray = { 1, 32, 28, 31, 34, 31, 1 };

        public override string GetInfo()
        {
            return "右边信息栏";
        }


        private void OnMouseUpHandler(int message)
        {
            if (message > 0)
            {
                append_postCmd(CmdType.ChangePage, message, 0, 0);
            }

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

            m_LineStartPoint = new Point(695 + Common.m_InitialPosX, 122 + Common.m_InitialPosY);
            m_LineEndPoint = new Point(695 + Common.m_InitialPosX, 558 + Common.m_InitialPosY);


            m_Examine = new Button(new Rectangle(697 + Common.m_InitialPosX, 130 + Common.m_InitialPosY, 103, 75), null, m_ImageArray[0], m_MessageArray[0]);
            m_EventSave = new Button(new Rectangle(697 + Common.m_InitialPosX, 210 + Common.m_InitialPosY, 103, 75), null, m_ImageArray[1], m_MessageArray[0]);


            m_EventInfo = new Button(new Rectangle(697 + Common.m_InitialPosX, 467 + Common.m_InitialPosY, 103, 75), null, m_ImageArray[7], m_MessageArray[1]);
            m_Road = new Button(new Rectangle(697 + Common.m_InitialPosX, 390 + Common.m_InitialPosY, 103, 75), null, m_ImageArray[2], m_MessageArray[2]);
            m_Road1 = new Button(new Rectangle(697 + Common.m_InitialPosX, 390 + Common.m_InitialPosY, 103, 75), null, m_ImageArray[6], -1);

            m_Fault = new Button(new Rectangle(697 + Common.m_InitialPosX, 467 + Common.m_InitialPosY, 103, 75), null, m_ImageArray[3], m_MessageArray[1]);

            m_SpecialInfo = new Button(new Rectangle(697 + Common.m_InitialPosX, 130 + Common.m_InitialPosY, 103, 75), null, m_ImageArray[4], m_MessageArray[4]);
            m_Return = new Button(new Rectangle(697 + Common.m_InitialPosX, 130 + Common.m_InitialPosY, 103, 75), null, m_ImageArray[5], m_MessageArray[5]);
            m_Wheel = new Button(new Rectangle(697 + Common.m_InitialPosX, 390 + Common.m_InitialPosY, 103, 75), null, m_ImageArray[5], m_MessageArray[6]);
            m_Report = new Button(new Rectangle(697 + Common.m_InitialPosX, 130 + Common.m_InitialPosY, 103, 75), null, m_ImageArray[8], m_MessageArray[0]);

            m_Examine.MouseUpEvent += OnMouseUpHandler;
            m_EventInfo.MouseUpEvent += OnMouseUpHandler;
            m_Road.MouseUpEvent += OnMouseUpHandler;
            m_Fault.MouseUpEvent += OnMouseUpHandler;
            m_SpecialInfo.MouseUpEvent += OnMouseUpHandler;
            m_Return.MouseUpEvent += OnMouseUpHandler;
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA==2)
            {
                m_Return.Message =(int) nParaC;
            }
            if (m_CurrentView != nParaB)
            {
                m_CurrentView = nParaB;
                m_ButtonList.Clear();
               
                switch (nParaB)
                {
                    case 1:
                        {
                            m_ButtonList.Add(m_Examine);
                            m_ButtonList.Add(m_EventSave);
                            m_ButtonList.Add(m_Road);
                            m_ButtonList.Add(m_Fault);
                        }

                        break;
                    case 2:
                        {
                            m_ButtonList.Add(m_Road);
                            m_ButtonList.Add(m_Fault);
                        }
                        break;
                    case 3:
                        {
                            m_ButtonList.Add(m_Road);
                            m_ButtonList.Add(m_Fault);
                        }
                        break;
                    case 4:
                        {
                            m_ButtonList.Add(m_SpecialInfo);
                            m_ButtonList.Add(m_Road);
                            m_ButtonList.Add(m_Fault);
                        }
                        break;
                    case 6:
                        {
                            m_ButtonList.Add(m_Return);
                        }
                        break;
                    case 7:
                        {
                            m_ButtonList.Add(m_Return);
                            m_ButtonList.Add(m_Road);
                            m_ButtonList.Add(m_Fault);
                        }
                        break;
                    case 10:
                        {
                            m_ButtonList.Add(m_Return);
                        }
                        break;
                    case 17:
                        {
                            m_ButtonList.Add(m_Return);
                        }
                        break;
                    case 18:
                        {
                            m_ButtonList.Add(m_Return);
                        }
                        break;
                    case 19:
                        {
                            m_ButtonList.Add(m_Return);

                        }
                        break;
                    case 20:
                        {
                            m_ButtonList.Add(m_Return);
                            m_ButtonList.Add(m_Wheel);
                        }
                        break;
                    case 21:
                        {
                            m_ButtonList.Add(m_Return);

                        }
                        break;
                    case 22:
                        {
                            m_ButtonList.Add(m_Return);

                        }
                        break;
                    case 23:
                        {
                            m_ButtonList.Add(m_Return);

                        }
                        break;
                    case 24:
                        {
                            m_ButtonList.Add(m_Return);

                        }
                        break;
                    case 25:
                        {
                            m_ButtonList.Add(m_Return);

                        }
                        break;
                    case 26:
                        {
                            m_ButtonList.Add(m_Return);

                        }
                        break;
                    case 27:
                        {
                            m_ButtonList.Add(m_Return);

                        }
                        break;
                    case 28:
                        {
                            m_ButtonList.Add(m_Return);
                            m_ButtonList.Add(m_Road1);
                            m_ButtonList.Add(m_Fault);
                        }
                        break;
                    case 32:
                        {
                            m_ButtonList.Add(m_Return);
                            m_ButtonList.Add(m_Road);
                            m_ButtonList.Add(m_EventInfo);
                        }
                        break;
                    case 33:
                        {
                            m_ButtonList.Add(m_Return);
                        }
                        break;
                    case 34:
                        {
                            m_ButtonList.Add(m_Report);
                            m_ButtonList.Add(m_Road);
                            m_ButtonList.Add(m_Fault);
                        }
                        break;
                }
            }
        }

        public override void paint(Graphics g)
        {

            g.DrawLine(Common.m_BlackPen, m_LineStartPoint, m_LineEndPoint);

            foreach (var v in m_ButtonList)
            {
                v.OnDraw(g);
            }
        }

        public override bool mouseDown(Point point)
        {
            if (m_CurrentView == 33)
            {
                return true;
            }
            foreach (var v in m_ButtonList)
            {
                v.MouseDown(point);
            }
            return true;
        }

        public override bool mouseUp(Point point)
        {
            if (m_CurrentView == 33)
            {
                return true;
            }
            foreach (var v in m_ButtonList)
            {
                v.MouseUp(point);
            }
            return true;
        }

    }
}
