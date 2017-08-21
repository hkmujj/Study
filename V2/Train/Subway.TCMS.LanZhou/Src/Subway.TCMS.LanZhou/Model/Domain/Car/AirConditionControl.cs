using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using Subway.TCMS.LanZhou.Model.Domain.Car.CarParts;

namespace Subway.TCMS.LanZhou.Model.Domain.Car
{
   public class AirConditionControl : NotificationObject
    {
        public Lazy<AirConditionFloatData> AirConditionFloatDataLazy { set; get; }
        public Lazy<AirConditionStatusData> AirConditionStatusLazy { get; set; }
    }
}
