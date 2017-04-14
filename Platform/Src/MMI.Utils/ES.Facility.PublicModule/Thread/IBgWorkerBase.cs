using System;

namespace ES.Facility.PublicModule.Thread
{
    public interface IBgWorkerBase
    {
        void go();

        event Action<int, object> onProgressStaus;

        event Action<int> onProgress;

        event Action<bool> onEnd;

    }
}
