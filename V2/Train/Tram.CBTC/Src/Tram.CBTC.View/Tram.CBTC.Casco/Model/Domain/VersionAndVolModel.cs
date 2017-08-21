using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Tram.CBTC.Casco.Model.Domain
{
    [Export]
    public class VersionAndVolModel : NotificationObject
    {
        public VersionAndVolModel()
        {
            ApplicationVersion = "1.0.0";
            DataVersion = "1.0.0";
            DataDateTime = (new DateTime(2017, 03, 07, 08, 55, 36)).ToString("yyyy年MM月DD日 HH:mm:ss");
            BuildDateTime = (new DateTime(2017, 03, 07, 09, 19, 0)).ToString("yyyy年MM月DD日 HH:mm:ss");
        }
        private string m_ApplicationVersion;
        private string m_DataVersion;
        private string m_DataDateTime;
        private string m_BuildDateTime;

        public string ApplicationVersion
        {
            get { return m_ApplicationVersion; }
            set
            {
                if (value == m_ApplicationVersion)
                    return;

                m_ApplicationVersion = value;
                RaisePropertyChanged(() => ApplicationVersion);
            }
        }

        public string DataVersion
        {
            get { return m_DataVersion; }
            set
            {
                if (value == m_DataVersion)
                    return;

                m_DataVersion = value;
                RaisePropertyChanged(() => DataVersion);
            }
        }

        public string DataDateTime
        {
            get { return m_DataDateTime; }
            set
            {
                if (value == m_DataDateTime)
                    return;

                m_DataDateTime = value;
                RaisePropertyChanged(() => DataDateTime);
            }
        }

        public string BuildDateTime
        {
            get { return m_BuildDateTime; }
            set
            {
                if (value == m_BuildDateTime)
                    return;

                m_BuildDateTime = value;
                RaisePropertyChanged(() => BuildDateTime);
            }
        }
    }
}
