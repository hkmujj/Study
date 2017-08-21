using System.ComponentModel.Composition;

namespace Motor.HMI.CRH380D.Models.BtnStragy
{
    public class StateInterfaceExportAttribute : ExportAttribute
    {

        public StateInterfaceExportAttribute()
            : base(typeof(IStateInterface))
        {
        }
    }
}