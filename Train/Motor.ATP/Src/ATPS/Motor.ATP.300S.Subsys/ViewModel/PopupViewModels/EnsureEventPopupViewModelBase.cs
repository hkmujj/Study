using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP._300S.Subsys.Constant;

//.PopupView;

namespace Motor.ATP._300S.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureEventPopupViewModelBase : DriverPopupViewModelBase, IContentOfEnsurce
    {
        private string m_EnsurceContent;

        public string EnsurceContent
        {
            set
            {
                if (value == m_EnsurceContent)
                {
                    return;
                }
                m_EnsurceContent = value;
                RaisePropertyChanged(() => EnsurceContent);
            }
            get { return m_EnsurceContent; }
        }

        public EnsureEventPopupViewModelBase()
        {
            PopupViewName = PopupContentViewNames.EnsureEventView;
        }
    }
}