using System;
using System.Collections.Generic;
using YunDa.JC.MMI.Common.Extensions;

namespace CommonControls.Extensions
{
    public static class DefTableExtension
    {
        private static readonly Action<DefTable, List<LogicInfo>> SetLogicInfoAction = (t, infos) => t.SetLogicInfo(infos);

        public static void InvokeSetLogicInfo(this DefTable ta, List<LogicInfo> logicInfos)
        {
            ta.InvokeIfNeed(SetLogicInfoAction, ta, logicInfos);
        }
    }
}