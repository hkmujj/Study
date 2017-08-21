namespace CRH2MMI.BrakeInfo.Pages
{
    class BrakePressurePage : BrakeInfoCommonPage
    {


        public BrakePressurePage(BrakeInfo brakeInfo) : base(brakeInfo)
        {
            PageName = "制动气缸压力";
            MaxXValue = 800;
        }
    }
}
