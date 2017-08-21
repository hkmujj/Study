using LightRail.HMI.SZLHLF.Enum;
using LightRail.HMI.SZLHLF.Resources.FontSource;
using System;
using System.Linq;
using System.Windows;

namespace LightRail.HMI.SZLHLF.Resources
{
    /// <summary>
    /// SZLHLFResourceManager.xaml 的交互逻辑
    /// </summary>
    public partial class SZLHLFResourceManager
    {
        public static SZLHLFResourceManager Instance { get; private set; }

        static SZLHLFResourceManager()
        {
            Instance = new SZLHLFResourceManager();
        }
        public SZLHLFResourceManager()
        {
            InitializeComponent();
        }

        public static void ChangedLanguage(Language language)
        {
            switch (language)
            {
                case Language.Ch:
                    ChangedResource(GetChResouce());
                    break;
                case Language.En:
                    ChangedResource(GetEnResource());
                    break;
                default:
                    throw new ArgumentOutOfRangeException("language", language, null);
            }
        }

        private static ResourceDictionary m_ChResourceDictionary;
        private static ResourceDictionary m_EnResourceDictionary;
        public static ResourceDictionary GetChResouce()
        {
            return m_ChResourceDictionary ??
                   (m_ChResourceDictionary =
                    new ResourceDictionary()
                    {
                        Source =
                            new Uri(
                                "pack://application:,,,/LightRail.HMI.SZLHLF;component/Resources/FontSource/CH/SZLHLFStringResourceCH.xaml",
                                UriKind.Absolute)
                    });
        }

        public static void ChangedResource(ResourceDictionary resource)
        {
            var tage =
                Instance.MergedDictionaries.FirstOrDefault(
                    f =>
                        f.Source.ToString().Contains(typeof(SZLHLFStringResourceEN).Name) ||
                        f.Source.ToString().Contains(typeof(SZLHLFStringResourceCH).Name));
            Instance.MergedDictionaries.Remove(tage);
            Instance.MergedDictionaries.Add(resource);
            OnResourceChanged(Instance);
        }
        public static ResourceDictionary GetEnResource()
        {
            return m_EnResourceDictionary ??
                     (m_EnResourceDictionary =
                      new ResourceDictionary()
                      {
                          Source =
                              new Uri(
                                  "pack://application:,,,/LightRail.HMI.SZLHLF;component/Resources/FontSource/EN/SZLHLFStringResourceEN.xaml",
                                  UriKind.RelativeOrAbsolute)
                      });
        }
        public static event EventHandler ResourceChanged;

        protected static void OnResourceChanged(object sender = null)
        {
            if (ResourceChanged != null)
            {
                ResourceChanged.Invoke(sender, EventArgs.Empty);
            }

        }
    }
}
