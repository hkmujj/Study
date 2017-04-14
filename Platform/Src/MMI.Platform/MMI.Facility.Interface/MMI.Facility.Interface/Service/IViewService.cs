using System;
using System.Collections.ObjectModel;
using System.Drawing;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;

namespace MMI.Facility.Interface.Service
{
    /// <summary>
    /// 
    /// </summary>
    public interface IViewService : IService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectName"></param>
        void Active(string projectName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="location"></param>
        void Active(string projectName, Point location);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectName"></param>
        void Deactive(string projectName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="form"></param>
        void Regist(string projectName, ProjectFormBase form);

        /// <summary>
        /// 当前屏元素表。
        /// </summary>
        ReadOnlyCollection<IScreenItem> CurrentScreenItems { get; }

        /// <summary>
        /// 当前屏元素表 变化 
        /// </summary>
        event Action<IViewService, ReadOnlyCollection<IScreenItem>> CurrentScreenItemsChanged;
    }
}
