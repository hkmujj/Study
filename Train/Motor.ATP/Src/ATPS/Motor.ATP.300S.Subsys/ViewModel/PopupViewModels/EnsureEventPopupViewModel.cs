using System.ComponentModel.Composition;
using MMI.Facility.Interface;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Interface.Service;

//.PopupView;

namespace Motor.ATP._300S.Subsys.ViewModel.PopupViewModels
{

    [Export]
    public class EnsureEventPopupViewModel : EnsureEventPopupViewModelBase
    {
        public IInfomationItem InfomationItem
        {
            set
            {
                if (Equals(value, m_InfomationItem))
                {
                    return;
                }
                m_InfomationItem = value;
                RaisePropertyChanged(() => InfomationItem);
            }
            get { return m_InfomationItem; }
        }

        private readonly IInfomationService m_InfomationService;
        private IInfomationItem m_InfomationItem;

        public EnsureEventPopupViewModel()
        {
            m_InfomationService = App.Current.ServiceManager.GetService<IInfomationService>(ATPType.ATP300S.ToString());
        }

        protected override void UpdateState()
        {
            InfomationItem = m_InfomationService.CurrentEnsureingInfomation;
            TitleContent = InfomationItem.Content.PopupTitle;
            EnsurceContent = InfomationItem.Content.PopupContent;
        }
    }
}