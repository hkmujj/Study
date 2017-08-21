using System.Collections.Generic;
using Excel.Interface;
using LightRail.HMI.SZLHLF.Extension;
using LightRail.HMI.SZLHLF.Interfaces;
using LightRail.HMI.SZLHLF.Model.NetWorkModel;
using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.SZLHLF.Model.Units
{
    /// <summary>
    ///     网络拓扑单元
    /// </summary>
    [ExcelLocation("网络拓扑界面接口.xls", "Sheet1")]
    public class NetWorkUnit : NotificationObject, ISetValueProvider, IUnit
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
        public string Gray { get; set; }

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

        /// <summary />
        /// <param name="propertyOrFieldName" />
        /// <param name="value" />
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
        
        public void DataChanged(IDictionary<int, bool> args)
        {
            args.UpdateIfContain(Green, b => State = NetWorkState.Green);
            args.UpdateIfContain(White, b => State = NetWorkState.White);
            args.UpdateIfContain(Pink, b => State = NetWorkState.Pink);
            args.UpdateIfContain(Red, b => State = NetWorkState.Red);
            args.UpdateIfContain(Gray, b => State = NetWorkState.Gray);
        }
    }
}