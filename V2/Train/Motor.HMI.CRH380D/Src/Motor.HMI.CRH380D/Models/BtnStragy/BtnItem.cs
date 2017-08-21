using System.Diagnostics;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace Motor.HMI.CRH380D.Models.BtnStragy
{
    [DebuggerDisplay("Content={Content}")]
    public class BtnItem : NotificationObject, IRaiseResourceChangedProvider
    {
        private string m_Content;
        private bool m_IsEnable;

        public BtnItem(string content, IBtnActionResponser actionResponser)
        {
            Content = content;
            ActionResponser = actionResponser;
            if (actionResponser != null)
            {
                actionResponser.Parent = this;
            }
            ClickCommand = new DelegateCommand(OnClick);
            IsEnable = true;
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

        public bool IsEnable
        {
            get { return m_IsEnable; }
            set
            {
                if (value == m_IsEnable)
                {
                    return;
                }
                m_IsEnable = value;
                RaisePropertyChanged(() => IsEnable);
            }
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