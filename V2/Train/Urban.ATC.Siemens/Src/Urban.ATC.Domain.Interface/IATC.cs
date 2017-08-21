using MMI.Facility.Interface.Service;
using Motor.ATP.Domain.Interface;
using System.ComponentModel;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.Domain.Interface
{
    public interface IATC : INotifyPropertyChanged
    {
        CourseState CourseState { get; }

        /// <summary>
        /// 目标距离
        /// </summary>
        double TargetDistance { get; }

        /// <summary>
        /// 目标速度
        /// </summary>
        double TargetSpeed { get; }

        BrakeDetailsType BrakeDetailsType { get; }
        TargetBarType TargetDistanceBarColor { get; }
        IFZoneStatus FZoneStatus { get; }
        ISpeed Speed { get; }

        ICZoneStatus CZoneStatus
        {
            get;
        }

        bool MMIBack { get; }

        IMZoneSates MZoneSates
        {
            get;
        }
    }
}