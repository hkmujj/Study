using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using Microsoft.Practices.Prism.ViewModel;
using Motor.ATP.Domain.Interface.UserAction;

namespace Motor.ATP.Domain.Model.UserAction
{
    [DebuggerDisplay("Id = {Id} Type = {UserActionType} Content = {Content} Enabled = {Enabled} Visible = {Visible}")]
    public class DriverSelectableItem : NotificationObject, IDriverSelectableItem
    {
        private static long m_LastGlobalId = 0;

        private long m_Id;
        private string m_Content;
        private Image m_Backgroud;
        private bool m_Enabled;
        private bool m_Visible;
        private object m_Tag;

        private readonly IDriverSelectableItemStateProvider m_StateProvider;
        private bool m_OutlineVisible;
        private string m_RawContent;

        public DriverSelectableItem(string content, Enum userActionType, Image backgroud,
            IDriverSelectableItemStateProvider stateProvider, object tag)
        {
            Contract.Requires(stateProvider != null);

            UserActionType = userActionType;
            Tag = tag;
            m_StateProvider = stateProvider;
            m_StateProvider.PropertyChanged += StateProviderOnPropertyChanged;
            Visible = m_StateProvider.Visible;
            Enabled = m_StateProvider.Enabled;
            OutlineVisible = m_StateProvider.OutlineVisible;
            Backgroud = backgroud;
            Content = content;
            m_RawContent = content;
            Id = ++m_LastGlobalId;
        }

        private void StateProviderOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            Visible = m_StateProvider.Visible;
            Enabled = m_StateProvider.Enabled;
            OutlineVisible = m_StateProvider.OutlineVisible;
            if (!string.IsNullOrWhiteSpace(m_StateProvider.ChangedContent))
            {
                Content = m_StateProvider.ChangedContent;
            }
            else
            {
                Content = m_RawContent;
            }
        }

        public long Id
        {
            get { return m_Id; }
            private set
            {
                if (value == m_Id)
                {
                    return;
                }
                m_Id = value;
                RaisePropertyChanged(() => Id);
            }
        }

        public Enum UserActionType { get; private set; }

        public string Content
        {
            get { return m_Content; }
            private set
            {
                if (value == m_Content)
                {
                    return;
                }
                m_Content = value;
                RaisePropertyChanged(() => Content);
            }
        }

        public Image Backgroud
        {
            get { return m_Backgroud; }
            private set
            {
                if (Equals(value, m_Backgroud))
                {
                    return;
                }
                m_Backgroud = value;
                RaisePropertyChanged(() => Backgroud);
            }
        }

        public bool Enabled
        {
            get { return m_Enabled; }
            private set
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
            private set
            {
                if (value.Equals(m_Visible))
                {
                    return;
                }
                m_Visible = value;
                RaisePropertyChanged(() => Visible);
            }
        }

        public bool OutlineVisible
        {
            get { return m_OutlineVisible; }
            set
            {
                if (value.Equals(m_OutlineVisible))
                {
                    return;
                }
                m_OutlineVisible = value;
                RaisePropertyChanged(() => OutlineVisible);
            }
        }

        public IDriverActionResponser ActionResponser { get; set; }

        /// <summary>
        /// 父结点
        /// </summary>
        public IDriverSelectable Parent { get; set; }

        public object Tag
        {
            get { return m_Tag; }
            private set
            {
                if (Equals(value, m_Tag))
                {
                    return;
                }
                m_Tag = value;
                RaisePropertyChanged(() => Tag);
            }
        }

        public void Initalize()
        {
            m_StateProvider.Initalize(this);            
        }

        public void UpdateStates()
        {
            m_StateProvider.UpdateState();
        }
    }
}