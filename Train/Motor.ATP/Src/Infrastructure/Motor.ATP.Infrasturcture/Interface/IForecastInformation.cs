using System.Collections.Generic;
using System.Collections.ObjectModel;
using Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// 预告信息
    /// </summary>
    public interface IForecastInformation : IATPPartial
    {
        /// <summary>
        /// 所有预告信息
        /// </summary>
        ReadOnlyCollection<IForecastInformationItem> ForecastInformationItems { get; }

        /// <summary>
        /// 第一排的预告
        /// </summary>
        IEnumerable<IForecastInformationItem> UpForecastInformationItems { get; }

        /// <summary>
        /// 第二排的
        /// </summary>
        IEnumerable<IForecastInformationItem> DowForecastInformationItems { get; }

        /// <summary>
        /// 命令区的
        /// </summary>
        IEnumerable<IForecastInformationItem> CmdForecastInformationItems { get; }

        /// <summary>
        /// 隔离
        /// </summary>
        IForecastInformationItem CmdIsolatedItem { get; }

        /// <summary>
        /// 减速信息
        /// </summary>
        ObservableCollection<ISpeedChangeInfo> DecelerateInfos { get; }
    }
}