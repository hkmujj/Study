using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Data;
using Subway.XiaMenLine1.Interface;
using Subway.XiaMenLine1.Interface.Enum;
using Subway.XiaMenLine1.Interface.Model;
using Subway.XiaMenLine1.Subsystem.Constant;
using Subway.XiaMenLine1.Subsystem.Events;
using Subway.XiaMenLine1.Interface.Resouce;

namespace Subway.XiaMenLine1.Subsystem.Model
{
    public class StationSettingModel : ViewModelBase
        , IStationSettingModel
    {
        private int m_StationIndex;
        private StationType m_StationType;

        private BoradercastModel m_Model;
        private string m_EndStation;
        private string m_NextStation;
        private string m_StartStation;
        private bool m_EndStationDown;
        private bool m_NextStationDown;
        private bool m_StartStationDown;
        private bool m_ManualButtonDown;
        private bool m_AutoButtonDown;
        private bool m_HalfButtonEnable;
        private bool m_ManualButtonEnable;

        public StationSettingModel(IMMI parent)
            : base(parent)
        {
            StationViewDump = new DelegateCommand<string>(StationViewDumpMethod);
            StationSelect = new DelegateCommand<string>(StationSelectMethod);
            StationComfirm = new DelegateCommand(StationComfirmMethod);
            SendBorder = new DelegateCommand<string>(SendBorderAction);
            HalfModelClick = new DelegateCommand(HalfModelClickMethod);
            ManulModelClick = new DelegateCommand(ManulModelClickMethod);
            HalfButtonEnable = true;
            ManualButtonEnable = true;
            ModelSelectMethod(BoradercastModel.Auto);
        }

        private void ManulModelClickMethod()
        {
            if (HalfButtonDown)
            {
                HalfButtonDown = false;
                HalfButtonEnable = false;
                ModelSelectMethod(BoradercastModel.HalfAuto);
            }
            if (!ManualButtonDown)
            {
                HalfButtonEnable = true;

            }
            ModelSelectMethod(BoradercastModel.Manual);
        }

        private void HalfModelClickMethod()
        {
            if (ManualButtonDown)
            {
                ManualButtonEnable = false;
                ManualButtonDown = false;
                ModelSelectMethod(BoradercastModel.Manual);
            }
            if (!HalfButtonDown)
            {
                ManualButtonEnable = true;
            }
            ModelSelectMethod(BoradercastModel.HalfAuto);
        }

        private void SendBorderAction(string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return;
            }
            BorderCastType type;
            if (Enum.TryParse(obj, true, out type))
            {
                switch (type)
                {
                    case BorderCastType.LastSkip:
                        SendBoolData(OutBoolKeys.上一站跳站, true, true);
                        break;
                    case BorderCastType.NextSkip:
                        SendBoolData(OutBoolKeys.下一站跳站, true, true);
                        break;
                    case BorderCastType.Arrive:
                        SendBoolData(OutBoolKeys.到站广播, true, true);
                        break;
                    case BorderCastType.DeArrive:
                        SendBoolData(OutBoolKeys.离站广播, true, true);
                        break;
                    case BorderCastType.Skip:
                        SendBoolData(OutBoolKeys.越站, true, true);
                        break;
                }
            }


        }

