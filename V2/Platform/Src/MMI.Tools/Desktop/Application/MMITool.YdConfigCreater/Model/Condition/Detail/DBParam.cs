using Microsoft.Practices.Prism.ViewModel;

namespace MMITool.Addin.YdConfigCreater.Model.Condition.Detail
{
    public class DBParam : NotificationObject
    {
        public DBParam()
        {
            Port = "80";
        }

        public string IP { get; set; }

        public string Port { get; private set; }

        public string DBName { get; set; }
    }
}