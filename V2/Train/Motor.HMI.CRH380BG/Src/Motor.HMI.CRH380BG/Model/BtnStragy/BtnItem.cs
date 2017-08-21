using System.Diagnostics;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Motor.HMI.CRH380BG.ViewModel;

namespace Motor.HMI.CRH380BG.Model.BtnStragy
{
    [DebuggerDisplay("Content={Content}")]
    public class BtnItem : NotificationObject, IRaiseResourceChangedProvider
    {
        private string m_Content;
        private bool m_IsSelected;

        public BtnItem(string content, bool isSelected, IBtnActionResponser actionResponser)
        {
            Content = content;
            IsSelected = isSelected;
            ActionResponser = actionResponser;
            if (actionResponser != null)
            {
                actionResponser.Parent = this;
            }
            ClickCommand = new DelegateCommand<StateViewModel>(OnClick);
        }

        private void OnClick(StateViewModel stateViewModel)
        {
            if (ActionResponser != null)
            {
                ActionResponser.ResponseClick(stateViewModel);
            }
        }

        public bool IsSelected
        {
            private set
            {
                if (value == m_IsSelected)
                {
                    return;
                }

                m_IsSelected = value;
                RaisePropertyChanged(() => IsSelected);
            }
            get { return m_IsSelected; }
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

        public DelegateCommand<StateViewModel> ClickCommand { private set; get; }

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