using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Input;
using LightRail.HMI.SZLHLF.Model;
using Motor.HMI.CRH380D.Models.State;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class TrainModel : ModelBase
    {
        private VOBCState m_MC00;
        private VOBCState m_MC01;

         [ImportingConstructor]
        public TrainModel()
        {
            CarModel0 = new CarModel(0);
            CarModel7 = new CarModel(7);
            CarModel6 = new CarModel(6);
            CarModel5 = new CarModel(5);
            CarModel4 = new CarModel(4);
            CarModel3 = new CarModel(3);
            CarModel2 = new CarModel(2);
            CarModel1 = new CarModel(1);

            AllCarModels = new ObservableCollection<CarModel>();
            AllCarModels.Add(CarModel0); 
            AllCarModels.Add(CarModel7);
            AllCarModels.Add(CarModel6);
            AllCarModels.Add(CarModel5);
            AllCarModels.Add(CarModel4);
            AllCarModels.Add(CarModel3);
            AllCarModels.Add(CarModel2);
            AllCarModels.Add(CarModel1);
        }
        
        /// <summary>
        /// MC00车激活状态
        /// </summary>
        public VOBCState MC00
        {
            get { return m_MC00; }
            set
            {
                if (value == m_MC00)
                {
                    return;
                }
                m_MC00 = value;
                RaisePropertyChanged(() => MC00);
            }
        }

        /// <summary>
        /// MC01车激活状态
        /// </summary>
        public VOBCState MC01
        {
            get { return m_MC01; }
            set
            {
                if (value == m_MC01)
                {
                    return;
                }
                m_MC01 = value;
                RaisePropertyChanged(() => MC01);
            }
        }

        public ObservableCollection<CarModel> AllCarModels;

        /// <summary>
        /// 车辆0
        /// </summary>
        public CarModel CarModel0 { get; set; }

        /// <summary>
        /// 车辆7
        /// </summary>
        public CarModel CarModel7 { get; set; }

        /// <summary>
        /// 车辆6
        /// </summary>
        public CarModel CarModel6 { get; set; }

        /// <summary>
        /// 车辆5
        /// </summary>
        public CarModel CarModel5 { get; set; }

        /// <summary>
        /// 车辆4
        /// </summary>
        public CarModel CarModel4 { get; set; }

        /// <summary>
        /// 车辆3
        /// </summary>
        public CarModel CarModel3 { get; set; }

        /// <summary>
        /// 车辆2
        /// </summary>
        public CarModel CarModel2 { get; set; }

        /// <summary>
        /// 车辆1
        /// </summary>
        public CarModel CarModel1 { get; set; }

        /// <summary>
        /// 车辆点击命令
        /// </summary>
        public ICommand TrainCheckedCommand { get; set; }
    }
}