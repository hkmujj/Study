using System;
using Engine.LCDM.HXD3.Common;
using Engine.LCDM.HXD3.Config;

namespace Engine.LCDM.HXD3.Extentions
{
    public static class CommunicationExtebtion
    {
        public static void SendData(this bool sendData, string key)
        {
            GlobalParam.Instance.InitPara.CommunicationDataService.WriteService.ChangeBool(
                GlobalParam.Instance.IndexConfig.OutBoolDescriptionDictionary[key], sendData);
        }
        public static void SendData(this float sendData, string key)
        {
            GlobalParam.Instance.InitPara.CommunicationDataService.WriteService.ChangeFloat(
                GlobalParam.Instance.IndexConfig.OutFloatDescriptionDictionary[key], sendData);
        }
    }
}