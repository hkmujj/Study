using Motor.ATP.Infrasturcture.Model;

namespace Motor.ATP._200H.Subsys.Model
{
    public class GlobalParam : GlobalParamBase
    {
        public static readonly GlobalParam Instance = new GlobalParam();

        public GlobalParam()
        {
            ButtonManager = new ButtonManager();
            GlobalTimer = Model.GlobalTimer.Instance;
        }

        public ButtonManager ButtonManager { get; private set; }
    }
}