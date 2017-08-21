using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using System.Windows.Media;
using Engine._6A.CommonControl;

namespace Engine._6A.Constance
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class MouseEventClass
    {
        /// <summary>
        /// 设置背景色
        /// </summary>
        /// <param name="col">所有派生于System.Windows.Controls.Control的控件</param>
        /// <param name="brush">ImageBrush画刷</param>
        public void SetBackground(Control col, Brush brush)
        {
            col.Background = brush;
        }

        public void SetBorderBrush(Control col, Brush brush)
        {
            col.BorderBrush = brush;
        }
        /// <summary>
        /// 设置背景色
        /// </summary>
        /// <param name="cols">控件字典</param>
        /// <param name="brush">ImageBrush画刷</param>
        public void ResetBackground(IDictionary<string, Control> cols, Brush brush)
        {
            foreach (var value in cols.Values)
            {
                value.Background = brush;
            }
        }
        /// <summary>
        /// 设置背景色
        /// </summary>
        /// <param name="cols">控件字典</param>
        /// <param name="brush">ImageBrush画刷</param>
        public void ResetBackground(IDictionary<string, object[]> cols)
        {
            foreach (var value in cols.Values)
            {
                ((Control)value[2]).Background = (Brush)value[1];
            }
        }
        /// <summary>
        /// 设置边框
        /// </summary>
        /// <param name="cols">控件字典</param>
        /// <param name="brush">ImageBrush画刷</param>
        public void ResetBorderBrush(IDictionary<string, Control> cols, Brush brush)
        {
            foreach (var value in cols.Values)
            {
                value.BorderBrush = brush;
            }
        }

        /// <summary>
        /// 设置前景色
        /// </summary>
        /// <param name="col">所有派生于System.Windows.Controls.Control的控件</param>
        /// <param name="brush">ImageBrush画刷</param>
        public void SetForground(Control col, Brush brush)
        {
            col.Foreground = brush;
        }
        /// <summary>
        /// 设置背景色
        /// </summary>
        /// <param name="col">RecImageButton控件</param>
        /// <param name="brush">ImageBrush画刷</param>
        public void SetRecImageButtonBrush(RecImageButton col, ImageBrush brush)
        {
            col.ImageBrush = brush;
        }
    }
}