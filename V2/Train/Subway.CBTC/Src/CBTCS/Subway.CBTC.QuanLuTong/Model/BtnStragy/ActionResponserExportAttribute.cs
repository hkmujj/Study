using System;
using System.ComponentModel.Composition;

namespace Subway.CBTC.QuanLuTong.Model.BtnStragy
{
    public class ActionResponserExportAttribute : ExportAttribute
    {
        public ActionResponserExportAttribute(Type name)
            : base(name.FullName, typeof(IBtnActionResponser))
        {
        }
    }
}