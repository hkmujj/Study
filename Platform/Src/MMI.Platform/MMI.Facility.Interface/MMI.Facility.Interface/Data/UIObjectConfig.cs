using System.Collections.Generic;
using MMI.Facility.Interface.Data.Config;

namespace MMI.Facility.Interface.Data
{
    /// <summary>
    /// 对象配置文件解析
    /// </summary>
    public class UIObjectConfig : IAppObjcetConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public UIObjectConfig()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="appName"></param>
        /// <param name="uiObjects"></param>
        public UIObjectConfig(string filename, string appName, List<IUIObject> uiObjects )
        {
            FileName = filename;
            AppName = appName;
            UIObjects = uiObjects;
        }

        /// <summary>
        /// 
        /// </summary>
        public IAppConfig ParentConfig { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { internal set; get; }

        /// <summary>
        /// 原始信息
        /// </summary>
        public List<IAppBaseObjectInfo> AppBaseObjectInfo { get; private set; }

        /// <summary>
        /// 工程名
        /// </summary>
        public string AppName { set; get; }

        /// <summary>
        /// 解析文件得到的
        /// </summary>
        public List<IUIObject> UIObjects { set; get; }
    }
}
