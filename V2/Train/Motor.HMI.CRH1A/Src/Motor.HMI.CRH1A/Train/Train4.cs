using System.Drawing;
using System.Drawing.Drawing2D;
using MMI.Facility.Interface.Attribute;
using Motor.HMI.CRH1A.Common;

namespace Motor.HMI.CRH1A.Train
{
    /// <summary>
    /// 视图 23 24 的列车配置信息 通过点击列车车厢可以缩小显示的事件的范围 而点击整车按钮可以
    /// 整车的活动事件信息
    /// </summary>  
    [GksDataType(DataType.isMMIObjectClass)]
    class Train4 : TrainBase
    {
        #region 私有字段

        private readonly bool[] m_DrivingRoomStatus = new bool[2];
        #endregion

        #region 静态字段
        /// <summary>
        /// 故障所在的车厢
        /// </summary>
        public static int SelectedIndex = 0;
        private static SolidBrush _selectedBrush;
        private static SolidBrush _selectedTxtBrush;

        /// <summary>
        /// 故障处背景的画刷
        /// </summary>
        public static SolidBrush SelectedBrush
        {
            set
            {
                _selectedBrush = value;
                if (_selectedBrush.Color == Color.Black)
                {
                    _selectedTxtBrush = Brushes.White as SolidBrush;
                }
                else
                {
                    _selectedTxtBrush = Brushes.Black as SolidBrush;
                }
            }
            get { return _selectedBrush; }
        }

        #endregion

        #region 重载方法
        public override string GetInfo()
        {
            return "列车配置3";
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
        #endregion

        #region 私有方法
        private void GetValue()
        {
            for (int index = 0; index < 2; index++)
            {
                m_DrivingRoomStatus[index] = BoolList[UIObj.InBoolList[index]];
            }
        }

        private void InitData()
        {
            Recposition.Offset(0,77);
            //个点坐标初始化
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

            // 列车形状设置
            Path = new GraphicsPath();
            Path.AddArc(Recposition.X, Recposition.Y, 30, 60, 90, 180);
            Path.AddLine(Recposition.X + 15, Recposition.Y, Recposition.X + 320, Recposition.Y);
            Path.AddArc(Recposition.X + 305, Recposition.Y, 30, 60, 270, 180);
            Path.AddLine(Recposition.X + 15, Recposition.Y + 60, Recposition.X + 320, Recposition.Y + 60);

            //个车厢初始化
            for (int i = 0; i < 6; i++)
            {
                TrainRect[i] = new Rectangle(Recposition.X + 42 * (i + 1), Recposition.Y + 15, 42, 42);
            }
            //做司机室
            Path00 = new GraphicsPath();
            Path00.AddArc(Recposition.X, Recposition.Y, 30, 60, 105, 130);
            Path00.AddLine(Point1[0], Point1[1]);
            Path00.AddLine(Point1[1], Point2[0]);
            Path00.AddLine(Point2[0].X, Point2[0].Y, Recposition.X + 5, Recposition.Y + 57);

            //右司机室
            Path01 = new GraphicsPath();

            Path01.AddArc(Recposition.X + 305, Recposition.Y, 30, 60, 308, 125);
            Path01.AddLine(Recposition.X + 330, Recposition.Y + 57, Point2[6].X, Point2[6].Y);
            Path01.AddLine(Point2[6], Point1[7]);
            Path01.AddLine(Point1[7], Point1[8]);

        }

        private void DrawOn(Graphics g)
        {
            //绘制整体轮廓
            g.FillPath(Blue1Brush, Path);

            //根据列车状态绘制列车信息
            if (SelectedIndex == 0)
            {
                g.FillPath(SelectedBrush, Path00);
                g.DrawString("01", Font, _selectedTxtBrush, Point1[0].X + 15, Point1[0].Y + 10);
            }
            else
            {
                g.FillPath(Blue2Brush, Path00);
                g.DrawString("01", Font, WhiteBrush, Point1[0].X + 15, Point1[0].Y + 10);
            }
            if (SelectedIndex == 7)
            {
                g.FillPath(SelectedBrush, Path01);
                g.DrawString("00", Font, _selectedTxtBrush, Point1[7].X + 15, Point1[0].Y + 10);
            }
            else
            {
                g.FillPath(Blue2Brush, Path01);
                g.DrawString("00", Font, WhiteBrush, Point1[7].X + 15, Point1[7].Y + 10);
            }

            for (int i = 0; i < 6; i++)
            {
                if (i == SelectedIndex - 1)
                {
                    g.FillRectangle(SelectedBrush, TrainRect[i]);
                    g.DrawString("0" + (i + 2).ToString(), Font, _selectedTxtBrush, Point1[i + 1].X + 15, Point1[0].Y + 10);
                }
                else
                {
                    g.FillRectangle(Blue2Brush, TrainRect[i]);
                    g.DrawString("0" + (i + 2).ToString(), Font, WhiteBrush, Point1[i + 1].X + 15, Point1[0].Y + 10);

                }

            }

            //绘制线条
            g.DrawLine(WhitePen1, Point1[0], Point1[8]);
            for (int i = 0; i < 7; i++)
            {

                g.DrawLine(WhitePen1, Point1[i + 1], Point2[i]);
            }
            g.DrawPath(BluePen, Path);

            ////////////////////////////////////////
            //-ycl-
            //////////////////////////////////////
            //绘制司机室激活状态
            if (m_DrivingRoomStatus[0])//左司机室处于激活状态
            {
                g.FillEllipse(Brushes.Black, Point1[0].X + 1, Point1[0].Y + 12, 10, 10);
            }
            else if (m_DrivingRoomStatus[1])
            {
                g.FillEllipse(Brushes.Black, Point1[7].X + 30, Point1[7].Y + 12, 10, 10);
            }

        }
        #endregion

        #region 静态方法
        static Train4()
        {
            SelectedBrush = new SolidBrush(Color.FromArgb(255, 0, 0));
        }
        #endregion
    }
}
