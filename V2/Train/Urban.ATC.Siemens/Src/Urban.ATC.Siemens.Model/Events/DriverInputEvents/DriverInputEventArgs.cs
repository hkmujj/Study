using System.Diagnostics;

namespace Motor.ATP.Domain.Model.Events.DriverInputEvents
{
    public class DriverInputEventArgs<T>
    {
        [DebuggerStepThrough]
        public DriverInputEventArgs(T selectedContent)
        {
            SelectedContent = selectedContent;
        }

        public T SelectedContent { private set; get; }
    }
}