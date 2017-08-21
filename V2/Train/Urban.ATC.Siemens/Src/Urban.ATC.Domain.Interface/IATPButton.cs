using System.Collections.ObjectModel;
using Motor.ATP.Domain.Interface.UserAction;

namespace Motor.ATP.Domain.Interface
{
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