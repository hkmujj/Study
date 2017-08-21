using System.ComponentModel.Composition;
using Engine.TCMS.HXD3.Controller;
using Engine.TCMS.HXD3.Model.TCMS;
using Engine.TCMS.HXD3.ViewModel.Domain;

namespace Engine.TCMS.HXD3.ViewModel
{
    [Export]
    public class HXD3TCMSViewModel
    {
        [ImportingConstructor]
        public HXD3TCMSViewModel(HXD3TCMSModel model, HXD3TCMSController controller, TCMSViewModel tcms)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            model.Parent = this;
            TCMS = tcms;
            TCMS.Parent = this;
        }

        public HXD3TCMSController Controller { private set; get; }

        public HXD3TCMSModel Model { private set; get; }

        public TCMSViewModel TCMS { private set; get; }
    }
}