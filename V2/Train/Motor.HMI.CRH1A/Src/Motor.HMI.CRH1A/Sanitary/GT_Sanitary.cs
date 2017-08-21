using System.Drawing;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Config;
using Motor.HMI.CRH1A.Sanitary;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;


namespace Motor.HMI.CRH1A
{
    [GksDataType(DataType.isMMIObjectClass)]
    class GT_Sanitary : CRH1BaseClass
    {
        public GT_MenuTitle Title = new GT_MenuTitle("����");//�˵�����
        public Rectangle Recposition = new Rectangle(0, 130, 800, 100);
        public Rectangle[] TrainRect = new Rectangle[8];//��ʾ������ı߿�
        public Rectangle[] WsRect = new Rectangle[8];//��ˮ��λ��
        public Rectangle[] NoRect = new Rectangle[8];//�������ʾλ��
        public Rectangle[] JsRect = new Rectangle[8];//��ˮ��λ��
        public Rectangle[] WcuRect = new Rectangle[8];//�ϲ����λ��
        public Rectangle[] WcdRect = new Rectangle[8];//�³�����λ��
        public int Weight = 65;//���ӵĿ��
        public int Height = 45;//���ӵĸ߶�

        public Rectangle PageFootRectangle { get { return m_Rect; } }
        Rectangle m_Rect;//ҳ������

        public SolidBrush Recbrush = new SolidBrush(Color.FromArgb(119, 136, 153));
        public Pen Rectpen = new Pen(Color.FromArgb(209, 226, 242), 3);
        public Pen TrainPen = new Pen(Color.FromArgb(177, 177, 177), 2);
        public Font Strfont = new Font("Arial", 11);
        public SolidBrush Whitebrush = new SolidBrush(Color.FromArgb(255, 255, 255));

        public Image[] Image = new Image[6];
        public bool[] Valueb = new bool[32];
        public float[] Valuef = new float[16];

        public string[] SrtWs = new string[6] { "<20%", "<40%", "<60%", "<80%", ">80%", "Full" };//��ˮ��Ϣ��ʾ
        public string[] SrtJs = new string[6] { "Empty", "<25%", "<50%", "<75%", ">75%", "100%" };//��ˮ��Ϣ��ʾ

        private SanitaryButtonMgr m_SanitaryButtonMgr;

        /// <summary>
        /// ѡ�еľ�ˮ��
        /// </summary>
        private int m_SelectedCleanBoxIdx;

        private Pen m_SelectedOutlinePen = new Pen(Color.Black, 2);

        public GT_Sanitary()
        {
            m_SanitaryButtonMgr = new SanitaryButtonMgr(this);
        }

        public override string GetInfo()
        {
            return "����ϵͳ";
        }


        protected override void NavigateTo(ViewConfig to)
        {
            //if (nParaA == ParaADefine.SwitchFromOhter)
            {
                ReselectCleanBox(-1);
            }
        }

        private void ReselectCleanBox(int idx)
        {
            if (idx == m_SelectedCleanBoxIdx)
            {
                m_SelectedCleanBoxIdx = -1;
            }
            else
            {
                m_SelectedCleanBoxIdx = idx;
            }
        }

