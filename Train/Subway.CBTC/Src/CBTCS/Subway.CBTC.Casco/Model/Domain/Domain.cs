using System;
using System.Windows.Input;
using CBTC.Infrasturcture.Model.Constant;
using CBTC.Infrasturcture.Model.Msg;
using CBTC.Infrasturcture.Model.Msg.Details;
using CBTC.Infrasturcture.Model.Send;
using MMI.Facility.Interface.Project;
using Subway.CBTC.Casco.Model.Domain.Signal.Speed;
using Subway.CBTC.Casco.Model.Domain.Signal.Warning;

namespace Subway.CBTC.Casco.Model.Domain
{
    public class Domain : global::CBTC.Infrasturcture.Model.CBTC
    {
        private UserActionType m_LastPreesed;

        public Domain(SubsystemInitParam initParam = null)
        {
            Type = SignalType.QuanLuTong;

            if (initParam != null)
            {
                InitParam = initParam;
                DataService = initParam.CommunicationDataService;
            }
        }

        public override void Initalize()
        {
            SignalInfo.Speed.SpeedDialPlate = FullSpeedDial.Instance;

            
            SignalInfo.WarningIntervention.TargitDistanceScale = new TagetDistanceScale();

            Message.MessageFactory = new MessageFactory(GlobalParam.Instance.InformationContents.Value, Message);

            Hardware.ButtonEvent += HardwareOnButtonEvent;
        }

        private void HardwareOnButtonEvent(UserActionType userActionType, MouseButtonState mouseButtonState)
        {
            if (mouseButtonState == MouseButtonState.Pressed)
            {
                m_LastPreesed = userActionType;
            }
            else
            {
                switch (userActionType)
                {
                    case UserActionType.Ok:
                        if (Message.ShowingItem != null)
                        {
                            SendInterface.EnsureMessage(new SendModel<IInformationContent>(Message.ShowingItem.InformationContent));
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("userActionType", userActionType, null);
                }
            }
        }
    }
}