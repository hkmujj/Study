using System;

namespace CRH2MMI.TrainLineNo
{
    class TrainLineChangePageEventArgs : EventArgs
    {
        public TrainLineChangePageEventArgs(TrainLinePageType changeTo)
        {
            ChangeTo = changeTo;
        }

        public TrainLinePageType ChangeTo { private set; get; }
    }
}
