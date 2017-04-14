using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interfaces;
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

        private readonly StringBuilder m_SaveConfigCompletedInfoBuffer;

        public SaveAllStrategy()
        {
            m_ModifiedViewModels = new List<IConfigureContentEditerViewModel>();
            m_SaveConfigCompletedInfoBuffer = new StringBuilder();
            ServiceLocator.Current.GetInstance<IEventAggregator>()
                .GetEvent<SaveConfigCompletedEvent>()
                .Subscribe(s =>
                {
                    m_SaveConfigCompletedInfoBuffer.AppendFormat("\r\n{0}\r\n", s);
                });
        }

        public void SaveExcute()
        {
            foreach (var vm in m_ModifiedViewModels)
            {
                vm.Controller.SaveConfig();
            }

            MessageBox.Show(ServiceLocator.Current.GetInstance<IApplicationService>().ShellWindow,
                m_SaveConfigCompletedInfoBuffer.ToString(), "Save completed");

            m_SaveConfigCompletedInfoBuffer.Clear();

        }

        public void CancelExcute()
        {
            foreach (var vm in m_ModifiedViewModels)
            {
                vm.Controller.ResetConfig();
            }

            m_SaveConfigCompletedInfoBuffer.Clear();
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