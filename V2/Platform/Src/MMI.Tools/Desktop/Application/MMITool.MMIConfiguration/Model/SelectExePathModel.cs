using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using Microsoft.Practices.Prism.ViewModel;

namespace MMITool.Addin.MMIConfiguration.Model
{
    [Export]
    public class SelectExePathModel : NotificationObject
    {
        public const int MaxSelectableItemCount = 15;

        private string m_FileFullName;
        private ObservableCollection<ConfigFileModel> m_ConfigFileCollection;
        private bool m_HasValidationError;

        public string FileFullName
        {
            set
            {
                m_FileFullName = value;
                HasValidationError = false;
                RaisePropertyChanged(() => FileFullName);
            }
            get { return m_FileFullName; }
        }

        public bool HasValidationError
        {
            set
            {
                if (value.Equals(m_HasValidationError))
                {
                    return;
                }
                m_HasValidationError = value;
                RaisePropertyChanged(() => HasValidationError);
            }
            get { return m_HasValidationError; }
        }

        public string FilePath { get { return Path.GetDirectoryName(FileFullName); }}

        public ObservableCollection<string> SelctableFullNames { get; private set; }

        public ObservableCollection<ConfigFileModel> ConfigConfigFileCollection
        {
            get { return m_ConfigFileCollection; }
            private set
            {
                if (Equals(value, m_ConfigFileCollection))
                {
                    return;
                }
                m_ConfigFileCollection = value;
                RaisePropertyChanged(() => ConfigConfigFileCollection);
            }
        }

        public SelectExePathModel()
        {
            ConfigConfigFileCollection = new ObservableCollection<ConfigFileModel>();
            SelctableFullNames = new ObservableCollection<string>();
        }
    }
}