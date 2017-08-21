using System.Windows;
using System.Windows.Controls;
using Subway.ShiJiaZhuangLine1.Interface.Enum;

namespace Subway.ShiJiaZhuangLine1.Subsystem.DataTemplateSelectors
{
    public class EvacuateAndDriverDoorStateDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EvacuateDoorDataTemplate { set; get; }

        public DataTemplate DriverDoorDataTemplate { set; get; }


        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var type = item.GetType();
            if (type == typeof(EvacuateDoorState))
            {
                return EvacuateDoorDataTemplate;
            }
            if (type == typeof(DriverDoorState))
            {
                return DriverDoorDataTemplate;
            }

            return null;
        }
    }
}