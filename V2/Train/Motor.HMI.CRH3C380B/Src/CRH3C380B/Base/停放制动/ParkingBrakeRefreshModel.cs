namespace Motor.HMI.CRH3C380B.Base.停放制动
{
    public class ParkingBrakeRefreshModel
    {
        public ParkingBrakeRefreshModel(int[] indexs)
        {
            ParkingBrakeState = ParkingBrakeState.None;
            Indexs = indexs;
        }

        public ParkingBrakeState ParkingBrakeState { set; get; }

        public int[] Indexs { private set; get; }
    }
}