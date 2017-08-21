using System;
using Excel.Interface;

namespace Subway.WuHanLine6.FaultInfos
{
    /// <summary>
    /// 故障信息
    /// </summary>
    [ExcelLocation("故障信息.xls", "Sheet1")]
    public class FaultInfo : ISetValueProvider
    {
        static FaultInfo()
        {
            Empty = new FaultInfo();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FaultInfo()
        {
            BtnStr = "提示";
        }

        /// <summary>
        ///
        /// </summary>
        public string BtnStr { get; set; }

        /// <summary>
        /// 空故障
        /// </summary>
        public static readonly FaultInfo Empty;

        /// <summary>
        /// 逻辑号
        /// </summary>
        [ExcelField("逻辑号")]
        public int LogicNumber { get; set; }

        /// <summary>
        /// 操作提示
        /// </summary>
        [ExcelField("操作提示")]
        public string Trips { get; set; }

        /// <summary>
        /// 故障描述
        /// </summary>
        [ExcelField("故障描述")]
        public string FaultDescribe { get; set; }

        /// <summary>
        /// 故障名称
        /// </summary>
        [ExcelField("故障名称")]
        public string FaultName { get; set; }

        /// <summary>
        /// 故障分类
        /// </summary>
        [ExcelField("故障分类")]
        public FaultType FaultType { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        [ExcelField("等级")]
        public FaultLevel Level { get; set; }

        /// <summary>
        /// 故障代码
        /// </summary>
        [ExcelField("故障代码")]
        public string FaultCode { get; set; }

        /// <summary>
        /// 车
        /// </summary>
        [ExcelField("车")]
        public string Car { get; set; }

        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime HappendTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 确认时间
        /// </summary>

        public DateTime ConfirmTime { get; set; }
        /// <summary>
        /// 是否确认
        /// </summary>

        public bool IsConfirm { get; private set; }

        /// <summary>
        /// 确认
        /// </summary>
        public void Confirm(DateTime date = default(DateTime))
        {
            IsConfirm = true;
            ConfirmTime = date == default(DateTime) ? DateTime.Now : date;
        }

        /// <summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        public FaultInfo Clone()
        {
            var tmp = new FaultInfo();
            tmp.Trips = Trips;
            tmp.FaultDescribe = FaultDescribe;
            tmp.FaultName = FaultName;
            tmp.FaultType = FaultType;
            tmp.FaultCode = FaultCode;
            tmp.Level = Level;
            tmp.LogicNumber = LogicNumber;
            tmp.BtnStr = BtnStr;
            tmp.Car = Car;
            return tmp;
        }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "Level":
                    Level = (FaultLevel)int.Parse(value);
                    break;

                case "FaultType":
                    FaultType = (FaultType)int.Parse(value);
                    break;
            }
        }
    }

    /// <summary>
    /// 故障等级
    /// </summary>
    [Flags]
    public enum FaultLevel
    {
        /// <summary>
        /// 1级故障
        /// </summary>
        Red = 1,

        /// <summary>
        /// 2级故障
        /// </summary>
        Yellow = 2,

        /// <summary>
        /// 事件
        /// </summary>
        Event = 3,

        /// <summary>
        /// 1，2级故障 Red   Yellow
        /// </summary>
        Level12 = 4,

        /// <summary>
        /// 所有故障
        /// </summary>
        All,
    }

    /// <summary>
    /// 故障分类
    /// </summary>
    public enum FaultType
    {
        /// <summary>
        /// EDCUl类型
        /// </summary>
        EDCU = 0,
    }
}