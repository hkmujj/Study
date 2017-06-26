using System.ComponentModel.Composition;
using Engine.TCMS.Turkmenistan.Event;
using Engine.TCMS.Turkmenistan.Model;
using Engine.TCMS.Turkmenistan.Model.BtnStragy;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TCMS.Turkmenistan.Controller.BtnActionResponser
{
    /// <summary>
    /// 
    /// </summary>

    [Export]
    public class LanguageSwitch : IBtnActionResponser
    {
        public BtnItem Parent { get; set; }
        public void UpdateState()
        {

        }

        public void ResponseClick()
        {
            GlobalParam.Instance.IsTurkmenistan = !GlobalParam.Instance.IsTurkmenistan;
            var model = ServiceLocator.Current.GetInstance<DomainModel>();
            model.CurrentStateInterface.UpdateState();
         


        }
    }
}
