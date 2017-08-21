using System.ComponentModel.Composition;
using System.Windows.Input;
using Engine._6A.Constance;
using Engine._6A.Event;
using Engine._6A.EventArgs;
using Engine._6A.Interface.ViewModel;
using Engine._6A.ViewModel.Common;
using Microsoft.Practices.Prism.Commands;

namespace Engine._6A.ViewModel.Axle8
{
    [Export(ContractName.Axle8, typeof(IButtonViewModel))]
    public class Axle8ButtonViewModel : ViewModelBase, IAxle8ButtonViewModel
    {
        public Axle8ButtonViewModel()
        {
            ChangedTrain = new DelegateCommand<string>(ChangedTrainMethod);
            Navigator = new DelegateCommand<string>(NavigatorMethod);
        }

        private void NavigatorMethod(string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return;
            }
            EventAggregator.GetEvent<NavigateEvent>().Publish(new NavigateEventArgs()
            {
                Region = RegionNames.ContentRegion,
                Name = obj,
            });

        }

        private void ChangedTrainMethod(string obj)
        {

        }

        public ICommand Navigator { get; private set; }
        public ICommand ChangedTrain { get; private set; }
    }
}