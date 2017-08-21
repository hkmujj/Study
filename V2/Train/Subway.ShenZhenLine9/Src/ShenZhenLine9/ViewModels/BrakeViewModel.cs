using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Subway.ShenZhenLine9.Controller.ViewModelController;
using Subway.ShenZhenLine9.Models;
using Subway.ShenZhenLine9.Models.Units;

namespace Subway.ShenZhenLine9.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    public class BrakeViewModel : ViewModelBase
    {
        private bool m_IsCar6Numbe2Cut;
        private bool m_IsCar6Numbe1Cut;
        private bool m_IsCar5Numbe2Cut;
        private bool m_IsCar5Numbe1Cut;
        private bool m_IsCar4Numbe2Cut;
        private bool m_IsCar4Numbe1Cut;
        private bool m_IsCar3Numbe2Cut;
        private bool m_IsCar3Numbe1Cut;
        private bool m_IsCar2Numbe2Cut;
        private bool m_IsCar2Numbe1Cut;
        private bool m_IsCar1Numbe2Cut;
        private bool m_IsCar1Numbe1Cut;
        private float m_Car6BrakeCylinder;
        private float m_Car5BrakeCylinder;
        private float m_Car4BrakeCylinder;
        private float m_Car3BrakeCylinder;
        private float m_Car2BrakeCylinder;
        private float m_Car1BrakeCylinder;

        /// <summary>
        /// 
        /// </summary>
        [ImportingConstructor]
        public BrakeViewModel(BrakeViewController controller)
        {
            Controller = controller;
            controller.ViewModel = this;
            BrakeUnits = new ObservableCollection<BrakeUnit>(GlobalParam.Instance.BrakeUnits);
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<BrakeUnit> BrakeUnits { get; }

        public BrakeViewController Controller { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> Car1Top { get { return BrakeUnits.Where(w => w.Car == 1 && w.Location <= 2); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> Car2Top { get { return BrakeUnits.Where(w => w.Car == 2 && w.Location <= 2); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> Car3Top { get { return BrakeUnits.Where(w => w.Car == 3 && w.Location <= 2); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> Car4Top { get { return BrakeUnits.Where(w => w.Car == 4 && w.Location <= 2); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> Car5Top { get { return BrakeUnits.Where(w => w.Car == 5 && w.Location <= 2); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> Car6Top { get { return BrakeUnits.Where(w => w.Car == 6 && w.Location <= 2); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> Car1Buttom { get { return BrakeUnits.Where(w => w.Car == 1 && w.Location != 1); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> Car2Buttom { get { return BrakeUnits.Where(w => w.Car == 2 && w.Location != 1); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> Car3Buttom { get { return BrakeUnits.Where(w => w.Car == 3 && w.Location != 1); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> Car4Buttom { get { return BrakeUnits.Where(w => w.Car == 4 && w.Location != 1); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> Car5Buttom { get { return BrakeUnits.Where(w => w.Car == 5 && w.Location != 1); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> Car6Buttom { get { return BrakeUnits.Where(w => w.Car == 6 && w.Location != 1); } }

        /// <summary>
        /// 转向架切除
        /// </summary>
        public bool IsCar1Numbe1Cut
        {
            get { return m_IsCar1Numbe1Cut; }
            set
            {
                if (value == m_IsCar1Numbe1Cut)
                {
                    return;
                }
                m_IsCar1Numbe1Cut = value;
                RaisePropertyChanged(() => IsCar1Numbe1Cut);
            }
        }

        /// <summary>
        /// 转向架切除
        /// </summary>
        public bool IsCar1Numbe2Cut
        {
            get { return m_IsCar1Numbe2Cut; }
            set
            {
                if (value == m_IsCar1Numbe2Cut)
                {
                    return;
                }
                m_IsCar1Numbe2Cut = value;
                RaisePropertyChanged(() => IsCar1Numbe2Cut);
            }
        }

        /// <summary>
        /// 转向架切除
        /// </summary>
        public bool IsCar2Numbe1Cut
        {
            get { return m_IsCar2Numbe1Cut; }
            set
            {
                if (value == m_IsCar2Numbe1Cut)
                {
                    return;
                }
                m_IsCar2Numbe1Cut = value;
                RaisePropertyChanged(() => IsCar2Numbe1Cut);
            }
        }

        /// <summary>
        /// 转向架切除
        /// </summary>
        public bool IsCar2Numbe2Cut
        {
            get { return m_IsCar2Numbe2Cut; }
            set
            {
                if (value == m_IsCar2Numbe2Cut)
                {
                    return;
                }
                m_IsCar2Numbe2Cut = value;
                RaisePropertyChanged(() => IsCar2Numbe2Cut);
            }
        }

        /// <summary>
        /// 转向架切除
        /// </summary>
        public bool IsCar3Numbe1Cut
        {
            get { return m_IsCar3Numbe1Cut; }
            set
            {
                if (value == m_IsCar3Numbe1Cut)
                {
                    return;
                }
                m_IsCar3Numbe1Cut = value;
                RaisePropertyChanged(() => IsCar3Numbe1Cut);
            }
        }

        /// <summary>
        /// 转向架切除
        /// </summary>
        public bool IsCar3Numbe2Cut
        {
            get { return m_IsCar3Numbe2Cut; }
            set
            {
                if (value == m_IsCar3Numbe2Cut)
                {
                    return;
                }
                m_IsCar3Numbe2Cut = value;
                RaisePropertyChanged(() => IsCar3Numbe2Cut);
            }
        }

        /// <summary>
        /// 转向架切除
        /// </summary>
        public bool IsCar4Numbe1Cut
        {
            get { return m_IsCar4Numbe1Cut; }
            set
            {
                if (value == m_IsCar4Numbe1Cut)
                {
                    return;
                }
                m_IsCar4Numbe1Cut = value;
                RaisePropertyChanged(() => IsCar4Numbe1Cut);
            }
        }

        /// <summary>
        /// 转向架切除
        /// </summary>
        public bool IsCar4Numbe2Cut
        {
            get { return m_IsCar4Numbe2Cut; }
            set
            {
                if (value == m_IsCar4Numbe2Cut)
                {
                    return;
                }
                m_IsCar4Numbe2Cut = value;
                RaisePropertyChanged(() => IsCar4Numbe2Cut);
            }
        }

        /// <summary>
        /// 转向架切除
        /// </summary>
        public bool IsCar5Numbe1Cut
        {
            get { return m_IsCar5Numbe1Cut; }
            set
            {
                if (value == m_IsCar5Numbe1Cut)
                {
                    return;
                }
                m_IsCar5Numbe1Cut = value;
                RaisePropertyChanged(() => IsCar5Numbe1Cut);
            }
        }

        /// <summary>
        /// 转向架切除
        /// </summary>
        public bool IsCar5Numbe2Cut
        {
            get { return m_IsCar5Numbe2Cut; }
            set
            {
                if (value == m_IsCar5Numbe2Cut)
                {
                    return;
                }
                m_IsCar5Numbe2Cut = value;
                RaisePropertyChanged(() => IsCar5Numbe2Cut);
            }
        }

        /// <summary>
        /// 转向架切除
        /// </summary>
        public bool IsCar6Numbe1Cut
        {
            get { return m_IsCar6Numbe1Cut; }
            set
            {
                if (value == m_IsCar6Numbe1Cut)
                {
                    return;
                }
                m_IsCar6Numbe1Cut = value;
                RaisePropertyChanged(() => IsCar6Numbe1Cut);
            }
        }

        /// <summary>
        /// 转向架切除
        /// </summary>
        public bool IsCar6Numbe2Cut
        {
            get { return m_IsCar6Numbe2Cut; }
            set
            {
                if (value == m_IsCar6Numbe2Cut)
                {
                    return;
                }
                m_IsCar6Numbe2Cut = value;
                RaisePropertyChanged(() => IsCar6Numbe2Cut);
            }
        }

        /// <summary>
        /// 车1制动缸
        /// </summary>
        public float Car1BrakeCylinder
        {
            get { return m_Car1BrakeCylinder; }
            set
            {
                if (value.Equals(m_Car1BrakeCylinder))
                    return;

                m_Car1BrakeCylinder = value;
                RaisePropertyChanged(() => Car1BrakeCylinder);
            }
        }

        /// <summary>
        /// 车2制动缸
        /// </summary>
        public float Car2BrakeCylinder
        {
            get { return m_Car2BrakeCylinder; }
            set
            {
                if (value.Equals(m_Car2BrakeCylinder))
                    return;

                m_Car2BrakeCylinder = value;
                RaisePropertyChanged(() => Car2BrakeCylinder);
            }
        }

        /// <summary>
        /// 车3制动缸
        /// </summary>
        public float Car3BrakeCylinder
        {
            get { return m_Car3BrakeCylinder; }
            set
            {
                if (value.Equals(m_Car3BrakeCylinder))
                    return;

                m_Car3BrakeCylinder = value;
                RaisePropertyChanged(() => Car3BrakeCylinder);
            }
        }

        /// <summary>
        /// 车4制动缸
        /// </summary>
        public float Car4BrakeCylinder
        {
            get { return m_Car4BrakeCylinder; }
            set
            {
                if (value.Equals(m_Car4BrakeCylinder))
                    return;

                m_Car4BrakeCylinder = value;
                RaisePropertyChanged(() => Car4BrakeCylinder);
            }
        }

        /// <summary>
        /// 车5制动缸
        /// </summary>
        public float Car5BrakeCylinder
        {
            get { return m_Car5BrakeCylinder; }
            set
            {
                if (value.Equals(m_Car5BrakeCylinder))
                    return;

                m_Car5BrakeCylinder = value;
                RaisePropertyChanged(() => Car5BrakeCylinder);
            }
        }

        /// <summary>
        /// 车6制动缸
        /// </summary>
        public float Car6BrakeCylinder
        {
            get { return m_Car6BrakeCylinder; }
            set
            {
                if (value.Equals(m_Car6BrakeCylinder))
                    return;

                m_Car6BrakeCylinder = value;
                RaisePropertyChanged(() => Car6BrakeCylinder);
            }
        }
    }
}