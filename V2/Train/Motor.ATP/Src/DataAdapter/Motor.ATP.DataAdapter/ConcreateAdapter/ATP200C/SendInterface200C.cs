using System.Collections.ObjectModel;
using Motor.ATP.DataAdapter.Extension;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.BrakeTest;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP.DataAdapter.ConcreateAdapter.ATP200C
{
    public class SendInterface200C : SendInterfaceBase<SignalDataOut200C>
    {
        private SignalDataOut200C signalDataOut200C;

        public SendInterface200C(SignalDataOut200C signalDataOut200C)
            : base(signalDataOut200C)
        {
            // TODO: Complete member initialization
            this.signalDataOut200C = signalDataOut200C;
        }

   

        //制动测试选择：300H和200C函数名一致，但内容不一致
        //200C
        public override bool SendBrakeTestSelect(SendModel<BrakeTestModel> brakeTest)
        {
            if (brakeTest.Content.GetType() == typeof(BrakeTestModel<BrakeTestType>))
            {
                var bt = (BrakeTestModel<BrakeTestType>)brakeTest.Content;

                if (bt.Data == BrakeTestType.EmagerceBrakeTest)
                {
                    DataOut.BrakeTestEmerg = true;
                }
                else if (bt.Data == BrakeTestType.NormalBrakeTest)
                {
                    DataOut.BrakeTestBreak7 = true;
                }
            }

           
            return false;
        }
         //DMI测试
        public override  bool SendAssistScreenTest(SendModel<AssistScreenTestModel> assistScreenTestModel)
        {
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
                        if ((ControlType)DataOut.SignalDataIn.ControlMode == ControlType.StandBy)
                        {
                            DataOut.UpdateDriverInterface(DriverInterfaceKeys.Root_载频);
                        }
                        
                        break;
                    case CTCSType.CTCS2:
                        DataOut.C2ConfirmSign = true;
                        if ((ControlType)DataOut.SignalDataIn.ControlMode == ControlType.StandBy)
                        {
                            DataOut.UpdateDriverInterface(DriverInterfaceKeys.Root_载频);
                        }
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

                    case 36:
                        DataOut.ReleaseSign = true;
                        break;
                    case 38:
                        DataOut.OSAlarmSign = true;
                        break;
                    case 39:
                        DataOut.MJSign = true;
                        break;
                    case 41:
                        DataOut.YDConfirmSign = true;
                        break;
                    case 42:
                        DataOut.EnterC2Sign = true;
                        break;
                    case 61:
                        DataOut.TrainBrakeReleaseSign = true;
                        break;
                    case 69:
                        DataOut.SRAlarmSign = true;
                        break;
                    case 75:
                        DataOut.EnterC0Sign = true;
                        break;
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
