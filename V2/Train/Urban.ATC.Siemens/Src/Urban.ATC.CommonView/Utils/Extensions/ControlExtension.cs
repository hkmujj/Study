using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CommonUtil.Util;
using Motor.ATP.Domain.Interface;
using Motor.ATP.Domain.Interface.Service;

namespace Motor.ATP.CommonView.Utils.Extensions
{

    public static class ControlExtension
    {
        private static readonly Action<Control, bool> UpdateVisibleAction = (control, visible) => control.Visible = visible;
        private static readonly Action<Control, bool> UpdateEnableAction = (control, enable) => control.Enabled = enable;

        public static void InvokeIfNeed(this Control control, Delegate func, params object[] args)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(func, args);
            }
            else
            {
                func.DynamicInvoke(args);
            }
        }

        public static void InvokeUpdateVisibleIfNeed(this Control control, bool visible)
        {
            control.InvokeIfNeed(UpdateVisibleAction, control, visible);
        }

        public static void InvokeUpdateEnableIfNeed(this Control control, bool enable)
        {
            control.InvokeIfNeed(UpdateEnableAction, control, enable);
        }

        /// <summary>
        /// 去掉 Padding 后的区域
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static Rectangle GetRectangleWithoutPadding(this Control control)
        {
            return new Rectangle(control.Padding.Left, control.Padding.Top, control.Width-control.Padding.Horizontal, control.Height - control.Padding.Vertical);
        }

        /// <summary>
        /// 去掉 Margion 后区域
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static Rectangle GetRectangleWithoutMargion(this Control control)
        {
            return new Rectangle(control.Margin.Left, control.Margin.Top, control.Width - control.Margin.Horizontal, control.Height - control.Margin.Vertical);
        }

        /// <summary>
        /// 去掉 Padding,Margion 后的区域
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static Rectangle GetActureRectangle(this Control control)
        {
            return new Rectangle(control.Margin.Left + control.Padding.Left, control.Margin.Top + control.Padding.Top,
                control.Width - control.Margin.Horizontal - control.Padding.Horizontal,
                control.Height - control.Margin.Vertical - control.Padding.Vertical);
        }

        /// <summary>
        /// 去掉 Padding,Margion 后的区域
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static RectangleF GetActureRectangleF(this Control control)
        {
            return new RectangleF(control.Margin.Left + control.Padding.Left, control.Margin.Top + control.Padding.Top,
                control.Width - control.Margin.Horizontal - control.Padding.Horizontal,
                control.Height - control.Margin.Vertical - control.Padding.Vertical);
        }

        /// <summary>
        /// 添加一个透明层， 修改程序亮度
        /// </summary>
        /// <param name="control"></param>
        /// <param name="opuaqureValue"></param>
        public static void AddOpuaqueLayer(this Control control, IOther opuaqureValue)
        {
            IOpaqueLayerService service = opuaqureValue.Parent.ServiceManager.GetService<IOpaqueLayerService>();
            control.Paint -= service.PaintOpaqueLayer;
            control.Paint += service.PaintOpaqueLayer;
            opuaqureValue.PropertyChanged += (sender, args) => OpuaqureValueOnPropertyChanged(control, sender, args);
        }

        /// <summary>
        /// 删除添加的透明层。
        /// </summary>
        /// <param name="control"></param>
        /// <param name="opuaqureValue"></param>
        public static void ClearOpuaqueLayer(this Control control, IOther opuaqureValue)
        {
            IOpaqueLayerService service = opuaqureValue.Parent.ServiceManager.GetService<IOpaqueLayerService>();
            control.Paint -= service.PaintOpaqueLayer;
            opuaqureValue.PropertyChanged -= (sender, args) => OpuaqureValueOnPropertyChanged(control, sender, args);
        }

        private static void OpuaqureValueOnPropertyChanged(Control control, object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs .PropertyName == PropertySupport.ExtractPropertyName<IOther, float>(a => a.Light))
            {
                control.Invalidate();
            }
        }
    }
}