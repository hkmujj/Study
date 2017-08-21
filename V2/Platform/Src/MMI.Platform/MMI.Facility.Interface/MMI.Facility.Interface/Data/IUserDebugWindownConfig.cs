using System.Drawing;

namespace MMI.Facility.Interface.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserDebugWindownConfig
    {
        /// <summary>
        /// 
        /// </summary>
        string TypeName { get; }

        /// <summary>
        /// 
        /// </summary>
        Rectangle Rectangle { get; }
    }
}