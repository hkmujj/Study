using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;

namespace Motor.TCMS.CRH400BF.Model.BtnStragy
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