using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls.Button;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH1A.Alarm.Fault;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Global;

namespace Motor.HMI.CRH1A.Alarm
{
    /// <summary>
    ///  ��ǰ��¼���ͼ �����¼�ͼ�갴ť ���ǵ��������¼
    /// Ҳ���Խ������ͼ
    /// </summary>  
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class GT_Active_Alarm : CRH1BaseClass
    {
        public GT_MenuTitle Title = new GT_MenuTitle("��ǰ����");//�˵�����
        public Rectangle Recposition = new Rectangle(0, 178, 790, 277);
        public CRH1AButton[] GButton = new CRH1AButton[4];
        public Image[] Image = new Image[2];
        public string[] Strtitle = { "��", "��", "����/ʱ��", "��", "��", "����" };//��ʱ��12�� ���Ը�����Ҫ��Ӻ�ɾ��
        public Pen Pen = new Pen(Color.FromArgb(210, 210, 210));
        public Rectangle[] Rect = new Rectangle[12];
        public SolidBrush Brush = new SolidBrush(Color.FromArgb(255, 255, 225));
        public SolidBrush Whitebrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        public SolidBrush Blackbrush = new SolidBrush(Color.FromArgb(0, 0, 0));
        public Pen RectPen = new Pen(Color.FromArgb(55, 55, 55), 2);
        public Font Font = new Font("Arial", 11);
        public Font Smallfont = new Font("Arial", 9);
        public Point[,] InfoPoint = new Point[10, 6];
        public int[] Point = new int[6] { 35, 80, 125, 245, 285, 330 };

        /// <summary>
        /// ����������еĻ����
        /// </summary>
        private List<ExceptionData> m_ActiveEventByCount = new List<ExceptionData>();
        private SortedList<int, ExceptionData> m_ActiveEventByUnit = new SortedList<int, ExceptionData>(); //����Ԫ���ֵ��¼�
        public int CurSelectIndex = 0;
        public int CurPage = 0;//��ǰҳ��Ϊ0
        public int NumPerPage = 10;//ÿҳ��ʾ�ļ�¼��  

        #region ���ط���
        public override string GetInfo()
        {
            return "��ǰ��¼�";
        }


        public override bool Initialize()
        {
            //3
            if (UIObj.ParaList.Count >= 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    Image[i] = System.Drawing.Image.FromFile(RecPath + "//" + UIObj.ParaList[i]);
                }
            }
            InitData();

            return true;
        }
        public override void paint(Graphics dcGs)
        {
            SelectFaultByCarId();
            DrawOn(dcGs);
            base.paint(dcGs);
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
            //  �� ť �� Ӧ �� ��
            foreach (var source in GButton.Where(w => w != null && w.Contains(nPoint) && w.IsEnable))
            {
                source.OnButtonDown();
            }
        }
        #endregion

