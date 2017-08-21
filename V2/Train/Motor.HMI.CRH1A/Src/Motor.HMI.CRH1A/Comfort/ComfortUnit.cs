using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using CommonUtil.Controls;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Train;
using CommonUtil.Controls;

namespace Motor.HMI.CRH1A.Comfort
{
    /// <summary>
    /// 舒适度单元  , 包含灯, 车图, 车温
    /// </summary>
    public class ComfortUnit : SelectableRectangleControl
    {
        private int m_Id;

        /// <summary>
        /// 车温
        /// </summary>
        public float[] Temperature
        {
            set
            {
                m_Temperature = value;
                if (TempeRectangle != null && TempeRectangle.Length <= value.Length)
                {
                    for (int i = 0; i < TempeRectangle.Length; i++)
                    {
                        TempeRectangle[i].SetText(value[i].ToString("F0"));
                    }
                }
            }
            get { return m_Temperature; }
        }

        /// <summary>
        /// HVAC的状态
        /// </summary>
        public HvacState HvacState { set; get; }

        /// <summary>
        /// 房间类型
        /// </summary>
        public RoomType RoomType
        {
            protected set;
            get;
        }

        /// <summary>
        /// 车的编号
        /// </summary>
        public int Id
        {
            set
            {
                m_Id = value;
                Name = TrainIDConvert.Convert(value);
            }
            get { return m_Id; }
        }

        public Rectangle UnitRectangle { set; get; }

        /// <summary>
        /// 车的名字,用于显示
        /// </summary>
        public string Name { private set; get; }


        /// <summary>
        /// 灯是否打开, 默认关闭
        /// </summary>
        public bool IsLight { set; get; }

        /// <summary>
        /// 整个单元的大小 
        /// </summary>
        public static Size UnitSize
        {
            get;
            private set;
        }

        /// <summary>
        /// 是否为车头
        /// </summary>
        public bool IsTrainHead { get { return RoomType == RoomType.Head; }}


        protected Rectangle TrainNameRectangle;
        protected Rectangle TrainBodyRectangle;
        protected Rectangle LightRectangle;
        protected GDIRectText[] TempeRectangle;
        private float[] m_Temperature;

        /// <summary>
        /// 名字和车身之间的间隔
        /// </summary>
        private const int NameAndTrainInterval = 20;

        /// <summary>
        /// 车和选中的边框的间隔
        /// </summary>
        private const int TrainBodyAndSelectedIntervale = 3;


        /// <summary>
        /// 车身大小
        /// </summary>
        private static readonly Size TrainBodySize;

        /// <summary>
        /// 灯的大小
        /// </summary>
        protected static readonly Size LightSize;

        /// <summary>
        /// 温度的大小
        /// </summary>
        protected static readonly Size TemperSize;

        /// <summary>
        /// 车的名字的大小
        /// </summary>
        private static readonly Size TrainNameSize;

        static ComfortUnit()
        {
            TrainBodySize = new Size(90, 70);
            LightSize = new Size(20, 30);
            TemperSize = new Size(25, 20);
            TrainNameSize = new Size(30, 25);

            UnitSize = new Size(TrainBodySize.Width + TrainBodyAndSelectedIntervale * 2,
                TrainBodySize.Height + TrainBodyAndSelectedIntervale + NameAndTrainInterval + TrainNameSize.Height);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="startPoint"></param>
        public ComfortUnit(Point startPoint)
            : base()
        {
            RoomType = RoomType.Body;
            UnitRectangle = new Rectangle(startPoint.X, startPoint.Y, UnitSize.Width, UnitSize.Height);
            SetInnerCtrolRect();
            base.OutLineRectangle = new Rectangle(UnitRectangle.X,
                UnitRectangle.Y + TrainNameSize.Height - TrainBodyAndSelectedIntervale,
                TrainBodySize.Width + TrainBodyAndSelectedIntervale * 2,
                TrainBodySize.Height + TrainBodyAndSelectedIntervale * 2);
        }

        public override void OnDraw(Graphics g)
        {
            // 编号
            g.DrawString(TrainIDConvert.Convert(Id), new Font("Arial", 12), new SolidBrush(Color.White), TrainNameRectangle);

            // 车身
            g.DrawImage(ComfortImageRes.Instance.GetTrainUnitImage(this), TrainBodyRectangle);

            // 灯
            g.DrawImage(ComfortImageRes.Instance.GetLightImage(IsLight), LightRectangle);

            // 温度
            foreach (var gtRectText in TempeRectangle)
            {
                gtRectText.OnDraw(g);
            }

            base.OnDraw(g);
        }

        private void SetInnerCtrolRect()
        {
            TrainNameRectangle = new Rectangle(UnitRectangle.X + UnitRectangle.Width / 2 - TrainNameSize.Width / 2, UnitRectangle.Y, TrainNameSize.Width, TrainNameSize.Height);
            TrainBodyRectangle = new Rectangle(UnitRectangle.X + TrainBodyAndSelectedIntervale, UnitRectangle.Y + TrainNameSize.Height, TrainBodySize.Width, TrainBodySize.Height);
            LightRectangle = new Rectangle(TrainBodyRectangle.X + 15, TrainBodyRectangle.Y + 15, LightSize.Width, LightSize.Height);

            var temperText = new GDIRectText();
            temperText.SetBkColor(192, 192, 192);
            temperText.SetTextColor(0, 0, 0);
            temperText.SetTextStyle(8, FormatStyle.Center, true, "Arial");
            temperText.Isdrawrectfrm = true;
            temperText.SetTextRect(TrainBodyRectangle.X + 55, TrainBodyRectangle.Y + 20, TemperSize.Width, TemperSize.Height);

            TempeRectangle = new[]
            {
                temperText
            };
        }
    }
}
