using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using LightRail.HMI.GZYGDC.Model.Units;
using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.GZYGDC.Model
{
    [Export]
    public class NetTopologyModel : NotificationObject
    {
        private ObservableCollection<NetTopologyUnit> m_NetTopologyUnits;


        public NetTopologyModel()
        {

            NetTopologyUnits = new ObservableCollection<NetTopologyUnit>(GlobalParam.Instance.NetTopologyUnits);

            //默认值
            

            
        }


        /// <summary>
        /// 所有网络拓扑单元
        /// </summary>
        public ObservableCollection<NetTopologyUnit> NetTopologyUnits
        {
            get { return m_NetTopologyUnits; }
            private set
            {
                if (Equals(value, m_NetTopologyUnits))
                {
                    return;
                }
                m_NetTopologyUnits = value;
                RaisePropertyChanged(() => NetTopologyUnits);
                RaisePropertyChanged(() => Car1HMI);
                RaisePropertyChanged(() => Car1Switch);
                RaisePropertyChanged(() => Car1Sig);
                RaisePropertyChanged(() => Car1Door1);
                RaisePropertyChanged(() => Car1CMS);
                RaisePropertyChanged(() => Car1Door2);
                RaisePropertyChanged(() => Car1VCMe);
                RaisePropertyChanged(() => Car1PA);
                RaisePropertyChanged(() => Car1HVAC);
                RaisePropertyChanged(() => Car1Door3);
                RaisePropertyChanged(() => Car1SKS2);
                RaisePropertyChanged(() => Car1VCUICU);
                RaisePropertyChanged(() => Car1SKS3);
                RaisePropertyChanged(() => Car1APS); 
                RaisePropertyChanged(() => Car2CMS);
                RaisePropertyChanged(() => Car2Door5);
                RaisePropertyChanged(() => Car2HVAC);
                RaisePropertyChanged(() => Car2Door4);
                RaisePropertyChanged(() => Car2RIOM);
                RaisePropertyChanged(() => Car2BCU);
                RaisePropertyChanged(() => Car2SKS4);
                RaisePropertyChanged(() => Car3Door6);
                RaisePropertyChanged(() => Car3HVAC);
                RaisePropertyChanged(() => Car3Door7);
                RaisePropertyChanged(() => Car3SKS8);
                RaisePropertyChanged(() => Car3VCUICU);
                RaisePropertyChanged(() => Car4HMI);
                RaisePropertyChanged(() => Car4Switch);
                RaisePropertyChanged(() => Car4Sig);
                RaisePropertyChanged(() => Car4Door8);
                RaisePropertyChanged(() => Car4HVAC);
                RaisePropertyChanged(() => Car4VCMe);
                RaisePropertyChanged(() => Car4Door9);
                RaisePropertyChanged(() => Car4CMS);
                RaisePropertyChanged(() => Car4Door10);
                RaisePropertyChanged(() => Car4PIDS);
                RaisePropertyChanged(() => Car4SKS5);
                RaisePropertyChanged(() => Car4APS);
                RaisePropertyChanged(() => Car4VCUICU);
                RaisePropertyChanged(() => Car4SKS9);
            }
        }



        /// <summary>
        /// 1车HMI
        /// </summary>
        public NetTopologyUnit Car1HMI { get { return NetTopologyUnits.Where(w => w.Car == 1 && w.Device == "HMI").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 1车Switch
        /// </summary>
        public NetTopologyUnit Car1Switch { get { return NetTopologyUnits.Where(w => w.Car == 1 && w.Device == "Switch").OrderBy(o => o.Device).FirstOrDefault(); } }


        /// <summary>
        /// 1车Sig
        /// </summary>
        public NetTopologyUnit Car1Sig { get { return NetTopologyUnits.Where(w => w.Car == 1 && w.Device == "Sig").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 1车Door1
        /// </summary>
        public NetTopologyUnit Car1Door1 { get { return NetTopologyUnits.Where(w => w.Car == 1 && w.Device == "Door1").OrderBy(o => o.Device).FirstOrDefault(); } }


        /// <summary>
        /// 1车CMS
        /// </summary>
        public NetTopologyUnit Car1CMS { get { return NetTopologyUnits.Where(w => w.Car == 1 && w.Device == "CMS").OrderBy(o => o.Device).FirstOrDefault(); } }


        /// <summary>
        /// 1车Door2
        /// </summary>
        public NetTopologyUnit Car1Door2 { get { return NetTopologyUnits.Where(w => w.Car == 1 && w.Device == "Door2").OrderBy(o => o.Device).FirstOrDefault(); } }


        /// <summary>
        /// 1车VCMe
        /// </summary>
        public NetTopologyUnit Car1VCMe { get { return NetTopologyUnits.Where(w => w.Car == 1 && w.Device == "VCMe").OrderBy(o => o.Device).FirstOrDefault(); } }


        /// <summary>
        /// 1车PA
        /// </summary>
        public NetTopologyUnit Car1PA { get { return NetTopologyUnits.Where(w => w.Car == 1 && w.Device == "PA").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 1车HVAC
        /// </summary>
        public NetTopologyUnit Car1HVAC { get { return NetTopologyUnits.Where(w => w.Car == 1 && w.Device == "HVAC").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 1车Door3
        /// </summary>
        public NetTopologyUnit Car1Door3 { get { return NetTopologyUnits.Where(w => w.Car == 1 && w.Device == "Door3").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 1车SKS2
        /// </summary>
        public NetTopologyUnit Car1SKS2 { get { return NetTopologyUnits.Where(w => w.Car == 1 && w.Device == "SKS2").OrderBy(o => o.Device).FirstOrDefault(); } }


        /// <summary>
        /// 1车VCUICU
        /// </summary>
        public NetTopologyUnit Car1VCUICU { get { return NetTopologyUnits.Where(w => w.Car == 1 && w.Device == "VCUICU").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 1车SKS3
        /// </summary>
        public NetTopologyUnit Car1SKS3 { get { return NetTopologyUnits.Where(w => w.Car == 1 && w.Device == "SKS3").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 1车APS
        /// </summary>
        public NetTopologyUnit Car1APS { get { return NetTopologyUnits.Where(w => w.Car == 1 && w.Device == "APS").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 2车CMS
        /// </summary>
        public NetTopologyUnit Car2CMS { get { return NetTopologyUnits.Where(w => w.Car == 2 && w.Device == "CMS").OrderBy(o => o.Device).FirstOrDefault(); } }


        /// <summary>
        /// 2车Door5
        /// </summary>
        public NetTopologyUnit Car2Door5 { get { return NetTopologyUnits.Where(w => w.Car == 2 && w.Device == "Door5").OrderBy(o => o.Device).FirstOrDefault(); } }


        /// <summary>
        /// 2车HVAC
        /// </summary>
        public NetTopologyUnit Car2HVAC { get { return NetTopologyUnits.Where(w => w.Car == 2 && w.Device == "HVAC").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 2车Door4
        /// </summary>
        public NetTopologyUnit Car2Door4 { get { return NetTopologyUnits.Where(w => w.Car == 2 && w.Device == "Door4").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 2车RIOM
        /// </summary>
        public NetTopologyUnit Car2RIOM { get { return NetTopologyUnits.Where(w => w.Car == 2 && w.Device == "RIOM").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 2车BCU
        /// </summary>
        public NetTopologyUnit Car2BCU { get { return NetTopologyUnits.Where(w => w.Car == 2 && w.Device == "BCU").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 2车SKS4
        /// </summary>
        public NetTopologyUnit Car2SKS4 { get { return NetTopologyUnits.Where(w => w.Car == 2 && w.Device == "SKS4").OrderBy(o => o.Device).FirstOrDefault(); } }


        /// <summary>
        /// 3车Door6
        /// </summary>
        public NetTopologyUnit Car3Door6 { get { return NetTopologyUnits.Where(w => w.Car == 3 && w.Device == "Door6").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 3车HVAC
        /// </summary>
        public NetTopologyUnit Car3HVAC { get { return NetTopologyUnits.Where(w => w.Car == 3 && w.Device == "HVAC").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 3车Door7
        /// </summary>
        public NetTopologyUnit Car3Door7 { get { return NetTopologyUnits.Where(w => w.Car == 3 && w.Device == "Door7").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 3车SKS8
        /// </summary>
        public NetTopologyUnit Car3SKS8 { get { return NetTopologyUnits.Where(w => w.Car == 3 && w.Device == "SKS8").OrderBy(o => o.Device).FirstOrDefault(); } }


        /// <summary>
        /// 3车VCUICU
        /// </summary>
        public NetTopologyUnit Car3VCUICU { get { return NetTopologyUnits.Where(w => w.Car == 3 && w.Device == "VCUICU").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 4车HMI
        /// </summary>
        public NetTopologyUnit Car4HMI { get { return NetTopologyUnits.Where(w => w.Car == 4 && w.Device == "HMI").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 4车Switch
        /// </summary>
        public NetTopologyUnit Car4Switch { get { return NetTopologyUnits.Where(w => w.Car == 4 && w.Device == "Switch").OrderBy(o => o.Device).FirstOrDefault(); } }


        /// <summary>
        /// 4车Sig
        /// </summary>
        public NetTopologyUnit Car4Sig { get { return NetTopologyUnits.Where(w => w.Car == 4 && w.Device == "Sig").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 4车Door8
        /// </summary>
        public NetTopologyUnit Car4Door8 { get { return NetTopologyUnits.Where(w => w.Car == 4 && w.Device == "Door8").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 4车HVAC
        /// </summary>
        public NetTopologyUnit Car4HVAC { get { return NetTopologyUnits.Where(w => w.Car == 4 && w.Device == "HVAC").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 4车VCMe
        /// </summary>
        public NetTopologyUnit Car4VCMe { get { return NetTopologyUnits.Where(w => w.Car == 4 && w.Device == "VCMe").OrderBy(o => o.Device).FirstOrDefault(); } }


        /// <summary>
        /// 4车Door9
        /// </summary>
        public NetTopologyUnit Car4Door9 { get { return NetTopologyUnits.Where(w => w.Car == 4 && w.Device == "Door9").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 4车CMS
        /// </summary>
        public NetTopologyUnit Car4CMS { get { return NetTopologyUnits.Where(w => w.Car == 4 && w.Device == "CMS").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 4车Door10
        /// </summary>
        public NetTopologyUnit Car4Door10 { get { return NetTopologyUnits.Where(w => w.Car == 4 && w.Device == "Door10").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 4车PIDS
        /// </summary>
        public NetTopologyUnit Car4PIDS { get { return NetTopologyUnits.Where(w => w.Car == 4 && w.Device == "PIDS").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 4车SKS5
        /// </summary>
        public NetTopologyUnit Car4SKS5 { get { return NetTopologyUnits.Where(w => w.Car == 4 && w.Device == "SKS5").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 4车APS
        /// </summary>
        public NetTopologyUnit Car4APS { get { return NetTopologyUnits.Where(w => w.Car == 4 && w.Device == "APS").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 4车VCUICU
        /// </summary>
        public NetTopologyUnit Car4VCUICU { get { return NetTopologyUnits.Where(w => w.Car == 4 && w.Device == "VCUICU").OrderBy(o => o.Device).FirstOrDefault(); } }



        /// <summary>
        /// 4车SKS9
        /// </summary>
        public NetTopologyUnit Car4SKS9 { get { return NetTopologyUnits.Where(w => w.Car == 4 && w.Device == "SKS9").OrderBy(o => o.Device).FirstOrDefault(); } }




    }
}