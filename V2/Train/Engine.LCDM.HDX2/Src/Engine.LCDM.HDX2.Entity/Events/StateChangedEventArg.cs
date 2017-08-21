using Engine.LCDM.HDX2.Entity.Model.BtnStragy;

namespace Engine.LCDM.HDX2.Entity.Events
{
    public class StateChangedEventArg
    {
        public StateChangedEventArg(StateInterfaceKey targetStateKey)
        {
            TargetStateKey = targetStateKey;
        }

        public StateInterfaceKey TargetStateKey { private set; get; }
    }
}