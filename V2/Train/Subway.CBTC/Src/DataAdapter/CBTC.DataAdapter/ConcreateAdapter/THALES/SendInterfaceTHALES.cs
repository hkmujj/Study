using CBTC.DataAdapter.Model;
using CBTC.Infrasturcture.Model.Msg.Details;
using CBTC.Infrasturcture.Model.Send;

namespace CBTC.DataAdapter.ConcreateAdapter.THALES
{
    public class SendInterfaceTHALES : SendInterfaceBase
    {
        public SendInterfaceTHALES(SignalDataOut dataOut) : base(dataOut)
        {
        }

        /// <summary>
        /// 确认一个消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public override bool EnsureMessage(SendModel<IInformationContent> message)
        {
            DataOut.CBTC.Message.MessageFactory.RemoveMessage(message.Content.Id);
            return base.EnsureMessage(message);
        }
    }
}