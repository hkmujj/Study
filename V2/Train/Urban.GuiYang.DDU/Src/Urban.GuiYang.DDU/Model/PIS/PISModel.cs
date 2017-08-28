using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.WPFInfrastructure.Interactivity;
using Urban.GuiYang.DDU.Model.Constant;
using Urban.GuiYang.DDU.Model.PIS.Details;
using Urban.GuiYang.DDU.Utils;

namespace Urban.GuiYang.DDU.Model.PIS
{
    [Export]
    public class PISModel : NotificationObject
    {
        private PISType m_PISType;
        private bool m_PopupViewVisible;

        [ImportingConstructor]
        public PISModel(HalfAutoSettingModel halfAutoSettingModel, ManaulSettingModel manaulSettingModel, StationModel stationModel)
        {
            HalfAutoSettingModel = halfAutoSettingModel;
            ManaulSettingModel = manaulSettingModel;
            StationModel = stationModel;
        }

        public bool PopupViewVisible
        {
            get { return m_PopupViewVisible; }
            set
            {
                if (value == m_PopupViewVisible)
                {
                    return;
                }
                m_PopupViewVisible = value;
                RaisePropertyChanged(() => PopupViewVisible);
            }
        }

        public PISType PISType
        {
            get { return m_PISType; }
            set
            {
                if (value == m_PISType)
                {
                    return;
                }

                m_PISType = value;
                RaisePropertyChanged(() => PISType);
            }
        }

        public DelegateCommand<CommandParameter> AutoCommand { get; set; }

        public DelegateCommand<CommandParameter> HalfAutoCommand { get; set; }

        public DelegateCommand<CommandParameter> ManualCommand { get; set; }

        public DelegateCommand<CommandParameter> DepartCommand { get; set; }

        public DelegateCommand<CommandParameter> NextStationCommand { get; set; }

        public DelegateCommand<CommandParameter> EndStationCommand { get; set; }


        public DelegateCommand<SelectedSettingFlag> SelectSettingCommand { get; set; }

        public DelegateCommand<string> LineSelect { get; set; }

        public DelegateCommand CanSelectedCommand { set; get; }

        public DelegateCommand NavigateToModifyHalfModelCommand { set; get; }

        public DelegateCommand NavigateToEmergBroadcastCommand { set; get; }

        public DelegateCommand<object> ItemSelectedCommand { set; get; }

        public DelegateCommand<object> NavigateToTypeCommand { get; set; }

        public DelegateCommand NavigateToLocationInfoCommand { get; set; }

        public HalfAutoSettingModel HalfAutoSettingModel { get; private set; }

        public ManaulSettingModel ManaulSettingModel { get; private set; }

        public Lazy<ReadOnlyCollection<Station>> StationCollection { get; set; }

        public Lazy<PageWrapper<Station>> ShowingStationList { get; set; }

        public StationModel StationModel { get; private set; }
    }
}