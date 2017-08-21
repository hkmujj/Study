using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Config;
using Motor.HMI.CRH1A.Traction;
using Motor.HMI.CRH1A.Traction1;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;


namespace Motor.HMI.CRH1A
{
    [GksDataType(DataType.isMMIObjectClass)]
    class GT_TractionStatus : CRH1BaseClass
    {
        #region ˽���ֶ�
        public GT_MenuTitle Title = new GT_MenuTitle("ǣ��");//�˵����� 
        public Rectangle Recposition = new Rectangle(0, 100, 800, 100);
        //public CRH1AButton[] G_Button = new CRH1AButton[1];//�ײ���ť
        public GDIRectText[] GText = new GDIRectText[3];//��ʾ��ѹ
        public Rectangle[] Rect = new Rectangle[5];//ҳ��߿� 
        Rectangle m_Rect;//ҳ������

        public LCM[] Lcm = new LCM[5]; //LCM
        public ACM[] Acm = new ACM[5];//ACM
        public MCM1[] Mcm1 = new MCM1[5];//MCM1
        public MCM2[] Mcm2 = new MCM2[5];//MCM2

        public GDIRectText[] AvText = new GDIRectText[5];//�����غɵ�ѹ
        public GDIRectText[] DcvText = new GDIRectText[5];//DC���ڵ�ѹ
        public GDIRectText[] Mc1Text = new GDIRectText[5];//MCM1��ʵ�ֵ�ǣ��/�ƶ���
        public GDIRectText[] Mc2Text = new GDIRectText[5];//MCM2��ʵ�ֵ�ǣ��/�ƶ���

