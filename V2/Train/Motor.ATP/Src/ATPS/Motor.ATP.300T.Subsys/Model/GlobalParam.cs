using Motor.ATP.Infrasturcture.Model;

namespace Motor.ATP._300T.Subsys.Model
{
    public class GlobalParam : GlobalParamBase
    {
        public static readonly GlobalParam Instance  = new GlobalParam();

        private GlobalParam()
        {
            GlobalTimer = Model.GlobalTimer.Instance;
        }
    }
}