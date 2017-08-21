using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.DataType.Running
{
    public interface IRunningViewService : IViewService
    {
        event Action<IRunningViewService, NotifyCollectionChangedAction> AcitvedFormChanged;
        event Action<IRunningViewService, NotifyCollectionChangedEventArgs> AllViewFormChanged;

        void Initalize(ViewServiceInitalizeParam initalizeParam);

        ReadOnlyCollection<ProjectFormBase> AllViewFormCollection { get; }

        ReadOnlyCollection<ProjectFormBase> ActivedFormCollection { get; }

        /// <summary>
        /// 更新当前显示的界面元素
        /// </summary>
        /// <param name="screenItems"></param>
        void UpdateCurrentScreenItems(IEnumerable<IScreenItem> screenItems);
    }
}