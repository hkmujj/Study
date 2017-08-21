using System.Drawing;
using System.Drawing.Drawing2D;
using MMI.Facility.Interface.Attribute;
using Motor.HMI.CRH1A.Common;

namespace Motor.HMI.CRH1A.Alarm
{
    /// <summary>
    /// ��ͼ 23 24 ���г�������Ϣ ͨ������г����������С��ʾ���¼��ķ�Χ �����������ť����
    /// �����Ļ�¼���Ϣ
    /// </summary>  
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    class GT_Trian2 : CRH1BaseClass
    {
        #region ˽���ֶ�
        public Rectangle Recposition = new Rectangle(20, 100, 100, 100);
        public Pen WhitePen1 = new Pen(Color.FromArgb(255, 255, 255));//��ɫ����
        public Pen WhitePen2 = new Pen(Color.FromArgb(240, 240, 240));//����ɫ����

        private bool[] m_DrivingRoomStatus = new bool[2];

        public Pen BluePen = new Pen(Color.FromArgb(48, 128, 168), 3);
        public SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));//��ɫ��ˢ
        public SolidBrush Blue1Brush = new SolidBrush(Color.FromArgb(48, 128, 168));//������ɫ��ˢ ���ڻ��Ƴ���
        public SolidBrush Blue2Brush = new SolidBrush(Color.FromArgb(56, 144, 192));//��ǳ��ɫ��ˢ ���ڻ��Ƹ��ڳ���
        public SolidBrush Blackbrush = new SolidBrush(Color.FromArgb(0, 0, 0));//��ɫ��ˢ
        public Font Font = new Font("Arial", 11);
        public GraphicsPath Path;//����
        public GraphicsPath Path01;//����1
        public GraphicsPath Path00;//����8
        //public GraphicsPath path_Selected;//������ѡ��
        public Rectangle[] TrainRect = new Rectangle[6];//�м����ڳ���
        public static bool[] Selected = new bool[9] { false, false, false, false, false, false, false, false, true };

        public Point[] Point1 = new Point[9];
        public Point[] Point2 = new Point[7];
        #endregion

        #region ��̬�ֶ�
        public static int  SelectedCarId=0;
        #endregion
        public override string GetInfo()
        {
            return "�г�����2";
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
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        public void GetValue()
        {
            for (int index = 0; index < 2; index++)
            {
                m_DrivingRoomStatus[index] = BoolList[UIObj.InBoolList[index]];
            }
        }

        public void InitData()
        {
            //���������ʼ��
            Point1[0] = new Point(Recposition.X + 4, Recposition.Y + 15);
            Point1[8] = new Point(Recposition.X + 331, Recposition.Y + 15);
            for (int i = 1; i < 8; i++)
            {
                Point1[i] = new Point(Recposition.X + 42 * i, Recposition.Y + 15);
            }
            for (int i = 0; i < 7; i++)
            {
                Point2[i] = new Point(Recposition.X + 42 * (i + 1), Recposition.Y + 57);
            }

            // �г���״����
            Path = new GraphicsPath();
            Path.AddArc(Recposition.X, Recposition.Y, 30, 60, 90, 180);
            Path.AddLine(Recposition.X + 15, Recposition.Y, Recposition.X + 320, Recposition.Y);
            Path.AddArc(Recposition.X + 305, Recposition.Y, 30, 60, 270, 180);
            Path.AddLine(Recposition.X + 15, Recposition.Y + 60, Recposition.X + 320, Recposition.Y + 60);

            //�������ʼ��
            for (int i = 0; i < 6; i++)
            {
                TrainRect[i] = new Rectangle(Recposition.X + 42 * (i + 1), Recposition.Y + 15, 42, 42);
            }
            //��˾����
            Path00 = new GraphicsPath();
            Path00.AddArc(Recposition.X, Recposition.Y, 30, 60, 105, 130);
            Path00.AddLine(Point1[0], Point1[1]);
            Path00.AddLine(Point1[1], Point2[0]);
            Path00.AddLine(Point2[0].X, Point2[0].Y, Recposition.X + 5, Recposition.Y + 57);

            //��˾����
            Path01 = new GraphicsPath();

            Path01.AddArc(Recposition.X + 305, Recposition.Y, 30, 60, 308, 125);
            Path01.AddLine(Recposition.X + 330, Recposition.Y + 57, Point2[6].X, Point2[6].Y);
            Path01.AddLine(Point2[6], Point1[7]);
            Path01.AddLine(Point1[7], Point1[8]);

        }

        public void DrawOn(Graphics g)
        {
            //������������
            g.FillPath(Blue1Brush, Path);

            //�����г�״̬�����г���Ϣ
            if (Selected[0])
            {
                g.FillPath(WhiteBrush, Path00);
                g.DrawString("01", Font, Blackbrush, Point1[0].X + 15, Point1[0].Y + 10);
            }
            else
            {
                g.FillPath(Blue2Brush, Path00);
                g.DrawString("01", Font, WhiteBrush, Point1[0].X + 15, Point1[0].Y + 10);
            }
            if (Selected[7])
            {
                g.FillPath(WhiteBrush, Path01);
                g.DrawString("00", Font, Blackbrush, Point1[7].X + 15, Point1[0].Y + 10);
            }
            else
            {
                g.FillPath(Blue2Brush, Path01);
                g.DrawString("00", Font, WhiteBrush, Point1[7].X + 15, Point1[7].Y + 10);
            }
            for (int i = 0; i < 6; i++)
            {
                if (Selected[i + 1])
                {
                    g.FillRectangle(WhiteBrush, TrainRect[i]);
                    g.DrawString("0" + (i + 2).ToString(), Font, Blackbrush, Point1[i + 1].X + 15, Point1[0].Y + 10);
                }
                else
                {
                    g.FillRectangle(Blue2Brush, TrainRect[i]);
                    g.DrawString("0" + (i + 2).ToString(), Font, WhiteBrush, Point1[i + 1].X + 15, Point1[0].Y + 10);


                }

            }
            if (Selected[8])//������ѡ���¼�
            {
                g.FillPath(WhiteBrush, Path00);
                g.DrawString("01", Font, Blackbrush, Point1[0].X + 15, Point1[0].Y + 10);

                g.FillPath(WhiteBrush, Path01);
                g.DrawString("00", Font, Blackbrush, Point1[7].X + 15, Point1[0].Y + 10);

                for (int i = 0; i < 6; i++)
                {
                    g.FillRectangle(WhiteBrush, TrainRect[i]);
                    g.DrawString("0" + (i + 2).ToString(), Font, Blackbrush, Point1[i + 1].X + 15, Point1[0].Y + 10);
                }

            }

            //��������
            g.DrawLine(WhitePen1, Point1[0], Point1[8]);
            for (int i = 0; i < 7; i++)
            {
                if (Selected[8])
                {
                    g.DrawLine(WhitePen2, Point1[i + 1], Point2[i]);
                }
                else
                {
                    g.DrawLine(WhitePen1, Point1[i + 1], Point2[i]);
                }
            }
            g.DrawPath(BluePen, Path);

            ////////////////////////////////////////
            //-ycl-
            //////////////////////////////////////
            //����˾���Ҽ���״̬
            if (m_DrivingRoomStatus[0])//��˾���Ҵ��ڼ���״̬
            {
                g.FillEllipse(Brushes.Black, Point1[0].X + 1, Point1[0].Y + 12, 10, 10);
            }
            else if (m_DrivingRoomStatus[1])
            {
                g.FillEllipse(Brushes.Black, Point1[7].X + 30, Point1[7].Y + 12, 10, 10);
            }

        }
        protected override bool OnMouseDown(Point point)
        {
            OnButtonDown(point.X, point.Y);
            return true;
        }

        public void OnButtonDown(int x, int y)
        {
            for (int i = 0; i < 8; i++)
            {
                if (x > Point1[i].X && x < Point1[i].X + TrainRect[0].Width && y > Point1[i].Y && y < Point1[i].Y + TrainRect[0].Height)
                {
                    SelectedCarId = i + 1;

                    for (int j = 0; j < 9; j++)
                    {
                        if (j == i)
                        {
                            Selected[j] = true;
                           
                        }
                        else
                        {
                            Selected[j] = false;
                        }
                    }
                }
            }

        }
    }
}