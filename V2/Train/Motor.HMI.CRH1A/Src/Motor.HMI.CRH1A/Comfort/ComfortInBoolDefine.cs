namespace Motor.HMI.CRH1A.Comfort
{
    public class ComfortInBoolDefine
    {
        private const int HvacStartOff = 16;

        public static int GetHvacCutOffIdx(int trainNo)
        {
            return HvacStartOff + trainNo;
        }

        public static int GetHvacFaultIdx(int trainNo)
        {

            return HvacStartOff + 8 + trainNo;
        }

        public static int GetHvacSwitch(int trainNo)
        {
            return HvacStartOff + 16 + trainNo;
        }
    }

}
