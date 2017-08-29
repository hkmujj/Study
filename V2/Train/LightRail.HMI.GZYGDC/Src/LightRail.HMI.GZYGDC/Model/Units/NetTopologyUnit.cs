using System.Collections.Generic;
using System.ComponentModel;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using  LightRail.HMI.GZYGDC.Extension;
using  LightRail.HMI.GZYGDC.Interfaces;
using LightRail.HMI.GZYGDC.Service;

namespace LightRail.HMI.GZYGDC.Model.Units
{
    /// <summary>
    /// 网络拓扑单元集合
    /// </summary>
    [ExcelLocation("广州有轨电车车辆屏界面接口表.xls", "NetTopologyUnit")]
    public class NetTopologyUnit : NotificationObject, IUnit, ISetValueProvider
    {
        private NetTopologyState m_State;

        /// <summary>
        /// 
        /// </summary>
        public NetTopologyUnit()
        {
            m_NetTopologyPriorityService = GlobalParam.Instance.InitParam?.DataPackage.ServiceManager.GetService<NetTopologyPriorityService>();
            if (m_NetTopologyPriorityService != null)
            {
                State = m_NetTopologyPriorityService.GetLowPriority();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public NetTopologyState State
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

        private readonly NetTopologyPriorityService m_NetTopologyPriorityService;

        /// <summary>
        /// 车
        /// </summary>
        [ExcelField("车")]
        public int Car { get; set; }
        /// <summary>
        /// 设备
        /// </summary>
        [ExcelField("设备")]
        public string Device { get; set; }
        /// <summary>
        /// 待机
        /// </summary>
        [ExcelField("待机")]
        public string StandBy { get; set; }
        /// <summary>
        /// 正常工作
        /// </summary>
        [ExcelField("正常工作")]
        public string Working { get; set; }
        /// <summary>
        /// 通信异常或未上电
        /// </summary>
        [ExcelField("通信异常或未上电")]
        public string CommunicationException { get; set; }
        /// <summary>
        /// 网关错误
        /// </summary>
        [ExcelField("网关错误")]
        public string GatewayError { get; set; }
        /// <summary>
        /// 网关部分主
        /// </summary>
        [ExcelField("网关部分主")]
        public string GatewayMain { get; set; }
        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        public void DataChanged(IDictionary<int, bool> args)
        {
            args.UpdateIfContain(StandBy, b =>
             {
                 State = m_NetTopologyPriorityService.GetPriorityValue(NetTopologyState.StandBy, b,this);
             });
            args.UpdateIfContain(Working, b =>
            {
                State = m_NetTopologyPriorityService.GetPriorityValue(NetTopologyState.Working, b,this);
            });
            args.UpdateIfContain(CommunicationException, b =>
            {
                State = m_NetTopologyPriorityService.GetPriorityValue(NetTopologyState.CommunicationException, b,this);
            });
            args.UpdateIfContain(GatewayError, b =>
            {
                State = m_NetTopologyPriorityService.GetPriorityValue(NetTopologyState.GatewayError, b,this);
            });
            args.UpdateIfContain(GatewayMain, b =>
            {
                State = m_NetTopologyPriorityService.GetPriorityValue(NetTopologyState.GatewayMain, b, this);
            });
        }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }

    /// <summary>
    /// 网络拓扑单元状态
    /// </summary>
    public enum NetTopologyState
    {
        /// <summary>
        /// 待机
        /// </summary>
        [Description("待机")]
        StandBy,
        /// <summary>
        /// 正常工作
        /// </summary>
        [Description("正常工作")]
        Working,
        /// <summary>
        /// 通信异常或未上电
        /// </summary>
        [Description("通信异常或未上电")]
        CommunicationException,
        /// <summary>
        /// 网关错误
        /// </summary>
        [Description("网关错误")]
        GatewayError,
        /// <summary>
        /// 网关部分主
        /// </summary>ss
        [Description("网关部分主")]
        GatewayMain,
    }
}