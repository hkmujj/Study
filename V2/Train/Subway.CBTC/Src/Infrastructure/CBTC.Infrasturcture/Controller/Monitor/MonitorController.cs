using System.ComponentModel;
using System.Linq;
using CBTC.Infrasturcture.Model.Monitor;
using CBTC.Infrasturcture.Model.Monitor.Detail;
using CBTC.Infrasturcture.Model.Msg.Details;
using CBTC.Infrasturcture.ViewModel.Monitor;
using MMI.Facility.WPFInfrastructure.Interfaces;
using PropertySupport = CommonUtil.Util.PropertySupport;

namespace CBTC.Infrasturcture.Controller.Monitor
{
    public class MonitorController : ControllerBase<MonitorViewModel>
    {
        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            ViewModel.Model.MsgCreater.PropertyChanged += MsgCreaterOnPropertyChanged;
        }

        private void MsgCreaterOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == PropertySupport.ExtractPropertyName<MsgCreater, bool>(m => m.Creat))
            {
                if (ViewModel.Model.MsgCreater.Creat)
                {
                    ViewModel.CBTC.Message.InformationItems.Add(
                        new InformationItem(new InformationContent(ViewModel.Model.MsgCreater.Id, "",
                            ViewModel.Model.MsgCreater.Content)));
                }
            }
            else if (propertyChangedEventArgs.PropertyName == PropertySupport.ExtractPropertyName<MsgCreater, bool>(m => m.Delete))
            {
                if (ViewModel.Model.MsgCreater.Delete)
                {
                    var it =
                        ViewModel.CBTC.Message.InformationItems.LastOrDefault(
                            l => l.InformationContent.Id == ViewModel.Model.MsgCreater.Id);
                    ViewModel.CBTC.Message.InformationItems.Remove(it);
                }
            }
        }
    }
}