using System;
using System.Threading;
using System.Threading.Tasks;
using Engine.TCMS.Turkmenistan.Model;

namespace Engine.TCMS.Turkmenistan.Extension
{
    public static class SentDataExtention
    {
        public static void SendBool(this string args, bool value, bool isReset)
        {
            if (GlobalParam.Instance.InitParam != null && GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary.ContainsKey(args))
            {
                var index = GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary[args];
                GlobalParam.Instance.InitParam.CommunicationDataService.WriteService.ChangeBool(index, value);
                if (isReset)
                {

                    Task.Factory.StartNew((() =>
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        GlobalParam.Instance.InitParam.CommunicationDataService.WriteService.ChangeBool(index, !value);
                    }));
                }
            }

        }
    }
}