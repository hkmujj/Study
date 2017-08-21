using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using CommonUtil.Annotations;
using CommonUtil.Util;
using Engine.LCDM.HXD3.Constance;
using Engine.LCDM.HXD3.Enums;
using Engine.LCDM.HXD3.Events;
using Engine.LCDM.HXD3.Interfaces;
using Engine.LCDM.HXD3.LCMDAtt;
using Engine.LCDM.HXD3.Resource;
using Engine.LCDM.HXD3.ViewModels;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Engine.LCDM.HXD3.Config;
using System.Windows.Markup;
using System.Collections.ObjectModel;
using Engine.LCDM.HXD3.Common;
using Excel.Interface;
using MMI.Facility.WPFInfrastructure.Interactivity;

namespace Engine.LCDM.HXD3.Controller
{
    [Export(typeof(LCDMController))]
    public class LCDMController : ControllerBase<LCDMViewModel>
    {
        [ImportingConstructor] 
        public LCDMController(IRegionManager regionmanager, IEventAggregator eventAggregator)
        {
            Navigator = new DelegateCommand<string>(NavigatorAction);
            NavigatorOne = new DelegateCommand<TrainStyleChoice>(OnNavigatorActionOne);
            ResponseButton = new DelegateCommand(ResponseButtonAction);
            LoadedCommand = new DelegateCommand(OnLoaded);
            RegionManager = regionmanager;
            EventAggregator = eventAggregator;
            EventAggregator.GetEvent<ViewChangedEvent>().Subscribe((args) =>
            {
                RegionManager.RequestNavigate(args.Att.RegionName, args.FullName);
            }, ThreadOption.UIThread);

            m_TrainType = GlobalParam.Instance.InitSets.Value.FirstOrDefault(t => t.Name == "机车类型");
        }

        private void OnLoaded()
        {
            OnNavigatorActionOne(TrainStyles.FlowStyle);
        }

        private void ResponseButtonAction()
        {
            if (!RegionManager.Regions.ContainsRegionWithName(RegionNames.BottomButton)) { return; }
            var s = RegionManager.Regions[RegionNames.BottomButton].ActiveViews.FirstOrDefault() as IButtons;
            if (s == null)
            {
                return;
            }
            switch (ViewModel.Buttons)
            {
                case Buttons.F1:
                    ExecuteButton(s.F1);
                    break;
                case Buttons.F2:
                    ExecuteButton(s.F2);
                    break;
                case Buttons.F3:
                    ExecuteButton(s.F3);
                    break;
                case Buttons.F4:
                    ExecuteButton(s.F4);
                    break;
                case Buttons.F5:
                    ExecuteButton(s.F5);
                    break;
                case Buttons.F6:
                    ExecuteButton(s.F6);
                    break;
                case Buttons.F7:
                    ExecuteButton(s.F7);
                    break;
                case Buttons.F8:
                    ExecuteButton(s.F8);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void ExecuteButton(Button btn)
        {

            if (btn != null)
            {
                if (btn.Visibility == Visibility.Visible)
                {
                    btn.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        if (btn.Command != null && btn.Visibility == Visibility.Visible)
                        {
                            btn.Command.Execute(btn.CommandParameter);
                        }
                    }));
                }
            }
        }

        private void OnNavigatorActionOne(TrainStyleChoice pa)
        {
            string fullName="";
            if (m_TrainType != null)
            {
                if (m_TrainType.Content == "货车")
                {
                    fullName = pa.Goods;
                }
                else
                {
                    fullName = pa.Passenger;
                }
            }
            NavigatorAction(fullName);
        }

        private void NavigatorAction(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                return;
            }
            var type = Type.GetType(fullName);
            if (type != null)
            {
                var deactive = type.GetCustomAttributes(typeof(DeactiveAttribute), false).Cast<DeactiveAttribute>();
                DeActive(deactive);
                var active = type.GetCustomAttributes(typeof(ActiveAttribute), false).Cast<ActiveAttribute>();
                Active(active);
                Active(fullName);
            }
        }

        public void InterNavigator(string fullName)
        {
            NavigatorAction(fullName);
        }

        private void DeActive(IEnumerable<DeactiveAttribute> deActive)
        {
            if (deActive == null)
            {
                return;
            }
            foreach (
                var region in deActive.Where(
                w=>RegionManager.Regions.ContainsRegionWithName(w.RegionName))
                .Select(attribute => RegionManager.Regions[attribute.RegionName]))
            {
                try
                {
                    region.ActiveViews.ToList().ForEach(f => region.Deactivate(f));
                }
                catch (Exception e)
                {
                    AppLog.Error(e.ToString());
                }
            }
        }
        private void Active(string fulleName)
        {
            var type = Type.GetType(fulleName);
            if (type != null)
            {
                var viewExport =
                    type.GetCustomAttributes(typeof(ViewExportAttribute), false).FirstOrDefault() as
                        ViewExportAttribute;
                if (viewExport != null)
                {
                    EventAggregator.GetEvent<ViewChangedEvent>().Publish(new ViewChangedEventArgs()
                    {
                        Att = viewExport,
                        FullName = fulleName,
                    });
                    EventAggregator.GetEvent<VieChangedNotifiEvent>().Publish(new VieChangedNotifiEvent.NotifiArgs() { FullName = fulleName });
                }
            }
        }

        private void ChangedViewAction(ViewExportAttribute view)
        {

        }
        private void Active(IEnumerable<ActiveAttribute> active)
        {
            if (active == null)
            {
                return;
            }
            foreach (var attribute in active)
            {
                NavigatorAction(attribute.ControlType.FullName);
            }
        }
        public IEventAggregator EventAggregator { get; private set; }
        protected IRegionManager RegionManager { get; private set; }
        public ICommand Navigator { get; private set; }

        public ICommand NavigatorOne { get; private set; }

        public ICommand ResponseButton { get; private set; }

        public ObservableCollection<InitialSet> InitialCollection
        {
            get { return m_InitialCollection; }
            set
            {
                if (Equals(value, m_InitialCollection))
                {
                    return;
                }
                m_InitialCollection = value;
                RaisePropertyChanged(() => InitialCollection);
            }
        }

        public ICommand LoadedCommand { private set; get; }

        private ObservableCollection<InitialSet> m_InitialCollection;
        private InitialSet m_TrainType;
    }
}