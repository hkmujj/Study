using Microsoft.Practices.Prism.ViewModel;

namespace Urban.GuiYang.DDU.Model.Train.CarPart
{
    public class CarPartBase : NotificationObject
    {
        public Car Car { protected get; set; }
    }
}