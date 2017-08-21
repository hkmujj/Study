using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.ShenZhenLine11.Config;
using Subway.ShenZhenLine11.Event;
using Subway.ShenZhenLine11.ViewModels;

namespace Subway.ShenZhenLine11.Controller
{
    [Export]
    public class RestViewController : ControllerBase<ReSetViewModel>
    {
        public RestViewController()
        {
            SendValue = new DelegateCommand<string>(SendValueAction);
            SendBoolDataEvent = ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<SendDataEvent<bool>>();
        }

        private void SendValueAction(string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return;
            }
            SendBoolDataEvent.Publish(new SendDataEnvetArgs<bool>()
            {
                Index = GlobalParam.Instance.IndexConfig.OutBoolDescriptionDictionary[obj],
                Value = true,
                IsClear = true,
            });
        }

        private readonly SendDataEvent<bool> SendBoolDataEvent;

        public ICommand SendValue { get; private set; }
    }
}