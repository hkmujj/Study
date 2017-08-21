namespace Motor.HMI.CRH3C380B.Common
{
    public class GetCarNum
    {
        public static string GetNum(int index, bool mashModel, bool tarinside)
        {
            var s=GlobalParam.Instance.ProjectType == ProjectType.CRH3C
                ? (tarinside
                    ? (mashModel ? 18 : 8) - index - (index > 7 ? 2 : 0)
                    : index + 1 + (index > 7 ? 2 : 0)).ToString("0")
                : (tarinside
                    ? (mashModel ? 28 : 18) - index - (index > 7 ? 2 : 0)
                    : index + 11 + (index > 7 ? 2 : 0)).ToString("00");
            return s;
        }
    }
}