using System;
using System.Collections.Generic;
using MMI.Facility.Interface.Args;

namespace MMI.Facility.Interface.Running
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRunningViewParam
    {
        /// <summary>
        /// 当前运行参数的工程名
        /// </summary>
        string ProjectName { get; }

        /// <summary>
        /// key : 视图编号
        /// </summary>
        Dictionary<int, IRunningViewUnitParam> ViewUnitParamDic { get; }

        /// <summary>
        /// 当前视图
        /// </summary>
        IRunningViewUnitParam CurrentRunningViewUnitParam { set; get; }

        /// <summary>
        /// 
        /// </summary>
        event Action<DataChangedArgs<IRunningViewUnitParam>> CurrentRunningViewUnitParamChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        void SelectCurrentRunningViewUnitParam(int index);
    }
}
