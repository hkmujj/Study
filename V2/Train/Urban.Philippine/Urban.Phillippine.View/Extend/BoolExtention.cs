using System.Windows;

namespace Urban.Phillippine.View.Extend
{
    public static class BoolExtention
    {
        public static Visibility ConvertVisibility(this bool value)
        {
            return value ? Visibility.Visible : Visibility.Hidden;
        }
    }
}