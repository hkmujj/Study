using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.BrakeTest;
using Motor.ATP.Infrasturcture.Interface.Infomation;

namespace Motor.ATP.Infrasturcture.Model
{
    public class SendInterfaceCounterableProxy : SendInterfaceProxy
    {
        private CountModel m_SendDMITestCount;
        private CountModel m_EnsureMessageCount;
        private CountModel m_SendFreqConfirmCount;
        private CountModel m_SendTrainDataConfirmCount;
        private CountModel m_SelectControlModeCount;
        private CountModel m_SelectCtcsCount;
        private CountModel m_SelectFreqCount;
        private CountModel m_SendRBCDataCount;
        private CountModel m_SendAlertCount;
        private CountModel m_SendReleaseCount;
        private CountModel m_SendStartCount;
        private CountModel m_SendDriverDataCount;
        private CountModel m_SendTrainDataCount;
        private CountModel m_SendDriverIdCount;
        private CountModel m_SendTrainIdCount;
        private CountModel m_SendAssistScreenTestCount;
        private CountModel m_SendBrakeTestSelectCount;
        private CountModel m_EnsurceBrakeTestSelectCount;

        [DebuggerDisplay("Count={Count}")]
        public class CountModel : NotificationObject
        {
            private object m_LastSendArgs;
            private ulong m_Count;

            public ulong Count
            {
                get { return m_Count; }
                internal set
                {
                    if (value == m_Count)
                    {
                        return;
                    }

                    m_Count = value;
                    RaisePropertyChanged(() => Count);
                }
            }

            public object LastSendArgs
            {
                get { return m_LastSendArgs; }
                internal set
                {
                    if (Equals(value, m_LastSendArgs))
                    {
                        return;
                    }

                    m_LastSendArgs = value;
                    RaisePropertyChanged(() => LastSendArgs);
                }
            }
        }

        public CountModel EnsurceBrakeTestSelectCount
        {
            get { return m_EnsurceBrakeTestSelectCount; }
            private set
            {
                if (Equals(value, m_EnsurceBrakeTestSelectCount))
                {
                    return;
                }

                m_EnsurceBrakeTestSelectCount = value;
                RaisePropertyChanged(() => EnsurceBrakeTestSelectCount);
            }
        }

        public CountModel SendBrakeTestSelectCount
        {
            get { return m_SendBrakeTestSelectCount; }
            private set
            {
                if (Equals(value, m_SendBrakeTestSelectCount))
                {
                    return;
                }

                m_SendBrakeTestSelectCount = value;
                RaisePropertyChanged(() => SendBrakeTestSelectCount);
            }
        }

        public CountModel SendAssistScreenTestCount
        {
            get { return m_SendAssistScreenTestCount; }
            private set
            {
                if (Equals(value, m_SendAssistScreenTestCount))
                {
                    return;
                }

                m_SendAssistScreenTestCount = value;
                RaisePropertyChanged(() => SendAssistScreenTestCount);
            }
        }

        public CountModel SendTrainIdCount
        {
            get { return m_SendTrainIdCount; }
            private set
            {
                if (Equals(value, m_SendTrainIdCount))
                {
                    return;
                }

                m_SendTrainIdCount = value;
                RaisePropertyChanged(() => SendTrainIdCount);
            }
        }

        public CountModel SendDriverIdCount
        {
            get { return m_SendDriverIdCount; }
            private set
            {
                if (Equals(value, m_SendDriverIdCount))
                {
                    return;
                }

                m_SendDriverIdCount = value;
                RaisePropertyChanged(() => SendDriverIdCount);
            }
        }

        public CountModel SendTrainDataCount
        {
            get { return m_SendTrainDataCount; }
            private set
            {
                if (Equals(value, m_SendTrainDataCount))
                {
                    return;
                }

                m_SendTrainDataCount = value;
                RaisePropertyChanged(() => SendTrainDataCount);
            }
        }

        public CountModel SendDriverDataCount
        {
            get { return m_SendDriverDataCount; }
            private set
            {
                if (Equals(value, m_SendDriverDataCount))
                {
                    return;
                }

                m_SendDriverDataCount = value;
                RaisePropertyChanged(() => SendDriverDataCount);
            }
        }

        public CountModel SendStartCount
        {
            get { return m_SendStartCount; }
            private set
            {
                if (Equals(value, m_SendStartCount))
                {
                    return;
                }

                m_SendStartCount = value;
                RaisePropertyChanged(() => SendStartCount);
            }
        }

        public CountModel SendReleaseCount
        {
            get { return m_SendReleaseCount; }
            private set
            {
                if (Equals(value, m_SendReleaseCount))
                {
                    return;
                }

                m_SendReleaseCount = value;
                RaisePropertyChanged(() => SendReleaseCount);
            }
        }

