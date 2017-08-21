using MsgReceiveMod;

namespace Engine.TCMS.HXD3D.Fault
{
    public class FaultMsgHXD3D : FaultMsgOrdinary
    {
        public MsgShowType MsgShowType { get; set; }

        public string SubSysName { get; set; }

        public int TrainID { get; set; }

        public bool HasAck { set; get; }
    }
}