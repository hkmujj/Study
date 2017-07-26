using System.ComponentModel;
using System.Linq;
using CommonUtil.Util;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Model;

namespace Motor.ATP._200C.Subsys.Model.Message
{
    public class ATP200CMessage : Infrasturcture.Model.Message.Message
    {
        private IMessageItem m_CurrentFlickingMessageItem;

        public ATP200CMessage(ATPDomain atp) : base(atp)
        {
            MessageCollection.CollectionChanged += (sender, args) =>
            {
                UpdateCurrentFlickingMessageItem();
            };
        }

        private void UpdateCurrentFlickingMessageItem()
        {
            if (CurrentFlickingMessageItem != null)
            {
                CurrentFlickingMessageItem.PropertyChanged -= CurrentFlickingMessageItemOnPropertyChanged;
            }

            CurrentFlickingMessageItem = MessageCollection.FirstOrDefault(f => f.Style == MessageStyle.FlashFrame);

            if (CurrentFlickingMessageItem != null)
            {
                CurrentFlickingMessageItem.PropertyChanged += CurrentFlickingMessageItemOnPropertyChanged;
            }
        }

        private void CurrentFlickingMessageItemOnPropertyChanged(object sender,
            PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName ==
                PropertySupport.ExtractPropertyName<IMessageItem, MessageStyle>(m => m.Style))
            {
                UpdateCurrentFlickingMessageItem();
            }
        }

        public IMessageItem CurrentFlickingMessageItem
        {
            get { return m_CurrentFlickingMessageItem; }
            set
            {
                if (Equals(value, m_CurrentFlickingMessageItem))
                {
                    return;
                }

                m_CurrentFlickingMessageItem = value;
                RaisePropertyChanged(() => CurrentFlickingMessageItem);
            }
        }
    }
}