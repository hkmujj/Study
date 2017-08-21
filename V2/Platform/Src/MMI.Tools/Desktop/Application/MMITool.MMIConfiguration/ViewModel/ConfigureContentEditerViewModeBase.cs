using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using MMITool.Addin.MMIConfiguration.Constant;
using MMITool.Addin.MMIConfiguration.Controller;
using MMITool.Addin.MMIConfiguration.Interface;
using MMITool.Addin.MMIConfiguration.SaveStrategy;

namespace MMITool.Addin.MMIConfiguration.ViewModel
{
    public class ConfigureContentEditerViewModeBase<TController> : NotificationObject, INavigationAware, IConfigureContentEditerViewModel, ITargetConfigProvider
        where TController : IConfigContentController
    {
        private readonly DelegateCommand m_OkCommand;

        private readonly ICommand m_CancelCommand;
        private TController m_Controller;

        private readonly ISaveStrategy m_SaveStrategy;

        public TController Controller
        {
            protected set
            {
                if (!Equals(m_Controller, value))
                {
                    m_Controller = value;
                    m_Controller.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == "IsModified")
                        {
                            m_OkCommand.RaiseCanExecuteChanged();
                            m_SaveStrategy.UpdateSaveItem(this);
                        }
                    };
                }
            }
            get { return m_Controller; }
        }

        IConfigContentController IConfigureContentEditerViewModel.Controller { get { return Controller; } }

        public virtual string TargetConfigFile { get; set; }

        /// <summary>
        /// 文件类型描述
        /// </summary>
        public string FileTypeDescription { get; protected set; }

        private ConfigureContentController m_ConfigureContentController
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ConfigureContentController>();
            }
        }

        public ConfigureContentEditerViewModeBase()
        {
            m_OkCommand = new DelegateCommand(OkExcute, CanOkExcute);
            m_CancelCommand = new DelegateCommand(CancelExcute);
            m_SaveStrategy = ServiceLocator.Current.GetInstance<ISaveStrategy>();
        }

        protected virtual void CancelExcute()
        {
            Controller.ResetConfig();
        }

        protected virtual bool CanOkExcute()
        {
            return m_SaveStrategy.CanSaveExcute;
        }

        protected virtual void OkExcute()
        {
            m_SaveStrategy.SaveExcute();
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            UpdateNavigateToParams(navigationContext);

            m_SaveStrategy.OnNavigatedTo(this, navigationContext);
        }

        protected void UpdateNavigateToParams(NavigationContext navigationContext)
        {
            TargetConfigFile = navigationContext.Parameters[NaviParamKeys.File];
            FileTypeDescription = navigationContext.Parameters[NaviParamKeys.FileTypeDescription];
            
            m_ConfigureContentController.OkCommand = m_OkCommand;
            m_ConfigureContentController.CancelCommand = m_CancelCommand;
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
            m_ConfigureContentController.OkCommand = null;
            m_ConfigureContentController.CancelCommand = null;
        }
    }
}