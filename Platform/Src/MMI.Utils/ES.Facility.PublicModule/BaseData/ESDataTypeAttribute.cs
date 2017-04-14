using System;


namespace ES.Facility.PublicModule.BaseData
{
    /// <summary>
    /// 序列化对象时使用
    /// 通用对象必须以该属性进行标记
    /// </summary>
    public class ESDataTypeAttribute : Attribute
    {
        private AttributeType _dataAttribe;

        /// <summary>
        /// 设置属性用
        /// </summary>
        public AttributeType DataAttribe
        {
            get { return _dataAttribe; }
            set { _dataAttribe = value; }
        }

        /// <summary>
        /// 设置属性类型，及在GKS中的类型分类
        /// </summary>
        /// <param name="NType"></param>
        public ESDataTypeAttribute(AttributeType nType) { _dataAttribe = nType; }

        /// <summary>
        /// 获取属性名称
        /// </summary>
        /// <returns></returns>
        public static string GetName() { return "ESAttribute"; }
    }

    /// <summary>
    /// 可序列化的类型，用于GKS类型的内部识别
    /// </summary>
    [Flags]
    public enum AttributeType
    {
        /// <summary>默认方式</summary>
        None = 10,
        
        /// <summary>
        /// 自动控制对象
        /// 该模式主要用于扩展对象的使用，即该类由其他插件类进行阐述
        /// 该类被实例 框架将按顺序调用 初始化接口 启动接口 直至接收到该类的退出消息 将其释放
        /// 接口类型参见 
        /// 交互模式参见 
        /// </summary>
        AutoControlClass = 15,
        
        /// <summary>
        /// 时间控制对象
        /// 该模式由框架定时器进行调用
        /// </summary>
        TimingClass = 17,

        /// <summary>
        /// 消息驱动对象
        /// 该模式由消息进行调用，且自身不实现启动接口
        /// </summary>
        MessaggingClass = 18,

        /// <summary>MMI的插件类</summary>
        isMMIObjectClass = 0,

        /// <summary>方法扩展类</summary>
        isFunc = 1,

        /// <summary>MMI的属性表述类</summary>
        isMMIAttributes = 2,
    }

    /// <summary>
    /// 被主框架调用时，决定正确的调用方式
    /// </summary>
    [Flags]
    public enum CallFunType
    {
        //图形
        Draw = 20,
    }

    public class EsModeType
    {

        protected AttributeType theModeType = AttributeType.None;

        /// <summary>
        /// 功能类型
        /// </summary>
        public AttributeType ModeType { get { return theModeType; } set { theModeType = value; } }


    }


}
