namespace ES.Facility.PublicModule.Test
{
    interface IFmsSysAddreBaseInfo
    {
        bool CopyToArr(ref byte[] outBytes, int nBeginIndex);
        int getDefinePortNum();
        int getFmsPort(bool isCmd);
    }
}
