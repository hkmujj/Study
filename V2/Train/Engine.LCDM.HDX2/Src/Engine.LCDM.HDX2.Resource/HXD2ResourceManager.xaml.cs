using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Documents;

namespace Engine.LCDM.HDX2.Resource
{
    /// <summary>
    /// HXD2ResourceManager.xaml 的交互逻辑
    /// </summary>
    public partial class HXD2ResourceManager
    {
        public static HXD2ResourceManager Instance { get { return ObjectCollection.FirstOrDefault(); } }

        private static readonly List<HXD2ResourceManager> ObjectCollection = new List<HXD2ResourceManager>();
        private static ResourceDictionary m_ChResourceDictionary;
        private static ResourceDictionary m_EnResourceDictionary;

        public static event EventHandler ResourceChanged;

        private static ResourceDictionary GetChResourceDictionary()
        {
            return m_ChResourceDictionary ??
                   (m_ChResourceDictionary =
                       new ResourceDictionary()
                       {
                           Source = new Uri("pack://application:,,,/Engine.LCDM.HDX2.Resource;component/CH/HXD2ResourceCH.xaml", UriKind.RelativeOrAbsolute)
                       });
        }

        private static ResourceDictionary GetEnResourceDictionary()
        {
            return m_EnResourceDictionary ??
                   (m_EnResourceDictionary =
                       new ResourceDictionary()
                       {
                           Source = new Uri("pack://application:,,,/Engine.LCDM.HDX2.Resource;component/EN/HXD2ResourceEN.xaml", UriKind.RelativeOrAbsolute)
                       });
        }

        public HXD2ResourceManager()
        {
            InitializeComponent();
            ObjectCollection.Add(this);
        }

        public static void ChangeResources(ResourceType type)
        {
            switch (type)
            {
                case ResourceType.Ch:
                    ChangeResource(GetChResourceDictionary());
                    break;
                case ResourceType.En:
                    ChangeResource(GetEnResourceDictionary());
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }

        private static void ChangeResource(ResourceDictionary resourceDictionary)
        {
            var manager = ObjectCollection.FirstOrDefault();
            if (manager != null)
            {
                var target =
                    manager.MergedDictionaries.FirstOrDefault(
                        f =>
                            f.Source.ToString().Contains(typeof (HXD2ResourceCH).Name) ||
                            f.Source.ToString().Contains(typeof(HXD2ResourceEN).Name));
                manager.MergedDictionaries.Remove(target);
                manager.MergedDictionaries.Add(resourceDictionary);
                OnResourceChanged(manager);    
            }

            if (ObjectCollection.Count > 1)
            {
                ObjectCollection.Clear();
                ObjectCollection.Add(manager);
            }
        }

        private static void OnResourceChanged(object sender = null)
        {
            var handler = ResourceChanged;
            if (handler != null)
            {
                handler(sender, EventArgs.Empty);
            }
        }
    }
}
