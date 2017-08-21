using System.Drawing;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Config;
using Motor.HMI.CRH1A.Door;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using CommonUtil.Controls.Button;


namespace Motor.HMI.CRH1A
{
    [GksDataType(DataType.isMMIObjectClass)]
    class GT_DoorStatus : CRH1BaseClass
    {
        public GT_MenuTitle Title = new GT_MenuTitle("����");//�˵�����       
        public Rectangle Recposition = new Rectangle(0, 170, 800, 100);
        public Rectangle[] TrainRect = new Rectangle[8];//��ʾ������ı߿�
        public Rectangle[] NoRect = new Rectangle[8];//�������ʾλ��    
        public int Weight = 65;//���ӵĿ��
        public int Height = 45;//���ӵĸ߶�
        Rectangle m_Rect;//ҳ������

        public SolidBrush Recbrush = new SolidBrush(Color.FromArgb(119, 136, 153));//������ˢ

        public Pen Rectpen = new Pen(Color.FromArgb(209, 226, 242), 3);//��������
        public Pen TrainPen = new Pen(Color.FromArgb(85, 85, 85), 2);

        public Font Strfont = new Font("Arial", 11);
        public SolidBrush Whitebrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        public Point[] Mc1 = new Point[6];//��˾���ҵ�����
        public Point[] Mc2 = new Point[6];//��˾���ҵ�����
        public bool[] Valueb = new bool[80];

        private DoorMgr m_DoorMgr;

        private CRH1AButton m_SwithInOutBtn;

        public GT_DoorStatus()
        {
         
            m_DoorMgr = new DoorMgr(this);
        }

        public override string GetInfo()
        {
            return "��ϵͳ״̬";
        }

        protected override void NavigateTo(ViewConfig to)
        {
            //if (nParaA == ParaADefine.SwitchFromOhter)
            {
                m_DoorMgr.SelectDoorInfo = null;
            }
        }

        public override bool Initialize()
        {
            //3
            InitData();

            m_DoorMgr.Init();


            return true;
        }
        public override void paint(Graphics dcGs)
        {
            ReFreshData();
            DrawOn(dcGs);

            //����
            m_DoorMgr.OnPaint(dcGs);

            base.paint(dcGs);
        }

        protected override bool OnMouseDown(Point point)
        {
            if (!PressDoor(point))
            {
                PressSwithBtn(point);
            }
            return true;
        }

        private bool PressSwithBtn(Point nPoint)
        {
            // �����л�����
            if (m_SwithInOutBtn.Contains(nPoint))
            {
                m_SwithInOutBtn.OnButtonDown();
                OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[(int)DoorOutBoolDefineHelper.GetOutBoolDefine(m_DoorMgr.SelectDoorInfo)], 1, 0);
                return true;
            }

            return false;
        }

        private bool PressDoor(Point nPoint)
        {
            return m_DoorMgr.OnMouseDown(nPoint);
        }

        protected override bool OnMouseUp(Point point)
        {
            ClickSwithBtn(point);

            return true;
        }

        private void ClickSwithBtn(Point nPoint)
        {
            if (m_SwithInOutBtn.Contains(nPoint))
            {
                m_SwithInOutBtn.OnButtonUp();
                OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[(int)DoorOutBoolDefineHelper.GetOutBoolDefine(m_DoorMgr.SelectDoorInfo)], 0, 0);
            }
        }

        public void ReFreshData()
        {

        }
        public void InitData()
        {
            m_SwithInOutBtn = new CRH1AButton();
            #region::::::::::::;;;;;;;;�� �� �� �� λ �� �� ʼ ��;;;;;;;;;;;;;;;;;;;;;;;;;;;
            //��˾����
            Mc1[0] = new Point(Recposition.X + 55, Recposition.Y + 50);
            Mc1[1] = new Point(Recposition.X + 115, Recposition.Y + 50);
            Mc1[2] = new Point(Recposition.X + 115, Recposition.Y + 110);
            Mc1[3] = new Point(Recposition.X + 55, Recposition.Y + 110);
            Mc1[4] = new Point(Recposition.X + 30, Recposition.Y + 90);
            Mc1[5] = new Point(Recposition.X + 30, Recposition.Y + 70);

            //��˾����
            Mc2[0] = new Point(Recposition.X + 670, Recposition.Y + 50);
            Mc2[1] = new Point(Recposition.X + 730, Recposition.Y + 50);
            Mc2[2] = new Point(Recposition.X + 755, Recposition.Y + 70);
            Mc2[3] = new Point(Recposition.X + 755, Recposition.Y + 90);
            Mc2[4] = new Point(Recposition.X + 730, Recposition.Y + 110);
            Mc2[5] = new Point(Recposition.X + 670, Recposition.Y + 110);
            for (int i = 0; i < 6; i++)
            {
                TrainRect[i] = new Rectangle(Recposition.X + i * 90 + 125, Recposition.Y + 50, 85, 60);
            }
            #endregion


            #region :::::::::::::::::::�������ʾ��λ������;;;;;;;;;;;;;;;;;;;
            for (int i = 0; i < 8; i++)
            {

                NoRect[i] = new Rectangle(Recposition.X + i * 90 + 70, Recposition.Y + 60, 30, 25);
            }
            #endregion

            #region ::::::::::::::::::::ҳ �� �� Ϣ �� �� λ ��::::::::::::::::::::::::::::::::
            m_Rect = new Rectangle(Recposition.X, Recposition.Y + 203, 790, 80);

            #endregion

            #region :::::::::::::::::::: �����г����� ::::::::::::::::::::::::::::::::

            const int BTN_WIDTH = 90;
            const int BTN_HIGH = 55;
            m_SwithInOutBtn.SetButtonColor(192, 192, 192);
            m_SwithInOutBtn.SetButtonRect(m_Rect.X + m_Rect.Width / 2 - BTN_WIDTH/2 , m_Rect.Y + 10, BTN_WIDTH, BTN_HIGH);
            m_SwithInOutBtn.SetButtonText("����/�г�");

            #endregion


        }
        public void DrawOn(Graphics g)
        {
            //���Ʋ˵�����
            Title.OnDraw(g);

            //�� �� �� �� �� ��
            g.DrawPolygon(TrainPen, Mc1);
            g.DrawPolygon(TrainPen, Mc2);
            for (int i = 0; i < 6; i++)
            {
                g.DrawRectangle(TrainPen, TrainRect[i]);
            }

            //���Ʊ��
            DrawTrainNumber(g);

            //ҳ���������

            g.FillRectangle(Recbrush, m_Rect);
            g.DrawRectangle(Rectpen, m_Rect);

            if (m_DoorMgr.SelectDoorInfo != null)
            {
                m_SwithInOutBtn.OnDraw(g);
            }

        }

        /// <summary>
        /// ���Ʊ��
        /// </summary>
        /// <param name="g"></param>
        private void DrawTrainNumber(Graphics g)
        {
            for (int i = 0; i < 8; i++)
            {
                if (i < 7)
                {
                    g.DrawString("0" + (i + 1).ToString(), new Font("Arial", 12), new SolidBrush(Color.White), NoRect[i]);
                }
                else
                {
                    g.DrawString("00", new Font("Arial", 12), new SolidBrush(Color.White), NoRect[i]);
                }
            }
        }


    }
}
