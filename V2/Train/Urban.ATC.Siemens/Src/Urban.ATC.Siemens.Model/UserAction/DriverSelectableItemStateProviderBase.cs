using System.Diagnostics.Contracts;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Service;
using Motor.ATP.Domain.Interface;
using Motor.ATP.Domain.Interface.Service;
using Motor.ATP.Domain.Interface.UserAction;

namespace Motor.ATP.Domain.Model.UserAction
{
    public class DriverSelectableItemStateProviderBase : NotificationObject, IDriverSelectableItemStateProvider
    {
        private bool m_Enabled;
        private bool m_Visible;
        private DriverSelectedType m_SelectedType;
        private bool m_OutlineVisible;

        private ICommunicationDataService m_CommunicationDataService;

        private IInterfaceAdapterService m_InterfaceAdapterService;
        private string m_ChangedContent;

        protected IInterfaceAdapterService InterfaceAdapterService
        {
            get
            {
                return m_InterfaceAdapterService ??
                       (m_InterfaceAdapterService = App.Current.ServiceManager.GetService<IInterfaceAdapterService>());
            }
        }

        protected IDriverSelectableItem DriverSelectableItem { private set; get; }

        protected IATP ATP
        {
            get { return DriverSelectableItem.GetATP(); }
        }

        public bool Enabled
        {
            get { return m_Enabled; }
            protected set
            {
                if (value.Equals(m_Enabled))
                {
                    return;
                }

                m_Enabled = value;
                RaisePropertyChanged(() => Enabled);
            }
        }

        public bool Visible
        {
            get { return m_Visible; }
            protected set
            {
                if (value.Equals(m_Visible))
                {
                    return;
                }

                m_Visible = value;
                RaisePropertyChanged(() => Visible);
            }
        }

        /// <summary>
        /// 外边是否可见
        /// </summary>
        public bool OutlineVisible
        {
            get { return m_OutlineVisible; }
            protected set
            {
                if (value.Equals(m_OutlineVisible))
                {
                    return;
                }

                m_OutlineVisible = value;
                RaisePropertyChanged(() => OutlineVisible);
            }
        }

        /// <summary>
        /// 改变后的内容
        /// </summary>
        public string ChangedContent
        {
            get { return m_ChangedContent; }
            set
            {
                if (value == m_ChangedContent)
                {
                    return;
                }

                m_ChangedContent = value;
                RaisePropertyChanged(() => ChangedContent);
            }
        }

        public DriverSelectedType SelectedType
        {
            get { return m_SelectedType; }
            protected set
            {
                if (value == m_SelectedType)
                {
                    return;
                }

                m_SelectedType = value;
                RaisePropertyChanged(() => SelectedType);
            }
        }


        public DriverSelectableItemStateProviderBase()
        {
            Visible = true;
            Enabled = true;
            SelectedType = DriverSelectedType.Relase;
        }

        public virtual void Initalize(IDriverSelectableItem item)
        {
            Contract.Requires(item != null);
            DriverSelectableItem = item;
        }

        public virtual void UpdateState()
        {

        }
    }
}