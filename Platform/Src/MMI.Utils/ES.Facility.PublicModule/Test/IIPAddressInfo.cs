namespace ES.Facility.PublicModule.Test
{
    interface IIPAddressInfo
    {
        void SetIpPoint(string cip, int nport);
        void SetIpPoint();
        bool SetPortFromIndex(IPAddressInfo.NetPort nPortIndex, bool isCmd);
    }
}
