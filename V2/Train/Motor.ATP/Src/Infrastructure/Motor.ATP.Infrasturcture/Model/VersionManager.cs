using System;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.ViewModel;
using Motor.ATP.Infrasturcture.Interface;

namespace Motor.ATP.Infrasturcture.Model
{
    public class VersionManager : NotificationObject, IVersionManager
    {
        private Version m_CurrentVersion;
        private string m_CFSK;
        private Version m_DMIAssistVersion;
        private Version m_DMIMainVersion;

        /// <summary>
        /// 当前版本
        /// </summary>
        public Version MainVersion
        {
            get { return m_CurrentVersion; }
            set
            {
                if (Equals(value, m_CurrentVersion))
                {
                    return;
                }
                m_CurrentVersion = value;
                RaisePropertyChanged(() => MainVersion);
            }
        }

        /// <summary>
        /// 主屏版本
        /// </summary>
        public Version DMIMainVersion
        {
            get { return m_DMIMainVersion; }
            set
            {
                if (Equals(value, m_DMIMainVersion))
                {
                    return;
                }
                m_DMIMainVersion = value;
                RaisePropertyChanged(() => DMIMainVersion);
            }
        }

        /// <summary>
        /// 辅屏版本
        /// </summary>
        public Version DMIAssistVersion
        {
            get { return m_DMIAssistVersion; }
            set
            {
                if (Equals(value, m_DMIAssistVersion))
                {
                    return;
                }
                m_DMIAssistVersion = value;
                RaisePropertyChanged(() => DMIAssistVersion);
            }
        }

        public string CFSK
        {
            get { return m_CFSK; }
            set
            {
                if (value == m_CFSK)
                {
                    return;
                }
                m_CFSK = value;
                RaisePropertyChanged(() => CFSK);
            }
        }

        public Lazy<ReadOnlyCollection<Version>> KnownVersions { get; set; }
    }
}