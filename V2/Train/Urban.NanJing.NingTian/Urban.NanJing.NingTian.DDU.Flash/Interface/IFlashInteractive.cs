using System.Collections.Generic;

namespace Urban.NanJing.NingTian.DDU.Flash.Interface
{
    public interface IFlashInteractive
    {
        void SetVisible(bool visible);

        void SetValue(string fun, IEnumerable<string> values);
        void SetValue(string fun, string value);

        event FlashDataEventHandler OnReceiveFromFlash;
    }
}