        public SolidBrush Recbrush = new SolidBrush(Color.FromArgb(119, 136, 153));//������ˢ
        public SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 255, 0));//��ɫ��ˢ
        public SolidBrush RedBrush = new SolidBrush(Color.FromArgb(255, 0, 0));//��ɫ��ˢ
        public SolidBrush BlueBrush = new SolidBrush(Color.FromArgb(0, 0, 255));//��ɫ��ˢ
        public SolidBrush BlackBrush = new SolidBrush(Color.FromArgb(0, 0, 0));//��ɫ��ˢ 
        public SolidBrush GrayBrush = new SolidBrush(Color.FromArgb(121, 121, 121));//��ɫ��ˢ 
        public Pen Rectpen = new Pen(Color.FromArgb(209, 226, 242), 3);//��������

        public Pen BlackPen = new Pen(Color.FromArgb(0, 0, 0), 2);
        public Pen LinePen = new Pen(Color.FromArgb(121, 121, 121), 3);//��������
        public Pen SLinePen = new Pen(Color.FromArgb(121, 121, 121), 1);//ϸ��������
        public Pen MLinePen = new Pen(Color.FromArgb(121, 121, 121), 2);//����������

        public Font Strfont = new Font("Arial", 11);
        public Rectangle[] Rectx = new Rectangle[2];
        public Point[] C1 = new Point[6];
        public Point[] C2 = new Point[6];
        public Point[] C3 = new Point[6];
        public Point[] C4 = new Point[6];
        public Point[] C5 = new Point[6];
        public bool[] Valueb = new bool[80];
        public float[] Valuef = new float[20];


        private TractionEquipMemtBase m_LastSelectedEquip;

        /// <summary>
        /// ҳ�ŵ� �����г�����
        /// </summary>
        private CRH1AButton m_SwithButton;


        #endregion


        #region �ӿ�����
        /// <summary>
        /// LCM״̬
        /// </summary>
        private bool[] m_IsLcmStatus = new bool[20];

        /// <summary>
        /// ACM״̬
        /// </summary>
        private bool[] m_IsAcmStatus = new bool[20];

        /// <summary>
        /// MCM1״̬
        /// </summary>
        private bool[] m_IsMcm1Status = new bool[20];

        /// <summary>
        /// MCM2״̬
        /// </summary>
        private bool[] m_IsMcm2Status = new bool[20];
        #endregion

        public override string GetInfo()
        {
            return "ǣ��";
        }



        public override bool Initialize()
        {
            //3
            InitData();


            return true;
        }
        public override void paint(Graphics dcGs)
        {
            GetValue();
            ReFreshData();
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        protected override void NavigateTo(ViewConfig to)
        {
            //if (ParaADefine.SwitchFromOhter == nParaA)
            {
                SelectEquipment(null);
            }
        }

        public void GetValue()
        {
            if (UIObj.InBoolList.Count >= 80)
            {
                //��ȡLCM״̬
                for (int index = 0; index < 20; index++)
                {
                    m_IsLcmStatus[index] = BoolList[UIObj.InBoolList[index]];
                    m_IsAcmStatus[index] = BoolList[UIObj.InBoolList[index + 20]];
                    m_IsMcm1Status[index] = BoolList[UIObj.InBoolList[index + 40]];
                    m_IsMcm2Status[index] = BoolList[UIObj.InBoolList[index + 60]];
                }
            }

            if (UIObj.InFloatList.Count >= 20)
            {
                for (int i = 0; i < 20; i++)
                {
                    Valuef[i] = FloatList[UIObj.InFloatList[i]];
                }
            }
        }

        /// <summary>
        /// ˢ������
        /// </summary>
        public void ReFreshData()
        {
            //LCM ״̬����
            for (int index = 0; index < 5; index++)
            {
                if (m_IsLcmStatus[index])
                {
                    Lcm[index].Status = EquipStatus.Active;
                }
                else if (m_IsLcmStatus[index + 5])
                {
                    Lcm[index].Status = EquipStatus.Well;
                }
                else if (m_IsLcmStatus[index + 10])
                {
                    Lcm[index].Status = EquipStatus.Fault;
                }
                else if (m_IsLcmStatus[index + 15])
                {
                    Lcm[index].Status = EquipStatus.CutOut;
                }
                else
                {
                    Lcm[index].Status = EquipStatus.Unknow;
                }
            }

            //ACM״̬����
            for (int index = 0; index < 5; index++)
            {
                if (m_IsAcmStatus[index])
                {
                    Acm[index].Status = EquipStatus.Active;
                }
                else if (m_IsAcmStatus[index + 5])
                {
                    Acm[index].Status = EquipStatus.Well;
                }
                else if (m_IsAcmStatus[index + 10])
                {
                    Acm[index].Status = EquipStatus.Fault;
                }
                else if (m_IsAcmStatus[index + 15])
                {
                    Acm[index].Status = EquipStatus.CutOut;
                }
                else
                {
                    Acm[index].Status = EquipStatus.Unknow;
                }
            }
            //MCM1״̬����
            for (int index = 0; index < 5; index++)
            {
                if (m_IsMcm1Status[index])
                {
                    Mcm1[index].Status = EquipStatus.Active;
                }
                else if (m_IsMcm1Status[index + 5])
                {
                    Mcm1[index].Status = EquipStatus.Well;
                }
                else if (m_IsMcm1Status[index + 10])
                {
                    Mcm1[index].Status = EquipStatus.Fault;
                }
                else if (m_IsMcm1Status[index + 15])
                {
                    Mcm1[index].Status = EquipStatus.CutOut;
                }
                else
                {
                    Mcm1[index].Status = EquipStatus.Unknow;
                }
            }

            //mcm2״̬����
            for (int index = 0; index < 5; index++)
            {
                if (m_IsMcm2Status[index + 0])
                {
                    Mcm2[index].Status = EquipStatus.Active;
                }
                else if (m_IsMcm2Status[index + 5])
                {
                    Mcm2[index].Status = EquipStatus.Well;
                }
                else if (m_IsMcm2Status[index + 10])
                {
                    Mcm2[index].Status = EquipStatus.Fault;
                }
                else if (m_IsMcm2Status[index + 15])
                {
                    Mcm2[index].Status = EquipStatus.CutOut;
                }
                else
                {
                    Mcm2[index].Status = EquipStatus.Unknow;
                }
            }


            for (int i = 0; i < 5; i++)
            {
                AvText[i].SetText(Convert.ToInt32(Valuef[i]).ToString());  //�����غɵ�ѹ����
                DcvText[i].SetText(Convert.ToInt32(Valuef[i + 5]).ToString());//DVC���ڵ�ѹ
                Mc1Text[i].SetText(Convert.ToInt32(Valuef[i + 10]).ToString());//MCM1�ƶ�/ǣ����
                Mc2Text[i].SetText(Convert.ToInt32(Valuef[i + 15]).ToString());//MCM2�ƶ�/ǣ����
            }
        }


        public void InitData()
        {
            m_SwithButton = new CRH1AButton();
            m_LastSelectedEquip = null;
            //#region ;;;;;;;;;;;;;;;; ��ť��ʼ��:::::::::::::::::::::::
            //G_Button[0] = new CRH1AButton();
            //G_Button[0].SetButtonRect(recposition.X + 390, recposition.Y + 285, 90, 50);
            //G_Button[0].SetButtonColor(192, 192, 192);
            //G_Button[0].SetButtonText("��ͨ/�ж�");
            //#endregion

            #region :::::::::::::::::::�� �� ��λ ��;;;;;;;;;;;;;;;;;;
            for (int i = 0; i < 5; i++)
            {
                Rect[i] = new Rectangle(Recposition.X + i * 150 + 20, Recposition.Y, 140, 260);
            }
            #endregion

            #region :::::::::::::::::::������λ�ü���ʼ��;;;;;;;;;;;;;;;;;;;

            for (int i = 0; i < 5; i++)
            {
                Lcm[i] = new LCM(Rect[i].X + 50, Rect[i].Y + 20, 40, 40) { TrainNumber = i };
            }
            #endregion

            #region ::::::::::::::::::::ҳ �� �� Ϣ �� �� λ ��::::::::::::::::::::::::::::::::
            m_Rect = new Rectangle(Recposition.X, Recposition.Y + 270, 790, 80);


            const int BTN_WIDTH = 90;
            const int BTN_HEIGHT = 55;
            var btnStartX = m_Rect.Width/2 - BTN_WIDTH/2 + m_Rect.X;
            var btnStartY = m_Rect.Y + 10;
            m_SwithButton.SetButtonRect(btnStartX, btnStartY, BTN_WIDTH, BTN_HEIGHT);
            m_SwithButton.SetButtonColor(192, 192, 192);
            m_SwithButton.SetButtonText("����/�г�");

            #endregion
            #region :::::::::::::::::::A C M �� ʾ λ �� �� �� ʼ �� λ ��::::::::::::::::::::::::::::::::
            for (int i = 0; i < 5; i++)
            {
                Acm[i] = new ACM(Rect[i].X + 10, Rect[i].Y + 70, 40, 40)  { TrainNumber = i};
            }

            #endregion

            #region ::::::::::::::::::::MCM1 MCM2 λ �� �� �� ʼ ��::::::::::::::::::::::::::::::::
            for (int i = 0; i < 5; i++)
            {
                if (i == 0 || i == 3)
                {
                    Mcm1[i] = new MCM1(Rect[i].X + 10, Rect[i].Y + 150, 40, 40) { TrainNumber = i};
                    Mcm2[i] = new MCM2(Rect[i].X + 95, Rect[i].Y + 150, 40, 40) { TrainNumber = i};
                }
                else
                {
                    Mcm1[i] = new MCM1(Rect[i].X + 95, Rect[i].Y + 150, 40, 40) { TrainNumber = i };
                    Mcm2[i] = new MCM2(Rect[i].X + 10, Rect[i].Y + 150, 40, 40) { TrainNumber = i };
                }
            }
            #endregion
            #region :::::::::::::::::::::::::�� �� �� �� �� ʾ λ ��:::::::::::::::;;;;;:::::::
            for (int i = 0; i < 5; i++)
            {
                //�����غɵ�ѹ
                AvText[i] = new GDIRectText();
                AvText[i].SetBkColor(192, 192, 192);
                AvText[i].SetLinePen(131, 131, 131, 1);
                AvText[i].Isdrawrectfrm = true;
                AvText[i].SetTextColor(0, 128, 0);
                AvText[i].SetTextStyle(10, FormatStyle.Center, true, "Arial");
                AvText[i].SetTextRect(Rect[i].X + 10, Rect[i].Y + 115, 40, 20);

                //DC���ڵ�ѹ
                DcvText[i] = new GDIRectText();
                DcvText[i].SetBkColor(192, 192, 192);
                DcvText[i].SetLinePen(131, 131, 131, 1);
                DcvText[i].Isdrawrectfrm = true;
                DcvText[i].SetTextColor(0, 128, 0);
                DcvText[i].SetTextStyle(10, FormatStyle.Center, true, "Arial");
                DcvText[i].SetTextRect(Rect[i].X + 80, Rect[i].Y + 90, 40, 20);

                //MC1ǣ��/�ƶ���
                Mc1Text[i] = new GDIRectText();
                Mc1Text[i].SetBkColor(192, 192, 192);
                Mc1Text[i].SetLinePen(131, 131, 131, 1);
                Mc1Text[i].Isdrawrectfrm = true;
                Mc1Text[i].SetTextColor(0, 128, 0);
                Mc1Text[i].SetTextStyle(10, FormatStyle.Center, true, "Arial");
                Mc1Text[i].SetTextRect(Rect[i].X + 10, Rect[i].Y + 195, 40, 20);

                //MC2ǣ��/�ƶ���
                Mc2Text[i] = new GDIRectText();
                Mc2Text[i].SetBkColor(192, 192, 192);
                Mc2Text[i].SetLinePen(131, 131, 131, 1);
                Mc2Text[i].Isdrawrectfrm = true;
                Mc2Text[i].SetTextColor(0, 128, 0);
                Mc2Text[i].SetTextStyle(10, FormatStyle.Center, true, "Arial");
                Mc2Text[i].SetTextRect(Rect[i].X + 95, Rect[i].Y + 195, 40, 20);



            }

            #endregion

            #region :::::::::::::::::::::::::�� �� �� �� �� �� �� ��:::::::::::::::;;;;;:::::::
            //C1
            C1[0] = new Point(Rect[0].X + 70, Rect[0].Y + 60);
            C1[1] = new Point(Rect[0].X + 70, Rect[0].Y + 170);
            C1[2] = new Point(Rect[0].X + 50, Rect[0].Y + 90);
            C1[3] = new Point(Rect[0].X + 70, Rect[0].Y + 90);
            C1[4] = new Point(Rect[0].X + 50, Rect[0].Y + 170);
            C1[5] = new Point(Rect[0].X + 95, Rect[0].Y + 170);

            //C2
            C2[0] = new Point(Rect[1].X + 70, Rect[1].Y + 60);
            C2[1] = new Point(Rect[1].X + 70, Rect[1].Y + 170);
            C2[2] = new Point(Rect[1].X + 50, Rect[1].Y + 90);
            C2[3] = new Point(Rect[1].X + 70, Rect[1].Y + 90);
            C2[4] = new Point(Rect[1].X + 50, Rect[1].Y + 170);
            C2[5] = new Point(Rect[1].X + 95, Rect[1].Y + 170);

            //C3
            C3[0] = new Point(Rect[2].X + 70, Rect[2].Y + 60);
            C3[1] = new Point(Rect[2].X + 70, Rect[2].Y + 170);
            C3[2] = new Point(Rect[2].X + 50, Rect[2].Y + 90);
            C3[3] = new Point(Rect[2].X + 70, Rect[2].Y + 90);
            C3[4] = new Point(Rect[2].X + 50, Rect[2].Y + 170);
            C3[5] = new Point(Rect[2].X + 95, Rect[2].Y + 170);

            //C4
            C4[0] = new Point(Rect[3].X + 70, Rect[3].Y + 60);
            C4[1] = new Point(Rect[3].X + 70, Rect[3].Y + 170);
            C4[2] = new Point(Rect[3].X + 50, Rect[3].Y + 90);
            C4[3] = new Point(Rect[3].X + 70, Rect[3].Y + 90);
            C4[4] = new Point(Rect[3].X + 50, Rect[3].Y + 170);
            C4[5] = new Point(Rect[3].X + 95, Rect[3].Y + 170);

            //C5
            C5[0] = new Point(Rect[4].X + 70, Rect[4].Y + 60);
            C5[1] = new Point(Rect[4].X + 70, Rect[4].Y + 170);
            C5[2] = new Point(Rect[4].X + 50, Rect[4].Y + 90);
            C5[3] = new Point(Rect[4].X + 70, Rect[4].Y + 90);
            C5[4] = new Point(Rect[4].X + 50, Rect[4].Y + 170);
            C5[5] = new Point(Rect[4].X + 95, Rect[4].Y + 170);


            #endregion
        }
        public void DrawOn(Graphics g)
        {
            //���Ʋ˵�����
            Title.OnDraw(g);

            //���Ʊ߿�
            for (int i = 0; i < 5; i++)
            {
                g.DrawRectangle(MLinePen, Rect[i]);
            }

            g.DrawString("01", new Font("Arial", 13), GrayBrush, Rect[0].X + 100, Rect[0].Y + 25);
            g.DrawString("03", new Font("Arial", 13), GrayBrush, Rect[1].X + 100, Rect[0].Y + 25);
            g.DrawString("04", new Font("Arial", 13), GrayBrush, Rect[2].X + 100, Rect[0].Y + 25);
            g.DrawString("06", new Font("Arial", 13), GrayBrush, Rect[3].X + 100, Rect[0].Y + 25);
            g.DrawString("08", new Font("Arial", 13), GrayBrush, Rect[4].X + 100, Rect[0].Y + 25);

            //���Ƶײ���ť

            for (int i = 0; i < 1; i++)
            {
                // G_Button[i].OnDraw(g);
            }

            //���Ʊ����� ACM MCM1 MCM 2
            for (int i = 0; i < 5; i++)
            {
                Lcm[i].OnDraw(g);
                Acm[i].OnDraw(g);
                Mcm1[i].OnDraw(g);
                Mcm2[i].OnDraw(g);
            }

            //���ı���Ļ���

            for (int i = 0; i < 5; i++)
            {
                AvText[i].OnDraw(g);
                g.DrawString("V", Strfont, BlackBrush, AvText[i].OutLineRectangle.X + 42, AvText[i].OutLineRectangle.Y + 2);

                DcvText[i].OnDraw(g);
                g.DrawString("V", Strfont, BlackBrush, DcvText[i].OutLineRectangle.X + 42, DcvText[i].OutLineRectangle.Y + 2);

                Mc1Text[i].OnDraw(g);
                g.DrawString("kN", Strfont, BlackBrush, Mc1Text[i].OutLineRectangle.X + 50, Mc1Text[i].OutLineRectangle.Y + 2);
                Mc2Text[i].OnDraw(g);
            }

            //��������
            g.DrawLine(LinePen, C1[0], C1[1]);
            g.DrawLine(LinePen, C1[2], C1[3]);
            g.DrawLine(LinePen, C1[4], C1[5]);

            g.DrawLine(LinePen, C2[0], C2[1]);
            g.DrawLine(LinePen, C2[2], C2[3]);
            g.DrawLine(LinePen, C2[4], C2[5]);

            g.DrawLine(LinePen, C3[0], C3[1]);
            g.DrawLine(LinePen, C3[2], C3[3]);
            g.DrawLine(LinePen, C3[4], C3[5]);

            g.DrawLine(LinePen, C4[0], C4[1]);
            g.DrawLine(LinePen, C4[2], C4[3]);
            g.DrawLine(LinePen, C4[4], C4[5]);

            g.DrawLine(LinePen, C5[0], C5[1]);
            g.DrawLine(LinePen, C5[2], C5[3]);
            g.DrawLine(LinePen, C5[4], C5[5]);

            //ҳ���������
            g.FillRectangle(Recbrush, m_Rect);
            g.DrawRectangle(Rectpen, m_Rect);
            if (m_LastSelectedEquip != null)
            {
                m_SwithButton.OnDraw(g);
            }

        }

        protected override bool OnMouseDown(Point point)
        {
            OnTractionEquipMementDown(point);
            OnButtonDown(point);
            return true;
        }

        private void OnTractionEquipMementDown(Point nPoint)
        {
            foreach (var l in Lcm)
            {
                if (l.OnMouseDown(nPoint))
                {
                    SelectEquipment(l);
                    return;
                }
            }

            foreach (var a in Acm)
            {
                if (a.OnMouseDown(nPoint))
                {
                    SelectEquipment(a);
                    return;
                }
            }

            foreach (var m in Mcm1)
            {
                if (m.OnMouseDown(nPoint))
                {
                    SelectEquipment(m);
                    return;
                }
            }

            foreach (var m in Mcm2)
            {
                if (m.OnMouseDown(nPoint))
                {
                    SelectEquipment(m);
                    return;
                }
            }
        }

        private void SelectEquipment(TractionEquipMemtBase equipMemt)
        {
            if (m_LastSelectedEquip != null)
            {
                m_LastSelectedEquip.IsSelected = false;
            }

            if (equipMemt != null)
            {
                equipMemt.IsSelected = true;
                if (equipMemt == m_LastSelectedEquip)
                {
                    equipMemt.IsSelected = false;
                    equipMemt = null;
                }
            }
            m_LastSelectedEquip = equipMemt;
        }


        protected override bool OnMouseUp(Point point)
        {
            OnButtonUp(point);
            return true;
        }

        public void OnButtonDown(Point point)
        {
            if (m_SwithButton.Contains(point) && m_LastSelectedEquip != null)
            {
                m_SwithButton.OnButtonDown();
                OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[TractionOutBoolAdpt.GetOutBoolIdx(m_LastSelectedEquip)], 1, 0);
            }
        }

        public void OnButtonUp(Point point)
        {
            #region ��ť�����¼�

            if (m_SwithButton.Contains(point) && m_LastSelectedEquip != null)
            {
                m_SwithButton.OnButtonUp();
                OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[TractionOutBoolAdpt.GetOutBoolIdx(m_LastSelectedEquip)], 0, 0);
            }

            #endregion
        }

    }
}
