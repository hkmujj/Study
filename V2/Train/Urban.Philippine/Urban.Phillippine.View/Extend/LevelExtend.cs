using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.Extend
{
    public static class LevelExtend
    {
        public static TractionBrakeLevel ConvertToLevel(this float fValue)
        {
            return (TractionBrakeLevel)((int)fValue);
        }

        public static TractionBrakeLevel ConvertToLevel(this int iValue)
        {
            return (TractionBrakeLevel)(iValue);
        }

        public static TractionBrakeLevel ConvertToLevel(this double dValue)
        {
            return (TractionBrakeLevel)((int)dValue);
        }
    }
}