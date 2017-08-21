using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using MMITool.Addin.MMIConfiguration.Model;
using MMITool.Addin.MMIConfiguration.ViewModel;

namespace MMITool.Addin.MMIConfiguration.Service
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ConfigFileManager
    {
        private readonly SelectExePathViewModel m_SelectExePathViewModel;

        [ImportingConstructor]
        public ConfigFileManager(SelectExePathViewModel selectExePathViewModel)
        {
            m_SelectExePathViewModel = selectExePathViewModel;
            ConfigFileModelCollection = new ReadOnlyCollection<ConfigFileModel>(selectExePathViewModel.Model.ConfigConfigFileCollection);
        }

        public ReadOnlyCollection<ConfigFileModel> ConfigFileModelCollection { private set; get; }

        public string TargetRootPath { get { return m_SelectExePathViewModel.Model.FilePath; }}

    }
}