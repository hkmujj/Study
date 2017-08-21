using CBTC.DataAdapter.Model;

namespace CBTC.DataAdapter.ConcreateAdapter.TCT
{
    public class SignalDataOutTCT : SignalDataOut
    {
        ///// <summary>
        ///// 制动测试试闸
        ///// </summary>
        //public bool BrakeTestSwitch { set; get; }

        ///// <summary>
        ///// 制动测试缓解
        ///// </summary>
        //public bool BrakeTestRelease { set; get; }

        ///// <summary>
        ///// 广播测试
        ///// </summary>
        //public bool BroadcastTest { set; get; }

        ///// <summary>
        ///// 灯测试
        ///// </summary>
        //public bool LampTest { set; get; }

        public SignalDataOutTCT() : base()
        {
            //BrakeTestSwitch = false;
            //BrakeTestRelease = false;
            //BroadcastTest = false;
            //LampTest = false;
        }

        public override void ClearInfo()
        {
            base.ClearInfo();
        }
    }
}