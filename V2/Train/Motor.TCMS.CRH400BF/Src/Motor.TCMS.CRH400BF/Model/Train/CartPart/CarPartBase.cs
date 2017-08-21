using Microsoft.Practices.Prism.ViewModel;

namespace Motor.TCMS.CRH400BF.Model.Train.CartPart
{
    public class CarPartBase : NotificationObject
    {
        public Car Car { protected get; set; }
    }
}
