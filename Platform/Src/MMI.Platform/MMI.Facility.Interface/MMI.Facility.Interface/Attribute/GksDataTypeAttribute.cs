// ReSharper disable All
#pragma warning disable 1591
namespace MMI.Facility.Interface.Attribute
{
    /// <summary>
    /// 序列化对象
    /// </summary>
    public class GksDataTypeAttribute : System.Attribute
    {
        public DataType DataType { get; set; }

        public GksDataTypeAttribute(DataType nType) { DataType = nType; }

        public static string GetName() { return "GksDataTypeAttribute"; }
    }

    public enum DataType
    {
        isMMIObjectClass,
        isFunc,
        isMMIAttributes,
    }
}
