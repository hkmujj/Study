using System.Linq;

namespace Urban.Wuxi.TMS
{
    static class TMSBaseClassExtension
    {
        private static int OutBoolStartIndex { set; get; }

        static TMSBaseClassExtension()
        {
            OutBoolStartIndex = int.MaxValue;
        }

        public static int GetOutboolStartIndex(this TMSBaseClass obj)
        {
            if (OutBoolStartIndex == int.MaxValue)
            {
                OutBoolStartIndex = obj.OutBoolList.Keys.Min();
            }
            return OutBoolStartIndex;
        }
    }
}