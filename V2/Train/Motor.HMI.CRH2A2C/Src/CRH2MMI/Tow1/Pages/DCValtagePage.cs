namespace CRH2MMI.Tow1.Pages
{
    class DCValtagePage : Tow1CommonPageBase
    {
        public DCValtagePage(TowInfo towInfo) : base(towInfo)
        {
            PageName = "直流电压";
            MaxXValue = 4000;
        }
    }
}
