using System;
using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.Infrasturcture.Events;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Model.Msg.Details;
using Tram.CBTC.Infrasturcture.Model.Send;

namespace Tram.CBTC.Infrasturcture.Model.Monitor.Detail
{
    public class SendeMonitor : NotificationObject, ISendInterface
    {

        private readonly ISendInterface m_SendInterface;
        private SendModel<IInformationContent> m_InformationContent;
        private SendModel<double> m_InputtedVolumn;
        private SendModel<double> m_InputtedLight;
        private SendModel<string> m_InputtedDriverId;
        private SendModel<string> m_ConfirmedPassword;
        private SendModel<bool> m_OpenedRadar;
        private SendModel<string> m_InputtedTrainId;
        private SendModel<LineRunDirection> m_InputtedLineRunDirection;

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


        public SendModel<string> ConfirmedPassword
        {
            get { return m_ConfirmedPassword; }
            private set
            {
                if (value == m_ConfirmedPassword)
                    return;

                m_ConfirmedPassword = value;
                RaisePropertyChanged(() => ConfirmedPassword);
            }
        }


        public SendModel<bool> OpenedRadar
        {
            get { return m_OpenedRadar; }
            private set
            {
                if (value == m_OpenedRadar)
                    return;

                m_OpenedRadar = value;
                RaisePropertyChanged(() => OpenedRadar);
            }
        }


        public SendModel<string> InputtedTrainId
        {
            get { return m_InputtedTrainId; }
            private set
            {
                if (value == m_InputtedTrainId)
                    return;

                m_InputtedTrainId = value;
                RaisePropertyChanged(() => InputtedTrainId);
            }
        }


        public SendModel<LineRunDirection> InputtedLineRunDirection
        {
            get { return m_InputtedLineRunDirection; }
            private set
            {
                if (value == m_InputtedLineRunDirection)
                    return;

                m_InputtedLineRunDirection = value;
                RaisePropertyChanged(() => InputtedLineRunDirection);
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

        public bool ConfirmPassword(SendModel<string> password)
        {
            ConfirmedPassword = password;
            return m_SendInterface.ConfirmPassword(password);
        }

        public bool OpenRadar(SendModel<bool> open)
        {
            OpenedRadar = open;
            return m_SendInterface.OpenRadar(open);
        }

        /// <summary>
        /// 输入车次号
        /// </summary>
        /// <param name="driverId"></param>
        /// <returns></returns>
        public bool InputTrainId(SendModel<string> trainId)
        {
            InputtedTrainId = trainId;
            return m_SendInterface.InputTrainId(trainId);
        }

        /// <summary>
        /// 输入线路运行方向
        /// </summary>
        /// <param name="lineRunDirection"></param>
        /// <returns></returns>
        public bool InputLineRunDirection(SendModel<LineRunDirection> lineRunDirection)
        {
            InputtedLineRunDirection = lineRunDirection;
            return m_SendInterface.InputLineRunDirection(lineRunDirection);
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
        /// 打开关闭声音
        /// </summary>
        /// <param name="sound"></param>
        /// <returns></returns>
        public bool OpenSound(SendModel<bool> sound)
        {
            return m_SendInterface.OpenSound(sound);
        }

        /// <summary>
        /// 选择车载联机模式
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SelectVehicleRunningModel(SendModel<VehicleRunningModel> model)
        {
            return m_SendInterface.SelectVehicleRunningModel(model);
        }

        /// <summary>
        /// 发送线路号
        /// </summary>
        /// <param name="lineId">LineID</param>
        /// <returns></returns>
        public bool SendLineID(SendModel<string> lineId)
        {
            return m_SendInterface.SendLineID(lineId);
        }

        /// <summary>
        /// 发送终点站
        /// </summary>
        /// <param name="endStation"></param>
        /// <returns></returns>
        public bool SendEndStation(SendModel<string> endStation)
        {
            return m_SendInterface.SendEndStation(endStation);
        }

        /// <summary>
        /// 发送计划，车次，单程
        /// </summary>
        /// <param name="planinfo"></param>
        /// <returns></returns>
        public bool SendPlanInfo(SendModel<PlanInfo> planinfo)
        {
            return m_SendInterface.SendPlanInfo(planinfo);
        }
    }
}