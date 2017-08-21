using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Subway.ShenZhenLine11.Config;
using Subway.ShenZhenLine11.Constance;
using Subway.ShenZhenLine11.Controller;
using Subway.ShenZhenLine11.Event;
using Subway.ShenZhenLine11.Interface;
using Subway.ShenZhenLine11.Resource;

namespace Subway.ShenZhenLine11.ViewModels
{

    [Export(typeof(ShenZhenViewModel))]
    public class ShenZhenViewModel : IStatusChanged, IClear, INavigationAware, IDataListener
    {

        [Import]
        public TitleViewModel Title { get; private set; }
        [Import]
        public DoorViewModel Door { get; private set; }
        [Import]
        public AirConditionViewModel AirCondition { get; private set; }
        [Import]
        public AssistPowerViewModel AssistPower { get; private set; }
        [Import]
        public EmergencyBordercastViewModel EmergencyBordercast { get; private set; }
        [Import]
        public EmergencyTalkViewModel EmergencyTalk { get; private set; }
        [Import]
        public BrakeViewModel Brake { get; private set; }
        [Import]
        public TractionViewModel Traction { get; private set; }
        [Import]
        public SmokeViewModel Smoke { get; private set; }
        [Import]
        public AirPumpViewModel AirPump { get; private set; }
        [Import]
        public PantographHSCBViewModel Pantograph { get; private set; }
        [Import]
        public MainMasterViewModel MainMaster { get; private set; }
        [Import]
        public EventInfoViewModel EventInfo { get; private set; }
        public ShenZhengController Controller { get; private set; }
        [Import]
        public BypassViewModel Bypass { get; private set; }
        [Import]
        public TractionLockViewModel TractionLock { get; private set; }
        [Import]
        public PasswordSettingViewModel PasswordSetting { get; private set; }
        [Import]
        public BorderCastViewModel BorderCast { get; private set; }
        [Import]
        public HelpViewModel HelpViewModel { get; private set; }
        [Import]
        public ReSetViewModel ReSetViewModel { get; private set; }

        public static ShenZhenViewModel Instance;
        [ImportingConstructor]
        public ShenZhenViewModel(ShenZhengController controller, IEventAggregator eventAggregator)
        {
            Controller = controller;
            controller.ViewModel = this;
            EventAggregator = eventAggregator;
            EventAggregator.GetEvent<SendDataEvent<float>>().Subscribe((args) =>
            {
                GlobalParam.Instance.InitPara.CommunicationDataService.WriteService.ChangeFloat(args.Index, args.Value);
                if (args.IsClear)
                {
                    Task.Factory.StartNew(() =>
                    {
                        Thread.Sleep(TimeSpan.FromMilliseconds(200));
                        GlobalParam.Instance.InitPara.CommunicationDataService.WriteService.ChangeFloat(args.Index, 0);
                    });
                }
            });
            EventAggregator.GetEvent<SendDataEvent<bool>>().Subscribe((args =>
           {
               GlobalParam.Instance.InitPara.CommunicationDataService.WriteService.ChangeBool(args.Index, args.Value);
               if (args.IsClear)
               {
                   ThreadPool.QueueUserWorkItem((state =>
                  {
                      Thread.Sleep(200);
                      GlobalParam.Instance.InitPara.CommunicationDataService.WriteService.ChangeBool(args.Index, false);
                  }));
               }
           }));
            Navigator = new DelegateCommand<string>((s =>
            {
                EventAggregator.GetEvent<NavigatorToEvent>().Publish(new NavigatorToEvent.NavigatorArgs() { Names = s });
            }));
            Instance = this;
            EventAggregator.GetEvent<NavigatorToEvent>()
                .Publish(new NavigatorToEvent.NavigatorArgs() { Names = ViewNames.BlackView });
            //controller.RequestNavigator(ViewNames.BlackView);
        }
        public ICommand Navigator { get; private set; }
        protected IEventAggregator EventAggregator;
        public void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {
            var type = typeof(ShenZhenViewModel);
            foreach (var tmp in type.GetProperties().Select(info => info.GetValue(this, null) as IStatusChanged))
            {
                tmp?.Changed(sender, args);
            }
            var index = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.MMI亮屏];
            if (args.NewValue.ContainsKey(index))
            {

                EventAggregator.GetEvent<NavigatorToEvent>()
              .Publish(new NavigatorToEvent.NavigatorArgs() { Names = args.NewValue[index] ? ViewNames.DoorView : ViewNames.BlackView });
                //Controller.RequestNavigator(args.NewValue[index] ? ViewNames.DoorView : ViewNames.BlackView);
                if (!args.NewValue[index])
                {
                    Clear();
                }
            }

        }

        public void Changed(object sender, CommunicationDataChangedArgs<float> args)
        {
            var type = typeof(ShenZhenViewModel);
            foreach (var tmp in type.GetProperties().Select(info => info.GetValue(this, null) as IStatusChanged))
            {
                tmp?.Changed(sender, args);
            }

        }

        public void Clear()
        {
            var type = typeof(ShenZhenViewModel);
            foreach (var tmp in type.GetProperties().Select(info => info.GetValue(this, null) as IClear))
            {
                tmp?.Clear();
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Controller.UpdateTitleName(navigationContext);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        /// <summary>
        /// bool 值变化
        /// </summary>
        /// <param name="sender"/><param name="dataChangedArgs"/>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            Changed(sender, dataChangedArgs);
        }

        /// <summary>
        /// float值变化
        /// </summary>
        /// <param name="sender"/><param name="dataChangedArgs"/>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            Changed(sender, dataChangedArgs);
        }

        /// <summary>
        /// data值变化
        /// </summary>
        /// <param name="sender"/><param name="dataChangedArgs"/>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
        }
    }
}