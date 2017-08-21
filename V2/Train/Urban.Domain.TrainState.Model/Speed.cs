using System.ComponentModel;
using Urban.Domain.TrainState.Interface;
using Urban.Domain.TrainState.Interface.Common;

namespace Urban.Domain.TrainState.Model
{
    public class Speed : UpdatingProvider<Speed>, ISpeed
    {
        public bool Visible { get; set; }
        public SpeedModel CurrentSpeed { get; set; }

        ISpeedModel ISpeed.CurrentSpeed
        {
            get { return CurrentSpeed; }
        }

        public SpeedModel LimitedSpeed { get; set; }

        ISpeedModel ISpeed.LimitedSpeed
        {
            get { return LimitedSpeed; }
        }

        public Speed()
        {
            CurrentSpeed = new SpeedModel();
            LimitedSpeed = new SpeedModel();
        }
    }
}