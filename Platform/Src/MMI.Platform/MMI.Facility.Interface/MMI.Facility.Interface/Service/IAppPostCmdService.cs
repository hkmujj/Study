using MMI.Facility.Interface.Data;

namespace MMI.Facility.Interface.Service
{
    /// <summary>
    /// 用户使用 app_postcmd 的服务 
    /// </summary>
    public interface IAppPostCmdService
    {
        /// <summary>
        /// 
        /// </summary>
        string AppName { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="nCT"></param>
        /// <param name="paraA"></param>
        /// <param name="paraB"></param>
        /// <param name="paraC"></param>
        void PostCmdA(int index, CmdType nCT, int paraA, int paraB, float paraC);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="nCT"></param>
        /// <param name="paraA"></param>
        /// <param name="paraB"></param>
        /// <param name="paraC"></param>
        /// <param name="str"></param>
        void PostCmdB(int index, CmdType nCT, int paraA, int paraB, float paraC, string str);
    }
}
