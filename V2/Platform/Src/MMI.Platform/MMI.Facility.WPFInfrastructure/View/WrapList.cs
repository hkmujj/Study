using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Mmi.Common.Control.WPF
{/// <summary>
    /// WrapList
    /// </summary>
    public class WrapList : WrapPanel
    {
        /// <summary>
        /// 
        /// </summary>
        public WrapList()
        {
            Orientation = Orientation.Horizontal;
        }

        /// <summary>
        /// 子UI
        /// </summary>
        // ReSharper disable once InconsistentNaming
        protected List<UIElement> m_ChildernUis;


        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// 
        /// </summary>
        protected bool m_ItemsChanged;

        #region ItemTemplate

        /// <summary>
        /// ItemTemplate Dependency Property
        /// </summary>
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(CollapsibleWrapList),
                new FrameworkPropertyMetadata((DataTemplate)null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the ItemTemplate property. This dependency property 
        /// indicates ....
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        #endregion

        /// <summary>
        /// ItemsSource Dependency Property
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(WrapList),
                new FrameworkPropertyMetadata((object)null,
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    new PropertyChangedCallback(OnItemsSourceChanged)));

        /// <summary>
        /// Gets or sets the ItemsSource property. This dependency property 
        /// indicates ....
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Handles changes to the ItemsSource property.
        /// </summary>
        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (WrapList)d;
            var oldItemsSource = (IEnumerable)e.OldValue;
            var newItemsSource = target.ItemsSource;
            target.OnItemsSourceChanged(oldItemsSource, newItemsSource);
        }


        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the ItemsSource property.
        /// </summary>
        protected virtual void OnItemsSourceChanged(IEnumerable oldItemsSource, IEnumerable newItemsSource)
        {
            m_ChildernUis = new List<UIElement>();
            m_ItemsChanged = true;
            if (newItemsSource != null)
            {
                foreach (var item in newItemsSource)
                {
                    m_ChildernUis.Add(new ContentControl() { Content = item, ContentTemplate = ItemTemplate });
                }
            }
        }

        /// <summary>
        /// 测量 <see cref="T:System.Windows.Controls.WrapPanel"/> 的子元素，以便准备在 <see cref="M:System.Windows.Controls.WrapPanel.ArrangeOverride(System.Windows.Size)"/> 传递过程中排列它们。
        /// </summary>
        /// <returns>
        /// <see cref="T:System.Windows.Size"/>，表示元素的所需大小。
        /// </returns>
        /// <param name="constraint">不应超过的上限 <see cref="T:System.Windows.Size"/>。</param>
        protected override Size MeasureOverride(Size constraint)
        {
            if (ItemsSource != null && m_ItemsChanged)
            {
                m_ItemsChanged = false;
                InternalChildren.Clear();
                m_ChildernUis.ForEach(e => InternalChildren.Add(e));
            }

            return base.MeasureOverride(constraint);
        }
    }
}
