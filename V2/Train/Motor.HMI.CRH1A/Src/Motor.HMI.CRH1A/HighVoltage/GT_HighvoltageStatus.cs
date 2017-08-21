using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Windows.Forms;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Config;
using Motor.HMI.CRH1A.HighVoltage;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;


namespace Motor.HMI.CRH1A
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class GT_HighvoltageStatus : CRH1BaseClass
    {
        public GT_MenuTitle Title = new GT_MenuTitle("��ѹ");//�˵�����
        public Rectangle Recposition = new Rectangle(0, 170, 800, 100);
        public CRH1AButton[] GButton = new CRH1AButton[2];//�ײ���ť

        public Rectangle[] NoRect = new Rectangle[3];//��ʾ���
        public Rectangle[] GongRect = new Rectangle[2];//�ܵ繭λ��
        public Rectangle[] DuanRect = new Rectangle[5];//��·��λ��
        public GDIRectText[] GText = new GDIRectText[6];//��ʾ�����ѹ�͵���
        public Rectangle[] ImageRect = new Rectangle[3];//�ײ�ͼ��λ��
        public Rectangle[] StrRect = new Rectangle[6];
        Rectangle m_Rect;//ҳ������

        /// <summary>
        /// �����г�
        /// </summary>
        private CRH1AButton m_SwitchBtn;

        public SolidBrush Recbrush = new SolidBrush(Color.FromArgb(119, 136, 153));//������ˢ
        public SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 255, 0));//��ɫ��ˢ
        public SolidBrush RedBrush = new SolidBrush(Color.FromArgb(255, 0, 0));//��ɫ��ˢ
        public SolidBrush BlueBrush = new SolidBrush(Color.FromArgb(0, 0, 255));//��ɫ��ˢ
        public SolidBrush BlackBrush = new SolidBrush(Color.FromArgb(0, 0, 0));//��ɫ��ˢ 

        public Pen Rectpen = new Pen(Color.FromArgb(209, 226, 242), 3);//��������

        public Pen BlackPen = new Pen(Color.FromArgb(0, 0, 0), 2);
        public Pen LinePen = new Pen(Color.FromArgb(121, 121, 121), 2);

        public Font Strfont = new Font("Arial", 11);
        public SolidBrush NoBrush = new SolidBrush(Color.FromArgb(147, 147, 147));

        Image[] m_Image = new Image[19];

        /// <summary>
        /// ͼƬ����
        /// </summary>
        public ImageMgr ImageMgr { private set; get; }

        /// <summary>
        /// ��ѡ�е�����
        /// </summary>
        public SelectableRectangleControl SelectedControl
        {
            get { return m_SelectedControl; }
            set { m_SelectedControl = value; }
        }

        public Point[] Points = new Point[18];
        public bool[] Valueb = new bool[21];
        public float[] Valuef = new float[6];

        /// <summary>
        /// �ܵ繭 , 2 ��
        /// </summary>
        private AcceptEleArc[] m_AcceptEleArcs;

        /// <summary>
        /// �ϵ��� 5��
        /// </summary>
        private HighVoltageSwitch[] m_HighVoltageSwitches;

        /// <summary>
        /// ��ѡ�е�����
        /// </summary>
        private SelectableRectangleControl m_SelectedControl;

        /// <summary>
        /// ��ѡ�е�Ԫ��
        /// </summary>
        private int m_SelectedIndex = 0;

        /// <summary>
        /// ״̬����
        /// </summary>
        private readonly StateAdpt m_StateAdpt = new StateAdpt();

        public override string GetInfo()
        {
            return "��ѹ״̬";
        }

        #region ���ط���

        protected override void NavigateTo(ViewConfig to)
        {
            //if (nParaA == ParaADefine.SwitchFromOhter)
            {
                ReselectControl(null);
            }
        }

        public override bool Initialize()
        {
            //3
            InitData();

            if (UIObj.ParaList.Count >= 19)
            {
                for (int i = 0; i < 19; i++)
                {
                    m_Image[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
                }

            }

            ImageMgr = new ImageMgr(m_Image);


            return true;
        }

        public override void paint(Graphics dcGs)
        {
            GetValue();
            ReFreshData();
            DrawOn(dcGs);
            base.paint(dcGs);
        }
        #endregion

        public void GetValue()
        {
            if (UIObj.InBoolList.Count >= 21)
            {
                for (int i = 0; i < 21; i++)
                {
                    Valueb[i] = BoolList[UIObj.InBoolList[i]];
                }
            }
            if (UIObj.InFloatList.Count >= 6)
            {
                for (int i = 0; i < 6; i++)
                {
                    Valuef[i] = FloatList[UIObj.InFloatList[i]];
                }
            }

        }

        public void ReFreshData()
        {
            for (int i = 0; i < 6; i++)
            {
                //////////////////////////////////////////////
                //-ycl-
                //ȡ��
                ////////////////////////////////////////////
                GText[i].SetText(Convert.ToInt32(Valuef[i]).ToString());
            }

            foreach (var voltageSwitch in m_HighVoltageSwitches)
            {
                voltageSwitch.CurrentState = m_StateAdpt.GetHighVoltageSwitchState(voltageSwitch, Valueb);
            }

            foreach (var acceptEleArc in m_AcceptEleArcs)
            {
                acceptEleArc.CurrentState = m_StateAdpt.GetAcceptEleArcState(acceptEleArc, Valueb);
            }

        }

        public void InitData()
        {
            #region ;;;;;;;;;;;;;;;; ��ť��ʼ��:::::::::::::::::::::::
            GButton[0] = new CRH1AButton();
            GButton[0].SetButtonRect(Recposition.X + 10, Recposition.Y + 213, 90, 50);
            GButton[0].SetButtonColor(192, 192, 192);
            GButton[0].SetButtonText("������_3�г�");

            GButton[1] = new CRH1AButton();
            GButton[1].SetButtonRect(Recposition.X + 650, Recposition.Y + 213, 90, 50);
            GButton[1].SetButtonColor(192, 192, 192);
            GButton[1].SetButtonText("��ѹ�����л�");

            m_SwitchBtn = new CRH1AButton();
            m_SwitchBtn.SetButtonRect(Recposition.X + 350, Recposition.Y + 213, 90, 50);
            m_SwitchBtn.SetButtonColor(192, 192, 192);
            m_SwitchBtn.SetButtonText("����/�г�");

            #endregion

            #region :::::::::::::::::::�����ʾ��λ������;;;;;;;;;;;;;;;;;;;

            #endregion

            #region ::::::::::::::::::::ҳ �� �� Ϣ �� �� λ ��::::::::::::::::::::::::::::::::
            m_Rect = new Rectangle(Recposition.X, Recposition.Y + 203, 790, 80);

            #endregion
            #region :::::::::::::::::::�� �� �� ״ ̬ �� ʾ λ ��::::::::::::::::::::::::::::::::


            m_AcceptEleArcs = new AcceptEleArc[2]
            {
                new AcceptEleArc(new Rectangle(Recposition.X + 100, Recposition.Y - 20, 40, 40), AcceptEleArc.Id.No2, this), 
                new AcceptEleArc(new Rectangle(Recposition.X + 660, Recposition.Y - 20, 40, 40), AcceptEleArc.Id.No7, this)
            };

            foreach (var arc in m_AcceptEleArcs)
            {
                arc.SelectedHandler += (sender, args) => ReselectControl(sender as SelectableRectangleControl);
            }

            //Gong_Rect[0] = new Rectangle(recposition.X + 100, recposition.Y - 20, 40, 40);
            //Gong_Rect[1] = new Rectangle(recposition.X + 660, recposition.Y - 20, 40, 40);



            ImageRect[0] = new Rectangle(Recposition.X + 103, Recposition.Y + 150, 40, 35);
            ImageRect[1] = new Rectangle(Recposition.X + 383, Recposition.Y + 150, 40, 35);
            ImageRect[2] = new Rectangle(Recposition.X + 663, Recposition.Y + 150, 40, 35);
            #endregion

            #region ::::::::::::::::::::�� �� �� · �� ״ ̬ λ ��::::::::::::::::::::::::::::::::

            m_HighVoltageSwitches = new[]
            {
                new HighVoltageSwitch(this, 0, Orientation.Vertical,
                    new Rectangle(Recposition.X + 100, Recposition.Y + 70, 40, 40)),
                new HighVoltageSwitch(this, 1, Orientation.Horizontal,
                    new Rectangle(Recposition.X + 200, Recposition.Y + 10, 40, 40)),
                new HighVoltageSwitch(this, 2, Orientation.Vertical,
                    new Rectangle(Recposition.X + 380, Recposition.Y + 70, 40, 40)),
                new HighVoltageSwitch(this, 3, Orientation.Horizontal,
                    new Rectangle(Recposition.X + 560, Recposition.Y + 10, 40, 40)),
                new HighVoltageSwitch(this, 4, Orientation.Vertical,
                    new Rectangle(Recposition.X + 660, Recposition.Y + 70, 40, 40)),
            };
            foreach (var voltageSwitch in m_HighVoltageSwitches)
            {
                voltageSwitch.SelectedHandler += (sender, args) => ReselectControl(sender as SelectableRectangleControl);
            }

            //Duan_Rect[0] = new Rectangle(recposition.X + 100, recposition.Y + 70, 40, 40);
            //Duan_Rect[1] = new Rectangle(recposition.X + 200, recposition.Y + 10, 40, 40);
            //Duan_Rect[2] = new Rectangle(recposition.X + 380, recposition.Y + 70, 40, 40);
            //Duan_Rect[3] = new Rectangle(recposition.X + 560, recposition.Y + 10, 40, 40);
            //Duan_Rect[4] = new Rectangle(recposition.X + 660, recposition.Y + 70, 40, 40);

            #endregion

            #region :::::::::::::::::::::::::�� �� �� ѹ �� �� �� �� ʾ λ ��:::::::::::::::;;;;;:::::::
            for (int i = 0; i < 6; i++)
            {
                GText[i] = new GDIRectText();
                GText[i].SetBkColor(192, 192, 192);
                GText[i].SetLinePen(131, 131, 131, 1);
                GText[i].Isdrawrectfrm = true;
                GText[i].SetTextColor(0, 128, 0);
                GText[i].SetTextStyle(10, FormatStyle.Center, true, "Arial");
                if (i % 2 == 0)
                {
                    GText[i].SetTextRect(Recposition.X + 60 + i * 140, Recposition.Y + 130, 35, 20);
                    StrRect[i] = new Rectangle(Recposition.X + 98 + i * 140, Recposition.Y + 133, 30, 15);
                }
                else
                {
                    GText[i].SetTextRect(Recposition.X + 140 + (i - 1) * 140, Recposition.Y + 130, 35, 20);
                    StrRect[i] = new Rectangle(Recposition.X + 180 + (i - 1) * 140, Recposition.Y + 133, 30, 15);
                }
            }

            #endregion

            #region :::::::::::::::::::::::::�� �� �� �� �� �� �� ��:::::::::::::::;;;;;:::::::

            Points[0] = new Point(Recposition.X + 120, Recposition.Y + 20);
            Points[1] = new Point(Recposition.X + 120, Recposition.Y + 30);
            Points[2] = new Point(Recposition.X + 120, Recposition.Y + 70);
            Points[3] = new Point(Recposition.X + 120, Recposition.Y + 110);
            Points[4] = new Point(Recposition.X + 120, Recposition.Y + 153);
            Points[5] = new Point(Recposition.X + 200, Recposition.Y + 30);
            Points[6] = new Point(Recposition.X + 240, Recposition.Y + 30);
            Points[7] = new Point(Recposition.X + 400, Recposition.Y + 30);
            Points[8] = new Point(Recposition.X + 400, Recposition.Y + 70);
            Points[9] = new Point(Recposition.X + 400, Recposition.Y + 110);
            Points[10] = new Point(Recposition.X + 400, Recposition.Y + 152);
            Points[11] = new Point(Recposition.X + 560, Recposition.Y + 30);
            Points[12] = new Point(Recposition.X + 600, Recposition.Y + 30);
            Points[13] = new Point(Recposition.X + 680, Recposition.Y + 20);
            Points[14] = new Point(Recposition.X + 680, Recposition.Y + 30);
            Points[15] = new Point(Recposition.X + 680, Recposition.Y + 70);
            Points[16] = new Point(Recposition.X + 680, Recposition.Y + 110);
            Points[17] = new Point(Recposition.X + 680, Recposition.Y + 152);

            #endregion
        }

        private void ReselectControl(SelectableRectangleControl selectableRectangleControl)
        {

            if (m_SelectedControl != null)
            {
                m_SelectedControl.IsSelected = false;
            }

            if (selectableRectangleControl != null)
            {
                selectableRectangleControl.IsSelected = true;
                if (selectableRectangleControl == m_SelectedControl)
                {
                    selectableRectangleControl.IsSelected = false;
                    selectableRectangleControl = null;
                }
            }
            m_SelectedControl = selectableRectangleControl;
        }

        public void DrawOn(Graphics g)
        {

            //���Ʋ˵�����
            Title.OnDraw(g);

            g.FillRectangle(Recbrush, m_Rect);
            g.DrawRectangle(Rectpen, m_Rect);

            //���Ƶײ���ť

            for (int i = 0; i < 2; i++)
            {
                GButton[i].OnDraw(g);
            }

            if (m_SelectedControl != null)
            {
                m_SwitchBtn.OnDraw(g);
            }

            //�����ܵ繭

            foreach (var arc in m_AcceptEleArcs)
            {
                arc.OnDraw(g);
            }

            //���������·��
            foreach (var voltageSwitch in m_HighVoltageSwitches)
            {
                voltageSwitch.OnDraw(g);
            }

            //�����߶�
            g.DrawLine(LinePen, Points[0], Points[2]);
            g.DrawLine(LinePen, Points[1], Points[5]);
            g.DrawLine(LinePen, Points[3], Points[4]);
            g.DrawLine(LinePen, Points[6], Points[11]);
            g.DrawLine(LinePen, Points[7], Points[8]);
            g.DrawLine(LinePen, Points[9], Points[10]);
            g.DrawLine(LinePen, Points[12], Points[14]);
            g.DrawLine(LinePen, Points[13], Points[15]);
            g.DrawLine(LinePen, Points[16], Points[17]);

            //������ѹ����ʾ
            for (int i = 0; i < 6; i++)
            {
                GText[i].OnDraw(g);
                if (i % 2 == 0)
                {
                    g.DrawString("kV", Strfont, BlackBrush, StrRect[i]);
                }
                else
                {
                    g.DrawString("A", Strfont, BlackBrush, StrRect[i]);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                g.DrawImage(m_Image[18], ImageRect[i]);
            }
        }


        protected override bool OnMouseDown(Point point)
        {
            OnButtonDown(point.X, point.Y);
            //�ܵ繭��ѡ��
            if (m_AcceptEleArcs.Any(arc => arc.OnMouseDown(point)))
            {
                return true;
            }

            //�����·����ѡ���¼�
            if (m_HighVoltageSwitches.Any(voltageSwitch => voltageSwitch.OnMouseDown(point)))
            {
                return true;
            }
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
                if (GButton[i].Contains(x, y))
                {
                    if (i == 0)
                    {
                        OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[7], 1, 0);
                    }
                    else if (i == 1)
                    {
                        OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[m_SelectedIndex], 1, 0);
                    }
                    GButton[i].OnButtonDown();
                    return;
                }
            }

            if ( m_SelectedControl != null && m_SwitchBtn.Contains(x, y))
            {
                OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[HighVolOutBoolIdxAdpt.GetCutInOrOffBoolIdx(m_SelectedControl)], 1, 0);
                m_SwitchBtn.OnButtonDown();
            }
            
        }

        public void OnButtonUp(int x, int y)
        {
            for (int i = 0; i < 2; i++)
            {

                if (GButton[i].Contains(x, y))
                {
                    if (i == 0)
                    {
                        OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[7], 0, 0);
                    }
                    else if (i == 1)
                    {
                        OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[m_SelectedIndex], 0, 0);
                    }
                    GButton[i].OnButtonUp();
                    return;
                }
            }

            if (m_SelectedControl != null && m_SwitchBtn.Contains(x, y))
            {
                OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[HighVolOutBoolIdxAdpt.GetCutInOrOffBoolIdx(m_SelectedControl)], 0, 0);
                m_SwitchBtn.OnButtonUp();
            }

        }
    }
}

