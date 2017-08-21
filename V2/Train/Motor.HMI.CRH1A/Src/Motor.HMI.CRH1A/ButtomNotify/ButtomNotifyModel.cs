using System.Diagnostics;

namespace Motor.HMI.CRH1A.ButtomNotify
{
    [DebuggerDisplay("IndexKey={IndexKey}")]
    public class ButtomNotifyModel
    {
        [DebuggerStepThrough]
        public ButtomNotifyModel(string indexKey, string content)
        {
            Content = content;
            IndexKey = indexKey;
        }

        public string IndexKey { private set; get; }

        public string Content { private set; get; }
    }
}