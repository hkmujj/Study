using System.Diagnostics;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace Subway.TCMS.LanZhou.Model.BtnStragy
{
    [DebuggerDisplay("Content={StateProvider.Content}")]
    public class BtnItem : NotificationObject, IRaiseResourceChangedProvider
    {
        public BtnItem(string content, IBtnActionResponser actionResponser)
        {
            StateProvider = new BtnStateProvider() {Content = content};
            
            ActionResponser = actionResponser;
            if (actionResponser != null)
            {
                actionResponser.Parent = this;
            }
            ClickCommand = new DelegateCommand(OnClick);
        }

        private void OnClick()
        {
            if (ActionResponser != null)
            {
                ActionResponser.ResponseClick();
            }
        }

        public BtnStateProvider StateProvider { get; private set; }

        public DelegateCommand ClickCommand { private set; get; }

        public IBtnActionResponser ActionResponser { private set; get; }

        public void UpdateState()
        {
            if (ActionResponser != null)
            {
                ActionResponser.UpdateState();
            }
        }

        public void RaiseResourceChanged()
        {
            RaisePropertyChanged(() => StateProvider.Content);
        }

    }
}