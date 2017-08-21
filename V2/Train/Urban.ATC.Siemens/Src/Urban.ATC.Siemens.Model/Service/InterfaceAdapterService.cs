using MMI.Facility.Interface.Data.Config;
using Motor.ATP.Domain.Interface.Service;

namespace Motor.ATP.Domain.Model.Service
{
    public class InterfaceAdapterService : IInterfaceAdapterService
    {

        public MMI.Facility.Interface.IReadOnlyDictionary<string, int> InBoolDictionary { private set; get; }

        public MMI.Facility.Interface.IReadOnlyDictionary<string, int> InFloatDictionary { private set; get; }

        public MMI.Facility.Interface.IReadOnlyDictionary<string, int> OutBoolDictionary { private set; get; }

        public MMI.Facility.Interface.IReadOnlyDictionary<string, int> OutFloatDictionary { private set; get; }

        public void Initalize(IProjectIndexDescriptionConfig descriptionConfig)
        {
            InBoolDictionary = descriptionConfig.InBoolDescriptionDictionary;
            InFloatDictionary = descriptionConfig.InFloatDescriptionDictionary;
            OutBoolDictionary = descriptionConfig.OutBoolDescriptionDictionary;
            OutFloatDictionary = descriptionConfig.OutFloatDescriptionDictionary;
        }
    }
}