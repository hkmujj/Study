using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ATP200C.MainView
{
    public class TextInfoManager
    {
        private readonly List<Text_Info> m_HappenedInfoCollection;

        private readonly List<Text_Info> m_CurrentShowTextInfoCollection;
        private volatile ConfigInfo m_CurrentConfirmingInfo;

        public ReadOnlyCollection<Text_Info> HappenedInfoCollection { private set; get; }

        /// <summary>
        /// 越后面的越新
        /// </summary>
        public ReadOnlyCollection<Text_Info> CurrentShowTextInfoCollection { private set; get; }

        public static TextInfoManager Instance { private set; get; }

        public int MaxShowCount { set; get; }

        /// <summary>
        /// 正在确认
        /// </summary>
        public bool IsConfirming { private set; get; }

        public ConfigInfo CurrentConfirmingInfo
        {
            set
            {
                m_CurrentConfirmingInfo = value;
                IsConfirming = m_CurrentConfirmingInfo != null;
            }
            get { return m_CurrentConfirmingInfo; }
        }

        public bool CanGoBack
        {
            get
            {
                if (IsConfirming)
                {
                    return false;
                }

                if (m_CurrentShowTextInfoCollection.Any()
                    && m_HappenedInfoCollection.Any()
                    && m_CurrentShowTextInfoCollection.Count < m_HappenedInfoCollection.Count
                    && m_CurrentShowTextInfoCollection.First() != m_HappenedInfoCollection.First())
                {
                    return true;
                }
                return false;
            }
        }

        public bool CanGoForward
        {
            get
            {
                if (IsConfirming)
                {
                    return false;
                }

                if (m_HappenedInfoCollection.Any()
                    && m_CurrentShowTextInfoCollection.Count < m_HappenedInfoCollection.Count
                    && m_CurrentShowTextInfoCollection.LastOrDefault() != m_HappenedInfoCollection.Last())
                {
                    return true;
                }
                return false;
            }
        }

        static TextInfoManager()
        {
            Instance = new TextInfoManager();
        }

        public TextInfoManager()
        {
            MaxShowCount = 5;

            m_HappenedInfoCollection = new List<Text_Info>();
            m_CurrentShowTextInfoCollection = new List<Text_Info>();

            HappenedInfoCollection = new ReadOnlyCollection<Text_Info>(m_HappenedInfoCollection);
            CurrentShowTextInfoCollection = new ReadOnlyCollection<Text_Info>(m_CurrentShowTextInfoCollection);
        }

        public void GoBack()
        {
            if (CanGoBack)
            {
                var firstShow = m_CurrentShowTextInfoCollection.First();
                m_CurrentShowTextInfoCollection.Insert(0, m_HappenedInfoCollection[m_HappenedInfoCollection.IndexOf(firstShow) - 1]);
                if (m_CurrentShowTextInfoCollection.Count > MaxShowCount)
                {
                    m_CurrentShowTextInfoCollection.Remove(m_CurrentShowTextInfoCollection.Last());
                }
            }
        }

        public void Add(Text_Info text)
        {
            m_HappenedInfoCollection.Add(text);

            while (CanGoForward)
            {
                GoForward();
            }
        }

        public void GoForward()
        {
            if (CanGoForward)
            {
                var last = m_HappenedInfoCollection.Last();
                m_CurrentShowTextInfoCollection.Add(last);
                if (m_CurrentShowTextInfoCollection.Count > MaxShowCount)
                {
                    m_CurrentShowTextInfoCollection.RemoveAt(0);
                }
            }
        }

        public void Clear()
        {
            m_HappenedInfoCollection.Clear();
            m_CurrentShowTextInfoCollection.Clear();
        }
    }
}