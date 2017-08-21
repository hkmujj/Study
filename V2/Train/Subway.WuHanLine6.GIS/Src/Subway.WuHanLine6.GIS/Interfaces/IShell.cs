using System.Windows;

namespace Subway.WuHanLine6.GIS.Interfaces
{
    public interface IShell
    {
        UIElement Map { get; }
        UIElement Station { get; }
        void ChangeToMap();
        void ChangeToStaion();
    }
}