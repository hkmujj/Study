using System;
using System.ComponentModel.Composition;
using LightRail.HMI.GZYGDC.Controller;
using LightRail.HMI.GZYGDC.Model;
using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.GZYGDC.ViewModel
{
    [Export]
    public class TitleViewModel : NotificationObject
    {
        [ImportingConstructor]
        public TitleViewModel(TitleController controller, TitleModel model)
        {
            Controller = controller;
            //Controller.ViewModel = Lazy<TitleViewModel>(this);

            Controller.ViewModel = new Lazy<TitleViewModel>(() => this);

            Model = model;

            Controller.Initalize();
        }


        public TitleController Controller { private set; get; }

        public TitleModel Model { private set; get; }
    }
}