using MMI.Facility.Interface.Project;

namespace ShenHuaHaoTMS
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