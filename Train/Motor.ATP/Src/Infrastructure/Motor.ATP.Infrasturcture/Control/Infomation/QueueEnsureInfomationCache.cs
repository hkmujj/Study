using System.Collections.Generic;
using System.Linq;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Model.Infomation;

namespace Motor.ATP.Infrasturcture.Control.Infomation
{
    public class QueueEnsureInfomationCache : IEnsureInfomationCache
    {
        private readonly List<IInfomationItem> m_InfomationItemContents;

        public QueueEnsureInfomationCache()
        {
            m_InfomationItemContents = new List<IInfomationItem>();
        }

        /// <summary>
        /// 清除所有
        /// </summary>
        public void Clear()
        {
            m_InfomationItemContents.Clear();
        }

        public void Add(IInfomationItem infomationItem)
        {
            if (infomationItem != null && !infomationItem.Content.IsOnlyTextInfo())
            {
                if (infomationItem.Content.NextControlType != ControlType.Unknown)
                {
                    // 如果有下一模式，则优先级最低。
                    var last = m_InfomationItemContents.LastOrDefault();
                    if (last != null && last.Content.NextControlType != ControlType.Unknown)
                    {
                        last =
                            m_InfomationItemContents.Last(l => l.Content.NextControlType != ControlType.Unknown);
                        m_InfomationItemContents.Insert(m_InfomationItemContents.IndexOf(last), infomationItem);
                    }
                    else
                    {
                        m_InfomationItemContents.Add(infomationItem);
                    }
                }
                else
                {
                    m_InfomationItemContents.Add(infomationItem);
                }
            }
        }

        public void Remove(IInfomationItem infomationItem)
        {
            m_InfomationItemContents.Remove(m_InfomationItemContents.FirstOrDefault(f => f.Content == infomationItem.Content));
        }

        public IInfomationItem GetCurrentEnsuringItemContent()
        {
            return m_InfomationItemContents.FirstOrDefault();
        }
    }
}