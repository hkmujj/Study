using System.Drawing;
using Engine.TCMS.HXD3D.Fault;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.TCMS.HXD3D.开机
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class ClassOverScreen : baseClass
    {
        public override string GetInfo()
        {
            return "课程结束视图";
        }

        private void GetValue()
        {
            append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[0], 0, 100f);
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            nErrorObjectIndex = -1;
            return true;
        }

        public override void paint(Graphics dcGs)
        {
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                if (FaultReceive.MsgInf != null)
                {
                    FaultReceive.MsgInf.ClearAllList();
                }
                GetValue();
            }
        }
    }
}