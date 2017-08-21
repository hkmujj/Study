using Excel.Interface;

namespace Engine._6A.Adapter.ConfigInfo
{
    [ExcelLocation("Messages.xls", "Version")]
    public class VersionInfo : ISetValueProvider
    {
        [ExcelField("Name")]
        public string Name { get; set; }
        [ExcelField("Version")]
        public string Version { get; set; }

        public void SetValue(string name, string value)
        {
            switch (name)
            {
                case "Name":
                    Name = value;
                    break;
                case "Version":
                    Version = value;
                    break;
            }
        }
    }
}