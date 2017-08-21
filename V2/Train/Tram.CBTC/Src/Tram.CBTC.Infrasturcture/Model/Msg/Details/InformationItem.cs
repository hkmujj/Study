using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;

namespace Tram.CBTC.Infrasturcture.Model.Msg.Details
{
    public class InformationItem : NotificationObject
    {
        [DebuggerStepThrough]
        public InformationItem(IInformationContent informationContent)
        {
            InformationContent = informationContent;
        }

        public IInformationContent InformationContent { get; internal set; }
    }
}