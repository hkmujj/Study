using System.Diagnostics;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.Model.BtnStragy
{
    [DebuggerDisplay("Content={Content}")]
    public class BtnItem : NotificationObject, IRaiseResourceChangedProvider
    {private string m_Content;

        public BtnItem(string content, IBtnActionResponser actionResponser, bool isSelected = false)
        {
            Content = content;
            ActionResponser = actionResponser;
            IsSelected = isSelected;
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

        public bool IsSelected { get; private set; }

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