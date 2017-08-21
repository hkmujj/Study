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
    class Train : baseClass
    {
        private Image[] m_ImageArray;

        private Rectangle[] m_DirectionArray;
        private Rectangle[] m_EscapeDoorArray;
        private Rectangle[] m_TrainRoomArray;
        private Carriage[] m_Carriage = new Carriage[6];


        //private TrainRect1 _trainRectLeft;
        //private TrainRect1 _trainRectRight;

        private TrainPolygon m_TrainPolygonLeftTop;
        private TrainPolygon m_TrainPolygonLeftDown;
        private TrainPolygon m_TrainPolygonRightTop;
        private TrainPolygon m_TrainPolygonRightDown;

        private TrainRect1 m_TrainRectLeft1;
        private TrainRect1 m_TrainRectRight1;

        public override string GetInfo()
        {
            return "列车状态";
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

            m_DirectionArray = new Rectangle[2] { new Rectangle(186 + Common.m_InitialPosX, 215 + Common.m_InitialPosY, 10, 25), new Rectangle(672 + Common.m_InitialPosX, 215 + Common.m_InitialPosY, 10, 25) };
            m_EscapeDoorArray = new Rectangle[2] { new Rectangle(196 + Common.m_InitialPosX, 215 + Common.m_InitialPosY, 12, 25), new Rectangle(660 + Common.m_InitialPosX, 215 + Common.m_InitialPosY, 12, 25) };
            m_TrainRoomArray = new Rectangle[2] { new Rectangle(204 + Common.m_InitialPosX, 207 + Common.m_InitialPosY, 16, 40), new Rectangle(648 + Common.m_InitialPosX, 207 + Common.m_InitialPosY, 16, 40) };

            for (int i = 0; i < 6; i++)
            {
                m_Carriage[i] = new Carriage((i + 1).ToString(), new Rectangle(Common.m_InitialPosX + Common.m_FirstTrainRect.X + 73 * i, Common.m_InitialPosY + 210, Common.m_FirstTrainRect.Width, Common.m_FirstTrainRect.Height));
            }




            return true;
        }

        private Color[] m_ColorArray = { Common.m_BackGroundColor, Color.Yellow };
        private void GetVlaue()
        {
            #region 车门  手柄 状态
            for (int i = 0; i < 6; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (BoolList[UIObj.InBoolList[3] + 40 * i + k * 5 + j])
                        {
                            m_Carriage[i].m_UpperDoorArray[k].ImagePicture = m_ImageArray[15 + j];
                        }

                        if (BoolList[UIObj.InBoolList[4] + 40 * i + k * 5 + j])
                        {
                            m_Carriage[i].m_DownDoorArray[k].ImagePicture = m_ImageArray[15 + j];
                        }
                    }

                    for (int j = 0; j < 3; j++)
                    {
                        if (BoolList[UIObj.InBoolList[5] + 24 * i + 3 * k + j])
                        {
                            m_Carriage[i].m_UpperHandleArray[k].ImagePicture = m_ImageArray[20 + j];
                        }

                        if (BoolList[UIObj.InBoolList[6] + 24 * i + 3 * k + j])
                        {
                            m_Carriage[i].m_DownHandleArray[k].ImagePicture = m_ImageArray[23 + j];
                        }
                    }
                }
            }

            #endregion


            # region SOS
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[8] + i])
                {
                    m_Carriage[i].State = TrainState.Sos;
                }
                else
                {
                    m_Carriage[i].State = TrainState.Normal;
                }
            }






            #endregion

            #region 车厢故障
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (BoolList[UIObj.InBoolList[7] + 2 * i + j])
                    {
                        m_Carriage[i].StateColor = m_ColorArray[j];
                    }
                }
            }
            #endregion

        }

        public override void paint(Graphics g)
        {
            GetVlaue();

            #region 方向



            m_DirectionArray = new Rectangle[2] { new Rectangle(186 + Common.m_InitialPosX, 216 + Common.m_InitialPosY, 10, 25), new Rectangle(672 + Common.m_InitialPosX, 216 + Common.m_InitialPosY, 10, 25) };

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[UIObj.InBoolList[0] + 3 * i + j])
                    {
                        g.DrawImage(m_ImageArray[3 * i + j], m_DirectionArray[i]);
                    }
                }
            }
            #endregion



            #region 驾驶室


            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[UIObj.InBoolList[2] + 3 * i + j])
                    {
                        g.DrawImage(m_ImageArray[9 + 3 * i + j], m_TrainRoomArray[i]);
                    }
                }
            }
            #endregion

            #region 逃生门


            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[UIObj.InBoolList[1] + 3 * i + j])
                    {
                        g.DrawImage(m_ImageArray




                            [6 + j], m_EscapeDoorArray[i]);
                    }
                }
            }
            #endregion

            for (int i = 0; i < 6; i++)
            {
                m_Carriage[i].OnDrawn(g);
            }


            m_TrainPolygonLeftTop = new TrainPolygon(new Point(215, 200), new List<int>() { 1153, 1154, 1155 }, 1, 1);
            m_TrainPolygonLeftDown = new TrainPolygon(new Point(215, 254), new List<int>() { 1156, 1157, 1158 }, 1, -1);
            m_TrainPolygonRightTop = new TrainPolygon(new Point(652, 200), new List<int>() { 1159, 1160, 1161 }, -1, 1);
            m_TrainPolygonRightDown = new TrainPolygon(new Point(652, 254), new List<int>() { 1162, 1163, 1164 }, -1, -1);

            m_TrainPolygonLeftTop.OnDraw(g);
            m_TrainPolygonLeftDown.OnDraw(g);
            m_TrainPolygonRightTop.OnDraw(g);
            m_TrainPolygonRightDown.OnDraw(g);


            m_TrainRectLeft1 = new TrainRect1(new Point(214, 215), new List<int>() { 1165, 1166, 1167 });
            m_TrainRectRight1 = new TrainRect1(new Point(642, 215), new List<int>() { 1168, 1169, 1170 });

            m_TrainRectLeft1.OnDraw(g);
            m_TrainRectRight1.OnDraw(g);
        }

    }

    public class Carriage
    {
        private Rectangle m_Rect;
        private string m_Label;
        private string m_Sos;

        public Color StateColor
        {
            set
            {
                m_StateBrush.Color = value;
            }
        }
        private SolidBrush m_StateBrush = new SolidBrush(Common.m_BackGroundColor);

        public RectText[] m_UpperDoorArray = new RectText[4];
        public RectText[] m_DownDoorArray = new RectText[4];

        public RectText[] m_UpperHandleArray = new RectText[4];
        public RectText[] m_DownHandleArray = new RectText[4];

        private StringFormat m_StringFormat = new StringFormat() { LineAlignment = StringAlignment.Far, Alignment = StringAlignment.Center };

        private StringFormat m_SosFormat = new StringFormat() { LineAlignment = StringAlignment.Near, Alignment = StringAlignment.Center };



        public TrainState State
        {
            set
            {
                if (value == TrainState.Normal)
                {
                    StateColor = Common.m_BackGroundColor;
                    m_Sos = string.Empty;
                }
                else
                {
                    StateColor = Color.Yellow;
                    m_Sos = "SOS";
                }
            }
        }

        public Carriage(string label, Rectangle rect)
        {
            m_Label = label;
            m_Rect = rect;

            for (int i = 0; i < 4; i++)
            {
                m_UpperDoorArray[i] = new RectText(new Rectangle(m_Rect.X + 6 + 14 * i, m_Rect.Y - 25, 12, 12), "", 1, 1, Color.Wheat, Common.m_BackGroundColor, Color.Black, 1, false);
                m_DownDoorArray[i] = new RectText(new Rectangle(m_Rect.X + 6 + 14 * i, m_Rect.Y + m_Rect.Height + 15, 12, 12), "", 1, 1, Color.Wheat, Common.m_BackGroundColor, Color.Black, 1, false);

                m_UpperHandleArray[i] = new RectText(new Rectangle(m_Rect.X + 6 + 14 * i, m_Rect.Y - 45, 12, 18), "", 1, 1, Color.Wheat, Common.m_BackGroundColor, Color.Black, 1, false);
                m_DownHandleArray[i] = new RectText(new Rectangle(m_Rect.X + 6 + 14 * i, m_Rect.Y + m_Rect.Height + 29, 12, 18), "", 1, 1, Color.Wheat, Common.m_BackGroundColor, Color.Black, 1, false);
            }
        }

        public void OnDrawn(Graphics g)
        {
            g.FillRectangle(m_StateBrush, m_Rect);
            g.DrawRectangle(Common.m_BlackPen, m_Rect);



            g.DrawString(m_Label, Common.m_12Font, Common.m_BlackBrush, m_Rect, m_StringFormat);
            g.DrawString(m_Sos, Common.m_12Font, Common.m_RedBrush, m_Rect, m_SosFormat);

            for (int i = 0; i < 4; i++)
            {
                m_UpperDoorArray[i].OnDraw(g);
                m_DownDoorArray[i].OnDraw(g);

                m_UpperHandleArray[i].OnDraw(g);
                m_DownHandleArray[i].OnDraw(g);
            }
        }


    }

    public enum TrainState
    {
        Normal = 0,
        Sos
    }
}
