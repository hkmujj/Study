using System.Collections.Generic;
using Excel.Interface;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.Config
{
    [ExcelLocation("IOStateView.xls", "IO")]
    public class IOStateViewUnit : ViewUnitBase
    {
        public IOStateViewUnit()
        {
            SignalStatus = Interface.Enum.SignalStatus.Lowlevel;
            Float = 0;
        }
        public override void Changed(IDictionary<int, bool> args, int? iPara = null)
        {
            if (SignalType == SignalType.Float)
            {
                return;
            }
            var index = IndexConfigure.InBoolIndex[LogicName];
            if (args.ContainsKey(index))
            {
                SignalStatus = args[index]
                    ? Interface.Enum.SignalStatus.HighLevel
                    : Interface.Enum.SignalStatus.Lowlevel;
            }
        }

        public override void Changed(IDictionary<int, float> args, int? ipara = null)
        {
            if (SignalType != SignalType.Float)
            {
                return;
            }
            var index = IndexConfigure.IntFloatIndex[LogicName];
            if (args.ContainsKey(index))
            {
                Float = args[index];
            }
        }
    }
}