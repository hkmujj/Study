using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Subway.ShiJiaZhuangLine1.Interface;
using Subway.ShiJiaZhuangLine1.Interface.Model;
using Subway.ShiJiaZhuangLine1.Subsystem.Constant;
using Subway.ShiJiaZhuangLine1.Subsystem.ViewModels;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Model
{
    public class EventPageModel : ViewModelBase, IEventPageModel
    {
        private string m_MaxPage;
        private string m_CurrentPage;
        private string m_AllFaultNumber;
        private Visibility m_VisibilityFalut;
        private string m_CofirmLogic;
        private string m_SystemFaluet;
        private string m_Datetime;
        private string m_CarNumber;


        private List<EventInfo> Infos;
        private void EventPageModel_CourseStateChanged(object sender, CourseStateChangedArgs e)
        {
            switch (e.CourseService.CurrentCourseState)
            {
                case CourseState.Unknown:
                    break;
                case CourseState.Started:
                    break;
                case CourseState.Stoped:
                    Parent.EventMgr.Clear();
                    Infos.Clear();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ChangeBools(CommunicationDataChangedArgs<bool> args)
        {

            foreach (var key in args.NewValue.Keys)
            {
                if (Parent.EventMgr.AllData.ContainsKey(key) && args.NewValue[key])
                {
                    Parent.EventMgr.Add(key);

                    var tmpEvent = (EventInfo) Parent.EventMgr.AllData[key];

                    if (tmpEvent != null) Infos.Add(tmpEvent.Clone().ConverterToType<EventInfo>());
                }
                else if (Parent.EventMgr.AllData.ContainsKey(key) && !args.NewValue[key])
                {
                    var tmp = Infos.FirstOrDefault(f => f.LogicId == key);
                    Infos.Remove(tmp);
                }
            }
            GetCurrentMethod();

        }

        public void ChangeFloats(CommunicationDataChangedArgs<float> args)
        {

        }
        public string AllFaultNumber
        {
            get { return m_AllFaultNumber; }
            set
            {
                if (value == m_AllFaultNumber)
                {
                    return;
                }
                m_AllFaultNumber = value;
                RaisePropertyChanged(() => AllFaultNumber);
            }
        }

        public string CurrentPage
        {
            get { return m_CurrentPage; }
            set
            {
                if (value == m_CurrentPage)
                {
                    return;
                }
                m_CurrentPage = value;
                RaisePropertyChanged(() => CurrentPage);
            }
        }

        public string MaxPage
        {
            get { return m_MaxPage; }
            set
            {
                if (value == m_MaxPage)
                {
                    return;
                }
                m_MaxPage = value;
                RaisePropertyChanged(() => MaxPage);
            }
        }
        public Visibility VisibilityFalut
        {
            get { return m_VisibilityFalut; }
            set
            {
                if (value == m_VisibilityFalut)
                {
                    return;
                }
                m_VisibilityFalut = value;
                RaisePropertyChanged(() => VisibilityFalut);
            }
        }


        public ICommand GetCurentEvent { get; private set; }

        public ICommand Sort { get; private set; }
        public ICommand FirstPage { get; private set; }
        public ICommand NextPage { get; private set; }
        public ICommand LastPage { get; private set; }
        public ICommand GetCurrent { get; private set; }
        public ICommand Reset { get; private set; }
        public ICommand Cofirm { get; private set; }
        public ICommand NavigatorToEvenInfo { get; private set; }

        public string CarNumber
        {
            get { return m_CarNumber; }
            set
            {
                if (value == m_CarNumber)
                {
                    return;
                }
                m_CarNumber = value;
                RaisePropertyChanged(() => CarNumber);
            }
        }

        public string Datetime
        {
            get { return m_Datetime; }
            set
            {
                if (value == m_Datetime)
                {
                    return;
                }
                m_Datetime = value;
                RaisePropertyChanged(() => Datetime);
            }
        }

        public string SystemFaluet
        {
            get { return m_SystemFaluet; }
            set
            {
                if (value == m_SystemFaluet)
                {
                    return;
                }
                m_SystemFaluet = value;
                RaisePropertyChanged(() => SystemFaluet);
            }
        }

        public string CofirmLogic
        {
            get { return m_CofirmLogic; }
            set
            {
                if (value == m_CofirmLogic)
                {
                    return;
                }
                m_CofirmLogic = value;
                RaisePropertyChanged(() => CofirmLogic);
            }
        }

        public EventPageModel(IMMI parent)
            : base(parent)
        {
            NavigatorToEvenInfo = new DelegateCommand(NavigatorToEvenInfoMethod);
            FirstPage = new DelegateCommand(FirstPageMethos);
            Infos = new List<EventInfo>();
            Sort = new DelegateCommand<string>(SortMethod);
            NextPage = new DelegateCommand(NextPageMethod);
            LastPage = new DelegateCommand(LastPageMethod);
            GetCurrent = new DelegateCommand(GetCurrentMethod);
            GetCurentEvent = new DelegateCommand<string>(GetCurentEventMethod);
            Cofirm = new DelegateCommand(CofirmMethod);
            VisibilityFalut = Visibility.Hidden;
            Parent.EventMgr.MaxFaultChanged += () =>
            {
                AllFaultNumber = Parent.EventMgr.MaxFalut.ToString();
                GetCurrentMethod();
            };
            Parent.EventMgr.CurrentPageChanged += () =>
            {
                CurrentPage = Parent.EventMgr.CurrentPage.ToString();
                GetCurrentMethod();
            };
            Parent.EventMgr.MaxPageChanged += () =>
            {
                MaxPage = Parent.EventMgr.MaxPage.ToString();
                GetCurrentMethod();
            };
            Parent.EventMgr.CofirmChanged += () =>
            {
                var tmp = Parent.EventMgr.CurrentData.Where(w => Infos.Exists(e => e.LogicId == w.LogicId)).OrderBy(f => f.HappenDateTime).Where(w => !w.IsCofirm).ToList();
                ConfirmEvent = tmp.FirstOrDefault();
                VisibilityFalut = tmp.Count != 0 ? Visibility.Visible : Visibility.Hidden;
            };
            Parent.EventMgr.InitPara();
            Reset = new DelegateCommand(() =>
            {
                Parent.EventMgr.Reset();
            });

            SubsysParams.Instance.SubsystemInitParam.DataPackage.ServiceManager.GetService<ICourseService>().CourseStateChanged += EventPageModel_CourseStateChanged;
        }

        private void NavigatorToEvenInfoMethod()
        {
            EventInfo = ConfirmEvent;
            ServiceLocator.Current.GetInstance<ShellViewModel>().Controller.ShellContentNavigateCommand.Execute(ViewNames.EventInfoView);
            ServiceLocator.Current.GetInstance<IRegionManager>().RequestNavigate(RegionNames.ShellBottomRegion, ViewNames.EventInfoButtonView);
        }

        private void FirstPageMethos()
        {
            Parent.EventMgr.FristPage();
        }

        private void CofirmMethod()
        {
            if (ConfirmEvent != null)
            {
                Parent.EventMgr.Cofirm(ConfirmEvent.LogicId);
            }
        }

        private void GetCurentEventMethod(string obj)
        {
            EventInfo = EventsInfos.FirstOrDefault(f => f.LogicId == Convert.ToInt32(obj));
        }

        public IEventInfo EventInfo
        {
            get { return m_EventInfo; }
            set
            {
                if (Equals(value, m_EventInfo))
                {
                    return;
                }
                m_EventInfo = value;
                RaisePropertyChanged(() => EventInfo);
            }
        }

        public IEventInfo ConfirmEvent
        {
            get { return m_ConfirmEvent; }
            set
            {
                if (Equals(value, m_ConfirmEvent))
                {
                    return;
                }
                m_ConfirmEvent = value;
                RaisePropertyChanged(() => ConfirmEvent);
            }
        }

        public ObservableCollection<IEventInfo> EventsInfos
        {
            get { return m_EventsInfos; }
            set
            {
                if (Equals(value, m_EventsInfos))
                {
                    return;
                }
                m_EventsInfos = value;
                RaisePropertyChanged(() => EventsInfos);
            }
        }

        private void GetCurrentMethod()
        {
            var tmp = Parent.EventMgr.GetCurrent().OrderByDescending(o => o.HappenDateTime).ToList();


            EventsInfos = new ObservableCollection<IEventInfo>(tmp);
            foreach (var eventsInfo in EventsInfos)
            {
                eventsInfo.Number = EventsInfos.IndexOf(eventsInfo) + 1;
            }
            AllFaultNumber = Parent.EventMgr.MaxFalut.ToString();
            CurrentPage = Parent.EventMgr.CurrentPage.ToString();
            MaxPage = Parent.EventMgr.MaxPage.ToString();

            if (Infos.Any(a => a.Level == EventLevel.Critical))
            {
                TitleEventLevel = EventLevel.Critical;
            }
            else if (Infos.Any(a => a.Level == EventLevel.Medium))
            {
                TitleEventLevel = EventLevel.Medium;
            }
            else if (Infos.Any(a => a.Level == EventLevel.Light))
            {
                TitleEventLevel = EventLevel.Light;
            }
            else
            {
                TitleEventLevel = EventLevel.Normal;
            }

        }
        private EventLevel m_TitleEventLevel;
        private ObservableCollection<IEventInfo> m_EventsInfos;
        private IEventInfo m_EventInfo;
        private IEventInfo m_ConfirmEvent;

        /// <summary>
        /// 标题故障等级
        /// </summary>
        public EventLevel TitleEventLevel
        {
            get
            {
                return m_TitleEventLevel;
            }
            set
            {
                if (value.Equals(m_TitleEventLevel))
                {
                    return;
                }
                m_TitleEventLevel = value;
                RaisePropertyChanged(() => TitleEventLevel);
            }
        }

        private string TimeToStrings(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd hh:mm:ss");
        }
        private void LastPageMethod()
        {
            Parent.EventMgr.LastPage();
        }

        private void NextPageMethod()
        {
            Parent.EventMgr.NextPage();
        }

        private void ClearData()
        {
            var tmp = typeof(EventPageModel);
            foreach (var info in tmp.GetProperties())
            {
                if (info.PropertyType == typeof(string))
                {
                    info.SetValue(this, "", null);
                }
                if (info.PropertyType == typeof(int))
                {
                    info.SetValue(this, 0, null);
                }
            }
        }
        private void SortMethod(string obj)
        {
            Enum.TryParse(obj, true, out EventLevel level);
            Parent.EventMgr.Sort(level);
            ClearData();
            GetCurrentMethod();
        }
    }
}