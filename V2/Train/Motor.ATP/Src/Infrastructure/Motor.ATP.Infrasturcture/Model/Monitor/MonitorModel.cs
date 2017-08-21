using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.ViewModel;
using Motor.ATP.Infrasturcture.Model.Monitor.Message;
using Motor.ATP.Infrasturcture.Model.Monitor.ViewControl;

namespace Motor.ATP.Infrasturcture.Model.Monitor
{
    public class MonitorModel : NotificationObject
    {
        public MonitorMessage MonitorMessage { get; private set; }

        public ReadOnlyCollection<ForecastInformationItem> ForecastInformationItems { get; set; }

        public ViewControlModel ViewControlModel { get; private set; }

        public MonitorModel()
        {
            MonitorMessage = new MonitorMessage();
            ViewControlModel = new ViewControlModel();
        }
    }
}