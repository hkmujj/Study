using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism;
using MMI.Facility.DataType.Config;
using MMITool.Addin.MMIConfiguration.Infrastructure;
using MMITool.Addin.MMIConfiguration.Model;
using MMITool.Addin.MMIConfiguration.ViewModel;

namespace MMITool.Addin.MMIConfiguration.Controller
{
    [Export]
    public class IndexDescriptionController : ConfigContentControllerBase<IndexDescriptionViewModel>
    {
        protected override bool HasInitalzied
        {
            get
            {
                return ViewModel.Model.ProjectIndexDescriptionConfigModels != null &&
                       ViewModel.Model.ProjectIndexDescriptionConfigModels.Any();
            }
        }

        public override void ResetConfig()
        {
            var indexConfig = XmlModelDeepCopy.Copy((ConcreateRootConfig.IndexDescriptionConfig ?? new IndexDescriptionConfig()));

            ViewModel.Model.ProjectIndexDescriptionConfigModels =
                new ObservableCollection<ProjectIndexDescriptionConfigModel>(indexConfig
                    .ProjectIndexDescriptionConfigs.Select(
                        s =>
                            new ProjectIndexDescriptionConfigModel(s,
                                new ObservableCollection<SelectableSubsystem>(ConcreateRootConfig.AppConfigs.Select(
                                    sub =>
                                        new SelectableSubsystem()
                                        {
                                            IsSelected = s.AppNames.Contains(sub.SubsystemConfig.Name),
                                            SubsystemConfig = sub.SubsystemConfig
                                        })))));

        }



        public override void SaveConfig()
        {
            foreach (var model in ViewModel.Model.ProjectIndexDescriptionConfigModels)
            {
                model.ProjectIndexDescriptionConfig.AppNames.Clear();
                model.ProjectIndexDescriptionConfig.AppNames.AddRange(
                    model.SelectableSubsystem.Where(w => w.IsSelected).Select(s => s.SubsystemConfig.Name));
            }

            ConcreateRootConfig.IndexDescriptionConfig.ProjectIndexDescriptionConfigs =
                new ObservableCollection<ProjectIndexDescriptionConfig>(
                    ViewModel.Model.ProjectIndexDescriptionConfigModels.Select(s => s.ProjectIndexDescriptionConfig));

            SaveConfig(ConcreateRootConfig.IndexDescriptionConfig);

        }
    }
}