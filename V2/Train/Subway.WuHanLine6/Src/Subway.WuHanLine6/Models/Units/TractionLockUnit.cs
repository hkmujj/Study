using System.Collections.Generic;
using Excel.Interface;
using Subway.WuHanLine6.Extention;

namespace Subway.WuHanLine6.Models.Units
{
    /// <summary>
    /// 牵引封锁单元
    /// </summary>
    [ExcelLocation("牵引封锁界面接口.xls", "Sheet1")]
    public class TractionLockUnit : UnitBase
    {
        private bool m_IsLock;

        /// <summary>
        /// 是否封锁
        /// </summary>
        public bool IsLock
        {
            get { return m_IsLock; }
            set
            {
                if (value == m_IsLock)
                {
                    return;
                }
                m_IsLock = value;
                RaisePropertyChanged(() => IsLock);
            }
        }

        /// <summary>
        /// 逻辑名称
        /// </summary>
        [ExcelField("逻辑名称")]
        public string LogicName { get; set; }

        /// <summary>
        /// 显示内容
        /// </summary>
        [ExcelField("显示内容")]
        public string Text { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [ExcelField("行")]
        public int Row { get; set; }

        /// <summary>
        /// 列
        /// </summary>
        [ExcelField("列")]
        public int Columm { get; set; }

        /// <summary>
        /// 行合并
        /// </summary>
        [ExcelField("行合并")]
        public int RowSpan { get; set; }

        /// <summary>
        /// Bool值改变
        /// </summary>
        /// <param name="args"></param>
        public override void Changed(IDictionary<int, bool> args)
        {
            args.UpdateIfContain(LogicName, b => IsLock = b);
        }
    }
}