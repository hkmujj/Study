using System;
using System.ComponentModel.Composition;

namespace Engine.TPX21F.HXN5B.Model.BtnStragy
{
    public class ActionResponserExportAttribute : ExportAttribute
    {
        public ActionResponserExportAttribute(Type name)
            : base(name.FullName, typeof(IBtnActionResponser))
        {
        }
    }
}