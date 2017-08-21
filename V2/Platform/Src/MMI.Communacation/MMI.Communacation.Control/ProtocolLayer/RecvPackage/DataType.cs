namespace MMI.Communacation.Control.ProtocolLayer.RecvPackage
{
    public enum RecvDataType : int
    {
        #region B , C 类协议

        FirstPackage = 1505,
        BCTypeMin = FirstPackage,

        SecondPackage = 1997,
        ThirdPackage = 1998,

        BCTypeMax = ThirdPackage,

        #endregion
    }
}
