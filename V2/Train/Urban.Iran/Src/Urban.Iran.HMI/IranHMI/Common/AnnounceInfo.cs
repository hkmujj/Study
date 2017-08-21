using System.Diagnostics;

namespace Urban.Iran.HMI.Common
{
    public class AnnounceInfo
    {
        [DebuggerStepThrough]
        public AnnounceInfo(int code = -1, string msg = "")
        {
            Msg = msg;
            Code = code;
        }

        public int Code { private set; get; }

        public string Msg { private set; get; }
    }
}