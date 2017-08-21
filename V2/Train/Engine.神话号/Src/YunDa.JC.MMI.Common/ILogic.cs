using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YunDa.JC.MMI.Common
{
    public interface ILogic
    {
        List<DataConfigInfo> DataConfigInfoCollection { get; set; }
        List<Int32> OutBoolConfigInfoCollection { get; set; }
        List<Int32> OutFloatConfigInfoCollection { get; set; }
        DataConfigInfo CurrentDataConfigInfo { set; }

        void SetState(Int32 logicID, Boolean state);

        void SetValue(Int32 logicID, float state);
    }
}
