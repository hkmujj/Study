using System.ComponentModel.Composition;
using System.Windows.Input;
using DevExpress.Mvvm;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Other.ContactLine.JW4G.ViewModel;

namespace Other.ContactLine.JW4G.Controller
{
    [Export]
    public class ContactLineController : ControllerBase<ContactLineViewModel>
    {
        [ImportingConstructor]
        public ContactLineController(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            RegionManager = regionManager;
            EventAggregator = eventAggregator;
            UpCommand = new DelegateCommand(UpAction);
            DownCommand = new DelegateCommand(DownAction);
        }

        private void UpAction()
        {
            ViewModel.Model.Index--;
        }

        private void DownAction()
        {
            ViewModel.Model.Index++;
        }

        protected IEventAggregator EventAggregator { get; private set; }
        public IRegionManager RegionManager { get; private set; }

        public ICommand UpCommand { get; private set; }
        public ICommand DownCommand { get; private set; }
    }
}