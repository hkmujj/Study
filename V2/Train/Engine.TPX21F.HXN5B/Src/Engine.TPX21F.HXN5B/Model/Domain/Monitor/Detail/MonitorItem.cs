using System;
using System.Diagnostics;
using CommonUtil.Util;
using Engine.TPX21F.HXN5B.Model.ConfigModel;
using Engine.TPX21F.HXN5B.Model.Domain.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.Monitor.Detail
{
    [DebuggerDisplay("Name={ItemConfig.Name} Content={Content}")]
    public class MonitorItem : NotificationObject
    {
        private object m_Content;

        [DebuggerStepThrough]
        public MonitorItem(MonitorItemConfig name)
        {
            ItemConfig = name;
            try
            {
                if (ItemConfig.ValueTypeString == "Float")
                {
                    ContentType = typeof(float);
                }
                else if (!string.IsNullOrWhiteSpace(ItemConfig.ValueTypeString))
                {
                    ContentType = GetType().Assembly.GetType(ItemConfig.ValueTypeString);
                }
            }
            catch (Exception e)
            {
                AppLog.Error("Can not found type {0}, {1}", ItemConfig.ValueTypeString, e);
            }
        }

        public virtual MonitorItemConfig ItemConfig { private set; get; }

        public object Content
        {
            private set
            {
                if (Equals(value, m_Content))
                {
                    return;
                }

                m_Content = value;
                RaisePropertyChanged(() => Content);
                //RaisePropertyChanged(() => ContentMonitorItemState);
                //RaisePropertyChanged(() => ContentMonitorControlSwitch);
                //RaisePropertyChanged(() => ContentFloat);
                //RaisePropertyChanged(() => ContentString);
                //RaisePropertyChanged(() => CutableItemState);
            }
            get { return m_Content; }
        }

        public MonitorItemState ContentMonitorItemState
        {
            set
            {
                if (Content is MonitorItemState && value == (MonitorItemState) Content)
                {
                    return;
                }

                Content = value;
                RaisePropertyChanged(() => ContentMonitorItemState);
            }
            get { return Content as MonitorItemState? ?? MonitorItemState.Open; }
        }

        public MonitorControlSwitch ContentMonitorControlSwitch
        {
            set
            {
                if (Content is MonitorControlSwitch && value == (MonitorControlSwitch) Content)
                {
                    return;
                }

                Content = value;
                RaisePropertyChanged(() => ContentMonitorControlSwitch);
            }
            get { return Content as MonitorControlSwitch? ?? MonitorControlSwitch.Move; }
        }

        public CutableItemState CutableItemState
        {
            set
            {
                if (Content is CutableItemState && value == (CutableItemState) Content)
                {
                    return;
                }

                Content = value;
                RaisePropertyChanged(() => CutableItemState);
            }
            get { return Content as CutableItemState? ?? CutableItemState.PutInto; }
        }


        public string ContentString
        {
            set
            {
                if (Content != null && value == (string) Content)
                {
                    return;
                }

                Content = value;
                RaisePropertyChanged(() => ContentString);
            }
            get { return Content as string; }
        }

        public Type ContentType { private set; get; }
    }

    [DebuggerDisplay("Name={ItemConfig.Name} ")]
    public class AssistMachineMonitorItem : MonitorItem
    {
        private string m_TBC;
        private string m_RFC2;
        private string m_RFC1;
        private string m_COMP2;
        private string m_COMP1;

        public MonitorAssistMachineItemConfig AssistMachineItemConfig
        {
            get { return (MonitorAssistMachineItemConfig) ItemConfig; }
        }

        public string COMP1
        {
            set
            {
                if (value.Equals(m_COMP1))
                {
                    return;
                }

                m_COMP1 = value;
                RaisePropertyChanged(() => COMP1);
            }
            get { return m_COMP1; }
        }

        public string COMP2
        {
            get { return m_COMP2; }
            set
            {
                if (value.Equals(m_COMP2))
                {
                    return;
                }

                m_COMP2 = value;
                RaisePropertyChanged(() => COMP2);
            }
        }

        public string RFC1
        {
            get { return m_RFC1; }
            set
            {
                if (value.Equals(m_RFC1))
                {
                    return;
                }

                m_RFC1 = value;
                RaisePropertyChanged(() => RFC1);
            }
        }

        public string RFC2
        {
            get { return m_RFC2; }
            set
            {
                if (value.Equals(m_RFC2))
                {
                    return;
                }

                m_RFC2 = value;
                RaisePropertyChanged(() => RFC2);
            }
        }

        public string TBC
        {
            get { return m_TBC; }
            set
            {
                if (value.Equals(m_TBC))
                {
                    return;
                }

                m_TBC = value;
                RaisePropertyChanged(() => TBC);
            }
        }

        public AssistMachineMonitorItem(MonitorAssistMachineItemConfig name) : base(name)
        {
        }
    }
}