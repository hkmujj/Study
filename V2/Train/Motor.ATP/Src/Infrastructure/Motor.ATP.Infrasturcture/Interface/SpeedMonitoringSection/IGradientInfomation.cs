using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection
{
    /// <summary>
    /// 坡度信息
    /// </summary>
    public interface IGradientInfomation : IATPPartial
    {
        /// <summary>
        /// 坡度集合变化
        /// </summary>
        event EventHandler GradientInfomationItemItemsChanged;

        /// <summary>
        /// 坡度集合
        /// </summary>
        ObservableCollection<IGradientInfomationItem> GradientInfomationItemItems { get; }

        /// <summary>
        /// 更新坡度信息
        /// </summary>
        /// <param name="gradientInfomationItems"></param>
        void UpdateGradientInfomationItems(IEnumerable<IGradientInfomationItem> gradientInfomationItems);

    }
}