using System.Windows;
using System.Windows.Controls;
using Subway.ShenZhenLine7.Models.Units;

namespace Subway.ShenZhenLine7.Views.Custom
{
    /// <summary>
    /// 高速断路器控件
    /// </summary>
    public class HightSpeedControl : Control
    {

        /// <summary>
        /// Get Or Set The HightSpped State
        /// </summary>
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
            "State", typeof(HighSpeedState), typeof(HightSpeedControl), new PropertyMetadata(default(HighSpeedState)));

        /// <summary>
        /// 高速断路器状态
        /// </summary>
        public HighSpeedState State { get { return (HighSpeedState)GetValue(StateProperty); } set { SetValue(StateProperty, value); } }
    }
}