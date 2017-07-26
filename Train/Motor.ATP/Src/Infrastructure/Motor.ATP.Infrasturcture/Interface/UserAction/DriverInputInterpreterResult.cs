using System.Diagnostics;

namespace Motor.ATP.Infrasturcture.Interface.UserAction
{
    /// <summary>
    /// 
    /// </summary>
    public static class DriverInputInterpreterResultExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rlt"></param>
        /// <returns></returns>
        public static DriverInputControlWord GetControlWord(this DriverInputInterpreterResult rlt)
        {
            return (DriverInputControlWord) rlt.Tag;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DriverInputInterpreterResult
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly DriverInputInterpreterResult ControlResult = new DriverInputInterpreterResult(null, InputType.Control);

        /// <summary>
        /// 
        /// </summary>
        public static readonly DriverInputInterpreterResult InvalidateResult = new DriverInputInterpreterResult(InputType.Invalidate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputContent"></param>
        /// <param name="driverInputType"></param>
        [DebuggerStepThrough]
        public DriverInputInterpreterResult(string inputContent, InputType driverInputType = InputType.New)
        {
            InputContent = inputContent;
            DriverInputType = driverInputType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driverInputType"></param>
        /// <param name="tag"></param>
        /// <param name="inputContent"></param>
        [DebuggerStepThrough]
        public DriverInputInterpreterResult(InputType driverInputType = InputType.Control, object tag = null, string inputContent = null)
        {
            DriverInputType = driverInputType;
            Tag = tag;
            InputContent = inputContent;
        }

        /// <summary>
        /// 
        /// </summary>
        public enum InputType
        {
            /// <summary>
            /// 无效的
            /// </summary>
            Invalidate,
            /// <summary>
            /// 新的
            /// </summary>
            New,
            /// <summary>
            /// 替换
            /// </summary>
            Replace,
            /// <summary>
            /// 控制
            /// </summary>
            Control,
        }

        /// <summary>
        /// 
        /// </summary>
        public InputType DriverInputType { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string InputContent { private set; get; }

        /// <summary>
        /// 附带的数据
        /// </summary>
        public object Tag { private set; get; }
    }
}