using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.HMI.CRH380D.Constant;
using Motor.HMI.CRH380D.Models.Model;
using Motor.HMI.CRH380D.Models.State;
using Motor.HMI.CRH380D.View.Common.CarFireDevice;

namespace Motor.HMI.CRH380D.Controller
{
    [Export]
    public class FireDeviceController : ControllerBase<FireDeviceModel>
    {
        [ImportingConstructor]
        public FireDeviceController()
        {
            m_RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
        }

        public override void Initalize()
        {
            //选中状态
            ViewModel.TrainModel.CarModel0.IsCheck = false;
            ViewModel.TrainModel.CarModel7.IsCheck = false;
            ViewModel.TrainModel.CarModel6.IsCheck = false;
            ViewModel.TrainModel.CarModel5.IsCheck = false;
            ViewModel.TrainModel.CarModel4.IsCheck = false;
            ViewModel.TrainModel.CarModel3.IsCheck = false;
            ViewModel.TrainModel.CarModel2.IsCheck = false;
            ViewModel.TrainModel.CarModel1.IsCheck = false;

            //按钮使能
            ViewModel.TrainModel.CarModel0.IsEnable = true;
            ViewModel.TrainModel.CarModel7.IsEnable = true;
            ViewModel.TrainModel.CarModel6.IsEnable = true;
            ViewModel.TrainModel.CarModel5.IsEnable = true;
            ViewModel.TrainModel.CarModel4.IsEnable = true;
            ViewModel.TrainModel.CarModel3.IsEnable = true;
            ViewModel.TrainModel.CarModel2.IsEnable = true;
            ViewModel.TrainModel.CarModel1.IsEnable = true;

            //Borderground
            ViewModel.TrainModel.CarModel0.State = CarState.None;
            ViewModel.TrainModel.CarModel7.State = CarState.None;
            ViewModel.TrainModel.CarModel6.State = CarState.None;
            ViewModel.TrainModel.CarModel5.State = CarState.None;
            ViewModel.TrainModel.CarModel4.State = CarState.None;
            ViewModel.TrainModel.CarModel3.State = CarState.None;
            ViewModel.TrainModel.CarModel2.State = CarState.None;
            ViewModel.TrainModel.CarModel1.State = CarState.None;
            
            ViewModel.TrainModel.TrainCheckedCommand = new DelegateCommand<CarModel>(OnTrainChecked);
        }

        public void Clear()
        {

        }

        /// <summary>
        /// 车辆点击处理函数
        /// </summary>
        public void OnTrainChecked(CarModel carModel)
        {
            bool OneIsCheck = false;

            //车辆选择复位
            foreach (var value in ViewModel.TrainModel.AllCarModels)
            {
                if (value.IsCheck && value != carModel)
                {
                    value.IsCheck = false;
                }
            }

            //区域加载
            foreach (var value in ViewModel.TrainModel.AllCarModels)
            {
                if (value.IsCheck)
                {
                    OneIsCheck = true;

                    switch (value.CarNum)
                    {
                        case 0:
                            m_RegionManager.RequestNavigate(RegionNames.FireDeviceCommon, typeof(Car0FireDeviceCommon).FullName);
                            break;
                        case 1:
                            m_RegionManager.RequestNavigate(RegionNames.FireDeviceCommon, typeof(Car1FireDeviceCommon).FullName);
                            break;
                        case 2:
                            m_RegionManager.RequestNavigate(RegionNames.FireDeviceCommon, typeof(Car2FireDeviceCommon).FullName);
                            break;
                        case 3:
                            m_RegionManager.RequestNavigate(RegionNames.FireDeviceCommon, typeof(Car3FireDeviceCommon).FullName);
                            break;
                        case 4:
                            m_RegionManager.RequestNavigate(RegionNames.FireDeviceCommon, typeof(Car4FireDeviceCommon).FullName);
                            break;
                        case 5:
                            m_RegionManager.RequestNavigate(RegionNames.FireDeviceCommon, typeof(Car5FireDeviceCommon).FullName);
                            break;
                        case 6:
                            m_RegionManager.RequestNavigate(RegionNames.FireDeviceCommon, typeof(Car6FireDeviceCommon).FullName);
                            break;
                        case 7:
                            m_RegionManager.RequestNavigate(RegionNames.FireDeviceCommon, typeof(Car7FireDeviceCommon).FullName);
                            break;
                        default:
                            m_RegionManager.RequestNavigate(RegionNames.FireDeviceCommon, typeof(NullCarFireDeviceCommon).FullName);
                            break;
                    }
                }
            }

            if (!OneIsCheck)
            {
                m_RegionManager.RequestNavigate(RegionNames.FireDeviceCommon, typeof(NullCarFireDeviceCommon).FullName);
            }
        }

        /// <summary>
        /// 区域管理器
        /// </summary>
        private readonly IRegionManager m_RegionManager;
    }
}