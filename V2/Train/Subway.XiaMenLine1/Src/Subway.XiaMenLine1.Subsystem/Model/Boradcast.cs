using Subway.XiaMenLine1.Interface;

namespace Subway.XiaMenLine1.Subsystem.Model
{
    public class Boradcast : IBoradcast
    {

        public int LogicNum { get; set; }
        public int Number { get; set; }
        public string Content { get; set; }


        public BoradcastType Type { get; set; }
    }
}