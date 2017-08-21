using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Model.Domain.SecondLevel.Details;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.Model.Domain.SecondLevel
{
    [Export]
    public class SetAccDataModel : NotificationObject
    {
        private AccDataItem m_CurrentSelectedAccDataItem;
        private bool m_HasAnyModified;
        private bool m_IsSureModify;
        public Lazy<ReadOnlyCollection<AccDataItem>> AccDataItemCollection { get; set; }

        public AccDataItem CurrentSelectedAccDataItem
        {
            get { return m_CurrentSelectedAccDataItem; }
            set
            {
                if (Equals(value, m_CurrentSelectedAccDataItem))
                {
                    return;
                }

                m_CurrentSelectedAccDataItem = value;
                if (null != m_CurrentSelectedAccDataItem)
                {
                    m_CurrentSelectedAccDataItem.BindableCaretIndex = 0;
                    m_CurrentSelectedAccDataItem.Text = m_CurrentSelectedAccDataItem.Value.ToString("0");
                }
                RaisePropertyChanged(() => CurrentSelectedAccDataItem);
            }
        }

        public bool HasAnyModified
        {
            set
            {
                if (value == m_HasAnyModified)
                {
                    return;
                }

                m_HasAnyModified = value;
                RaisePropertyChanged(() => HasAnyModified);
            }
            get { return m_HasAnyModified; }
        }

        public bool IsSureModify
        {
            set
            {
                if (value == m_IsSureModify)
                {
                    return;
                }

                m_IsSureModify = value;
                RaisePropertyChanged(() => IsSureModify);
            }
            get { return m_IsSureModify; }
        }
    }
}