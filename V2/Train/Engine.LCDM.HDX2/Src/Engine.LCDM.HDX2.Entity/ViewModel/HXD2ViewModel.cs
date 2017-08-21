

using System.ComponentModel.Composition;
using Engine.LCDM.HDX2.Entity.Controller;
using Engine.LCDM.HDX2.Entity.Model;
using Engine.LCDM.HDX2.Entity.Model.Domain;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.LCDM.HDX2.Entity.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class HXD2ViewModel : NotificationObject
    {
        [ImportingConstructor]
        public HXD2ViewModel(LCDMModel lcdmModel, HXD2Model model, HXD2Controller controller)
        {
            LCDMModel = lcdmModel;
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            //controller.Initalize();
        }

        public HXD2Controller Controller { private set; get; }

        public LCDMModel LCDMModel { private set; get; }

        public HXD2Model Model { private set; get; }
    }
}