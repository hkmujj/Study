using MMI.Facility.Interface.Project;

namespace SH_Reconnect
{
    public class GlobalParam
    {
        public static readonly GlobalParam Instance = new GlobalParam();

        public SubsystemInitParam InitParam { get; private set; }

        public void Initalize(SubsystemInitParam initParam)
        {
            InitParam = initParam;
        }
    }
}