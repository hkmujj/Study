using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using LightRail.HMI.SZLHLF.Model;
using Motor.HMI.CRH380D.Controller;
using Motor.HMI.CRH380D.Models.State;
using Motor.HMI.CRH380D.Models.Units;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class WCDeviceModel : ModelBase
    {
        private float m_WaterValue0;
        private float m_WaterValue7;
        private float m_WaterValue6;
        private float m_WaterValue5;
        private float m_WaterValue4;
        private float m_WaterValue3;
        private float m_WaterValue2;
        private float m_WaterValue1;
        private float m_DirtValue0;
        private float m_DirtValue7;
        private float m_DirtValue6;
        private float m_DirtValue5;
        private float m_DirtValue4;
        private float m_DirtValue3;
        private float m_DirtValue2;
        private float m_DirtValue1;
        private WCDeviceState m_WCDevice21;
        private WCDeviceState m_WCDevice22;
        private WCDeviceState m_WCDevice31;
        private WCDeviceState m_WCDevice32;
        private WCDeviceState m_WCDevice51;
        private WCDeviceState m_WCDevice52;
        private WCDeviceState m_WCDevice61;
        private WCDeviceState m_WCDevice62;
        private WCDeviceState m_WCDevice71;
        private WCDeviceState m_WCDevice72;

        [ImportingConstructor]
        public WCDeviceModel(WCDeviceController wCDeviceController)
        {
            TrainModel = new TrainModel();

            WCDeviceController = wCDeviceController;
            WCDeviceController.ViewModel = this;
            WCDeviceController.Initalize();
        }

        /// <summary>
        /// 控制
        /// </summary>
        public WCDeviceController WCDeviceController { get; set; }
        
        /// <summary>
        /// 车辆集合
        /// </summary>
        public TrainModel TrainModel { get; private set; }

        /// <summary>
        /// 车0水箱量
        /// </summary>
        public float WaterValue0
        {
            get { return m_WaterValue0; }
            set
            {
                if (value == m_WaterValue0)
                {
                    return;
                }
                m_WaterValue0 = value;
                RaisePropertyChanged(() => WaterValue0);
            }
        }

        /// <summary>
        /// 车1水箱量
        /// </summary>
        public float WaterValue1
        {
            get { return m_WaterValue1; }
            set
            {
                if (value == m_WaterValue1)
                {
                    return;
                }
                m_WaterValue1 = value;
                RaisePropertyChanged(() => WaterValue1);
            }
        }

        /// <summary>
        /// 车2水箱量
        /// </summary>
        public float WaterValue2
        {
            get { return m_WaterValue2; }
            set
            {
                if (value == m_WaterValue2)
                {
                    return;
                }
                m_WaterValue2 = value;
                RaisePropertyChanged(() => WaterValue2);
            }
        }

        /// <summary>
        /// 车3水箱量
        /// </summary>
        public float WaterValue3
        {
            get { return m_WaterValue3; }
            set
            {
                if (value == m_WaterValue3)
                {
                    return;
                }
                m_WaterValue3 = value;
                RaisePropertyChanged(() => WaterValue3);
            }
        }

        /// <summary>
        /// 车4水箱量
        /// </summary>
        public float WaterValue4
        {
            get { return m_WaterValue4; }
            set
            {
                if (value == m_WaterValue4)
                {
                    return;
                }
                m_WaterValue4 = value;
                RaisePropertyChanged(() => WaterValue4);
            }
        }

        /// <summary>
        /// 车5水箱量
        /// </summary>
        public float WaterValue5
        {
            get { return m_WaterValue5; }
            set
            {
                if (value == m_WaterValue5)
                {
                    return;
                }
                m_WaterValue5 = value;
                RaisePropertyChanged(() => WaterValue5);
            }
        }

        /// <summary>
        /// 车6水箱量
        /// </summary>
        public float WaterValue6
        {
            get { return m_WaterValue6; }
            set
            {
                if (value == m_WaterValue6)
                {
                    return;
                }
                m_WaterValue6 = value;
                RaisePropertyChanged(() => WaterValue6);
            }
        }

        /// <summary>
        /// 车7水箱量
        /// </summary>
        public float WaterValue7
        {
            get { return m_WaterValue7; }
            set
            {
                if (value == m_WaterValue7)
                {
                    return;
                }
                m_WaterValue7 = value;
                RaisePropertyChanged(() => WaterValue7);
            }
        }

        /// <summary>
        /// 车2污物箱量
        /// </summary>
        public float DirtValue2
        {
            get { return m_DirtValue2; }
            set
            {
                if (value == m_DirtValue2)
                {
                    return;
                }
                m_DirtValue2 = value;
                RaisePropertyChanged(() => DirtValue2);
            }
        }

        /// <summary>
        /// 车3污物箱量
        /// </summary>
        public float DirtValue3
        {
            get { return m_DirtValue3; }
            set
            {
                if (value == m_DirtValue3)
                {
                    return;
                }
                m_DirtValue3 = value;
                RaisePropertyChanged(() => DirtValue3);
            }
        }

        /// <summary>
        /// 车5污物箱量
        /// </summary>
        public float DirtValue5
        {
            get { return m_DirtValue5; }
            set
            {
                if (value == m_DirtValue5)
                {
                    return;
                }
                m_DirtValue5 = value;
                RaisePropertyChanged(() => DirtValue5);
            }
        }

        /// <summary>
        /// 车6污物箱量
        /// </summary>
        public float DirtValue6
        {
            get { return m_DirtValue6; }
            set
            {
                if (value == m_DirtValue6)
                {
                    return;
                }
                m_DirtValue6 = value;
                RaisePropertyChanged(() => DirtValue6);
            }
        }

        /// <summary>
        /// 车7污物箱量
        /// </summary>
        public float DirtValue7
        {
            get { return m_DirtValue7; }
            set
            {
                if (value == m_DirtValue7)
                {
                    return;
                }
                m_DirtValue7 = value;
                RaisePropertyChanged(() => DirtValue7);
            }
        }
        
        /// <summary>
        /// 车2卫生间1状态
        /// </summary>
        public WCDeviceState WCDevice21
        {
            get { return m_WCDevice21; }
            set
            {
                if (value == m_WCDevice21)
                {
                    return;
                }
                m_WCDevice21 = value;
                RaisePropertyChanged(() => WCDevice21);
            }
        }

        /// <summary>
        /// 车2卫生间2状态
        /// </summary>
        public WCDeviceState WCDevice22
        {
            get { return m_WCDevice22; }
            set
            {
                if (value == m_WCDevice22)
                {
                    return;
                }
                m_WCDevice22 = value;
                RaisePropertyChanged(() => WCDevice22);
            }
        }

        /// <summary>
        /// 车3卫生间1状态
        /// </summary>
        public WCDeviceState WCDevice31
        {
            get { return m_WCDevice31; }
            set
            {
                if (value == m_WCDevice31)
                {
                    return;
                }
                m_WCDevice31 = value;
                RaisePropertyChanged(() => WCDevice31);
            }
        }

        /// <summary>
        /// 车3卫生间2状态
        /// </summary>
        public WCDeviceState WCDevice32
        {
            get { return m_WCDevice32; }
            set
            {
                if (value == m_WCDevice32)
                {
                    return;
                }
                m_WCDevice32 = value;
                RaisePropertyChanged(() => WCDevice32);
            }
        }

        /// <summary>
        /// 车5卫生间1状态
        /// </summary>
        public WCDeviceState WCDevice51
        {
            get { return m_WCDevice51; }
            set
            {
                if (value == m_WCDevice51)
                {
                    return;
                }
                m_WCDevice51 = value;
                RaisePropertyChanged(() => WCDevice51);
            }
        }

        /// <summary>
        /// 车5卫生间2状态
        /// </summary>
        public WCDeviceState WCDevice52
        {
            get { return m_WCDevice52; }
            set
            {
                if (value == m_WCDevice52)
                {
                    return;
                }
                m_WCDevice52 = value;
                RaisePropertyChanged(() => WCDevice52);
            }
        }

        /// <summary>
        /// 车6卫生间1状态
        /// </summary>
        public WCDeviceState WCDevice61
        {
            get { return m_WCDevice61; }
            set
            {
                if (value == m_WCDevice61)
                {
                    return;
                }
                m_WCDevice61 = value;
                RaisePropertyChanged(() => WCDevice61);
            }
        }

        /// <summary>
        /// 车6卫生间2状态
        /// </summary>
        public WCDeviceState WCDevice62
        {
            get { return m_WCDevice62; }
            set
            {
                if (value == m_WCDevice62)
                {
                    return;
                }
                m_WCDevice62 = value;
                RaisePropertyChanged(() => WCDevice62);
            }
        }

        /// <summary>
        /// 车7卫生间1状态
        /// </summary>
        public WCDeviceState WCDevice71
        {
            get { return m_WCDevice71; }
            set
            {
                if (value == m_WCDevice71)
                {
                    return;
                }
                m_WCDevice71 = value;
                RaisePropertyChanged(() => WCDevice71);
            }
        }

        /// <summary>
        /// 车7卫生间2状态
        /// </summary>
        public WCDeviceState WCDevice72
        {
            get { return m_WCDevice72; }
            set
            {
                if (value == m_WCDevice72)
                {
                    return;
                }
                m_WCDevice72 = value;
                RaisePropertyChanged(() => WCDevice72);
            }
        }
    }
}