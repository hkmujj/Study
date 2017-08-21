using System.Diagnostics;
using System.Drawing;
using CommonUtil.Controls;

namespace CRH2MMI.Common.Models
{
    class ButtonStateRecorder
    {
        public MouseState State { set; get; }

        public Point Location { set; get; }

        [DebuggerStepThrough]
        public ButtonStateRecorder()
        {
            State = MouseState.MouseUp;
        }

    }

}
