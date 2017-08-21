using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMI.Facility.DataType;
using GT_MMI.Interface.Button;

namespace GT_MMI.Interface.State
{
    interface IStateFlyWeight
    {
        IState this[StateType type] { get; }

        void Initalize(baseClass viewClass, IButtonManager btnManager);
    }
}
