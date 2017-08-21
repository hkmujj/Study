using System;
using System.Windows.Forms;

namespace YunDa.JC.MMI.Common.Extensions
{
    public static class ILogicExtension
    {

        private static readonly Action<ILogic, int, bool> SetStateAction =
            (il, logicID, state) => il.SetState(logicID, state);

        public static void InvokeSetState(this Control contrl, ILogic c,  int id, bool state)
        {
            contrl.InvokeIfNeed(SetStateAction, c, id, state);
        }
    }
}