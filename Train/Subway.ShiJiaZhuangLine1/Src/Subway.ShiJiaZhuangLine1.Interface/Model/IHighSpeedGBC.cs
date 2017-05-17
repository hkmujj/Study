using System.Windows;

namespace Subway.ShiJiaZhuangLine1.Interface.Model
{
    public interface IHighSpeedGBC : IViewModel
    {
        Visibility HighSpeedGCBVisibilityOne { get; }
        Visibility HighSpeedGCBVisibilityTwo { get; }
        Visibility HighSpeedGCBVisibilityThree { get; }
        Visibility HighSpeedGCBVisibilityFour { get; }
        Visibility HighSpeedGCBVisibilityFive { get; }
        Visibility HighSpeedGCBVisibilitySix { get; }
        Visibility HighSpeedGCBVisibilitySeven { get; }
        Visibility HighSpeedGCBVisibilityEight { get; }

    }
}