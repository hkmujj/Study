using System.Diagnostics;

namespace Motor.ATP.Domain.Interface.UserAction
{
    public static class DriverInputInterpreterResultExtension
    {
        public static DriverInputControlWord GetControlWord(this DriverInputInterpreterResult rlt)
        {
            return (DriverInputControlWord) rlt.Tag;
        }
    }

    public class DriverInputInterpreterResult
    {
        public static readonly DriverInputInterpreterResult ControlResult = new DriverInputInterpreterResult(null, InputType.Control);

        public static readonly DriverInputInterpreterResult InvalidateResult = new DriverInputInterpreterResult(InputType.Invalidate);

        [DebuggerStepThrough]
        public DriverInputInterpreterResult(string inputContent, InputType driverInputType = InputType.New)
        {
            InputContent = inputContent;
            DriverInputType = driverInputType;
        }

        [DebuggerStepThrough]
        public DriverInputInterpreterResult(InputType driverInputType = InputType.Control, object tag = null, string inputContent = null)
        {
            DriverInputType = driverInputType;
            Tag = tag;
            InputContent = inputContent;
        }

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

        public InputType DriverInputType { private set; get; }

        public string InputContent { private set; get; }

        /// <summary>
        /// 附带的数据
        /// </summary>
        public object Tag { private set; get; }
    }
}