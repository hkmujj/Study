using System;
using System.Collections.ObjectModel;
using System.Linq;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.BrakeTest;
using Motor.ATP.Infrasturcture.Interface.Infomation;

namespace Motor.ATP.DataAdapter.ConcreateAdapter.ATP300S
{
    public class SendInterface300S : SendInterfaceBase<SignalDataOut300S>
    {
        public SendInterface300S(SignalDataOut300S dataOut) : base(dataOut)
        {
        }

        /// <summary>
        /// 制动测试选择
        /// </summary>
        /// <param name="brakeTest"></param>
        /// <returns></returns>
        public override bool SendBrakeTestSelect(SendModel<BrakeTestModel> brakeTest)
        {
            var btm = brakeTest.Content as BrakeTestModel<BrakeTestSelect>;
            if (btm != null && btm.Data == BrakeTestSelect.Run)
            {
                DataOut.BrakeTestSelectProcess = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 制动测试请求确认
        /// </summary>
        /// <param name="brakeTest"></param>
        /// <returns></returns>
        public override bool EnsurceBrakeTestSelect(SendModel<BrakeTestModel> brakeTest)
        {
            var btm = brakeTest.Content as BrakeTestModel<BrakeTestSelect>;
            if (btm != null && btm.Data == BrakeTestSelect.Run)
            {
                DataOut.BrakeTest = true;
                return true;
            }
            //TODO
            return false;
        }
        public override bool SendTrainId(SendModel<DriverDataModel> trainId)
        {
            DataOut.TrainCode = trainId.Content.TrainId;
            DataOut.TrainNum = DataOut.StringFloatConverter.GetNumberInt(trainId.Content.TrainId);//float.Parse(trainId);
            return true;
        }

        public override bool SendDriverId(SendModel<DriverDataModel> driverid)
        {
            DataOut.DriverCode = driverid.Content.DriverId;
            DataOut.DriverNum = DataOut.StringFloatConverter.GetNumberInt(driverid.Content.DriverId);//float.Parse(driverid);
            return true;
        }

        public override bool SendTrainData(SendModel<ReadOnlyCollection<string>> traindata)
        {
            if (traindata.Type == SendModelType.Ok)
            {
                DataOut.TrainLength = float.Parse(traindata.Content.First());
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

        public override bool SendRBCData(SendModel<RBCDataModel> rbcData)
        {

            if (rbcData.Type == SendModelType.Ok && rbcData.Content.ValidDataType== (RBCDataType.RBCID | RBCDataType.TelNumber))
            {
                DataOut.ATP.TrainInfo.ConnectState.RBCID = rbcData.Content.RbcId;
                DataOut.ATP.TrainInfo.ConnectState.TelNumber = rbcData.Content.TelNumber;

                Array.Clear(DataOut.RBCDataBuffer, 0, DataOut.RBCDataBuffer.Length);
                rbcData.Content.RbcId.CopyTo(0, DataOut.RBCDataBuffer, 0, rbcData.Content.RbcId.Length);
                var id = DataOut.StringFloatConverter.ConvertCharArray(DataOut.RBCDataBuffer);
                DataOut.RBCID = id[0];
                DataOut.RBCID2 = id[1];
                DataOut.RBCID3 = id[2];

                Array.Clear(DataOut.RBCDataBuffer, 0, DataOut.RBCDataBuffer.Length);
                rbcData.Content.TelNumber.CopyTo(0, DataOut.RBCDataBuffer, 0, rbcData.Content.TelNumber.Length);
                var tn = DataOut.StringFloatConverter.ConvertCharArray(DataOut.RBCDataBuffer);
                DataOut.RBCNum = tn[0];
                DataOut.RBCNum2 = tn[1];
                DataOut.RBCNum3 = tn[2];

                if (DataOut.EnterC3SelFlag)
                {
                    DataOut.RBCLinkSign = true;
                    DataOut.C3ConfirmSign = true;

                    DataOut.EnterC3SelFlag = false;
                    DataOut.C3SelFlag = false;
                }
            }
            else
            {
                DataOut.EnterC3SelFlag = false;
                DataOut.C3SelFlag = false;
            }
            return true;
            //throw new NotImplementedException();
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
                    default:
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
                    case CTCSType.CTCS2:
                        DataOut.C2ConfirmSign = true;
                        break;
                    case CTCSType.CTCS3:
                        DataOut.EnterC3SelFlag = true;
                        DataOut.C3SelFlag = true;
                        break;
                    default:
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
                        if ((ControlType)DataOut.SignalDataIn.ControlMode == ControlType.Shunting)
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
                        if ((ControlType)DataOut.SignalDataIn.ControlMode == ControlType.LKJ)
                        {
                            DataOut.CSModeExit = true;
                        }
                        else
                        {
                            DataOut.CSModeSel = true;
                        }
                        break;
                    default:
                        break;
                }
            }
            return true;
        }

        public override bool SendTrainDataConfirm()
        {
            DataOut.StartTrainDataSign = true;
            return true;
        }

        public override bool SendFreqConfirm()
        {
            DataOut.StartFreqSign = true;
            return true;
        }

        public override bool EnsureMessage(SendModel<IInfomationItem> message)
        {
            if (message.Type == SendModelType.Ok)
            {
                switch (message.Content.Content.Id)
                {
                    case 35:
                        DataOut.BrakeTest = true;
                        break;
                    case 36:
                        DataOut.ReleaseSign = true;
                        break;
                    case 37:
                        DataOut.AlertSign = true;
                        break;
                    case 38:
                        DataOut.AlertSign = true;
                        break;
                    case 39:
                        DataOut.MJSign = true;
                        break;
                    case 40:
                        DataOut.ExeSign = true;
                        break;
                    case 41:
                        DataOut.YDConfirmSign = true;
                        break;
                    case 42:
                        DataOut.EnterC2Sign = true;
                        break;
                    case 43:
                        DataOut.EnterC3Sign = true;
                        break;
                    case 44:
                        DataOut.FaultNormalBreakSign = true;
                        break;
                    case 45:
                        DataOut.ExceSelSign = true;
                        break;
                    case 46:
                        DataOut.BackProtectSign = true;
                        break;
                    case 47:
                        DataOut.SlideConfirmSign = true;
                        break;
                    case 48:
                        DataOut.StartSign = true;
                        break;
                    case 49:
                        DataOut.EBRelSign = true;
                        break;
                    case 50:
                        DataOut.CBRelSign = true;
                        break;
                    case 51:
                        DataOut.EnterOSSign = true;
                        break;
                    default:
                        break;
                }
            }
            else if (message.Type == SendModelType.Cancel)
            {
                switch (message.Content.Content.Id)
                {
                    case 45: //取消已选择越行，确认越行
                        DataOut.CancleExceSelSign = true;
                        break;
                    case 40: //取消确认越行
                        DataOut.CancleExeSign = true;
                        break;
                }
            }
            return true;
        }
    }
}