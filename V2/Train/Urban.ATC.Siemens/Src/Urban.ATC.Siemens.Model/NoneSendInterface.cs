using Motor.ATP.Domain.Interface;
using Motor.ATP.Domain.Interface.Infomation;

namespace Motor.ATP.Domain.Model
{
    public class NoneSendInterface : ISendInterface
    {
        public bool SendTrainId(string trainId)
        {
            return false;
        }

        public bool SendDriverId(string driverid)
        {
            return false;
        }

        public bool SendTrainData(string traindata)
        {
            return false;
        }

        public bool SendDriverData(string trainid, string driverid, string rbcId, string tel)
        {
            return false;
        }

        public bool SendStart(SendModel<object> startState)
        {
            return false;
        }

        public bool SendRelease(SendModel<object> relaseState)
        {
            return false;
        }

        public bool SendAlert()
        {
            return false;
        }

        public bool SendRBCData(SendModel<RBCDataModel> rbcData)
        {
            return false;
        }

        public bool SendBreakTest()
        {
            return false;
        }

        public bool SelectFreq(SendModel<TrainFreq> trainFreq)
        {
            return false;
        }

        public bool SelectCtcs(SendModel<CTCSType> ctcsType)
        {
            return false;
        }

        public bool SelectControlMode(SendModel<ControlType> controlType)
        {
            return false;
        }

        public bool SendTrainDataConfirm()
        {
            return false;
        }

        public bool SendFreqConfirm()
        {
            return false;
        }

        public bool EnsureMessage(SendModel<IInfomationItem> message)
        {
            return false;
        }
    }
}