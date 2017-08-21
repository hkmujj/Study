using System.Collections.ObjectModel;
using Motor.ATP.DataAdapter.Extension;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.BrakeTest;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP.DataAdapter.ConcreateAdapter.ATP200H
{
    public class SendInterface200H : SendInterfaceBase<SignalDataOut200H>
    {
        private SignalDataOut200H signalDataOut200H;

        public SendInterface200H(SignalDataOut200H signalDataOut200H)
            : base(signalDataOut200H)
        {
            // TODO: Complete member initialization
            this.signalDataOut200H = signalDataOut200H;
        }

   

        //制动测试选择：200C
        public override bool SendBrakeTestSelect(SendModel<BrakeTestModel> brakeTest)
        { 
           
            return false;
        }
         //DMI测试
        public override  bool SendAssistScreenTest(SendModel<AssistScreenTestModel> assistScreenTestModel)
        {
            return false;
        }
        //辅屏按钮
        public override bool AssistSwich(SendModel<bool> asssistModel)
        {
            if (asssistModel.Type == SendModelType.Ok)
            {
                DataOut.ScreenSwitchFlag = true;
            }
            else
            {
                DataOut.ScreenSwitchFlag = false;
            }
                
            return false;
        }

        /// <summary>
        /// 发送车次号
        /// </summary>
        /// <param name="trainId"></param>
        /// <returns></returns>
        public override bool SendTrainId(SendModel<DriverDataModel> trainId)
        {
            if (trainId.Type == SendModelType.Ok)
            {
                DataOut.TrainCode = trainId.Content.TrainId;
                DataOut.TrainNum = DataOut.StringFloatConverter.GetNumberInt(trainId.Content.TrainId); //float.Parse(trainId);
                return true;
            }
            return false;
        }

        public override bool SendDriverId(SendModel<DriverDataModel> driverid)
        {
            if (driverid.Type == SendModelType.Ok)
            {
                DataOut.DriverCode = driverid.Content.DriverId;
                DataOut.DriverNum = DataOut.StringFloatConverter.GetNumberInt(driverid.Content.DriverId); //float.Parse(driverid);
                return true;
            }
            return false;
        }

        public override bool SendTrainData(SendModel<ReadOnlyCollection<string>> traindata)
        {
            if (traindata.Type == SendModelType.Ok)
            {
                DataOut.TrainLength = float.Parse(traindata.Content[0]);
            }
            return true;
        }

        public override bool SendDriverData(string trainid, string driverid, string rbcId, string tel)
        {
            DataOut.RBCID = float.Parse(rbcId);
            DataOut.RBCNum = float.Parse(tel);
            return true;
        }

        public override bool SendStart(SendModel<object> startState)
        {
            if (startState.Type == SendModelType.Ok)
            {
                DataOut.StartConfirm = true;
            }
            return true;
        }

        public override bool SendRelease(SendModel<object> relaseState)
        {
            if (relaseState.Type == SendModelType.Ok)
            {
                DataOut.ReleaseSign = true;
            }
            return true;
        }

        public override bool SendAlert()
        {
            DataOut.AlertBotton = true;
            return true;
        }

 

        public override bool SelectFreq(SendModel<TrainFreq> trainFreq)
        {
            if (trainFreq.Type == SendModelType.Ok)
            {
                switch (trainFreq.Content)
                {
                    case TrainFreq.Up:
                        DataOut.FrequencyUp = true;
                        break;
                    case TrainFreq.Down:
                        DataOut.FrequencyDown = true;
                        break;
                }
            }

            return true;
        }

        public override bool SelectCtcs(SendModel<CTCSType> ctcsType)
        {
            if (ctcsType.Type == SendModelType.Ok)
            {
                switch (ctcsType.Content)
                {
                    case CTCSType.CTCS0:
                        DataOut.C0ConfirmSign = true;                  
                        break;
                    case CTCSType.CTCS2:
                        DataOut.C2ConfirmSign = true;
                       
                        break;
                }
            }

            return true;
        }

        public override bool SelectControlMode(SendModel<ControlType> controlType)
        {
            if (controlType.Type == SendModelType.Ok)
            {
                switch (controlType.Content)
                {
                    case ControlType.Shunting:
                        if ((ControlType) DataOut.SignalDataIn.ControlMode == ControlType.Shunting)
                        {
                            DataOut.SHModeExit = true;
                        }
                        else
                        {
                            DataOut.SHModeSel = true;
                        }
                        break;
                    case ControlType.OnSight:
                        DataOut.OSModeSel = true;
                        break;
                    case ControlType.LKJ:
                        if ((ControlType) DataOut.SignalDataIn.ControlMode == ControlType.LKJ)
                        {
                            DataOut.CSModeExit = true;
                        }
                        else
                        {
                            DataOut.CSModeSel = true;
                        }
                        break;
                }
            }

            return true;
        }
       

        public override bool EnsureMessage(SendModel<IInfomationItem> message)
        {
            if (message.Type == SendModelType.Ok)
            {
                switch (message.Content.Content.Id)
                {

               
                    case 41:
                        DataOut.YDConfirmSign = true;
                        break;
                   /* case 42:
                        DataOut.EnterC2Sign = true;
                        break;        
                    case 75:
                        DataOut.EnterC0Sign = true;
                        break;*/
                    default:
                        break;
                }
            }
            else if (message.Type == SendModelType.Cancel)
            {
            }

            return true;
        }
    }
}
