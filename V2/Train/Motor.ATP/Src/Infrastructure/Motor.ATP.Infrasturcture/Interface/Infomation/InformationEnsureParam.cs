using System.Diagnostics;

namespace Motor.ATP.Infrasturcture.Interface.Infomation
{
    public class InformationEnsureParam
    {
        [DebuggerStepThrough]
        public InformationEnsureParam(InformationEnsureType ensureType = InformationEnsureType.Ok)
        {
            EnsureType = ensureType;
        }

        public InformationEnsureType EnsureType { get; private set; }
    }
}