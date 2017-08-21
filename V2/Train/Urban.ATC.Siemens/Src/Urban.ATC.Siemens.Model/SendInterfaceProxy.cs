using System;
using CommonUtil.Util;
using Motor.ATP.Domain.Interface;
using Motor.ATP.Domain.Interface.Infomation;

namespace Motor.ATP.Domain.Model
{
    public class SendInterfaceProxy : ISendInterface
    {
        public ISendInterface RealInterface { set; get; }

        public bool SendTrainId(string trainId)
        {
            LogMgr.Debug("SendSendTrainId({0})", trainId);
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SendTrainId(trainId);
                }
                catch (Exception e)
                {
                    LogMgr.Error(string.Format("Send train id error. {0}", e));
                }
            }
            return false;
        }

        public bool SendDriverId(string driverid)
        {
            LogMgr.Debug("SendDriverId({0})", driverid);
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SendDriverId(driverid);
                }
                catch (Exception e)
                {
                    LogMgr.Error(string.Format("SendDriverId error. {0}", e));
                }
            }
            return false;
        }

        public bool SendTrainData(string traindata)
        {
            LogMgr.Debug("SendTrainData({0})", traindata);
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SendTrainData(traindata);
                }
                catch (Exception e)
                {
                    LogMgr.Error(string.Format("SendTrainData error. {0}", e));
                }
            }
            return false;
        }

        public bool SendDriverData(string trainid, string driverid, string rbcId, string tel)
        {
            LogMgr.Debug("SendDriverData({0},{1},{2},{3})", trainid, driverid, rbcId, tel);
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SendDriverData(trainid, driverid, rbcId, tel);
                }
                catch (Exception e)
                {
                    LogMgr.Error(string.Format("SendDriverData. {0}", e));
                }
            }
            return false;
        }

        public bool SendStart(SendModel<object> startState)
        {
            LogMgr.Debug("SendStart()");
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SendStart(startState);
                }
                catch (Exception e)
                {
                    LogMgr.Error(string.Format("SendStart. {0}", e));
                }
            }
            return false;
        }

        public bool SendRelease(SendModel<object> relaseState)
        {
            LogMgr.Debug("SendRelease()");
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SendRelease(relaseState);
                }
                catch (Exception e)
                {
                    LogMgr.Error(string.Format("SendRelease error. {0}", e));
                }
            }
            return false;
        }

        public bool SendAlert()
        {
            LogMgr.Debug("SendAlert()");
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SendAlert();
                }
                catch (Exception e)
                {
                    LogMgr.Error(string.Format("SendAlert error. {0}", e));
                }
            }
            return false;
        }

        public bool SendRBCData(SendModel<RBCDataModel> rbcData)
        {
            LogMgr.Debug("SendRBCData(RBCDataModel rbcData)");
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SendRBCData(rbcData);
                }
                catch (Exception e)
                {
                    LogMgr.Error(string.Format("SendRBCData error. {0}", e));
                }
            }
            return false;
        }

        public bool SendBreakTest()
        {
            LogMgr.Debug("SendBreakTest()");
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SendBreakTest();
                }
                catch (Exception e)
                {
                    LogMgr.Error(string.Format("SendBreakTest error. {0}", e));
                }
            }
            return false;
        }

        public bool SelectFreq(SendModel<TrainFreq> trainFreq)
        {
            LogMgr.Debug("SelectFreq({0})", trainFreq);
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SelectFreq(trainFreq);
                }
                catch (Exception e)
                {
                    LogMgr.Error(string.Format("SelectFreq error. {0}", e));
                }
            }
            return false;
        }

        public bool SelectCtcs(SendModel<CTCSType> ctcsType)
        {
            LogMgr.Debug("SelectCtcs({0})", ctcsType);
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SelectCtcs(ctcsType);
                }
                catch (Exception e)
                {
                    LogMgr.Error(string.Format("SelectCtcs error. {0}", e));
                }
            }
            return false;
        }

        public bool SelectControlMode(SendModel<ControlType> controlType)
        {
            LogMgr.Debug("SelectControlMode({0})", controlType);
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SelectControlMode(controlType);
                }
                catch (Exception e)
                {
                    LogMgr.Error(string.Format("SelectControlMode error. {0}", e));
                }
            }
            return false;
        }

        public bool SendTrainDataConfirm()
        {
            LogMgr.Debug("SendTrainDataConfirm()");
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SendTrainDataConfirm();
                }
                catch (Exception e)
                {
                    LogMgr.Error(string.Format("SendTrainDataConfirm error. {0}", e));
                }
            }
            return false;
        }

        public bool SendFreqConfirm()
        {
            LogMgr.Debug("SendFreqConfirm()");
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.SendFreqConfirm();
                }
                catch (Exception e)
                {
                    LogMgr.Error(string.Format("SendFreqConfirm error. {0}", e));
                }
            }
            return false;
        }

        public bool EnsureMessage(SendModel<IInfomationItem> message)
        {
            LogMgr.Debug("EnsureMessage({0})", message.Content.Content.Id);
            if (RealInterface != null)
            {
                try
                {
                    return RealInterface.EnsureMessage(message);
                }
                catch (Exception e)
                {
                    LogMgr.Error(string.Format("EnsureMessage error. {0}", e));
                }
            }
            return false;
        }
    }
}