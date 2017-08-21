using Microsoft.Practices.Prism.Events;

namespace MMI.Facility.WPFInfrastructure.Event
{
    /// <summary>
    /// 发送数据请求事件
    /// </summary>
    public class SendDataRequestEvent : CompositePresentationEvent<SendDataRequestEvent.Args>
    {
        /// <summary>
        /// 
        /// </summary>
        public class Args
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="indexString"></param>
            /// <param name="valueB"></param>
            /// <param name="dataType"></param>
            public Args(string indexString, bool valueB, DataType dataType = DataType.OutBool)
            {
                Type = dataType;
                ValueB = valueB;
                IndexString = indexString;
                Index = -1;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="indexString"></param>
            /// <param name="vaf"></param>
            /// <param name="dataType"></param>
            public Args(string indexString, float vaf, DataType dataType = DataType.OutFloat)
            {
                Type = dataType;
                ValueF = vaf;
                IndexString = indexString;
                Index = -1;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="index"></param>
            /// <param name="valueB"></param>
            /// <param name="dataType"></param>
            public Args(int index, bool valueB, DataType dataType = DataType.OutBool)
            {
                Type = dataType;
                ValueB = valueB;
                Index = index;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="index"></param>
            /// <param name="vaf"></param>
            /// <param name="dataType"></param>
            public Args(int index, float vaf, DataType dataType = DataType.OutFloat)
            {
                Type = dataType;
                ValueF = vaf;
                Index = index;
            }

            /// <summary>
            /// 数据类型
            /// </summary>
            public DataType Type { get; private set; }

            /// <summary>
            /// string 型索引
            /// </summary>
            public string IndexString { private set; get; }

            /// <summary>
            /// int 型索引 
            /// </summary>
            public int Index { get; private set; }

            /// <summary>
            /// ValueB
            /// </summary>
            public bool ValueB { get; private set; }

            /// <summary>
            /// ValueF
            /// </summary>
            public float ValueF { get; private set; }
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public enum DataType
        {
            /// <summary>
            /// InBool
            /// </summary>
            InBool,

            /// <summary>
            /// OutBool
            /// </summary>
            OutBool,

            /// <summary>
            /// InFloat
            /// </summary>
            InFloat,

            /// <summary>
            /// OutFloat
            /// </summary>
            OutFloat,
        }

    }
}