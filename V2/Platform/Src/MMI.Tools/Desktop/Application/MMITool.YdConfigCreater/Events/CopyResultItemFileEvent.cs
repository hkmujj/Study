using System.Diagnostics;
using Microsoft.Practices.Prism.Events;
using MMITool.Addin.YdConfigCreater.Model.Result.Detail;

namespace MMITool.Addin.YdConfigCreater.Events
{
    public class CopyResultItemFileEvent : CompositePresentationEvent<CopyResultItemFileEvent.Args>
    {
        public class Args
        {
            [DebuggerStepThrough]
            public Args(ResultItem resultItem)
            {
                ResultItem = resultItem;
            }

            public ResultItem ResultItem { get; private set; }
        }
    }
}