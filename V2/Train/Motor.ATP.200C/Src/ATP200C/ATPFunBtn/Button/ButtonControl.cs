using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GT_MMI.Interface.Button;

namespace GT_MMI.Button
{
    class ButtonControl : IButtonControl
    {
        public ButtonControl(ButtonType buttonType, Point location)
        {
            ButtonType = buttonType;

            m_ButtonRectangle = new Rectangle(location, m_DefaulceSize);
        }

        // ReSharper disable once InconsistentNaming
        private static readonly Size m_DefaulceSize = new Size(80, 60);

        private Rectangle m_ButtonRectangle;

        public ButtonType ButtonType { get; private set; }

        public bool Enable { get; set; }

        public bool Visible { get; set; }

        public string Text { get; set; }

        public Image Resoure { get; set; }

        public void OnPaint(Graphics g)
        {
            if (Visible)
            {
                if (Enable)
                {
                    
                }
                else
                {
                    
                }
            }
        }
    }
}
