using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.Interface.Data;
using Subway.ShiJiaZhuangLine1.Interface;
using Subway.ShiJiaZhuangLine1.Interface.Enum;
using Subway.ShiJiaZhuangLine1.Interface.Model;
using Subway.ShiJiaZhuangLine1.Interface.Resouce;
using Subway.ShiJiaZhuangLine1.Subsystem.Constant;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Model
{
    public class StationSettingModel : ViewModelBase
        , IStationSettingModel
    {
        private int StationIndex;
        private StationType StationType;

        private BoradercastModel Model;
        private string m_EndStation;
        private string m_NextStation;
        private string m_StartStation;
        private bool m_EndStationDown;
        private bool m_NextStationDown;
        private bool m_StartStationDown;
        private bool m_ManualButtonDown;
        private bool m_AutoButtonDown;

        public StationSettingModel(IMMI parent)
            : base(parent)
        {
            ModelSelect = new DelegateCommand<string>(ModelSelectMethod);
            StationViewDump = new DelegateCommand<string>(StationViewDumpMethod);
            StationSelect = new DelegateCommand<IStation>(StationSelectMethod);
            StationComfirm = new DelegateCommand(StationComfirmMethod);
            Up = new DelegateCommand(UpAction);
            Down = new DelegateCommand(DownAtion);
            SkipStation = new DelegateCommand(SkipStationAction);
        }

        private void SkipStationAction()
        {
            var index = IndexConfigure.Instance.IndexFacade.OutBoolDictionary[OutBoolKeys.越站];
            Task.Factory.StartNew(new Action(() =>
            {
                Parent.Dataserver.WriteService.ChangeBool(index, true);
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Parent.Dataserver.WriteService.ChangeBool(index, false);
            }));
        }

        private void DownAtion()
        {
            var index = IndexConfigure.Instance.IndexFacade.OutBoolDictionary[OutBoolKeys.广播界面上行];
            Parent.Dataserver.WriteService.ChangeBool(index, false);
            index = IndexConfigure.Instance.IndexFacade.OutBoolDictionary[OutBoolKeys.广播界面下行];
            Parent.Dataserver.WriteService.ChangeBool(index, true);
        }

        private void UpAction()
        {
            var index = IndexConfigure.Instance.IndexFacade.OutBoolDictionary[OutBoolKeys.广播界面上行];
            Parent.Dataserver.WriteService.ChangeBool(index, true);
            index = IndexConfigure.Instance.IndexFacade.OutBoolDictionary[OutBoolKeys.广播界面下行];
            Parent.Dataserver.WriteService.ChangeBool(index, false);
        }


        private void StationComfirmMethod()
        {
            switch (StationType)
            {
                case StationType.Start:
                    var index = IndexConfigure.Instance.IndexFacade.OutFloatDictionary[OutFloatKeys.半动报站起始站];
                    Parent.Dataserver.WriteService.ChangeFloat(index, StationIndex);
                    StartStation = Parent.StationsMgr.GetStationNames(StationIndex);
                    break;
                case StationType.Next:
                    var index1 = IndexConfigure.Instance.IndexFacade.OutFloatDictionary[OutFloatKeys.手动报站下一站];
                    Parent.Dataserver.WriteService.ChangeFloat(index1, StationIndex);
                    NextStation = Parent.StationsMgr.GetStationNames(StationIndex);
                    break;
                case StationType.End:
                    var index2 = IndexConfigure.Instance.IndexFacade.OutFloatDictionary[OutFloatKeys.半动报站终点站];
                    Parent.Dataserver.WriteService.ChangeFloat(index2, StationIndex);
                    EndStation = Parent.StationsMgr.GetStationNames(StationIndex);
                    break;
            }

            RegionManager.RequestNavigate(RegionNames.MainContentContentRegion, ViewNames.BoradercastSettingView);
            RegionManager.RequestNavigate(RegionNames.ShellContentRegion, ViewNames.ShellContentMainContentView);

        }

        private void StationSelectMethod(IStation obj)
        {
            if (obj != null)
            {
                StationIndex = obj.Number;
            }
        }

        private void StationViewDumpMethod(string obj)
        {
            StationType type;
            Enum.TryParse(obj, out type);
            if (Model == BoradercastModel.Auto)
            {
                return;
            }

            StationType = type;
            RegionManager.RequestNavigate(RegionNames.MainContentContentRegion, ViewNames.StationSettingView);
            RegionManager.RequestNavigate(RegionNames.ShellContentRegion, ViewNames.ShellContentMainContentView);
            ChangedButtonStatus();

        }

        private void ChangedButtonStatus()
        {
            switch (StationType)
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
        private void ModelSelectMethod(string obj)
        {
            BoradercastModel model;
            Enum.TryParse(obj, true, out model);
            if (model == BoradercastModel.Auto)
            {
                AutoButtonDown = true;
                ManualButtonDown = false;
                var index = IndexConfigure.Instance.IndexFacade.OutBoolDictionary[OutBoolKeys.报站模式自动];
                Parent.Dataserver.WriteService.ChangeBool(index, true);
                var index1 = IndexConfigure.Instance.IndexFacade.OutBoolDictionary[OutBoolKeys.报站模式半自动];
                Parent.Dataserver.WriteService.ChangeBool(index1, false);
                Model = model;
            }
            else if (model == BoradercastModel.Manual)
            {
                AutoButtonDown = false;
                ManualButtonDown = true;
                var index = IndexConfigure.Instance.IndexFacade.OutBoolDictionary[OutBoolKeys.报站模式半自动];
                Parent.Dataserver.WriteService.ChangeBool(index, true);
                var index1 = IndexConfigure.Instance.IndexFacade.OutBoolDictionary[OutBoolKeys.报站模式自动];
                Parent.Dataserver.WriteService.ChangeBool(index1, false);
                Model = model;
            }
        }

        public void ChangeBools(CommunicationDataChangedArgs<bool> args)
        {

        }

        public void ChangeFloats(CommunicationDataChangedArgs<float> args)
        {

        }

        public ICommand ModelSelect { get; private set; }
        public ICommand StationViewDump { get; private set; }
        public ICommand StationSelect { get; private set; }
        public ICommand StationComfirm { get; private set; }

        public bool AutoButtonDown
        {
            get { return m_AutoButtonDown; }
            set
            {
                if (value == m_AutoButtonDown)
                {
                    return;
                }
                m_AutoButtonDown = value;
                RaisePropertyChanged(() => AutoButtonDown);
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
                RaisePropertyChanged(() => ManualButtonDown);
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
            Model = BoradercastModel.Auto;
            AutoButtonDown = true;
            ManualButtonDown = false;
            var index = IndexConfigure.Instance.IndexFacade.OutBoolDictionary[OutBoolKeys.报站模式自动];
            Parent.Dataserver.WriteService.ChangeBool(index, true);
        }

        public ICommand Up { get; private set; }
        public ICommand Down { get; private set; }
        public ICommand SkipStation { get; private set; }
    }
}