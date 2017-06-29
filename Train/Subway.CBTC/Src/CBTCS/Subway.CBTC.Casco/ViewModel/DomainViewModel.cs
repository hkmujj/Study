using System.ComponentModel.Composition;
using CBTC.Infrasturcture.ViewModel.Monitor;
using Microsoft.Practices.Prism.ViewModel;
using Subway.CBTC.Casco.Controller;
using Subway.CBTC.Casco.Model;
using Subway.CBTC.Casco.Model.Domain;

namespace Subway.CBTC.Casco.ViewModel
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

        public global::CBTC.Infrasturcture.Model.CBTC CBTC { get { return Domain; } }
    }
}