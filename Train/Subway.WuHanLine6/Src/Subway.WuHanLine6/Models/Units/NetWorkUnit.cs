using System.Collections.Generic;
using Excel.Interface;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Models.States;

namespace Subway.WuHanLine6.Models.Units
{
    /// <summary>
    ///     网络拓扑单元
    /// </summary>
    [ExcelLocation("网络拓扑界面接口.xls", "Sheet1")]
    public class NetWorkUnit : UnitBase
    {
        private NetWorkState m_State;

        /// <summary>
        ///     工作正常
        /// </summary>
        [ExcelField("工作正常")]
        public string Green { get; set; }

        /// <summary>
        ///     待机中
        /// </summary>
        [ExcelField("待机中")]
        public string Yellow { get; set; }

        /// <summary>
        ///     通信异常或未上电
        /// </summary>
        [ExcelField("通信异常或未上电")]
        public string White { get; set; }

        /// <summary>
        ///     门网关部分主
        /// </summary>
        [ExcelField("门网关部分主")]
        public string Pink { get; set; }

        /// <summary>
        ///     门网关错误
        /// </summary>
        [ExcelField("门网关错误")]
        public string Red { get; set; }

        /// <summary>
        ///     编号
        /// </summary>
        [ExcelField("编号")]
        public int Number { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        public NetWorkState State
        {
            get { return m_State; }
            set
            {
                if (value == m_State)
                {
                    return;
                }
                m_State = value;
                RaisePropertyChanged(() => State);
            }
        }

        /// <summary>
        ///     Bool值改变
        /// </summary>
        /// <param name="args"></param>
        public override void Changed(IDictionary<int, bool> args)
        {
            args.UpdateIfContainWhereTrue(Green, b => State = NetWorkState.Green);
            args.UpdateIfContainWhereTrue(Yellow, b => State = NetWorkState.Yellow);
            args.UpdateIfContainWhereTrue(White, b => State = NetWorkState.White);
            args.UpdateIfContainWhereTrue(Pink, b => State = NetWorkState.Pink);
            args.UpdateIfContainWhereTrue(Red, b => State = NetWorkState.Red);
        }
    }
}