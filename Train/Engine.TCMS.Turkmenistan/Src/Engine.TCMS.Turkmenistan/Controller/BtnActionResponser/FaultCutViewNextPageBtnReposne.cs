using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.TCMS.Turkmenistan.Model.BtnStragy;
using Engine.TCMS.Turkmenistan.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TCMS.Turkmenistan.Controller.BtnActionResponser
{
    [Export]
    public class FaultCutViewNextPageBtnReposne : IBtnActionResponser
    {
        public BtnItem Parent { get; set; }
        public void UpdateState()
        {

        }

        public void ResponseClick()
        {
            var model = ServiceLocator.Current.GetInstance<DomainViewModel>().Model.FaultCutModel;
            var index = model.AllUnit.IndexOf(model.SelectItem);
            model.SelectItem = index < model.AllUnit.Count - 1 ? model.AllUnit[index + 1] : model.AllUnit.FirstOrDefault();
        }
    }
}