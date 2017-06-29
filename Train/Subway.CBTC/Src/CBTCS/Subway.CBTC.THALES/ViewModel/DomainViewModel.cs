using System.ComponentModel.Composition;
using CBTC.Infrasturcture.ViewModel.Monitor;
using Microsoft.Practices.Prism.ViewModel;
using Subway.CBTC.THALES.Controller;
using Subway.CBTC.THALES.Model;
using Subway.CBTC.THALES.Model.Domain;

namespace Subway.CBTC.THALES.ViewModel
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