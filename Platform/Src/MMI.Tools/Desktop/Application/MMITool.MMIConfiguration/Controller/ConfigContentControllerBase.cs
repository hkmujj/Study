using System.Windows;
using CommonUtil.Util;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Control.Data;
using MMI.Facility.WPFInfrastructure.Interfaces;
using MMITool.Addin.MMIConfiguration.Events;
using MMITool.Addin.MMIConfiguration.Interface;
using MMITool.Addin.MMIConfiguration.Service;

namespace MMITool.Addin.MMIConfiguration.Controller
{
    public abstract class ConfigContentControllerBase<T> : NotificationObject, IConfigContentController<T>
        where T : ITargetConfigProvider
    {
        public virtual T ViewModel { get; set; }

        public bool IsModified
        {
            get { return m_IsModified; }
            protected set
            {
                if (value.Equals(m_IsModified))
                {
                    return;
                }
                m_IsModified = value;
                RaisePropertyChanged(() => IsModified);
            }
        }


        protected abstract bool HasInitalzied { get; }

        // ReSharper disable once InconsistentNaming
        protected ConfigFileManager m_ConfigFileManager;
        private bool m_IsModified;

        private IEventAggregator m_EventAggregator;

        protected ConfigContentControllerBase()
        {
            IsModified = true;
            m_ConfigFileManager = ServiceLocator.Current.GetInstance<ConfigFileManager>();
            m_EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            m_EventAggregator.GetEvent<ParserConfigCompletedEvent>().Subscribe(s =>
            {
                ViewModel.TargetConfigFile = null;
                ResetConfig();
            });
        }

        public virtual void SaveConfig()
        {

        }

        public virtual void ResetConfig()
        {

        }

        public void NavigateToThis()
        {
            if (!HasInitalzied)
            {
                ResetConfig();
            }
        }


        protected void SaveConfig<TConfig>(TConfig data, string targetConfigFile)
        {
            DataSerialization.SerializeToXmlFile(data, targetConfigFile);

            m_EventAggregator.GetEvent<SaveConfigCompletedEvent>()
                .Publish(string.Format("{0} : Save config to file {1} completed!", ViewModel.FileTypeDescription,
                    targetConfigFile));

        }

        protected void SaveConfig<TConfig>(TConfig data)
        {
            SaveConfig(data, ViewModel.TargetConfigFile);
        }

        protected MMI.Facility.DataType.Config.Config ConcreateRootConfig
        {
            get { return ConfigManager.Instance.Config; }
        }
    }
}