using System;

namespace CRH2MMI.Monitor
{
    enum KeyBoardControlType
    {
        Delete,
        Set,
    }

    class KeyBoardControlPressedEventArgs : EventArgs
    {
        public KeyBoardControlPressedEventArgs(KeyBoardControlType type)
        {
            Type = type;
        }

        public KeyBoardControlType Type { private set; get; }
    }

    class KeyBoardNumberPressedEventArgs : EventArgs
    {
        public KeyBoardNumberPressedEventArgs(int pressedNumber)
        {
            PressedNumber = pressedNumber;
        }

        public int PressedNumber { private set; get; }
    }
}
