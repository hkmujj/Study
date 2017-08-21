using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMI.Facility.DataType;
using GT_MMI.Interface.Button;
using GT_MMI.Interface.State;

namespace GT_MMI.States
{
    class InitializationState : StateBase
    {
        public InitializationState(baseClass baseClass, IButtonManager buttonManager)
            : base(baseClass, buttonManager)
        {
        }
    }
}
