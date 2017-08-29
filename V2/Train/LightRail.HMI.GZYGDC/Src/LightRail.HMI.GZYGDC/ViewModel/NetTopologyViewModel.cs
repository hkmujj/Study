using System;
using System.ComponentModel.Composition;
using LightRail.HMI.GZYGDC.Controller;
using LightRail.HMI.GZYGDC.Model;
using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.GZYGDC.ViewModel
{
    [Export]
    public class NetTopologyViewModel : NotificationObject
    {
        [ImportingConstructor]
        public NetTopologyViewModel(NetTopologyController controller, NetTopologyModel model)
        {
            Controller = controller;

            Controller.ViewModel = new Lazy<NetTopologyViewModel>(() => this);

            Model = model;

            Controller.Initalize();

            Instance = this;
        }

        public static NetTopologyViewModel Instance { private set; get; }

        public NetTopologyController Controller { private set; get; }

        public NetTopologyModel Model { private set; get; }
    }
}