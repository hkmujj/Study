using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace Subway.XiaMenLine1.Subsystem.Extension
{
    public static class WritableReadServiceExtension
    {

        public static bool ChangeBool(this IWritableCommunicationDataReadService writable,string index, bool value, bool notifyDataChangedEvent = false)
        {
            
            return writable.ChangeBool(GetInBoolIndex(index), value, notifyDataChangedEvent);
        }

        public static bool ChangeBool(this IWritableCommunicationDataReadService writable, string index, bool value, out CommunicationDataChangedArgs<bool> changed)
        {
            return writable.ChangeBool(GetInBoolIndex(index), value, out changed);
        }


        public static bool ChangeFloat(this IWritableCommunicationDataReadService writable, string index, float value, bool notifyDataChangedEvent = false)
        {
            return writable.ChangeFloat(GetInFloatIndex(index), value, notifyDataChangedEvent);
        }

        public static bool ChangeFloat(this IWritableCommunicationDataReadService writable, string index, float value, out CommunicationDataChangedArgs<float> changed)
        {
            return writable.ChangeFloat(GetInBoolIndex(index), value, out changed);
        }


        private static int GetInBoolIndex(string name)
        {
            return SubsysParams.Instance.IndexDescriptionConfig.InBoolDescriptionDictionary[name];
        }

        public static int GetInFloatIndex(string name)
        {
            return SubsysParams.Instance.IndexDescriptionConfig.InFloatDescriptionDictionary[name];
        }
    }
}