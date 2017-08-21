using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.Casco.Controller;
using Tram.CBTC.Casco.Model;
using Tram.CBTC.Casco.Model.Domain;
using Tram.CBTC.Infrasturcture.Model.Fault;
using Tram.CBTC.Infrasturcture.ViewModel.Monitor;

namespace Tram.CBTC.Casco.ViewModel
{
    [Export]
    public class DomainViewModel : NotificationObject, ICBTCProvider
    {
        [ImportingConstructor]
        public DomainViewModel(DomainController controller, DomainModel model)
        {
            Controller = controller;
            Model = model;
            Domain = new Domain(GlobalParam.Instance.InitParam);
        }

        public Domain Domain { get; set; }

        public DomainController Controller { private set; get; }

        public DomainModel Model { private set; get; }
        

        public global::Tram.CBTC.Infrasturcture.Model.CBTC CBTC { get { return Domain; } }
    }

    public class Test : NotificationObject
    {
        public static Test Instance = new Test();
        public Test()
        {
            RadarTargets = new ObservableCollection<RadarTarget>();
            //RadarTargets.Add(new RadarTarget() { BackColor = CBTCColor.Red, HorizontalDistance = 20, VerticalDistance = 100 });
            //RadarTargets.Add(new RadarTarget() { BackColor = CBTCColor.Green, HorizontalDistance = 50, VerticalDistance = 150 });
            //RadarTargets.Add(new RadarTarget() { BackColor = CBTCColor.White, HorizontalDistance = 80, VerticalDistance = 190 });
        }
        public ObservableCollection<RadarTarget> RadarTargets { get; set; }
    }
}