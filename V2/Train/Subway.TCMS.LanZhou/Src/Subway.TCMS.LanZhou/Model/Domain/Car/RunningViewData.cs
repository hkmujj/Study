using System;
using Microsoft.Practices.Prism.ViewModel;
using Subway.TCMS.LanZhou.Model.Domain.Car.CarParts;

namespace Subway.TCMS.LanZhou.Model.Domain.Car
{
    public class RunningViewData : NotificationObject
    {
        public Lazy<RunningViewFloatData> RunningViewFloatDataLazy { get; set; }
    }
}
