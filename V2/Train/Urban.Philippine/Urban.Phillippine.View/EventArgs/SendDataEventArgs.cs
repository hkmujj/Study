using MMI.Facility.Interface.Data;

namespace Urban.Phillippine.View.EventArgs
{
    public class SendDataEventArgs
    {
        public int SendData { get; set; }
        public CmdType Type { get; set; }
        public int Index { get; set; }
    }
}