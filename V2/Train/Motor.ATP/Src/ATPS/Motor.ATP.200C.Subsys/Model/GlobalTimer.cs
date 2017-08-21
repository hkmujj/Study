using Motor.ATP.Infrasturcture.Model;

namespace Motor.ATP._200C.Subsys.Model
{
    public sealed class GlobalTimer : GlobalTimerBase
    {
        public static readonly GlobalTimer Instance = new GlobalTimer();

        private GlobalTimer()
        {
            
        }
    }
}