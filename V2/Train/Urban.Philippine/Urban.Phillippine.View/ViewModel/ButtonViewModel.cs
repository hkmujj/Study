using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Urban.Phillippine.View.Constant;
using Urban.Phillippine.View.Event;
using Urban.Phillippine.View.EventArgs;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Phillippine.View.ViewModel
{
    [Export(typeof(IButtonViewModel))]
    public class ButtonViewModel : ViewModelBase, IButtonViewModel
    {
        public ButtonViewModel()
        {
            ChangedContent = new DelegateCommand<string>(agrs =>
            {
                if (string.IsNullOrEmpty(agrs))
                {
                    return;
                }
                EventAggregator.GetEvent<NavigatorEvent>().Publish(new NavigatorEventArgs
                {
                    Region = RegionNames.ContentRegion,
                    Name = agrs
                });
            });
            ChangedMain = new DelegateCommand<string>(agrs =>
            {
                if (string.IsNullOrEmpty(agrs))
                {
                    return;
                }
                EventAggregator.GetEvent<NavigatorEvent>().Publish(new NavigatorEventArgs
                {
                    Region = RegionNames.MainShellRegion,
                    Name = agrs
                });
            });
            ChangedContentButton = new DelegateCommand<string>((args) =>
              {
                  if (string.IsNullOrEmpty(args))
                  {
                      return;
                  }
                  EventAggregator.GetEvent<NavigatorEvent>().Publish(new NavigatorEventArgs
                  {
                      Region = RegionNames.ContentAndButtonRegion,
                      Name = args
                  });
              });
        }

        public ICommand ChangedContent { get; set; }
        public ICommand ChangedMain { get; set; }
        public ICommand ChangedContentButton { get; set; }
    }
}