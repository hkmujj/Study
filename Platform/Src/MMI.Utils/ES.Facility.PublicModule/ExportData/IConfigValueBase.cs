using System.Collections.Generic;

namespace ES.Facility.PublicModule.ExportData
{
    interface IConfigValueBase<TParaKey, TValueType>
    {
        void clear();
        bool decodeFromString(string cString, ref int tmpErrorIndex);
        bool initParaListFromString(ref int tmpErrorIndex);
        Dictionary<TParaKey, TValueType> ParaObjList { get; set; }
        List<string> StringList { get; set; }
    }
}
