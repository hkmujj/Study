using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Extension;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.TCMS.LanZhou.Constant;
using Subway.TCMS.LanZhou.Event;
using Subway.TCMS.LanZhou.Model;
using Subway.TCMS.LanZhou.Model.Domain;
using Subway.TCMS.LanZhou.Model.Domain.PIS;
using Subway.TCMS.LanZhou.Utils;
using Subway.TCMS.LanZhou.View.Contents;
using Subway.TCMS.LanZhou.View.Contents.AirCondition;
using Subway.TCMS.LanZhou.View.Contents.BreakDownInformation;
using Subway.TCMS.LanZhou.View.Contents.HelpInformation;
using Subway.TCMS.LanZhou.View.Contents.TrainStatus;
using Subway.TCMS.LanZhou.ViewModel;


namespace Subway.TCMS.LanZhou.Controller.Domain

{
    [Export]
    public class PISController: ControllerBase<PISViewModel>
    {
        private readonly IRegionManager m_RegionManager;

        public PISModel Model { get { return ViewModel.Model; } }
        private readonly IEventAggregator m_EventAggregator;

        [ImportingConstructor]
        public PISController(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            m_RegionManager = regionManager;
            m_EventAggregator = eventAggregator;
        }
        /// <summary>
        /// Initalize
        /// </summary>
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
            Model.CurrentStation = Model.StationCollection.Value.FirstOrDefault();
            Model.EndStation = Model.StationCollection.Value.LastOrDefault();

            Model.NavigateToLocationInfoCommand = new DelegateCommand(() => NavigateTo(typeof(LineInformationView)));
            Model.ReturnRunningViewCommand = new DelegateCommand(() => NavigateTo(typeof(RunningView)));
            Model.RunningHelpPageReturnCommand = new DelegateCommand(() => NavigateTo(typeof(RunningView)));
            Model.AirConditionSettingCommand = new DelegateCommand(() => NavigateTo(typeof(AirConditionSetting)));
            Model.AirConditionStatusCommand = new DelegateCommand(() => NavigateTo(typeof(AirConditionStatus)));
            Model.RunningHelpNextPageCommand = new DelegateCommand(() => NavigateTo(typeof(RunningHelpView1)));
            Model.RunningHelpPrePageCommand = new DelegateCommand(() => NavigateTo(typeof(RunningHelpView)));

            Model.FaultDetailPageViewCommand = new DelegateCommand(() => NavigateTo(typeof(BreakDownInformationDetail)));

            Model.TrainStatusHelpNextPage1Command = new DelegateCommand(() => NavigateTo(typeof(TrainStatusHelpView1)));
            Model.TrainStatusHelpNextPage2Command = new DelegateCommand(() => NavigateTo(typeof(TrainStatusHelpView2)));
            Model.TrainStatusHelpPrePage1Command = new DelegateCommand(() => NavigateTo(typeof(TrainStatusHelpView)));
            Model.TrainStatusHelpPrePage2Command = new DelegateCommand(() => NavigateTo(typeof(TrainStatusHelpView1)));
            Model.AirConditionSettingHelCommand = new DelegateCommand(() => NavigateTo(typeof(AirConditionSetting)));
            Model.AirConditionStatusHelpCommand = new DelegateCommand(() => NavigateTo(typeof(AirConditionStatus)));

            Model.TrainStatusTowCommand = new DelegateCommand(() => TrainStatusNavigateTo(typeof(TrainTowStatusView)));
            Model.TrainStatusBrakeCommand = new DelegateCommand(() => TrainStatusNavigateTo(typeof(TrainBrakeStatusView)));
            Model.TrainStatusAssistCommand = new DelegateCommand(() => TrainStatusNavigateTo(typeof(TrainAssistStatusView)));
           
        }
  
        private void NavigateTo(Type target)
        {
            m_EventAggregator.GetEvent<ViewChangedEvent>().Publish(new ViewChangedEvent.Args(target));
            m_RegionManager.RequestNavigate(RegionNames.ContentContent, target.FullName);
        }

        private void TrainStatusNavigateTo(Type target)
        {
            NavigateTo(typeof(TrainStatusCommonView));
            m_RegionManager.RequestNavigate(RegionNames.TrainStatusContent, target.FullName);
        }
    }
}