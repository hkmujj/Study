using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using CommonUtil.Controls;
using Motor.HMI.CRH1A.Common;
using CommonUtil.Controls;

namespace Motor.HMI.CRH1A.HighVoltage
{
    public class HighVolOutBoolIdxAdpt
    {
        public static int GetCutInOrOffBoolIdx(SelectableRectangleControl selectable)
        {
            if (selectable is AcceptEleArc)
            {
                var arc = selectable as AcceptEleArc;
                if (arc.CurrentId == AcceptEleArc.Id.No2)
                {
                    return 0;
                }
                return 1;
            }
            var sw = selectable as HighVoltageSwitch;
                // 增加受电弓的偏移
            Debug.Assert(sw != null, "sw != null");
            return sw.Id + 2;
        }
    }
}
