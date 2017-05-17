using System.Windows;
using System.Windows.Controls;

namespace Subway.ShiJiaZhuangLine1.Subsystem.View.Contents.HelpChildren
{
    /// <summary>
    /// HelpGroupContent.xaml 的交互逻辑
    /// </summary>
    public partial class HelpGroupContent : UserControl
    {
        public HelpGroupContent()
        {
            InitializeComponent();
            ContentMargin = new Thickness(6);
        }

        public static readonly DependencyProperty ContentMarginProperty = DependencyProperty.Register(
            "ContentMargin", typeof (Thickness), typeof (HelpGroupContent), new PropertyMetadata(default(Thickness)));

        public Thickness ContentMargin
        {
            get { return (Thickness) GetValue(ContentMarginProperty); }
            set { SetValue(ContentMarginProperty, value); }
        }

        public static readonly DependencyProperty GroupNameProperty = DependencyProperty.Register(
            "GroupName", typeof(string), typeof(HelpGroupContent), new PropertyMetadata(default(string)));

        public string GroupName
        {
            get { return (string)GetValue(GroupNameProperty); }
            set { SetValue(GroupNameProperty, value); }
        }

        public static readonly DependencyProperty StateSourceProperty = DependencyProperty.Register(
            "StateSource", typeof(object[]), typeof(HelpGroupContent), new PropertyMetadata(default(object[])));

        public object[] StateSource
        {
            get { return (object[])GetValue(StateSourceProperty); }
            set { SetValue(StateSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
            "ItemTemplate", typeof(DataTemplate), typeof(HelpGroupContent), new PropertyMetadata(default(DataTemplate)));

        /// <summary>
        /// 显示内容的模板
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }


        public static readonly DependencyProperty ItemDataTemplateSelectorProperty = DependencyProperty.Register(
            "ItemDataTemplateSelector", typeof (DataTemplateSelector), typeof (HelpGroupContent), new PropertyMetadata(default(DataTemplateSelector)));

        public DataTemplateSelector ItemDataTemplateSelector
        {
            get { return (DataTemplateSelector) GetValue(ItemDataTemplateSelectorProperty); }
            set { SetValue(ItemDataTemplateSelectorProperty, value); }
        }
    }
}
