using System.Windows;
using System.Windows.Controls;
using Subway.ShenZhenLine9.Models.Units;

namespace Subway.ShenZhenLine9.Models
{
    public class BogieAndBrekeDatatempateSelector : DataTemplateSelector
    {

        public DataTemplate BrakeTemplate { get; set; }
        public DataTemplate BogieTemplate { get; set; }
        /// <summary>在派生类中重写时，基于自定义逻辑返回 <see cref="T:System.Windows.DataTemplate" />。</summary>
        /// <returns>返回 <see cref="T:System.Windows.DataTemplate" /> 或 null。默认值为 null。</returns>
        /// <param name="item">要为其选择模板的数据对象。</param>
        /// <param name="container">数据绑定对象。</param>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var flag = item.GetType() == typeof(BrakeState);
            if (flag)
            {
                return BrakeTemplate;
            }

            flag = item.GetType() == typeof(bool);
            if (flag)
            {
                return BogieTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
