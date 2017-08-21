using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Excel.Interface;

namespace Engine.TPX21F.HXN5B.Model.ConfigModel
{
    [ExcelLocation("Engine.TPX21F.HXN5B自测试元素集.xls", "Sheet1")]
    [DebuggerDisplay("GroupName={GroupName}, Content={Content}")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class SelfTestItemConfig : ISetValueProvider
    {
        [ExcelField("组名")]
        public string GroupName { get; private set; }

        [ExcelField("内容")]
        public string Content { get; private set; }

        [ExcelField("接通按键发送数据索引")]
        public string CnontectIndexKey { get; private set; }

        [ExcelField("断开按键发送数据索引")]
        public string DiscnontectIndexKey { get; private set; }

        [ExcelField("描述信息")]
        public string Description { get; private set; }

        [ExcelField("元素标题1")]
        public string ItemTitle1 { get; private set; }

        [ExcelField("元素标题2")]
        public string ItemTitle2 { get; private set; }

        [ExcelField("元素标题3")]
        public string ItemTitle3 { get; private set; }

        [ExcelField("元素标题4")]
        public string ItemTitle4 { get; private set; }

        [ExcelField("元素标题5")]
        public string ItemTitle5 { get; private set; }

        [ExcelField("元素标题6")]
        public string ItemTitle6 { get; private set; }

        [ExcelField("元素标题7")]
        public string ItemTitle7 { get; private set; }

        [ExcelField("元素标题8")]
        public string ItemTitle8 { get; private set; }

        [ExcelField("元素标题9")]
        public string ItemTitle9 { get; private set; }

        [ExcelField("元素标题10")]
        public string ItemTitle10 { get; private set; }

        [ExcelField("元素标题11")]
        public string ItemTitle11 { get; private set; }

        [ExcelField("元素标题12")]
        public string ItemTitle12 { get; private set; }

        [ExcelField("元素标题13")]
        public string ItemTitle13 { get; private set; }

        [ExcelField("元素标题14")]
        public string ItemTitle14 { get; private set; }

        [ExcelField("元素标题15")]
        public string ItemTitle15 { get; private set; }

        [ExcelField("值格式1")]
        public string ValueStringFormat1 { get; private set; }

        [ExcelField("值格式2")]
        public string ValueStringFormat2 { get; private set; }

        [ExcelField("值格式3")]
        public string ValueStringFormat3 { get; private set; }

        [ExcelField("值格式4")]
        public string ValueStringFormat4 { get; private set; }

        [ExcelField("值格式5")]
        public string ValueStringFormat5 { get; private set; }

        [ExcelField("值格式6")]
        public string ValueStringFormat6 { get; private set; }

        [ExcelField("值格式7")]
        public string ValueStringFormat7 { get; private set; }

        [ExcelField("值格式8")]
        public string ValueStringFormat8 { get; private set; }

        [ExcelField("值格式9")]
        public string ValueStringFormat9 { get; private set; }

        [ExcelField("值格式10")]
        public string ValueStringFormat10 { get; private set; }

        [ExcelField("值格式11")]
        public string ValueStringFormat11 { get; private set; }

        [ExcelField("值格式12")]
        public string ValueStringFormat12 { get; private set; }

        [ExcelField("值格式13")]
        public string ValueStringFormat13 { get; private set; }

        [ExcelField("值格式14")]
        public string ValueStringFormat14 { get; private set; }

        [ExcelField("值格式15")]
        public string ValueStringFormat15 { get; private set; }

        [ExcelField("Keys1")]
        public List<string> Keys1 { private set; get; }

        [ExcelField("值类型1")]
        public string ValueType1 { get; private set; }

        [ExcelField("Keys2")]
        public List<string> Keys2 { private set; get; }

        [ExcelField("值类型2")]
        public string ValueType2 { get; private set; }

        [ExcelField("Keys3")]
        public List<string> Keys3 { private set; get; }

        [ExcelField("值类型3")]
        public string ValueType3 { get; private set; }

        [ExcelField("Keys4")]
        public List<string> Keys4 { private set; get; }

        [ExcelField("值类型4")]
        public string ValueType4 { get; private set; }

        [ExcelField("Keys5")]
        public List<string> Keys5 { private set; get; }

        [ExcelField("值类型5")]
        public string ValueType5 { get; private set; }

        [ExcelField("Keys6")]
        public List<string> Keys6 { private set; get; }

        [ExcelField("值类型6")]
        public string ValueType6 { get; private set; }

        [ExcelField("Keys7")]
        public List<string> Keys7 { private set; get; }

        [ExcelField("值类型7")]
        public string ValueType7 { get; private set; }

        [ExcelField("Keys8")]
        public List<string> Keys8 { private set; get; }

        [ExcelField("值类型8")]
        public string ValueType8 { get; private set; }

        [ExcelField("Keys9")]
        public List<string> Keys9 { private set; get; }

        [ExcelField("值类型9")]
        public string ValueType9 { get; private set; }

        [ExcelField("Keys10")]
        public List<string> Keys10 { private set; get; }

        [ExcelField("值类型10")]
        public string ValueType10 { get; private set; }

        [ExcelField("Keys11")]
        public List<string> Keys11 { private set; get; }

        [ExcelField("值类型11")]
        public string ValueType11 { get; private set; }

        [ExcelField("Keys12")]
        public List<string> Keys12 { private set; get; }

        [ExcelField("值类型12")]
        public string ValueType12 { get; private set; }

        [ExcelField("Keys13")]
        public List<string> Keys13 { private set; get; }

        [ExcelField("值类型13")]
        public string ValueType13 { get; private set; }

        [ExcelField("Keys14")]
        public List<string> Keys14 { private set; get; }

        [ExcelField("值类型14")]
        public string ValueType14 { get; private set; }

        [ExcelField("Keys15")]
        public List<string> Keys15 { private set; get; }

        [ExcelField("值类型15")]
        public string ValueType15 { get; private set; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "Keys1":
                    Keys1 = value.Split(';').ToList();
                    break;
                case "Keys2":
                    Keys2 = value.Split(';').ToList();
                    break;
                case "Keys3":
                    Keys3 = value.Split(';').ToList();
                    break;
                case "Keys4":
                    Keys4 = value.Split(';').ToList();
                    break;
                case "Keys5":
                    Keys5 = value.Split(';').ToList();
                    break;
                case "Keys6":
                    Keys6 = value.Split(';').ToList();
                    break;
                case "Keys7":
                    Keys7 = value.Split(';').ToList();
                    break;
                case "Keys8":
                    Keys8 = value.Split(';').ToList();
                    break;
                case "Keys9":
                    Keys9 = value.Split(';').ToList();
                    break;
                case "Keys10":
                    Keys10 = value.Split(';').ToList();
                    break;
                case "Keys11":
                    Keys11 = value.Split(';').ToList();
                    break;
                case "Keys12":
                    Keys12 = value.Split(';').ToList();
                    break;
                case "Keys13":
                    Keys13 = value.Split(';').ToList();
                    break;
                case "Keys14":
                    Keys14 = value.Split(';').ToList();
                    break;
                case "Keys15":
                    Keys15 = value.Split(';').ToList();
                    break;
            }
        }
    }
}