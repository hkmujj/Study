using System.Collections.Generic;
using Microsoft.Practices.Prism.Events;

namespace Subway.WuHanLine6.Events
{
    /// <summary>
    /// Float 量变化事件
    /// </summary>
    public class FloatChangedEvent : CompositePresentationEvent<IDictionary<int, float>>
    {
    }
}