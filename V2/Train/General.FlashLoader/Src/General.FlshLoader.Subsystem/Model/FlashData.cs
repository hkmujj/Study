using System.Diagnostics;
using General.FlashLoader.Subsystem.Interface;

namespace General.FlashLoader.Subsystem.Model
{
    public class FlashData
    {
        [DebuggerStepThrough]
        public FlashData(FlashCommandType commandType, string value)
        {
            CommandType = commandType;
            Value = value;
        }

        public FlashCommandType CommandType { private set; get; }

        public string Value { private set; get; }
    }
}