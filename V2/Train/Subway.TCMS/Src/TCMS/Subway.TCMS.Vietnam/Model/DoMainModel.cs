using System.ComponentModel.Composition;
using Subway.TCMS.Infrasturcture.Model.Constance;

namespace Subway.TCMS.Vietnam.Model
{
    [Export]
    public class DoMainModel : global::Subway.TCMS.Infrasturcture.Model.TCMS
    {
        public DoMainModel()
        {
            Type = TCMSType.Vietnam;
        }
    }
}