        public CountModel SendAlertCount
        {
            get { return m_SendAlertCount; }
            private set
            {
                if (Equals(value, m_SendAlertCount))
                {
                    return;
                }

                m_SendAlertCount = value;
                RaisePropertyChanged(() => SendAlertCount);
            }
        }

        public CountModel SendRBCDataCount
        {
            get { return m_SendRBCDataCount; }
            private set
            {
                if (Equals(value, m_SendRBCDataCount))
                {
                    return;
                }

                m_SendRBCDataCount = value;
                RaisePropertyChanged(() => SendRBCDataCount);
            }
        }

        public CountModel SelectFreqCount
        {
            get { return m_SelectFreqCount; }
            private set
            {
                if (Equals(value, m_SelectFreqCount))
                {
                    return;
                }

                m_SelectFreqCount = value;
                RaisePropertyChanged(() => SelectFreqCount);
            }
        }

        public CountModel SelectCtcsCount
        {
            get { return m_SelectCtcsCount; }
            private set
            {
                if (Equals(value, m_SelectCtcsCount))
                {
                    return;
                }

                m_SelectCtcsCount = value;
                RaisePropertyChanged(() => SelectCtcsCount);
            }
        }

        public CountModel SelectControlModeCount
        {
            get { return m_SelectControlModeCount; }
            private set
            {
                if (Equals(value, m_SelectControlModeCount))
                {
                    return;
                }

                m_SelectControlModeCount = value;
                RaisePropertyChanged(() => SelectControlModeCount);
            }
        }

        public CountModel SendTrainDataConfirmCount
        {
            get { return m_SendTrainDataConfirmCount; }
            private set
            {
                if (Equals(value, m_SendTrainDataConfirmCount))
                {
                    return;
                }

                m_SendTrainDataConfirmCount = value;
                RaisePropertyChanged(() => SendTrainDataConfirmCount);
            }
        }

        public CountModel SendFreqConfirmCount
        {
            get { return m_SendFreqConfirmCount; }
            private set
            {
                if (Equals(value, m_SendFreqConfirmCount))
                {
                    return;
                }

                m_SendFreqConfirmCount = value;
                RaisePropertyChanged(() => SendFreqConfirmCount);
            }
        }

        public CountModel EnsureMessageCount
        {
            get { return m_EnsureMessageCount; }
            private set
            {
                if (Equals(value, m_EnsureMessageCount))
                {
                    return;
                }

                m_EnsureMessageCount = value;
                RaisePropertyChanged(() => EnsureMessageCount);
            }
        }

        public CountModel SendDMITestCount
        {
            get { return m_SendDMITestCount; }
            private set
            {
                if (Equals(value, m_SendDMITestCount))
                {
                    return;
                }

                m_SendDMITestCount = value;
                RaisePropertyChanged(() => SendDMITestCount);
            }
        }

        public SendInterfaceCounterableProxy()
        {
            EnsurceBrakeTestSelectCount = new CountModel();
            SendBrakeTestSelectCount = new CountModel();
            SendAssistScreenTestCount = new CountModel();
            SendTrainIdCount = new CountModel();
            SendDriverIdCount = new CountModel();
            SendTrainDataCount = new CountModel();
            SendTrainDataCount = new CountModel();
            SendDriverDataCount = new CountModel();
            SendStartCount = new CountModel();
            SendReleaseCount = new CountModel();
            SendAlertCount = new CountModel();
            SendRBCDataCount = new CountModel();
            SelectFreqCount = new CountModel();
            SelectCtcsCount = new CountModel();
            SelectControlModeCount = new CountModel();
            SendFreqConfirmCount = new CountModel();
            EnsureMessageCount = new CountModel();
            SendDMITestCount = new CountModel();
            SendTrainDataConfirmCount = new CountModel();
        }

        /// <summary>警惕</summary>
        /// <returns></returns>
        public override bool SendAlert()
        {
            ++SendAlertCount.Count;
            return base.SendAlert();
        }

        /// <summary>
        /// 制动测试确认
        /// </summary>
        /// <param name="brakeTest"></param>
        /// <returns></returns>
        public override bool EnsurceBrakeTestSelect(SendModel<BrakeTestModel> brakeTest)
        {
            ++EnsurceBrakeTestSelectCount.Count;
            EnsurceBrakeTestSelectCount.LastSendArgs = brakeTest;
            return base.EnsurceBrakeTestSelect(brakeTest);
        }

        /// <summary>
        /// 制动测试选择
        /// </summary>
        /// <param name="brakeTest"></param>
        /// <returns></returns>
        public override bool SendBrakeTestSelect(SendModel<BrakeTestModel> brakeTest)
        {
            ++SendBrakeTestSelectCount.Count;
            SendBrakeTestSelectCount.LastSendArgs = brakeTest;
            return base.SendBrakeTestSelect(brakeTest);
        }

