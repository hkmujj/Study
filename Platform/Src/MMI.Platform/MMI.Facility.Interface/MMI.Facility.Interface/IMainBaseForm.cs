using System;
using MMI.Facility.Interface.Addins;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Hook;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMainBaseForm 
    {
        /// <summary>
        /// 保存类对象类型 ,key :文件名+类名 value: type
        /// </summary>
        IReadOnlyDictionary<string, Type> AddinTypeDic { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eHotKeyValue"></param>
        void HotKeyEvent(HotKeys eHotKeyValue);

        /// <summary>
        /// 
        /// </summary>
        IAddInLoader AddInLoader { get; }

        /// <summary>
        /// 图元对象
        /// </summary>
        IViewObjectManager ViewObjectMgr { set; get; }

        /// <summary>
        /// 配置文件管理 
        /// </summary>
        IConfigManager ConfigManager { set; get; }

        /// <summary>
        /// 
        /// </summary>
        ICommunicationDataFacadeService CommunicationDataService { get; }

        /// <summary>
        /// 
        /// </summary>
        IDataPackage DataPackage { set; get; }
    }
}
