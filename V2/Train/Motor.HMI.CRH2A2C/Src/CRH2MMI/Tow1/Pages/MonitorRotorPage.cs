namespace CRH2MMI.Tow1.Pages
{
    class MonitorRotorPage : Tow1CommonPageBase
    {
        public MonitorRotorPage(TowInfo towInfo) : base(towInfo)
        {
            PageName = "电机转子";
            MaxXValue = 300;
        }
    }
}