        private void SendBoolData(string key, bool value, bool isRest = false)
        {
            var index = IndexConfigure.Instance.IndexFacade.OutBoolDictionary[key];
            Parent.Dataserver.WriteService.ChangeBool(index, value);
            if (isRest)
            {
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    Parent.Dataserver.WriteService.ChangeBool(index, !value);
                });
            }
        }
        private void StationComfirmMethod()
        {
            switch (m_StationType)
            {
                case StationType.Start:
                    var index = IndexConfigure.Instance.IndexFacade.OutFloatDictionary[OutFloatKeys.半动报站起始站];
                    Parent.Dataserver.WriteService.ChangeFloat(index, m_StationIndex);
                    StartStation = Parent.StationsMgr.GetStationNames(m_StationIndex);
                    break;
                case StationType.Next:
                    var index1 = IndexConfigure.Instance.IndexFacade.OutFloatDictionary[OutFloatKeys.手动报站下一站];
                    Parent.Dataserver.WriteService.ChangeFloat(index1, m_StationIndex);
                    NextStation = Parent.StationsMgr.GetStationNames(m_StationIndex);
                    break;
                case StationType.End:
                    var index2 = IndexConfigure.Instance.IndexFacade.OutFloatDictionary[OutFloatKeys.半动报站终点站];
                    Parent.Dataserver.WriteService.ChangeFloat(index2, m_StationIndex);
                    EndStation = Parent.StationsMgr.GetStationNames(m_StationIndex);
                    break;
            }

            RegionManager.RequestNavigate(RegionNames.MainContentContentRegion, ViewNames.BoradercastSettingView);
            RegionManager.RequestNavigate(RegionNames.ShellContentRegion, ViewNames.ShellContentMainContentView);

        }

        private void StationSelectMethod(string obj)
        {
            int idnex;
            if (int.TryParse(obj, out idnex))
            {
                m_StationIndex = idnex;
            }
        }

        private void StationViewDumpMethod(string obj)
        {
            StationType type;
            Enum.TryParse(obj, out type);
            ChangedButtonStatus();
            if (m_Model == BoradercastModel.Auto)
            {
                ClearButtonStatus();
                return;
            }

            m_StationType = type;
            RegionManager.RequestNavigate(RegionNames.MainContentContentRegion, ViewNames.StationSettingView);
            RegionManager.RequestNavigate(RegionNames.ShellContentRegion, ViewNames.ShellContentMainContentView);
            ChangedButtonStatus();

        }

        private void ClearButtonStatus()
        {
            StartStationDown = false;
            NextStationDown = false;
            EndStationDown = false;
        }
        private void ChangedButtonStatus()
        {
            switch (m_StationType)
            {
                case StationType.Start:
                    StartStationDown = true;
                    NextStationDown = false;
                    EndStationDown = false;
                    break;
                case StationType.Next:
                    StartStationDown = false;
                    NextStationDown = true;
                    EndStationDown = false;
                    break;
                case StationType.End:
                    StartStationDown = false;
                    NextStationDown = false;
                    EndStationDown = true;
                    break;

            }
        }
        private void ModelSelectMethod(BoradercastModel model)
        {

            if (model == BoradercastModel.Manual)
            {
                var index = IndexConfigure.Instance.IndexFacade.OutBoolDictionary[OutBoolKeys.报站模式手动];
                Parent.Dataserver.WriteService.ChangeBool(index, ManualButtonDown);
                m_Model = model;
            }
            else if (model == BoradercastModel.HalfAuto)
            {
                var index2 = IndexConfigure.Instance.IndexFacade.OutBoolDictionary[OutBoolKeys.报站模式半自动];
                Parent.Dataserver.WriteService.ChangeBool(index2, HalfButtonDown);
                m_Model = model;
            }
            var isauto = (!ManualButtonDown) && (!HalfButtonDown);
            var index1 = IndexConfigure.Instance.IndexFacade.OutBoolDictionary[OutBoolKeys.报站模式自动];
            Parent.Dataserver.WriteService.ChangeBool(index1, isauto);
            m_Model = BoradercastModel.Auto;
        }

        public void ChangeBools(CommunicationDataChangedArgs<bool> args)
        {

        }

        public void ChangeFloats(CommunicationDataChangedArgs<float> args)
        {

        }
        public DelegateCommand HalfModelClick { get; private set; }
        public DelegateCommand ManulModelClick { get; private set; }
        public ICommand StationViewDump { get; private set; }
        public ICommand StationSelect { get; private set; }
        public ICommand StationComfirm { get; private set; }
        public ICommand SendBorder { get; private set; }

        public bool HalfButtonDown
        {
            get { return m_AutoButtonDown; }
            set
            {
                if (value == m_AutoButtonDown)
                {
                    return;
                }
                m_AutoButtonDown = value;
                HalfModelClick.RaiseCanExecuteChanged();
                ManulModelClick.RaiseCanExecuteChanged();
                RaisePropertyChanged(() => HalfButtonDown);
            }
        }

        public bool ManualButtonDown
        {
            get { return m_ManualButtonDown; }
            set
            {
                if (value == m_ManualButtonDown)
                {
                    return;
                }
                m_ManualButtonDown = value;
                HalfModelClick.RaiseCanExecuteChanged();
                ManulModelClick.RaiseCanExecuteChanged();
                RaisePropertyChanged(() => ManualButtonDown);
            }
        }

        public bool HalfButtonEnable
        {
            get { return m_HalfButtonEnable; }
            set
            {
                if (value == m_HalfButtonEnable)
                    return;

                m_HalfButtonEnable = value;
                RaisePropertyChanged(() => HalfButtonEnable);
            }
        }

        public bool ManualButtonEnable
        {
            get { return m_ManualButtonEnable; }
            set
            {
                if (value == m_ManualButtonEnable)
                    return;

                m_ManualButtonEnable = value;
                RaisePropertyChanged(() => ManualButtonEnable);
            }
        }

        public bool StartStationDown
        {
            get { return m_StartStationDown; }
            set
            {
                if (value == m_StartStationDown)
                {
                    return;
                }
                m_StartStationDown = value;
                RaisePropertyChanged(() => StartStationDown);
            }
        }

        public bool NextStationDown
        {
            get { return m_NextStationDown; }
            set
            {
                if (value == m_NextStationDown)
                {
                    return;
                }
                m_NextStationDown = value;
                RaisePropertyChanged(() => NextStationDown);
            }
        }

        public bool EndStationDown
        {
            get { return m_EndStationDown; }
            set
            {
                if (value == m_EndStationDown)
                {
                    return;
                }
                m_EndStationDown = value;
                RaisePropertyChanged(() => EndStationDown);
            }
        }

        public string StartStation
        {
            get { return m_StartStation; }
            set
            {
                if (value == m_StartStation)
                {
                    return;
                }
                m_StartStation = value;
                RaisePropertyChanged(() => StartStation);
            }
        }

        public string NextStation
        {
            get { return m_NextStation; }
            set
            {
                if (value == m_NextStation)
                {
                    return;
                }
                m_NextStation = value;
                RaisePropertyChanged(() => NextStation);
            }
        }

        public string EndStation
        {
            get { return m_EndStation; }
            set
            {
                if (value == m_EndStation)
                {
                    return;
                }
                m_EndStation = value;
                RaisePropertyChanged(() => EndStation);
            }
        }

        public void Init()
        {
            m_Model = BoradercastModel.Auto;
        }
    }
}