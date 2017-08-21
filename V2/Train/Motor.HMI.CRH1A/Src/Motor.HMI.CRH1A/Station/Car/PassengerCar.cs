using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using CommonUtil.Controls;

namespace Motor.HMI.CRH1A.Station.Car
{
    class PassengerCar : DoorViewCarBase
    {
        public PassengerCar(int carNo, CarStyle carStyle, GT_Station stationView)
            : base(carNo, carStyle,stationView)
        {
        }

        protected override void DrawBody(Graphics g)
        {
            g.DrawRectangle(CommonResouce.BlackPen, BodyBounds);
        }
    }
}
