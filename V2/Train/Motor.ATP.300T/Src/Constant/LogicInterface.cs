using System.Linq;
using MMI.Facility.Interface;

namespace Motor.ATP._300T.Constant
{
    public static class LogicInterface
    {
        public const int VirOutBoolStartIndex = 4800;

        private static int? m_OutBoolStartIndex;

        private static int? m_FixOutBoolOffset;

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

        public static int GetFixOutBoolOffset(this baseClass src)
        {
            if (m_FixOutBoolOffset == null)
            {
                m_FixOutBoolOffset = src.NeedFix4800() ? VirOutBoolStartIndex : 0;
            }
            return (int) m_FixOutBoolOffset;

        }
    }
}