        #region ˽�з���
        private void InitData()
        {
            ///////////�ײ���ť��ʼ��
            GButton[0] = new CRH1AButton();
            GButton[0].SetButtonRect(Recposition.X + 710, Recposition.Y - 75, 80, 60);
            GButton[0].SetButtonColor(192, 192, 192);
            GButton[0].SetButtonText("����");

            GButton[1] = new CRH1AButton();
            GButton[1].SetButtonColor(192, 192, 192);
            GButton[1].SetButtonRect(Recposition.X, Recposition.Y + 335, 80, 50);

            GButton[2] = new CRH1AButton();
            GButton[2].SetButtonColor(192, 192, 192);
            GButton[2].SetButtonRect(Recposition.X + 85, Recposition.Y + 335, 80, 50);

            GButton[3] = new CRH1AButton();
            GButton[3].SetButtonColor(192, 192, 192);
            GButton[3].SetButtonRect(Recposition.X + 400, Recposition.Y + 335, 80, 50);
            GButton[3].SetButtonText("��������");


            /////ÿ��������ο��ʼ��
            for (int i = 0; i < 11; i++)
            {
                Rect[i] = new Rectangle(Recposition.X, Recposition.Y + 25 * i, 800, 25);
            }
            Rect[11] = new Rectangle(Recposition.X, Recposition.Y, 30, 275);

            // ������Ϣ��ʾ�����ʼ��
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 6; j++)
                {
                    InfoPoint[i, j] = new Point(Point[j] + Recposition.X, Recposition.Y + i * 25 + 30);
                }
            GlobalInfo.Instance.ButtonStatusChange += () =>
            {
                foreach (var button in GButton)
                {
                    button.IsEnable = GlobalInfo.Instance.ButtonEnable;
                }
            };
        }
        private void DrawOn(Graphics g)
        {
            #region ���ƻ���Ԫ��
            //���Ʋ˵�����
            Title.OnDraw(g);

            //���Ƶײ���ť
            for (int i = 0; i < 4; i++)
            {
                GButton[i].OnDraw(g);
            }
            g.DrawImage(Image[0], GButton[1].OutLineRectangle);
            g.DrawImage(Image[1], GButton[2].OutLineRectangle);

            //�����ɫ
            g.DrawRectangle(RectPen, Recposition);
            for (int i = 0; i < 11; i++)
            {
                if (i == 0)
                {
                    g.FillRectangle(Brush, Rect[i]);
                }
                else
                {
                    g.FillRectangle(Whitebrush, Rect[i]);
                }
            }
            g.FillRectangle(Brush, Rect[11]);

            //��ӱ���ͱ��
            for (int i = 0; i < 10; i++)//��ӱ��
            {
                if (i < 9)
                {
                    g.DrawString((i + 1).ToString(), Font, Blackbrush, Recposition.X + 10, Recposition.Y + (i + 1) * 25 + 5);
                }
                else
                {
                    g.DrawString("10", Font, Blackbrush, Recposition.X + 5, Recposition.Y + (i + 1) * 25 + 5);
                }
            }
            g.DrawString("��", Font, Blackbrush, Recposition.X + 38, Recposition.Y + 5);
            g.DrawString("��", Font, Blackbrush, Recposition.X + 85, Recposition.Y + 5);
            g.DrawString("����/ʱ��", Font, Blackbrush, Recposition.X + 130, Recposition.Y + 5);
            g.DrawString("��", Font, Blackbrush, Recposition.X + 250, Recposition.Y + 5);
            g.DrawString("��", Font, Blackbrush, Recposition.X + 290, Recposition.Y + 5);
            g.DrawString("����", Font, Blackbrush, Recposition.X + 450, Recposition.Y + 5);
            #endregion

            #region ��ӵ�ǰ�������ʾ
            if (m_ActiveEventByCount.Count > 0)
            {
                for (int i = CurSelectIndex; i <= m_ActiveEventByCount.Count - 1 && i <= CurSelectIndex + 9; i++)
                {
                    g.DrawString(m_ActiveEventByCount[i].ExCarUnit.ToString(), Smallfont, Blackbrush, InfoPoint[i - CurSelectIndex, 0]);
                    g.DrawString(m_ActiveEventByCount[i].ExCarId.ToString("00"), Smallfont, Blackbrush, InfoPoint[i - CurSelectIndex, 1]);
                    g.DrawString(m_ActiveEventByCount[i].Startdate.ToString(), Smallfont, Blackbrush, InfoPoint[i - CurSelectIndex, 2]);
                    g.DrawString(m_ActiveEventByCount[i].ExId.ToString(), Smallfont, Blackbrush, InfoPoint[i - CurSelectIndex, 3]);
                    g.DrawString(m_ActiveEventByCount[i].ExTypeInfo, Smallfont, Blackbrush, InfoPoint[i - CurSelectIndex, 4]);
                    g.DrawString(m_ActiveEventByCount[i].ExText, Smallfont, Blackbrush, InfoPoint[i - CurSelectIndex, 5]);
                }
            }
            #endregion
            //��ӹ�����Ϣ
            //if (GT_AlarmOverView.selected_Unit[14])
            //{

            //}
            //else
            //{
            //    if (activeEventByUnit.Count > 0)
            //    {
            //        for (int i = CurSelectIndex; i <= activeEventByUnit.Count && i <= CurSelectIndex + 9; i++)
            //        {
            //            g.DrawString(activeEventByUnit[i].exUnit.ToString(), smallfont, blackbrush, Info_Point[i - CurSelectIndex, 0]);
            //            g.DrawString(activeEventByUnit[i].exCarID.ToString(), smallfont, blackbrush, Info_Point[i - CurSelectIndex, 1]);
            //            g.DrawString(activeEventByUnit[i].startdate.ToString(), smallfont, blackbrush, Info_Point[i - CurSelectIndex, 2]);
            //            g.DrawString(activeEventByUnit[i].exID.ToString(), smallfont, blackbrush, Info_Point[i - CurSelectIndex, 3]);
            //            g.DrawString(activeEventByUnit[i].exType.ToString(), smallfont, blackbrush, Info_Point[i - CurSelectIndex, 4]);
            //            g.DrawString(activeEventByUnit[i].exText, smallfont, blackbrush, Info_Point[i - CurSelectIndex, 5]);
            //        }
            //    }
            //}

            //��������
            for (int i = 0; i <= 275; i += 25)
            {
                g.DrawLine(Pen, Recposition.X, Recposition.Y + i, Recposition.X + 800, Recposition.Y + i);
            }
            g.DrawLine(Pen, Recposition.X + 30, Recposition.Y, Recposition.X + 30, Recposition.Y + 275);
            g.DrawLine(Pen, Recposition.X + 75, Recposition.Y, Recposition.X + 75, Recposition.Y + 275);
            g.DrawLine(Pen, Recposition.X + 120, Recposition.Y, Recposition.X + 120, Recposition.Y + 275);
            g.DrawLine(Pen, Recposition.X + 240, Recposition.Y, Recposition.X + 240, Recposition.Y + 275);
            g.DrawLine(Pen, Recposition.X + 280, Recposition.Y, Recposition.X + 280, Recposition.Y + 275);
            g.DrawLine(Pen, Recposition.X + 330, Recposition.Y, Recposition.X + 330, Recposition.Y + 275);

        }
        private void OnButtonUp(Point nPoint)
        {
            for (int i = 0; i < 4; i++)
            {

                if (GButton[i].Contains(nPoint) && GButton[i].IsEnable)
                {
                    switch (i)
                    {
                        case 0:
                            GT_Trian2.SelectedCarId = -1;
                            for (int j = 0; j < 8; j++)
                            {
                                GT_Trian2.Selected[i] = false;
                            }
                            GT_Trian2.Selected[8] = true;
                            break;
                        case 1:
                            if (CurSelectIndex + 9 < m_ActiveEventByCount.Count)
                            {
                                CurPage++;
                            }
                            CurSelectIndex = NumPerPage * CurPage;
                            break;
                        case 2:
                            if (CurPage > 0)
                            {
                                CurPage--;
                            }
                            CurSelectIndex = NumPerPage * CurPage;
                            break;
                        case 3:
                            GT_AlarmOverView.SelectedUnitView = -1;
                            //for (int j = 0;j < 14;j++ )
                            //{
                            //    GT_AlarmOverView.selected_Unit[j] = false;
                            //}
                            //GT_AlarmOverView.selected_Unit[14] = true;
                            for (int j = 0; j < 8; j++)
                            {
                                GT_Trian2.Selected[j] = false;
                            }
                            GT_Trian2.Selected[8] = true;
                            OnPost(CmdType.ChangePage, 25, 0, 0);
                            break;
                    }
                    GButton[i].OnButtonUp();
                }
            }
        }

        /// <summary>
        /// �������ѡ������
        /// </summary>
        private void SelectFaultByCarId()
        {
            m_ActiveEventByCount.Clear();

            if (GT_AlarmOverView.SelectedUnitView > 0 && GT_AlarmOverView.SelectedUnitView < 15)
            {
                var allFault = GT_GalobalFaultManage.Instance.AllActiveFaults;
                foreach (ExceptionData exData in allFault)
                {
                    if (exData.ExUnit == GT_AlarmOverView.SelectedUnitView)
                    {
                        m_ActiveEventByCount.Add(exData);
                    }
                }

                return;
            }

            if (GT_Trian2.SelectedCarId > 0 && GT_Trian2.SelectedCarId < 9)
            {
                var allFault = GT_GalobalFaultManage.Instance.AllActiveFaults;
                foreach (ExceptionData exData in allFault)
                {
                    if (exData.ExCarId == GT_Trian2.SelectedCarId)
                    {
                        m_ActiveEventByCount.Add(exData);
                    }
                }
            }
            else
            {
                var allFault = GT_GalobalFaultManage.Instance.AllActiveFaults;
                foreach (ExceptionData exData in allFault)
                {
                    m_ActiveEventByCount.Add(exData);
                }
            }
        }
        #endregion
    }
}