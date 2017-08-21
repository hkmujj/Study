using System.Collections.Generic;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

using Motor.HMI.CRH3C380B.Base.Fault;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.黑屏
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIClassOverScreen : CRH3C380BBase
    {
        private IEnumerable<CRH3C380BBase> NeedClearObjects
        {
            get
            {
                yield return this.FindNeighbourObject<DMIFault>();
                yield return this.FindNeighbourObject<DMITitle>();
            }
        }

        //2
        public override string GetInfo()
        {
            return "DMI课程结束视图";
        }

        //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //1
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                foreach (var needClearObject in NeedClearObjects)
                {
                    needClearObject.Clear();
                }
            }
        }
    }
}