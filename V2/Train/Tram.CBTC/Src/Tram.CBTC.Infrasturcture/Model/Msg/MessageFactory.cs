using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Tram.CBTC.Infrasturcture.Model.Msg.Details;

namespace Tram.CBTC.Infrasturcture.Model.Msg
{
    public class MessageFactory
    {
        [DebuggerStepThrough]
        public MessageFactory(ReadOnlyCollection<IInformationContent> informationContents, Message target)
        {
            InformationContents = informationContents;
            m_Target = target;
        }

        private readonly Message m_Target;

        public ReadOnlyCollection<IInformationContent> InformationContents { get; private set; }

        public virtual void CreateMessage(int id, DateTime dt = new DateTime())
        {
            var it = InformationContents.FirstOrDefault(f => f.Id == id);
            if (it != null)
            {
                var temp = m_Target.InformationItems.FirstOrDefault(f => f.InformationContent.Id == id);
                if (temp == null)
                {
                    var tmp = (IInformationContent)it.Clone();
                    tmp.HappenDate = dt;
                    m_Target.InformationItems.Add(new InformationItem(tmp));
                }
            }
        }

        public virtual void ClearMessages()
        {
            m_Target.InformationItems.Clear();
        }

        public virtual void RemoveMessage(int id)
        {
            var it = m_Target.InformationItems.FirstOrDefault(f => f.InformationContent.Id == id);
            m_Target.InformationItems.Remove(it);
        }
    }
}