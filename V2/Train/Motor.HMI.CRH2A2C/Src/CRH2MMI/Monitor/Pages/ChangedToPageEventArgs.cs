using System;

namespace CRH2MMI.Monitor.Pages
{
    class ChangedToPageEventArgs : EventArgs
    {
        public ChangedToPageEventArgs(ChangeTo to)
        {
            To = to;
        }

        public enum ChangeTo
        {
            Date,
            Time,
        }

        public ChangeTo To { private set; get; }
    }
}