        public override bool Initialize()
        {
            //3
            InitData();

            InitButton();

            //////////////////�� �� ͼ Ƭ
            if (UIObj.ParaList.Count >= 6)
                for (int i = 0; i < 6; i++)
                {
                    Image[i] = System.Drawing.Image.FromFile(RecPath + "//" + UIObj.ParaList[i]);
                }

            return true;
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void InitButton()
        {
            m_SanitaryButtonMgr.Init();
            m_SanitaryButtonMgr.ClearAllDownHandler +=
                (sender, args) =>
                    OnPost(CmdType.SetBoolValue,
                        UIObj.OutBoolList[SanitaryOutBoolIdxAdpt.GetClearAllIdx(m_SelectedCleanBoxIdx)], 1, 0);

            m_SanitaryButtonMgr.ClearSigleDownHandler +=
                (sender, args) =>
                    OnPost(CmdType.SetBoolValue,
                        UIObj.OutBoolList[SanitaryOutBoolIdxAdpt.GetClearSingleIdx(m_SelectedCleanBoxIdx)], 1, 0);

            m_SanitaryButtonMgr.ClearAllUpHandler += (sender, args) => OnPost(CmdType.SetBoolValue,
                        UIObj.OutBoolList[SanitaryOutBoolIdxAdpt.GetClearAllIdx(m_SelectedCleanBoxIdx)], 0, 0);
            m_SanitaryButtonMgr.ClearSigleUpHandler += (sender, args) => OnPost(CmdType.SetBoolValue,
                    UIObj.OutBoolList[SanitaryOutBoolIdxAdpt.GetClearSingleIdx(m_SelectedCleanBoxIdx)], 0, 0);
        }

        protected override bool OnMouseDown(Point point)
        {
            for (int i = 0; i < JsRect.Length; i++)
            {
                if (JsRect[i].Contains(point))
                {
                    ReselectCleanBox(i);
                    return true;
                }
            }
            m_SanitaryButtonMgr.OnButtonDown(point.X, point.Y);
            return true;
        }

        protected override bool OnMouseUp(Point point)
        {
            m_SanitaryButtonMgr.OnButtonUp(point.X, point.Y);
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            GetValue();
            ReFreshData();
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        public void GetValue()
        {
            if (UIObj.InBoolList.Count >= 32)
            {
                for (int i = 0; i < 32; i++)
                {
                    Valueb[i] = BoolList[UIObj.InBoolList[i]];
                }
            }
            if (UIObj.InFloatList.Count >= 16)
            {
                for (int i = 0; i < 16; i++)
                {
                    Valuef[i] = FloatList[UIObj.InFloatList[i]];
                }
            }

        }

        public void ReFreshData()
        {
            for (int i = 0; i < 16; i++)
            {

            }

        }
        public void InitData()
        {

            #region::::::::::::;;;;;;;;�� �� �� �� λ �� �� ʼ ��;;;;;;;;;;;;;;;;;;;;;;;;;;;

            for (int i = 0; i < 8; i++)
            {
                TrainRect[i] = new Rectangle(Recposition.X + i * 90 + 40, Recposition.Y, 85, 234);
            }
            #endregion
            #region::::::::::::::::::::�� ˮ �� �� λ �� �� ��:::::::::::::::::::::::::::::
            for (int i = 0; i < 8; i++)
            {
                WsRect[i] = new Rectangle(Recposition.X + i * 90 + 50, Recposition.Y + 30, Weight, Height);

            }
            #endregion

            #region :::::::::::::::::::::::�� ˮ �� �� λ �� �� ��;;;;;;;;;;;;;;;;;;;;;;;;;;
            for (int i = 0; i < 8; i++)
            {
                JsRect[i] = new Rectangle(Recposition.X + i * 90 + 50, Recposition.Y + 90, Weight, Height);
            }
            #endregion


            #region :;;;;:::::::::::::::: �� �� λ �� �� ��;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
            for (int i = 0; i < 8; i++)
            {
                WcuRect[i] = new Rectangle(Recposition.X + i * 90 + 60, Recposition.Y + 145, 40, 40);//��8��
                WcdRect[i] = new Rectangle(Recposition.X + i * 90 + 60, Recposition.Y + 190, 40, 40);//��8��
            }

            #endregion
            #region :::::::::::::::::::�������ʾ��λ������;;;;;;;;;;;;;;;;;;;
            for (int i = 0; i < 8; i++)
            {
                NoRect[i] = new Rectangle(Recposition.X + i * 90 + 65, Recposition.Y + 10, 30, 25);
            }
            #endregion

            #region ::::::::::::::::::::ҳ �� �� Ϣ �� �� λ ��::::::::::::::::::::::::::::::::
            m_Rect = new Rectangle(Recposition.X, Recposition.Y + 240, 790, 80);

            #endregion
        }
        public void DrawOn(Graphics g)
        {

            //���Ʋ˵�����
            Title.OnDraw(g);

            //�� �� �� �� �� ��
            for (int i = 0; i < 8; i++)
            {
                g.DrawRectangle(TrainPen, TrainRect[i]);
            }

            //���Ʊ��
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
            //������ˮ��
            for (int i = 0; i < 8; i++)
            {
                g.DrawImage(Image[0], WsRect[i]);
                g.DrawRectangle(TrainPen, WsRect[i]);
            }
            //��ʾ��ˮ��ˮλ��Ϣ
            for (int i = 0; i < 8; i++)
            {
                if (Valuef[i] < 20)
                {
                    g.DrawString(SrtWs[0], Strfont, Whitebrush, WsRect[i].X + 12, WsRect[i].Y + 20);
                }
                else if (Valuef[i] >= 20 && Valuef[i] < 40)
                {
                    g.DrawString(SrtWs[1], Strfont, Whitebrush, WsRect[i].X + 12, WsRect[i].Y + 20);
                }
                else if (Valuef[i] >= 40 && Valuef[i] < 60)
                {
                    g.DrawString(SrtWs[2], Strfont, Whitebrush, WsRect[i].X + 12, WsRect[i].Y + 20);
                }
                else if (Valuef[i] >= 60 && Valuef[i] < 80)
                {
                    g.DrawString(SrtWs[3], Strfont, Whitebrush, WsRect[i].X + 12, WsRect[i].Y + 20);
                }
                else if (Valuef[i] >= 80 && Valuef[i] < 100)
                {
                    g.DrawString(SrtWs[4], Strfont, Whitebrush, WsRect[i].X + 12, WsRect[i].Y + 20);
                }
                else
                {
                    g.DrawString(SrtWs[5], Strfont, Whitebrush, WsRect[i].X + 12, WsRect[i].Y + 20);
                }
            }
            //���ƾ�ˮ��
            for (int i = 0; i < 8; i++)
            {
                g.DrawImage(Image[1], JsRect[i]);
                g.DrawRectangle(TrainPen, JsRect[i]);
            }

            //��ʾ��ˮ����Ϣ
            for (int i = 0; i < 8; i++)
            {
                if (Valuef[i + 8] <= 0)
                {
                    g.DrawString(SrtJs[0], Strfont, Whitebrush, JsRect[i].X + 12, JsRect[i].Y + 20);
                }
                else if (Valuef[i + 8] >= 0 && Valuef[i + 8] < 25)
                {
                    g.DrawString(SrtJs[1], Strfont, Whitebrush, JsRect[i].X + 12, JsRect[i].Y + 20);
                }
                else if (Valuef[i + 8] >= 25 && Valuef[i + 8] < 50)
                {
                    g.DrawString(SrtJs[2], Strfont, Whitebrush, JsRect[i].X + 12, JsRect[i].Y + 20);
                }
                else if (Valuef[i + 8] >= 50 && Valuef[i + 8] < 75)
                {
                    g.DrawString(SrtJs[3], Strfont, Whitebrush, JsRect[i].X + 12, JsRect[i].Y + 20);
                }
                else if (Valuef[i + 8] >= 75 && Valuef[i + 8] < 100)
                {
                    g.DrawString(SrtJs[4], Strfont, Whitebrush, JsRect[i].X + 12, JsRect[i].Y + 20);
                }
                else
                {
                    g.DrawString(SrtJs[5], Strfont, Whitebrush, JsRect[i].X + 12, JsRect[i].Y + 20);
                }
            }


            //�� �� �� ��

            for (int i = 0; i < 8; i++)
            {

                //�ϱ�
                if (!Valueb[i] && !Valueb[i + 8])//û�� û����
                {
                    g.DrawImage(Image[2], WcuRect[i]);
                }
                if (!Valueb[i] && Valueb[i + 8])//û�� �й���
                {
                    g.DrawImage(Image[3], WcuRect[i]);
                }
                if (Valueb[i] && !Valueb[i + 8])//���� û����
                {
                    g.DrawImage(Image[4], WcuRect[i]);
                }
                if (Valueb[i] && Valueb[i + 8])//���� �й���
                {
                    g.DrawImage(Image[5], WcuRect[i]);
                }

                //�±�
                if (!Valueb[i + 16] && !Valueb[i + 24])//û�� û����
                {
                    g.DrawImage(Image[2], WcdRect[i]);
                }
                if (!Valueb[i] && Valueb[i + 8])//û�� �й���
                {
                    g.DrawImage(Image[3], WcdRect[i]);
                }
                if (Valueb[i] && !Valueb[i + 8])//���� û����
                {
                    g.DrawImage(Image[4], WcdRect[i]);
                }
                if (Valueb[i] && Valueb[i + 8])//���� �й���
                {
                    g.DrawImage(Image[5], WcdRect[i]);
                }

            }
            //ҳ���������

            g.FillRectangle(Recbrush, m_Rect);
            g.DrawRectangle(Rectpen, m_Rect);
            if (-1 != m_SelectedCleanBoxIdx)
            {
                // ����button
                m_SanitaryButtonMgr.OnDraw(g);
                g.DrawRectangle(m_SelectedOutlinePen, JsRect[m_SelectedCleanBoxIdx]);

            }

        }


    }
}
