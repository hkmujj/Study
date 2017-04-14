using System.Text;

namespace MMI.Communacation.Interface.ProtocolLayer
{
    public class CirCmd
    {
        public StringBuilder CirMsgA { private set; get; }

        public StringBuilder CirMsgB { private set; get; }

        public StringBuilder CirMsgC { private set; get; }

        public StringBuilder CirMsgD { private set; get; }

        public CirCmd()
        {
            CirMsgA = new StringBuilder();
            CirMsgB = new StringBuilder();
            CirMsgC = new StringBuilder();
            CirMsgD = new StringBuilder();
        }

    }
}
