using MMI.Facility.Interface.Data;

namespace MMI.Facility.Interface.Attribute
{
    /// <summary>
    /// 
    /// </summary>
    public class DebugFormAttribute : System.Attribute
    {
        /// <summary>
        /// 所属的启动模式
        /// </summary>
        public StartModel ShowInModel { set; get; }

    }
}