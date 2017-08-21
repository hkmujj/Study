using System;

namespace CRH2MMI.TrainLineNo
{
    class ControlPressedEventArgs : EventArgs
    {
        public ControlPressedEventArgs(ControlType type)
        {
            Type = type;
        }

        public ControlType  Type { private set; get; }


        public  enum ControlType
        {
            GotoLeft,
            GotoRight,
            Delete,
        }
    }
}
