using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism;
using Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Infrasturcture.Model
{
    public class GradientInfomation : ATPPartialBase, IGradientInfomation
    {
        private static readonly int MaxRefreshDelayCount = 10;

        private int m_RefreshDelayCount;

        public GradientInfomation(ATPDomain parent)
            : base(parent)
        {
            GradientInfomationItemItems =
                new ObservableCollection<IGradientInfomationItem>(
                    Enumerable.Range(0, MaxRefreshDelayCount).Select(s => new SlopeInfomationItem(this, GradientType.None)));
        }

        public event EventHandler GradientInfomationItemItemsChanged;

        public ObservableCollection<IGradientInfomationItem> GradientInfomationItemItems { get; private set; }


        public void UpdateGradientInfomationItems(IEnumerable<IGradientInfomationItem> gradientInfomationItems)
        {
            GradientInfomationItemItems.Clear();
            GradientInfomationItemItems.AddRange(gradientInfomationItems);
            OnGradientInfomationItemItemsChanged();
        }

        protected virtual void OnGradientInfomationItemItemsChanged()
        {
            if (++m_RefreshDelayCount <= MaxRefreshDelayCount)
            {
                return;
            }
            m_RefreshDelayCount = 0;
            var handler = GradientInfomationItemItemsChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}