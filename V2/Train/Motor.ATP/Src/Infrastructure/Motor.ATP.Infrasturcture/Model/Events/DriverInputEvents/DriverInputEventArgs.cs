using System.Diagnostics;
using Motor.ATP.Infrasturcture.Interface;

namespace Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents
{
    public class DriverInputEventArgs<T>
    {
        [DebuggerStepThrough]
        public DriverInputEventArgs(T selectedContent, IATP atp) : this(selectedContent, atp.ATPType)
        {
        }

        [DebuggerStepThrough]
        public DriverInputEventArgs(T selectedContent, ATPType atpType = ATPType.Unkown)
        {
            SelectedContent = selectedContent;
            ATPType = atpType;
        }

        public T SelectedContent { private set; get; }

        public ATPType ATPType { get; private set; }
    }
}