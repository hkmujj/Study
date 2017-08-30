
using CommonUtil.Util;
using Subway.TCMS.Infrasturcture.Controller;
using Subway.TCMS.Infrasturcture.Model.Monitor;
using Subway.TCMS.Infrasturcture.Model.Monitor.Detail;
using Subway.TCMS.Infrasturcture.Model.Recive;
using Subway.TCMS.Infrasturcture.Model.Send;

namespace Subway.TCMS.Infrasturcture.ViewModel
{
    public class MonitorViewModel<T> : MonitorViewModel where T : Model.TCMS
    {
        public MonitorViewModel(T tcms) : base(new MonitorModel(), new MonitorController())
        {
            TCMS = tcms;
            MonitorModel.SendMonitor = new SendMonitor(tcms.SendInterface);
            MonitorModel.ReciveMonitor = new ReciveMonitor(tcms.ReciveInterface);
            TCMS.SendInterface = MonitorModel.SendMonitor;
            TCMS.ReciveInterface = MonitorModel.ReciveMonitor;
            TCMS.PropertyChanged += TCMS_PropertyChanged;
        }

        private void TCMS_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == PropertySupport.ExtractPropertyName<Model.TCMS, ISendInterface>(t => t.SendInterface))
            {
                MonitorModel.SendMonitor = new SendMonitor(TCMS.SendInterface);
                TCMS.SendInterface = MonitorModel.SendMonitor;
            }
            if (e.PropertyName == PropertySupport.ExtractPropertyName<Model.TCMS, IReciveInterface>(t => t.ReciveInterface))
            {
                MonitorModel.ReciveMonitor = new ReciveMonitor(TCMS.ReciveInterface);
                TCMS.ReciveInterface = MonitorModel.ReciveMonitor;
            }
        }
    }

    public class MonitorViewModel
    {
        public MonitorViewModel() : this(new MonitorModel(), new MonitorController()) { }
        public MonitorViewModel(MonitorModel monitorModel, MonitorController monitorController)
        {
            MonitorModel = monitorModel;
            MonitorController = monitorController;
        }
        public MonitorController MonitorController { get; private set; }
        public MonitorModel MonitorModel { get; private set; }
        public Model.TCMS TCMS { get; protected set; }
    }
}
