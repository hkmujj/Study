using System.ComponentModel.Composition;
using Engine.TCMS.Turkmenistan.Model.BtnStragy;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TCMS.Turkmenistan.Controller.BtnActionResponser
{
    [Export]
    public class BackBtnResponse : IBtnActionResponser
    {
        public BtnItem Parent { get; set; }
        public void UpdateState()
        {

        }

        public void ResponseClick()
        {
            ServiceLocator.Current.GetInstance<DomainController>().NavigateBack();
        }
    }
}
