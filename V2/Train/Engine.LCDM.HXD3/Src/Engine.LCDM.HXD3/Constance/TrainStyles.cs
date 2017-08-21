using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.LCDM.HXD3.ViewModels;

namespace Engine.LCDM.HXD3.Constance
{
    public class TrainStyles
    {
        public readonly static TrainStyleChoice FlowStyle=new TrainStyleChoice(ViewNames.FlowView,ViewNames.FlowViewH);
        public readonly static TrainStyleChoice PowerBrakeSetStyle=new TrainStyleChoice(ViewNames.PowerBrakeSet,ViewNames.PowerBrakeSetH);
    }
}
