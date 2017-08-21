using System.ComponentModel.Composition;

namespace Subway.ShenZhenLine11.ViewModels
{
    [Export(typeof(BaseInfoViewModel))]
    public class BaseInfoViewModel : SubViewModelBase
    {
        public string CurrentStation { get; set; }
        public string NextStation { get; set; }
        public string EnStation { get; set; }
    }
}