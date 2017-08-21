using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using Subway.XiaMenLine1.Interface;
using Subway.XiaMenLine1.Interface.Enum;
using Subway.XiaMenLine1.Interface.Model;
using Subway.XiaMenLine1.Interface.Resouce;

namespace Subway.XiaMenLine1.Subsystem.Model
{
    public class TractionModel : NotificationObject, ITraction
    {
        private TractionStatus m_CarFiveTractionStatus;
        private TractionStatus m_CarFourTractionStatus;
        private TractionStatus m_CarThreeTractionStatus;
        private TractionStatus m_CarTwoTractionStatus;

        public TractionModel()
        {
            CarTwoTractionStatus = TractionStatus.TractionClosed;
            CarThreeTractionStatus = TractionStatus.TractionClosed;
            CarFourTractionStatus = TractionStatus.TractionClosed;
            CarFiveTractionStatus = TractionStatus.TractionClosed;
        }
        public TractionStatus CarTwoTractionStatus
        {
            get { return m_CarTwoTractionStatus; }
            set
            {
                if (value == m_CarTwoTractionStatus)
                {
                    return;
                }
                m_CarTwoTractionStatus = value;
                RaisePropertyChanged(() => CarTwoTractionStatus);
            }
        }

        public TractionStatus CarThreeTractionStatus
        {
            get { return m_CarThreeTractionStatus; }
            set
            {
                if (value == m_CarThreeTractionStatus)
                {
                    return;
                }
                m_CarThreeTractionStatus = value;
                RaisePropertyChanged(() => CarThreeTractionStatus);
            }
        }

        public TractionStatus CarFourTractionStatus
        {
            get { return m_CarFourTractionStatus; }
            set
            {
                if (value == m_CarFourTractionStatus)
                {
                    return;
                }
                m_CarFourTractionStatus = value;
                RaisePropertyChanged(() => CarFourTractionStatus);
            }
        }

        public TractionStatus CarFiveTractionStatus
        {
            get { return m_CarFiveTractionStatus; }
            set
            {
                if (value == m_CarFiveTractionStatus)
                {
                    return;
                }
                m_CarFiveTractionStatus = value;
                RaisePropertyChanged(() => CarFiveTractionStatus);
            }
        }

        public void ChangeStatus(object sender, CommunicationDataChangedArgs e)
        {
            ChangeBools(e.ChangedBools);
            ChangeFloats(e.ChangedFloats);
        }

        public void ChangeBools(CommunicationDataChangedArgs<bool> args)
        {
            var index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车2号牵引故障];
            var index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车2号牵引激活];
            var index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车2号牵引切除];
            var index4 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车2号牵引断开];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3) || args.NewValue.ContainsKey(index4))
            {
                CarTwoTractionStatus = TractionStatus.TractionClosed;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    CarTwoTractionStatus = TractionStatus.TractionFault;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    CarTwoTractionStatus = TractionStatus.TractionActive;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    CarTwoTractionStatus = TractionStatus.TractionCut;
                }
                else if (args.NewValue.ContainsKey(index4) && args.NewValue[index4])
                {
                    CarTwoTractionStatus = TractionStatus.TractionClosed;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车3号牵引故障];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车3号牵引激活];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车3号牵引切除];
            index4 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车3号牵引断开];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3) || args.NewValue.ContainsKey(index4))
            {
                CarThreeTractionStatus = TractionStatus.TractionClosed;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    CarThreeTractionStatus = TractionStatus.TractionFault;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    CarThreeTractionStatus = TractionStatus.TractionActive;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    CarThreeTractionStatus = TractionStatus.TractionCut;
                }
                else if (args.NewValue.ContainsKey(index4) && args.NewValue[index4])
                {
                    CarThreeTractionStatus = TractionStatus.TractionClosed;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车4号牵引故障];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车4号牵引激活];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车4号牵引切除];
            index4 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车4号牵引断开];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3) || args.NewValue.ContainsKey(index4))
            {
                CarFourTractionStatus = TractionStatus.TractionClosed;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    CarFourTractionStatus = TractionStatus.TractionFault;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    CarFourTractionStatus = TractionStatus.TractionActive;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    CarFourTractionStatus = TractionStatus.TractionCut;
                }
                else if (args.NewValue.ContainsKey(index4) && args.NewValue[index4])
                {
                    CarFourTractionStatus = TractionStatus.TractionClosed;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车5号牵引故障];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车5号牵引激活];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车5号牵引切除];
            index4 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车5号牵引断开];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3) || args.NewValue.ContainsKey(index4))
            {
                CarFiveTractionStatus = TractionStatus.TractionClosed;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    CarFiveTractionStatus = TractionStatus.TractionFault;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    CarFiveTractionStatus = TractionStatus.TractionActive;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    CarFiveTractionStatus = TractionStatus.TractionCut;
                }
                else if (args.NewValue.ContainsKey(index4) && args.NewValue[index4])
                {
                    CarFiveTractionStatus = TractionStatus.TractionClosed;
                }
            }
        }

        public void ChangeFloats(CommunicationDataChangedArgs<float> args)
        {

        }
    }
}