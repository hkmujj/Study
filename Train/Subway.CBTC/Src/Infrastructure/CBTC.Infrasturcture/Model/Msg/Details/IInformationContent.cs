using System.Diagnostics;
using CBTC.Infrasturcture.Model.Constant;

namespace CBTC.Infrasturcture.Model.Msg.Details
{

    public interface IInformationContent
    {
        int Id { get; }

        string HappenDataKey { get; }

        string Content { get; }

        string SendDataKey { get; }

        InformationShowType InformationShowType { get; }
        MessageType MessageType { get; }
    }
}