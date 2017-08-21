using System;
using System.IO;
using MMI.Facility.Interface.Data.Config;

namespace MMI.Facility.DataType.Config
{
    class SystemDicrectory : ISystemDicrectory
    {
        public string BaseDirectory { get; private set; }

        public string AddinDirectory { get; private set; }

        public string SystemConfigDirectory { get; private set; }

        public string SystemResourceDirectory { get; private set; }

        public SystemDicrectory()
        {
            BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            AddinDirectory = Path.Combine(BaseDirectory, "Addin");
            SystemConfigDirectory = Path.Combine(BaseDirectory, "Config");
            SystemResourceDirectory = Path.Combine(BaseDirectory, "Resource");
        }
    }
}
