using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Engine.LCDM.HDX3.Resource.FonSource;
using Engine.LCDM.HXD3.Enums;

namespace Engine.LCDM.HXD3.Resource
{
    /// <summary>
    /// LCDMResourceManager.xaml 的交互逻辑
    /// </summary>
    public partial class LCDMResourceManager
    {
        public static LCDMResourceManager Instance { get; private set; }
        static LCDMResourceManager()
        {
            Instance = new LCDMResourceManager();
        }

        public LCDMResourceManager()
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
                                "pack://application:,,,/Engine.LCDM.HXD3;component/Resource/FonSource/CH/LCDMStringReosurceCH.xaml",
                                UriKind.Absolute)
                    });
        }

        public static void ChangedResource(ResourceDictionary reource)
        {
            var tage =
                Instance.MergedDictionaries.FirstOrDefault(
                    f =>
                        f.Source.ToString().Contains(typeof(LCDMStringReosurceEN).Name) ||
                        f.Source.ToString().Contains(typeof(LCDMStringReosurceCH).Name));
            Instance.MergedDictionaries.Remove(tage);
            Instance.MergedDictionaries.Add(reource);
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
                                  "pack://application:,,,/Engine.LCDM.HXD3;component/Resource/FonSource/EN/LCDMStringReosurceEN.xaml",
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
