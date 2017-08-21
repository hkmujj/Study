using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Motor.ATP.Domain.Interface;
using Motor.ATP.Domain.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Domain.Model
{
    public class ForecastInformation : ATPPartialBase, IForecastInformation
    {
        public ForecastInformation(ATPDomain parent)
            : base(parent)
        {
            ForecastInformationItems = Enumerable.Range(0, 13).Select(s => new ForecastInformationItem()).Cast<IForecastInformationItem>().ToList().AsReadOnly();
            DecelerateInfos = new ObservableCollection<ISpeedChangeInfo>(Enumerable.Range(0, 10).Select(s => new SpeedChangeInfo(SpeedChangeType.None)));
        }

        private static readonly int MaxRefreshDelayCount = 10;

        private int m_RefreshDelayCount;
        private ReadOnlyCollection<IForecastInformationItem> m_ForecastInformationItems;

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
                m_ForecastInformationItems = value;
                RaisePropertyChanged(() => ForecastInformationItems);
                RaisePropertyChanged(() => UpForecastInformationItems);
                RaisePropertyChanged(() => DowForecastInformationItems);
                RaisePropertyChanged(() => CmdForecastInformationItems);
            }
        }

        public IEnumerable<IForecastInformationItem> UpForecastInformationItems
        {
            get { return ForecastInformationItems.Take(10).Where((w, i) => i % 2 == 0); }
        }

        public IEnumerable<IForecastInformationItem> DowForecastInformationItems
        {
            get { return ForecastInformationItems.Take(10).Where((w, i) => i % 2 == 1); }
        }

        /// <summary>
        /// 命令区的
        /// </summary>
        public IEnumerable<IForecastInformationItem> CmdForecastInformationItems
        {
            get { return ForecastInformationItems.Skip(10).Take(3); }
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