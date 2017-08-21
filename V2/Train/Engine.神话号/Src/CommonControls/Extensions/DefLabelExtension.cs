using System;
using YunDa.JC.MMI.Common.Extensions;

namespace CommonControls.Extensions
{
    public static class DefLabelExtension
    {
        private static readonly Action<DefLabel, string> SetDefTextAction = (label, s) => label.DefText = s;

        public static void InvokeSetDefText(this DefLabel la, string txt)
        {
            la.InvokeIfNeed(SetDefTextAction, la, txt);
        }
    }
}