using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using LightRail.HMI.SZLHLF.Model;
using Motor.HMI.CRH380D.Controller;
using Motor.HMI.CRH380D.Models.Units;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class DoorModel : ModelBase
    {
        private ObservableCollection<DoorUnit> m_DoorUnits;

        [ImportingConstructor]
        public DoorModel(DoorController doorController)
        {
            DoorUnits = new ObservableCollection<DoorUnit>(GlobalParam.Instance.DoorUnits.OrderBy(o => o.Num));

            DoorController = doorController;
            DoorController.ViewModel = new Lazy<DoorModel>(()=>this);
            DoorController.Initalize();
        }

       public DoorController DoorController { get; private set; }

        /// <summary>
        /// 所有门单元
        /// </summary>
        public ObservableCollection<DoorUnit> DoorUnits
        {
            get { return m_DoorUnits; }
            private set
            {
                if (Equals(value, m_DoorUnits))
                {
                    return;
                }
                m_DoorUnits = value;
                RaisePropertyChanged(() => DoorUnits);
                RaisePropertyChanged(() => Car0Door1);
                RaisePropertyChanged(() => Car0Door3);
                RaisePropertyChanged(() => Car1Door1);
                RaisePropertyChanged(() => Car1Door3);
                RaisePropertyChanged(() => Car2Door1);
                RaisePropertyChanged(() => Car2Door2);
                RaisePropertyChanged(() => Car2Door3);
                RaisePropertyChanged(() => Car2Door4);
                RaisePropertyChanged(() => Car3Door1);
                RaisePropertyChanged(() => Car3Door2);
                RaisePropertyChanged(() => Car3Door3);
                RaisePropertyChanged(() => Car3Door4);
                RaisePropertyChanged(() => Car4Door1);
                RaisePropertyChanged(() => Car4Door3);
                RaisePropertyChanged(() => Car5Door1);
                RaisePropertyChanged(() => Car5Door2);
                RaisePropertyChanged(() => Car5Door3);
                RaisePropertyChanged(() => Car5Door4);
                RaisePropertyChanged(() => Car6Door1);
                RaisePropertyChanged(() => Car6Door2);
                RaisePropertyChanged(() => Car6Door3);
                RaisePropertyChanged(() => Car6Door4);
                RaisePropertyChanged(() => Car7Door1);
                RaisePropertyChanged(() => Car7Door2);
                RaisePropertyChanged(() => Car7Door3);
                RaisePropertyChanged(() => Car7Door4);
            }
        }
        /// <summary>
        /// 0车1门
        /// </summary>
        public DoorUnit Car0Door1 { get {return DoorUnits.Where(w => w.Car == 0 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 0车3门
        /// </summary>
        public DoorUnit Car0Door3 { get { return DoorUnits.Where(w => w.Car == 0 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 1车1门
        /// </summary>
        public DoorUnit Car1Door1 { get { return DoorUnits.Where(w => w.Car == 1 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 1车3门
        /// </summary>
        public DoorUnit Car1Door3 { get { return DoorUnits.Where(w => w.Car == 1 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 2车1门
        /// </summary>
        public DoorUnit Car2Door1 { get { return DoorUnits.Where(w => w.Car == 2 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 2车2门
        /// </summary>
        public DoorUnit Car2Door2 { get { return DoorUnits.Where(w => w.Car == 2 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 2车3门
        /// </summary>
        public DoorUnit Car2Door3 { get { return DoorUnits.Where(w => w.Car == 2 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 2车4门
        /// </summary>
        public DoorUnit Car2Door4 { get { return DoorUnits.Where(w => w.Car == 2 && w.Location == 4).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 3车1门
        /// </summary>
        public DoorUnit Car3Door1 { get { return DoorUnits.Where(w => w.Car == 3 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 3车2门
        /// </summary>
        public DoorUnit Car3Door2 { get { return DoorUnits.Where(w => w.Car == 3 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 3车3门
        /// </summary>
        public DoorUnit Car3Door3 { get { return DoorUnits.Where(w => w.Car == 3 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 3车4门
        /// </summary>
        public DoorUnit Car3Door4 { get { return DoorUnits.Where(w => w.Car == 3 && w.Location == 4).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 4车1门
        /// </summary>
        public DoorUnit Car4Door1 { get { return DoorUnits.Where(w => w.Car == 4 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        
        /// <summary>
        /// 4车3门
        /// </summary>
        public DoorUnit Car4Door3 { get { return DoorUnits.Where(w => w.Car == 4 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }
        
        /// <summary>
        /// 5车1门
        /// </summary>
        public DoorUnit Car5Door1 { get { return DoorUnits.Where(w => w.Car == 5 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 5车2门
        /// </summary>
        public DoorUnit Car5Door2 { get { return DoorUnits.Where(w => w.Car == 5 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 5车3门
        /// </summary>
        public DoorUnit Car5Door3 { get { return DoorUnits.Where(w => w.Car == 5 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 5车4门
        /// </summary>
        public DoorUnit Car5Door4 { get { return DoorUnits.Where(w => w.Car == 5 && w.Location == 4).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 6车1门
        /// </summary>
        public DoorUnit Car6Door1 { get { return DoorUnits.Where(w => w.Car == 6 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 6车2门
        /// </summary>
        public DoorUnit Car6Door2 { get { return DoorUnits.Where(w => w.Car == 6 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 6车3门
        /// </summary>
        public DoorUnit Car6Door3 { get { return DoorUnits.Where(w => w.Car == 6 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 6车4门
        /// </summary>
        public DoorUnit Car6Door4 { get { return DoorUnits.Where(w => w.Car == 6 && w.Location == 4).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 7车1门
        /// </summary>
        public DoorUnit Car7Door1 { get { return DoorUnits.Where(w => w.Car == 7 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 7车2门
        /// </summary>
        public DoorUnit Car7Door2 { get { return DoorUnits.Where(w => w.Car == 7 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 7车3门
        /// </summary>
        public DoorUnit Car7Door3 { get { return DoorUnits.Where(w => w.Car == 7 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 7车4门
        /// </summary>
        public DoorUnit Car7Door4 { get { return DoorUnits.Where(w => w.Car == 7 && w.Location == 4).OrderBy(o => o.Location).FirstOrDefault(); } }
    }
}