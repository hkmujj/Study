using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Text;

namespace GT_MMI.Interface.Button
{
    /// <summary>
    /// 按钮控制器
    /// </summary>
    interface IButtonControl
    {
        /// <summary>
        /// 按钮类型
        /// </summary>
        ButtonType ButtonType { get; }

        /// <summary>
        /// 按钮能否响应
        /// </summary>
        bool Enable { set; get; }

        /// <summary>
        /// 按钮可见
        /// </summary>
        bool Visible { set; get; }

        /// <summary>
        /// 按钮上文字
        /// </summary>
        string Text { set; get; }

        /// <summary>
        /// 图片资源
        /// </summary>
        Image Resoure { set; get; }

        /// <summary>
        /// 绘制按钮
        /// </summary>
        /// <param name="g"></param>
        void OnPaint(Graphics g);

    }
}
