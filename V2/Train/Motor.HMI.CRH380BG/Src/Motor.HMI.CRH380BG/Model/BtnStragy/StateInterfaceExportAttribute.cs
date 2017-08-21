using System.ComponentModel.Composition;

namespace Motor.HMI.CRH380BG.Model.BtnStragy
{
    public class StateInterfaceExportAttribute : ExportAttribute
    {

        public StateInterfaceExportAttribute()
            : base(typeof(IStateInterface))
        {
        }
    }
}