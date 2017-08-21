using System.Collections.ObjectModel;
using Urban.Domain.TrainState.Interface.Common;

namespace Urban.Domain.TrainState.Interface
{
    /// <summary>
    /// 轴
    /// </summary>
    public interface IAxis : IFaultable, IIdentityProvide, IStateUpdatable, IListeningModelProvider
    {
        
        /// <summary>
        /// 轴距
        /// </summary>
        double Wheelbase { get; }

        /// <summary>
        /// 轮子 
        /// </summary>
        ReadOnlyCollection<IWheel>  Wheels { get; }

        /// <summary>
        /// 制动状态
        /// </summary>
        IBraking Braking { get; }

    }
}
