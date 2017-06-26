using System;
using Engine.TCMS.Turkmenistan.Event;
using Excel.Interface;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TCMS.Turkmenistan.Model.Fault
{
    [ExcelLocation("故障信息.xls", "Sheet1")]
    public class FaultInfo : NotificationObject, ISetValueProvider
    {
        private string m_Message;
        private string m_Car;

        public FaultInfo()
        {
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<ReSourceChangedEvent>()
                .Subscribe(ResouceChanged, ThreadOption.UIThread);
        }

        private void ResouceChanged(string s)
        {
            Message = GlobalParam.Instance.IsTurkmenistan ? MessgaeTm : MessageCh;
            Car = GlobalParam.Instance.IsTurkmenistan ? CarTm : CarCh;
        }

        /// <summary>
        /// 逻辑号
        /// </summary>
        [ExcelField("逻辑号")]
        public int LogicNumber { get; set; }
        /// <summary>
        /// 故障类型
        /// </summary>
        [ExcelField("故障类型")]
        public FaultType Type { get; set; }

        /// <summary>
        /// true  本车  false 他车
        /// </summary>

        public string Car
        {
            get { return m_Car; }
            set
            {
                if (value == m_Car)
                    return;
                m_Car = value;
                RaisePropertyChanged(() => Car);
            }
        }

        /// <summary>
        /// true  本车  false 他车
        /// </summary>
        [ExcelField("本车或者他车中文")]
        public string CarCh { get; set; }
        /// <summary>
        /// true  本车  false 他车
        /// </summary>
        [ExcelField("本车或者他车土库曼斯坦文")]
        public string CarTm { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message
        {
            get { return m_Message; }
            set
            {
                if (value == m_Message)
                    return;
                m_Message = value;
                RaisePropertyChanged(() => Message);
            }
        }

        /// <summary>
        /// 中文故障信息
        /// </summary>
        [ExcelField("故障内容土库曼斯坦文")]
        public string MessgaeTm { get; set; }
        /// <summary>
        /// 英文故障信息
        /// </summary>
        [ExcelField("故障内容中文")]
        public string MessageCh { get; set; }
        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime HappenTime { get; set; }
        /// <summary>
        /// 恢复时间
        /// </summary>
        public DateTime ConfirmTime { get; set; }

        public void SetValue(string propertyOrFieldName, string value)
        {

        }

        public FaultInfo Clone()
        {
            var tmp = new FaultInfo();
            tmp.LogicNumber = this.LogicNumber;
            tmp.Type = this.Type;
            tmp.Car = Car;
            tmp.CarCh = CarCh;
            tmp.CarTm = CarTm;
            tmp.Message = Message;
            tmp.MessageCh = MessageCh;
            tmp.MessgaeTm = MessgaeTm;

            return tmp;
        }

        ~FaultInfo()
        {
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<ReSourceChangedEvent>().Unsubscribe(this.ResouceChanged);
        }

    }

    public enum FaultType
    {

    }
}
