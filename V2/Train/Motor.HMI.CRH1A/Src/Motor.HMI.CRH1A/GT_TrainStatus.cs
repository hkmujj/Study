using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Global;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;

namespace Motor.HMI.CRH1A
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    class GT_TrainStatus : CRH1BaseClass
    {
        private GT_MenuTitle m_Title = new GT_MenuTitle("�г�״̬");//�˵�����
        private Rectangle m_Recposition = new Rectangle(0, 178, 790, 277);
        private CRH1AButton[] m_GButton = new CRH1AButton[2];
        private Pen m_Pen = new Pen(Color.FromArgb(210, 210, 210));
        private Rectangle[] m_NoRect = new Rectangle[22];//��ʾ��ŵ�С���ο�
        private Rectangle[] m_StrRect = new Rectangle[22];//��ʾ״̬�ı���ľ��ο�
        private GDIRectText[] m_GText = new GDIRectText[22];//��ʾ״̬��Ϣ
        private SolidBrush m_Brush = new SolidBrush(Color.FromArgb(255, 255, 225));
        private SolidBrush m_Whitebrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        private SolidBrush m_Blackbrush = new SolidBrush(Color.FromArgb(0, 0, 0));
        private Pen m_RectPen = new Pen(Color.FromArgb(55, 55, 55), 2);
        private Font m_Font = new Font("Arial", 11);
        private string[] m_Strtitle ={ "Ԫ��/ϵͳ", "�����ƶ���·", "ͣ���ƶ�", "ǣ������", "ȫ���Źر�", "ǣ����", "�ƶ���", "ȫ������·��", "�Ӵ�������",
                                    "�����ѹ������", "���ַ�����������","Ԫ��/ϵͳ", "����", "����"," "," "," "," "," "," "," "," "};//��ʱ��12�� ���Ը�����Ҫ��Ӻ�ɾ��

        #region �ӿ�����
        private bool[] m_BoolValue = new bool[8];
        private float m_TrainSatusTraction;
        private float m_TrainStatusBrake;
        private float m_TrainStatusJieChuWangDianLiu;
        private float m_TrainStatusXianShu;

        #endregion
        public override string GetInfo()
        {
            return "�г�״̬";
        }

        public override bool Initialize()
        {
            //3
            InitData();

            return true;
        }
        public override void paint(Graphics g)
        {
            GetValue();
            DrawOn(g);
        }
        public void GetValue()
        {
            if (UIObj.InBoolList.Count >= 8)
            {
                for (int index = 0; index < 8; index++)
                {
                    m_BoolValue[index] = BoolList[UIObj.InBoolList[index]];
                }
            }

            m_TrainSatusTraction = FloatList[UIObj.InFloatList[0]];
            m_TrainStatusBrake = FloatList[UIObj.InFloatList[1]];
            m_TrainStatusJieChuWangDianLiu = FloatList[UIObj.InFloatList[2]];
            m_TrainStatusXianShu = FloatList[UIObj.InFloatList[3]]*GlobalInfo.Instance.Crh1AConfig.AdaptConfig.LimitSpeedCoefficient;
            GlobalParam.Instance.TrainInfo.MaxSpeed = m_TrainStatusXianShu;

        }
        public void InitData()
        {   ///////////�ײ���ť��ʼ��
            m_GButton[0] = new CRH1AButton();
            m_GButton[0].SetButtonRect(m_Recposition.X + 624, m_Recposition.Y + 335, 80, 50);
            m_GButton[0].SetButtonColor(192, 192, 192);
            m_GButton[0].SetButtonText("���˵�");
            m_GButton[1] = new CRH1AButton();
            m_GButton[1].SetButtonColor(192, 192, 192);
            m_GButton[1].SetButtonRect(m_Recposition.X + 710, m_Recposition.Y + 335, 80, 50);
            m_GButton[1].SetButtonText("����");

            #region::::::::::::��ʾ��ŵ�С���ο��ʼ��:::::::::::

            for (int i = 0; i < 22; i++)
            {
                if (i < 11)
                {
                    m_NoRect[i] = new Rectangle(m_Recposition.X, m_Recposition.Y + 25 * i, 30, 25);
                }
                else
                {
                    m_NoRect[i] = new Rectangle(m_Recposition.X + 400, m_Recposition.Y + 25 * (i - 11), 30, 25);
                }
            }
            #endregion

            #region::::::::::::��ʾ״̬�ı���ľ��ο�ĳ�ʼ��:::::::::::::

            for (int i = 0; i < 22; i++)
            {
                if (i < 11)
                {
                    m_StrRect[i] = new Rectangle(m_Recposition.X + 30, m_Recposition.Y + 25 * i, 250, 25);
                }
                else
                {
                    m_StrRect[i] = new Rectangle(m_Recposition.X + 430, m_Recposition.Y + 25 * (i - 11), 250, 25);
                }
            }
            #endregion

            #region ::;;;;;:::::::::�� ʾ ״  ̬ �� Ϣ;::;;;::;;;;;::::::::

            for (int i = 0; i < 20; i++)
            {
                m_GText[i] = new GDIRectText();
                m_GText[i].SetBkColor(255, 255, 255);
                m_GText[i].SetTextColor(0, 0, 0);
                m_GText[i].SetTextStyle(11, FormatStyle.Center, false, "Arial");
                if (i < 10)
                {
                    m_GText[i].SetTextRect(m_Recposition.X + 280, m_Recposition.Y + (i + 1) * 25, 120, 25);
                }
                else
                {
                    m_GText[i].SetTextRect(m_Recposition.X + 680, m_Recposition.Y + (i - 9) * 25, 120, 25);
                }

            }
            m_GText[20] = new GDIRectText();
            m_GText[20].SetBkColor(255, 255, 225);
            m_GText[20].SetTextColor(0, 0, 0);
            m_GText[20].SetTextStyle(11, FormatStyle.Center, false, "Arial");
            m_GText[20].SetTextRect(m_Recposition.X + 280, m_Recposition.Y, 120, 25);
            m_GText[20].SetText("״̬");

            m_GText[21] = new GDIRectText();
            m_GText[21].SetBkColor(255, 255, 225);
            m_GText[21].SetTextColor(0, 0, 0);
            m_GText[21].SetTextStyle(11, FormatStyle.Center, false, "Arial");
            m_GText[21].SetTextRect(m_Recposition.X + 680, m_Recposition.Y, 120, 25);
            m_GText[21].SetText("״̬");

            #endregion
        }
        public void DrawOn(Graphics g)
        {

            //���Ʋ˵�����
            m_Title.OnDraw(g);
            //���Ƶײ���ť
            for (int i = 0; i < 2; i++)
            {
                m_GButton[i].OnDraw(g);
            }
            //����״̬��Ϣ��ʾ�ľ��ο�
            g.DrawRectangle(m_RectPen, m_Recposition);

            //���Ʊ�ſ�������
            for (int i = 0; i < 22; i++)
            {
                g.FillRectangle(m_Brush, m_NoRect[i]);
                if (i > 0 && i < 11)
                    g.DrawString(i.ToString(), m_Font, m_Blackbrush, m_NoRect[i]);
                if (i > 11)
                {
                    g.DrawString((i - 1).ToString(), m_Font, m_Blackbrush, m_NoRect[i]);
                }
            }
            //����״̬��Ϣ����
            for (int i = 0; i < 22; i++)
            {
                if (i == 0 || i == 11)
                {
                    g.FillRectangle(m_Brush, m_StrRect[i]);
                    g.DrawString(m_Strtitle[i], m_Font, m_Blackbrush, m_StrRect[i]);
                }
                else
                {
                    g.FillRectangle(m_Whitebrush, m_StrRect[i]);
                    g.DrawString(m_Strtitle[i], m_Font, m_Blackbrush, m_StrRect[i]);
                }
            }

            //����״̬��Ϣ
            StatusSetting();//����״̬
            for (int i = 0; i < 22; i++)
            {
                m_GText[i].OnDraw(g);
            }

            //��������
            for (int i = 0; i <= 275; i += 25)
            {
                g.DrawLine(m_Pen, m_Recposition.X, m_Recposition.Y + i, m_Recposition.X + 800, m_Recposition.Y + i);
            }
            g.DrawLine(m_Pen, m_Recposition.X + 30, m_Recposition.Y, m_Recposition.X + 30, m_Recposition.Y + 275);
            g.DrawLine(m_Pen, m_Recposition.X + 280, m_Recposition.Y, m_Recposition.X + 280, m_Recposition.Y + 275);
            g.DrawLine(m_Pen, m_Recposition.X + 400, m_Recposition.Y, m_Recposition.X + 400, m_Recposition.Y + 275);
            g.DrawLine(m_Pen, m_Recposition.X + 430, m_Recposition.Y, m_Recposition.X + 430, m_Recposition.Y + 275);
            g.DrawLine(m_Pen, m_Recposition.X + 680, m_Recposition.Y, m_Recposition.X + 680, m_Recposition.Y + 275);


        }

        private void StatusSetting()
        {
            //�����ƶ���·
            if (m_BoolValue[0])
            {
                m_GText[0].SetText("��");
            }
            else
            {
                m_GText[0].SetText("�ر�");
            }

            //ͣ���ƶ�
            if (m_BoolValue[1])
            {
                m_GText[1].SetText("��");
            }
            else
            {
                m_GText[1].SetText("��");
            }

            //ǣ������
            if (m_BoolValue[2])
            {
                m_GText[2].SetText("��");
            }
            else
            {
                m_GText[2].SetText("��");
            }

            //ȫ���Źر�
            if (m_BoolValue[3])
            {
                m_GText[3].SetText("��");
            }
            else
            {
                m_GText[3].SetText("��");
            }

            // ǣ����
            m_GText[4].SetText(m_TrainSatusTraction.ToString("F0") + "        %");

            //�ƶ���
            m_GText[5].SetText(m_TrainStatusBrake.ToString("F0") + "        %");


            //ȫ������·��
            if (m_BoolValue[4])
            {
                m_GText[6].SetText("��");
            }
            else
            {
                m_GText[6].SetText("�ر�");
            }

            //�Ӵ�������
            m_GText[7].SetText(m_TrainStatusJieChuWangDianLiu.ToString() + "       A");

            //�����ѹ������
            if (m_BoolValue[5])
            {
                m_GText[8].SetText("��");
            }
            else
            {
                m_GText[8].SetText("��");
            }

            //���ַ�����������
            if (m_BoolValue[6])
            {
                m_GText[9].SetText("��");
            }
            else
            {
                m_GText[9].SetText("��");
            }

            //����/��Ԯ
            if (m_BoolValue[7])
            {
                m_GText[10].SetText("��");
            }
            else
            {
                m_GText[10].SetText("��");
            }

            //����
            //G_Text[11].SetText(TrainStatus_XianShu.ToString());
            //////////////////////////
            //-ycl-
            /////////////////////////
            m_GText[11].SetText(GlobalParam.Instance.TrainInfo.MaxSpeedText);
        }

        protected override bool OnMouseDown(Point point)
        {
            OnButtonDown(point.X, point.Y);
            return true;
        }
        protected override bool OnMouseUp(Point point)
        {
            OnButtonUp(point.X, point.Y);
            return true;
        }
        public void OnButtonDown(int x, int y)
        {
            //  �� ť �� Ӧ �� ��
            for (int i = 0; i < 2; i++)
            {
                if (m_GButton[i].Contains(x, y))
                {

                    m_GButton[i].OnButtonDown();

                }

            }

        }

        public void OnButtonUp(int x, int y)
        {
            for (int i = 0; i < 2; i++)
            {

                if (m_GButton[i].Contains(x, y))
                {
                    switch (i)
                    {
                        case 0:
                            OnPost(CmdType.ChangePage, 1, 0, 0);
                            break;
                        case 1:
                            OnPost(CmdType.ChangePage, 3, 0, 0);
                            break;


                    }
                    m_GButton[i].OnButtonUp();
                }
            }

        }




    }
}