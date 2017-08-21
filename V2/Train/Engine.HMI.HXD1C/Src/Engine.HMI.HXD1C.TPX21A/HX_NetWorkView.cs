using System.Drawing;
using Engine.HMI.HXD1C.TPX21A.Button;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.HMI.HXD1C.TPX21A
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    internal class HX_NetWorkView : baseClass, IButtonEventListener
    {
        private readonly bool[] m_IsNumButtonDown = new bool[10]; //�ײ����ְ�ť�Ƿ���
        private Rectangle m_Cab1Rect; //cab1����
        private Rectangle m_Cab2Rect; //cab2����
        private Rectangle m_MioRect; //MIO����
        private Rectangle m_CCURect; //CCU����

        private readonly HxRectText[] m_Cab1Button = new HxRectText[4]; //cab1���豸ͼ��
        private readonly HxRectText[] m_Cab2Button = new HxRectText[4]; //cab2���豸ͼ��
        private readonly HxRectText[] m_MioButton = new HxRectText[7]; //MIO���豸ͼ��
        private readonly HxRectText[] m_CCUButton = new HxRectText[4]; //CCU���豸ͼ��
        private readonly HxRectText[] m_OtherButton = new HxRectText[5]; //���ಿ��

        private readonly string[] m_StrCab1S = new string[4] {"DXM11", "DIM12", "AXM13", "IDU1"};
        private readonly string[] m_StrCab2S = new string[4] {"DXM21", "DIM22", "AXM23", "IDU2"};
        private readonly string[] m_StrCcu = new string[5] {"ACU1", "TCU1", "BCU", "ACU2", "TCU2"};
        private readonly string[] m_StrCcuCenter = {"GWM/OCE", "ERM", "VCM2", "VCM1"};
        private readonly Pen m_LightPen = new Pen(Color.FromArgb(192, 191, 191), 1);
        private readonly Pen m_GrayPen = new Pen(Color.FromArgb(192, 191, 191), 2);
        private readonly Pen m_GreenPen = new Pen(Color.FromArgb(149, 231, 150), 2);
        private readonly Pen m_RedPen = new Pen(Color.FromArgb(255, 0, 0), 2);
        private readonly Point[] m_PointA = new Point[6];
        private readonly Point[] m_PointB = new Point[6];

        public int m_StarBit; //Bool������ʼλ��
        public int m_BitCount; //Bool�������ĸ���

        public bool ResponseMouseDown(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 6)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }

                if (btn <= ButtonName.B10)
                {
                    m_IsNumButtonDown[(int) (btn - 800)] = true;
                }
                ButtonDownEvent();
                return true;
            }

            return false;
        }

        public bool ResponseMouseUp(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 6)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }

                return true;
            }

            return false;
        }

        public override string GetInfo()
        {
            return "����������ͼ";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            GlobalParam.Instance.AddButtonEventListener(this);

            InitData();

            if (UIObj.InBoolList.Count >= 2)
            {
                m_StarBit = UIObj.InBoolList[0];
                m_BitCount = UIObj.InBoolList[1];
            }

            nErrorObjectIndex = -1;
            return true;
        }

        public void InitData()
        {
            #region �������ʼ��

            m_Cab1Rect = new Rectangle(HxCommon.Recposition.X + 15, HxCommon.Recposition.Y + 40, 150, 300);
            m_Cab2Rect = new Rectangle(HxCommon.Recposition.X + 640, HxCommon.Recposition.Y + 40, 150, 300);
            m_MioRect = new Rectangle(m_Cab1Rect.X + 60, m_Cab1Rect.Bottom + 65, 580, 64);
            m_CCURect = new Rectangle(HxCommon.Recposition.X + 360, HxCommon.Recposition.Y + 100, 170, 240);

            #endregion

            #region

            /*-----------------------------------------------------
              * 
              *            �豸�����ʼ��
              * 
              *-----------------------------------------------------
              */
            //cab1��
            for (int i = 0; i < 4; i++)
            {
                m_Cab1Button[i] = new HxRectText();
                m_Cab1Button[i].SetBkColor(198, 195, 198);
                m_Cab1Button[i].SetText(m_StrCab1S[i]);
                m_Cab1Button[i].SetTextColor(0, 0, 0);
                m_Cab1Button[i].SetTextStyle(12, FormatStyle.Center, true, "����");
                m_Cab1Button[i].SetLinePen(143, 142, 141, 2);
                m_Cab1Button[i].SetDrawFrm(true);

                if (i < 3)
                {
                    m_Cab1Button[i].SetTextRect(m_Cab1Rect.X + 65, m_Cab1Rect.Y + 5 + 60*i, 75, 55);
                }
                else
                {
                    m_Cab1Button[i].SetTextRect(m_Cab1Rect.X + 65, m_Cab1Rect.Bottom - 60, 75, 55);
                }
            }

            //cab2��
            for (int i = 0; i < 4; i++)
            {
                m_Cab2Button[i] = new HxRectText();
                m_Cab2Button[i].SetBkColor(198, 195, 198);
                m_Cab2Button[i].SetText(m_StrCab2S[i]);
                m_Cab2Button[i].SetTextColor(0, 0, 0);
                m_Cab2Button[i].SetTextStyle(12, FormatStyle.Center, true, "����");
                m_Cab2Button[i].SetLinePen(143, 142, 141, 2);
                m_Cab2Button[i].SetDrawFrm(true);

                if (i < 3)
                {
                    m_Cab2Button[i].SetTextRect(m_Cab2Rect.X + 5, m_Cab1Rect.Y + 5 + 60*i, 75, 55);
                }
                else
                {
                    m_Cab2Button[i].SetTextRect(m_Cab2Rect.X + 5, m_Cab1Rect.Bottom - 60, 75, 55);
                }
            }

            //MIO��
            for (int i = 0; i < 7; i++)
            {
                m_MioButton[i] = new HxRectText();
                m_MioButton[i].SetBkColor(198, 195, 198);
                m_MioButton[i].SetText("DXM3" + (i + 1).ToString());
                m_MioButton[i].SetTextColor(0, 0, 0);
                m_MioButton[i].SetTextStyle(12, FormatStyle.Center, true, "����");
                m_MioButton[i].SetLinePen(143, 142, 141, 2);
                m_MioButton[i].SetDrawFrm(true);
                m_MioButton[i].SetTextRect(m_MioRect.X + 5 + 83*i, m_MioRect.Y + 4, 75, 55);
            }

            //CCU��
            for (int i = 0; i < 4; i++)
            {
                m_CCUButton[i] = new HxRectText();
                m_CCUButton[i].SetBkColor(198, 195, 198);
                m_CCUButton[i].SetText(m_StrCcuCenter[i]);
                m_CCUButton[i].SetTextColor(0, 0, 0);
                m_CCUButton[i].SetTextStyle(12, FormatStyle.Center, true, "����");
                m_CCUButton[i].SetLinePen(143, 142, 141, 2);
                m_CCUButton[i].SetDrawFrm(true);
            }

            m_CCUButton[0].SetTextRect(m_CCURect.X + 4, m_CCURect.Y + 4, 75, 55);
            m_CCUButton[1].SetTextRect(m_CCURect.X + 4, m_CCURect.Bottom - 59, 75, 55);
            m_CCUButton[2].SetTextRect(m_CCURect.Right - 79, m_CCURect.Y + 4, 75, 55);
            m_CCUButton[3].SetTextRect(m_CCURect.Right - 79, m_CCURect.Bottom - 59, 75, 55);

            //������
            for (int i = 0; i < 5; i++)
            {
                m_OtherButton[i] = new HxRectText();
                m_OtherButton[i].SetBkColor(198, 195, 198);
                m_OtherButton[i].SetTextColor(0, 0, 0);
                m_OtherButton[i].SetTextStyle(12, FormatStyle.Center, true, "����");
                m_OtherButton[i].SetLinePen(143, 142, 141, 2);
                m_OtherButton[i].SetDrawFrm(true);
                m_OtherButton[i].SetText(m_StrCcu[i]);
            }
            m_OtherButton[0].SetTextRect(m_Cab1Button[1].m_RectPosition.Right + 25, m_Cab1Button[1].m_RectPosition.Y, 75, 55);
            m_OtherButton[1].SetTextRect(m_Cab1Button[3].m_RectPosition.Right + 25, m_Cab1Button[3].m_RectPosition.Y, 75, 55);
            m_OtherButton[2].SetTextRect(m_OtherButton[0].m_RectPosition.Right + 15, m_OtherButton[0].m_RectPosition.Y, 75, 55);
            m_OtherButton[3].SetTextRect(m_CCURect.Right + 15, m_OtherButton[0].m_RectPosition.Y, 75, 55);
            m_OtherButton[4].SetTextRect(m_CCURect.Right + 15, m_OtherButton[1].m_RectPosition.Y, 75, 55);

            #endregion

            #region ���������ʼ�� 

            m_PointA[0] = new Point(m_Cab1Rect.X + 25, m_Cab1Button[0].m_RectPosition.Y + 15);
            m_PointA[1] = new Point(m_Cab2Rect.Right - 5, m_Cab2Button[0].m_RectPosition.Y + 20);
            m_PointA[2] = new Point(m_Cab1Rect.X + 25, m_Cab1Button[2].m_RectPosition.Bottom + 20);
            m_PointA[3] = new Point(m_Cab2Rect.Right - 5, m_PointA[2].Y);
            m_PointA[4] = new Point(m_MioButton[0].m_RectPosition.X + 25, m_MioButton[0].m_RectPosition.Y - 45);
            m_PointA[5] = new Point(m_MioButton[6].m_RectPosition.X + 25, m_MioButton[6].m_RectPosition.Y - 45);

            m_PointB[0] = new Point(m_PointA[0].X - 10, m_PointA[0].Y + 10);
            m_PointB[1] = new Point(m_PointA[1].X - 10, m_PointA[1].Y + 10);
            m_PointB[2] = new Point(m_PointA[2].X - 10, m_PointA[2].Y + 10);
            m_PointB[3] = new Point(m_PointA[3].X - 10, m_PointA[3].Y + 10);
            m_PointB[4] = new Point(m_PointA[4].X + 10, m_PointA[4].Y + 10);
            m_PointB[5] = new Point(m_PointA[5].X + 10, m_PointA[5].Y + 10);


            #endregion
        }

        public override void paint(Graphics g)
        {
            GetValue();
            ButtonDownEvent();
            DrawOn(g);
            base.paint(g);
        }

        public void GetValue()
        {
            //���ñ���
            HxCommon.HTitle.SetText("��������");

            HxCommon.ButtonText[0].SetText("ǣ������");
            HxCommon.ButtonText[1].SetText("�¶�");
            HxCommon.ButtonText[2].SetText("����");
            HxCommon.ButtonText[3].SetText("����ϵͳ");
            HxCommon.ButtonText[4].SetText("����״̬");
            HxCommon.ButtonText[5].SetText("��������");
            HxCommon.ButtonText[6].SetText(" ");
            HxCommon.ButtonText[7].SetText("��������");
            HxCommon.ButtonText[8].SetText("���ڶ���");
            HxCommon.ButtonText[9].SetText("������");

            for (int i = 0; i < 10; i++)
            {
                if (i == 2)
                {
                    HxCommon.ButtonText[i].SetBkColor(255, 255, 0);
                    HxCommon.ButtonText[i].SetTextColor(0, 0, 0);
                }
                else
                {
                    HxCommon.ButtonText[i].SetBkColor(0, 0, 0);
                    HxCommon.ButtonText[i].SetTextColor(255, 255, 255);
                }
            }

            for (int i = 0; i < 10; i++)
            {
                m_IsNumButtonDown[i] = BoolList[UIObj.InBoolList[i + 2]];
            }
        }

        public void DrawOn(Graphics g)
        {
            //�ײ�������ť����
            for (int i = 0; i < 10; i++)
            {
                HxCommon.ButtonText[i].OnDraw(g);
            }

            //������λ�õĻ���
            g.DrawRectangle(m_LightPen, m_Cab1Rect);
            g.DrawRectangle(m_LightPen, m_Cab2Rect);
            g.DrawRectangle(m_LightPen, m_CCURect);
            g.DrawRectangle(m_LightPen, m_MioRect);

            #region ������ͼ�λ���

            for (int i = 0; i < 4; i++)
            {
                if (BoolList[m_StarBit + 2*i]) //cab1�豸��������״̬
                {
                    m_Cab1Button[i].SetBkColor(146, 245, 133);
                    m_Cab1Button[i].OnDraw(g);
                    if (i < 3)
                    {
                        g.DrawLine(m_GreenPen, m_Cab1Button[i].m_RectPosition.X, m_Cab1Button[i].m_RectPosition.Y + 15,
                            m_PointA[0].X, m_Cab1Button[i].m_RectPosition.Y + 15);
                        g.DrawLine(m_GreenPen, m_Cab1Button[i].m_RectPosition.X, m_Cab1Button[i].m_RectPosition.Y + 25,
                            m_PointB[0].X, m_Cab1Button[i].m_RectPosition.Y + 25);

                    }
                    else
                    {
                        g.DrawLine(m_GreenPen, m_Cab1Button[i].m_RectPosition.X + 25, m_Cab1Button[i].m_RectPosition.Y,
                            m_Cab1Button[i].m_RectPosition.X + 25, m_PointA[2].Y);
                        g.DrawLine(m_GreenPen, m_Cab1Button[i].m_RectPosition.X + 35, m_Cab1Button[i].m_RectPosition.Y,
                            m_Cab1Button[i].m_RectPosition.X + 35, m_PointB[2].Y);
                    }
                }
                else if (BoolList[m_StarBit + 2*i + 1]) //cab1�豸���ڹ���״̬
                {
                    m_Cab1Button[i].SetBkColor(255, 0, 0);
                    m_Cab1Button[i].OnDraw(g);
                    if (i < 3)
                    {
                        g.DrawLine(m_RedPen, m_Cab1Button[i].m_RectPosition.X, m_Cab1Button[i].m_RectPosition.Y + 15, m_PointA[0].X,
                            m_Cab1Button[i].m_RectPosition.Y + 15);
                        g.DrawLine(m_RedPen, m_Cab1Button[i].m_RectPosition.X, m_Cab1Button[i].m_RectPosition.Y + 25, m_PointB[0].X,
                            m_Cab1Button[i].m_RectPosition.Y + 25);

                    }
                    else
                    {
                        g.DrawLine(m_RedPen, m_Cab1Button[i].m_RectPosition.X + 25, m_Cab1Button[i].m_RectPosition.Y,
                            m_Cab1Button[i].m_RectPosition.X + 25, m_PointA[2].Y);
                        g.DrawLine(m_RedPen, m_Cab1Button[i].m_RectPosition.X + 35, m_Cab1Button[i].m_RectPosition.Y,
                            m_Cab1Button[i].m_RectPosition.X + 35, m_PointB[2].Y);
                    }
                }
                else
                {
                    m_Cab1Button[i].SetBkColor(198, 195, 198);
                    m_Cab1Button[i].OnDraw(g);
                    if (i < 3)
                    {
                        g.DrawLine(m_GrayPen, m_Cab1Button[i].m_RectPosition.X, m_Cab1Button[i].m_RectPosition.Y + 15, m_PointA[0].X,
                            m_Cab1Button[i].m_RectPosition.Y + 15);
                        g.DrawLine(m_GrayPen, m_Cab1Button[i].m_RectPosition.X, m_Cab1Button[i].m_RectPosition.Y + 25, m_PointB[0].X,
                            m_Cab1Button[i].m_RectPosition.Y + 25);

                    }
                    else
                    {
                        g.DrawLine(m_GrayPen, m_Cab1Button[i].m_RectPosition.X + 25, m_Cab1Button[i].m_RectPosition.Y,
                            m_Cab1Button[i].m_RectPosition.X + 25, m_PointA[2].Y);
                        g.DrawLine(m_GrayPen, m_Cab1Button[i].m_RectPosition.X + 35, m_Cab1Button[i].m_RectPosition.Y,
                            m_Cab1Button[i].m_RectPosition.X + 35, m_PointB[2].Y);
                    }
                }
            }

            //cab2
            for (int i = 0; i < 4; i++)
            {
                if (BoolList[m_StarBit + 2*i + 8]) //cab1�豸��������״̬
                {
                    m_Cab2Button[i].SetBkColor(146, 245, 133);
                    m_Cab2Button[i].OnDraw(g);
                    if (i < 3)
                    {
                        g.DrawLine(m_GreenPen, m_Cab2Button[i].m_RectPosition.Right, m_Cab1Button[i].m_RectPosition.Y + 20,
                            m_PointA[1].X, m_Cab2Button[i].m_RectPosition.Y + 20);
                        g.DrawLine(m_GreenPen, m_Cab2Button[i].m_RectPosition.Right, m_Cab1Button[i].m_RectPosition.Y + 30,
                            m_PointB[1].X, m_Cab2Button[i].m_RectPosition.Y + 30);

                    }
                    else
                    {
                        g.DrawLine(m_GreenPen, m_Cab2Button[i].m_RectPosition.X + 25, m_Cab2Button[i].m_RectPosition.Y,
                            m_Cab2Button[i].m_RectPosition.X + 25, m_PointA[2].Y);
                        g.DrawLine(m_GreenPen, m_Cab2Button[i].m_RectPosition.X + 35, m_Cab2Button[i].m_RectPosition.Y,
                            m_Cab2Button[i].m_RectPosition.X + 35, m_PointB[2].Y);
                    }
                }
                else if (BoolList[m_StarBit + 2*i + 9]) //cab1�豸���ڹ���״̬
                {
                    m_Cab2Button[i].SetBkColor(255, 0, 0);
                    m_Cab2Button[i].OnDraw(g);
                    if (i < 3)
                    {
                        g.DrawLine(m_RedPen, m_Cab2Button[i].m_RectPosition.Right, m_Cab1Button[i].m_RectPosition.Y + 20,
                            m_PointA[1].X, m_Cab2Button[i].m_RectPosition.Y + 20);
                        g.DrawLine(m_RedPen, m_Cab2Button[i].m_RectPosition.Right, m_Cab1Button[i].m_RectPosition.Y + 30,
                            m_PointB[1].X, m_Cab2Button[i].m_RectPosition.Y + 30);

                    }
                    else
                    {
                        g.DrawLine(m_RedPen, m_Cab2Button[i].m_RectPosition.X + 25, m_Cab2Button[i].m_RectPosition.Y,
                            m_Cab2Button[i].m_RectPosition.X + 25, m_PointA[2].Y);
                        g.DrawLine(m_RedPen, m_Cab2Button[i].m_RectPosition.X + 35, m_Cab2Button[i].m_RectPosition.Y,
                            m_Cab2Button[i].m_RectPosition.X + 35, m_PointB[2].Y);
                    }
                }
                else
                {
                    m_Cab2Button[i].SetBkColor(198, 195, 198);
                    m_Cab2Button[i].OnDraw(g);
                    if (i < 3)
                    {
                        g.DrawLine(m_GrayPen, m_Cab2Button[i].m_RectPosition.Right, m_Cab1Button[i].m_RectPosition.Y + 20,
                            m_PointA[1].X, m_Cab2Button[i].m_RectPosition.Y + 20);
                        g.DrawLine(m_GrayPen, m_Cab2Button[i].m_RectPosition.Right, m_Cab1Button[i].m_RectPosition.Y + 30,
                            m_PointB[1].X, m_Cab2Button[i].m_RectPosition.Y + 30);
                    }
                    else
                    {
                        g.DrawLine(m_GrayPen, m_Cab2Button[i].m_RectPosition.X + 25, m_Cab2Button[i].m_RectPosition.Y,
                            m_Cab2Button[i].m_RectPosition.X + 25, m_PointA[2].Y);
                        g.DrawLine(m_GrayPen, m_Cab2Button[i].m_RectPosition.X + 35, m_Cab2Button[i].m_RectPosition.Y,
                            m_Cab2Button[i].m_RectPosition.X + 35, m_PointB[2].Y);
                    }
                }
            }

            //ccu
            for (int i = 0; i < 4; i++)
            {
                if (BoolList[m_StarBit + 2*i + 16]) //ccu�豸��������״̬
                {
                    m_CCUButton[i].SetBkColor(146, 245, 133);
                    m_CCUButton[i].OnDraw(g);
                    if (i == 0 || i == 2)
                    {
                        g.DrawLine(m_GreenPen, m_CCUButton[i].m_RectPosition.X + 35, m_CCUButton[i].m_RectPosition.Bottom,
                            m_CCUButton[i].m_RectPosition.X + 35, m_PointA[2].Y);
                        g.DrawLine(m_GreenPen, m_CCUButton[i].m_RectPosition.X + 45, m_CCUButton[i].m_RectPosition.Bottom,
                            m_CCUButton[i].m_RectPosition.X + 45, m_PointB[2].Y);
                    }
                    else
                    {
                        g.DrawLine(m_GreenPen, m_CCUButton[i].m_RectPosition.X + 35, m_CCUButton[i].m_RectPosition.Y,
                            m_CCUButton[i].m_RectPosition.X + 35, m_PointA[2].Y);
                        g.DrawLine(m_GreenPen, m_CCUButton[i].m_RectPosition.X + 45, m_CCUButton[i].m_RectPosition.Y,
                            m_CCUButton[i].m_RectPosition.X + 45, m_PointB[2].Y);
                    }
                }
                else if (BoolList[m_StarBit + 2*i + 17]) //ccu�豸���ڹ���״̬
                {
                    m_CCUButton[i].SetBkColor(255, 0, 0);
                    m_CCUButton[i].OnDraw(g);
                    if (i == 0 || i == 2)
                    {
                        g.DrawLine(m_RedPen, m_CCUButton[i].m_RectPosition.X + 35, m_CCUButton[i].m_RectPosition.Bottom,
                            m_CCUButton[i].m_RectPosition.X + 35, m_PointA[2].Y);
                        g.DrawLine(m_RedPen, m_CCUButton[i].m_RectPosition.X + 45, m_CCUButton[i].m_RectPosition.Bottom,
                            m_CCUButton[i].m_RectPosition.X + 45, m_PointB[2].Y);
                    }
                    else
                    {
                        g.DrawLine(m_RedPen, m_CCUButton[i].m_RectPosition.X + 35, m_CCUButton[i].m_RectPosition.Y,
                            m_CCUButton[i].m_RectPosition.X + 35, m_PointA[2].Y);
                        g.DrawLine(m_RedPen, m_CCUButton[i].m_RectPosition.X + 45, m_CCUButton[i].m_RectPosition.Y,
                            m_CCUButton[i].m_RectPosition.X + 45, m_PointB[2].Y);
                    }
                }
                else
                {
                    m_CCUButton[i].SetBkColor(198, 195, 198);
                    m_CCUButton[i].OnDraw(g);
                    if (i == 0 || i == 2)
                    {
                        g.DrawLine(m_GrayPen, m_CCUButton[i].m_RectPosition.X + 35, m_CCUButton[i].m_RectPosition.Bottom,
                            m_CCUButton[i].m_RectPosition.X + 35, m_PointA[2].Y);
                        g.DrawLine(m_GrayPen, m_CCUButton[i].m_RectPosition.X + 45, m_CCUButton[i].m_RectPosition.Bottom,
                            m_CCUButton[i].m_RectPosition.X + 45, m_PointB[2].Y);
                    }
                    else
                    {
                        g.DrawLine(m_GrayPen, m_CCUButton[i].m_RectPosition.X + 35, m_CCUButton[i].m_RectPosition.Y,
                            m_CCUButton[i].m_RectPosition.X + 35, m_PointA[2].Y);
                        g.DrawLine(m_GrayPen, m_CCUButton[i].m_RectPosition.X + 45, m_CCUButton[i].m_RectPosition.Y,
                            m_CCUButton[i].m_RectPosition.X + 45, m_PointB[2].Y);
                    }
                }
            }

            //MIO
            for (int i = 0; i < 7; i++)
            {
                if (BoolList[m_StarBit + 2*i + 24]) //mio�豸��������״̬
                {
                    m_MioButton[i].SetBkColor(146, 245, 133);
                    m_MioButton[i].OnDraw(g);
                    g.DrawLine(m_GreenPen, m_MioButton[i].m_RectPosition.X + 25, m_MioButton[i].m_RectPosition.Y,
                        m_MioButton[i].m_RectPosition.X + 25, m_PointA[4].Y);
                    g.DrawLine(m_GreenPen, m_MioButton[i].m_RectPosition.X + 35, m_MioButton[i].m_RectPosition.Y,
                        m_MioButton[i].m_RectPosition.X + 35, m_PointB[4].Y);
                }
                else if (BoolList[m_StarBit + 2*i + 25]) //mio�豸���ڹ���״̬
                {
                    m_MioButton[i].SetBkColor(255, 0, 0);
                    m_MioButton[i].OnDraw(g);
                    g.DrawLine(m_RedPen, m_MioButton[i].m_RectPosition.X + 25, m_MioButton[i].m_RectPosition.Y,
                        m_MioButton[i].m_RectPosition.X + 25, m_PointA[4].Y);
                    g.DrawLine(m_RedPen, m_MioButton[i].m_RectPosition.X + 35, m_MioButton[i].m_RectPosition.Y,
                        m_MioButton[i].m_RectPosition.X + 35, m_PointB[4].Y);
                }
                else
                {
                    m_MioButton[i].SetBkColor(198, 195, 198);
                    m_MioButton[i].OnDraw(g);
                    g.DrawLine(m_GrayPen, m_MioButton[i].m_RectPosition.X + 25, m_MioButton[i].m_RectPosition.Y,
                        m_MioButton[i].m_RectPosition.X + 25, m_PointA[4].Y);
                    g.DrawLine(m_GrayPen, m_MioButton[i].m_RectPosition.X + 35, m_MioButton[i].m_RectPosition.Y,
                        m_MioButton[i].m_RectPosition.X + 35, m_PointB[4].Y);
                }
            }

            //other
            for (int i = 0; i < 5; i++)
            {
                if (BoolList[m_StarBit + 2*i + 38]) //other�豸��������״̬
                {
                    m_OtherButton[i].SetBkColor(146, 245, 133);
                    m_OtherButton[i].OnDraw(g);
                    if (i == 1 || i == 4)
                    {
                        g.DrawLine(m_GreenPen, m_OtherButton[i].m_RectPosition.X + 35, m_OtherButton[i].m_RectPosition.Y,
                            m_OtherButton[i].m_RectPosition.X + 35, m_PointA[2].Y);
                        g.DrawLine(m_GreenPen, m_OtherButton[i].m_RectPosition.X + 45, m_OtherButton[i].m_RectPosition.Y,
                            m_OtherButton[i].m_RectPosition.X + 45, m_PointB[4].Y);
                    }
                    else if (i == 2)
                    {
                        g.DrawLine(m_GreenPen, m_OtherButton[i].m_RectPosition.X + 35, m_OtherButton[i].m_RectPosition.Bottom,
                            m_OtherButton[i].m_RectPosition.X + 35, m_PointA[4].Y);
                        g.DrawLine(m_GreenPen, m_OtherButton[i].m_RectPosition.X + 45, m_OtherButton[i].m_RectPosition.Bottom,
                            m_OtherButton[i].m_RectPosition.X + 45, m_PointB[4].Y);
                    }
                    else
                    {
                        g.DrawLine(m_GreenPen, m_OtherButton[i].m_RectPosition.X + 35, m_OtherButton[i].m_RectPosition.Bottom,
                            m_OtherButton[i].m_RectPosition.X + 35, m_PointA[2].Y);
                        g.DrawLine(m_GreenPen, m_OtherButton[i].m_RectPosition.X + 45, m_OtherButton[i].m_RectPosition.Bottom,
                            m_OtherButton[i].m_RectPosition.X + 45, m_PointB[2].Y);
                    }
                }
                else if (BoolList[m_StarBit + 2*i + 39]) //other�豸���ڹ���״̬
                {
                    m_OtherButton[i].SetBkColor(255, 0, 0);
                    m_OtherButton[i].OnDraw(g);
                    if (i == 1 || i == 4)
                    {
                        g.DrawLine(m_RedPen, m_OtherButton[i].m_RectPosition.X + 35, m_OtherButton[i].m_RectPosition.Y,
                            m_OtherButton[i].m_RectPosition.X + 35, m_PointA[2].Y);
                        g.DrawLine(m_RedPen, m_OtherButton[i].m_RectPosition.X + 45, m_OtherButton[i].m_RectPosition.Y,
                            m_OtherButton[i].m_RectPosition.X + 45, m_PointB[2].Y);
                    }
                    else if (i == 2)
                    {
                        g.DrawLine(m_RedPen, m_OtherButton[i].m_RectPosition.X + 35, m_OtherButton[i].m_RectPosition.Bottom,
                            m_OtherButton[i].m_RectPosition.X + 35, m_PointA[4].Y);
                        g.DrawLine(m_RedPen, m_OtherButton[i].m_RectPosition.X + 45, m_OtherButton[i].m_RectPosition.Bottom,
                            m_OtherButton[i].m_RectPosition.X + 45, m_PointB[4].Y);
                    }
                    else
                    {
                        g.DrawLine(m_RedPen, m_OtherButton[i].m_RectPosition.X + 35, m_OtherButton[i].m_RectPosition.Bottom,
                            m_OtherButton[i].m_RectPosition.X + 35, m_PointA[2].Y);
                        g.DrawLine(m_RedPen, m_OtherButton[i].m_RectPosition.X + 45, m_OtherButton[i].m_RectPosition.Bottom,
                            m_OtherButton[i].m_RectPosition.X + 45, m_PointB[2].Y);
                    }
                }
                else
                {
                    m_OtherButton[i].SetBkColor(198, 195, 198);
                    m_OtherButton[i].OnDraw(g);
                    if (i == 1 || i == 4)
                    {
                        g.DrawLine(m_GrayPen, m_OtherButton[i].m_RectPosition.X + 35, m_OtherButton[i].m_RectPosition.Y,
                            m_OtherButton[i].m_RectPosition.X + 35, m_PointA[2].Y);
                        g.DrawLine(m_GrayPen, m_OtherButton[i].m_RectPosition.X + 45, m_OtherButton[i].m_RectPosition.Y,
                            m_OtherButton[i].m_RectPosition.X + 45, m_PointB[2].Y);
                    }
                    else if (i == 2)
                    {
                        g.DrawLine(m_GrayPen, m_OtherButton[i].m_RectPosition.X + 35, m_OtherButton[i].m_RectPosition.Bottom,
                            m_OtherButton[i].m_RectPosition.X + 35, m_PointA[4].Y);
                        g.DrawLine(m_GrayPen, m_OtherButton[i].m_RectPosition.X + 45, m_OtherButton[i].m_RectPosition.Bottom,
                            m_OtherButton[i].m_RectPosition.X + 45, m_PointB[4].Y);
                    }
                    else
                    {
                        g.DrawLine(m_GrayPen, m_OtherButton[i].m_RectPosition.X + 35, m_OtherButton[i].m_RectPosition.Bottom,
                            m_OtherButton[i].m_RectPosition.X + 35, m_PointA[2].Y);
                        g.DrawLine(m_GrayPen, m_OtherButton[i].m_RectPosition.X + 45, m_OtherButton[i].m_RectPosition.Bottom,
                            m_OtherButton[i].m_RectPosition.X + 45, m_PointB[2].Y);
                    }
                }
            }

            #endregion

            #region ����������

            if (BoolList[m_StarBit + 48]) //����A����
            {
                g.DrawLine(m_GreenPen, m_PointA[0], m_PointA[2]);
                g.DrawLine(m_GreenPen, m_PointA[1], m_PointA[3]);
                g.DrawLine(m_GreenPen, m_PointA[2], m_PointA[3]);
                g.DrawLine(m_GreenPen, m_PointA[4], m_PointA[5]);

            }
            else if (BoolList[m_StarBit + 49]) //����A����
            {
                g.DrawLine(m_RedPen, m_PointA[0], m_PointA[2]);
                g.DrawLine(m_RedPen, m_PointA[1], m_PointA[3]);
                g.DrawLine(m_RedPen, m_PointA[2], m_PointA[3]);
                g.DrawLine(m_RedPen, m_PointA[4], m_PointA[5]);
            }
            else
            {
                g.DrawLine(m_GrayPen, m_PointA[0], m_PointA[2]);
                g.DrawLine(m_GrayPen, m_PointA[1], m_PointA[3]);
                g.DrawLine(m_GrayPen, m_PointA[2], m_PointA[3]);
                g.DrawLine(m_GrayPen, m_PointA[4], m_PointA[5]);
            }

            if (BoolList[m_StarBit + 50]) //����B����
            {
                g.DrawLine(m_GreenPen, m_PointB[0], m_PointB[2]);
                g.DrawLine(m_GreenPen, m_PointB[1], m_PointB[3]);
                g.DrawLine(m_GreenPen, m_PointB[2], m_PointB[3]);
                g.DrawLine(m_GreenPen, m_PointB[4], m_PointB[5]);

            }
            else if (BoolList[m_StarBit + 51]) //����A����
            {
                g.DrawLine(m_RedPen, m_PointB[0], m_PointB[2]);
                g.DrawLine(m_RedPen, m_PointB[1], m_PointB[3]);
                g.DrawLine(m_RedPen, m_PointB[2], m_PointB[3]);
                g.DrawLine(m_RedPen, m_PointB[4], m_PointB[5]);
            }
            else
            {
                g.DrawLine(m_GrayPen, m_PointB[0], m_PointB[2]);
                g.DrawLine(m_GrayPen, m_PointB[1], m_PointB[3]);
                g.DrawLine(m_GrayPen, m_PointB[2], m_PointB[3]);
                g.DrawLine(m_GrayPen, m_PointB[4], m_PointB[5]);
            }

            #endregion

            g.DrawString("line_A", HxCommon.Font12B, HxCommon.WhiteBrush, m_PointA[2].X + 15, m_PointA[2].Y - 18);
            g.DrawString("line_B", HxCommon.Font12B, HxCommon.WhiteBrush, m_PointB[2].X + 5, m_PointB[2].Y + 5);
            g.DrawString("line_A", HxCommon.Font12B, HxCommon.WhiteBrush, m_OtherButton[1].m_RectPosition.X + 40,
                m_Cab1Rect.Bottom + 5);
            g.DrawString("line_B", HxCommon.Font12B, HxCommon.WhiteBrush, m_MioButton[1].m_RectPosition.Right - 25,
                m_PointB[4].Y + 10);

            g.DrawString("cab1", HxCommon.Font12B, HxCommon.WhiteBrush, m_Cab1Rect.X + 60, m_Cab1Rect.Bottom + 5);
            g.DrawString("cab2", HxCommon.Font12B, HxCommon.WhiteBrush, m_Cab2Rect.X + 60, m_Cab2Rect.Bottom + 5);
            g.DrawString("MIO", HxCommon.Font12B, HxCommon.WhiteBrush, m_MioRect.Right + 5, m_MioRect.Y + 30);
            g.DrawString("CCU", HxCommon.Font12B, HxCommon.WhiteBrush, m_CCURect.X + 65, m_CCURect.Y - 20);



        }

        /// <summary>
        /// ��ӦӲ����ť�����¼�
        /// </summary>
        public void ButtonDownEvent()
        {
            for (int i = 0; i < 10; i++)
            {
                if (m_IsNumButtonDown[i])
                {
                    switch (i)
                    {
                        case 0:
                            break;
                        case 1: //��ת���¶���ͼ
                            append_postCmd(CmdType.ChangePage, 5, 0, 0);
                            break;
                        case 2: //��ת���������
                            append_postCmd(CmdType.ChangePage, 6, 0, 0);
                            break;
                        case 3: //��ת������ϵͳ
                            append_postCmd(CmdType.ChangePage, 7, 0, 0);
                            break;
                        case 4: //��ת��ǣ��״̬
                            append_postCmd(CmdType.ChangePage, 8, 0, 0);
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7: //��ת���������� 
                            append_postCmd(CmdType.ChangePage, 11, 0, 0);
                            break;
                        case 8: //��ת�����ڶ��� 
                            append_postCmd(CmdType.ChangePage, 12, 0, 0);
                            break;
                        case 9: //����������
                            append_postCmd(CmdType.ChangePage, 1, 0, 0);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

    }
}
