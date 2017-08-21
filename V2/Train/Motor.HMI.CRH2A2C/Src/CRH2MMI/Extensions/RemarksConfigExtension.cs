using System.Collections.Generic;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Controls.Roundness;
using CRH2MMI.Common.Screen;

namespace CRH2MMI.Extensions
{
    public static class RemarksConfigExtension
    {
        public static IEnumerable<CommonInnerControlBase> ReflectViews(this RemarksConfig config)
        {
            if (config == null || !config.RemarkItems.Any())
            {
                yield break;
            }

            foreach (var it in config.RemarkItems)
            {
                var type = it.GetType();
                if (type == typeof(TextRemarkItem))
                {
                    yield return ReflectTextView((TextRemarkItem) it);
                }
                else if (type == typeof(RoudnessRemarkItem))
                {
                    yield return ReflectRoudnessView((RoudnessRemarkItem) it);
                }
            }
        }

        private static CommonInnerControlBase ReflectRoudnessView(RoudnessRemarkItem it)
        {
            return new GDIRoundness()
            {
                ContentColor = it.ForgroundColor,
                NeedDrawArc = false,
                OutLineRectangle = it.Rectangle,
                Center = it.Center,
                R = it.R,
                NeedDrawContent = true,
            };
        }

        private static CommonInnerControlBase ReflectTextView(TextRemarkItem it)
        {
            return new GDIRectText()
            {
                BackColorVisible = it.BackgroudColorVisible,
                BkColor = it.BackgroundColor,
                TextColor = it.ForgroundColor,
                Text = it.Text,
                OutLineRectangle = it.Rectangle,
                Bold = true,
            };
        }
    }
}