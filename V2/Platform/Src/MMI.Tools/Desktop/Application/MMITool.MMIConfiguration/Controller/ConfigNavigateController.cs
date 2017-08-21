using System;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Interfaces;
using MMITool.Addin.MMIConfiguration.Constant;
using MMITool.Addin.MMIConfiguration.Model;
using MMITool.Addin.MMIConfiguration.View.ConfigureContent;
using MMITool.Addin.MMIConfiguration.ViewModel;

namespace MMITool.Addin.MMIConfiguration.Controller
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ConfigNavigateController : ControllerBase<ConfigNavigateViewModel>
    {
        private readonly IRegionManager m_RegionManager;

        private readonly ConfigureContentViewModel m_ConfigureContentViewModel;

        public ICommand SelectedChangedCommand { private set; get; }

        public ICommand IsSeniorConfigModelChangedCommand { private set; get; }

        [ImportingConstructor]
        public ConfigNavigateController(IRegionManager regionManager, ConfigureContentViewModel configureContentViewModel)
        {
            m_RegionManager = regionManager;
            m_ConfigureContentViewModel = configureContentViewModel;
            SelectedChangedCommand = new DelegateCommand<ConfigFileModel>(SelectedChangedExcute);
            IsSeniorConfigModelChangedCommand = new DelegateCommand(IsSeniorConfigModelChangedExcute);
        }

        public void UpdateConfigFileCollection(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            switch (notifyCollectionChangedEventArgs.Action)
            {
                case NotifyCollectionChangedAction.Add:


                    if (ViewModel.Model.IsSeniorConfigModel)
                    {
                        ViewModel.Model.ConfigConfigFileCollection.AddRange(
                            notifyCollectionChangedEventArgs.NewItems.Cast<ConfigFileModel>());
                    }
                    else
                    {
                        ViewModel.Model.ConfigConfigFileCollection.AddRange(
                       notifyCollectionChangedEventArgs.NewItems.Cast<ConfigFileModel>()
                           .Where(w => !w.ConfigTypeMapInfo.ViewMappedConfigType.IsSeniorItem));
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var newItem in notifyCollectionChangedEventArgs.NewItems)
                    {
                        ViewModel.Model.ConfigConfigFileCollection.Remove(newItem as ConfigFileModel);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    ViewModel.Model.ConfigConfigFileCollection.Clear();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void IsSeniorConfigModelChangedExcute()
        {
            ViewModel.Model.ConfigConfigFileCollection.Clear();

            if (ViewModel.Model.IsSeniorConfigModel)
            {
                ViewModel.Model.ConfigConfigFileCollection.AddRange(
                    ViewModel.SelectExePathViewModel.Model.ConfigConfigFileCollection);
            }
            else
            {
                ViewModel.Model.ConfigConfigFileCollection.AddRange(
                    ViewModel.SelectExePathViewModel.Model.ConfigConfigFileCollection.Where(
                        w => !w.ConfigTypeMapInfo.ViewMappedConfigType.IsSeniorItem));
            }

        }

        private void SelectedChangedExcute(ConfigFileModel configFile)
        {
            m_ConfigureContentViewModel.Controller.Select(configFile);

            SelectContentEditer(configFile);

        }

        private void SelectContentEditer(ConfigFileModel configFile)
        {
            if (configFile == null)
            {
                var uriQuery = new UriQuery
                {
                    {NaviParamKeys.File, string.Empty},
                    {NaviParamKeys.FileTypeDescription, string.Empty}
                };
                m_RegionManager.RequestNavigate(ConfigurationRegionNames.ConfigureEditRegion,
                    new Uri(string.Format("{0}{1}", typeof(NoneConfigureContentView), uriQuery), UriKind.Relative));
            }
            else
            {
                var uriQuery = new UriQuery
                {
                    {NaviParamKeys.File, configFile.FileFullName},
                    {NaviParamKeys.FileTypeDescription, configFile.ConfigTypeMapInfo.ConfigureDescription.Description}
                };
                m_RegionManager.RequestNavigate(ConfigurationRegionNames.ConfigureEditRegion,
                    new Uri(string.Format("{0}{1}", configFile.ConfigTypeMapInfo.ViewMappedConfigType.ViewType.Name, uriQuery), UriKind.Relative));
            }
        }

    }
}