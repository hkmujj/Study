using System;
using System.Windows.Forms;

namespace Motor.ATP.CommonView.Utils.Extensions
{
    public static class TextBoxBaseExtension
    {
        private static readonly Action<TextBoxBase, string> UpdateTextAction = (label, s) => label.Text = s;

        public static void InvokeUpdateTextIfNeed(this TextBoxBase label, string text)
        {
            label.InvokeIfNeed(UpdateTextAction, label, text);
        }
    }
}