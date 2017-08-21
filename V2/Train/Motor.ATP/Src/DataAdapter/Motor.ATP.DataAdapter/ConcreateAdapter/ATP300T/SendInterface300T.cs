using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.BrakeTest;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.Infrasturcture.Model.Infomation;
using Motor.ATP.DataAdapter.Extension;
using Motor.ATP.Infrasturcture.Control.Infomation;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using CommonUtil.Controls;
using Motor.ATP.DataAdapter.Util;

namespace Motor.ATP.DataAdapter.ConcreateAdapter.ATP300T
{
    public class SendInterface300T : SendInterfaceBase<SignalDataOut300T>
    {
        private SignalDataOut300T signalDataOut300T;
        private Dictionary<int, Action<bool>> m_EnsureMessageOkActionDictionary;


        public SendInterface300T(SignalDataOut300T signalDataOut300T)
            : base(signalDataOut300T)
        {
            // TODO: Complete member initialization
            InitalizeMessageActions();
            this.signalDataOut300T = signalDataOut300T;
        }

        ///// <summary>
        ///// 制动测试选择
        ///// </summary>
        ///// <param name="brakeTest"></param>
        ///// <returns></returns>
        //public override bool SendBrakeTestSelect(SendModel<BrakeTestModel> brakeTest)
        //{
        //    var btm = brakeTest.Content as BrakeTestModel<BrakeTestSelect>;
        //    if (btm != null && btm.Data == BrakeTestSelect.Run)
        //    {
        //        DataOut.BrakeTestSelectProcess = true;
        //        return true;
        //    }
        //    return false;
        //}

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
        //司机号和车次号输入
        //确定进入第二步：制动测试提示
        //取消继续显示驾驶数据
        public override bool SendDriverData(SendModel<DriverDataModel> driverid)
        {
            if (driverid.Type == SendModelType.Ok)
            {
                
                DataOut.ConfirmOrCancelDriveDataInputSign = MessageConfirmType.MessageConfirmType_Confirm;
                DataOut.DriverCode = driverid.Content.DriverId;
                DataOut.DriverNum = DataOut.StringFloatConverter.GetNumberInt(driverid.Content.DriverId);//float.Parse(driverid);
                DataOut.TrainCode = driverid.Content.TrainId;
                DataOut.TrainNum = DataOut.StringFloatConverter.GetNumberInt(driverid.Content.TrainId);//float.Parse(trainId);
            }else
            {

                DataOut.ConfirmOrCancelDriveDataInputSign = MessageConfirmType.MessageConfirmType_Cancel;
            }
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
                DataOut.ConfirmOrCancelTrainDataInputSign = MessageConfirmType.MessageConfirmType_Confirm;
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

                DataOut.ConfirmOrCancelRBCInputSign = MessageConfirmType.MessageConfirmType_Confirm;
                DataOut.RBCLinkSign = true;
            }
            else
            {
                DataOut.ConfirmOrCancelRBCInputSign = MessageConfirmType.MessageConfirmType_Cancel;
              
            }
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
                        DataOut.ConfirmOrCancelLevelSign = MessageConfirmType.MessageConfirmType_Confirm;
                        break;
                    case CTCSType.CTCS3:
                        DataOut.ConfirmOrCancelLevelSign = MessageConfirmType.MessageConfirmType_Cancel;
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

        /// <summary>
        /// 通知消息计时变化 
        /// </summary>
        /// <param name="messageItem"></param>
        /// <returns></returns>
        public override bool OnMessageTimeChanged(IMessageItem messageItem)
        {
            var timingSpan = messageItem.GetTimingSpan();

            DataOut.TimeSpan = (float)timingSpan.TotalSeconds;

            return true;
        }

        /// <summary>
        /// 响应用户动作，目前只有root界面 B10按键触发
        ///     用于快速跳过启动界面等特殊需求
        /// </summary>
        /// <param name="actionType"></param>
        /// <param name="mouseState"></param>
        /// <returns></returns>
        public override bool OnUserAction(UserActionType actionType, MouseState mouseState)
        {   
            if (actionType == UserActionType.B10 && mouseState == MouseState.MouseDown)
            {
                DataOut.FastButton = true;
            }
            return true;
        }

        private void InitalizeMessageActions()
        {
            m_EnsureMessageOkActionDictionary = new Dictionary<int, Action<bool>>()
            {
                {35, (b) => {
                     if(b)  DataOut.BrakeTest = true;
                    else DataOut.CancleBrakeTest = true;
                }},
                {37, (b) =>  DataOut.AlertSign = true},
                {40, (b) =>  DataOut.ExeSign = true},
                {41, (b) =>  DataOut.YDConfirmSign = true},
                {42, (b) =>  DataOut.EnterC2Sign = true},
                {43, (b) =>  DataOut.EnterC3Sign = true},
                {44, (b) =>  DataOut.FaultNormalBreakSign = true},
                {51, (b) =>  DataOut.EnterOSSign = true},
                {57, (b) =>  DataOut.StartSign = true},
                {59, (b) => { if(b) { DataOut.BrakeTestFailSign = true;}}},
                {69, (b) =>  DataOut.YDConfirmSign = true},
                {70, (b) => {
                    if (b) 
                    {
                        DataOut.ConfirmOrCancelC2Sign = MessageConfirmType.MessageConfirmType_Confirm;
                        DataOut.C2ConfirmSign = true;
                    }
                    else DataOut.ConfirmOrCancelC2Sign = MessageConfirmType.MessageConfirmType_Cancel;
                }},

                {81, (b) => DataOut.C2ConfirmSign=true},
                   
                {82, (b) => DataOut.C3ConfirmSign=true},
                {83, (b) => {
                    if (b)
                    {
                        DataOut.ConfirmOrCancelC3Sign = MessageConfirmType.MessageConfirmType_Confirm;
                        DataOut.C3ConfirmSign = true;
                    }
                    else DataOut.ConfirmOrCancelC3Sign = MessageConfirmType.MessageConfirmType_Cancel;
                }},
                {87, (b) =>  DataOut.BaliseLostSign = true},
                {91, (b) =>  DataOut.MJSign = true},
            };
        }

        public override bool EnsureMessage(SendModel<IInfomationItem> message)
        {
            if (message.Type == SendModelType.Ok)
            {
                if (m_EnsureMessageOkActionDictionary.ContainsKey(message.Content.Content.Id))
                {
                    m_EnsureMessageOkActionDictionary[message.Content.Content.Id](true);
                }
            }
            else if (message.Type == SendModelType.Cancel)
            {
                if (m_EnsureMessageOkActionDictionary.ContainsKey(message.Content.Content.Id))
                {
                    m_EnsureMessageOkActionDictionary[message.Content.Content.Id](false);
                }
            }
            return true;
        }
    }
}