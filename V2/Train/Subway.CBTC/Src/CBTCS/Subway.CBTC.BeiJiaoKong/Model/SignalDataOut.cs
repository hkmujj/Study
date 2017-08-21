using Motor.ATP.DataAdapter.Util;
using Motor.ATP.Infrasturcture.Model;

namespace Motor.ATP.DataAdapter.Model
{
    public class SignalDataOut
    {
        public IStringFloatConverter StringFloatConverter { get; private set; }

        public readonly char[] RBCDataBuffer = new char[4 * 3];

        public ATPDomain ATP { set; get; }

        /// <summary>
        /// 输入接口
        /// </summary>
        public SignalDataIn SignalDataIn { get; set; }

        /// <summary>
        /// 启动确认,人工操作
        /// </summary>
        public bool StartConfirm { set; get; }

        /// <summary>
        /// 请求制动测试选择
        /// </summary>
        public bool BrakeTestSelectProcess { set; get; }

        /// <summary>
        /// 请求制动测试
        /// </summary>
        public bool BrakeTest { set; get; }

        /// <summary>
        /// C2确认
        /// </summary>
        public bool C2ConfirmSign { set; get; }

        /// <summary>
        /// C3确认
        /// </summary>
        public bool C3ConfirmSign { set; get; }

        /// <summary>
        /// 载频上行
        /// </summary>
        public bool FrequencyUp { set; get; }

        /// <summary>
        /// 载频下行
        /// </summary>
        public bool FrequencyDown { set; get; }

        /// <summary>
        /// 允许缓解
        /// </summary>
        public bool ReleaseSign { set; get; }

        /// <summary>
        /// 选择调车模式
        /// </summary>
        public bool SHModeSel { set; get; }

        /// <summary>
        /// 选择目视模式
        /// </summary>
        public bool OSModeSel { set; get; }

        /// <summary>
        /// 选择机车信号模式
        /// </summary>
        public bool CSModeSel { set; get; }

        /// <summary>
        /// 退出调车
        /// </summary>
        public bool SHModeExit { set; get; }

        /// <summary>
        /// 退出目视
        /// </summary>
        public bool OSModeExit { set; get; }

        /// <summary>
        /// 退出机信
        /// </summary>
        public bool CSModeExit { set; get; }

        /// <summary>
        /// 警惕确认
        /// </summary>
        public bool AlertSign { set; get; }

        /// <summary>
        /// 电话号码发送确认
        /// </summary>
        public bool RBCCodeSign { set; get; }

        /// <summary>
        /// 冒进确认
        /// </summary>
        public bool MJSign { set; get; }

        /// <summary>
        /// 越行确认
        /// </summary>
        public bool ExeSign { set; get; }

        /// <summary>
        /// 取消越行确认
        /// </summary>
        public bool CancleExeSign { set; get; }

        /// <summary>
        /// 进入C2
        /// </summary>
        public bool EnterC2Sign { set; get; }

        /// <summary>
        /// 进入C3
        /// </summary>
        public bool EnterC3Sign { set; get; }

        /// <summary>引导确认
        /// 
        /// </summary>
        public bool YDConfirmSign { set; get; }

        /// <summary>
        /// RBC连接确认
        /// </summary>
        public bool RBCLinkSign { set; get; }

        /// <summary>
        /// 警惕按钮
        /// </summary>
        public bool AlertBotton { set; get; }

        /// <summary>
        /// 进入目视行车模式确认
        /// </summary>
        public bool EnterOSSign { set; get; }


        /// <summary>
        /// 司机号字母
        /// </summary>
        public string DriverCode { set; get; }

        /// <summary>
        /// 司机号数字
        /// </summary>
        public float DriverNum { set; get; }

        /// <summary>
        /// 车次号字母
        /// </summary>
        public string TrainCode { set; get; }

        /// <summary>
        /// 车次号数字
        /// </summary>
        public float TrainNum { set; get; }

        /// <summary>
        /// 列车长度
        /// </summary>
        public float TrainLength { set; get; }

        /// <summary>
        /// RBCID
        /// </summary>
        public float RBCID { set; get; }

        /// <summary>
        /// RBC电话号码
        /// </summary>
        public float RBCNum { set; get; }

        /// <summary>
        /// ATP音量
        /// </summary>
        public float ATPVolume { set; get; }

        /// <summary>
        /// RBCID
        /// </summary>
        public float RBCID2 { set; get; }

        /// <summary>
        /// RBC电话号码
        /// </summary>
        public float RBCNum2 { set; get; }

        /// <summary>
        /// RBCID
        /// </summary>
        public float RBCID3 { set; get; }

        /// <summary>
        /// RBC电话号码
        /// </summary>
        public float RBCNum3 { set; get; }


        public SignalDataOut()
        {
            StringFloatConverter = new StringFloatConverter();
        }

        public virtual void ClearInfo()
        {
            DriverCode = "";
            TrainCode = "";
            DriverNum = -1;
            TrainNum = -1;
            RBCID = -1;
            RBCNum = -1;
            TrainLength = -1;
        }
    }
}