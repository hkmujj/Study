namespace MMI.Communacation.Interface.AppLayer
{
    public interface ICommunacationDataSender
    {
        bool Send(byte[] datas, string ip, int port);
    }
}