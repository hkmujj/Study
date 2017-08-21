using Tram.CBTC.DataAdapter.Util;

namespace Tram.CBTC.DataAdapter.Model
{
    public class SignalDataOut
    {
        public IStringFloatConverter StringFloatConverter { get; private set; }

        public readonly char[] RBCDataBuffer = new char[4 * 3];

        public global::Tram.CBTC.Infrasturcture.Model.CBTC CBTC { set; get; }

        /// <summary>
        /// 输入接口
        /// </summary>
        public SignalDataIn SignalDataIn { get; set; }


         //bool————————————————————————————————
       

        ///// <summary>
        ///// 允许缓解
        ///// </summary>
        //public bool ReleaseSign { set; get; }


        //float————————————————————————————————

        /// <summary>
        /// 司机号字母
        /// </summary>
        public string DriverLetter { set; get; }

        /// <summary>
        /// 司机号
        /// </summary>
        public float DriverNum { set; get; }

        /// <summary>
        /// 车次号字母
        /// </summary>
        public string TrainLetter { set; get; }

        /// <summary>
        /// 车次号
        /// </summary>
        public float TrainNum { set; get; }

        /// <summary>
        /// 编组
        /// </summary>
        public float GroupNum { set; get; }

        /// <summary>
        /// 目的地号字母
        /// </summary>
        public float DestLetter { set; get; }

        /// <summary>
        /// 目的地号
        /// </summary>
        public float DestNum { set; get; }

        /// <summary>
        /// ATP音量
        /// </summary>
        public float ATPVolume { set; get; }


        /// <summary>
        ///雷达状态
        /// </summary>
        public bool OpenRadar { set; get; }

        public SignalDataOut()
        {
            StringFloatConverter = new StringFloatConverter();
            OpenRadar = true;
        }

        public virtual void ClearInfo()
        {
            DriverLetter = "";
            TrainLetter = "";
            DriverNum = -1;
            TrainNum = -1;
            GroupNum = -1;
        }
    }
}