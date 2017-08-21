using System;
using System.Collections.Generic;
using System.Drawing;
using Engine.HMI.HXD1C.TPX21A.Button;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.HMI.HXD1C.TPX21A
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    internal class HX_TractionView : baseClass, IButtonEventListener
    {
        private Rectangle m_Rect; //�����������Ϣ��ʾ����
        private readonly HxRectText[] m_TxtTruckBolster = new HxRectText[2]; //ת���
        private readonly Rectangle[] m_InfoRect = new Rectangle[6]; //��ʾת���ǣ����/�ƶ�������״ͼ��
        private readonly SortedList<int, Image> m_ImageList = new SortedList<int, Image>(); //��������Ҫ�Ľ���Ԫ��
        private Rectangle m_IconRect; //ͼ����Ϣ��ʾ��
        private readonly Rectangle[] m_ChildiconsRect = new Rectangle[8]; //ͼ����������Ϣ���ĳ�ʼ��
        private readonly HxRectText[] m_TxtStatus = new HxRectText[2]; //�ٶ� ǣ���ƶ� ״̬��ʾͼ��

        private readonly bool[] m_IsNumButtonDown = new bool[10]; //�ײ����ְ�ť�Ƿ���
        private readonly bool[] m_IsTrainDirection = new bool[3]; //��������
        private readonly bool[] m_IsAlert = new bool[2]; //����װ��
        private readonly bool[] m_IsTrainBrake = new bool[5]; //�����ƶ�
        private readonly bool[] m_IsStopBrake = new bool[4]; //ͣ���ƶ�
        private readonly bool[] m_IsCurrentCollector1 = new bool[3]; //�ܵ繭1״̬
        private readonly bool[] m_IsCurrentCollector2 = new bool[3]; //�ܵ繭2״̬
        private readonly TriangleMark[] m_TMark = new TriangleMark[6]; //����ͼ��
        private readonly bool[] m_IsElectricMachine = new bool[4]; //���״̬ 
        private readonly int[] m_TargetTractionN = new int[6]; //Ŀ��ǣ����
        private readonly int[] m_TargetBrakeN = new int[6]; //Ŀ���ƶ���
        private readonly int[] m_RealTractionN = new int[6]; //ʵ��ǣ����
        private readonly int[] m_RealBrakeN = new int[6]; //ʵ���ƶ���

        private readonly int[] m_Value = new int[3]; //�ٶ� ǣ�����ƶ���

        public override string GetInfo()
        {
            return "ǣ��������ͼ";
        }

        public bool ResponseMouseDown(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 4)
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
            if (GlobalParam.Instance.CurrentViewId == 4)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }

                return true;
            }

            return false;
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            GlobalParam.Instance.AddButtonEventListener(this);

            InitData();

            //����ͼƬ
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Image image = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
                m_ImageList.Add(i, image);
            }
            nErrorObjectIndex = -1;

            return true;
        }

        public override void paint(Graphics g)
        {
            GetValue();
            ButtonDownEvent();
            DrawOn(g);
            base.paint(g);
        }

        public void InitData()
        {
            m_Rect = new Rectangle(HxCommon.Recposition.X + 3, HxCommon.Recposition.Y + 30, 600, 455);
            m_IconRect = new Rectangle(m_Rect.Right + 4, m_Rect.Y, 195, 455);

            //״̬��Ϣ���߿�λ�ó�ʼ��
            for (int i = 0; i < 8; i++)
            {
                if (i < 2)
                {
                    m_ChildiconsRect[i] = new Rectangle(m_IconRect.X + 2, m_IconRect.Y + 2 + 91*i, 188, 89);
                }
                else
                {
                    if (i%2 == 0)
                    {
                        m_ChildiconsRect[i] = new Rectangle(m_IconRect.X + 2, m_IconRect.Y + 2 + 91*2 + (i - 2)/2*91, 93, 89);
                    }
                    else
                    {
                        m_ChildiconsRect[i] = new Rectangle(m_IconRect.X + 98, m_IconRect.Y + 2 + 91*2 + (i - 3)/2*91, 93, 89);
                    }
                }
            }

            //��״��Ϣ��ʾ����ı߿�λ�ó�ʼ��
            for (int i = 0; i < 6; i++)
            {
                if (i < 3)
                {
                    m_InfoRect[i] = new Rectangle(m_Rect.X + 95 + i*75, m_Rect.Y + 35, 40, 380);
                }
                else
                {
                    m_InfoRect[i] = new Rectangle(m_Rect.X + 380 + (i - 3)*75, m_Rect.Y + 35, 40, 380);
                }

            }

            //ת��ܵ��ı���ĳ�ʼ��
            for (int i = 0; i < 2; i++)
            {
                m_TxtTruckBolster[i] = new HxRectText();
                m_TxtTruckBolster[i].SetBkColor(0, 0, 0);
                m_TxtTruckBolster[i].SetTextRect(m_Rect.X + 60 + 280*i, m_Rect.Y + 420, 220, 30);
                m_TxtTruckBolster[i].SetDrawFrm(true);
                m_TxtTruckBolster[i].SetLinePen(84, 84, 84, 1);
                m_TxtTruckBolster[i].SetText("ת���" + (i + 1).ToString());
                m_TxtTruckBolster[i].SetTextStyle(12, FormatStyle.Center, true, "����");
                m_TxtTruckBolster[i].SetTextColor(255, 255, 255);
            }

            for (int i = 0; i < 2; i++)
            {
                m_TxtStatus[i] = new HxRectText();
                m_TxtStatus[i].SetBkColor(0, 0, 0);
                m_TxtStatus[i].SetDrawFrm(true);
                m_TxtStatus[i].SetTextColor(255, 255, 255);
                m_TxtStatus[i].SetTextStyle(14, FormatStyle.Center, true, "����");
                m_TxtStatus[i].SetTextRect(m_ChildiconsRect[i].X + 4, m_ChildiconsRect[i].Y + 45, 100, 35);
            }

            //����ͼ��ĳ�ʼ��
            for (int i = 0; i < 6; i++)
            {
                m_TMark[i] = new TriangleMark(m_InfoRect[i].X + 22, m_InfoRect[i].Bottom);
            }

        }

        /// <summary>
        /// ˢ������
        /// </summary>
        public void GetValue()
        {
            //���ñ���
            HxCommon.HTitle.SetText("ǣ������");

            HxCommon.ButtonText[0].SetText("��Ҫ����");
            HxCommon.ButtonText[1].SetText("");
            HxCommon.ButtonText[2].SetText("");
            HxCommon.ButtonText[3].SetText("");
            HxCommon.ButtonText[4].SetText("�г�����");
            HxCommon.ButtonText[5].SetText("��������");
            HxCommon.ButtonText[6].SetText("ǣ������");
            HxCommon.ButtonText[7].SetText("");
            HxCommon.ButtonText[8].SetText(" ");
            HxCommon.ButtonText[9].SetText("������");
            for (int i = 0; i < 10; i++)
            {
                if (i == 6)
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

            #region ��״̬��ϢBool ���Ķ���

            //��������
            for (int i = 0; i < 3; i++)
            {
                m_IsTrainDirection[i] = BoolList[UIObj.InBoolList[i]];
            }

            //����װ��
            for (int i = 0; i < 2; i++)
            {
                m_IsAlert[i] = BoolList[UIObj.InBoolList[i + 3]];
            }

            //�����ƶ�
            for (int i = 0; i < 4; i++)
            {
                m_IsTrainBrake[i] = BoolList[UIObj.InBoolList[i + 5]];
            }
            m_IsTrainBrake[4] = BoolList[UIObj.InBoolList[UIObj.InBoolList.Count - 1]];

            //ͣ���ƶ� 
            for (int i = 0; i < 4; i++)
            {
                m_IsStopBrake[i] = BoolList[UIObj.InBoolList[i + 9]];
            }

            //�ײ����ְ�ť
            for (int i = 0; i < 10; i++)
            {
                m_IsNumButtonDown[i] = BoolList[UIObj.InBoolList[i + 13]];
            }

            //�ܵ繭1
            for (int i = 0; i < 3; i++)
            {
                m_IsCurrentCollector1[i] = BoolList[UIObj.InBoolList[i + 23]];
            }

            //�ܵ繭2
            for (int i = 0; i < 3; i++)
            {
                m_IsCurrentCollector2[i] = BoolList[UIObj.InBoolList[i + 26]];
            }

            //���״̬
            for (int i = 0; i < 4; i++)
            {
                m_IsElectricMachine[i] = BoolList[UIObj.InBoolList[i + 29]];
            }

            #endregion

            #region;;;;;ǣ���� �ƶ��� ���� ����

            for (int i = 0; i < 12; i++)
            {
                if (i%2 == 0) //����Ŀ��ǣ����
                {
                    m_TargetTractionN[i/2] = Convert.ToInt32(FloatList[UIObj.InFloatList[i]]);

                    if (m_TargetTractionN[i/2] > 100) //�����ֵΪ ����100 ��С��0���쳣ֵʱ ʹ����� 0 100
                    {
                        m_TargetTractionN[i/2] = 100;
                    }

                    if (m_TargetTractionN[i/2] < 0)
                    {
                        m_TargetTractionN[i/2] = 0;
                    }
                }
                else //����ʵ���ƶ���
                {
                    m_TargetBrakeN[i/2] = Convert.ToInt32(FloatList[UIObj.InFloatList[i]]);

                    if (m_TargetBrakeN[i/2] > 100)
                    {
                        m_TargetBrakeN[i/2] = 100;
                    }

                    if (m_TargetBrakeN[i/2] < 0)
                    {
                        m_TargetBrakeN[i/2] = 0;
                    }
                }

                if (i%2 == 0) //����ʵ��ǣ����
                {
                    m_RealTractionN[i/2] = Convert.ToInt32(FloatList[UIObj.InFloatList[i + 12]]);

                    if (m_RealTractionN[i/2] > 100) //�����ֵΪ ����100 ��С��0���쳣ֵʱ ʹ����� 0 100
                    {
                        m_RealTractionN[i/2] = 100;
                    }

                    if (m_RealTractionN[i/2] < 0)
                    {
                        m_RealTractionN[i/2] = 0;
                    }
                }
                else //����ʵ���ƶ���
                {
                    m_RealBrakeN[i/2] = Convert.ToInt32(FloatList[UIObj.InFloatList[i + 12]]);

                    if (m_RealBrakeN[i/2] > 100) //�����ֵΪ ����100 ��С��0���쳣ֵʱ ʹ����� 0 100
                    {
                        m_RealBrakeN[i/2] = 100;
                    }

                    if (m_RealBrakeN[i/2] < 0)
                    {
                        m_RealBrakeN[i/2] = 0;
                    }
                }
            }

            #endregion

            //���� ǣ���� �ƶ�������
            for (int i = 0; i < 3; i++)
            {
                m_Value[i] = Convert.ToInt32(FloatList[UIObj.InFloatList[i + 24]]);
            }
            m_TxtStatus[0].SetText(m_Value[0].ToString());

            if (m_Value[1] > 0)
            {
                m_TxtStatus[1].SetTextColor(0, 0, 255);
                m_TxtStatus[1].SetText(m_Value[1].ToString());
            }
            else
            {
                m_TxtStatus[1].SetTextColor(255, 0, 0);
                m_TxtStatus[1].SetText(m_Value[2].ToString());
            }


            //���ݸ��µ�ǣ�����ƶ����ı�����ͼ��λ��
            for (int i = 0; i < 6; i++)
            {
                if (m_TargetTractionN[i] == 0 && m_TargetBrakeN[i] == 0)
                {
                    m_TMark[i].SetColor(TriangleMark.ColorType.Gray);
                    m_TMark[i].SetPosition(m_InfoRect[i].X + 22, m_InfoRect[i].Bottom);
                }
                else
                {
                    if (m_TargetTractionN[i] != 0) //ǣ������Ϊ0 ����Ϊ��ɫ
                    {
                        m_TMark[i].SetColor(TriangleMark.ColorType.Blue);
                        m_TMark[i].SetPosition(m_InfoRect[i].X + 22,
                            m_InfoRect[i].Bottom - m_TargetTractionN[i]*m_InfoRect[i].Height/108);
                    }
                    else
                    {
                        m_TMark[i].SetColor(TriangleMark.ColorType.Red);
                        m_TMark[i].SetPosition(m_InfoRect[i].X + 22,
                            m_InfoRect[i].Bottom - m_TargetBrakeN[i]*m_InfoRect[i].Height/108);
                    }
                }
            }
        }

        public void DrawOn(Graphics g)
        {
            g.DrawRectangle(HxCommon.LinePen1, m_Rect);
            g.DrawRectangle(HxCommon.LinePen1, m_IconRect);

            //������ͼ�����ı߿�
            for (int i = 0; i < 8; i++)
            {
                g.DrawRectangle(HxCommon.LinePen1, m_ChildiconsRect[i]);
            }

            for (int i = 0; i < 2; i++)
            {
                m_TxtTruckBolster[i].OnDraw(g);
            }

            #region �� �� �� �� ��

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    if (j%5 == 0)
                    {
                        g.DrawLine(HxCommon.WhitePen2, m_InfoRect[i].X - 15.5f,
                            m_InfoRect[i].Bottom - 0.1f - j*m_InfoRect[i].Height/27.0f,
                            m_InfoRect[i].X - 2.5f, m_InfoRect[i].Bottom - 0.1f - j*m_InfoRect[i].Height/27.0f);

                        if (i == 0 || i == 3)
                        {
                            if (j == 25)
                            {
                                g.DrawString((j*4).ToString(), HxCommon.Font14, HxCommon.WhiteBrush,
                                    m_InfoRect[i].X - 50,
                                    m_InfoRect[i].Bottom - 0.1f - j*m_InfoRect[i].Height/27.0f - 10);
                            }
                            else if (j == 0)
                            {
                                g.DrawString("0", HxCommon.Font14, HxCommon.WhiteBrush, m_InfoRect[i].X - 30,
                                    m_InfoRect[i].Bottom - 0.1f - j*m_InfoRect[i].Height/27.0f - 15);
                            }
                            else
                            {
                                g.DrawString((j*4).ToString(), HxCommon.Font14, HxCommon.WhiteBrush,
                                    m_InfoRect[i].X - 40,
                                    m_InfoRect[i].Bottom - 0.1f - j*m_InfoRect[i].Height/27.0f - 10);
                            }

                            g.DrawString("[%]", HxCommon.Font14, HxCommon.WhiteBrush, m_InfoRect[i].X - 45,
                                m_InfoRect[i].Y + 55);
                        }
                    }
                    else
                    {
                        g.DrawLine(HxCommon.WhitePen2, m_InfoRect[i].X - 10.5f,
                            m_InfoRect[i].Bottom - 0.1f - j*m_InfoRect[i].Height/27.0f,
                            m_InfoRect[i].X - 2.5f, m_InfoRect[i].Bottom - 0.1f - j*m_InfoRect[i].Height/27.0f);
                    }
                }

                g.DrawLine(HxCommon.LightWhitePen1, m_InfoRect[i].X - 2.5f, m_InfoRect[i].Bottom, m_InfoRect[i].X - 2.5f,
                    m_InfoRect[i].Y);
            }

            g.DrawString("ǣ��", HxCommon.Font14B, HxCommon.BlueBrush, m_InfoRect[2].X + 30, m_InfoRect[2].Y - 25);
            g.DrawString("/", HxCommon.Font14B, HxCommon.GrayBrush, m_InfoRect[2].X + 68, m_InfoRect[2].Y - 25);
            g.DrawString("�ƶ�", HxCommon.Font14B, HxCommon.RedBrush, m_InfoRect[2].X + 83, m_InfoRect[2].Y - 25);

            #endregion

            #region ״̬����״̬�Ļ���

            //�ٶ� �ƶ�ǣ��״̬�Ļ���
            for (int i = 0; i < 2; i++)
            {
                m_TxtStatus[i].OnDraw(g);
            }

            g.DrawString("�趨�ٶ�", HxCommon.Font14B, HxCommon.WhiteBrush, m_ChildiconsRect[0].X + 25,
                m_ChildiconsRect[0].Y + 15);
            g.DrawString("ǣ��", HxCommon.Font14B, HxCommon.BlueBrush, m_ChildiconsRect[1].X + 20,
                m_ChildiconsRect[1].Y + 15);
            g.DrawString("/", HxCommon.Font14B, HxCommon.GrayBrush, m_ChildiconsRect[1].X + 58,
                m_ChildiconsRect[1].Y + 15);
            g.DrawString("�ƶ�", HxCommon.Font14B, HxCommon.RedBrush, m_ChildiconsRect[1].X + 73,
                m_ChildiconsRect[1].Y + 15);
            g.DrawString("km/h", HxCommon.Font14B, HxCommon.WhiteBrush, m_ChildiconsRect[0].X + 108,
                m_ChildiconsRect[0].Y + 45);
            g.DrawString("kN", HxCommon.Font14B, HxCommon.WhiteBrush, m_ChildiconsRect[1].X + 108,
                m_ChildiconsRect[1].Y + 45);

            //���ƻ���������Ϣ
            for (int i = 0; i < 3; i++)
            {
                if (m_IsTrainDirection[i])
                {
                    g.DrawImage(m_ImageList[i], m_ChildiconsRect[2].X + 5, m_ChildiconsRect[2].Y + 5,
                        m_ChildiconsRect[2].Width - 10, m_ChildiconsRect[2].Height - 10);
                    break;
                }
                else
                {
                }
            }

            //���ƾ���װ��״̬��Ϣ
            for (int i = 0; i < 2; i++)
            {
                if (m_IsAlert[i])
                {
                    g.DrawImage(m_ImageList[i + 3], m_ChildiconsRect[4].X + 5, m_ChildiconsRect[4].Y + 5,
                        m_ChildiconsRect[4].Width - 10, m_ChildiconsRect[4].Height - 10);
                    break;
                }
                else
                {
                }
            }

            //���ƻ����ƶ�״̬
            for (int i = 0; i < 5; i++)
            {
                if (m_IsTrainBrake[i])
                {
                    if (i < 4)
                    {
                        g.DrawImage(m_ImageList[i + 5], m_ChildiconsRect[5].X + 5, m_ChildiconsRect[5].Y + 5,
                            m_ChildiconsRect[5].Width - 10, m_ChildiconsRect[5].Height - 10);
                        break;
                    }
                    else
                    {
                        g.DrawImage(m_ImageList[m_ImageList.Count - 1], m_ChildiconsRect[5].X + 5, m_ChildiconsRect[5].Y + 5,
                            m_ChildiconsRect[5].Width - 10, m_ChildiconsRect[5].Height - 10);
                    }

                }
                else
                {
                }
            }

            //���ƽ����ƶ�״̬
            for (int i = 0; i < 4; i++)
            {
                if (m_IsStopBrake[i])
                {
                    g.DrawImage(m_ImageList[i + 9], m_ChildiconsRect[6].X + 5, m_ChildiconsRect[6].Y + 5,
                        m_ChildiconsRect[6].Width - 10, m_ChildiconsRect[6].Height - 10);
                    break;
                }
                else
                {
                }
            }

            //�ܵ繭����
            for (int i = 0; i < 3; i++)
            {
                if (m_IsCurrentCollector1[0] && m_IsCurrentCollector2[0]) //�ܵ繭1 2����
                {
                    g.DrawImage(m_ImageList[13], m_ChildiconsRect[3].X + 5, m_ChildiconsRect[3].Y + 5,
                        m_ChildiconsRect[3].Width - 10, m_ChildiconsRect[3].Height - 40);
                    g.DrawString("1     2", HxCommon.Font12B, HxCommon.DeadBrush, m_ChildiconsRect[3].X + 10,
                        m_ChildiconsRect[3].Y + 60);
                }
                else if (m_IsCurrentCollector1[1] || m_IsCurrentCollector2[1]) //�й�����
                {
                    g.DrawImage(m_ImageList[14], m_ChildiconsRect[3].X + 5, m_ChildiconsRect[3].Y + 5,
                        m_ChildiconsRect[3].Width - 10, m_ChildiconsRect[3].Height - 40);

                    if (m_IsCurrentCollector1[1])
                    {
                        g.DrawString("1", HxCommon.Font12B, HxCommon.WhiteBrush, m_ChildiconsRect[3].X + 10,
                            m_ChildiconsRect[3].Y + 60);
                    }
                    else
                    {
                        g.DrawString("1", HxCommon.Font12B, HxCommon.DeadBrush, m_ChildiconsRect[3].X + 10,
                            m_ChildiconsRect[3].Y + 60);
                    }

                    if (m_IsCurrentCollector2[1])
                    {
                        g.DrawString("2", HxCommon.Font12B, HxCommon.WhiteBrush, m_ChildiconsRect[3].X + 70,
                            m_ChildiconsRect[3].Y + 60);
                    }
                    else
                    {
                        g.DrawString("2", HxCommon.Font12B, HxCommon.DeadBrush, m_ChildiconsRect[3].X + 70,
                            m_ChildiconsRect[3].Y + 60);
                    }
                }
            }

            //���Ƶ��״̬
            for (int i = 0; i < 4; i++)
            {
                if (m_IsElectricMachine[i])
                {
                    g.DrawImage(m_ImageList[i + 16], m_ChildiconsRect[7].X + 5, m_ChildiconsRect[7].Y + 5,
                        m_ChildiconsRect[7].Width - 10, m_ChildiconsRect[7].Height - 10);
                    break;
                }
            }

            #endregion

            //�ײ�������ť����
            for (int i = 0; i < 10; i++)
            {
                HxCommon.ButtonText[i].OnDraw(g);
            }

            //��ʾʵ��ǣ��/�ƶ���
            for (int i = 0; i < 6; i++)
            {
                if (m_RealTractionN[i] != 0)
                {
                    g.FillRectangle(HxCommon.BlueBrush, m_InfoRect[i].X + 1,
                        m_InfoRect[i].Bottom - m_RealTractionN[i]*m_InfoRect[i].Height/108,
                        m_InfoRect[i].Width - 2, m_RealTractionN[i]*m_InfoRect[i].Height/108);
                }
                else
                {
                    g.FillRectangle(HxCommon.RedBrush, m_InfoRect[i].X + 1,
                        m_InfoRect[i].Bottom - m_RealBrakeN[i]*m_InfoRect[i].Height/108,
                        m_InfoRect[i].Width - 2, m_RealBrakeN[i]*m_InfoRect[i].Height/108);
                }
            }

            //��������ͼ��
            for (int i = 0; i < 6; i++)
            {
                m_TMark[i].OnDraw(g);
            }

            //�����ƶ�/ǣ������״ͼ��
            for (int i = 0; i < 6; i++)
            {
                g.DrawRectangle(HxCommon.LightWhitePen1, m_InfoRect[i]);
            }
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
                        case 0: //����"1"����ת����Ҫ������ͼ
                            append_postCmd(CmdType.ChangePage, 2, 0, 0);
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4: //����5�л����г�������ͼ
                            append_postCmd(CmdType.ChangePage, 3, 0, 0);
                            break;
                        case 5: //����6�л����г����ý���
                            append_postCmd(CmdType.ChangePage, 16, 0, 0);
                            break;
                        case 6: //����7�л���ǣ�����ݽ���
                            append_postCmd(CmdType.ChangePage, 4, 0, 0);
                            break;
                        case 7:
                            break;
                        case 8:
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

    /*-----------------------------------------------------------------------------------
     * 
     *
     *����״ͼ�εĸ�����ͼ�� 
     * ����ͨ�����ø߶����������ʾ��ֵ
     * 
     *-----------------------------------------------------------------------------------
     */
}