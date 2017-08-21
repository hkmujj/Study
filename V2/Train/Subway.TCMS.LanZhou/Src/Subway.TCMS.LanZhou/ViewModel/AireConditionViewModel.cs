using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Subway.TCMS.LanZhou.ViewModel
{
    [Export]
    public class AireConditionViewModel : NotificationObject
    {
        protected DomainViewModel Domain { get; private set; }

        public void Initalize(DomainViewModel domain)
        {
            Domain = domain;
        }
    }
}