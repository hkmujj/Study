namespace Urban.Domain.TrainState.Interface.Common
{
    // ReSharper disable once InconsistentNaming
    public static class IUpdateParamExtension
    {
        public static float GetInFloatAt(this IUpdateParam updateParam, int index)
        {
            return updateParam.DataService.ReadService.ReadOnlyFloatDictionary[index];
        }

        public static float GetOutFloatAt(this IUpdateParam updateParam, int index)
        {
            return updateParam.DataService.WriteService.ReadOnlyFloatDictionary[index];
        }
        public static bool GetInBoolAt(this IUpdateParam updateParam, int index)
        {
            return updateParam.DataService.ReadService.ReadOnlyBoolDictionary[index];
        }
        public static bool GetOutBoolAt(this IUpdateParam updateParam, int index)
        {
            return updateParam.DataService.WriteService.ReadOnlyBoolDictionary[index];
        }


        public static float GetInFloatAt(this IUpdateParam updateParam, string index)
        {
            return updateParam.GetInFloatAt(updateParam.IndexFacade.InFloatDictionary[index]);
        }

        public static float GetOutFloatAt(this IUpdateParam updateParam, string index)
        {
            return updateParam.GetOutFloatAt(updateParam.IndexFacade.OutFloatDictionary[index]);
        }
        public static bool GetInBoolAt(this IUpdateParam updateParam, string index)
        {
            return updateParam.GetInBoolAt(updateParam.IndexFacade.InBoolDictionary[index]);
        }
        public static bool GetOutBoolAt(this IUpdateParam updateParam, string index)
        {
            return updateParam.GetOutBoolAt(updateParam.IndexFacade.OutBoolDictionary[index]);
        }
    }
}