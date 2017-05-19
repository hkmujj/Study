using System.Windows;
using System.Windows.Controls;
using Subway.ShiJiaZhuangLine1.Interface.Enum;

namespace Subway.ShiJiaZhuangLine1.Subsystem.StyleSelectors
{
    public class EvacuateAndDriverDoorStateStyleSelector : StyleSelector
    {
        public Style EvacuateDoorStyle { set; get; }

        public Style DriverDoorStyle { set; get; }


        public override Style SelectStyle(object item, DependencyObject container)
        {
            var type = item.GetType();
            if (type == typeof(EvacuateDoorState))
            {
                return EvacuateDoorStyle;
            }
            if (type == typeof(DriverDoorState))
            {
                return DriverDoorStyle;
            }

            return null;
        }
    }
}