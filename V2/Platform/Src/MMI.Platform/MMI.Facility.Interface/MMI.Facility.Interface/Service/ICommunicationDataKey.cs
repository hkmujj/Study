using System.Collections.Generic;
using System.Diagnostics;
using MMI.Facility.Interface.Data.Config;

namespace MMI.Facility.Interface.Service
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommunicationDataKey : IEqualityComparer<ICommunicationDataKey>
    {
        /// <summary>
        /// 
        /// </summary>
        ProjectType ProjectType { get; }

        /// <summary>
        /// 
        /// </summary>
        string AppName { get; }

        /// <summary>
        /// 
        /// </summary>
        string KeyString { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string ToString();


    }

    /// <summary>
    /// 
    /// </summary>
    public class CommunicationDataKey : ICommunicationDataKey
    {
        /// <summary>
        /// Empty
        /// </summary>
        public static readonly CommunicationDataKey Empty = new CommunicationDataKey(ProjectType.Unkown, string.Empty);

        public ProjectType ProjectType { get; private set; }

        public string AppName { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        [DebuggerStepThrough]
        public CommunicationDataKey(IAppConfig appConfig)
        {
            if (appConfig != null)
            {
                ProjectType = appConfig.ProjectType;
                AppName = appConfig.AppName;
            }
            UpdateKeyString();
        }

        /// <summary>
        /// 
        /// </summary>
        [DebuggerStepThrough]
        public CommunicationDataKey(ProjectType projectType, string appName)
        {
            KeyString = string.Empty;
            ProjectType = projectType;
            AppName = appName;
            UpdateKeyString();
        }

        public string KeyString { get; private set; }

        private void UpdateKeyString()
        {
            KeyString = string.Format("{0} : {1}", ProjectType, AppName);
        }

        public override string ToString()
        {
            return KeyString;
        }

        public bool Equals(ICommunicationDataKey x, ICommunicationDataKey y)
        {
            return x.KeyString == y.KeyString;
        }

        public int GetHashCode(ICommunicationDataKey obj)
        {
            return obj.KeyString.GetHashCode();
        }
    }
}