using System.Collections.Generic;
using System.ComponentModel;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Subway.ShenZhenLine9.Extention;
using Subway.ShenZhenLine9.Interfaces;
using Subway.ShenZhenLine9.Service;

namespace Subway.ShenZhenLine9.Models.Units
{
    /// <summary>
    /// 
    /// </summary>
    [ExcelLocation("深圳地铁7号线子系统界面接口表.xls", "BrakeUnit")]
    public class BrakeUnit : NotificationObject, ISetValueProvider, IUnit
    {
        private BrakeState m_State;

        /// <summary>
        /// 
        /// </summary>
        public BrakeUnit()
        {
            m_BrakePriorityService = GlobalParam.Instance.InitParam?.DataPackage.ServiceManager.GetService<BrakePriorityService>();
            if (m_BrakePriorityService != null)
            {
                State = m_BrakePriorityService.GetLowPriority();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public BrakeState State
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

        private readonly BrakePriorityService m_BrakePriorityService;
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
        /// 制动切除
        /// </summary>
        [ExcelField("制动切除")]
        public string BrakeCut { get; set; }
        /// <summary>
        /// 制动严重故障
        /// </summary>
        [ExcelField("制动严重故障")]
        public string BrakeFault { get; set; }
        /// <summary>
        /// 常用制动施加
        /// </summary>
        [ExcelField("常用制动施加")]
        public string BrakeUnRemit { get; set; }
        /// <summary>
        /// 常用制动缓解
        /// </summary>
        [ExcelField("常用制动缓解")]
        public string BrakeRemit { get; set; }
        /// <summary>
        /// 状态未知
        /// </summary>
        [ExcelField("状态未知")]
        public string BrakeUnKnow { get; set; }
        /// <summary>
        /// 停放制动施加
        /// </summary>
        [ExcelField("停放制动施加")]
        public string ParkingUnRemit { get; set; }
        /// <summary>
        /// 停放制动缓解
        /// </summary>
        [ExcelField("停放制动缓解")]
        public string ParkingRemit { get; set; }
        /// <summary>
        /// 停放制动缓解
        /// </summary>
        [ExcelField("停放制动切除")]
        public string ParkinCut { get; set; }
        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }

        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        public void DataChanged(IDictionary<int, bool> args)
        {
            args.UpdateIfContain(BrakeCut, b => State = m_BrakePriorityService.GetPriorityValue(this, BrakeState.BrakeCut, b));
            args.UpdateIfContain(BrakeFault, b => State = m_BrakePriorityService.GetPriorityValue(this, BrakeState.BrakeFault, b));
            args.UpdateIfContain(BrakeUnRemit, b => State = m_BrakePriorityService.GetPriorityValue(this, BrakeState.BrakeUnRemit, b));
            args.UpdateIfContain(BrakeRemit, b => State = m_BrakePriorityService.GetPriorityValue(this, BrakeState.BrakeRemit, b));
            args.UpdateIfContain(ParkingUnRemit, b => State = m_BrakePriorityService.GetPriorityValue(this, BrakeState.ParkingUnRemit, b));
            args.UpdateIfContain(ParkingRemit, b => State = m_BrakePriorityService.GetPriorityValue(this, BrakeState.ParkingRemit, b));
            args.UpdateIfContain(BrakeUnKnow, b => State = m_BrakePriorityService.GetPriorityValue(this, BrakeState.BrakeUnKnow, b));
            args.UpdateIfContain(ParkinCut, b => State = m_BrakePriorityService.GetPriorityValue(this, BrakeState.ParkingCut, b));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum BrakeState
    {
        /// <summary>
        /// 默认
        /// </summary>
        [Description()]
        Noaml,
        /// <summary>
        /// 
        /// </summary>
        [Description("制动切除")]
        BrakeCut,
        /// <summary>
        /// 
        /// </summary>
        [Description("制动严重故障")]
        BrakeFault,
        /// <summary>
        /// 
        /// </summary>
        [Description("常用制动施加")]
        BrakeUnRemit,
        /// <summary>
        /// 
        /// </summary>
        [Description("常用制动缓解")]
        BrakeRemit,
        /// <summary>
        /// 
        /// </summary>
        [Description("状态未知")]
        BrakeUnKnow,
        /// <summary>
        /// 
        /// </summary>
        [Description("停放制动施加")]
        ParkingUnRemit,
        /// <summary>
        /// 
        /// </summary>
        [Description("停放制动缓解")]
        ParkingRemit,
        /// <summary>
        /// 
        /// </summary>
        [Description("停放制动切除")]
        ParkingCut,
    }
}