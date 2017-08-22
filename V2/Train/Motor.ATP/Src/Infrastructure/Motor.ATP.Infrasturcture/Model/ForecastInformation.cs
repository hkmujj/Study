using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Infrasturcture.Model
{
    public class ForecastInformation : ATPPartialBase, IForecastInformation
    {
        public ForecastInformation(ATPDomain parent)
            : base(parent)
        {
            ForecastInformationItems =
                Enumerable.Range(0, MaxRefreshDelayCount + MaxCmdItemCount)
                    .Select(s => new ForecastInformationItem())
                    .Cast<IForecastInformationItem>()
                    .ToList()
                    .AsReadOnly();
            DecelerateInfos =
                new ObservableCollection<ISpeedChangeInfo>(
                    Enumerable.Range(0, MaxRefreshDelayCount).Select(s => new SpeedChangeInfo(SpeedChangeType.None)));
        }

        /// <summary>
        /// 最大的预告信息个数 = 10
        /// </summary>
        protected const int MaxRefreshDelayCount = 10;

        /// <summary>
        /// 最大的命令显示个数 = 3
        /// </summary>
        protected const int MaxCmdItemCount = 3;

        private int m_RefreshDelayCount;
        private ReadOnlyCollection<IForecastInformationItem> m_ForecastInformationItems;
        private IForecastInformationItem m_CmdIsolatedItem;

        /// <summary>
        /// 所有预告信息
        /// </summary>
        public ReadOnlyCollection<IForecastInformationItem> ForecastInformationItems
        {
            get { return m_ForecastInformationItems; }
            private set
            {
                if (Equals(value, m_ForecastInformationItems))
                {
                    return;
                }

                if (m_ForecastInformationItems != null)
                {
                    foreach (var it in m_ForecastInformationItems.Skip(MaxRefreshDelayCount))
                    {
                        it.PropertyChanged -= CmdForecastInformationItemOnPropertyChanged;
                    }
                }

                m_ForecastInformationItems = value;
                if (m_ForecastInformationItems != null)
                {
                    foreach (var it in m_ForecastInformationItems.Skip(MaxRefreshDelayCount))
                    {
                        it.PropertyChanged += CmdForecastInformationItemOnPropertyChanged;
                    }
                }

                RaisePropertyChanged(() => ForecastInformationItems);
                RaisePropertyChanged(() => UpForecastInformationItems);
                RaisePropertyChanged(() => DowForecastInformationItems);
                RaisePropertyChanged(() => CmdForecastInformationItems);
                RaisePropertyChanged(() => CmdIsolatedItem);
            }
        }

        private void CmdForecastInformationItemOnPropertyChanged(object sender,
            PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateCmdIsolatedItem();
        }

        [Browsable(false)]
        public IEnumerable<IForecastInformationItem> UpForecastInformationItems
        {
            get { return ForecastInformationItems.Take(MaxRefreshDelayCount).Where((w, i) => i%2 == 0); }
        }

        [Browsable(false)]
        public IEnumerable<IForecastInformationItem> DowForecastInformationItems
        {
            get { return ForecastInformationItems.Take(MaxRefreshDelayCount).Where((w, i) => i%2 == 1); }
        }

        /// <summary>
        /// 命令区的
        /// </summary>
        [Browsable(false)]
        public virtual IEnumerable<IForecastInformationItem> CmdForecastInformationItems
        {
            get { return ForecastInformationItems.Skip(MaxRefreshDelayCount).Take(MaxCmdItemCount); }
        }

        /// <summary>
        /// 隔离
        /// </summary>
        public IForecastInformationItem CmdIsolatedItem
        {
            private set
            {
                if (Equals(value, m_CmdIsolatedItem))
                {
                    return;
                }

                m_CmdIsolatedItem = value;
                RaisePropertyChanged(() => CmdIsolatedItem);
            }
            get { return m_CmdIsolatedItem; }
        }

        private void UpdateCmdIsolatedItem()
        {
            CmdIsolatedItem = ForecastInformationItems.Skip(MaxRefreshDelayCount).FirstOrDefault(
                f => f.Type == ForecastInformationType.Isolated && Math.Abs(f.Distance) < float.Epsilon);
        }

        /// <summary>
        /// 减速信息
        /// </summary>
        public ObservableCollection<ISpeedChangeInfo> DecelerateInfos { get; private set; }

        public event EventHandler DecelerateInfomationItemItemsChanged;

        public virtual void UpdateDecelerateInfomationItems(IEnumerable<ISpeedChangeInfo> decelerateInfomationItems)
        {
            throw new NotSupportedException();
            //DecelerateInfos.Clear();
            //DecelerateInfos.AddRange(decelerateInfomationItems);
            //OnDecelerateInfomationItemItemsChanged();
        }

        protected virtual void OnDecelerateInfomationItemItemsChanged()
        {
            if (++m_RefreshDelayCount <= MaxRefreshDelayCount)
            {
                return;
            }

            m_RefreshDelayCount = 0;
            var handler = DecelerateInfomationItemItemsChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}