using System;
using System.Collections.ObjectModel;
using CommonUtil.Controls;
using CommonUtil.Util;
using Microsoft.Practices.Prism.ViewModel;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.BrakeTest;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP.Infrasturcture.Model
{
    public class SendInterfaceProxy : NotificationObject, ISendInterface
    {
        public ISendInterface RealInterface { set; get; }

        /// <summary>
        /// 制动测试确认
        /// </summary>
        /// <param name="brakeTest"></param>
        /// <returns></returns>
        public virtual bool EnsurceBrakeTestSelect(SendModel<BrakeTestModel> brakeTest)
        {
            AppLog.Debug("EnsurceBrakeTestSelect({0})", brakeTest.Content);
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.EnsurceBrakeTestSelect(brakeTest);
                }
                catch (Exception e)
                {
                    AppLog.Error(string.Format("Ensure brake test selectstate error. {0}", e));
                }
            }

            return false;
        }

        /// <summary>
        /// 制动测试选择
        /// </summary>
        /// <param name="brakeTest"></param>
        /// <returns></returns>
        public virtual bool SendBrakeTestSelect(SendModel<BrakeTestModel> brakeTest)
        {
            AppLog.Debug("SendBrakeTestSelect({0})", brakeTest.Content);
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SendBrakeTestSelect(brakeTest);
                }
                catch (Exception e)
                {
                    AppLog.Error(string.Format("Send brake test selectstate error. {0}", e));
                }
            }

            return false;
        }

        public virtual bool SendAssistScreenTest(SendModel<AssistScreenTestModel> assistScreenTestModel)
        {
            AppLog.Debug("SendAssistScreenTest({0})", assistScreenTestModel.Content);
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SendAssistScreenTest(assistScreenTestModel);
                }
                catch (Exception e)
                {
                    AppLog.Error(string.Format("Send brake test selectstate error. {0}", e));
                }
            }

            return false;
        }


        /// <summary>
        /// 发送司机号和车次号
        /// </summary>
        /// <param name="driverid"></param>
        /// <returns></returns>
        public virtual bool SendDriverData(SendModel<DriverDataModel> driverid)
        {
            AppLog.Debug("SendDriverId({0})", driverid);
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SendDriverData(driverid);
                }
                catch (Exception e)
                {
                    AppLog.Error(string.Format("SendDriverId error. {0}", e));
                }
            }
            return false;
        }

        /// <summary>
        /// 发送车次号
        /// </summary>
        /// <param name="trainId"></param>
        /// <returns></returns>
        public virtual bool SendTrainId(SendModel<DriverDataModel> trainId)
        {
            AppLog.Debug("SendSendTrainId({0})", trainId);
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SendTrainId(trainId);
                }
                catch (Exception e)
                {
                    AppLog.Error(string.Format("Send train id error. {0}", e));
                }
            }

            return false;
        }

        public virtual bool SendDriverId(SendModel<DriverDataModel> driverid)
        {
            AppLog.Debug("SendDriverId({0})", driverid);
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SendDriverId(driverid);
                }
                catch (Exception e)
                {
                    AppLog.Error(string.Format("SendDriverId error. {0}", e));
                }
            }

            return false;
        }

        public virtual bool SendTrainData(SendModel<ReadOnlyCollection<string>> traindata)
        {
            AppLog.Debug("SendTrainData({0})", traindata.Content != null ? string.Join(",", traindata.Content) : "null");
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SendTrainData(traindata);
                }
                catch (Exception e)
                {
                    AppLog.Error(string.Format("SendTrainData error. {0}", e));
                }
            }

            return false;
        }


        public virtual bool SendStart(SendModel<object> startState)
        {
            AppLog.Debug("SendStart()");
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SendStart(startState);
                }
                catch (Exception e)
                {
                    AppLog.Error(string.Format("SendStart. {0}", e));
                }
            }

            return false;
        }

        public virtual bool SendRelease(SendModel<object> relaseState)
        {
            AppLog.Debug("SendRelease()");
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SendRelease(relaseState);
                }
                catch (Exception e)
                {
                    AppLog.Error(string.Format("SendRelease error. {0}", e));
                }
            }

            return false;
        }

        public virtual bool SendAlert()
        {
            AppLog.Debug("SendAlert()");
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SendAlert();
                }
                catch (Exception e)
                {
                    AppLog.Error(string.Format("SendAlert error. {0}", e));
                }
            }

            return false;
        }

        public virtual bool SendRBCData(SendModel<RBCDataModel> rbcData)
        {
            AppLog.Debug("SendRBCData(RBCDataModel rbcData)");
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SendRBCData(rbcData);
                }
                catch (Exception e)
                {
                    AppLog.Error(string.Format("SendRBCData error. {0}", e));
                }
            }

            return false;
        }

        public virtual bool SelectFreq(SendModel<TrainFreq> trainFreq)
        {
            AppLog.Debug("SelectFreq({0})", trainFreq);
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SelectFreq(trainFreq);
                }
                catch (Exception e)
                {
                    AppLog.Error(string.Format("SelectFreq error. {0}", e));
                }
            }

            return false;
        }

        public virtual bool SelectCtcs(SendModel<CTCSType> ctcsType)
        {
            AppLog.Debug("SelectCtcs({0})", ctcsType);
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SelectCtcs(ctcsType);
                }
                catch (Exception e)
                {
                    AppLog.Error(string.Format("SelectCtcs error. {0}", e));
                }
            }

            return false;
        }

        public virtual bool SelectControlMode(SendModel<ControlType> controlType)
        {
            AppLog.Debug("SelectControlMode({0})", controlType);
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SelectControlMode(controlType);
                }
                catch (Exception e)
                {
                    AppLog.Error(string.Format("SelectControlMode error. {0}", e));
                }
            }

            return false;
        }

        public virtual bool SendTrainDataConfirm()
        {
            AppLog.Debug("SendTrainDataConfirm()");
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SendTrainDataConfirm();
                }
                catch (Exception e)
                {
                    AppLog.Error(string.Format("SendTrainDataConfirm error. {0}", e));
                }
            }

            return false;
        }

        public virtual bool SendFreqConfirm()
        {
            AppLog.Debug("SendFreqConfirm()");
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SendFreqConfirm();
                }
                catch (Exception e)
                {
                    AppLog.Error(string.Format("SendFreqConfirm error. {0}", e));
                }
            }

            return false;
        }

        public virtual bool EnsureMessage(SendModel<IInfomationItem> message)
        {
            AppLog.Debug("EnsureMessage({0})", message.Content.Content.Id);
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.EnsureMessage(message);
                }
                catch (Exception e)
                {
                    AppLog.Error(string.Format("EnsureMessage error. {0}", e));
                }
            }

            return false;
        }

        /// <summary>
        /// 通知消息计时变化 
        /// </summary>
        /// <param name="messageItem"></param>
        /// <returns></returns>
        public virtual bool OnMessageTimeChanged(IMessageItem messageItem)
        {
            AppLog.Debug("NotifyMessageTimeChanged({0})", messageItem.Content);
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.OnMessageTimeChanged(messageItem);
                }
                catch (Exception e)
                {
                    AppLog.Error(string.Format("NotifyMessageTimeChanged error. {0}", e));
                }
            }

            return false;
        }

        /// <summary>
        /// 响应用户动作，目前只有root界面 B10按键触发
        ///     用于快速跳过启动界面等特殊需求
        /// </summary>
        /// <param name="actionType"></param>
        /// <param name="mouseState"></param>
        /// <returns></returns>
        public virtual bool OnUserAction(UserActionType actionType, MouseState mouseState)
        {
            AppLog.Debug("OnUserAction({0},{1})", actionType, mouseState);
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.OnUserAction(actionType, mouseState);
                }
                catch (Exception e)
                {
                    AppLog.Error(string.Format("OnUserAction error. {0}", e));
                }
            }

            return false;
        }

        /// <summary>
        /// DMI 测试
        /// </summary>
        /// <param name="dmiModel"></param>
        /// <returns></returns>
        public virtual bool SendDMITest(SendModel<object> dmiModel)
        {
            AppLog.Debug("SendDMITest()");
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SendDMITest(dmiModel);
                }
                catch (Exception e)
                {
                    AppLog.Error(string.Format("SendDMITest error. {0}", e));
                }
            }

            return false;
        }
    }
}