using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GT_MMI.Interface.Button;

namespace GT_MMI.Button
{
    class ButtonManager : IButtonManager
    {
        public event EventHandler<ButtonMouseEventArgs> MouseEvent;

        public ButtonManager()
        {
            ConstructButtons();
        }

        public Dictionary<ButtonType, IButtonControl> ButtonControlDictionary { get; private set; }

        public IButtonControl this[ButtonType type]
        {
            get { return ButtonControlDictionary[type]; }
        }


        private void ConstructButtons()
        {
            ButtonControlDictionary = new Dictionary<ButtonType, IButtonControl>();
            // TODO
        }


        public void CheckButtonState()
        {
            // TODO check button if mouse down or up
        }

        public void OnPaint(Graphics g)
        {
            foreach (var buttonControl in ButtonControlDictionary)
            {
                buttonControl.Value.OnPaint(g);
            }
        }
    }
}
