using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Motor.HMI.CRH1A.Station.Door;
using Motor.HMI.CRH1A.Station.Overload;

namespace Motor.HMI.CRH1A.Station.Car
{
    interface IDoorViewCar : IDisposable    
    {

        CarStyle CarStyle { get; }

        CarType CarType { get; }

        string CarName { get; }

        /// <summary>
        /// 从0开始
        /// </summary>
        int CarNo { get; }


        IStationDoor LocationUpDoor { get; }

        IStationDoor LocationDownDoor { get; }

        OverloadView OverloadView { get; }

        void OnDraw(Graphics g);
    }
}
