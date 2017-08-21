using System.Diagnostics;
using Engine.LCDM.HDX2.Resource;

namespace Engine.LCDM.HDX2.Entity.Events
{
    public class ChangeResourceEventArgs
    {
        [DebuggerStepThrough]
        public ChangeResourceEventArgs(ResourceType changToResourceType)
        {
            ChangToResourceType = changToResourceType;
        }

        public ResourceType ChangToResourceType { private set; get; }
    }
}