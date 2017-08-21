using Microsoft.Practices.Prism.ViewModel;

namespace Subway.TCMS.LanZhou.Model.Domain.Car.CarParts
{
    public class CarPartBase : NotificationObject
    {
        public CarModel CarModel { protected get; set; }

    }
}