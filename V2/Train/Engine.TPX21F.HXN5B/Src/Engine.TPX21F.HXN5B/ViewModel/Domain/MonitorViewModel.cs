using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Controller.Domain.Monitor;
using Engine.TPX21F.HXN5B.Model.Domain.Monitor;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.ViewModel.Domain
{
    [Export]
    public class MonitorViewModel : NotificationObject
    {
        [ImportingConstructor]
        public MonitorViewModel(MonitorModel model, MonitorControlller controlller)
        {
            Model = model;
            Controlller = controlller;
            controlller.ViewModel = this;
            Controlller.Initalize();
        }

        public MonitorModel Model { private set; get; }

        public MonitorControlller Controlller { private set; get; }
    }
}