using System.Diagnostics;

namespace Yunda.FlashTester.Model
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