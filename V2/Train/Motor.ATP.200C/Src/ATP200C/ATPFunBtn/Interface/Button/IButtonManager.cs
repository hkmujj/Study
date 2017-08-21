using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace GT_MMI.Interface.Button
{
    interface IButtonManager
    {
        event EventHandler<ButtonMouseEventArgs> MouseEvent;

        //Dictionary<ButtonType, IButtonControl> ButtonControlDictionary { get; }

        IButtonControl this[ButtonType type] { get; }

        /// <summary>
        /// 更新按钮状态
        /// </summary>
        void CheckButtonState();

        void OnPaint(Graphics g);

    }
}
