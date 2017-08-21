using CBTC.Infrasturcture.Model.Constant;
using CBTC.Infrasturcture.Model.Msg;
using MMI.Facility.Interface.Project;

namespace Subway.CBTC.BeiJiaoKong.Models.Domain
{
    public class Domain : global::CBTC.Infrasturcture.Model.CBTC
    {
        public Domain(SubsystemInitParam initParam = null)
        {
            Type = SignalType.TCT;

            if (initParam != null)
            {
                InitParam = initParam;
                DataService = initParam.CommunicationDataService;
            }

            Initalize();
        }

        public override void Initalize()
        {
            SignalInfo.Speed.SpeedDialPlate = FullSpeedDial.Instance;

            Message.MessageFactory = new MessageFactory(GlobalParams.Instance.InformationContents.Value, Message);

        }

    }
}