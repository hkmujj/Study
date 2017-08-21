using System;
using System.Drawing;
using CommonUtil.Rtf;

namespace Motor.ATP.Infrasturcture.Control
{
    public static class RtfHelper
    {
        public static RtfDocument CreateDefaultPopupViewRtf(Action<RtfFormattedParagraph> contentUpdate)
        {
            var rtf = new RtfDocument();
            rtf.FontTable.Add(new RtfFont("Calibri"));
            rtf.ColorTable.Add(new RtfColor(Color.White));
            var formatting = new RtfParagraphFormatting(18, RtfTextAlign.Left)
            {
                TextColorIndex = 1,
                LineSpacingPercent = 3,
                FirstLineIndent = 0
            };
            var paragraph = new RtfFormattedParagraph { Formatting = formatting };
            contentUpdate(paragraph);
            rtf.Contents.Add(paragraph);
            return rtf;
        }
    }
}