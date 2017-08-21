using System.Collections.Generic;
using MMI.Facility.Interface.Data;
using Subway.ShenZhenLine11.Config;
using Subway.ShenZhenLine11.Controller;
using Subway.ShenZhenLine11.Info;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Subway.ShenZhenLine11.Constance;
using Subway.ShenZhenLine11.Event;
using Subway.ShenZhenLine11.Resource;

namespace Subway.ShenZhenLine11.ViewModels
{
    [Export(typeof(EventInfoViewModel))]
    public class EventInfoViewModel : SubViewModelBase
    {
        private ObservableCollection<EventInfo> m_EventInfos;
        private int m_FaultCount;
        private int m_CurrentPage;
        private int m_MaxPage;
        private int m_TotalNum;
        private string m_FaultConfirm;
        private string m_Temporary;
        public EventInfo CurrenInfo { get; private set; }
        public EventInfoController Controller { get; private set; }

        [ImportingConstructor()]
        public EventInfoViewModel(EventInfoController controller)
        {
            Controller = controller;
            controller.ViewModel = this;
            MaxPage = 1;
            CurrentPage = 1;
            Manager = new EventManager();
            Manager.Load(GlobalParam.Instance.InitPara.AppConfig.AppPaths.ConfigDirectory);
            Manager.CurrentChanged += (args, args1) =>
            {
                var index = 1;
                foreach (var info in args1)
                {
                    info.Number = index;
                    index++;
                }
                CurrenInfo = Manager.Current.Where(w => !w.Confirm).OrderBy(o => o.Time).FirstOrDefault();
                if (CurrenInfo != null)
                {
                    FaultConfirm = $"{CurrenInfo.FaultNum.ToString("0000")}    {CurrenInfo.Time.ToString("yyyy-MM-dd hh:mm:ss")}  {CurrenInfo.FaultContent}";
                }
                else
                {
                    FaultConfirm = "";
                }

                EventLevel = Manager.GetHightLevel();
                if (EventLevel > EventLevel.Light && EventLevel != EventLevel.Temporary)
                {
                    ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<SendDataEvent<bool>>().Publish(new SendDataEnvetArgs<bool>()
                    {
                        Index = GlobalParam.Instance.IndexConfig.OutBoolDescriptionDictionary[OutBoolKeys.故障声音],
                        Value = true,
                    });
                }
                else
                {
                    ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<SendDataEvent<bool>>().Publish(new SendDataEnvetArgs<bool>()
                    {
                        Index = GlobalParam.Instance.IndexConfig.OutBoolDescriptionDictionary[OutBoolKeys.故障声音],
                        Value = false,
                    });
                }
                EventInfos = new ObservableCollection<EventInfo>(args1);
            };
            Manager.CurrentPageChanged += (args, args1) =>
            {
                CurrentPage = args1;
            };
            Manager.MaxPageChanged += (args, args1) =>
            {
                MaxPage = args1;
            };
            Manager.MaxNumChanged += (args, args1) =>
            {
                TotalNum = args1;
            };
        }

        public string Temporary
        {
            get { return m_Temporary; }
            set
            {
                if (value == m_Temporary)
                {
                    return;
                }
                m_Temporary = value;
                RaisePropertyChanged(() => Temporary);
            }
        }

        public string FaultConfirm
        {
            get { return m_FaultConfirm; }
            set
            {
                if (value == m_FaultConfirm)
                {
                    return;
                }
                m_FaultConfirm = value;
                RaisePropertyChanged(() => FaultConfirm);
            }
        }

        public int MaxPage
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

        public int TotalNum
        {
            get { return m_TotalNum; }

            set
            {
                if (value == m_TotalNum)
                {
                    return;
                }
                m_TotalNum = value;
                RaisePropertyChanged(() => TotalNum);
            }
        }

        public int CurrentPage
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

        public int FaultCount
        {
            get { return m_FaultCount; }

            set
            {
                if (value == m_FaultCount)
                {
                    return;
                }
                m_FaultCount = value;
                RaisePropertyChanged(() => FaultCount);
            }
        }

        public EventManager Manager { get; private set; }

        public EventLevel EventLevel
        {
            get { return m_EventLevel; }
            set
            {
                if (value == m_EventLevel)
                {
                    return;
                }
                m_EventLevel = value;
                RaisePropertyChanged(() => EventLevel);
            }
        }

        public ObservableCollection<EventInfo> EventInfos
        {
            get { return m_EventInfos; }

            set
            {
                if (Equals(value, m_EventInfos))
                {
                    return;
                }
                m_EventInfos = value;
                RaisePropertyChanged(() => EventInfos);
            }
        }

        private readonly List<string> m_TemporaryFault = new List<string>();
        private EventLevel m_EventLevel;

        private void TemChanged()
        {
            if (m_TemporaryFault.Count != 0)
            {
                Temporary = m_TemporaryFault[0];
                ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToEvent>().Publish(new NavigatorToEvent.NavigatorArgs() { Names = ViewNames.TemporaryEventView });
                //Controller.RequestNavigator(ViewNames.TemporaryEventView);
            }
            else
            {
                ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToEvent>().Publish(new NavigatorToEvent.NavigatorArgs() { Names = ViewNames.MainShell });
                //Controller.RequestNavigator(ViewNames.MainShell);
            }
        }
        public override void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {
            foreach (var b in args.NewValue.Where(b => Manager.AllInfo.ContainsKey(b.Key)))
            {
                if (Manager.AllInfo[b.Key].EventLevel == EventLevel.Temporary)
                {
                    if (b.Value)
                    {
                        m_TemporaryFault.Add(Manager.AllInfo[b.Key].FaultContent);
                        TemChanged();
                    }
                    else
                    {
                        m_TemporaryFault.Remove(Manager.AllInfo[b.Key].FaultContent);
                        TemChanged();
                    }
                }
                else
                {
                    if (b.Value)
                    {
                        Manager.Add(b.Key);
                    }
                    else
                    {
                        Manager.Remove(b.Key);
                    }
                }
            }
            base.Changed(sender, args);
        }
    }
}