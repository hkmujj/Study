namespace Urban.Phillippine.View.EventArgs
{
    public class FaultChangedArgs<T>
    {
        public FaultChangedArgs(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
        public T OldValue { get; private set; }
        public T NewValue { get; private set; }
    }
}