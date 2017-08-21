using System.Collections.Generic;
using System.Drawing;
using CRH2MMI.Common.Util;
using CRH2MMI.PowerClassify.PowerStates;

namespace CRH2MMI.PowerClassify
{
    internal class PowerUnitCollectoin<T> : UnitBase where T : IPowerUnit
    {
        public List<T> PowerUnits { get; set; }

        public PowerUnitCollectoin()
        {
            PowerUnits = new List<T>();
        }

        public override void RefreshByState(PowerFrom powerFrom)
        {
            PowerFrom = powerFrom;
            var color = PowerFromColorAdaptor.GetColor(powerFrom);
            Color = color;

            PowerUnits.ForEach(e => e.RefreshByState(powerFrom));

        }

        public override void OnDraw(Graphics g)
        {
            PowerUnits.ForEach(e => e.OnDraw(g));
        }
    }
}
