using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using CommonUtil.Controls;
using CommonUtil.Controls.Roundness;
using CommonUtil.Model;
using CommonUtil.Util;
using CommonUtil.Util.Extension;
using Motor.HMI.CRH1A.Common.EventArg;
using Motor.HMI.CRH1A.Common.Global;
using Motor.HMI.CRH1A.Common.Train;

namespace Motor.HMI.CRH1A.Station.Car
{
    class DriverCar : DoorViewCarBase
    {

        public DriverCar(int carNo, CarStyle carStyle, DriverLocation driverLocation, GT_Station stationView)
            : base(carNo, carStyle, stationView)
        {
            DriverLocation = driverLocation;

            InitalizeBodyPath();

            InitalizeDriverState();

            GlobalEvent.Instance.DriverLocationChanged += OnDriverLocationChanged;
        }

        private GraphicsPath m_BodyPath;

        public DriverLocation DriverLocation { private set; get; }

        private GDIRoundness m_DriverState;

        private Matrix GetTurnMatrixOfBody()
        {
            return MatrixHelper.CreateTurnMatrix(m_BodyPath.GetBounds().GetCenterPoint().X, TurnOrientation.Horizontal);
        }

        private void InitalizeBodyPath()
        {
            const int X = 0;
            int y = DefaultNameSize.Height;
            m_BodyPath = new GraphicsPath();
            m_BodyPath.AddLine(new Point(X + 15, y + 0), new Point(X + 80, y + 0));
            m_BodyPath.AddLine(new Point(X + 80, y + 0), new Point(X + 80, y + 70));
            m_BodyPath.AddLine(new Point(X + 80, y + 70), new Point(X + 15, y + 70));
            m_BodyPath.AddLine(new Point(X + 15, y + 70), new Point(X + 0, y + 50));
            m_BodyPath.AddLine(new Point(X + 0, y + 50), new Point(X + 0, y + 20));
            m_BodyPath.AddLine(new Point(X + 0, y + 20), new Point(X + 15, y + 0));

            m_BodyPath.CloseAllFigures();

            if (CarStyle == CarStyle.Right)
            {
                m_BodyPath.Transform(GetTurnMatrixOfBody());
            }
        }


        private void InitalizeDriverState()
        {
            var body = m_BodyPath.GetBounds();
            const int INTERVAL = 2;
            const int R = 5;

            m_DriverState = new GDIRoundness()
            {
                R = R,
                ContentColor = Color.Black,
                NeedDrawContent = true,
                Visible = false,
                Center = new Point(R + INTERVAL, (int)body.GetCenterPoint().Y),
            };

            if (CarStyle == CarStyle.Right)
            {
                m_DriverState.Center = new Point((int) ( body.Width - R - INTERVAL ), (int) body.GetCenterPoint().Y);
            }
        }


        protected override void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            base.OnOutLineChanged(sender, eventArgs);

            m_DriverState.Center = LocationChangeMatrix.TransformPoint(m_DriverState.Center);

            m_BodyPath.Transform(LocationChangeMatrix);
        }


        private void OnDriverLocationChanged(object sender, EventArgs<ChangedArgs<DriverLocation>> args)
        {
            m_DriverState.Visible = args.Arg.NewValue == DriverLocation;
        }

        protected override void DrawBody(Graphics g)
        {
            m_DriverState.OnDraw(g);

            g.DrawPath(CommonResouce.BlackPen, m_BodyPath);
        }

        public override void Dispose()
        {
            GlobalEvent.Instance.DriverLocationChanged -= OnDriverLocationChanged;
        }
    }
}
