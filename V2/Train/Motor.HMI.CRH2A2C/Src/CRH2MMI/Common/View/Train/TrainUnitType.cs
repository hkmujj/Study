using System;
using System.Collections.Generic;
using System.Linq;

namespace CRH2MMI.Common.View.Train
{
    /// <summary>
    /// 单元号
    /// </summary>
    enum TrainUnitNo
    {
        U1 = 0,
        U2,
        U3,
        U4,
        U5,
        U6,
        U7,
    }

    internal class TrainUnitNoHelper
    {
        public static readonly List<TrainUnitNo> AllUnitNoes =
            Enum.GetNames(typeof (TrainUnitNo)).Select(s => (TrainUnitNo)(Enum.Parse(typeof (TrainUnitNo), s))).ToList();
    }
}