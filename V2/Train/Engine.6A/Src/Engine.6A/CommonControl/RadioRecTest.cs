using System.Windows;
using System.Windows.Controls;

namespace Engine._6A.CommonControl
{
    public class RadioRecTest : Button
    {
        static RadioRecTest()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RadioRecTest), new FrameworkPropertyMetadata(typeof(RadioRecTest)));
        }
    }
}