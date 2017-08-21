using System.Collections.Generic;
using Microsoft.Practices.Prism.Events;

namespace Subway.WuHanLine6.Events
{
    /// <summary>
    /// Bool 量变化事件
    /// </summary>
    public class BoolChangedEvent : CompositePresentationEvent<IDictionary<int, bool>>
    {
    }
}