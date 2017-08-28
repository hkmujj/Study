using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Extension;
using MMI.Facility.WPFInfrastructure.Interactivity;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Urban.GuiYang.DDU.Constant;
using Urban.GuiYang.DDU.Events;
using Urban.GuiYang.DDU.Extension;
using Urban.GuiYang.DDU.Model;
using Urban.GuiYang.DDU.Model.Constant;
using Urban.GuiYang.DDU.Model.PIS;
using Urban.GuiYang.DDU.Model.PIS.Details;
using Urban.GuiYang.DDU.Resources.Keys;
using Urban.GuiYang.DDU.Utils;
using Urban.GuiYang.DDU.View.Contents.PIS;
using Urban.GuiYang.DDU.View.Contents.PIS.PopupView;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.Controller.Domain.PIS
{
    [Export]
    public class PISController : ControllerBase<PISViewModel>
    {
        private readonly IRegionManager m_RegionManager;

        public PISModel Model { get { return ViewModel.Model; } }

        private readonly IEventAggregator m_EventAggregator;

        private SelectedSettingFlag m_CurrentSelectedSettingFlag;

        [ImportingConstructor]
        public PISController(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            m_RegionManager = regionManager;
            m_EventAggregator = eventAggregator;
        }



        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            Model.StationCollection =
                new Lazy<ReadOnlyCollection<Station>>(
                    () =>
                    {
                        var sc = GlobalParam.Instance.StationConfigCollection.Value.Select(s => new Station(s))
                            .ToList()
                            .AsReadOnly();

                        m_EventAggregator.GetEvent<LazyValueCreatedEvent>().PublishLater(new LazyValueCreatedEvent.Args());

                        return sc;
                    });

            Model.ShowingStationList = new Lazy<PageWrapper<Station>>(() =>
            {
                var ls = new PageWrapper<Station>(8 * 5, f => f.StationConfig.StationKey != 0, true);
                ls.Reset(Model.StationCollection.Value);
                return ls;
            });

            Model.NavigateToTypeCommand = new DelegateCommand<object>(OnNavigateTo);
            Model.SelectSettingCommand = new DelegateCommand<SelectedSettingFlag>(OnSelectedSetting);
            Model.CanSelectedCommand = new DelegateCommand(() => Model.PopupViewVisible = false);
            Model.ItemSelectedCommand = new DelegateCommand<object>(OnItemSelected);
            Model.NavigateToModifyHalfModelCommand = new DelegateCommand(() => NavigateTo(typeof(HalfAutoSettingView).FullName));
            Model.NavigateToEmergBroadcastCommand = new DelegateCommand(OnEmergBroadcast);
            Model.NavigateToLocationInfoCommand = new DelegateCommand(() => NavigateTo(typeof(LocationInfoView).FullName));
            Model.LineSelect = new DelegateCommand<string>(OnLineSelect);
            Model.StationModel.NextStation = Model.StationCollection.Value.FirstOrDefault();
            Model.StationModel.EndStatiion = Model.StationCollection.Value.LastOrDefault();

            Model.HalfAutoSettingModel.OkModifyCommand = new DelegateCommand(OnOkModifyHalfAutoSetting);
            Model.HalfAutoSettingModel.CancelModifyCommand = new DelegateCommand(() => NavigateTo(PISType.HalfAuto));


            Model.AutoCommand = new DelegateCommand<CommandParameter>(OnAuto);
            Model.HalfAutoCommand = new DelegateCommand<CommandParameter>(OnHalfAuto);
            Model.ManualCommand = new DelegateCommand<CommandParameter>(OnManual);
            Model.DepartCommand = new DelegateCommand<CommandParameter>((args) => { ViewModel.Parent.SendInterface.EnsureDepart(args.Parameter != null && (string)args.Parameter == "1"); });
            Model.NextStationCommand = new DelegateCommand<CommandParameter>((args) => { ViewModel.Parent.SendInterface.EnsureNextStation(args.Parameter != null && (string)args.Parameter == "1"); });
            Model.EndStationCommand = new DelegateCommand<CommandParameter>((args) => { ViewModel.Parent.SendInterface.EnsureEndStation(args.Parameter != null && (string)args.Parameter == "1"); });
        }

        private void OnLineSelect(string obj)
        {

        }

        private void OnManual(CommandParameter commandParameter)
        {
            if (commandParameter.Parameter != null && commandParameter.Parameter.ToString().Equals("1"))
            {
                ViewModel.Parent.SendInterface.SetHalfAuto(false);
                ViewModel.Parent.SendInterface.SetAuto(false);
                ViewModel.Parent.SendInterface.SetManual(true);
            }
        }

        private void OnHalfAuto(CommandParameter commandParameter)
        {
            if (commandParameter.Parameter != null && commandParameter.Parameter.ToString().Equals("1"))
            {
                ViewModel.Parent.SendInterface.SetHalfAuto(true);
                ViewModel.Parent.SendInterface.SetAuto(false);
                ViewModel.Parent.SendInterface.SetManual(false);
            }
        }

        private void OnAuto(CommandParameter commandParameter)
        {
            if (commandParameter.Parameter != null && commandParameter.Parameter.ToString().Equals("1"))
            {
                ViewModel.Parent.SendInterface.SetHalfAuto(false);
                ViewModel.Parent.SendInterface.SetAuto(true);
                ViewModel.Parent.SendInterface.SetManual(false);
            }
        }



        private void OnEmergBroadcast()
        {
            ViewModel.Parent.Controller.NavigateTo(StateKeys.Root_Layout2);
            m_RegionManager.RequestNavigateToContent(typeof(EmergBroadcastView));
        }

        private void OnOkModifyHalfAutoSetting()
        {
            ViewModel.Parent.SendInterface.SendSettingStation(PISType.HalfAuto, Model.HalfAutoSettingModel.SettingModel);
            NavigateTo(PISType.HalfAuto);
        }

        private void OnItemSelected(object obj)
        {
            if (obj == null)
            {
                return;
            }

            Model.PopupViewVisible = false;

            var staion = obj as Station;
            if (staion != null)
            {
                UpdateSettingStation(m_CurrentSelectedSettingFlag, staion);
            }

            var lineId = obj as string;
            if (lineId != null)
            {
                UpdateSettingLineId(m_CurrentSelectedSettingFlag, lineId);
            }
        }

        private void UpdateSettingLineId(SelectedSettingFlag selectedSettingFlag, string lineId)
        {
            switch (selectedSettingFlag.PISType)
            {
                case PISType.Auto:
                    break;
                case PISType.HalfAuto:
                    Model.HalfAutoSettingModel.SettingModel.LineId = lineId;
                    break;
                case PISType.Manaul:
                    Model.ManaulSettingModel.SettingModel.LineId = lineId;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateSettingStation(SelectedSettingFlag selectedSettingFlag, Station staion)
        {
            if (selectedSettingFlag.SelecttedSettingType != PISSelecttedSettingType.StationList)
            {
                return;
            }

            SettingStationModel settingStationModel = null;
            switch (selectedSettingFlag.PISType)
            {
                case PISType.Auto:
                    break;
                case PISType.HalfAuto:
                    settingStationModel = Model.HalfAutoSettingModel.SettingModel;
                    break;
                case PISType.Manaul:
                    settingStationModel = Model.ManaulSettingModel.SettingModel;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            if (settingStationModel != null/*&&settingStationModel== Model.HalfAutoSettingModel.SettingModel*/)
            {
                UpdateSettingModel(selectedSettingFlag.SelectedStationType, staion, settingStationModel);

                ViewModel.Parent.SendInterface.SendSettingStation(m_CurrentSelectedSettingFlag.PISType,
                    settingStationModel);
            }
            //if (settingStationModel != null && settingStationModel == Model.ManaulSettingModel.SettingModel)
            //{
            //    UpdateSettingModel(selectedSettingFlag.SelectedStationType, staion, settingStationModel);
            //    ViewModel.Parent.SendInterface.SendManualSettingStation(m_CurrentSelectedSettingFlag.PISType,
            //        settingStationModel);
            //}

        }

        private static void UpdateSettingModel(PISSelectedStationType selectedStationType, Station staion,
            SettingStationModel settingStationModel)
        {
            switch (selectedStationType)
            {
                case PISSelectedStationType.Start:
                    settingStationModel.StartStation = staion;
                    break;
                case PISSelectedStationType.Next:
                    settingStationModel.NextStation = staion;
                    break;
                case PISSelectedStationType.End:
                    settingStationModel.EndStation = staion;
                    break;
                case PISSelectedStationType.Skip1:
                    settingStationModel.SkipStation1 = staion;
                    break;
                case PISSelectedStationType.Skip2:
                    settingStationModel.SkipStation2 = staion;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        private void OnSelectedSetting(SelectedSettingFlag obj)
        {
            m_CurrentSelectedSettingFlag = obj;
            switch (obj.SelecttedSettingType)
            {
                case PISSelecttedSettingType.None:
                    break;
                case PISSelecttedSettingType.LineId:
                    m_RegionManager.RequestNavigate(RegionNames.ContentContentContentPISContentPopupView, typeof(LineIdListView).FullName);
                    break;
                case PISSelecttedSettingType.StationList:
                    m_RegionManager.RequestNavigate(RegionNames.ContentContentContentPISContentPopupView, typeof(StationListView).FullName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Model.PopupViewVisible = true;
        }

        private void OnNavigateTo(object pisType)
        {
            if (pisType is PISType)
            {
                var t = (PISType)pisType;

                NavigateTo(t);
            }
        }

        public void NavigateTo(PISType pisType)
        {
            string target;
            switch (pisType)
            {
                case PISType.Auto:
                    target = typeof(AutoModelView).FullName;
                    break;
                case PISType.HalfAuto:
                    target = typeof(HalfAutoModelView).FullName;
                    break;
                case PISType.Manaul:
                    target = typeof(ManualModelView).FullName;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("pisType", pisType, null);
            }
            NavigateTo(target);
        }

        private void NavigateTo(string target)
        {
            Model.PopupViewVisible = false;
            m_RegionManager.RequestNavigate(RegionNames.ContentContentContentPISContent, target);
        }
    }
}