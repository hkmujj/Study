using System.Diagnostics;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace Subway.CBTC.Casco.Model.BtnStragy
{
    [DebuggerDisplay("Content={Content}")]
    public class BtnItem : NotificationObject, IRaiseResourceChangedProvider
    {private string m_Content;

        public BtnItem(string content, IBtnActionResponser actionResponser)
        {
            Content = content;
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

        public string Content
        {
            private set
            {
                if (value == m_Content)
                {
                    return;
                }

                m_Content = value;
                RaisePropertyChanged(() => Content);
            }
            get { return m_Content; }
        }

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
            RaisePropertyChanged(() => Content);
        }
    }
}