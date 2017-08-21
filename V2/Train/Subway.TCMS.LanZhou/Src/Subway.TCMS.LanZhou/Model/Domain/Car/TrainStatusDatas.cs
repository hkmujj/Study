using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using Subway.TCMS.LanZhou.Model.Domain.Car.CarParts;

namespace Subway.TCMS.LanZhou.Model.Domain.Car
{
    public class TrainStatusDatas : NotificationObject
    {
        public Lazy<TrainStatusTowView> TrainStatusTowViewLazy { get; set; }
        public Lazy<TrainStatusBrakeView> TrainStatusBrakeViewLazy { get; set; }
        public Lazy<TrainStatusAsisstView> TrainStatusAsisstViewLazy { get; set; }
    }
}
