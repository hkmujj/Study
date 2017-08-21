using System.ComponentModel.Composition;

namespace Engine.TPX21F.HXN5B.Model.BtnStragy
{
    public class StateInterfaceExportAttribute : ExportAttribute
    {

        public StateInterfaceExportAttribute()
            : base(typeof(IStateInterface))
        {
        }
    }
}