        /// <summary>辅屏测试</summary>
        /// <param name="assistScreenTestModel"></param>
        /// <returns></returns>
        public override bool SendAssistScreenTest(SendModel<AssistScreenTestModel> assistScreenTestModel)
        {
            ++SendAssistScreenTestCount.Count;
            SendAssistScreenTestCount.LastSendArgs = assistScreenTestModel;
            return base.SendAssistScreenTest(assistScreenTestModel);
        }

        /// <summary>
        /// 发送车次号
        /// </summary>
        /// <param name="trainId"></param>
        /// <returns></returns>
        public override bool SendTrainId(SendModel<DriverDataModel> trainId)
        {
            ++SendTrainIdCount.Count;
            SendTrainIdCount.LastSendArgs = trainId;
            return base.SendTrainId(trainId);
        }

        /// <summary>发送司机号</summary>
        /// <param name="driverid"></param>
        /// <returns></returns>
        public override bool SendDriverId(SendModel<DriverDataModel> driverid)
        {
            ++SendDriverIdCount.Count;
            SendDriverIdCount.LastSendArgs = driverid;
            return base.SendDriverId(driverid);
        }

        /// <summary>
        /// 发送司机号和车次号
        /// </summary>
        /// <param name="trainData"></param>
        /// <returns></returns>
        public override bool SendDriverData(SendModel<DriverDataModel> trainData)
        {
            ++SendDriverDataCount.Count;
            SendDriverIdCount.LastSendArgs = trainData;
            return base.SendDriverData(trainData);
        }

        /// <summary>发送列车数据</summary>
        /// <param name="traindata"></param>
        /// <returns></returns>
        public override bool SendTrainData(SendModel<ReadOnlyCollection<string>> traindata)
        {
            ++SendTrainDataCount.Count;
            SendTrainDataCount.LastSendArgs = traindata;
            return base.SendTrainData(traindata);
        }


        /// <summary>发送启动</summary>
        /// <param name="startState"></param>
        /// <returns></returns>
        public override bool SendStart(SendModel<object> startState)
        {
            ++SendStartCount.Count;
            SendStartCount.LastSendArgs = startState;
            return base.SendStart(startState);
        }

        /// <summary>缓解</summary>
        /// <param name="relaseState"></param>
        /// <returns></returns>
        public override bool SendRelease(SendModel<object> relaseState)
        {
            ++SendReleaseCount.Count;
            SendReleaseCount.LastSendArgs = relaseState;
            return base.SendRelease(relaseState);
        }

        /// <summary>RBC 数据（包括RBCid和电话号码等）</summary>
        /// <param name="rbcData"></param>
        /// <returns></returns>
        public override bool SendRBCData(SendModel<RBCDataModel> rbcData)
        {
            ++SendRBCDataCount.Count;
            SendRBCDataCount.LastSendArgs = rbcData;
            return base.SendRBCData(rbcData);
        }

        /// <summary>选择载频</summary>
        /// <param name="trainFreq"></param>
        /// <returns></returns>
        public override bool SelectFreq(SendModel<TrainFreq> trainFreq)
        {
            ++SelectFreqCount.Count;
            SelectFreqCount.LastSendArgs = trainFreq;
            return base.SelectFreq(trainFreq);
        }

        /// <summary>选择CTCS</summary>
        /// <param name="ctcsType"></param>
        /// <returns></returns>
        public override bool SelectCtcs(SendModel<CTCSType> ctcsType)
        {
            ++SelectCtcsCount.Count;
            SelectCtcsCount.LastSendArgs = ctcsType;
            return base.SelectCtcs(ctcsType);
        }

        /// <summary>选择控制模式</summary>
        /// <param name="controlType"></param>
        /// <returns></returns>
        public override bool SelectControlMode(SendModel<ControlType> controlType)
        {
            ++SelectControlModeCount.Count;
            SelectControlModeCount.LastSendArgs = controlType;
            return base.SelectControlMode(controlType);
        }

        /// <summary>发送 确认列车数据</summary>
        /// <returns></returns>
        public override bool SendTrainDataConfirm()
        {
            ++SendTrainDataConfirmCount.Count;
            return base.SendTrainDataConfirm();
        }

        /// <summary>发送 确认载频</summary>
        /// <returns></returns>
        public override bool SendFreqConfirm()
        {
            ++SendFreqConfirmCount.Count;
            return base.SendFreqConfirm();
        }

        /// <summary>确认一个消息</summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public override bool EnsureMessage(SendModel<IInfomationItem> message)
        {
            ++EnsureMessageCount.Count;
            EnsureMessageCount.LastSendArgs = message;
            return base.EnsureMessage(message);
        }

        /// <summary>
        /// DMI 测试
        /// </summary>
        /// <param name="dmiModel"></param>
        /// <returns></returns>
        public override bool SendDMITest(SendModel<object> dmiModel)
        {
            ++SendDMITestCount.Count;
            SendDMITestCount.LastSendArgs = dmiModel;
            return base.SendDMITest(dmiModel);
        }

    }
}