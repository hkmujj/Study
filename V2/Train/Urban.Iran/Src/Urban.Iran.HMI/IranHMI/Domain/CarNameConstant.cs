using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Urban.Iran.HMI.Domain
{
    /// <summary>
    /// 车厢名常数
    /// </summary>
    public static class CarNameConstant
    {
        public const string Car01011 = "01011";
        public const string Car01012 = "01012";
        public const string Car01013 = "01013";
        public const string Car01014 = "01014";
        public const string Car01015 = "01015";

        public const string Tc1 = Car01011;
        public const string Tc2 = Car01015;

        public const string Mp1 = Car01012;
        public const string Mp2 = Car01014;

        public const string Cab1 = "Cab1";
        public const string Cab2 = "Cab2";

        public static readonly ReadOnlyCollection<string> CarNameCollection =
            new ReadOnlyCollection<string>(new List<string>
            {
                Car01011,
                Car01012,
                Car01013,
                Car01014,
                Car01015,
            });

        /// <summary>
        /// M车
        /// </summary>
        public const string M = Car01013;

    }
}