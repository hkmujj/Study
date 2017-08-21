namespace CRH2MMI.Tow1.Pages
{
    class MonitorElectricityPage : Tow1CommonPageBase
    {
        public MonitorElectricityPage(TowInfo towInfo) : base(towInfo)
        {
            PageName = "电机电流";
            MaxXValue = 2000;
        }
    }
}
