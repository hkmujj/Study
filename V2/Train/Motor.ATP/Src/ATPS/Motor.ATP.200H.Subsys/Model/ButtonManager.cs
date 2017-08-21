using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Motor.ATP._200H.Subsys.Model
{

    public class ButtonManager
    {
        public ReadOnlyCollection<RecoredAction> LastRelaseActionCollection { private set; get; }
        public ReadOnlyCollection<RecoredAction> LastPressActionCollection { private set; get; }

        private readonly List<RecoredAction> m_LastRelaseActionCollection;
        private readonly List<RecoredAction> m_LastPressActionCollection;

        public ButtonManager()
        {
            m_LastPressActionCollection = new List<RecoredAction>();
            
            m_LastRelaseActionCollection = new List<RecoredAction>();

            LastRelaseActionCollection = m_LastRelaseActionCollection.AsReadOnly();
            LastPressActionCollection = m_LastPressActionCollection.AsReadOnly();
        }

        public void AddPressAction(RecoredAction recoredAction)
        {
            if (recoredAction != null)
            {
                m_LastPressActionCollection .Add(recoredAction);
                if (m_LastPressActionCollection.Count > 2)
                {
                    m_LastPressActionCollection.RemoveAt(0);
                }
            }
        }

        public void AddRelaseAction(RecoredAction recoredAction)
        {
            if (recoredAction != null)
            {
                m_LastRelaseActionCollection.Add(recoredAction);
                if (m_LastRelaseActionCollection.Count > 2)
                {
                    m_LastRelaseActionCollection.RemoveAt(0);
                }
            }
        }
    }
}