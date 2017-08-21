using System.Collections.Generic;
using System.Linq;
using Engine.HMI.HXD1C.TPX21A.Button;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Engine.HMI.HXD1C.TPX21A
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class GlobalParam : baseClass
    {
        public static GlobalParam Instance { private set; get; }

        public int CurrentViewId { private set; get; }

        private List<IButtonEventListener> m_ButtonEventListeners;

        public override bool init(ref int nErrorObjectIndex)
        {
            Instance = this;
            m_ButtonEventListeners = new List<IButtonEventListener>();

            return true;
        }

        public void AddButtonEventListener(IButtonEventListener listener)
        {
            m_ButtonEventListeners.Add(listener);
        }

        public bool NotifyButtonDownEvent(ButtonName btn)
        {
            return m_ButtonEventListeners.Count(a => a.ResponseMouseDown(btn)) > 0;
        }

        public bool NotifyButtonUpEvent(ButtonName btn)
        {
            return m_ButtonEventListeners.Count(a => a.ResponseMouseUp(btn)) > 0;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            CurrentViewId = nParaB;
        }
    }
}