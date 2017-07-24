using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interfaces;
using MMITool.Addin.MMIConfiguration.Controller;
using MMITool.Addin.MMIConfiguration.Events;
using MMITool.Addin.MMIConfiguration.Interface;

namespace MMITool.Addin.MMIConfiguration.SaveStrategy
{
    [Export(typeof(ISaveStrategy))]
    public class SaveAllStrategy : ISaveStrategy
    {
        public bool CanSaveExcute { get { return true; } }
        public bool CanCancelExcute { get { return true; } }

        private readonly List<IConfigureContentEditerViewModel> m_ModifiedViewModels;

        private readonly List<string> m_SaveConfigCompletedInfoBuffer;

        [Import]
        private Lazy<SelectExePathController> m_SelectExePathController;

        public SaveAllStrategy()
        {
            m_ModifiedViewModels = new List<IConfigureContentEditerViewModel>();
            m_SaveConfigCompletedInfoBuffer = new List<string>();

            IEventAggregator eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

            eventAggregator.GetEvent<SaveConfigCompletedEvent>()
                .Subscribe(s =>
                {
                    m_SaveConfigCompletedInfoBuffer.Add(string.Format("\r\n{0}\r\n", s));
                });

            eventAggregator.GetEvent<ParserConfigCompletedEvent>().Subscribe(s => ResetAll());
        }

        public void SaveExcute()
        {
            foreach (var vm in m_ModifiedViewModels)
            {
                vm.Controller.SaveConfig();
            }

            MessageBox.Show(ServiceLocator.Current.GetInstance<IApplicationService>().ShellWindow,
                string.Join("\r\n", m_SaveConfigCompletedInfoBuffer), "Save completed");

            ReloadConfig();

            ClearCache();
        }

        private void ReloadConfig()
        {
            m_SelectExePathController.Value.Reload();
        }

        public void CancelExcute()
        {
            ClearCache();
        }

        private void ResetAll()
        {
            
        }

        private void ClearCache()
        {
            foreach (var vm in m_ModifiedViewModels)
            {
                vm.Controller.ResetConfig();
            }

            var last = m_ModifiedViewModels.LastOrDefault();

            m_ModifiedViewModels.Clear();

            if (last != null)
            {
                m_ModifiedViewModels.Add(last);
            }

            m_SaveConfigCompletedInfoBuffer.Clear();
            var last1 = m_SaveConfigCompletedInfoBuffer.LastOrDefault();
            if (last1 != null)
            {
                m_SaveConfigCompletedInfoBuffer.Add(last1);
            }
        }

        public void OnNavigatedTo(IConfigureContentEditerViewModel viewModel, NavigationContext navigationContext)
        {
            viewModel.Controller.NavigateToThis();
            m_ModifiedViewModels.Remove(viewModel);
            m_ModifiedViewModels.Add(viewModel);
        }

        public void UpdateSaveItem(IConfigureContentEditerViewModel viewModel)
        {
        }
    }
}