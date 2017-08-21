using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;
using Motor.HMI.CRH1A.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using CommonUtil.Controls.Button;
using Motor.HMI.CRH1A.Common.Global;

namespace Motor.HMI.CRH1A
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    internal class GT_MainMenuButton : CRH1BaseClass
    {
        private readonly GT_MenuTitle m_Title = new GT_MenuTitle("���˵�"); //���ò˵�����
        public Rectangle m_Recposition = new Rectangle(0, 178, 70, 40);
        public Rectangle[] m_Rect = new Rectangle[4];
        //public int level;//�û�Ȩ�޵ȼ� 0Ϊ˾�� 1Ϊ�г�Ա 2Ϊά����Ա
        public CRH1AButton[] m_DiaButton = new CRH1AButton[3]; //���ϵͳ�Ĳ˵���ť
        public CRH1AButton[] m_InsButton = new CRH1AButton[5]; //�������Ĳ˵���ť
        public CRH1AButton[] m_MaiButton = new CRH1AButton[2]; //ά�����˵���ť
        public CRH1AButton[] m_TestButton = new CRH1AButton[3]; //�������˵���ť
        public CRH1AButton m_RunButton; //���г�վ��ť
        public Pen m_Whitepen = new Pen(Color.FromArgb(255, 255, 255), 3);
        public SolidBrush m_Brush = new SolidBrush(Color.FromArgb(119, 136, 153));

        public Font m_Strfont = new Font("Arial", 12);
        public SolidBrush m_Whitebrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        public float m_Valuef; //��ȡ�г��ٶ�


        public override string GetInfo()
        {
            return "���˵���ť��";
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
            if (UIObj.InFloatList.Count > 0)
            {
                m_Valuef = GlobalInfo.Instance.Crh1AConfig.AdaptConfig.CurrentSpeedCoefficient*FloatList[UIObj.InFloatList[0]];
                var isEnable = (Math.Abs(m_Valuef) <= float.Epsilon);
                if (GlobalInfo.Instance.ButtonEnable && !isEnable)
                {
                    foreach (var btn in m_TestButton)
                    {
                        btn.IsEnable = false;
                    }
                }
                else
                {
                    foreach (var btn in m_TestButton)
                    {
                        btn.IsEnable = true;
                    }
                }
            }
        }

        public void InitData()
        {
            //���ø���˵�����λ��
            m_Rect[0] = new Rectangle(m_Recposition.X, m_Recposition.Y, 175, 275);
            m_Rect[1] = new Rectangle(m_Recposition.X + 180, m_Recposition.Y, 245, 275);
            m_Rect[2] = new Rectangle(m_Recposition.X + 435, m_Recposition.Y, 175, 275);
            m_Rect[3] = new Rectangle(m_Recposition.X + 615, m_Recposition.Y, 175, 275);

            //////////////////����ÿ��˵����İ�ť��λ��/////////////////////////
            //��ʼ�����ϵͳ�ť
            for (int i = 0; i < 3; i++)
            {
                m_DiaButton[i] = new CRH1AButton();
                m_DiaButton[i].SetButtonRect(m_Rect[0].X + 45, m_Rect[0].Y + 60*i + 50, 85, 50);
                m_DiaButton[i].SetButtonColor(192, 192, 192);
            }
            m_DiaButton[0].SetButtonText("������¼");
            m_DiaButton[1].SetButtonText("�����ܿ�");
            m_DiaButton[2].SetButtonText("���ϱ���");

            //��ʼ���������İ�ť
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 2; j++)
                {
                    if ((i*2 + j) < 5)
                    {
                        m_InsButton[i*2 + j] = new CRH1AButton();
                        m_InsButton[i*2 + j].SetButtonRect(m_Rect[1].X + j*90 + 35, m_Rect[1].Y + i*60 + 50, 85, 50);
                        m_InsButton[i*2 + j].SetButtonColor(192, 192, 192);
                    }

                }
            m_InsButton[0].SetButtonText("ϵͳ");
            m_InsButton[1].SetButtonText("�г�״̬");
            m_InsButton[2].SetButtonText("����");
            m_InsButton[3].SetButtonText("ǣ��/�ƶ�");
            m_InsButton[4].SetButtonText("����");
            //��ʼ��ά����
            m_MaiButton[0] = new CRH1AButton();
            m_MaiButton[0].SetButtonRect(m_Rect[2].X + 45, m_Rect[2].Y + 50, 85, 50);
            m_MaiButton[0].SetButtonColor(192, 192, 192);
            m_MaiButton[0].SetButtonText("����");

            //��ʼ�����鰴ť��
            for (int i = 0; i < 3; i++)
            {
                m_TestButton[i] = new CRH1AButton();
                m_TestButton[i].SetButtonColor(192, 192, 192);
                m_TestButton[i].SetButtonRect(m_Rect[3].X + 45, m_Rect[3].Y + 60*i + 50, 85, 50);
            }
            m_TestButton[0].SetButtonText("�ƶ�����");
            m_TestButton[1].SetButtonText("�ֱ�����");
            m_TestButton[2].SetButtonText("�Ʋ���");
            m_RunButton = new CRH1AButton();
            m_RunButton.SetButtonRect(m_Recposition.X + 705, m_Recposition.Y + 330, 85, 50);
            m_RunButton.SetButtonText("����/��վ");
            m_RunButton.SetButtonColor(192, 192, 192);

            GlobalInfo.Instance.ButtonStatusChange += () =>
            {
                foreach (var t in m_DiaButton)
                {
                    t.IsEnable = GlobalInfo.Instance.ButtonEnable;
                }
                foreach (var t in m_InsButton)
                {
                    t.IsEnable = GlobalInfo.Instance.ButtonEnable;
                }
                foreach (var t in m_MaiButton.Where(w => w is CRH1AButton))
                {
                    t.IsEnable = GlobalInfo.Instance.ButtonEnable;
                }
                foreach (var t in m_TestButton)
                {
                    t.IsEnable = GlobalInfo.Instance.ButtonEnable;
                }
                m_RunButton.IsEnable = GlobalInfo.Instance.ButtonEnable;
            };
        }

        public void DrawOn(Graphics g)
        {
            //���Ʋ˵�����
            m_Title.OnDraw(g);
            for (int i = 0; i < 4; i++)
            {
                g.FillRectangle(m_Brush, m_Rect[i]);
                g.DrawRectangle(m_Whitepen, m_Rect[i]);
            }
            for (int i = 0; i < 3; i++)
            {
                m_DiaButton[i].OnDraw(g);
            }
            for (int i = 0; i < 5; i++)
            {
                m_InsButton[i].OnDraw(g);
            }
            m_MaiButton[0].OnDraw(g);
            for (int i = 0; i < 3; i++)
            {
                m_TestButton[i].OnDraw(g);
            }
            g.DrawString("���ϵͳ", m_Strfont, m_Whitebrush, m_Rect[0].X + 40, m_Rect[0].Y + 15);
            g.DrawString("����", m_Strfont, m_Whitebrush, m_Rect[1].X + 120, m_Rect[0].Y + 15);
            g.DrawString("ά��", m_Strfont, m_Whitebrush, m_Rect[2].X + 55, m_Rect[0].Y + 15);
            g.DrawString("����", m_Strfont, m_Whitebrush, m_Rect[3].X + 55, m_Rect[0].Y + 15);
            m_RunButton.OnDraw(g);
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
            ///////���ϵͳ��ť��Ӧ�¼� 
            foreach (var button in m_DiaButton.Where(w => w.Contains(x, y) && w.IsEnable))
            {
                button.OnButtonDown();
            }
            /////////��������ť��Ӧ�¼�
            foreach (var button in m_InsButton.Where(w => w.Contains(x, y) && w.IsEnable))
            {
                button.OnButtonDown();
            }
            ///////////��Ӧά������ť
            foreach (var button in m_MaiButton.Where(w => w != null && w.Contains(x, y) && w.IsEnable))
            {
                button.OnButtonDown();
            }
            /////////��Ӧ��������ť
            foreach (var button in m_TestButton.Where(w => w.Contains(x, y) && w.IsEnable))
            {
                button.OnButtonDown();
            }
            ////////��Ӧ���г�վ��ť
            if (m_RunButton.Contains(x, y) && m_RunButton.IsEnable)
            {
                m_RunButton.OnButtonDown();
            }
        }

        public void OnButtonUp(int x, int y)
        {
            ///////���ϵͳ��ť��Ӧ�¼� 
            for (int i = 0; i < 3; i++)
                if (m_DiaButton[i].Contains(x, y) && m_DiaButton[i].IsEnable)
                {
                    if (i == 0)
                    {
                        OnPost(CmdType.ChangePage, 23, 0, 0);
                    }
                    else if (i == 1)
                    {
                        OnPost(CmdType.ChangePage, 25, 0, 0);
                    }
                    else
                    {
                        OnPost(CmdType.ChangePage, 24, 0, 0);
                    }
                    m_DiaButton[i].OnButtonUp();

                }
            /////////��������ť��Ӧ�¼�
            for (int i = 0; i < 5; i++)
            {
                if (m_InsButton[i].Contains(x, y) && m_InsButton[i].IsEnable)
                {
                    if (i == 0)
                    {
                        OnPost(CmdType.ChangePage, 4, 0, 0);
                    }
                    else if (i == 1)
                    {
                        OnPost(CmdType.ChangePage, 6, 0, 0);
                    }
                    else if (i == 2)
                    {
                        OnPost(CmdType.ChangePage, 18, 0, 0);
                    }
                    else if (i == 3)
                    {
                        OnPost(CmdType.ChangePage, 29, 0, 0);
                    }
                    else if (i == 4)
                    {
                        OnPost(CmdType.ChangePage, 19, 0, 0);
                    }
                    else
                    {

                    }
                    m_InsButton[i].OnButtonUp();

                }
            }
            ///////////��Ӧά������ť
            for (int i = 0; i < 1; i++)
            {

                if (m_MaiButton[i].Contains(x, y) && m_MaiButton[i].IsEnable)
                {
                    if (i == 0)
                    {
                        OnPost(CmdType.ChangePage, 26, 0, 0);
                    }
                    else
                    {

                    }
                    m_MaiButton[i].OnButtonUp();

                }

            }
            /////////��Ӧ��������ť
            for (int i = 0; i < 3; i++)
            {

                if (m_TestButton[i].Contains(x, y)
                    && m_TestButton[i].IsEnable)
                {
                    if (i == 0)
                    {
                        OnPost(CmdType.ChangePage, 30, 0, 0); //�ƶ�����
                    }
                    else if (i == 1)
                    {
                        OnPost(CmdType.ChangePage, 31, 0, 0); //��ʻ���Ʋ���
                    }
                    else
                    {
                        OnPost(CmdType.ChangePage, 32, 0, 0); //���ƵƲ���
                    }
                    m_TestButton[i].OnButtonUp();

                }

            }
            ////////��Ӧ���г�վ��ť
            if (m_RunButton.Contains(x, y) && m_RunButton.IsEnable)
            {
                if (m_Valuef >= 3) //�ٶȸ���3kmʱ��ת�����н���
                    OnPost(CmdType.ChangePage, 3, 0, 0);
                else
                {
                    OnPost(CmdType.ChangePage, 5, 0, 0);
                }

                m_RunButton.OnButtonUp();
            }
        }
    }
}
