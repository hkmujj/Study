using System.Collections.Generic;
using Excel.Interface;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Models.States;

namespace Subway.WuHanLine6.Models.Units
{
    /// <summary>
    /// 压缩机 冷凝机  通风机
    /// </summary>
    [ExcelLocation("界面接口表.xls", "ChanceUnit")]
    public class ChanceUnit : UnitBase
    {
        private bool m_IsOpen;

        /// <summary>
        /// 类型
        /// </summary>
        [ExcelField("类型")]
        public ChanceType Type { get; private set; }

        /// <summary>
        /// 打开
        /// </summary>
        [ExcelField("打开")]
        public string Open { get; set; }

        /// <summary>
        /// 车
        /// </summary>
        [ExcelField("车")]
        public int Car { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        [ExcelField("位置")]
        public int Location { get; set; }

        /// <summary>
        /// 是否打开  True 打开 False未打开
        /// </summary>
        public bool IsOpen
        {
            get { return m_IsOpen; }
            set
            {
                if (value == m_IsOpen)
                {
                    return;
                }
                m_IsOpen = value;
                RaisePropertyChanged(() => IsOpen);
            }
        }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public override void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "Type":
                    Type = (ChanceType)int.Parse(value);
                    break;
            }
        }

        /// <summary>
        /// Bool值改变
        /// </summary>
        /// <param name="args"></param>
        public override void Changed(IDictionary<int, bool> args)
        {
            args.UpdateIfContain(Open, b => IsOpen = b);
        }
    }
}