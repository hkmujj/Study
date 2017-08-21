using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CRH2MMI.Common.Global;

namespace CRH2MMI.Common.View.Train
{
    enum TrainUnitState
    {
        Nomal,
        Fault,
        Unknown,
    }

    class TrainUnitStateColorAdpt
    {
        private static List<Color> Colors { set; get; }

        public static Color GetStateColor(TrainUnitState state)
        {
            if (Colors == null)
            {
                Colors = GlobalInfo.CurrentCRH2Config.TrainConfig.TrainUnits.First().Colors;
            }

            switch (state)
            {
                case TrainUnitState.Nomal:
                    return Colors[1];
                case TrainUnitState.Fault:
                    return Colors[2];
                case TrainUnitState.Unknown:
                    return Colors[0];
                default:
                    throw new ArgumentOutOfRangeException("state");
            }
        }
    }
}
