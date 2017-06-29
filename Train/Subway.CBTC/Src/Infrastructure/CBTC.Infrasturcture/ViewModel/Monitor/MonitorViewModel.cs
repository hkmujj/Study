using System;
using System.ComponentModel;
using System.Diagnostics;
using CBTC.Infrasturcture.Controller.Monitor;
using CBTC.Infrasturcture.Model.Monitor;
using CBTC.Infrasturcture.Model.Monitor.Detail;
using CBTC.Infrasturcture.Model.Send;
using Microsoft.Practices.Prism.ViewModel;
using PropertySupport = CommonUtil.Util.PropertySupport;

namespace CBTC.Infrasturcture.ViewModel.Monitor
{
    public class MonitorViewModel<T> : MonitorViewModel where T : ICBTCProvider
    {
        [DebuggerStepThrough]
        public MonitorViewModel(T cbtcProvider)
            :this(cbtcProvider, new MonitorModel(), new MonitorController())
        {
        }

        [DebuggerStepThrough]
        public MonitorViewModel(T cbtcProvider, MonitorModel model, MonitorController controller)
            : base(model, controller)
        {
            CBTCProvider = cbtcProvider;
            Model.SendeMonitor = new SendeMonitor(CBTC.SendInterface);
            CBTC.SendInterface = Model.SendeMonitor;
            CBTC.PropertyChanged += CBTCOnPropertyChanged;
        }

        private void CBTCOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == PropertySupport.ExtractPropertyName<Model.CBTC, ISendInterface>(t => t.SendInterface))
            {
                Model.SendeMonitor = new SendeMonitor(CBTC.SendInterface);
                CBTC.SendInterface = Model.SendeMonitor;
            }
        }

        public T CBTCProvider { get; private set; }

        public sealed override Model.CBTC CBTC { get { return CBTCProvider.CBTC; } }
    }

    public class MonitorViewModel : NotificationObject
    {
        [DebuggerStepThrough]
        public MonitorViewModel() : this(new MonitorModel(), new MonitorController())
        {

        }

        [DebuggerStepThrough]
        public MonitorViewModel(MonitorModel model, MonitorController controller)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
        }

        public MonitorModel Model { get; private set; }

        public MonitorController Controller { get; private set; }

        public virtual Model.CBTC CBTC { get { return null; } }
    }

    public interface ICBTCProvider
    {
        Model.CBTC CBTC { get; }
    }
}
