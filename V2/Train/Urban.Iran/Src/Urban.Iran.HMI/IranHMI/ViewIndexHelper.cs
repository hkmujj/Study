namespace Urban.Iran.HMI
{
    public static class ViewIndexHelper
    {
        public static bool IsInEventListView(IranViewIndex iranViewIndex)
        {
            return iranViewIndex == IranViewIndex.ActiveEvents || iranViewIndex == IranViewIndex.EventHistory;
        }
    }
}