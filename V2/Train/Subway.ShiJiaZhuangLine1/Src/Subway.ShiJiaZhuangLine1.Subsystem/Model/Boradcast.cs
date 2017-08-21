using Microsoft.Practices.Prism.ViewModel;
using Subway.ShiJiaZhuangLine1.Interface;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Model
{
    public class Boradcast : NotificationObject, IBoradcast
    {
        public int LogicNum { get; set; }
        public int Number { get; set; }
        public string Content { get; set; }

    }
}