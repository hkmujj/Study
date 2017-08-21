using System.Runtime.Remoting.Messaging;
using System.Windows;
using System.Windows.Controls;

namespace Engine.LCDM.HDX2.Entity.TemplateSelector
{
    public class TextContentControlTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OnLineTexTemplate { set; get; }

        public DataTemplate DoubleLineTexTemplate { set; get; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null || item == DependencyProperty.UnsetValue)
            {
                return OnLineTexTemplate;    
            }
            var arr = item as string[];
            if (arr != null)
            {
                if (arr.Length > 1)
                {
                    return DoubleLineTexTemplate;
                }
            }

            return OnLineTexTemplate;
        }
    }
}