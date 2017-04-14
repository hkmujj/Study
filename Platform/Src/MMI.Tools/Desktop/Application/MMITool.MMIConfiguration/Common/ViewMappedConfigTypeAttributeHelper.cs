using System.Collections.ObjectModel;
using System.Linq;
using MMITool.Addin.MMIConfiguration.Attributes;
using MMITool.Addin.MMIConfiguration.View.ConfigureContent;

namespace MMITool.Addin.MMIConfiguration.Common
{
    public class ViewMappedConfigTypeAttributeHelper
    {
        private static ReadOnlyCollection<ViewMappedConfigTypeAttribute> m_ViewMappedConfigTypeCollection;

        public static ReadOnlyCollection<ViewMappedConfigTypeAttribute> ViewMappedConfigTypeCollection
        {
            get { return m_ViewMappedConfigTypeCollection ?? InitalizeViewMappedConfigTypeCollection(); }
        }

        private static ReadOnlyCollection<ViewMappedConfigTypeAttribute> InitalizeViewMappedConfigTypeCollection()
        {
            m_ViewMappedConfigTypeCollection =
                typeof (SystemConfigView).Assembly.GetTypes()
                    .Select(s => s.GetCustomAttributes(typeof (ViewMappedConfigTypeAttribute), false))
                    .Where(w => w.Any())
                    .Select(s => s[0])
                    .Cast<ViewMappedConfigTypeAttribute>()
                    .ToList().AsReadOnly();

            return m_ViewMappedConfigTypeCollection;
        }
    }
}