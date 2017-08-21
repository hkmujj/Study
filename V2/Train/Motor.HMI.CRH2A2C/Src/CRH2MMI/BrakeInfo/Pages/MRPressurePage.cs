namespace CRH2MMI.BrakeInfo.Pages
{
    class MRPressurePage : BrakeInfoCommonPage
    {
        public MRPressurePage(BrakeInfo brakeInfo) : base(brakeInfo)
        {
            PageName = "MR压力";
            MaxXValue = 1000;
        }
    }
}
