using System;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Project;
using Tram.CBTC.Casco.Model.Domain.Signal.Speed;
using Tram.CBTC.Casco.Model.Domain.Signal.Warning;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Model.Msg;
using Tram.CBTC.Infrasturcture.Model.Msg.Details;
using Tram.CBTC.Infrasturcture.Model.Send;

namespace Tram.CBTC.Casco.Model.Domain
{
    public class Domain : global::Tram.CBTC.Infrasturcture.Model.CBTC
    {
        private UserActionType m_LastPreesed;

        public Domain(SubsystemInitParam initParam = null)
        {
            Type = SignalType.CASCO;

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

            Recive = ServiceLocator.Current.GetInstance<Recive>();

            Recive.InitEndStationAndRoadID();
            Recive.InitPlan();

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