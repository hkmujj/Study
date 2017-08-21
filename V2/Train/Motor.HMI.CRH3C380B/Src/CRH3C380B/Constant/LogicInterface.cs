using System;
using System.Linq;
using MMI.Facility.Interface;

namespace Motor.HMI.CRH3C380B.Constant
{
    public static class LogicInterface
    {
        public const int VirOutBoolStartIndex = 4800;

        private static int? m_OutBoolStartIndex;

        public static int GetActureOutBoolStartIndex(this baseClass src)
        {
            if (m_OutBoolStartIndex == null)
            {
                m_OutBoolStartIndex = src.OutBoolList.Min(m => m.Key);
            }

            return (int) m_OutBoolStartIndex;
        }

        public static bool NeedFix4800(this baseClass src)
        {
            return src.GetActureOutBoolStartIndex() == 0;
        }

        public static int GetActureOutbIndex(this baseClass src, string name)
        {
            return GetActureOutbIndex(src, src.GetOutBoolIndex(name));
        }

        public static int GetActureOutbIndex(this baseClass src, int index)
        {
            if (NeedFix4800(src))
            {
                return index >= VirOutBoolStartIndex ? index - VirOutBoolStartIndex : index;
            }

            return index;
        }
    }
}