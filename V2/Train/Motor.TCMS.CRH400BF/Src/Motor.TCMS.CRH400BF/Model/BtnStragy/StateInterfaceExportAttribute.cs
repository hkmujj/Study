using System.ComponentModel.Composition;

namespace Motor.TCMS.CRH400BF.Model.BtnStragy
{
    public class StateInterfaceExportAttribute : ExportAttribute
    {

        public StateInterfaceExportAttribute()
            : base(typeof(IStateInterface))
        {
        }
    }
}