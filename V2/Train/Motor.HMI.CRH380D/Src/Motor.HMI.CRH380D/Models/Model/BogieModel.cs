using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Motor.HMI.CRH380D.Controller;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class BogieModel : ModelBase
    {
        private float m_BogieTemperature0;
        private float m_BogieTemperature7;
        private float m_BogieTemperature6;
        private float m_BogieTemperature5;
        private float m_BogieTemperature4;
        private float m_BogieTemperature3;
        private float m_BogieTemperature2;
        private float m_BogieTemperature1;
        private OneBogieModel m_BogieDisplay;
        private bool m_BogieVisibility; 
        private ObservableCollection<OneBogieModel> m_AllBogieModels;
        
        [ImportingConstructor]
        public BogieModel(BogieController bogieController, TrainModel trainModel)
        {
            TrainModel = trainModel;

            BogieModel0 = new OneBogieModel(0);
            BogieModel1 = new OneBogieModel(1);
            BogieModel2 = new OneBogieModel(2);
            BogieModel3 = new OneBogieModel(3);
            BogieModel4 = new OneBogieModel(4);
            BogieModel5 = new OneBogieModel(5);
            BogieModel6 = new OneBogieModel(6);
            BogieModel7 = new OneBogieModel(7);
            BogieDisplay = new OneBogieModel(0);

            AllBogieModels = new ObservableCollection<OneBogieModel>();
            AllBogieModels.Add(BogieModel0);
            AllBogieModels.Add(BogieModel1);
            AllBogieModels.Add(BogieModel2);
            AllBogieModels.Add(BogieModel3);
            AllBogieModels.Add(BogieModel4);
            AllBogieModels.Add(BogieModel5);
            AllBogieModels.Add(BogieModel6);AllBogieModels.Add(BogieModel7);


            BogieController = bogieController;
            BogieController.ViewModel = this;
            BogieController.Initalize();
        }

        /// <summary>
        /// 控制
        /// </summary>
        public BogieController BogieController { get; set; }
        
        /// <summary>
        /// 车辆集合
        /// </summary>
        public TrainModel TrainModel { get; private set; }

        /// <summary>
        /// 转向架信息集合
        /// </summary>
        public ObservableCollection<OneBogieModel> AllBogieModels
        {
            get { return m_AllBogieModels; }
            set
            {
                if (Equals(value, m_AllBogieModels))
                {
                    return;
                }
                m_AllBogieModels = value;
                RaisePropertyChanged(() => AllBogieModels);
                RaisePropertyChanged(() => BogieModel0);
                RaisePropertyChanged(() => BogieModel1);
                RaisePropertyChanged(() => BogieModel2);
                RaisePropertyChanged(() => BogieModel3);
                RaisePropertyChanged(() => BogieModel4);
                RaisePropertyChanged(() => BogieModel5);
                RaisePropertyChanged(() => BogieModel6);
                RaisePropertyChanged(() => BogieModel7);
                if (BogieController != null)
                {
                    BogieController.Change();
                }
            }
        }

        /// <summary>
        /// 车0转向架信息
        /// </summary>
        public OneBogieModel BogieModel0 { get; set; }

        /// <summary>
        /// 车1转向架信息
        /// </summary>
        public OneBogieModel BogieModel1 { get; set; }

        /// <summary>
        /// 车2转向架信息
        /// </summary>
        public OneBogieModel BogieModel2 { get; set; }

        /// <summary>
        /// 车3转向架信息
        /// </summary>
        public OneBogieModel BogieModel3 { get; set; }

        /// <summary>
        /// 车4转向架信息
        /// </summary>
        public OneBogieModel BogieModel4 { get; set; }

        /// <summary>
        /// 车5转向架信息
        /// </summary>
        public OneBogieModel BogieModel5 { get; set; }

        /// <summary>
        /// 车6转向架信息
        /// </summary>
        public OneBogieModel BogieModel6 { get; set; }

        /// <summary>
        /// 车7转向架信息
        /// </summary>
        public OneBogieModel BogieModel7 { get; set; }


        /// <summary>
        /// 需要显示的转向架详细信息
        /// </summary>
        public OneBogieModel BogieDisplay
        {
            get { return m_BogieDisplay; }
            set
            {
                if (Equals(value,m_BogieDisplay))
                {
                    return;
                }
                m_BogieDisplay = value;
                RaisePropertyChanged(()=>BogieDisplay);
            }
        }

        /// <summary>
        /// 车0转向架温度
        /// </summary>
        public float BogieTemperature0
        {
            get { return m_BogieTemperature0; }
            set {
                if (value == m_BogieTemperature0)
                {
                    return;
                }
                m_BogieTemperature0 = value;
                RaisePropertyChanged(() => BogieTemperature0);
            }
        }

        /// <summary>
        /// 车7转向架温度
        /// </summary>
        public float BogieTemperature7
        {
            get { return m_BogieTemperature7; }
            set
            {
                if (value == m_BogieTemperature7)
                {
                    return;
                }
                m_BogieTemperature7 = value;
                RaisePropertyChanged(() => BogieTemperature7);
            }
        }

        /// <summary>
        /// 车6转向架温度
        /// </summary>
        public float BogieTemperature6
        {
            get { return m_BogieTemperature6; }
            set
            {
                if (value == m_BogieTemperature6)
                {
                    return;
                }
                m_BogieTemperature6 = value;
                RaisePropertyChanged(() => BogieTemperature6);
            }
        }

        /// <summary>
        /// 车5转向架温度
        /// </summary>
        public float BogieTemperature5
        {
            get { return m_BogieTemperature5; }
            set
            {
                if (value == m_BogieTemperature5)
                {
                    return;
                }
                m_BogieTemperature5 = value;
                RaisePropertyChanged(() => BogieTemperature5);
            }
        }

        /// <summary>
        /// 车4转向架温度
        /// </summary>
        public float BogieTemperature4
        {
            get { return m_BogieTemperature4; }
            set
            {
                if (value == m_BogieTemperature4)
                {
                    return;
                }
                m_BogieTemperature4 = value;
                RaisePropertyChanged(() => BogieTemperature4);
            }
        }

        /// <summary>
        /// 车3转向架温度
        /// </summary>
        public float BogieTemperature3
        {
            get { return m_BogieTemperature3; }
            set
            {
                if (value == m_BogieTemperature3)
                {
                    return;
                }
                m_BogieTemperature3 = value;
                RaisePropertyChanged(() => BogieTemperature3);
            }
        }

        /// <summary>
        /// 车2转向架温度
        /// </summary>
        public float BogieTemperature2
        {
            get { return m_BogieTemperature2; }
            set
            {
                if (value == m_BogieTemperature2)
                {
                    return;
                }
                m_BogieTemperature2 = value;
                RaisePropertyChanged(() => BogieTemperature2);
            }
        }

        /// <summary>
        /// 车1转向架温度
        /// </summary>
        public float BogieTemperature1
        {
            get { return m_BogieTemperature1; }
            set
            {
                if (value == m_BogieTemperature1)
                {
                    return;
                }
                m_BogieTemperature1 = value;
                RaisePropertyChanged(() => BogieTemperature1);
            }
        }

        /// <summary>
        /// 转向架详细信息是否显示
        /// </summary>
        public bool BogieVisibility
        {
            get { return m_BogieVisibility; }
            set
            {
                if (value == m_BogieVisibility)
                {
                    return;
                }
                m_BogieVisibility = value;
                RaisePropertyChanged(() => BogieVisibility);
            }
        }
    }
}