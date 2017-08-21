using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonControls
{
    public interface ISetMode
    {
        Int32 ModeID { get; set; }
        void SetMode();
        Int32 GetMode();
    }
}
