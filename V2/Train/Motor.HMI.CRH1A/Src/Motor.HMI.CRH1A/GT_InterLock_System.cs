using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Motor.HMI.CRH1A.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using CommonUtil.Controls.Button;

namespace Motor.HMI.CRH1A
{


    /// <summary>
    ///   ���ػ���ҳ��ĵ�����ť
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class GT_InTerLock_Button : CRH1BaseClass
    {
        public Rectangle Recposition = new Rectangle(2, 513, 790, 300);
        public CRH1AButton GButton;


        public override string GetInfo()
        {
            return "���ػ���ҳ�水ť";
        }

        public override bool Initialize()
        {
            InitData();

            return true;
        }
        public override void paint(Graphics dcGs)
        {


            OnPaint((dcGs));
            base.paint(dcGs);
        }

        public void InitData()
        {

            GButton = new CRH1AButton();
            GButton.SetButtonRect(Recposition.X + 400, Recposition.Y, 90, 50);
            GButton.SetButtonText("����");
            GButton.SetButtonColor(192, 192, 192);

        }

        public void OnPaint(Graphics g)
        {

            //���Ʋ˵�����

            GButton.OnDraw(g);
        }

        protected override bool OnMouseDown(Point point)
        {
            OnButtonDown(point);
            return true;
        }
        protected override bool OnMouseUp(Point point)
        {
            OnButtonUp(point);
            return true;
        }

        public void OnButtonDown(Point nPoint)
        {

            if (GButton.Contains(nPoint))
                GButton.OnButtonDown();

        }
        public void OnButtonUp(Point nPoint)
        {
            if (GButton.Contains(nPoint))
            {
                OnPost(CmdType.ChangePage, 19, 0, 0);
                GButton.OnButtonUp();
            }
        }

    }

    [GksDataType(DataType.isMMIObjectClass)]
    class GT_InterLock_System : CRH1BaseClass
    {
        public GT_MenuTitle Title = new GT_MenuTitle("����");//�˵����� 
        public Rectangle Recposition = new Rectangle(2, 140, 790, 300);
        public Pen BackPen = new Pen(Color.FromArgb(163, 180, 198), 3);
        public SolidBrush BackBrush = new SolidBrush(Color.FromArgb(119, 136, 153));

        public CRH1AButton[] GButton = new CRH1AButton[3];
        public bool[] TractionStatus = new bool[21];
        public bool[] SpeekStatus = new bool[5];
        public bool[] BrakeStatus = new bool[9];
        public bool[] Status = new bool[3];
        public override string GetInfo()
        {
            return "����";
        }


        public override bool Initialize()
        {
            InitData();

            return true;
        }
        public override void paint(Graphics dcGs)
        {
            GetValue();
            RefreshData();
            OnPaint((dcGs));
            base.paint(dcGs);
        }
        public void GetValue()
        {
            if (UIObj.InBoolList.Count >= 34)
            {
                for (int i = 0; i < 21; i++)
                {
                    TractionStatus[i] = BoolList[UIObj.InBoolList[i]];
                }

                for (int i = 0; i < 5; i++)
                {
                    SpeekStatus[i] = BoolList[UIObj.InBoolList[i + 21]];
                }
                for (int i = 0; i < 9; i++)
                {
                    BrakeStatus[i] = BoolList[UIObj.InBoolList[i + 25]];
                }

                BrakeStatus[0] = BoolList[UIObj.InBoolList[10]];
            }

        }
        public void InitData()
        {
            for (int i = 0; i < 3; i++)
            {
                GButton[i] = new CRH1AButton();
                GButton[i].SetButtonRect(Recposition.X + 140 + i * 180, Recposition.Y + 100, 110, 60);

            }
            GButton[0].SetButtonText("ǣ������");
            GButton[1].SetButtonText("����");
            GButton[2].SetButtonText("�����ƶ�");

        }

        public void RefreshData()
        {
            foreach (bool b in TractionStatus)
            {
                if (b)
                {
                    Status[0] = true;
                    break;
                }
                Status[0] = false;
            }
            foreach (bool b in SpeekStatus)
            {
                if (b)
                {
                    Status[1] = true;
                    break;
                }
                Status[1] = false;

            }
            foreach (bool b in BrakeStatus)
            {
                if (b)
                {
                    Status[2] = true;
                    break;
                }
                Status[2] = false;
            }
        }
        public void OnPaint(Graphics g)
        {
            Title.OnDraw(g);
            //���Ʊ�����
            g.FillRectangle(BackBrush, Recposition);
            g.DrawRectangle(BackPen, Recposition);

            for (int i = 0; i < 3; i++)
            {
                if (Status[i])
                {
                    GButton[i].SetButtonColor(255, 0, 0);
                }
                else
                {
                    GButton[i].SetButtonColor(0, 128, 0);
                }
                GButton[i].OnDraw(g);
            }



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
            for (int i = 0; i < 3; i++)
            {
                if (GButton[i].Contains(x, y))
                    GButton[i].OnButtonDown();
            }
        }

