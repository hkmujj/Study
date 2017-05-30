using System.ComponentModel.Composition;
using Engine.TCMS.Turkmenistan.Extension;
using Engine.TCMS.Turkmenistan.Model.BtnStragy;
using Engine.TCMS.Turkmenistan.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TCMS.Turkmenistan.Controller.BtnActionResponser
{
    [Export]
    public class FaultViewCutBtnResponse : IBtnActionResponser
    {
        public BtnItem Parent { get; set; }
        public void UpdateState()
        {

        }

        public void ResponseClick()
        {
            var model = ServiceLocator.Current.GetInstance<DomainViewModel>().Model.FaultCutModel;
            model.SelectItem.OutCutName.SendBool(true, true);
        }
    }
    [Export]
    public class FaultViewCutRestBtnResponse : IBtnActionResponser
    {
        public BtnItem Parent { get; set; }
        public void UpdateState()
        {

        }

        public void ResponseClick()
        {
            var model = ServiceLocator.Current.GetInstance<DomainViewModel>().Model.FaultCutModel;
            model.SelectItem.OutRestCutName.SendBool(true, true);
        }
    }
}