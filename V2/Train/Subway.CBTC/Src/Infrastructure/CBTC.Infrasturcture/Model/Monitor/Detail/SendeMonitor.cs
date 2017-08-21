using CBTC.Infrasturcture.Model.Msg.Details;
using CBTC.Infrasturcture.Model.Send;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Win32.SafeHandles;

namespace CBTC.Infrasturcture.Model.Monitor.Detail
{
    public class SendeMonitor : NotificationObject, ISendInterface
    {

        private readonly ISendInterface m_SendInterface;
        private SendModel<IInformationContent> m_InformationContent;
        private SendModel<double> m_InputtedVolumn;
        private SendModel<double> m_InputtedLight;
        private SendModel<string> m_InputtedDriverId;

        public SendeMonitor(ISendInterface sendInterface)
        {
            m_SendInterface = sendInterface;
            if (m_SendInterface == null)
            {
                m_SendInterface = new EmptySendInterface();
            }
        }

        public SendModel<IInformationContent> InformationContent
        {
            get { return m_InformationContent; }
            private set
            {
                if (Equals(value, m_InformationContent))
                    return;

                m_InformationContent = value;
                RaisePropertyChanged(() => InformationContent);
            }
        }

        public SendModel<string> InputtedDriverId
        {
            get { return m_InputtedDriverId; }
            private set
            {
                if (value == m_InputtedDriverId)
                    return;

                m_InputtedDriverId = value;
                RaisePropertyChanged(() => InputtedDriverId);
            }
        }

        public SendModel<double> InputtedLight
        {
            get { return m_InputtedLight; }
            private set
            {
                if (value.Equals(m_InputtedLight))
                    return;

                m_InputtedLight = value;
                RaisePropertyChanged(() => InputtedLight);
            }
        }

        public SendModel<double> InputtedVolumn
        {
            get { return m_InputtedVolumn; }
            private set
            {
                if (value.Equals(m_InputtedVolumn))
                    return;

                m_InputtedVolumn = value;
                RaisePropertyChanged(() => InputtedVolumn);
            }
        }

        /// <summary>
        /// 确认一个消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool EnsureMessage(SendModel<IInformationContent> message)
        {
            InformationContent = message;
            return m_SendInterface.EnsureMessage(message);
        }

        /// <summary>
        /// 输入司机号
        /// </summary>
        /// <param name="driverId"></param>
        /// <returns></returns>
        public bool InputDriverId(SendModel<string> driverId)
        {
            InputtedDriverId = driverId;
            return m_SendInterface.InputDriverId(driverId);
        }

        /// <summary>
        /// 输入亮度
        /// </summary>
        /// <param name="light"></param>
        /// <returns></returns>
        public bool InputLight(SendModel<double> light)
        {
            InputtedLight = light;
            return m_SendInterface.InputLight(light);
        }

        /// <summary>
        /// 输入音量
        /// </summary>
        /// <param name="volumn"></param>
        /// <returns></returns>
        public bool InputVolumn(SendModel<double> volumn)
        {
            InputtedVolumn = volumn;
            return m_SendInterface.InputVolumn(volumn);
        }


        ///<summary>
        /// 输入司机号数字
        /// </summary>
        /// <param name="driveridnum"></param>
        /// <returns></returns>
        public virtual bool InputDriverIDNum(SendModel<float> driveridnum)
        {
            return false;
        }

        ///<summary>
        /// 输入司机号数字
        /// </summary>
        /// <param name="trainidnum"></param>
        /// <returns></returns>
        public virtual bool InputTrainIDNum(SendModel<float> trainidnum)
        {
            return false;
        }
        ///<summary>
        /// 制动测试试闸
        /// </summary>
        ///<param name = "braketestswitch">
        /// <returen></returen>
        /// </param>
        public virtual bool InputBrakeTestSwitch(SendModel<bool> braketestswitch)
        {
            return false;
        }

        ///<summary>
        /// 制动测试缓解
        /// </summary>
        ///<param name = "braketestrelease">
        /// <returen></returen>
        /// </param>
        public virtual bool InputBrakeTestRelease(SendModel<bool> braketestrelease)
        {
            return false;
        }

        ///<summary>
        /// 广播测试
        /// </summary>
        ///<param name = "broadcasttest">
        /// <returen></returen>
        /// </param>
        public virtual bool InputBroadcastTest(SendModel<bool> broadcasttest)
        {
            return false;
        }

        ///<summary>
        /// 灯测试
        /// </summary>
        ///<param name = "lamptest">
        /// <returen></returen>
        /// </param>
        public virtual bool InputLampTest(SendModel<bool> lamptest)
        {
            return false;
        }

        /// <summary>
        /// 发送后备模式是否强制
        /// </summary>
        /// <param name="bm">true 强制 false 非强制</param>
        /// <returns>true 发送成功  false  发送失败</returns>
        public bool InputBM(SendModel<bool> bm)
        {
            m_SendInterface.InputBM(bm);
            return false;
        }
    }
}