        public void OnButtonUp(int x, int y)
        {
            for (int i = 0; i < 3; i++)
            {
                if (GButton[i].Contains(x, y))
                {
                    if (i == 0)
                    {
                        OnPost(CmdType.ChangePage, 20, 0, 0);
                    }
                    else if (i == 1)
                    {
                        OnPost(CmdType.ChangePage, 21, 0, 0);
                    }
                    else
                    {
                        if (i == 2)
                        {
                            OnPost(CmdType.ChangePage, 22, 0, 0);
                        }

                    }
                    GButton[i].OnButtonUp();
                }
            }
        }
    }



    /// <summary>
    ///   ǣ����ϻ����˵� ��ϸ��ʾ��Щ����ǣ��������������Ϣ
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class Tractionblock : CRH1BaseClass
    {

        public GT_MenuTitle Title = new GT_MenuTitle("ǣ������");//�˵�����
        public Rectangle Recposition = new Rectangle(2, 140, 790, 300);
        public Pen BackPen = new Pen(Color.FromArgb(163, 180, 198), 2);
        public SolidBrush BackBrush = new SolidBrush(Color.FromArgb(119, 136, 153));
        public SolidBrush RedBrush = new SolidBrush(Color.FromArgb(255, 0, 0));
        public SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 128, 0));
        public SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        public Pen BlackPen = new Pen(Color.FromArgb(0, 0, 0));
        public Rectangle[] StatusRect = new Rectangle[21];

        public string[] StrInfo = new string[21]{ "�����ƶ�","˾�������ƶ�","�Ż���","�����ֱ������ƶ�λ��","�����ֱ�����","����/ϴ��ģʽ���ٶȹ���","DSD��������ģʽ",
                                                 "����ģʽ����","�ƶ�����","�ٶȼ������","����ͣ����ť�Ѱ���","�˿ͽ����ƶ�������","ǣ�������Ѽ���","ǣ����ȫ��·��ʧЧ",
                                                 "ת����ϳ����ѱ���","ʵʩͣ���ƶ�","�ܵ繭��������","��ӵ�Դ����������","TC/CCU̫��","�������еĳ��Ŷ��رղ�����","ATP�������ƶ�" };


        public Font Strfont = new Font("Arial", 11);
        public bool[] Valueb = new bool[21];

        public override string GetInfo()
        {
            return "ǣ������";
        }


        public override bool Initialize()
        {
            InitData();
            return true;
        }
        public override void paint(Graphics dcGs)
        {
            GetValue();
            OnPaint((dcGs));
            base.paint(dcGs);
        }
        public void GetValue()
        {
            if (UIObj.InBoolList.Count >= 21)
            {
                for (int i = 0; i < 21; i++)
                {
                    Valueb[i] = BoolList[UIObj.InBoolList[i]];
                }
            }

        }
        public void InitData()
        {
            for (int i = 0; i < 21; i++)
            {
                if (i < 11)
                {
                    StatusRect[i] = new Rectangle(Recposition.X + 20, Recposition.Y + 25 * (i + 1), 35, 20);
                }
                else
                {
                    StatusRect[i] = new Rectangle(Recposition.X + 420, Recposition.Y + 25 * (i - 10), 35, 20);
                }
            }

        }
        public void OnPaint(Graphics g)
        {
            //���Ʋ˵�����
            Title.OnDraw(g);

            g.FillRectangle(BackBrush, Recposition);
            g.DrawRectangle(BlackPen, Recposition);
            for (int i = 0; i < 21; i++)
            {
                if (Valueb[i])
                {
                    g.FillRectangle(RedBrush, StatusRect[i]);
                }
                else
                {
                    g.FillRectangle(GreenBrush, StatusRect[i]);
                }
                g.DrawRectangle(BlackPen, StatusRect[i]);
                g.DrawString(StrInfo[i], Strfont, WhiteBrush, StatusRect[i].X + 40, StatusRect[i].Y + 3);
            }
            g.DrawString("ǣ������", new Font("Arial", 12), WhiteBrush, Recposition.X + 385, Recposition.Y + 6);
        }
    }



    /// <summary>
    ///   ��ϸ��ʾ��Щ��ʹ�ٶ����ƻ�����������Ϣ
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class GT_SpeedLimit : CRH1BaseClass
    {
        public GT_MenuTitle Title = new GT_MenuTitle("����");//�˵�����    
        public Rectangle Recposition = new Rectangle(2, 140, 790, 300);
        public Pen BackPen = new Pen(Color.FromArgb(163, 180, 198), 2);
        public SolidBrush BackBrush = new SolidBrush(Color.FromArgb(119, 136, 153));
        public SolidBrush RedBrush = new SolidBrush(Color.FromArgb(255, 0, 0));
        public SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 128, 0));
        public SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        public Pen BlackPen = new Pen(Color.FromArgb(0, 0, 0));
        public Rectangle[] StatusRect = new Rectangle[5];
        public string[] StrInfo = new string[5] { "����", "����", "ǣ��/�ƶ�������", "Ħ��/�ƶ�������", "ά����Ա��¼" };

        public Font Strfont = new Font("Arial", 11);
        public bool[] Valueb = new bool[5];

        public override string GetInfo()
        {
            return "����";
        }

        public override bool Initialize()
        {
            InitData();
            return true;
        }
        public override void paint(Graphics dcGs)
        {
            GetValue();
            OnPaint((dcGs));
            base.paint(dcGs);
        }
        public void GetValue()
        {
            if (UIObj.InBoolList.Count >= 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    Valueb[i] = BoolList[UIObj.InBoolList[i]];
                }
            }

        }
        public void InitData()
        {
            for (int i = 0; i < 5; i++)
            {
                StatusRect[i] = new Rectangle(Recposition.X + 280, Recposition.Y + 25 * i + 60, 35, 20);
            }
        }
        public void OnPaint(Graphics g)
        {
            //���Ʋ˵�����
            Title.OnDraw(g);

            g.FillRectangle(BackBrush, Recposition);
            g.DrawRectangle(BlackPen, Recposition);
            for (int i = 0; i < 5; i++)
            {
                if (Valueb[i])
                {
                    g.FillRectangle(RedBrush, StatusRect[i]);
                }
                else
                {
                    g.FillRectangle(GreenBrush, StatusRect[i]);
                }
                g.DrawRectangle(BlackPen, StatusRect[i]);
                g.DrawString(StrInfo[i], Strfont, WhiteBrush, StatusRect[i].X + 40, StatusRect[i].Y + 3);
            }
            g.DrawString("����", new Font("Arial", 12), WhiteBrush, Recposition.X + 385, Recposition.Y + 6);
        }
    }


    /// <summary>
    ///   ��ϸ��ʾ��������ƶ�������������Ϣ
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class GT_EmergencyBrake : CRH1BaseClass
    {
        public GT_MenuTitle Title = new GT_MenuTitle("�����ƶ�");//�˵�����
        public Rectangle Recposition = new Rectangle(2, 140, 790, 300);
        public Pen BackPen = new Pen(Color.FromArgb(163, 180, 198), 2);
        public SolidBrush BackBrush = new SolidBrush(Color.FromArgb(119, 136, 153));
        public SolidBrush RedBrush = new SolidBrush(Color.FromArgb(255, 0, 0));
        public SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 128, 0));
        public SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        public Pen BlackPen = new Pen(Color.FromArgb(0, 0, 0));
        public Rectangle[] StatusRect = new Rectangle[9];
        public string[] StrInfo = new string[9]{ "����ͣ����ť�Ѱ���", "��˾�������ֱ�����", "ATPϵͳ����", "��ȫ��·��Դ���ж�", "DSDϵͳ����" ,
                                                   "�����ƶ���·���","�����ѹ����","�ӿص�Ԫ��������ƶ�","�ƶ�ϵͳ����" };

        public Font Strfont = new Font("Arial", 11);
        public bool[] Valueb = new bool[9];

        public override string GetInfo()
        {
            return "�����ƶ�";
        }


        public override bool Initialize()
        {
            InitData();
            return true;
        }
        public override void paint(Graphics dcGs)
        {
            GetValue();
            OnPaint((dcGs));
            base.paint(dcGs);
        }
        public void GetValue()
        {
            if (UIObj.InBoolList.Count >= 9)
            {
                for (int i = 0; i < 9; i++)
                {
                    Valueb[i] = BoolList[UIObj.InBoolList[i]];
                }
            }

        }
        public void InitData()
        {
            for (int i = 0; i < 9; i++)
            {
                StatusRect[i] = new Rectangle(Recposition.X + 280, Recposition.Y + 25 * i + 60, 35, 20);
            }
        }
        public void OnPaint(Graphics g)
        {
            //���Ʋ˵�����
            Title.OnDraw(g);

            g.FillRectangle(BackBrush, Recposition);
            g.DrawRectangle(BlackPen, Recposition);
            for (int i = 0; i < 9; i++)
            {
                if (Valueb[i])
                {
                    g.FillRectangle(RedBrush, StatusRect[i]);
                }
                else
                {
                    g.FillRectangle(GreenBrush, StatusRect[i]);
                }
                g.DrawRectangle(BlackPen, StatusRect[i]);
                g.DrawString(StrInfo[i], Strfont, WhiteBrush, StatusRect[i].X + 90, StatusRect[i].Y + 3);
            }
            g.DrawString("�����ƶ�", new Font("Arial", 12), WhiteBrush, Recposition.X + 385, Recposition.Y + 6);
        }
    }


}
