using System.Collections.Generic;
using System.Linq;

namespace ATP200C.PublicComponents
{
    public enum UserActionData
    {
        Num1 = BtnTypeName.Num1,
        Num2,
        Num3,
        Num4,
        Num5,
        Num6,
        Num7,
        Num8,
        Num9,
        Num0,

        Switch,
    }

    public static class UserActionDataExtension
    {
        private static readonly Dictionary<UserActionData, char[]> m_DataMeanDictionary = new Dictionary<UserActionData, char[]>()
                                                                                 {
                                                                                     { UserActionData.Num1, new char[0] { } },
                                                                                     { UserActionData.Num2, new char[] { 'a', 'b', 'c' } },
                                                                                     { UserActionData.Num3, new char[] { 'd', 'e', 'f' } },
                                                                                     { UserActionData.Num4, new char[] { 'g', 'h', 'i' } },
                                                                                     { UserActionData.Num5, new char[] { 'j', 'k', 'l' } },
                                                                                     { UserActionData.Num6, new char[] { 'm', 'n', 'o' } },
                                                                                     { UserActionData.Num7, new char[] { 'p', 'q', 'r', 's' } },
                                                                                     { UserActionData.Num8, new char[] { 't', 'u', 'v' } },
                                                                                     { UserActionData.Num9, new char[] { 'w', 'x', 'y', 'z' } },
                                                                                     { UserActionData.Num0, new char[0] },
                                                                                     { UserActionData.Switch, new char[0] },
                                                                                 };

        public static char GetCharOfIndex(this UserActionData data, int index)
        {
            var datas = m_DataMeanDictionary[data];
            if (datas.Any())
            {
                return datas[index % datas.Length];
            }
            return ' ';
        }
    }
}