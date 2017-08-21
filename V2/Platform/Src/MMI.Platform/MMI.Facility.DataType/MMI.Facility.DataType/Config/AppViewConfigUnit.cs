using System.Drawing;
using MMI.Facility.Interface.Data.Config;

namespace MMI.Facility.DataType.Config
{
    public class AppViewConfigUnit : IAppViewConfigUnit
    {
        public IAppViewConfig ParentConfig { get; set; }

        public int Index { get; set; }

        public Color BgColor { get; set; }

        public Image BgImage { get; set; }

        public string BgImageFn { get; set; }

        public string BgImageFile { get; set; }

        public string RecPath { get; set; }  
    }
}