using MMI.Facility.Interface.Project;

namespace TestSubsystem.Constant
{
    public class SubParam
    {
        public const string AppName = "TestSubsystem";

        public static readonly SubParam Instance = new SubParam();

        private SubParam()
        {
            
        }

        public SubsystemInitParam SubsystemInitParam { set; get; }
    }
}