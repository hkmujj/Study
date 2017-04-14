using Microsoft.Practices.Prism.ViewModel;

namespace MMITool.Addin.MMIConfiguration.Model
{
    public class ConfigFileModel : NotificationObject
    {
        private string m_FileFullName;
        private string m_FileRelativePath;

        public ConfigTypeMapInfo ConfigTypeMapInfo { set; get; }

        public string BasePath { private set; get; }

        public string FileRelativePath
        {
            private set
            {
                if (value == m_FileRelativePath)
                {
                    return;
                }
                m_FileRelativePath = value;
                RaisePropertyChanged(() => FileRelativePath);
            }
            get { return m_FileRelativePath; }
        }

        public string FileFullName
        {
            set
            {
                if (value == m_FileFullName)
                {
                    return;
                }
                m_FileFullName = value;
                FileRelativePath = FileFullName.Replace(BasePath, "");
                RaisePropertyChanged(() => FileFullName);
            }
            get { return m_FileFullName; }
        }

        public ConfigFileModel(string basePath)
        {
            BasePath = basePath;
        }

        public string GetKey()
        {
            return FileFullName;
        }

    }
}