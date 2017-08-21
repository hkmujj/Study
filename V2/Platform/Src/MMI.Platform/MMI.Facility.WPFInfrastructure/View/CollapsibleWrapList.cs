using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mmi.Common.Control.WPF
{
    /// <summary>
    /// CollapsibleWrapList
    /// </summary>
    public class CollapsibleWrapList : WrapList
    {
        /// <summary>
        /// 
        /// </summary>
        public CollapsibleWrapList()
        {
            Orientation = Orientation.Vertical;
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the ItemsSource property.
        /// </summary>
        protected override void OnItemsSourceChanged(IEnumerable oldItemsSource, IEnumerable newItemsSource)
        {
            UpdateChildren();
        }


        #region HeaderTemplate

        /// <summary>
        /// HeaderTemplate Dependency Property
        /// </summary>
        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(CollapsibleWrapList),
                new FrameworkPropertyMetadata((DataTemplate)null,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the HeaderTemplate property. This dependency property 
        /// indicates ....
        /// </summary>
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        #endregion

        #region HeaderStyle

        /// <summary>
        /// HeaderStyle Dependency Property
        /// </summary>
        public static readonly DependencyProperty HeaderStyleProperty =
            DependencyProperty.Register("HeaderStyle", typeof(Style), typeof(CollapsibleWrapList),
                new FrameworkPropertyMetadata((Style)null));

        /// <summary>
        /// Gets or sets the HeaderStyle property. This dependency property 
        /// indicates ....
        /// </summary>
        public Style HeaderStyle
        {
            get { return (Style)GetValue(HeaderStyleProperty); }
            set { SetValue(HeaderStyleProperty, value); }
        }

        #endregion


        bool m_GroupChanged;

        void UpdateChildren()
        {//����_5_1_a_s_p_x
            var groups = ItemsSource;
            m_ChildernUis = new List<UIElement>();
            m_GroupChanged = true;

            foreach (var g in groups)
            {
                var converted = GroupingWrapper.Create(g);

                var groupHeader = new WrapListGroupHeader()
                {
                    Content = converted.Key,
                    ContentTemplate = HeaderTemplate,
                    IsChecked = true,
                    Style = HeaderStyle
                };

                groupHeader.Click += (ss, ee) =>
                {
                    if (converted.Any())
                    {
                        m_GroupChanged = true;
                        InvalidateMeasure();
                    }
                };

                m_ChildernUis.Add(groupHeader);

                foreach (var item in converted)
                {
                    var childContent = new ContentControl() { Content = item, ContentTemplate = ItemTemplate };
                    m_ChildernUis.Add(childContent);
                }
            }
        }


        /// <summary>
        /// ���� <see cref="T:System.Windows.Controls.WrapPanel"/> ����Ԫ�أ��Ա�׼���� <see cref="M:System.Windows.Controls.WrapPanel.ArrangeOverride(System.Windows.Size)"/> ���ݹ������������ǡ�
        /// </summary>
        /// <returns>
        /// <see cref="T:System.Windows.Size"/>����ʾԪ�ص������С��
        /// </returns>
        /// <param name="availableSize">��Ӧ���������� <see cref="T:System.Windows.Size"/>��</param>
        protected override Size MeasureOverride(Size availableSize)
        {
            if (m_GroupChanged && ItemsSource != null)
            {
                InternalChildren.Clear();

                bool add = false;
                foreach (var ui in m_ChildernUis)
                {
                    var group = ui as WrapListGroupHeader;
                    if (group != null)
                    {
                        add = group.IsChecked.Value;
                        InternalChildren.Add(ui);
                    }
                    else if (add)
                        InternalChildren.Add(ui);
                }
                m_GroupChanged = false;
            }

            return base.MeasureOverride(availableSize);
        }


    }
}