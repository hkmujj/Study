using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using DevExpress.Mvvm;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.CBTC.Casco.Model;
using Subway.CBTC.Casco.ViewModel;

namespace Subway.CBTC.Casco.Controller
{
    [Export]
    public class MeesageControoler : ControllerBase<MessageModel>
    {
        [Import]
        private Lazy<DomainViewModel> DomainViewModel;
        public MeesageControoler()
        {
            UpCommand = new DelegateCommand(UpCommandMethod, UpCommandCanExcute);
            DownCommand = new DelegateCommand(DownCommandMethod, DownCommandCanExcute);
        }

        private bool UpCommandCanExcute()
        {
            if (ViewModel.SelectItem == null)
            {
                return false;
            }
            var index = DomainViewModel.Value.CBTC.Message.InfosCollection.IndexOf(ViewModel.SelectItem);
            if (index == 0)
            {
                return false;
            }
            return true;
        }

        private void UpCommandMethod()
        {
            if (DomainViewModel.Value.CBTC.Message.InfosCollection.Count != 0)
            {
                if (ViewModel.SelectItem == null)
                {
                    ViewModel.SelectItem = DomainViewModel.Value.CBTC.Message.InfosCollection[0];
                }
                else
                {
                    var index = DomainViewModel.Value.CBTC.Message.InfosCollection.IndexOf(ViewModel.SelectItem);
                    if (index > 0)
                    {
                        ViewModel.SelectItem = DomainViewModel.Value.CBTC.Message.InfosCollection[index - 1];
                    }
                }
            }
        }
        private bool DownCommandCanExcute()
        {
            if (ViewModel.SelectItem == null)
            {
                return false;
            }
            var index = DomainViewModel.Value.CBTC.Message.InfosCollection.IndexOf(ViewModel.SelectItem);
            if (index == DomainViewModel.Value.CBTC.Message.InfosCollection.Count - 1)
            {
                return false;
            }
            return true;
        }

        private void DownCommandMethod()
        {
            if (DomainViewModel.Value.CBTC.Message.InfosCollection.Count != 0)
            {
                if (ViewModel.SelectItem == null)
                {
                    ViewModel.SelectItem = DomainViewModel.Value.CBTC.Message.InfosCollection[0];
                }
                else
                {
                    var index = DomainViewModel.Value.CBTC.Message.InfosCollection.IndexOf(ViewModel.SelectItem);
                    if (index < DomainViewModel.Value.CBTC.Message.InfosCollection.Count - 1)
                    {
                        ViewModel.SelectItem = DomainViewModel.Value.CBTC.Message.InfosCollection[index + 1];
                    }
                }
            }
        }
        public DelegateCommand UpCommand { get; private set; }
        public DelegateCommand DownCommand { get; private set; }
    }
}
