namespace Urban.Iran.HMI.Events
{
    public static class EventPriorityExtension
    {
        public static bool IsFullScreenEvent(this EventPriority eventPriority)
        {
            return eventPriority == EventPriority.Information;
        }
    }
}