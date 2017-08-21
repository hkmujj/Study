using System.Windows;
using System.Windows.Controls;
using Subway.XiaMenLine1.Interface.Enum;

namespace Subway.XiaMenLine1.Subsystem.View.Contents.HelpChildren
{
    internal class AssistStateItem
    {
        public AssistPowerStatus Inverter { set; get; }

        public AssistPowerStatus Charge { set; get; }
        public bool IsSingle { get; set; }
    }

    internal class AssistStateDataTempleteSelector : DataTemplateSelector
    {
        public DataTemplate NormalTemplate { get; set; }
        public DataTemplate SingleTemplate { get; set; }

        /// <summary>
        /// 在派生类中重写时，基于自定义逻辑返回 <see cref="T:System.Windows.DataTemplate"/>。
        /// </summary>
        /// <returns>
        /// 返回 <see cref="T:System.Windows.DataTemplate"/> 或 null。默认值为 null。
        /// </returns>
        /// <param name="item">要为其选择模板的数据对象。</param><param name="container">数据绑定对象。</param>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var tmp = item as AssistStateItem;
            if (tmp == null)
            {
                return NormalTemplate;
            }
            return tmp.IsSingle ? SingleTemplate : NormalTemplate;
        }
    }
}