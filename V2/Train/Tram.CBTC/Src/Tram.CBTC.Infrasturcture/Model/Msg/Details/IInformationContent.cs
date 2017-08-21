using System;
using Tram.CBTC.Infrasturcture.Model.Constant;

namespace Tram.CBTC.Infrasturcture.Model.Msg.Details
{

    public interface IInformationContent : ICloneable
    {
        int Id { get; }

        string HappenDataKey { get; }

        string Content { get; }

        string SendDataKey { get; }
        /// <summary>
        /// 故障发生时间
        /// </summary>
        DateTime HappenDate { get; set; }

        InformationShowType InformationShowType { get; }
        MessageType MessageType { get; }
    }
}