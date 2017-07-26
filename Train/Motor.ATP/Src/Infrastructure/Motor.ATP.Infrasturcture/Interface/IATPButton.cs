using System.Collections.ObjectModel;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// ATP 硬件按键
    /// </summary>
    public interface IATPHardwareButton : IATPPartial
    {
        /// <summary>
        /// 按键状态表
        /// </summary>
        ReadOnlyCollection<IHardwareButton> HardwareButtonCollection { get; }

        /// <summary>
        /// 
        /// </summary>
        IHardwareButtonViewModel HardwareButtonViewModel { get; }
    }
}