using System.ComponentModel.Composition;
using Engine.LCDM.HDX2.Entity.Model.Domain;

namespace Engine.LCDM.HDX2.Entity.Model.BtnStragy
{
    public class StateInterfaceExportAttribute : ExportAttribute
    {

        public StateInterfaceExportAttribute()
            : base(typeof(IStateInterface))
        {
        }
    }
}