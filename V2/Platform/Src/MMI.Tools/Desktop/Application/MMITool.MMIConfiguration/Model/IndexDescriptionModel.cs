using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.DataType.Config;
using MMI.Facility.Interface.Data.Config;

namespace MMITool.Addin.MMIConfiguration.Model
{
    [Export]
    public class IndexDescriptionModel : NotificationObject
    {
        private ObservableCollection<ProjectIndexDescriptionConfigModel> m_ProjectIndexDescriptionConfigModels;

        public ObservableCollection<ProjectIndexDescriptionConfigModel> ProjectIndexDescriptionConfigModels
        {
            set
            {
                if (Equals(value, m_ProjectIndexDescriptionConfigModels))
                {
                    return;
                }
                m_ProjectIndexDescriptionConfigModels = value;
                RaisePropertyChanged(() => ProjectIndexDescriptionConfigModels);
            }
            get { return m_ProjectIndexDescriptionConfigModels; }
        }

    }

    public class ProjectIndexDescriptionConfigModel : NotificationObject
    {
        public ProjectIndexDescriptionConfigModel(ProjectIndexDescriptionConfig projectIndexDescriptionConfig, ObservableCollection<SelectableSubsystem> selectableSubsystem)
        {
            ProjectIndexDescriptionConfig = projectIndexDescriptionConfig;
            SelectableSubsystem = selectableSubsystem;
        }

        public ProjectIndexDescriptionConfig ProjectIndexDescriptionConfig { private set; get; }

        public ObservableCollection<SelectableSubsystem> SelectableSubsystem { private set; get; }
    }

    public class SelectableSubsystem : NotificationObject
    {
        private ISubsystemConfig m_SubsystemConfig;
        private bool m_IsSelected;

        public ISubsystemConfig SubsystemConfig
        {
            set
            {
                if (Equals(value, m_SubsystemConfig))
                {
                    return;
                }
                m_SubsystemConfig = value;
                RaisePropertyChanged(() => SubsystemConfig);
            }
            get { return m_SubsystemConfig; }
        }

        public bool IsSelected
        {
            set
            {
                if (value == m_IsSelected)
                {
                    return;
                }
                m_IsSelected = value;
                RaisePropertyChanged(() => IsSelected);
            }
            get { return m_IsSelected; }
        }
    }

}