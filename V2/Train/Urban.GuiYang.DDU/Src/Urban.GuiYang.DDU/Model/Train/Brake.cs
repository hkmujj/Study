using System;
using Microsoft.Practices.Prism.ViewModel;
using Urban.GuiYang.DDU.Model.Train.CarPart;

namespace Urban.GuiYang.DDU.Model.Train
{
    public class Brake : NotificationObject
    {

        public Lazy<BrakePage1> BrakePage1 { get; set; }
        public Lazy<BrakePage2> BrakePage2 { get; set; }
        public Lazy<BrakePage3> BrakePage3 { get; set; }
    }
}