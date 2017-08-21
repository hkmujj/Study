using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.Interface.Data;
using Subway.XiaMenLine1.Interface;
using Subway.XiaMenLine1.Interface.Model;
using Subway.XiaMenLine1.Interface.Resouce;
using Subway.XiaMenLine1.Subsystem.Extension;

namespace Subway.XiaMenLine1.Subsystem.Model
{
    public class EnmergencyBorader : ViewModelBase, IEnmergencyBorader
    {
        private ObservableCollection<IBoradcast> m_LoveBoradcast;
        private ObservableCollection<IBoradcast> m_EmergentBoradcast;
        private ObservableCollection<IBoradcast> m_CustBoradcast;
        public static EnmergencyBorader Instance { get; private set; }
        public void ChangeBools(CommunicationDataChangedArgs<bool> args)
        {

        }

        private void GetCurrentMethod()
        {
            LoveBoradcast = new ObservableCollection<IBoradcast>(Parent.BoradercastMgr.AllData.Values.Where(w => w.Type == BoradcastType.Love));
            EmergentBoradcast = new ObservableCollection<IBoradcast>(Parent.BoradercastMgr.AllData.Values.Where(w => w.Type == BoradcastType.Emergent));
            CustBoradcast = new ObservableCollection<IBoradcast>(Parent.BoradercastMgr.AllData.Values.Where(w => w.Type == BoradcastType.Customized));
        }



        public void ChangeFloats(CommunicationDataChangedArgs<float> args)
        {

        }

        public ICommand SendBoradType { get; set; }
        public ICommand SenBoradID { get; private set; }
        public ICommand GetCurrent { get; private set; }

        public ObservableCollection<IBoradcast> LoveBoradcast
        {
            get { return m_LoveBoradcast; }
            private set
            {
                if (Equals(value, m_LoveBoradcast))
                    return;

                m_LoveBoradcast = value;
                RaisePropertyChanged(() => LoveBoradcast);
            }
        }

        public ObservableCollection<IBoradcast> EmergentBoradcast
        {
            get { return m_EmergentBoradcast; }
            private set
            {
                if (Equals(value, m_EmergentBoradcast))
                    return;

                m_EmergentBoradcast = value;
                RaisePropertyChanged(() => EmergentBoradcast);
            }
        }

        public ObservableCollection<IBoradcast> CustBoradcast
        {
            get { return m_CustBoradcast; }
            private set
            {
                if (Equals(value, m_CustBoradcast))
                    return;

                m_CustBoradcast = value;
                RaisePropertyChanged(() => CustBoradcast);
            }
        }


        public EnmergencyBorader(IMMI parent) : base(parent)
        {
            GetCurrent = new DelegateCommand(GetCurrentMethod);
            SenBoradID = new DelegateCommand<IBoradcast>(SendBoradIDMethod);
            SendBoradType = new DelegateCommand<string>(SendBoradTypeMethod);
            Instance = this;
        }

        private void SendBoradTypeMethod(string obj)
        {
            obj.SendData(true, true);
        }

        private void SendBoradIDMethod(IBoradcast obj)
        {
            if (obj == null)
            {
                return;
            }
            var idnex = SubsysParams.Instance.IndexDescriptionConfig.OutFloatDescriptionDictionary[OutFloatKeys.紧急广播编号];
            Parent.Dataserver.WriteService.ChangeFloat(idnex, obj.Number);
        }
    }
}