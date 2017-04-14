using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Win32;
using MMI.Facility.Control.Data;
using MMI.Facility.WPFInfrastructure.Interfaces;
using MMITool.Addin.MMIConfiguration.Constant;
using MMITool.Addin.MMIConfiguration.Model;
using MMITool.Addin.MMIConfiguration.Service;
using MMITool.Addin.MMIConfiguration.View.ConfigureContent;
using MMITool.Addin.MMIConfiguration.ViewModel;
using MMITool.Common.Util;

namespace MMITool.Addin.MMIConfiguration.Controller
{
    [Export]
    public class SelectExePathController : ControllerBase<SelectExePathViewModel>, IDisposable
    {
        private IEnumerable<ConfigTypeMapInfo> m_ConfigTypeMapInfoCollection
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ConfigureRepository>().AllConfigTypeMapInfos;
            }
        }

        public override SelectExePathViewModel ViewModel
        {
            protected get { return m_ViewModel; }
            set
            {
                if (Equals(value, m_ViewModel))
                {
                    return;
                }
                if (ViewModel != null)
                {
                    ViewModel.Model.PropertyChanged -= ModelOnPropertyChanged;
                }
                m_ViewModel = value;

                if (ViewModel != null)
                {
                    ViewModel.Model.PropertyChanged += ModelOnPropertyChanged;
                }
            }
        }


        private OpenFileDialog m_OpenFileDialog;
        private readonly IRegionManager m_RegionManager;

        private readonly IApplicationService m_ApplicationService;

        private Process m_Process;
        private bool m_IsStartingProcess;
        private bool m_NeedQueryProcess;

        /// <summary>
        /// 打开文件选择框命令
        /// </summary>
        public ICommand SelectFileCommand { private set; get; }

        public DelegateCommand StartSelectedExeCommand { get; private set; }

        /// <summary>
        /// 获取文件夹下所有文件
        /// </summary>
        public DelegateCommand ParserConfigCommand { private set; get; }

        public ICommand ValidationErrorCommand { private set; get; }

        public DelegateCommand OpenExeDirectoryCommand { private set; get; }

        private readonly object m_LockObj = new object();
        private SelectExePathViewModel m_ViewModel;

        private bool IsStartingProcess
        {
            set
            {
                lock (m_LockObj)
                {
                    m_IsStartingProcess = value; 
                    StartSelectedExeCommand.RaiseCanExecuteChanged();
                }
            }
            get { return m_IsStartingProcess; }
        }

        [ImportingConstructor]
        public SelectExePathController(IRegionManager regionManager, IApplicationService applicationService)
        {
            m_RegionManager = regionManager;
            m_ApplicationService = applicationService;
            SelectFileCommand = new DelegateCommand(SelectFileExcute);
            ParserConfigCommand = new DelegateCommand(ParserConfigExcute, HasRightFile);
            ValidationErrorCommand = new DelegateCommand(ValidationErrorExcute);
            StartSelectedExeCommand = new DelegateCommand(OnStartSelectedExe, CanStartSelectedExe);

            OpenExeDirectoryCommand = new DelegateCommand(OpenExeDirectory, HasRightFile);

        }

        private void OpenExeDirectory()
        {
            try
            {
                Process.Start(Path.GetDirectoryName(ViewModel.Model.FileFullName));
            }
            catch (Exception e)
            {
                LogMgr.Debug(String.Format("Can not open directory {0}", e));
            }
        }

        private void ModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            StartSelectedExeCommand.RaiseCanExecuteChanged();
            OpenExeDirectoryCommand.RaiseCanExecuteChanged();
        }

        private bool CanStartSelectedExe()
        {
            return HasRightFile() && !IsStartingProcess;
        }

        private void OnStartSelectedExe()
        {
            try
            {
                m_NeedQueryProcess = true;
                m_Process = new Process
                {
                    StartInfo = new ProcessStartInfo(ViewModel.Model.FileFullName){ WorkingDirectory = Path.GetDirectoryName(ViewModel.Model.FileFullName)},
                    EnableRaisingEvents = true
                };
                m_Process.Exited += (sender, args) =>
                {
                    IsStartingProcess = false;
                    m_NeedQueryProcess = false;
                };
                m_Process.Start();

                IsStartingProcess = true;

                ThreadPool.QueueUserWorkItem(QueryProcessStarted, m_Process);
            }
            catch (Exception e)
            {
                LogMgr.Error(String.Format("Can not start {0}, error = {1}", ViewModel.Model.FileFullName, e));
            }
        }

        private void QueryProcessStarted(object state)
        {
            var p = (Process) state;

            var cnt = 0;
            while (m_NeedQueryProcess)
            {
                Thread.Sleep(3000);
                ++cnt;
                if (cnt >= 5)
                {
                    m_NeedQueryProcess = false;
                }
            }

            IsStartingProcess = false;
        }

        private void ValidationErrorExcute()
        {
        }

        /// <summary>
        /// 打开文件选择框
        /// </summary>
        void SelectFileExcute()
        {
            m_OpenFileDialog = new OpenFileDialog { InitialDirectory = AppDomain.CurrentDomain.BaseDirectory, Filter = "应用程序|*.exe" };
            if ((bool)m_OpenFileDialog.ShowDialog())
            {
                ViewModel.Model.FileFullName = m_OpenFileDialog.FileName;
            }
        }

        private bool HasRightFile()
        {
            return !ViewModel.Model.HasValidationError && File.Exists(ViewModel.Model.FileFullName);
        }


        /// <summary>
        /// 打开文件
        /// </summary>
        void ParserConfigExcute()
        {
            if (string.IsNullOrEmpty(ViewModel.Model.FileFullName))
            {
                return;
            }

          
            try
            {
                m_RegionManager.RequestNavigate(ConfigurationRegionNames.ConfigureEditRegion, new Uri(typeof(NoneConfigureContentView).Name, UriKind.Relative));

                ViewModel.Model.ConfigConfigFileCollection.Clear();
                var basePath = Path.GetDirectoryName(ViewModel.Model.FileFullName);
                string[] files = Directory.GetFileSystemEntries(basePath, "*.xml", SearchOption.AllDirectories);
                foreach (
                    var model in
                        files.Where(
                            file =>
                                m_ConfigTypeMapInfoCollection.FirstOrDefault(
                                    f => f.ConfigureDescription.FileName == Path.GetFileName(file)) != null)
                            .Select(
                                file =>
                                    new ConfigFileModel(basePath)
                                    {
                                        FileFullName = file,
                                        ConfigTypeMapInfo =
                                            m_ConfigTypeMapInfoCollection.FirstOrDefault(
                                                f => f.ConfigureDescription.FileName == Path.GetFileName(file))
                                    }))
                {
                    ViewModel.Model.ConfigConfigFileCollection.Add(model);
                }

                ConfigManager.Instance.LoadConfig(Path.GetDirectoryName(ViewModel.Model.FileFullName));
            }
            catch(Exception e)
            {
                MessageBox.Show(m_ApplicationService.ShellWindow, e.ToString(), "Parser Config error");
                return;
            }

            UpdateSelectableItems();
        }

        public void UpdateSelectableItems(IEnumerable<string> selectables)
        {
            ViewModel.Model.SelctableFullNames.Clear();
            ViewModel.Model.SelctableFullNames.AddRange(selectables.Take(SelectExePathModel.MaxSelectableItemCount));
        }

        private void UpdateSelectableItems()
        {
            if (!ViewModel.Model.SelctableFullNames.Contains(ViewModel.Model.FileFullName))
            {
                ViewModel.Model.SelctableFullNames.Insert(0, ViewModel.Model.FileFullName);
            }
            else if (ViewModel.Model.SelctableFullNames.FirstOrDefault() != ViewModel.Model.FileFullName)
            {
                ViewModel.Model.SelctableFullNames.Move(
                    ViewModel.Model.SelctableFullNames.IndexOf(ViewModel.Model.FileFullName), 0);
            }

            var config = ServiceLocator.Current.GetInstance<ConfigurationParam>();
            config.SaveSelectableItems(ViewModel.Model.SelctableFullNames);
        }

        public void Dispose()
        {
            m_NeedQueryProcess = false;
        }
    }
}
