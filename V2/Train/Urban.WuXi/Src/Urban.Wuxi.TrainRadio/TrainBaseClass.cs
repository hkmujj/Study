using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;

namespace Urban.Wuxi.TrainRadio
{
    public class TrainBaseClass:baseClass
    {
        public void OnPost(CmdType type, int nParaA, int nParaB, float nParaC)
        {
            switch (type)
            {
                case CmdType.SetBoolValue:
                    append_postCmd(type, nParaA + 3200, nParaB, nParaC);
                    break;
                case CmdType.SetFloatValue:
                    append_postCmd(type, nParaA + 400, nParaB, nParaC);
                    break;
                default:
                    append_postCmd(type, nParaA, nParaB, nParaC);
                    break;
            }
        }
    }
}