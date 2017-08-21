using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;

namespace HD_HXD2_TMS.Extension
{
    public static class BaseClassExtension
    {
        public static void SetOutBoolValue(this baseClass src, int nParaA, int nParaB, float nParaC)
        {
            src.append_postCmd(CmdType.SetBoolValue,  4800 + nParaA, nParaB, nParaC);
        }

        public static void SetOutFloatValue(this baseClass src, int nParaA, int nParaB, float nParaC)
        {
            src.append_postCmd(CmdType.SetFloatValue, 600 + nParaA, nParaB, nParaC);
        }
    }
}