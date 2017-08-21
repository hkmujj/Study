using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CommonUtil.Controls;
using CommonUtil.Util.Extension;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Station.Door;
using Motor.HMI.CRH1A.Station.Overload;

namespace Motor.HMI.CRH1A.Station.Car
{
    abstract class DoorViewCarBase : CommonInnerControlBase, IDoorViewCar
    {
        protected DoorViewCarBase(int carNo, CarStyle carStyle, GT_Station stationView)
        {
            CarStyle = carStyle;
            StationView = stationView;
            CarNo = carNo;
            CarName = CarNameFactory.GetCarName(carNo);
            m_NameView = new GDIRectText()
                         {
                             Text = CarName,
                             Size = new Size(DefaultNameSize.Width, DefaultNameSize.Height),
                             TextColor = Color.Black,
                             BkColor = Color.FromArgb(192,192,192),
                             TextFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                         };
            BodyBounds = new Rectangle(Point.Empty, DefaultBodySize);

            InitalizeOverloadView();

            InitalizeDoors();

            OutLineChanged += OnOutLineChanged;
        }

        protected GT_Station StationView;

        public static Size DefaultSize = new Size(80, 95);

        public static Size DefaultBodySize = new Size(DefaultSize.Width, 70);

        public static Size DefaultNameSize = new Size(DefaultSize.Width, DefaultSize.Height - DefaultBodySize.Height);

        public CarStyle CarStyle { get; private set; }

        public CarType CarType { get; protected set; }

        public string CarName { private set; get; }

        /// <summary>
        /// 车身区域
        /// </summary>
        public Rectangle BodyBounds { private set; get; }

        /// <summary>
        /// 从0开始
        /// </summary>
        public int CarNo { private set; get; }

        public IStationDoor LocationUpDoor { protected set; get; }

        public IStationDoor LocationDownDoor { protected set; get; }

        public OverloadView OverloadView { protected set; get; }

        private readonly GDIRectText m_NameView;

        protected virtual void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            m_OutLineRectangle = new Rectangle(m_OutLineRectangle.Location, DefaultSize);

            BodyBounds = new Rectangle(Location.Translate(0, DefaultNameSize.Height), DefaultBodySize);

            m_NameView.Location = m_OutLineRectangle.Location;

            if (OverloadView != null)
            {
                OverloadView.Center = BodyBounds.GetCenterPoint();
            }

            LocationUpDoor.Location = LocationChangeMatrix.TransformPoint(LocationUpDoor.Location);
            LocationDownDoor.Location = LocationChangeMatrix.TransformPoint(LocationDownDoor.Location);
        }


        private void InitalizeOverloadView()
        {
            OverloadView = new OverloadView()
                           {
                               Center = BodyBounds.GetCenterPoint(),
                               RefreshAction = o =>
                               {
                                   var obj = (OverloadView) o;
                                   obj.State = StationView.Resource.GetOverloadState(this);
                               }
                           };
        }

        private void InitalizeDoors()
        {
            var factory = new StationDoorFactory(StationView.Resource);
            var x = 0;
            const int INTERVAL = 2;
            switch (CarStyle)
            {
                case CarStyle.Left :
                    x = DefaultBodySize.Width - StationDoorBase.DefaultSize.Width - INTERVAL;
                    break;
                case CarStyle.Middle :
                    x = DefaultBodySize.Width / 2 - StationDoorBase.DefaultSize.Width / 2 ;
                    break;
                case CarStyle.Right :
                    x = INTERVAL;
                    break;
                default :
                    throw new ArgumentOutOfRangeException();
            }

            LocationUpDoor = factory.CreateStationDoor(new Point(x, DefaultNameSize.Height + INTERVAL), CarNo, true);

            LocationDownDoor = factory.CreateStationDoor(new Point(x, DefaultSize.Height - StationDoorBase.DefaultSize.Height - INTERVAL), CarNo, false);
        }

        protected abstract void DrawBody(Graphics g);

        public override void OnDraw(Graphics g)
        {
            m_NameView.OnDraw(g);

            DrawBody(g);

            if (LocationUpDoor != null)
            {
                LocationUpDoor.OnPaint(g);
            }
            if (LocationDownDoor != null)
            {
                LocationDownDoor.OnPaint(g);
            }
            if (OverloadView != null)
            {
                OverloadView.OnPaint(g);
            }
        }

        public virtual void Dispose()
        {
            
        }
    }
}
