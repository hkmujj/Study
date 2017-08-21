using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;
using Motor.ATP.Infrasturcture.Controller.Monitor;
using Motor.ATP.Infrasturcture.Model;
using Motor.ATP.Infrasturcture.Model.Monitor;

namespace Motor.ATP.Infrasturcture.ViewModel
{
    public class MonitorViewModel : NotificationObject
    {
        [DebuggerStepThrough]
        public MonitorViewModel(ATPDomain domain)
            : this(domain, new MonitorModel(), new MonitorController())
        {
        }

        [DebuggerStepThrough]
        public MonitorViewModel(ATPDomain domain, MonitorController controller)
            : this(domain, new MonitorModel(), controller)
        {
        }

        [DebuggerStepThrough]
        public MonitorViewModel(ATPDomain domain, MonitorModel monitorModel, MonitorController controller)
        {
            Domain = domain;
            Model = monitorModel;
            Controller = controller;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public MonitorModel Model { get; private set; }

        public MonitorController Controller { get; private set; }

        public ATPDomain Domain { get; private set; }

    }
}