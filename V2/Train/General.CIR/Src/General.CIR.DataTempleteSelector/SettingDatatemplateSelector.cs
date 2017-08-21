using System;
using System.Windows;
using System.Windows.Controls;
using General.CIR.Models.Units;

namespace General.CIR.DataTempleteSelector
{
	public class SettingDatatemplateSelector : DataTemplateSelector
	{
		public DataTemplate NormalTemplate
		{
			get;
			set;
		}

		public DataTemplate ReciverTemplate
		{
			get;
			set;
		}

		public DataTemplate SpeakerTemplate
		{
			get;
			set;
		}

		public DataTemplate ScreenTemplate
		{
			get;
			set;
		}

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			SettingItem settingItem = item as SettingItem;
			DataTemplate result = NormalTemplate;
			bool flag = settingItem != null;
			if (flag)
			{
				switch (settingItem.Type)
				{
				case ItemType.Normal:
					result = NormalTemplate;
					break;
				case ItemType.Speaker:
					result = SpeakerTemplate;
					break;
				case ItemType.Recvice:
					result = ReciverTemplate;
					break;
				case ItemType.Screen:
					result = ScreenTemplate;
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
			return result;
		}
	}
}
