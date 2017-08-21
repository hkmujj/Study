using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonControls
{
    public class LogicInfo
    {
        public LogicInfo(Int32 id,String description="")
        {
            ID = id;
            Description = description;
        }

        public Int32 ID { get;private set; }
        public String Description { get; private set; }
    }
}
