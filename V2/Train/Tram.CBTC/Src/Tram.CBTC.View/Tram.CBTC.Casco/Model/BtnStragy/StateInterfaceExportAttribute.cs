using System.ComponentModel.Composition;

namespace Tram.CBTC.Casco.Model.BtnStragy
{
    public class StateInterfaceExportAttribute : ExportAttribute
    {

        public StateInterfaceExportAttribute()
            : base(typeof(IStateInterface))
        {
        }
    }
}