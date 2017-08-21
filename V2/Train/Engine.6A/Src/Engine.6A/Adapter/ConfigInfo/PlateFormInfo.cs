using Excel.Interface;

namespace Engine._6A.Adapter.ConfigInfo
{
    [ExcelLocation("Messages.xls", "PlatformInfo")]
    public class PlateFormInfo : ISetValueProvider
    {
        [ExcelField("Name")]
        public string Name { get; set; }
        [ExcelField("Content")]
        public string Content { get; set; }

        public void SetValue(string name, string value)
        {
            switch (name)
            {
                case "Name":
                    Name = value;
                    break;
                case "Content":
                    Content = value;
                    break;
            }
        }
    }
}