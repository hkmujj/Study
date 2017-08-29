using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Motor.HMI.CRH380D.Controller;
using Motor.HMI.CRH380D.Models.Units;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class FireDeviceModel : ModelBase
    {
        private ObservableCollection<FireDeviceUnit> m_LCMUnits;

        [ImportingConstructor]
        public FireDeviceModel(FireDeviceController fireDeviceController)
        {
            TrainModel = new TrainModel();


            FireDeviceUnits = new ObservableCollection<FireDeviceUnit>(GlobalParam.Instance.FireDeviceUnits.OrderBy(o => o.Num));

            FireDeviceController = fireDeviceController;
            FireDeviceController.ViewModel = this;
            FireDeviceController.Initalize();
        }

        /// <summary>
        /// 控制
        /// </summary>
        public FireDeviceController FireDeviceController { get; set; }
        
        /// <summary>
        /// 车辆集合
        /// </summary>
        public TrainModel TrainModel { get; private set; }

        /// <summary>
        /// 火灾探测器单元
        /// </summary>
        public ObservableCollection<FireDeviceUnit> FireDeviceUnits
        {
            get { return m_LCMUnits; }
            private set
            {
                if (Equals(value, m_LCMUnits))
                {
                    return;
                }
                m_LCMUnits = value;
                RaisePropertyChanged(() => FireDeviceUnits);
                RaisePropertyChanged(() => FireDevice00);
                RaisePropertyChanged(() => FireDevice01);
                RaisePropertyChanged(() => FireDevice02);
                RaisePropertyChanged(() => FireDevice03);
                RaisePropertyChanged(() => FireDevice04);
                RaisePropertyChanged(() => FireDevice05);
                RaisePropertyChanged(() => FireDevice06);
                RaisePropertyChanged(() => FireDevice07);
                RaisePropertyChanged(() => FireDevice08);
                RaisePropertyChanged(() => FireDevice09);
                RaisePropertyChanged(() => FireDevice10);
                RaisePropertyChanged(() => FireDevice11);
                RaisePropertyChanged(() => FireDevice12);
                RaisePropertyChanged(() => FireDevice13);
                RaisePropertyChanged(() => FireDevice14);
                RaisePropertyChanged(() => FireDevice15);
                RaisePropertyChanged(() => FireDevice16);
                RaisePropertyChanged(() => FireDevice17);
                RaisePropertyChanged(() => FireDevice18);
                RaisePropertyChanged(() => FireDevice19);
                RaisePropertyChanged(() => FireDevice20);
                RaisePropertyChanged(() => FireDevice21);
                RaisePropertyChanged(() => FireDevice22);
                RaisePropertyChanged(() => FireDevice23);
                RaisePropertyChanged(() => FireDevice24);
                RaisePropertyChanged(() => FireDevice25);
                RaisePropertyChanged(() => FireDevice26);
                RaisePropertyChanged(() => FireDevice27);
                RaisePropertyChanged(() => FireDevice28);
                RaisePropertyChanged(() => FireDevice29);
                RaisePropertyChanged(() => FireDevice30);
                RaisePropertyChanged(() => FireDevice31);
                RaisePropertyChanged(() => FireDevice32);
                RaisePropertyChanged(() => FireDevice33);
                RaisePropertyChanged(() => FireDevice34);
                RaisePropertyChanged(() => FireDevice35);
                RaisePropertyChanged(() => FireDevice36);
                RaisePropertyChanged(() => FireDevice37);
                RaisePropertyChanged(() => FireDevice38);
                RaisePropertyChanged(() => FireDevice39);
                RaisePropertyChanged(() => FireDevice40);
                RaisePropertyChanged(() => FireDevice41);
                RaisePropertyChanged(() => FireDevice42);
                RaisePropertyChanged(() => FireDevice43);
                RaisePropertyChanged(() => FireDevice44);
                RaisePropertyChanged(() => FireDevice45);
                RaisePropertyChanged(() => FireDevice46);
                RaisePropertyChanged(() => FireDevice47);
                RaisePropertyChanged(() => FireDevice48);
                RaisePropertyChanged(() => FireDevice49);
                RaisePropertyChanged(() => FireDevice50);
                RaisePropertyChanged(() => FireDevice51);
                RaisePropertyChanged(() => FireDevice52);
                RaisePropertyChanged(() => FireDevice53);
                RaisePropertyChanged(() => FireDevice54);
                RaisePropertyChanged(() => FireDevice55);
                RaisePropertyChanged(() => FireDevice56);
                RaisePropertyChanged(() => FireDevice57);
                RaisePropertyChanged(() => FireDevice58);
                RaisePropertyChanged(() => FireDevice59);
                RaisePropertyChanged(() => FireDevice60);
                RaisePropertyChanged(() => FireDevice61);
                RaisePropertyChanged(() => FireDevice62);
                RaisePropertyChanged(() => FireDevice63);
                RaisePropertyChanged(() => FireDevice64);
                RaisePropertyChanged(() => FireDevice65);
                RaisePropertyChanged(() => FireDevice66);
                RaisePropertyChanged(() => FireDevice67);
                RaisePropertyChanged(() => FireDevice68);
                RaisePropertyChanged(() => FireDevice69);
                RaisePropertyChanged(() => FireDevice70);
                RaisePropertyChanged(() => FireDevice71);
                RaisePropertyChanged(() => FireDevice72);
                RaisePropertyChanged(() => FireDevice73);
                RaisePropertyChanged(() => FireDevice74);
                RaisePropertyChanged(() => FireDevice75);
                RaisePropertyChanged(() => FireDevice76);
                RaisePropertyChanged(() => FireDevice77);
                RaisePropertyChanged(() => FireDevice78);
                RaisePropertyChanged(() => FireDevice79);
            }
        }
        /// <summary>
        /// 车0位置0发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice00 { get { return FireDeviceUnits.Where(w => w.Car == 0 && w.Location == 0).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车0位置1发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice01 { get { return FireDeviceUnits.Where(w => w.Car == 0 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车0位置2发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice02 { get { return FireDeviceUnits.Where(w => w.Car == 0 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车0位置3发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice03 { get { return FireDeviceUnits.Where(w => w.Car == 0 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车0位置4发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice04 { get { return FireDeviceUnits.Where(w => w.Car == 0 && w.Location == 4).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车0位置5发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice05 { get { return FireDeviceUnits.Where(w => w.Car == 0 && w.Location == 5).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车0位置6发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice06 { get { return FireDeviceUnits.Where(w => w.Car == 0 && w.Location == 6).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车0位置7发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice07 { get { return FireDeviceUnits.Where(w => w.Car == 0 && w.Location == 7).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车0位置8发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice08 { get { return FireDeviceUnits.Where(w => w.Car == 0 && w.Location == 8).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车0位置9发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice09 { get { return FireDeviceUnits.Where(w => w.Car == 0 && w.Location == 9).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车1位置0发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice10 { get { return FireDeviceUnits.Where(w => w.Car == 1 && w.Location == 0).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车1位置1发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice11 { get { return FireDeviceUnits.Where(w => w.Car == 1 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车1位置2发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice12 { get { return FireDeviceUnits.Where(w => w.Car == 1 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车1位置3发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice13 { get { return FireDeviceUnits.Where(w => w.Car == 1 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车1位置4发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice14 { get { return FireDeviceUnits.Where(w => w.Car == 1 && w.Location == 4).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车1位置5发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice15 { get { return FireDeviceUnits.Where(w => w.Car == 1 && w.Location == 5).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车1位置6发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice16 { get { return FireDeviceUnits.Where(w => w.Car == 1 && w.Location == 6).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车1位置7发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice17 { get { return FireDeviceUnits.Where(w => w.Car == 1 && w.Location == 7).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车1位置8发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice18 { get { return FireDeviceUnits.Where(w => w.Car == 1 && w.Location == 8).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车1位置9发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice19 { get { return FireDeviceUnits.Where(w => w.Car == 1 && w.Location == 9).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车2位置0发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice20 { get { return FireDeviceUnits.Where(w => w.Car == 2 && w.Location == 0).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车2位置1发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice21 { get { return FireDeviceUnits.Where(w => w.Car == 2 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车2位置2发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice22 { get { return FireDeviceUnits.Where(w => w.Car == 2 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车2位置3发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice23 { get { return FireDeviceUnits.Where(w => w.Car == 2 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车2位置4发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice24 { get { return FireDeviceUnits.Where(w => w.Car == 2 && w.Location == 4).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车2位置5发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice25 { get { return FireDeviceUnits.Where(w => w.Car == 2 && w.Location == 5).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车2位置6发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice26 { get { return FireDeviceUnits.Where(w => w.Car == 2 && w.Location == 6).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车2位置7发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice27 { get { return FireDeviceUnits.Where(w => w.Car == 2 && w.Location == 7).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车2位置8发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice28 { get { return FireDeviceUnits.Where(w => w.Car == 2 && w.Location == 8).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车2位置9发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice29 { get { return FireDeviceUnits.Where(w => w.Car == 2 && w.Location == 9).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车3位置0发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice30 { get { return FireDeviceUnits.Where(w => w.Car == 3 && w.Location == 0).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车3位置1发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice31 { get { return FireDeviceUnits.Where(w => w.Car == 3 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车3位置2发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice32 { get { return FireDeviceUnits.Where(w => w.Car == 3 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车3位置3发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice33 { get { return FireDeviceUnits.Where(w => w.Car == 3 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车3位置4发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice34 { get { return FireDeviceUnits.Where(w => w.Car == 3 && w.Location == 4).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车3位置5发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice35 { get { return FireDeviceUnits.Where(w => w.Car == 3 && w.Location == 5).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车3位置6发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice36 { get { return FireDeviceUnits.Where(w => w.Car == 3 && w.Location == 6).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车3位置7发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice37 { get { return FireDeviceUnits.Where(w => w.Car == 3 && w.Location == 7).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车3位置8发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice38 { get { return FireDeviceUnits.Where(w => w.Car == 3 && w.Location == 8).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车3位置9发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice39 { get { return FireDeviceUnits.Where(w => w.Car == 3 && w.Location == 9).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车4位置0发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice40 { get { return FireDeviceUnits.Where(w => w.Car == 4 && w.Location == 0).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车4位置1发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice41 { get { return FireDeviceUnits.Where(w => w.Car == 4 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车4位置2发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice42 { get { return FireDeviceUnits.Where(w => w.Car == 4 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车4位置3发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice43 { get { return FireDeviceUnits.Where(w => w.Car == 4 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车4位置4发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice44 { get { return FireDeviceUnits.Where(w => w.Car == 4 && w.Location == 4).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车4位置5发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice45 { get { return FireDeviceUnits.Where(w => w.Car == 4 && w.Location == 5).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车4位置6发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice46 { get { return FireDeviceUnits.Where(w => w.Car == 4 && w.Location == 6).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车4位置7发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice47 { get { return FireDeviceUnits.Where(w => w.Car == 4 && w.Location == 7).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车4位置8发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice48 { get { return FireDeviceUnits.Where(w => w.Car == 4 && w.Location == 8).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车4位置9发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice49 { get { return FireDeviceUnits.Where(w => w.Car == 4 && w.Location == 9).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车5位置0发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice50 { get { return FireDeviceUnits.Where(w => w.Car == 5 && w.Location == 0).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车5位置1发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice51 { get { return FireDeviceUnits.Where(w => w.Car == 5 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车5位置2发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice52 { get { return FireDeviceUnits.Where(w => w.Car == 5 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车5位置3发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice53 { get { return FireDeviceUnits.Where(w => w.Car == 5 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车5位置4发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice54 { get { return FireDeviceUnits.Where(w => w.Car == 5 && w.Location == 4).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车5位置5发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice55 { get { return FireDeviceUnits.Where(w => w.Car == 5 && w.Location == 5).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车5位置6发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice56 { get { return FireDeviceUnits.Where(w => w.Car == 5 && w.Location == 6).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车5位置7发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice57 { get { return FireDeviceUnits.Where(w => w.Car == 5 && w.Location == 7).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车5位置8发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice58 { get { return FireDeviceUnits.Where(w => w.Car == 5 && w.Location == 8).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车5位置9发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice59 { get { return FireDeviceUnits.Where(w => w.Car == 5 && w.Location == 9).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车6位置0发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice60 { get { return FireDeviceUnits.Where(w => w.Car == 6 && w.Location == 0).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车6位置1发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice61 { get { return FireDeviceUnits.Where(w => w.Car == 6 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车6位置2发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice62 { get { return FireDeviceUnits.Where(w => w.Car == 6 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车6位置3发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice63 { get { return FireDeviceUnits.Where(w => w.Car == 6 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车6位置4发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice64 { get { return FireDeviceUnits.Where(w => w.Car == 6 && w.Location == 4).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车6位置5发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice65 { get { return FireDeviceUnits.Where(w => w.Car == 6 && w.Location == 5).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车6位置6发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice66 { get { return FireDeviceUnits.Where(w => w.Car == 6 && w.Location == 6).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车6位置7发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice67 { get { return FireDeviceUnits.Where(w => w.Car == 6 && w.Location == 7).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车6位置8发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice68 { get { return FireDeviceUnits.Where(w => w.Car == 6 && w.Location == 8).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车6位置9发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice69 { get { return FireDeviceUnits.Where(w => w.Car == 6 && w.Location == 9).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车7位置0发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice70 { get { return FireDeviceUnits.Where(w => w.Car == 7 && w.Location == 0).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车7位置1发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice71 { get { return FireDeviceUnits.Where(w => w.Car == 7 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车7位置2发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice72 { get { return FireDeviceUnits.Where(w => w.Car == 7 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车7位置3发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice73 { get { return FireDeviceUnits.Where(w => w.Car == 7 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车7位置4发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice74 { get { return FireDeviceUnits.Where(w => w.Car == 7 && w.Location == 4).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车7位置5发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice75 { get { return FireDeviceUnits.Where(w => w.Car == 7 && w.Location == 5).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车7位置6发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice76 { get { return FireDeviceUnits.Where(w => w.Car == 7 && w.Location == 6).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车7位置7发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice77 { get { return FireDeviceUnits.Where(w => w.Car == 7 && w.Location == 7).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车7位置8发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice78 { get { return FireDeviceUnits.Where(w => w.Car == 7 && w.Location == 8).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车7位置9发生火灾
        /// </summary>
        public FireDeviceUnit FireDevice79 { get { return FireDeviceUnits.Where(w => w.Car == 7 && w.Location == 9).OrderBy(o => o.Location).FirstOrDefault(); } }
    }
}