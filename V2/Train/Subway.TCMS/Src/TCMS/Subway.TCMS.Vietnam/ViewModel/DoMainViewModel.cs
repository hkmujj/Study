using System.ComponentModel.Composition;
using Subway.TCMS.Vietnam.Model;

namespace Subway.TCMS.Vietnam.ViewModel
{
    [Export]
    public class DoMainViewModel
    {
        public DoMainViewModel()
        {

        }
        [Import]
        public DoMainModel DoMainModel { get; private set; }
    }
}
