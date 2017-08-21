using System;
using System.IO;
using System.Windows.Forms;

namespace Motor.ATP.CommonView.Utils.Extensions
{
    public static class RichTextBoxExtension
    {
        private static readonly Action<RichTextBox, Stream, RichTextBoxStreamType> LoadFileAction =
            (box, stream, type) =>
            {
                box.LoadFile(stream, type);
            };

        public static readonly Action<RichTextBox, string> UpdateRtfAction = (box, s) => { box.Rtf = s; };

        /// <summary>
        /// LoadFile
        /// </summary>
        /// <param name="richText"></param>
        /// <param name="stream"></param>
        /// <param name="type"></param>
        public static void InvokeLoadFileIfNeed(this RichTextBox richText, Stream stream, RichTextBoxStreamType type)
        {
            richText.InvokeIfNeed(LoadFileAction, richText, stream, type);
        }

        public static void InvokeUpdateRtfIfNeed(this RichTextBox richText, string rtf)
        {
            richText.InvokeIfNeed(UpdateRtfAction, richText, rtf);
        }
    }
}